﻿using System;
using System.Collections.Generic;

namespace DevFreela.Core.Entities
{
    public class User : BaseEntity
    {
        public User(string fullName, string email, string username, string password, string role, DateTime birthDate)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Username = username;
            Password = password;
            Role = role;
            Active = true;
            CreatedAt = DateTime.Now;

            Skills = new List<UserSkill>();
            OwnedProjects = new List<Project>();
            FreelanceProjects = new List<Project>();
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool Active { get; private set; }

        public List<UserSkill> Skills { get; private set; }
        public List<Project> OwnedProjects { get; private set; }
        public List<Project> FreelanceProjects { get; private set; }
        public List<ProjectComment> Comments { get; private set; }
    }
}
