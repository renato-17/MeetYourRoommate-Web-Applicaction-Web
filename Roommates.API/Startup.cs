using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Roommates.API.Domain.Persistence.Contexts;
using Roommates.API.Domain.Repositories;
using Roommates.API.Domain.Services;
using Roommates.API.Extensions;
using Roommates.API.Persistance.Repositories;
using Roommates.API.Services;
using Roommates.API.Settings;

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
            services.ActiveCors();
            services.AddCustomSwagger();

            services.AddControllers();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("MySQLConnection"));
                //options.UseInMemoryDatabase("meetyourroommate-api-in-memory");
                //options.UseMySQL(Configuration.GetConnectionString("AzureMySQLConnection"));
            });

            // AppSettings Section Reference
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // JSON Web Token Authentication Configuration
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            // Authentication Service Configuration
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonService, PersonService>();

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();

            services.AddScoped<ILessorRepository, LessorRepository>();
            services.AddScoped<ILessorService, LessorService>();

            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IPropertyService, PropertyService>();

            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ITeamService, TeamService>();

            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskService, TaskService>();

            services.AddScoped<IStudyCenterRepository, StudyCenterRepository>();
            services.AddScoped<IStudyCenterService, StudyCenterService>();

            services.AddScoped<ICampusRepository, CampusRepository>();
            services.AddScoped<ICampusService, CampusService>();

            services.AddScoped<IPropertyDetailRepository, PropertyDetailRepository>();
            services.AddScoped<IPropertyDetailService, PropertyDetailService>();

            services.AddScoped<IPropertyResourceRepository, PropertyResourceRepository>();
            services.AddScoped<IPropertyResourceService, PropertyResourceService>();

            services.AddScoped<IAdRepository, AdRepository>();
            services.AddScoped<IAdService, AdService>();

            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();

            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<IRequestService, RequestService>();

            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IReservationService, ReservationService>();

            services.AddScoped<IReservationDetailRepository, ReservationDetailRepository>();
            services.AddScoped<IReservationDetailService, ReservationDetailService>();

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

            app.UseCors(x => x.SetIsOriginAllowed(origin => true)
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCustomSwagger();

        }
    }
}
