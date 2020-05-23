using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace DataLayerApi.Extensions
{
    public static class InitializingExtension
    {
        //private static readonly Contact itContact = new Contact
        //{
        //    Name = "Team alfa (samců)",
        //    Email = "IT_Team_Alfa@proficredit.cz",
        //    Url = "http://czphaptfs01:8080/tfs/DefaultCollection/PCCZ/Alfa/_dashboards"
        //};

        /// <summary>
        /// Povolit zobrazování XML dokumentace v UI
        /// </summary>
        /// <param name="option">Instance nastaveni</param>
        public static void EnableXmlComments(this SwaggerGenOptions option)
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            //... and tell Swagger to use those XML comments.
            if (File.Exists(xmlPath))
                option.IncludeXmlComments(xmlPath);
        }

        public static void UseSwaggerApi(this IApplicationBuilder applicationBuilder/*, IVersionStore versionStore*/)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            applicationBuilder.UseSwagger(o =>
            {
                o.RouteTemplate = o.RouteTemplate;// "docs/PcczApiSwagger.json";
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            applicationBuilder.UseSwaggerUI(c =>
            {
                //foreach (var version in versionStore.GetVersions())
                //    c.SwaggerEndpoint($"../docs/{version.Name}/PcczApiSwagger.json", version.DropdownTitle);

                    //c.SwaggerEndpoint($"../docs/PcczApiSwagger.json", "Buss");

                c.OAuthAppName("API lokálního IT");
                c.RoutePrefix = "docs";
            });
        }

        //public static VersionModel[] GetAvaibleVersions(this IServiceCollection services)
        //{
        //    return new[] {
        //        new VersionModel
        //        {
        //            Name = "v1",
        //            DropdownTitle = "Local API",
        //            Info = new Info
        //            {
        //                Version = "v1",
        //                Title = "PcCzAPI",
        //                Description = "Proficredit czech API- přístup ke službám lokálního API",
        //                Contact = itContact
        //            }
        //        },
        //        new VersionModel
        //        {
        //            Name = "v2",
        //            DropdownTitle = "Mobile API",
        //            Info = new Info
        //            {
        //                Version = "v2",
        //                Title = "Global mobile API",
        //                Description = "Proficredit czech API- přístup ke službám pro globální API",
        //                Contact = itContact
        //            }
        //        }
        //    };
        //}
    }
}
