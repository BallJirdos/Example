using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayerApi.Configuration;
using DataLayerApi.Configuration.Settings;
using DataLayerApi.Extensions;
using DataLayerApi.Models.Entities;
using DataLayerApi.Repositories;
using DataLayerApi.Services.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using DataLayerApi.Services;
using Microsoft.Extensions.Hosting;
using DataLayerApi.Models.Entities.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http.Features;

namespace DataLayerApi
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
            services.AddRazorPages();

            var appSettingsSection = this.Configuration.GetSection(nameof(ApplicationSettings));
            services.Configure<ApplicationSettings>(appSettingsSection);

            services.AddAutoMapper(typeof(Startup));

            services.AddDbContextPool<ApplicationContext>(opts => opts.UseLazyLoadingProxies(true).UseSqlServer(this.Configuration.GetConnectionString("BusinessSystemConnection"),
            options => options.MigrationsAssembly("DataLayerApi")));

            services.AddIdentity<User, Role>(opt =>
            {
                opt.Password.RequiredLength = 7;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;

                opt.User.RequireUniqueEmail = true;

                opt.SignIn.RequireConfirmedEmail = true;

                //opt.Tokens.
            }).AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var applicationSettings = appSettingsSection.Get<ApplicationSettings>();
                var jwt = applicationSettings.Jwt;

                options.SaveToken = true;
                options.RequireHttpsMetadata = jwt.RequireHttpsMetadata;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = !string.IsNullOrEmpty(jwt.ValidIssuer),
                    ValidateAudience = true,
                    ValidAudience = jwt.ValidAudience,
                    ValidIssuer = jwt.ValidIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecureKey)),
                    
                };
            });

            services.AddHttpContextAccessor();
            services.AddTransient((s) => services);
            services.AddTransient(typeof(Lazy<>), typeof(Lazier<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<UnitOfWork>();
            services.AddTransient<IHttpContextService, HttpContextService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddScoped<UnitOfWorkFactory>();
            services.AddScoped<IUserService, UserService>();

            services.AddCors();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.IgnoreNullValues = true;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Business system Api", Version = "v1" });
                c.EnableXmlComments();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                  });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, ILogger<Startup> logger, IOptions<ApplicationSettings> appOptions)
        {
            //neni funkcni nacitani, musi se dal zkoumat
            var loggingOptions = this.Configuration.GetSection("Log4NetCore")
                                               .Get<Log4NetProviderOptions>();
            //loggerFactory.AddLog4Net(loggingOptions);
            loggerFactory.AddLog4Net();


            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error-local-development");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            //app.UseAuthorization();

            app.UseCors(builder =>
            {
                builder.WithOrigins(appOptions.Value.UrlCors)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            // EXECUTION
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Values Api V1");
            });

            logger.LogInformation("START- Configurace dokončena");
        }
    }
}
