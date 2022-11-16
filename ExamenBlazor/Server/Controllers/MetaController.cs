using ExamenBlazor.Shared.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class MetaController : ControllerBase
    {
        private readonly ApplicationDbContext context;       
        public MetaController(ApplicationDbContext context)
        {
            this.context = context;          
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Meta meta)
        {           
            context.Add(meta);
            await context.SaveChangesAsync();
            return meta.MetaId;
        }

        [HttpGet]     
        public async Task<ActionResult<List<Meta>>> Get()
        {
            return await context.Metas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Meta>> Get(int id)
        {
            return await context.Metas.Where(x => x.MetaId == id).Include(x=>x.ListadeTareas).FirstOrDefaultAsync();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Meta meta)
        {
            context.Attach(meta).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Metas.AnyAsync(x => x.MetaId == id);
            if (!existe) { return NotFound(); }
            context.Remove(new Meta { MetaId = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
