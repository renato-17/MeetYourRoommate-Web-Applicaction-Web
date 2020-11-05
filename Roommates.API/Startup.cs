using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Roommates.API.Domain.Persistence.Contexts;
using Roommates.API.Domain.Repositories;
using Roommates.API.Domain.Services;
using Roommates.API.Extensions;
using Roommates.API.Persistance.Repositories;
using Roommates.API.Services;

namespace Roommates.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomSwagger();

            services.AddControllers();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("MySQLConnection"));
                //options.UseInMemoryDatabase("meetyourroommate-api-in-memory");
            });

            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();

            services.AddScoped<ILessorRepository, LessorRepository>();
            services.AddScoped<ILessorService, LessorService>();

            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IPropertyService, PropertyService>();

            services.AddScoped<IPropertyDetailRepository, PropertyDetailRepository>();
            services.AddScoped<IPropertyDetailService, PropertyDetailService>();

            services.AddScoped<IPropertyResourceRepository, PropertyResourceRepository>();
            services.AddScoped<IPropertyResourceService, PropertyResourceService>();

            services.AddScoped<IAdRepository, AdRepository>();
            services.AddScoped<IAdService, AdService>();

            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(Startup));

            services.AddRouting(opt => opt.LowercaseUrls = true);
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCustomSwagger();
        }
    }
}
