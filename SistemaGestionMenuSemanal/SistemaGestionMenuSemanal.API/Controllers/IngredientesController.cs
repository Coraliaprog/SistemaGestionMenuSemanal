using Microsoft.AspNetCore.Mvc;
using SistemaGestionMenuSemanal.API.Models.Entities;

namespace SistemaGestionMenuSemanal.API.Controllers
{
    [ApiController]
    [Route("api/ingredientes")]
    public class IngredientesController : ControllerBase
    {
        private static readonly List<Ingrediente> _ingredientes = new List<Ingrediente>
        {
            new Ingrediente { Id = 1, Nombre = "Arroz", Categoria = "Grano", Cantidad = 10, IsActive = true },
            new Ingrediente { Id = 2, Nombre = "Pollo", Categoria = "Proteína", Cantidad = 5, IsActive = true },
            new Ingrediente { Id = 3, Nombre = "Lechuga", Categoria = "Vegetal", Cantidad = 8, IsActive = true }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Ingrediente>> GetAll()
        {
            return Ok(_ingredientes);
        }

        [HttpGet("{id}")]
        public ActionResult<Ingrediente> GetById(int id)
        {
            var ingrediente = _ingredientes.FirstOrDefault(i => i.Id == id);

            if (ingrediente == null)
                return NotFound();

            return Ok(ingrediente);
        }

        [HttpPost]
        public ActionResult<Ingrediente> Create(Ingrediente ingrediente)
        {
            if (string.IsNullOrWhiteSpace(ingrediente.Nombre))
                return BadRequest("El nombre es obligatorio.");

            ingrediente.Id = _ingredientes.Max(i => i.Id) + 1;
            _ingredientes.Add(ingrediente);

            return CreatedAtAction(nameof(GetById), new { id = ingrediente.Id }, ingrediente);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Ingrediente ingredienteActualizado)
        {
            var ingrediente = _ingredientes.FirstOrDefault(i => i.Id == id);

            if (ingrediente == null)
                return NotFound();

            ingrediente.Nombre = ingredienteActualizado.Nombre;
            ingrediente.Categoria = ingredienteActualizado.Categoria;
            ingrediente.Cantidad = ingredienteActualizado.Cantidad;
            ingrediente.IsActive = ingredienteActualizado.IsActive;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ingrediente = _ingredientes.FirstOrDefault(i => i.Id == id);

            if (ingrediente == null)
                return NotFound();

            _ingredientes.Remove(ingrediente);

            return NoContent();
        }
    }
}