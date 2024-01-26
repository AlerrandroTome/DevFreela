using DevFreela.Core.Entities;
using System.Threading.Tasks;

namespace DevFreela.Core.Repositories
{
    public interface ICommentRepository
    {
        Task AddAsync(ProjectComment comment);
    }
}
