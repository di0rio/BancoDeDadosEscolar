namespace SGE.Models
{
    public class Sala
    {
        public Guid SalaId { get; set; }
        public string SalaNome { get; set; }
        public bool CadAtivo { get; set; }
        public DateTime? CadInativo { get; set; }
        public IEnumerable<ReservaSala>? Reservas { get; set; }
    }
}
