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
    public class IsUserEmailExistQueryHandler : IRequestHandler<IsUserEmailExistQuery, bool>
    {
        private readonly IRepository<User> _userRepository;

        public IsUserEmailExistQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<bool> Handle(IsUserEmailExistQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_userRepository.Get().Any(user => user.Email == request.SearchingEmail.ToLower()));
        }
    }
}
