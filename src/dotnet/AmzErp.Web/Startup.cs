using AmzErp.Common.Logging.Trace;
using AmzErp.Common.Model;
using AmzErp.Common.Mvc;
using AmzErp.Common.Mvc.Filters;
using AmzErp.Common.Mvc.JsonConverter;
using AmzErp.Common.Mvc.ModelBinder.Json.Net;
using AmzErp.DataAccess;
using AmzErp.Web.Code.Logging.Trace;
using LogDashboard;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using MassTransit;

namespace AmzErp.Web
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


            #region  MVC
            var mvcBuilder = services.AddControllers(option =>
            {
                option.Filters.Add<ExceptionFilter>();
                option.Filters.Add<RequestLoggerFilter>();
                option.Filters.Add<AuthorizeFilter>();
                option.ModelBinderProviders.Insert(0, new JObjectModelBinderProvider());
            })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                    options.SuppressConsumesConstraintForFormFileParameters = true;
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                    options.JsonSerializerOptions.Converters.Add(new DateTimeNullConverter());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            var feature = new ControllerFeature();
            mvcBuilder.PartManager.PopulateFeature(feature);

            foreach (var controller in feature.Controllers.Select(c => c.AsType()))
            {
                mvcBuilder.Services.TryAddTransient(controller, controller);
            }

            mvcBuilder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ControllerActivator>());
            services.AddHttpContextAccessor();
            #endregion MVC
            services.AddAutoDependencyInjection();

            #region Add DbConnection
            services.AddDbContext<AmzErpDbContext>(options =>
            {
                var dbConnection = Configuration.GetSection("ConnectionStings:DbConnection")?.Value ??
                                 throw new InvalidOperationException("Invalid Databse Connection");
                options.UseMySql(dbConnection, ServerVersion.AutoDetect(dbConnection));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            #endregion
            services.AddDistributedMemoryCache();
            services.AddSession(options => { options.Cookie.Name = "sessionId"; });

            var redisClient = new CSRedis.CSRedisClient(
                Configuration.GetSection("ConnectionStings:RedisConfig")?.Value ??
                throw new InvalidOperationException("Invalid Redis Connection"));

            services.AddSingleton(Configuration.GetSection("AmzDeveloper").Get<AmzDeveloperConfig>());
            services.AddSingleton(redisClient);
            services.AddSingleton<IDistributedCache>(new Microsoft.Extensions.Caching.Redis.CSRedisCache(redisClient));
            services.AddSingleton<IRequestTrace, RequestTraceService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AmzErpDbContext context, ILoggerFactory loggerFactory)
        {
            #region Add Logging Service
            loggerFactory.AddLog4Net(Path.Combine(env.ContentRootPath, "log4net.config"));
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           // app.UseLogDashboard();
            app.UseRouting();
            app.UseAuthorization().UseSession();

            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    ContentTypeProvider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider(),
            //    FileProvider = env.WebRootFileProvider,
            //    RequestPath = "",
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            DbInitializer.Initializer(context);
        }
    }
}
