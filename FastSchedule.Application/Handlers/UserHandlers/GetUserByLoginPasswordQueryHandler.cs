using AutoMapper;
using FastSchedule.Application.Dto;
using FastSchedule.Application.Queries;
using FastSchedule.Application.Services.PasswordService;
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
    public class GetUserByLoginPasswordQueryHandler : IRequestHandler<GetUserByLoginPasswordQuery, UserDto?>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public GetUserByLoginPasswordQueryHandler(IRepository<User> userRepository, IMapper mapper, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        public async Task<UserDto?> Handle(GetUserByLoginPasswordQuery request, CancellationToken cancellationToken)
        {
            var userByLogin = _mapper.Map<UserDto>(await _userRepository.FirstOrDefaultAsync(user => user.Login == request.Login.ToLower()));
            var userHash = _passwordService.GetHash(request.Password, userByLogin.Salt);
            if(userHash == userByLogin.Password)
            {
                // Пароль совпадает
                return userByLogin;
            }
            // Неверный пароль
            return null;
        }
    }
}
