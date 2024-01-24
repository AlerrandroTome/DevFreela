using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.Queries.GetUser
{
    public class GetUserByIdQuery : IRequest<UserDetailsViewModel>
    {
        public int Id { get; set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
