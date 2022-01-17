using LegionSociety.Contacts.Data;
using LegionSociety.Contacts.Data.Models;
using LegionSociety.Contacts.Services;
using LegionSociety.Contacts.Services.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LegionSociety.Contacts.Web
{
    public static class IOC
    {
        public static IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DbContext, ContactContext>();
            services.AddScoped<IRepository<Contact>, ContactRepository>();
            services.AddScoped<IContactMapper, ContactMapper>();
            services.AddScoped<IAuthenticationService, ClaimsBasedAuthenticationService>();

            if(configuration["envName"] == "Development")
            {
                services.AddScoped<IPasswordHashService, DoNothingPasswordHashService>();
            }
            else
            {
                services.AddScoped<IPasswordHashService, PasswordHashService>();
            }

            return services;
        }
    }
}
