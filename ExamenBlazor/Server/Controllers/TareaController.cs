using ExamenBlazor.Shared.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public TareaController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Tarea tarea)
        {
            if(tarea.TareaId > 0)
            {
                tarea.TareaId = 0;
            }
            
            if(String.IsNullOrEmpty(tarea.Nombre))
            {
                string mensajeError = "El campo nombre es requerido";
                return BadRequest(mensajeError);
            }

            tarea.FechadeCreacion = DateTime.Now;
            context.Add(tarea);
            await context.SaveChangesAsync();
            return tarea.TareaId;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Tarea>>> GetTareasporMeta(int id)
        {
            return await context.Tareas.Where(x=>x.MetaId == id).ToListAsync();
        }

        [Route("ObtenerunaTarea/{id}")]
        [HttpGet]
        public async Task<ActionResult<Tarea>> Get(int id)
        {
            return await context.Tareas.Where(x => x.TareaId == id).FirstOrDefaultAsync();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Tarea tarea)
        {
            context.Attach(tarea).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Tareas.AnyAsync(x => x.TareaId == id);
            if (!existe) { return NotFound(); }
            context.Remove(new Tarea { TareaId = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        [Route("TerminarTarea")]
        [HttpPut]
        public async Task<ActionResult> PutTerminar(Tarea tarea)
        {
            tarea.Es_Completada = true;
            var oldtarea = await context.Tareas.FindAsync(tarea.TareaId);          
            context.Entry(oldtarea).CurrentValues.SetValues(tarea);

            await context.SaveChangesAsync();
            return NoContent();
        }

        [Route("MarcarImportante")]
        [HttpPut]
        public async Task<ActionResult> PutImportante(Tarea tarea)
        {
            tarea.Es_Importante = true;
            var oldtarea = await context.Tareas.FindAsync(tarea.TareaId);
            context.Entry(oldtarea).CurrentValues.SetValues(tarea);

            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
