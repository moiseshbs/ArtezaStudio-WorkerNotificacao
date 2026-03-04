using ArtezaStudio.WorkerNotificacao.Domain.Enums;

namespace ArtezaStudio.WorkerNotificacao.Domain.Entities;

public class Notificacao
{
    /// <summary>
    /// Identificador único da notificação.
    /// </summary>
    public Guid MessageId { get; private set; }

    /// <summary>
    /// Identificador do usuário destinatário da notificação.
    /// </summary>
    public long UsuarioId { get; private set; }

    /// <summary>
    /// Endereço de e-mail do destinatário da notificação.
    /// </summary>
    public string EmailDestino { get; private set; }

    /// <summary>
    /// Tipo da notificação, (Comentario, curtida, seguidor).
    /// </summary>
    public TipoNotificacao Tipo { get; private set; }

    /// <summary>
    /// Assunto da notificação.
    /// </summary>
    public string Assunto { get; private set; }

    /// <summary>
    /// Conteúdo da notificação.
    /// </summary>
    public string Conteudo { get; private set; }

    /// <summary>
    /// Status da notificação, indicando se ela está pendente, enviada ou falhou.
    /// </summary>
    public StatusNotificacao Status { get; private set; }

    /// <summary>
    /// Data e hora em que a notificação foi criada, gerada automaticamente no momento da criação.
    /// </summary>
    public DateTime DataCriacao { get; private set; }

    /// <summary>
    /// Data e hora em que a notificação foi enviada, ou null se ainda não foi enviada.
    /// </summary>
    public DateTime? DataEnvio { get; private set; }

    /// <summary>
    /// Número de tentativas de envio realizadas para esta notificação, começando em 0 e incrementando a cada tentativa de envio, mesmo que falhe.
    /// </summary>
    public int TentativasEnvio { get; private set; }

    /// <summary>
    /// Mensagem de erro detalhada em caso de falha no envio da notificação, ou null se a notificação foi enviada com sucesso ou ainda não foi enviada.
    /// </summary>
    public string? MensagemErro { get; private set; }

    /// <summary>
    /// Construtor para criar uma nova notificação,
    /// e definindo o Status como Pendente e TentativasEnvio como 0.
    /// </summary>
    public Notificacao(Guid messageId, long usuarioId, string emailDestino, TipoNotificacao tipo, string assunto, string conteudo)
    {
        MessageId = messageId;
        UsuarioId = usuarioId;
        EmailDestino = emailDestino;
        Tipo = tipo;
        Assunto = assunto;
        Conteudo = conteudo;
        Status = StatusNotificacao.Pendente;
        DataCriacao = DateTime.UtcNow;
        TentativasEnvio = 0;
    }

    /// <summary>
    /// Marca a notificação como enviada.
    /// </summary>
    public void MarcarComoEnviada()
    {
        Status = StatusNotificacao.Enviada;
        DataEnvio = DateTime.UtcNow;
    }

    /// <summary>
    /// Marca a notificação como falha.
    /// </summary>
    public void MarcarComoFalha(string mensagemErro)
    {
        TentativasEnvio++;
        MensagemErro = mensagemErro;
        Status = TentativasEnvio >= 3 ? StatusNotificacao.Falha : StatusNotificacao.Pendente;
    }
}