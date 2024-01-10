using System;

namespace DevFreela.Application.InputModels
{
    public class NewUserInputModel
    {
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public DateTime BirthDate { get; private set; }
    }
}
