using TravelAgent.DTO.Common;
using TravelAgent.DTO.Reservation;

namespace TravelAgent.Services.Interfaces
{
    public interface IReservationService
    {
        public PaginationDataOut<ReservationResponseDTO> GetAll(PageInfo pageInfo, int id);
        public ResponsePackage<ReservationResponseDTO> Get(int id);
        public ResponsePackageNoData Add(ReservationDTO reservation);
        public ResponsePackageNoData Update(int id, ReservationDTO reservation);
        public ResponsePackageNoData Delete(int id);
        public ResponsePackageNoData WebHook(string payload);
    }
}
