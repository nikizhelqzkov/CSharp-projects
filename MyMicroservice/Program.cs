using Microsoft.EntityFrameworkCore;
using MyMicroservice.DataAccess.DataProvider.Clients;
using MyMicroservice.DataAccess.DataProvider.Interfaces;
using MyMicroservice.DataContext;
using MyMicroservice.Helper;
using MyMicroservice.Services;

namespace BikeApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            //builder.Services.AddDbContext<FMCData81_DevHackathonContext>(options =>
            //{
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("FmcUser"));
            //});
            builder.Services.AddDbContext<BikeStoresDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Local"));
            })
                .AddScoped<IStoreService, StoreService>()
                .AddScoped<IOrderService, OrderService>()
                .AddScoped<IStoreDataProvider, StoreProvider>()
                .AddScoped<IOrderProvider, OrderProvider>()
             .AddControllers()
             .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseRouting();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Test1 Api v1");
                });
            }
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}


