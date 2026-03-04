using ArtezaStudio.WorkerNotificacao.Domain.Enums;

namespace ArtezaStudio.WorkerNotificacao.Application.DTOs;

public record NotificacaoMessageDto
{
    /// <summary>
    /// Identificador único da mensagem, gerado automaticamente no momento da criação.
    /// </summary>
    public Guid MessageId { get; init; }

    /// <summary>
    /// Identificador do usuário destinatário da notificação.
    /// </summary>
    public long UsuarioId { get; init; }

    /// <summary>
    /// Endereço de e-mail do destinatário da notificação.
    /// </summary>
    public string EmailUsuario { get; init; } = string.Empty;

    /// <summary>
    /// Tipo da notificação, (Comentario, curtida, seguidor).
    /// </summary>
    public TipoNotificacao TipoNotificacao { get; init; } 

    /// <summary>
    /// Dados específicos do evento que gerou a notificação, como nome do usuário que comentou, título da publicação curtida, etc.
    /// </summary>
    public Dictionary<string, string> DadosEvento { get; init; } = new();
}