using ArtezaStudio.WorkerNotificacao.Domain.Entities;
using ArtezaStudio.WorkerNotificacao.Domain.Interfaces;
using ArtezaStudio.WorkerNotificacao.Infrastructure.Data;

namespace ArtezaStudio.WorkerNotificacao.Infrastructure.Repositories;

public class NotificacaoRepository : INotificacaoRepository
{
    private readonly ApplicationDbContext _context;

    public NotificacaoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SalvarAsync(Notificacao notificacao)
    {
        await _context.Notificacoes.AddAsync(notificacao);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Notificacao notificacao)
    {
        _context.Notificacoes.Update(notificacao);
        await _context.SaveChangesAsync();
    }

    public async Task<Notificacao?> ObterPorIdAsync(Guid id)
    {
        return await _context.Notificacoes.FindAsync(id);
    }
}