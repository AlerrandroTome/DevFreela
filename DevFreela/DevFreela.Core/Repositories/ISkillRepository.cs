using DevFreela.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevFreela.Core.Repositories
{
    public interface ISkillRepository
    {
        Task<List<SkillDTO>> GetAll();
    }
}
