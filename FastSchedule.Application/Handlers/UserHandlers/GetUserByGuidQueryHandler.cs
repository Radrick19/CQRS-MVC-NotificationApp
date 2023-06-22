using AutoMapper;
using FastSchedule.Application.Dto;
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
    public class GetUserByGuidQueryHandler : IRequestHandler<GetUserByGuidQuery, UserDto>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetUserByGuidQueryHandler(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByGuidQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<UserDto>(await _userRepository.FirstOrDefaultAsync(user=> user.Guid == new Guid(request.UserGuid)));
        }
    }
}
