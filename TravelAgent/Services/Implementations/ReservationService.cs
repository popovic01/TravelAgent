using AutoMapper;
using TravelAgent.AppDbContext;
using TravelAgent.DTO.Common;
using TravelAgent.DTO.Reservation;
using TravelAgent.Model;
using TravelAgent.Services.Interfaces;

namespace TravelAgent.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ReservationService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ResponsePackageNoData Add(ReservationDTO reservation)
        {
            var retVal = new ResponsePackageNoData();

            _context.Reservations.Add(_mapper.Map<Reservation>(reservation));
            _context.SaveChanges();
            retVal.Message = $"Successfully added Reservation {reservation.ReservationCode}";

            return retVal;
        }

        public ResponsePackageNoData Delete(int id)
        {
            var retVal = new ResponsePackageNoData();

            var reservation = _context.Reservations.FirstOrDefault(x => x.Id == id);

            if (reservation == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No Reservation with ID {id}";
            }
            else
            {
                _context.Reservations.Remove(reservation);
                _context.SaveChanges();
                retVal.Message = $"Successfully deleted Reservation {reservation.ReservationCode}";
            }
            return retVal;
        }

        public ResponsePackage<ReservationDTO> Get(int id)
        {
            var retVal = new ResponsePackage<ReservationDTO>();

            var reservation = _context.Reservations.FirstOrDefault(x => x.Id == id);

            if (reservation == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No Reservation with ID {id}";
            }
            else
                retVal.TransferObject = _mapper.Map<ReservationDTO>(reservation);

            return retVal;
        }

        public PaginationDataOut<ReservationDTO> GetAll()
        {
            PaginationDataOut<ReservationDTO> retVal = new PaginationDataOut<ReservationDTO>();

            IQueryable<Reservation> reservations = _context.Reservations;

            retVal.Count = reservations.Count();

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
                retVal.Message = $"No Reservation with ID {id}";
            }
            else
            {
                reservationDb.Date = reservation.Date;
                reservationDb.ReservationCode = reservation.ReservationCode;
                reservationDb.Client = (Client)_context.Users.FirstOrDefault(x => x.Id == reservation.ClientId);
                reservationDb.Offer = _context.Offers.FirstOrDefault(x => x.Id == reservation.OfferId);
                _context.SaveChanges();
                retVal.Message = $"Successfully updated Reservation {reservation.ReservationCode}";
            }

            return retVal;
        }
    }
}
