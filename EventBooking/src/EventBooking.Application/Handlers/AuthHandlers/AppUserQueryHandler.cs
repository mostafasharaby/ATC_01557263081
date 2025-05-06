using Auth.Application.Queries;
using AutoMapper;
using EventBooking.Application.DTOs;
using EventBooking.Application.Pagination;
using EventBooking.Application.Responses;
using EventBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EventBooking.Application.Handlers.AuthHandlers
{
    internal class AppUserQueryHandler : IRequestHandler<GetAppUserById, Response<UserDto>>,
                                         IRequestHandler<GetAllAppUsers, PaginatedResult<UserDto>>


    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public readonly ResponseHandler _responseHandler;

        public AppUserQueryHandler(UserManager<AppUser> userManager, IMapper mapper, ResponseHandler responseHandler)
        {
            _userManager = userManager;
            _mapper = mapper;
            _responseHandler = responseHandler;

        }
        public async Task<Response<UserDto>> Handle(GetAppUserById request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return _responseHandler.Unauthorized<UserDto>("Unauthorized User");
            }

            var userDto = _mapper.Map<UserDto>(user);
            return _responseHandler.Success(userDto);
        }

        public async Task<PaginatedResult<UserDto>> Handle(GetAllAppUsers request, CancellationToken cancellationToken)
        {
            var allUsers = _userManager.Users.AsQueryable();
            var paginatedResult = _mapper.ProjectTo<UserDto>(allUsers).ToPaginatedListAsync(request.PageNumber, request.PageSize);

            return await paginatedResult;
        }
    }
}
