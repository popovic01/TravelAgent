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
                retVal.Message = $"Već postoji tag {tag.Name}";
                retVal.Status = 409;
            }
            else
            {
                _context.Tags.Add(_mapper.Map<Tag>(tag));
                _context.SaveChanges();
                retVal.Message = $"Uspešno dodat tag {tag.Name}";
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
                retVal.Message = $"Ne postoji tag sa id-jem {id}";
            }
            else
            {
                _context.Tags.Remove(tag);
                _context.SaveChanges();
                retVal.Message = $"Uspešno obrisan tag {tag.Name}";
            }
            return retVal;
        }

        public PaginationDataOut<TagIdDTO> GetAll(PageInfo pageInfo)
        {
            PaginationDataOut<TagIdDTO> retVal = new ();

            IQueryable<Tag> tags = _context.Tags;
            retVal.Count = tags.Count();

            if (!pageInfo.GetAll)
            {
                tags = tags
                    .OrderByDescending(x => x.Id)
                    .Skip(pageInfo.PageSize * (pageInfo.Page))
                    .Take(pageInfo.PageSize);
            }

            tags.ToList().ForEach(x => retVal.Data.Add(_mapper.Map<TagIdDTO>(x)));
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
                retVal.Message = $"Ne postoji tag sa id-jem {id}";
            }
            else if (tagNameDb != null)
            {
                retVal.Message = $"Već postoji tag {tag.Name}";
                retVal.Status = 409;
            }
            else
            {
                tagDb.Name = tag.Name;
                _context.SaveChanges();
                retVal.Message = $"Uspešno izmenjen tag {tag.Name}";
            }

            return retVal;
        }
    }
}
