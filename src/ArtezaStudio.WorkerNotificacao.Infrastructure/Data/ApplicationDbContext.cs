using ArtezaStudio.WorkerNotificacao.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtezaStudio.WorkerNotificacao.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }

    public DbSet<Notificacao> Notificacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Notificacao>(entity =>
        {
            entity.HasKey(e => e.MessageId);
            
            entity.Property(e => e.EmailDestino)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.Assunto)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(e => e.Conteudo)
                .IsRequired();

            entity.Property(e => e.MensagemErro)
                .HasMaxLength(1000);

            entity.HasIndex(e => e.UsuarioId);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.DataCriacao);
        });
    }
}