using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTiquetes.DataBase;
using WebApiTiquetes.Models;

namespace WebApiTiquetes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly ContextoBD _contexto;

        public UsuariosController(ContextoBD contexto)
        { 

         _contexto = contexto;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetUsuarios()
        {
            return await _contexto.usuarios.ToListAsync();
        }
            [HttpGet("{id}")]
            public async Task<ActionResult<Usuarios>> GetUsuario(int id)
            {
                var usuario = await _contexto.usuarios.FindAsync(id);
                if (usuario == null ) return NotFound();
                return usuario;
            }
            [HttpPost]
            public async Task<ActionResult<Usuarios>> CrearUsuario( Usuarios usuario)
            {
                usuario.us_fecha_adicion = DateTime.UtcNow;
                _contexto.usuarios.Add(usuario);
                await _contexto.SaveChangesAsync();
                return CreatedAtAction(nameof(GetUsuario), new { id = usuario.us_identificador}, usuario);
            }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, Usuarios usuario)
        {
            if (id != usuario.us_identificador) return BadRequest();
            var usuarioExistente = await _contexto.usuarios.FindAsync(id);
            if (usuarioExistente == null) return NotFound();

            usuarioExistente.us_identificador = usuario.us_identificador;
            usuarioExistente.us_nombre_completo = usuario.us_nombre_completo;
            usuarioExistente.us_correo = usuario.us_correo;
            usuarioExistente.us_clave = usuario.us_clave;
            usuarioExistente.us_ro_identificador = usuario.us_ro_identificador;
            usuarioExistente.us_estado = usuario.us_estado;
            usuarioExistente.us_fecha_adicion = usuario.us_fecha_adicion;
            usuarioExistente.us_adicionado_por = usuario.us_adicionado_por;
            usuarioExistente.us_fecha_modificacion = usuario.us_fecha_modificacion;
            usuarioExistente.us_modificado_por = usuario.us_modificado_por;

            await _contexto.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuario = await _contexto.usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            _contexto.usuarios.Remove(usuario);
            return NoContent();
        }



        
       
    }
}
