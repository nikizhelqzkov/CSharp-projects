//using Microsoft.EntityFrameworkCore;
//using MyMicroservice.DataAccess.DataProvider.Clients;
//using MyMicroservice.DataAccess.DataProvider.Interfaces;
//using MyMicroservice.Db;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//builder.Services
//               .AddSwaggerGen()
//               .AddDbContext<BikeStoresDBContext>(options =>
//               {
//                   options.UseSqlServer("Data Source=NZHELYAZKOVCM\\SQLEXPRESS;Initial Catalog=BikeStores;Integrated Security=True");
//               })
//               .AddScoped<IStoreDataProvider, StoreProvider>()
//               .AddControllers()
//               .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseAuthorization();

//app.MapControllers();

//app.Run();



using Microsoft.EntityFrameworkCore;
using MyMicroservice.DataAccess.DataProvider.Clients;
using MyMicroservice.DataAccess.DataProvider.Interfaces;
using MyMicroservice.Db;
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

            //builder.Services.AddDbContext<FMCData81_DevHackathonContext>(options =>
            //{
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("FmcUser"));
            //});
            builder.Services.AddDbContext<BikeStoresDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Local"));
            })
                .AddScoped<IStoreService, StoreService>()
                .AddScoped<IStoreDataProvider, StoreProvider>()
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


