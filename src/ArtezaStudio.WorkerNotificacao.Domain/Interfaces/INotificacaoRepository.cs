using ArtezaStudio.WorkerNotificacao.Domain.Entities;

namespace ArtezaStudio.WorkerNotificacao.Domain.Interfaces;

public interface INotificacaoRepository
{
    /// <summary>
    /// Salva uma nova notificação no banco de dados.
    /// </summary>
    Task SalvarAsync(Notificacao notificacao);

    /// <summary>
    /// Atualiza uma notificação existente no banco de dados, identificada pelo seu Id.
    /// </summary>
    Task AtualizarAsync(Notificacao notificacao);

    /// <summary>
    /// Obtém uma notificação do banco de dados pelo seu Id.
    /// </summary>
    Task<Notificacao?> ObterPorIdAsync(Guid id);
}