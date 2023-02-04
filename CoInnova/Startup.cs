using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOROPO.SAPRESSC.Core.Persistence.EF.Extensions;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Security.Common.Persistence.Extensions;
using MOROPO.SAPRESSC.Core.Security.Common.Web.Frontend.Extensions;
using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Extensions;
using MOROPO.SAPRESSC.Middleware.Security.Identity.Web.Backend.Extensions;
using MOROPO.SAPRESSC.Middleware.Security.Identity.Web.Common.Extensions;
using MOROPO.UIC.CoInnova.Persistence.Entities;
using Serilog;
using SmartSolucionesCuba.AspNetCore.Authentication.ApiKey.Extensions;
using System.Linq;

namespace MOROPO.UIC.CoInnova
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

            this.configuration = configuration;
            this.env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Options.MediaThumbnailResizeOptions>(configuration.GetSection("MediaThumbnailResize"));
            services.Configure<SAPRESSC.Middleware.Media.Web.Common.Options.RelativeMediaFileManagementOptions>(configuration.GetSection("FileManagement"));

            services.AddDbContext<Persistence.EF.ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<DbContext, Persistence.EF.ApplicationDbContext>();

            services.PerformCoreWebSecurityCommonFrontendServiceRegistration();
            services.PerformMiddlewareSecurityIdentityWebCommonServiceRegistration();
            services.PerformMiddlewareSecurityIdentityWebBackendServiceRegistration();
            services.PerformMiddlewareMediaWebCommonRelativePathFileManagementServiceRegistration();

            services.AddScoped<IEntityRepository<SAPRESSC.Core.Persistence.EF.Entities.Trace, long>, SAPRESSC.Core.Persistence.EF.Repositories.TraceRepository>();
            services.AddScoped<IEntityRepository<ApiKey, System.Guid>, Persistence.EF.Repositories.ApiKeyRepository>();
            services.AddScoped<IEntityRepository<PrototypingLevel, System.Guid>, Persistence.EF.Repositories.PrototypingLevelRepository>();
            services.AddScoped<IEntityRepository<Collaborator, System.Guid>, Persistence.EF.Repositories.CollaboratorRepository>();
            services.AddScoped<IEntityRepository<IdeaWorkflow, System.Guid>, Persistence.EF.Repositories.IdeaWorkflowRepository>();
            services.AddScoped<IEntityRepository<IdeaWorkflowStep, System.Guid>, Persistence.EF.Repositories.IdeaWorkflowStepRepository>();
            services.AddScoped<IEntityRepository<ChallangeWorkflow, System.Guid>, Persistence.EF.Repositories.ChallangeWorkflowRepository>();
            services.AddScoped<IEntityRepository<ChallangeWorkflowStep, System.Guid>, Persistence.EF.Repositories.ChallangeWorkflowStepRepository>();

            services.AddSingleton<SAPRESSC.Core.Persistence.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade, Persistence.EF.Providers.DbSchemaConfigurationProvider>();

            services.AddSingleton<SAPRESSC.Core.Persistence.EF.Helpers.Facade.IDataInitializationHelper, Helpers.DataInitializationHelper>();

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
            services.AddSingleton<SAPRESSC.Core.Web.Security.Helpers.Facade.ILicenseValidationHelperFacade, Helpers.WillcardLicenseValidationHelper>();

            services.AddTransient<SAPRESSC.Core.Web.Security.Models.View.Contrat.IUserProfileViewModel, Models.View.UserProfileViewModel>();

            services.AddSingleton<SAPRESSC.Core.Web.Common.ModelBinding.IConcretModelBindHelper, Helpers.ConcretModelBindHelper>();

            services.AddScoped<SAPRESSC.Core.Web.Security.Managers.Facade.IUserProfileManagerFacade, Managers.UserProfileManager>();
            #endregion

            #region Globalization configuration
            var defaultCulture = new System.Globalization.CultureInfo("es");

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(defaultCulture);
                options.SupportedCultures = new[] { defaultCulture };
                options.SupportedUICultures = new[] { defaultCulture };

                options.RequestCultureProviders = new[] { new SAPRESSC.Core.Web.Common.Localization.UrlRequestCultureProvider() };
            });

            services.AddSingleton<Helpers.GlobalViewLocalizationHelper>();
            #endregion

            services.Configure<Microsoft.AspNetCore.Routing.RouteOptions>(options => options.LowercaseUrls = true);

            services.AddResponseCaching();
            services.AddMemoryCache();
            services.AddSession();

            services.AddMvc(mvc =>
                mvc.ModelBinderProviders.Insert(0, new SAPRESSC.Core.Web.Common.ModelBinding.AbstractsModelBinderProvider())
            )
            .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization(options =>
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    return factory.Create(typeof(SharedResources));
                }
            )
            .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_0);

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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions> requestLocalizationOptions, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory, DbContext dbContext)
        {
            loggerFactory.AddSerilog(Log.Logger);

            app.UseRequestLocalization(requestLocalizationOptions.Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else if (env.IsStaging())
            {
                dbContext.Database.Migrate();

                app.PerformDatabaseInitialization();

                if (!(dbContext as Persistence.EF.ApplicationDbContext).Users.Any())
                {
                    var userId = configuration["DefaultUser:Id"];

                    var defaultUser = new SAPRESSC.Middleware.Security.Identity.Persistence.Entities.User
                    {
                        Id = userId,
                        Password = configuration["DefaultUser:Password"].ToHashedPassword(),
                        Enabled = true,
                        HasFullAccess = true,
                        PasswordChangeRequired = true,
                        Identity = new SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity
                        {
                            Name = userId,
                            Sex = SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Unknown,
                            CivilStatus = SAPRESSC.Middleware.Identity.Persistence.Enumerations.CivilStatus.Unknow
                        }
                    };

                    defaultUser.Signature = SAPRESSC.Middleware.Security.Identity.Persistence.EF.Helpers.Integrity.UserIntegrityHelper.GetSignature(defaultUser);

                    dbContext.Set<SAPRESSC.Middleware.Security.Identity.Persistence.Entities.User>().Add(defaultUser);

                    (dbContext as SAPRESSC.Core.Persistence.EF.Context.AbstractTraceableDbContext).SaveChanges("system");
                }
            }
            else
            {
                app.UseExceptionHandler("/error");

                app.UseHsts();
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
