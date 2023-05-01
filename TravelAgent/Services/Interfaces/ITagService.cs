using TravelAgent.DTO.Common;
using TravelAgent.DTO.Tag;

namespace TravelAgent.Services.Interfaces
{
    public interface ITagService
    {
        public PaginationDataOut<TagIdDTO> GetAll(FilterParamsDTO filterParams);
        public ResponsePackage<TagDTO> Get(int id);
        public ResponsePackageNoData Add(TagDTO tag);
        public ResponsePackageNoData Update(int id, TagDTO tag);
        public ResponsePackageNoData Delete(int id);
    }
}
