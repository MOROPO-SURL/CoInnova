using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Security.Common.Persistence.Extensions;
using MOROPO.Framework.Guama.Core.Security.Common.Presentation.Web.Frontend.Extensions;
using MOROPO.Framework.Guama.Media.Presentation.Web.Common.Extensions;
using MOROPO.Framework.Guama.Security.Personal.Presentation.Web.Backend.Extensions;
using MOROPO.UIC.CoInnova.Persistence.Entities;
using Serilog;
using SmartSolucionesCuba.AspNetCore.Authentication.ApiKey.Extensions;
using System.Linq;

namespace MOROPO.UIC.CoInnova
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

            this.configuration = configuration;
            this.env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Options.MediaThumbnailResizeOptions>(configuration.GetSection("MediaThumbnailResize"));
            services.Configure<Framework.Guama.Media.Presentation.Web.Common.Options.RelativeMediaFileManagementOptions>(configuration.GetSection("FileManagement"));
            services.Configure<Models.View.PrototypeDisplayViewModel>(configuration.GetSection("prototypeDefault"));

            services.AddDbContext<Persistence.EF.ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<DbContext, Persistence.EF.ApplicationDbContext>();

            services.PerformCoreWebSecurityCommonFrontendServiceRegistration();
            services.PerformSecurityPersonalWebBackendServiceRegistration();
            services.PerformMediaWebCommonRelativePathFileManagementServiceRegistration();

            services.AddScoped<IEntityRepository<Framework.Guama.Core.Persistence.ORM.EF.Entities.Trace, long>, Framework.Guama.Core.Persistence.ORM.EF.Repositories.TraceRepository>();
            services.AddScoped<IEntityRepository<ApiKey, System.Guid>, Persistence.EF.Repositories.ApiKeyRepository>();
            services.AddScoped<IEntityRepository<PrototypingLevel, System.Guid>, Persistence.EF.Repositories.PrototypingLevelRepository>();
            services.AddScoped<IEntityRepository<Collaborator, System.Guid>, Persistence.EF.Repositories.CollaboratorRepository>();
            services.AddScoped<IEntityRepository<IdeaWorkflow, System.Guid>, Persistence.EF.Repositories.IdeaWorkflowRepository>();
            services.AddScoped<IEntityRepository<IdeaWorkflowStep, System.Guid>, Persistence.EF.Repositories.IdeaWorkflowStepRepository>();
            services.AddScoped<IEntityRepository<ChallangeWorkflow, System.Guid>, Persistence.EF.Repositories.ChallangeWorkflowRepository>();
            services.AddScoped<IEntityRepository<ChallangeWorkflowStep, System.Guid>, Persistence.EF.Repositories.ChallangeWorkflowStepRepository>();
            services.AddScoped<IEntityRepository<Sphere, System.Guid>, Persistence.EF.Repositories.SphereRepository>();
            services.AddScoped<IEntityRepository<Prototype, System.Guid>, Persistence.EF.Repositories.PrototypeRepository>();
            services.AddScoped<IEntityRepository<Idea, System.Guid>, Persistence.EF.Repositories.IdeaRepository>();
            services.AddScoped<IEntityRepository<UseCase, System.Guid>, Persistence.EF.Repositories.UseCaseRepository>();
            services.AddScoped<IEntityRepository<Challange, System.Guid>, Persistence.EF.Repositories.ChallangeRepository>();

            services.AddSingleton<Managers.RelativeDocumentFileManager>();

            services.AddSingleton<Framework.Guama.Core.Persistence.ORM.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade, Persistence.EF.Providers.DbSchemaConfigurationProvider>();

            services.AddSingleton<Framework.Guama.Core.Persistence.ORM.EF.Helpers.Facade.IDataInitializationHelper, Helpers.DataInitializationHelper>();

            services.AddHttpContextAccessor();

            services.AddAuthentication(config =>
            {
                config.DefaultScheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(config =>
            {
                config.AccessDeniedPath = new PathString("/security/error/forbidden");
                config.LoginPath = new PathString("/security/account/login");
                config.ExpireTimeSpan = System.TimeSpan.FromMinutes(5);
                config.SlidingExpiration = true;
            })
            .AddApiKeyInHeaderOrQueryParams<Providers.ApiKeySessionProvider>(SmartSolucionesCuba.AspNetCore.Authentication.ApiKey.ApiKeyAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Realm = "CoInnova Restfull API";
                options.KeyName = Constants.Authorization.API_KEY_HEADER_PROPERTY;
                options.SuppressWWWAuthenticateHeader = true;
            });

            #region Security concreter dependencies configuration
            services.AddSingleton<Framework.Guama.Core.Presentation.Web.Security.Helpers.Facade.ILicenseValidationHelperFacade, Helpers.WillcardLicenseValidationHelper>();

            services.AddTransient<Framework.Guama.Core.Presentation.Web.Security.Models.View.Contrat.IUserProfileViewModel, Models.View.UserProfileViewModel>();

            services.AddSingleton<Framework.Guama.Core.Presentation.Web.Common.ModelBinding.Contract.IConcretModelBindHelper, Helpers.ConcretModelBindHelper>();

            services.AddScoped<Framework.Guama.Core.Presentation.Web.Security.Managers.Facade.IUserProfileManagerFacade, Managers.UserProfileManager>();

            services.AddSingleton<Framework.Guama.Core.Presentation.Web.Security.Providers.Facade.IRoleIdProviderFacade>(Framework.Guama.Core.Security.Common.Presentation.Web.Frontend.Providers.RoleMemoryProvider.Instance);
            #endregion

            #region Globalization configuration
            var defaultCulture = new System.Globalization.CultureInfo("es");

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(defaultCulture);
                options.SupportedCultures = new[] { defaultCulture };
                options.SupportedUICultures = new[] { defaultCulture };

                options.RequestCultureProviders = new[] { new Framework.Guama.Core.Presentation.Web.Common.Localization.UrlRequestCultureProvider() };
            });

            services.AddSingleton<Helpers.GlobalViewLocalizationHelper>();
            #endregion

            services.Configure<Microsoft.AspNetCore.Routing.RouteOptions>(options => options.LowercaseUrls = true);

            services.AddResponseCaching();
            services.AddMemoryCache();
            services.AddSession();

            services.AddMvc(mvc =>
            {
                mvc.EnableEndpointRouting = false;
                mvc.ModelBinderProviders.Insert(0, new Framework.Guama.Core.Presentation.Web.Common.ModelBinding.AbstractsModelBinderProvider());
            })
            .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization(options =>
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    return factory.Create(typeof(SharedResources));
                }
            );

            services.AddDataProtection().PersistKeysToFileSystem(new System.IO.DirectoryInfo(System.IO.Path.Combine(env.ContentRootPath, "dpkeys")));

            services.AddSingleton<Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataProvider, Microsoft.AspNetCore.Mvc.ViewFeatures.CookieTempDataProvider>();

            var openApiSecurityScheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
            {
                Description = string.Format("Authorization Header \"{0}: {1}\"", Constants.Authorization.API_KEY_HEADER_PROPERTY, "{token}"),
                Name = "Authorization",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
            };

            services.AddSingleton(openApiSecurityScheme);

            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "CoInnova Restfull API",
                    Version = "v1"
                });
                setup.DocInclusionPredicate((string stringParam, Microsoft.AspNetCore.Mvc.ApiExplorer.ApiDescription apiDescription) =>
                {
                    return apiDescription.RelativePath.StartsWith("api/");
                });
                setup.AddSecurityDefinition(Constants.Authorization.API_KEY_HEADER_PROPERTY, openApiSecurityScheme);
                setup.OperationFilter<Filters.Swagger.OperationSwaggerFilter>();
                setup.EnableAnnotations();
                setup.IgnoreObsoleteActions();
            });
        }

        public void Configure(IApplicationBuilder app, Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions> requestLocalizationOptions, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory, DbContext dbContext)
        {
            System.AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            loggerFactory.AddSerilog(Log.Logger);

            app.UseRequestLocalization(requestLocalizationOptions.Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else if (env.IsStaging())
            {
                dbContext.Database.Migrate();

                var dataInitializationHelpers = app.ApplicationServices.GetServices<Framework.Guama.Core.Persistence.ORM.EF.Helpers.Facade.IDataInitializationHelper>();

                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    var scopedDbContext = serviceScope.ServiceProvider.GetService<DbContext>();

                    foreach (var dataInitializationHelper in dataInitializationHelpers)
                    {
                        dataInitializationHelper.InitialiseData(scopedDbContext);
                    }
                }

                if (!(dbContext as Persistence.EF.ApplicationDbContext).Users.Any())
                {
                    var userId = configuration["DefaultUser:Id"];

                    var defaultUser = new Framework.Guama.Security.Personal.Persistence.Entities.User
                    {
                        Id = userId,
                        Password = configuration["DefaultUser:Password"].ToHashedPassword(),
                        Enabled = true,
                        HasFullAccess = true,
                        PasswordChangeRequired = true,
                        Identity = new Framework.Guama.Identity.Persistence.Entities.PersonIdentity
                        {
                            Name = userId,
                            Sex = Framework.Guama.Identity.Persistence.Enumerations.Sex.Unknown,
                            CivilStatus = Framework.Guama.Identity.Persistence.Enumerations.CivilStatus.Unknow
                        }
                    };

                    defaultUser.Signature = Framework.Guama.Security.Personal.Persistence.ORM.EF.Helpers.Integrity.UserIntegrityHelper.GetSignature(defaultUser);

                    dbContext.Set<Framework.Guama.Security.Personal.Persistence.Entities.User>().Add(defaultUser);

                    (dbContext as Framework.Guama.Core.Persistence.ORM.EF.Context.Contract.AbstractTraceableDbContext).SaveChanges("system");
                }
            }
            else
            {
                app.UseExceptionHandler("/error");

                app.UseHsts();
            }

            var atStartUpRoles = dbContext.Set<Framework.Guama.Core.Security.Common.Persistence.Entities.Role>().Include(e => e.PermissionBridges).ThenInclude(b => b.Stern).ToArray();

            foreach (var item in atStartUpRoles)
            {
                Framework.Guama.Core.Security.Common.Presentation.Web.Frontend.Providers.RoleMemoryProvider.Instance.Roles.Add(item);
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();

            app.UseSwagger(setup =>
            {
                setup.RouteTemplate = "api/docs/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint("/api/docs/v1/swagger.json", "CoInnova Restfull API v1");
                setup.RoutePrefix = "api/docs";
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute("defaultLocalizedAreaRoutes",
                    template: "{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/{area:exists}/{controller=home}/{action=index}/{id?}");

                routes.MapRoute(
                    name: "defaultLocalizedRoutes",
                    template: "{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/{controller=home}/{action=index}/{id?}");

                routes.MapRoute("defaultAreaRoutes",
                    template: "{area:exists}/{controller=home}/{action=index}/{id?}");

                routes.MapRoute(
                    name: "defaultRoutes",
                    template: "{controller=home}/{action=index}/{id?}");
            });
        }
    }
}
