using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Progra620241_Assets_FreddyGonzalez.Models;

internal class Program {
    private static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        //aca agregamos los valores de conexion a la bd.
        //1 agregar la info en una etiqueta en el archivo appsettings.json
        var CnnStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("CNNSTR"));
        //por seguridad es mejor agregar la info de la contrasena en este codigo o podriamos tener un almacen externo. Investigar que son los user Secrets
        CnnStrBuilder.Password = "progra6";

        //ahora que hemos extraido la cadena de conexion creamos una variable tipo string que la almacene.
        string CnnStr = CnnStrBuilder.ConnectionString;
        //ahora conectamos el proyecto a la bd
        builder.Services.AddDbContext<Progra620241Context>(options => options.UseSqlServer(CnnStr));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}