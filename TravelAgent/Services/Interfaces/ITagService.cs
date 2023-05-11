using TravelAgent.DTO.Common;
using TravelAgent.DTO.Tag;

namespace TravelAgent.Services.Interfaces
{
    public interface ITagService
    {
        public PaginationDataOut<TagIdDTO> GetAll(PageInfo pageInfo);
        public ResponsePackageNoData Add(TagDTO tag);
        public ResponsePackageNoData Update(int id, TagDTO tag);
        public ResponsePackageNoData Delete(int id);
    }
}
