using TravelAgent.DTO.Common;
using TravelAgent.DTO.Reservation;

namespace TravelAgent.Services.Interfaces
{
    public interface IReservationService
    {
        public PaginationDataOut<ReservationDTO> GetAll(PageInfo pageInfo);
        public PaginationDataOut<ReservationDTO> GetAllByUser(PageInfo pageInfo, int id);
        public ResponsePackage<ReservationDTO> Get(int id);
        public ResponsePackageNoData Add(ReservationDTO reservation);
        public ResponsePackageNoData Update(int id, ReservationDTO reservation);
        public ResponsePackageNoData Delete(int id);
    }
}
