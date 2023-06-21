using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Queries
{
    public class IsUserEmailExistQuery : IRequest<bool>
    {
        public string SearchingEmail { get; set; }

        public IsUserEmailExistQuery(string searchingEmail)
        {
            SearchingEmail = searchingEmail;
        }
    }
}
