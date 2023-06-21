using AutoMapper;
using FastSchedule.Application.Commands;
using FastSchedule.Application.Dto;
using FastSchedule.Application.Services.PasswordService;
using FastSchedule.Domain.Interfaces;
using FastSchedule.Domain.Models;
using MediatR;
using SHA3.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Handlers.UserHandlers
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserDto>
    {
        private readonly IPasswordService _passwordService;
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddUserCommandHandler(IRepository<User> userRepository, IUnitOfWork unitOfWork, IMapper mapper, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        public async Task<UserDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            string salt = _passwordService.GenerateSalt();
            string hash = _passwordService.GetHash(request.Password, salt);

            var user = new UserDto
            {
                Email = request.Email.ToLower(),
                Login = request.Login.ToLower(),
                Password = hash,
                Salt = salt,
                Guid = Guid.NewGuid(),
            };

            await _userRepository.AddAsync(_mapper.Map<User>(user));
            await _unitOfWork.SaveChangesAsync();
            return user;
        }
    }
}
