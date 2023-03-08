using AutoMapper;
using TravelAgent.AppDbContext;
using TravelAgent.DTO.Common;
using TravelAgent.DTO.User;
using TravelAgent.Helpers;
using TravelAgent.Model;
using TravelAgent.Services.Interfaces;

namespace TravelAgent.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthHelper _auth;

        public UserService(ApplicationDbContext context, IMapper mapper, IAuthHelper auth)
        {
            _context = context;
            _mapper = mapper;
            _auth = auth;
        }

        public ResponsePackageNoData Register(UserRequestDTO dataIn)
        {
            var retVal = new ResponsePackageNoData();

            var userDb = _context.Users.FirstOrDefault(x => x.Username == dataIn.Username);

            if (userDb != null)
            {
                retVal.Message = $"Already exists User {dataIn.Username}";
                retVal.Status = 409;
            }
            else
            {
                _auth.CreatePasswordHash(dataIn.Password, out byte[] passwordHash, out byte[] passwordSalt);
                User user = new User();
                user.Username = dataIn.Username;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt; 

                _context.Users.Add(user);
                _context.SaveChanges();
                retVal.Message = $"Successfully registered User {dataIn.Username}";

                string token = _auth.CreateToken(user);
                //HttpContext.Session.SetString(StaticDetails.UserToken, token);
                //HttpContext.Session.SetString(StaticDetails.Role, user.TypeOfUser);
            }

            return retVal;
        }

        public ResponsePackageNoData Login(UserRequestDTO user)
        {
            var retVal = new ResponsePackageNoData();

            var userFromDb = _context.Users.FirstOrDefault(u => u.Username == user.Username);

            if (userFromDb != null)
            {
                if (_auth.VerifyPasswordHash(user.Password, userFromDb.PasswordHash, userFromDb.PasswordSalt))
                {
                    string token = _auth.CreateToken(userFromDb);
                    //HttpContext.Session.SetString(StaticDetails.UserToken, token);
                    //HttpContext.Session.SetString(StaticDetails.Role, userFromDb.TypeOfUser);

                    retVal.Message = $"You logged in successfully";
                }
                else
                {
                    retVal.Message = $"Password is not correct";
                    retVal.Status = 401;
                }
            }
            else
            {
                retVal.Message = $"Username {user.Username} doesn't exist";
                retVal.Status = 404;
            }

            return retVal;
        }

        public ResponsePackageNoData Logout(UserRequestDTO user)
        {
            var retVal = new ResponsePackageNoData();

            //HttpContext.Session.Clear();
            retVal.Message = "You logged out successfully";
            return retVal;
        }

        public ResponsePackageNoData Delete(int id)
        {
            var retVal = new ResponsePackageNoData();

            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No User with ID {id}";
            }
            else
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                retVal.Message = $"Successfully deleted User {user.Username}";
            }
            return retVal;
        }

        public ResponsePackage<UserResponseDTO> Get(int id)
        {
            var retVal = new ResponsePackage<UserResponseDTO>();

            var user = _context.Users
                .FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No User with ID {id}";
            }
            else
                retVal.TransferObject = _mapper.Map<UserResponseDTO>(user);

            return retVal;
        }

        public PaginationDataOut<UserResponseDTO> GetAll(SearchDTO searchData)
        {
            PaginationDataOut<UserResponseDTO> retVal = new PaginationDataOut<UserResponseDTO>();

            IQueryable<User> users = _context.Users;

            if (!string.IsNullOrWhiteSpace(searchData.SearchFilter))
            {
                users = users.Where(x => x.Username.ToLower().Contains(searchData.SearchFilter));
            }
            retVal.Count = users.Count();

            users.ToList().ForEach(x => retVal.Data.Add(_mapper.Map<UserResponseDTO>(x)));
            return retVal;
        }

        public ResponsePackageNoData Update(int id, UserRequestDTO user)
        {
            var retVal = new ResponsePackageNoData();

            var userDb = _context.Users
                .FirstOrDefault(x => x.Id == id);
            var userNameDb = _context.Users
                .FirstOrDefault(x => x.Username.ToLower() == user.Username.ToLower());

            if (userDb == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No User with ID {id}";
            }
            else if (userNameDb != null)
            {
                retVal.Message = $"Already exists User {user.Username}";
                retVal.Status = 409;
            }
            else
            {
                userDb.Username = user.Username;                
                _context.SaveChanges();
                retVal.Message = $"Successfully updated User {user.Username}";
            }

            return retVal;
        }
    }
}
