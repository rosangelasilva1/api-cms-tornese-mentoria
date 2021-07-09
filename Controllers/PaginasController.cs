using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using admin_cms.Models.Infraestrutura.Database;
using admin_cms.Models.Dominio.Entidades;

namespace admin_cms.Controllers
{
    public class PaginasController : ControllerBase
    {
        private readonly ContextoCms _context;

        public PaginasController(ContextoCms context)
        {
            _context = context;
        }

        // GET: Home
        [HttpGet]
        [Route("/")]
        public IActionResult Root()
        {
            return StatusCode(200,new {Mensagem="Bem Vindo(a) a API"});
        }

        // GET: Paginas
        [HttpGet]
        [Route("/api/paginas.json")]
        public async Task<IActionResult> Index()
        {
            return StatusCode(200,await _context.Paginas.ToListAsync());
        }

        [HttpPost]
        [Route("/api/paginas.json")]
        public async Task<IActionResult> Create([FromBody] Pagina pagina)
        {
            _context.Add(pagina);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }        

        [HttpPut]
        [Route("/api/paginas/{id}.json")]
        public async Task<IActionResult> Update(int id,[FromBody] Pagina pagina)
        {
            pagina.Id = id;
            _context.Update(pagina);
            await _context.SaveChangesAsync();
            return StatusCode(200);
        }        

        [HttpDelete]
        [Route("/api/paginas/{id}.json")]
        public async Task<IActionResult> Delete(int id)
        {
              
            if (id == 0)
            {
                return StatusCode(400,new {Mensagem="O Id é Obrigatorio"});
            }

            var pagina = await _context.Paginas.FirstOrDefaultAsync(m => m.Id == id);
            if (pagina == null)
            {
                return StatusCode(404,new {Mensagem="A Pagina não foi encontrada"});
            }

            _context.Paginas.Remove(pagina);
            await _context.SaveChangesAsync();
            return StatusCode(204);
        }        

    }
}
