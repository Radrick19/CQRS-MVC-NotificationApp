using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Queries
{
    public class IsUserLoginExistQuery : IRequest<bool>
    {
        public string SearchingLogin { get; set; }

        public IsUserLoginExistQuery(string searchingLogin)
        {
            SearchingLogin = searchingLogin;
        }
    }
}
