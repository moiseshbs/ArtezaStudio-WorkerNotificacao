namespace ArtezaStudio.WorkerNotificacao.Application.Interfaces;

public interface IEmailService
{
    /// <summary>
    /// Envia um email para o destinatário especificado com o assunto e conteúdo fornecidos.
    /// </summary>
    Task<bool> EnviarEmailAsync(string destinatario, string assunto, string conteudo);
}