using Auth.Application.Helpers;
using EventBooking.Application.Behavior;
using EventBooking.Application.Helpers;
using EventBooking.Application.Resources;
using EventBooking.Application.Responses;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EventBooking.Application.DI
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddIApplicationDI(this IServiceCollection services)
        {
            services.AddTransient<ResponseHandler>();
            services.AddSingleton<SharedResource>();
            services.AddScoped<IUserRegistrationHelper, UserRegistrationHelper>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(BookingCommandHandler).Assembly));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(BookingQueryHandler).Assembly));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(EventCommandHandler).Assembly));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(EventQueryHandler).Assembly));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
