using ArtezaStudio.WorkerNotificacao.Application.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace ArtezaStudio.WorkerNotificacao.Application.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<bool> EnviarEmailAsync(string destinatario, string assunto, string conteudo)
    {
        try
        {
            var remetenteEmail = _configuration["Email:RemetenteEmail"];
            if (string.IsNullOrEmpty(remetenteEmail))
            {
                _logger.LogError("Configuração 'Email:RemetenteEmail' não encontrada ou vazia");
                return false;
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(
                _configuration["Email:RemetenteNome"],
                remetenteEmail
            ));
            message.To.Add(MailboxAddress.Parse(destinatario));
            message.Subject = assunto;

            var bodyBuilder = new BodyBuilder { HtmlBody = conteudo };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(
                _configuration["Email:SmtpHost"],
                int.Parse(_configuration["Email:SmtpPort"] ?? "587"),
                SecureSocketOptions.StartTls
            );

            await client.AuthenticateAsync(
                _configuration["Email:Usuario"],
                _configuration["Email:Senha"]
            );

            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            _logger.LogInformation("Email enviado com sucesso para {Destinatario}", destinatario);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao enviar email para {Destinatario}", destinatario);
            return false;
        }
    }
}