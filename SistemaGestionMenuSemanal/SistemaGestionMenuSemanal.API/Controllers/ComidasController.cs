using Microsoft.AspNetCore.Mvc;
using SistemaGestionMenuSemanal.API.Models.Entities;

namespace SistemaGestionMenuSemanal.API.Controllers
{
    [ApiController]
    [Route("api/comidas")]
    public class ComidasController : ControllerBase
    {
        private static readonly List<Comida> _comidas = new List<Comida>
        {
            new Comida { Id = 1, Nombre = "Arroz con pollo", Tipo = "Almuerzo", DiaSemana = "Lunes", IsActive = true },
            new Comida { Id = 2, Nombre = "Avena con frutas", Tipo = "Desayuno", DiaSemana = "Martes", IsActive = true },
            new Comida { Id = 3, Nombre = "Ensalada de tuna", Tipo = "Cena", DiaSemana = "Miércoles", IsActive = true }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Comida>> GetAll()
        {
            return Ok(_comidas);
        }

        [HttpGet("{id}")]
        public ActionResult<Comida> GetById(int id)
        {
            var comida = _comidas.FirstOrDefault(c => c.Id == id);

            if (comida == null)
            {
                return NotFound();
            }

            return Ok(comida);
        }

        [HttpPost]
        public ActionResult<Comida> Create(Comida comida)
        {
            if (string.IsNullOrWhiteSpace(comida.Nombre))
            {
                return BadRequest("Nombre de la comida es requerido.");
            }

            int newId = _comidas.Any() ? _comidas.Max(c => c.Id) + 1 : 1;

            comida.Id = newId;
            comida.IsActive = true;

            _comidas.Add(comida);

            return CreatedAtAction(nameof(GetById), new { id = comida.Id }, comida);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Comida comida)
        {
            var existing = _comidas.FirstOrDefault(c => c.Id == id);

            if (existing == null)
            {
                return NotFound();
            }

            existing.Nombre = comida.Nombre;
            existing.Tipo = comida.Tipo;
            existing.DiaSemana = comida.DiaSemana;
            existing.IsActive = comida.IsActive;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _comidas.FirstOrDefault(c => c.Id == id);

            if (existing == null)
            {
                return NotFound();
            }

            _comidas.Remove(existing);

            return NoContent();
        }
    }
}