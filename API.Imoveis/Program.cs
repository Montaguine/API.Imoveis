
using API.Imoveis.Controllers;

namespace API.Imoveis
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapGet("/imovel", () =>
            {
                var imovel = ImovelController.lista;
                return imovel;
            });

            app.MapGet("/imovel/{id}", (int id) =>
            {
                var busca = ImovelController.lista.Where(x => x.Id == id).FirstOrDefault();
                return busca;
            });

            app.MapPost("/imovel/{id}/{nome}", (int id, string nome) =>
            {
                Console.WriteLine(ImovelController.VerificaId(id) + "Estadp da verificacao");
                if (ImovelController.VerificaId(id))
                {
                    return "Falha! Id já cadastrado!";
                }
                else
                {
                    ImovelController.NovoImovel(id, nome);
                    return "Cadastrado com sucesso!";
                }    
            });

            app.MapDelete("/imovel/{id}", (int id) =>
            {
                ImovelController.DeletarImovel(id);
                return "deletado com sucesso!";
            });

            app.MapPut("/imovel/{id}/{nome}", (int id, string nome) =>
            {
                ImovelController.Atualizar(id, nome);
                return "atualizado com sucesso!";
            });

            app.Run();
        }
    }
}
