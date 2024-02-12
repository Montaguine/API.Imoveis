using Microsoft.AspNetCore.Mvc;

namespace API.Imoveis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImovelController : ControllerBase
    {
        private static readonly string[] Proprietarios = new[]
{
            "A", "B", "C", "D", "E", "F", "G"
        };

        public static IEnumerable<Imoveis> lista = Get();
        public static void NovoImovel(int id, string nome)
        {
            var novo = new Imoveis
            {
                Id = id,
                Nome = nome
            };

            lista = lista.Append(novo);
        }

        public static bool VerificaId(int id)
        {
            var busca = lista.Where(x => x.Id == id).FirstOrDefault();
            Console.WriteLine(busca + "Conteudo da busca");
            if (busca != null)
            {   
                return true;
            }
            return false;
        }
        public static void DeletarImovel(int id)
        {
            lista = lista.Where(x => x.Id != id).ToList();
        }

        public static void Atualizar(int id, string nome)
        {
            var busca = lista.Where(x => x.Id == id).FirstOrDefault();
            busca.Nome = nome;
            DeletarImovel(id);
            lista = lista.Append(busca);
        }

        private readonly ILogger<ImovelController> _logger;

        public ImovelController(ILogger<ImovelController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetImovel")]
        private static IEnumerable<Imoveis> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Imoveis
            {
                Id = index,
                Nome = Proprietarios[Random.Shared.Next(Proprietarios.Length)]
            })
            .ToArray();
        }
    }
}




