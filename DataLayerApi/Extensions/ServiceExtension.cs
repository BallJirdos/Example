using DataLayerApi.Services.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DataLayerApi.Extensions
{
    public static class ServiceExtension
    {
        ///// <summary>
        ///// Inicializace verzí API
        ///// </summary>
        ///// <param name="services">Kolekce služeb.</param>
        ///// <param name="versionModels"></param>
        //public static void InitializeVersions(this IServiceCollection services/*, params VersionModel[] versionModels*/)
        //{
        //    services.AddApiVersioning(o =>
        //    {
        //        o.DefaultApiVersion = new ApiVersion(2, 0); // specify the default api version
        //        o.ReportApiVersions = true;
        //        o.ApiVersionReader = new HeaderApiVersionReader("api-version");
        //        o.ApiVersionSelector = new CurrentImplementationApiVersionSelector(o);
        //        o.AssumeDefaultVersionWhenUnspecified = true; // assume that the caller wants the default version if they don't specify
        //        //o.ApiVersionReader = new MediaTypeApiVersionReader(); // read the version number from the accept header
        //    });

        //    //services.AddTransient<IVersionStore>((v) => new VersionStore(versionModels));
        //}

        /// <summary>
        /// Registrace služeb
        /// </summary>
        /// <param name="services">Kolekce služeb.</param>
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(Lazy<>), typeof(Lazier<>));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddTransient<IDataContext, WebDataContext>();
            //services.AddTransient<IErrorLogger, DbLogger>();
            //services.AddTransient<IPcdUserService, PcdUserService>();
            //services.AddTransient<ITrafficLogger, TrafficLogger>();
            //services.AddTransient<IDateTime, DateTimeWrap>();
            //services.AddTransient<IHttpHeaderService, HttpHeaderService>();
        }

        /// <summary>
        /// Konfigurace swaggeru.
        /// </summary>
        /// <param name="services">Kolekce služeb.</param>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(option =>
            {
                //nahrazeni verze v url
                //option.DocumentFilter<ReplaceVersionWithExactValueInPath>();
                //option.OperationFilter<AddDefaultValuesFilter>();

                /*option.DocInclusionPredicate((version, apiDesc) =>
                {
                    //Odebrani parametru pro verzi ze swagger
                    var versionParameters = apiDesc.ParameterDescriptions
                        .Where(p => p.Name == "api-version" || p.Name == "version").ToArray();

                    foreach (var parameter in versionParameters)
                    {
                        apiDesc.ParameterDescriptions.Remove(parameter);
                    }

                    //hlidani verze api pro controller/ methodu, aby se nabizeli v dokumentaci pro spravnou verzi
                    var actionApiVersionModel = apiDesc.ActionDescriptor?.GetApiVersion();
                    //pokud neni deklarovana verze, je pridavano vsude
                    if (actionApiVersionModel == null)
                        return true;

                    if (actionApiVersionModel.DeclaredApiVersions.Any())
                        return actionApiVersionModel.DeclaredApiVersions.Any(v => $"v{v.ToString()}" == version);

                    return actionApiVersionModel.ImplementedApiVersions.Any(v => $"v{v.ToString()}" == version);
                });*/

                var sp = services.BuildServiceProvider();
                //var versionStore = sp.GetService<IVersionStore>();
                //foreach (var version in versionStore.GetVersions())
                //{
                //    option.SwaggerDoc(version.Name, version.Info);
                //}
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "PcCzAPI",
                    Description = "Proficredit czech API- přístup ke službám lokálního API",
                    Contact = new OpenApiContact
                    {
                        Name = "Team alfa (samců)",
                        Email = "IT_Team_Alfa@proficredit.cz",
                        Url = new Uri("http://czphaptfs01:8080/tfs/DefaultCollection/PCCZ/Alfa/_dashboards")
                    }
                });

                //... and tell Swagger to use those XML comments.
                option.EnableXmlComments();
                //option.DescribeAllEnumsAsStrings();
            });

            //services.AddSwaggerDocument();
        }

        /*/// <summary>
        /// Configures the authorization.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            // Replace the default authorization policy provider with our own
            // custom provider which can return authorization policies for given
            // policy names (instead of using the default policy provider)
            services.AddSingleton<IAuthorizationPolicyProvider, PcdPolicyProvider>();

            // As always, handlers must be provided for the requirements of the authorization policies
            services.AddSingleton<IAuthorizationHandler, PcdAuthorizationHandler>();
        }

        /// <summary>
        /// Povolit zobrazování XML dokumentace v UI
        /// </summary>
        /// <param name="option">Instance nastaveni</param>
        private static void EnableXmlComments(this SwaggerGenOptions option)
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            //... and tell Swagger to use those XML comments.
            if (File.Exists(xmlPath))
                option.IncludeXmlComments(xmlPath);
        }*/
    }
}
