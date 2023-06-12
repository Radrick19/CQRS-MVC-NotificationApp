using AutoMapper;
using FastSchedule.Application.Commands.TaskCommands;
using FastSchedule.Application.Dto;
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
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddUserCommandHandler(IRepository<User> userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            string hash;
            string salt = DateTime.Now.GetHashCode().ToString();
            using (var shaAlg = Sha3.Sha3256())
            {
                string inputPassHash = Convert.ToBase64String(shaAlg.ComputeHash(Encoding.UTF8.GetBytes(request.Password)));
                hash = Convert.ToBase64String(shaAlg.ComputeHash(Encoding.UTF8.GetBytes(inputPassHash + salt)));
            }

            var user = new UserDto
            {
                Email = request.Email,
                Login = request.Login,
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
