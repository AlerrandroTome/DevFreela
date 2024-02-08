using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllProjectsQueryHandlerTest
    {
        [Fact]
        public async Task ThreeProjectsExists_Executed_ReturnThreeProjectViewModels()
        {
            // Arrange
            List<Project> projects = new List<Project>
            {
                new Project("Nome Do Teste 1", "Descricao De Teste 1", 1, 2, 10000),
                new Project("Nome Do Teste 2", "Descricao De Teste 2", 1, 2, 20000),
                new Project("Nome Do Teste 3", "Descricao De Teste 3", 1, 2, 30000)
            };
            Mock<IProjectRepository> projectRepositoryMock = new Mock<IProjectRepository>();
            // Mocking the result of a method that is going to be called in the handle method
            projectRepositoryMock.Setup(s => s.GetAllAsync().Result).Returns(projects);

            GetAllProjectsQuery getAllProjectsQuery = new GetAllProjectsQuery("");
            GetAllProjectsQueryHandler getAllProjectsCommandHandler = new GetAllProjectsQueryHandler(projectRepositoryMock.Object);

            // Act
            List<ProjectViewModel> projectViewModels = await getAllProjectsCommandHandler.Handle(getAllProjectsQuery, new CancellationToken());

            // Assert
            Assert.NotNull(projectViewModels);
            Assert.NotEmpty(projectViewModels);
            Assert.Equal(projects.Count, projectViewModels.Count);

            projectRepositoryMock.Verify(p => p.GetAllAsync().Result, Times.Once);
        }
    }
}
