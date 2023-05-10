using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public ReservationService(ApplicationDbContext context, IMapper mapper, ICommonHelper commonHelper)
        {
            _context = context;
            _mapper = mapper;
            _commonHelper = commonHelper;
        }

        public ResponsePackageNoData Add(ReservationDTO reservation)
        {
            var retVal = new ResponsePackageNoData();

            try
            {
                var reservationDb = new Reservation()
                {
                    ReservationCode = _commonHelper.RandomString(6),
                    Date = reservation.Date,
                    Offer = _context.Offers.FirstOrDefault(x => x.Id == reservation.OfferId),
                    Client = (Client)_context.Users.FirstOrDefault(x => x.Id == reservation.ClientId)
                };

                _context.Reservations.Add(reservationDb);
                _commonHelper.ExecuteProcedure("BUY_OFFER_PROCEDURE", reservation.OfferId, 1);
                _context.SaveChanges();
                retVal.Message = $"Uspešno dodata rezervacija {reservationDb.ReservationCode}";
            }
            catch (Exception ex)
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

        public ResponsePackage<ReservationDTO> Get(int id)
        {
            var retVal = new ResponsePackage<ReservationDTO>();

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
                retVal.TransferObject = _mapper.Map<ReservationDTO>(reservation);

            return retVal;
        }

        public PaginationDataOut<ReservationDTO> GetAll(PageInfo pageInfo)
        {
            PaginationDataOut<ReservationDTO> retVal = new PaginationDataOut<ReservationDTO>();

            IQueryable<Reservation> reservations = _context.Reservations
                .Include(x => x.Offer)
                .Include(x => x.Client);

            retVal.Count = reservations.Count();

            reservations = reservations
                .OrderByDescending(x => x.Id)
                .Skip(pageInfo.PageSize * (pageInfo.Page - 1))
                .Take(pageInfo.PageSize);

            reservations.ToList().ForEach(x => retVal.Data.Add(_mapper.Map<ReservationDTO>(x)));
            return retVal;
        }

        public PaginationDataOut<ReservationDTO> GetAllByUser(PageInfo pageInfo, int id)
        {
            PaginationDataOut<ReservationDTO> retVal = new PaginationDataOut<ReservationDTO>();

            IQueryable<Reservation> reservations = _context.Reservations
                .Include(x => x.Offer)
                .Include(x => x.Client)
                .Where(x => x.Client.Id == id);

            retVal.Count = reservations.Count();

            reservations = reservations
                .OrderByDescending(x => x.Id)
                .Skip(pageInfo.PageSize * (pageInfo.Page - 1))
                .Take(pageInfo.PageSize);

            reservations.ToList().ForEach(x => retVal.Data.Add(_mapper.Map<ReservationDTO>(x)));
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
    }
}
