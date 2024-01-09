using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext
    {
        public DevFreelaDbContext()
        {
            Projects = new List<Project>
            {
                new Project("Meu projeto ASPNET Core 1", "Minha Descrição de Projeto 1", 1, 1, 10000),
                new Project("Meu projeto ASPNET Core 2", "Minha Descrição de Projeto 2", 1, 1, 20000),
                new Project("Meu projeto ASPNET Core 3", "Minha Descrição de Projeto 3", 1, 1, 30000),
            };
            Users = new List<User> 
            {
                new User("Alerrandro Tome", "alerrandro@gmail.com", new DateTime(2002, 4, 23)),
                new User("Robert C Martin", "robert@gmail.com", new DateTime(1950, 1, 1)),
                new User("Anderson", "anderson@gmail.com", new DateTime(1980, 1, 1)),
            };
            Skills = new List<Skill> 
            {
                new Skill(".NET CORE"),
                new Skill("C#"),
                new Skill("SQL"),
            };
        }

        public List<Project> Projects { get; set; }
        public List<User> Users { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
