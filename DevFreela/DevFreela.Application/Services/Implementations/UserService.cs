using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace DevFreela.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly DevFreelaDbContext _dbContext;

        public UserService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(NewUserInputModel inputModel)
        {
            User user = new User(inputModel.FullName, inputModel.Email, inputModel.Username, inputModel.Password, inputModel.BirthDate);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user.Id;
        }

        public List<UserViewModel> GetAll(string query)
        {
            List<User> users = _dbContext.Users.ToList();
            List<UserViewModel> usersViewModel = users.Select(u => new UserViewModel(u.FullName, u.Email, u.Username, u.BirthDate, u.CreatedAt, u.Active)).ToList();
            return usersViewModel;
        }

        public UserDetailsViewModel GetById(int id)
        {
            User user = _dbContext.Users.SingleOrDefault(p => p.Id == id);

            if(user is null)
            {
                return null;
            }

            UserDetailsViewModel userDetailsViewModel = new UserDetailsViewModel(user.Id, user.FullName, user.Email, user.Username, user.BirthDate, user.CreatedAt, user.Active);
            return userDetailsViewModel;
        }

        public bool Login(LoginInputModel inputModel)
        {
            return _dbContext.Users.Any(u => u.Active 
                                            && u.Username.Equals(inputModel.Username) 
                                            && u.Password.Equals(inputModel.Password));
        }
    }
}
