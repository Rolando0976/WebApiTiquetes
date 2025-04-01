using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTiquetes.DataBase;
using WebApiTiquetes.Models;

namespace WebApiTiquetes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : Controller
    {
        private readonly ContextoBD _contexto;

        public TicketsController(ContextoBD contexto) {

            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticketes>>> GetTicketes()
        {
            return await _contexto.tiquetes.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticketes>> GetTiquete(int id)
        {
            var tiquete = await _contexto.tiquetes.FindAsync(id);
            if (tiquete == null) return NotFound();
            return tiquete;
        }
        [HttpPost]
        public async Task<ActionResult<Ticketes>> CrearTiquete(Ticketes tiquete)
        {
            tiquete.ti_fecha_adicion = DateTime.UtcNow;
            _contexto.tiquetes.Add(tiquete);
            await _contexto.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTiquete), new { id = tiquete.ti_identificador }, tiquete);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActializarTiquete(int id, Ticketes tiquete)
        {
            if (id != tiquete.ti_identificador) return BadRequest();
            var tiqueteExistente = await _contexto.tiquetes.FindAsync(id);
            if(tiqueteExistente == null) return NotFound();



            tiqueteExistente.ti_identificador = tiquete.ti_identificador;
            tiqueteExistente.ti_asunto = tiquete.ti_asunto;
            tiqueteExistente.ti_categoria = tiquete.ti_categoria;
            tiqueteExistente.ti_us_id_asigna = tiquete.ti_us_id_asigna;
            tiqueteExistente.ti_urgencia = tiquete.ti_urgencia;
            tiqueteExistente.ti_importancia = tiquete.ti_importancia;
            tiqueteExistente.ti_estado = tiquete.ti_estado;
            tiqueteExistente.ti_fecha_adicion = tiquete.ti_fecha_adicion;
            tiqueteExistente.ti_adicionado_por = tiquete.ti_adicionado_por;
            tiqueteExistente.ti_fecha_modificacion = tiquete.ti_fecha_modificacion;
            tiqueteExistente.ti_modificado_por = tiquete.ti_modificado_por;

            await _contexto.SaveChangesAsync();
            return NoContent();


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarTiquete (int id)
        {
            var tickete = await _contexto.tiquetes.FindAsync(id);
            if (tickete == null) return NotFound();

            _contexto.tiquetes.Remove(tickete);
            await _contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
