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
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<UserDto>(await _userRepository.GetByIdAsync(request.UserId));
        }
    }
}
