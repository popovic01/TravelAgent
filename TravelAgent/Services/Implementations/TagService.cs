using AutoMapper;
using TravelAgent.AppDbContext;
using TravelAgent.DTO.Common;
using TravelAgent.DTO.Tag;
using TravelAgent.Model;
using TravelAgent.Services.Interfaces;

namespace TravelAgent.Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TagService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ResponsePackageNoData Add(TagDTO tag)
        {
            var retVal = new ResponsePackageNoData();

            var tagDb = _context.Tags.FirstOrDefault(x => x.Name == tag.Name);

            if (tagDb != null)
            {
                retVal.Message = $"Already exists Tag {tag.Name}";
                retVal.Status = 409;
            }
            else
            {
                _context.Tags.Add(_mapper.Map<Tag>(tag));
                _context.SaveChanges();
                retVal.Message = $"Successfully added Tag {tag.Name}";
            }

            return retVal;
        }

        public ResponsePackageNoData Delete(int id)
        {
            var retVal = new ResponsePackageNoData();

            var tag = _context.Tags.FirstOrDefault(x => x.Id == id);

            if (tag == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No Tag with ID {id}";
            }
            else
            {
                _context.Tags.Remove(tag);
                _context.SaveChanges();
                retVal.Message = $"Successfully deleted Tag {tag.Name}";
            }
            return retVal;
        }

        public ResponsePackage<TagDTO> Get(int id)
        {
            var retVal = new ResponsePackage<TagDTO>();

            var tag = _context.Tags
                .FirstOrDefault(x => x.Id == id);

            if (tag == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No Tag with ID {id}";
            }
            else
                retVal.TransferObject = _mapper.Map<TagDTO>(tag);

            return retVal;
        }

        public PaginationDataOut<TagDTO> GetAll(FilterParamsDTO filterParams)
        {
            PaginationDataOut<TagDTO> retVal = new PaginationDataOut<TagDTO>();

            IQueryable<Tag> tags = _context.Tags;

            if (!string.IsNullOrWhiteSpace(filterParams.SearchFilter))
            {
                tags = tags.Where(x => x.Name.ToLower().Contains(filterParams.SearchFilter));
            }
            retVal.Count = tags.Count();

            tags = tags
                .OrderByDescending(x => x.Id)
                .Skip(filterParams.PageSize * (filterParams.Page - 1))
                .Take(filterParams.PageSize);

            tags.ToList().ForEach(x => retVal.Data.Add(_mapper.Map<TagDTO>(x)));
            return retVal;
        }

        public ResponsePackageNoData Update(int id, TagDTO tag)
        {
            var retVal = new ResponsePackageNoData();

            var tagDb = _context.Tags
                .FirstOrDefault(x => x.Id == id);
            var tagNameDb = _context.Tags
                .FirstOrDefault(x => x.Name.ToLower() == tag.Name.ToLower());

            if (tagDb == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No Tag with ID {id}";
            }
            else if (tagNameDb != null)
            {
                retVal.Message = $"Already exists Tag {tag.Name}";
                retVal.Status = 409;
            }
            else
            {
                tagDb.Name = tag.Name;
                _context.SaveChanges();
                retVal.Message = $"Successfully updated Tag {tag.Name}";
            }

            return retVal;
        }
    }
}
