using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TarefaAPI.Data;
using TarefaAPI.DTO;
using TarefaAPI.Models;

namespace TarefaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TarefasController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var tarefas = await _context.Tarefas
                .Select(t => new TarefaResponseDTO
                {
                    Id = t.Id,
                    Titulo = t.Titulo,
                    Descricao = t.Descricao,
                    Status = t.Status.ToString()
                })
                .ToListAsync();

            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa is null)
                return NotFound();

            var response = new TarefaResponseDTO
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                Status = tarefa.Status.ToString()
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TarefaCreateDTO dto)
        {
            var tarefa = new Tarefa
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Status = StatusTarefa.Pendente
            };

            _context.Tarefas.Add(tarefa);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { id = tarefa.Id},
                tarefa
                );

        }
    }
}
