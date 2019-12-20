using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Common;
using Autofac;
using System.IO;
using System.Reflection;
using Autofac.Extras.DynamicProxy;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Common.RedisHelper;

namespace CommonBaseRole
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
            services.AddControllers();

            //注入 Redis 缓存
            services.AddScoped<IRedisCacheManager, RedisCacheManager>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,//是否验证秘钥
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Consts.SecurityKey)),

                        ValidateIssuer = true,//是否验证发送者
                        ValidIssuer = Consts.Issuer,

                        ValidateAudience = true,//是否验证接收者
                        ValidAudience = Consts.Audience,

                        ValidateLifetime = true,//是否验证过期时间
                        ClockSkew = TimeSpan.Zero,

                    };
                });

            #region Swagger
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "CommonBaseRole API",
                    Description = "项目接口说明书",
                    Contact = new OpenApiContact { },
                    License = new OpenApiLicense { },
                });
                c.OrderActionsBy(o => o.RelativePath);

                var xmlPath = Path.Combine(basePath, "CommonBaseRole.xml");
                c.IncludeXmlComments(xmlPath, true);//默认第二个参数为False，这个是controller的注释
            });
            #endregion
        }

        #region Autofac
        /// <summary>
        /// 使用Autofac注入配置
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //直接注册某一个类和接口
            //左边的是实现类，右边的AS是接口
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            //builder.RegisterType<WebSiteServices>().As<IWebSiteServices>();


            //注册要通过反射创建的组件
            var serviceDllFile = Path.Combine(basePath, "Services.dll");
            var assemblysServices = Assembly.LoadFile(serviceDllFile);

            builder.RegisterAssemblyTypes(assemblysServices)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .EnableInterfaceInterceptors();

            var repositoryDllFile = Path.Combine(basePath, "Repository.dll");
            var assemblysRepository = Assembly.LoadFile(repositoryDllFile);

            builder.RegisterAssemblyTypes(assemblysRepository)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .EnableInterfaceInterceptors();
        }
        #endregion

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                #region Swagger
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/swagger/v1/swagger.json", ".NET Core 3.1 V1");
                });
                #endregion
            }


            app.UseStatusCodePages();//把错误码返回到前台

            app.UseRouting();

            app.UseAuthentication();//开启认证中间件

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
