namespace SGE.Models
{
    public class ReservaSala
    {
        public Guid ReservaSalaId { get; set; }
        public Guid SalaId { get; set; }
        public Sala? Sala { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public DateOnly DataReserva { get; set; }
        public DateOnly DataFimReserva { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFim { get; set; }
        public bool CadAtivo { get; set; }
        public DateTime? CadInativo { get; set; }
        public string? CorReserva { get; set; }
    }
}
