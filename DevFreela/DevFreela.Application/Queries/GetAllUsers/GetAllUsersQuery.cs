using DevFreela.Application.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace DevFreela.Application.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<UserViewModel>>
    {
        public string Query { get; set; }

        public GetAllUsersQuery(string query)
        {
            Query = query;
        }
    }
}
