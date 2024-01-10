using System;

namespace DevFreela.Application.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(string fullName, string email, string username, DateTime birthDate, DateTime createdAt, bool active)
        {
            FullName = fullName;
            Email = email;
            Username = username;
            BirthDate = birthDate;
            CreatedAt = createdAt;
            Active = active;
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Username { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool Active { get; private set; }
    }
}
