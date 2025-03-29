using Microsoft.AspNetCore.Mvc;
using WebApiTiquetes.DataBase;



using WebApiTiquetes.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiTiquetes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : Controller
    {
        private readonly ContextoBD _contexto;

        public RolesController(ContextoBD contexto) { 
        
        
        _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roles>>> GetRoles() 
        { 
        return await _contexto.roles.ToListAsync(); 
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Roles>> GetRol(int id)
        {
            var rol = await _contexto.roles.FindAsync(id);
            if (rol == null) return NotFound();
            return rol;
        }
        [HttpPost]
        public async Task<ActionResult<Roles>> CrearRol( Roles rol)
        {
            rol.ro_fecha_adicion = DateTime.UtcNow;
            _contexto.roles.Add(rol);
            await _contexto.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRol), new { id = rol.ro_identificador }, rol);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpddateRol(int id, Roles rol)
        {
            if (id != rol.ro_identificador) return BadRequest();
            var rolExistente = await _contexto.roles.FindAsync(id);
            if (rolExistente == null ) return NotFound();

            rolExistente.ro_descripcion = rol.ro_descripcion;
            rolExistente.ro_fecha_adicion = rol.ro_fecha_adicion;
            rolExistente.ro_adicionado_por = rol.ro_adicionado_por;
            rolExistente.ro_fecha_modificacion = DateTime.UtcNow;
            rolExistente.ro_modificado_por = rol.ro_modificado_por;

            await _contexto.SaveChangesAsync();
           return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarRol(int id)
        {
            var rol = await _contexto.roles.FindAsync(id);
            if (rol == null) return NotFound();

            _contexto.roles.Remove(rol);
            await _contexto.SaveChangesAsync();
            return NoContent();






        }
    }
}
