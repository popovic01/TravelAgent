using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;
using TravelAgent.AppDbContext;
using TravelAgent.DTO.Common;
using TravelAgent.DTO.Reservation;
using TravelAgent.Helpers;
using TravelAgent.Model;
using TravelAgent.Services.Interfaces;

namespace TravelAgent.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICommonHelper _commonHelper;

        public ReservationService(ApplicationDbContext context, IMapper mapper, 
            ICommonHelper commonHelper)
        {
            _context = context;
            _mapper = mapper;
            _commonHelper = commonHelper;
        }

        public ResponsePackageNoData Add(ReservationDTO reservation)
        {
            var retVal = new ResponsePackage<string>();

            try
            {
                var reservationInDb = _context.Reservations
                    .Include(x => x.Offer)
                    .FirstOrDefault(x => x.Client.Id == reservation.ClientId && x.Offer.Id == reservation.OfferId);
                if (reservationInDb != null)
                {
                    retVal.Message = $"Već ste rezervisali ponudu {reservationInDb.Offer.Name}";
                    retVal.Status = 409;
                    return retVal;
                }
                var reservationDb = new Reservation()
                {
                    ReservationCode = _commonHelper.RandomString(6),
                    Date = reservation.Date,
                    Offer = _context.Offers.FirstOrDefault(x => x.Id == reservation.OfferId),
                    Client = (Client)_context.Users.FirstOrDefault(x => x.Id == reservation.ClientId)
                };

                _context.Reservations.Add(reservationDb);
                _commonHelper.ExecuteProcedure("BUY_OFFER_PROCEDURE", reservation.OfferId, 1);

                //stripe settings 
                var domain = "http://localhost:4200/";
                //we are creating session
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string>
                    {
                      "card",
                    },
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    SuccessUrl = domain + "success",
                    CancelUrl = domain + "failure",
                };

                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(reservationDb.Offer.Price * 100),//20.00 -> 2000
                        Currency = "eur",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = reservationDb.Offer.Name,
                            Description = reservationDb.Offer.Description
                        },
                    },
                    Quantity = 1
                };
                options.LineItems.Add(sessionLineItem);

                var service = new SessionService();
                Session session = service.Create(options);

                reservationDb.SessionId = session.Id;
                _context.SaveChanges();
                retVal.Message = $"Uspešno dodata rezervacija {reservationDb.ReservationCode}";
                retVal.TransferObject = session.Id;
            }
            catch (Exception)
            {
                retVal.Message = "Došlo je do greške";
                retVal.Status = 400;
            }

            return retVal;
        }

        public ResponsePackageNoData Delete(int id)
        {
            var retVal = new ResponsePackageNoData();

            var reservation = _context.Reservations
                .Include(x => x.Offer)
                .FirstOrDefault(x => x.Id == id);

            if (reservation == null)
            {
                retVal.Status = 404;
                retVal.Message = $"Ne postoji rezervacija sa id-jem {id}";
            }
            else
            {
                _context.Reservations.Remove(reservation);
                _commonHelper.ExecuteProcedure("BUY_OFFER_PROCEDURE", reservation.Offer.Id, 0);
                _context.SaveChanges();
                retVal.Message = $"Uspešno obrisana rezervacija {reservation.ReservationCode}";
            }
            return retVal;
        }

        public ResponsePackage<ReservationResponseDTO> Get(int id)
        {
            var retVal = new ResponsePackage<ReservationResponseDTO>();

            var reservation = _context.Reservations
                .Include(x => x.Offer)
                .Include(x => x.Client)
                .FirstOrDefault(x => x.Id == id);

            if (reservation == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No postoji rezervacija sa id-jem {id}";
            }
            else
                retVal.TransferObject = _mapper.Map<ReservationResponseDTO>(reservation);

            return retVal;
        }

        public PaginationDataOut<ReservationResponseDTO> GetAll(PageInfo pageInfo, int id)
        {
            PaginationDataOut<ReservationResponseDTO> retVal = new ();

            IQueryable<Reservation> reservations = _context.Reservations
                .Include(x => x.Offer)
                .Include(x => x.Client);

            if (id != 0)
                reservations = reservations.Where(x => x.Client.Id == id);

            retVal.Count = reservations.Count();

            reservations = reservations
                .OrderByDescending(x => x.Id);

            reservations.ToList().ForEach(x => retVal.Data.Add(_mapper.Map<ReservationResponseDTO>(x)));
            return retVal;
        }

        public ResponsePackageNoData Update(int id, ReservationDTO reservation)
        {
            var retVal = new ResponsePackageNoData();

            var reservationDb = _context.Reservations
                .FirstOrDefault(x => x.Id == id);

            if (reservationDb == null)
            {
                retVal.Status = 404;
                retVal.Message = $"Ne postoji rezervacija sa id-jem {id}";
            }
            else
            {
                reservationDb.Date = reservation.Date;
                reservationDb.ReservationCode = reservation.ReservationCode;
                reservationDb.Client = (Client)_context.Users.FirstOrDefault(x => x.Id == reservation.ClientId);
                reservationDb.Offer = _context.Offers.FirstOrDefault(x => x.Id == reservation.OfferId);
                _context.SaveChanges();
                retVal.Message = $"Uspešno izmenjena rezervacija {reservation.ReservationCode}";
            }

            return retVal;
        }

        public ResponsePackageNoData WebHook(string payload)
        {
            var retVal = new ResponsePackageNoData();

            var stripeObject = Newtonsoft.Json.JsonConvert.DeserializeObject<StripeDTO>(payload);
            if (stripeObject.Type.Equals("checkout.session.completed"))
            {
                var reservationDb = _context.Reservations.OrderByDescending(x => x.Id).FirstOrDefault();
                reservationDb.PaymentIntent = stripeObject.Data.Object.PaymentIntent;
                _context.SaveChanges();
            }
            return retVal;
        }
    }
}
