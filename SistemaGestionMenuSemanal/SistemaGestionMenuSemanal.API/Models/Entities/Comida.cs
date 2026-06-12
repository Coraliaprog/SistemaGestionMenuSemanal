namespace SistemaGestionMenuSemanal.API.Models.Entities
{
    public class Comida
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string DiaSemana { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public List<Ingrediente> Ingredientes { get; set; } = new List<Ingrediente>();
    }
}