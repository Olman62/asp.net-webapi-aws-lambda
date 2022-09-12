using Amazon;
using Amazon.SecretsManager;
using Enyim.Caching.Configuration;

namespace WeatherLambdaApi2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAmazonSecretsManager>(a =>
                new AmazonSecretsManagerClient(RegionEndpoint.USEast1)
            );

            services.AddEnyimMemcached(memcachedClientOptions =>
            {
                memcachedClientOptions.Servers.Add(new Server
                {
                    //Address = "*****.***.cfg.use1.cache.amazonaws.com",
                    Address = "127.0.0.1", // This is a test local address.
                    Port = 11211
                });
            });

            services.AddControllers();

            // Configuring Swagger/OpenAPI
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseEnyimMemcached();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
                });
            });
        }
    }
}