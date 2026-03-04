using ArtezaStudio.WorkerNotificacao.Application.DTOs;
using ArtezaStudio.WorkerNotificacao.Application.Interfaces;
using ArtezaStudio.WorkerNotificacao.Domain.Entities;
using ArtezaStudio.WorkerNotificacao.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace ArtezaStudio.WorkerNotificacao.Application.UseCases;

public class ProcessarNotificacaoUseCase : IProcessarNotificacaoUseCase
{
    private readonly INotificacaoRepository _repository;
    private readonly IEmailService _emailService;
    private readonly ITemplateEmailService _templateService;
    private readonly ILogger<ProcessarNotificacaoUseCase> _logger;

    public ProcessarNotificacaoUseCase(
        INotificacaoRepository repository,
        IEmailService emailService,
        ITemplateEmailService templateService,
        ILogger<ProcessarNotificacaoUseCase> logger)
    {
        _repository = repository;
        _emailService = emailService;
        _templateService = templateService;
        _logger = logger;
    }

    public async Task ExecutarAsync(NotificacaoMessageDto notificacaoMessageDto)
    {
        // Gerar conteúdo do email baseado no template
        var (assunto, conteudo) = _templateService.GerarEmail(notificacaoMessageDto.TipoNotificacao, notificacaoMessageDto.DadosEvento);
        
        // Criar entidade de notificação
        var notificacao = new Notificacao(
            notificacaoMessageDto.MessageId,
            notificacaoMessageDto.UsuarioId,
            notificacaoMessageDto.EmailUsuario,
            notificacaoMessageDto.TipoNotificacao,
            assunto,
            conteudo
        );

        await _repository.SalvarAsync(notificacao);

        // Tentar enviar email
        try
        {
            var sucesso = await _emailService.EnviarEmailAsync(
                notificacao.EmailDestino,
                notificacao.Assunto,
                notificacao.Conteudo
            );

            if (sucesso)
            {
                notificacao.MarcarComoEnviada();
            }
            else
            {
                notificacao.MarcarComoFalha("Falha no envio do email");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar notificação {MessageId}", notificacao.MessageId);
            notificacao.MarcarComoFalha(ex.Message);
        }

        await _repository.AtualizarAsync(notificacao);
    }
}