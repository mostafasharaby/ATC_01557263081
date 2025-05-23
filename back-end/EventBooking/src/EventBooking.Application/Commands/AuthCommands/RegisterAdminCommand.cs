﻿using EventBooking.Application.Responses;
using MediatR;

namespace EventBooking.Application.Commands.AuthCommands
{
    public record RegisterAdminCommand(string? UserName, string? Password, string? ConfirmPassword, string? Email)
        : IRequest<Response<string>>;

    public record RegisterUserCommand : RegisterAdminCommand, IRequest<Response<string>>
    {
        public RegisterUserCommand(string? UserName, string? Password, string? ConfirmPassword, string? Email) : base(UserName, Password, ConfirmPassword, Email)
        {
        }
    }
}