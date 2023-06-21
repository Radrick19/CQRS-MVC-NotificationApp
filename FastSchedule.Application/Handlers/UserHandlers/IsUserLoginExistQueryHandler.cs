using FastSchedule.Application.Queries;
using FastSchedule.Domain.Interfaces;
using FastSchedule.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Handlers.UserHandlers
{
    public class IsUserLoginExistQueryHandler : IRequestHandler<IsUserLoginExistQuery, bool>
    {
        private readonly IRepository<User> _userRepository;

        public IsUserLoginExistQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<bool> Handle(IsUserLoginExistQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_userRepository.Get().Any(user => user.Login == request.SearchingLogin.ToLower()));
        }
    }
}
