using Microsoft.EntityFrameworkCore;
using SGE.Models;

namespace SGE.Data
{
    public class SGEContext : DbContext
    {
        public SGEContext(DbContextOptions<SGEContext> options) : base(options)
        { }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<AlunoTurma> AlunosTurma { get; set; }
        public DbSet<ReservaSala> ReservasSala { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoUsuario> TiposUsuario { get; set; }
        public DbSet<Ocorrencia> Ocorrencias { get; set; }
        public DbSet<TipoOcorrencia> TiposOcorrencia { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>().ToTable("Aluno");
            modelBuilder.Entity<AlunoTurma>().ToTable("AlunoTurma");
            modelBuilder.Entity<ReservaSala>().ToTable("ReservaSala");
            modelBuilder.Entity<Sala>().ToTable("Sala");
            modelBuilder.Entity<Turma>().ToTable("Turma");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<TipoUsuario>().ToTable("TipoUsuario");
            modelBuilder.Entity<Ocorrencia>().ToTable("Ocorrencia");
            modelBuilder.Entity<TipoOcorrencia>().ToTable("TipoOcorrencia");
        }
    }
}
