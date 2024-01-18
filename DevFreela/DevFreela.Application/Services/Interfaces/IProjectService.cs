using DevFreela.Application.InputModels;
using DevFreela.Application.ViewModels;
using System.Collections.Generic;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IProjectService
    {
        ProjectDetailsViewModel GetById(int id);
        void Start(int id);
        void Finish(int id);
    }
}
