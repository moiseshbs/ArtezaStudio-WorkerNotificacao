namespace ArtezaStudio.WorkerNotificacao.Domain.Enums;

public enum StatusNotificacao
{
    /// <summary>
    /// Notificação que ainda não foi processada ou enviada.
    /// </summary>
    Pendente = 1,

    /// <summary>
    /// Notificação que foi processada e enviada com sucesso ao destinatário.
    /// </summary>
    Enviada = 2,

    /// <summary>
    /// Notificação que falhou ao ser enviada.
    /// </summary>
    Falha = 3
}