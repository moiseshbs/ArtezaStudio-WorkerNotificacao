namespace ArtezaStudio.WorkerNotificacao.Domain.Enums;

public enum TipoNotificacao
{
    /// <summary>
    /// Notificação relacionada a um comentário.
    /// </summary>
    Comentario = 1,

    /// <summary>
    /// Notificação relacionada a uma curtida.
    /// </summary>
    Curtida = 2,
    
    /// <summary>
    /// Notificação relacionada a um novo seguidor.
    /// </summary>
    Seguidor = 3
}