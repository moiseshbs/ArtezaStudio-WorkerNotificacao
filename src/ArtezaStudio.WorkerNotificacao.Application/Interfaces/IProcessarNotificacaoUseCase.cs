using ArtezaStudio.WorkerNotificacao.Application.DTOs;

namespace ArtezaStudio.WorkerNotificacao.Application.Interfaces;

public interface IProcessarNotificacaoUseCase
{
    /// <summary>
    /// Processa uma notificação recebida, realizando as seguintes etapas:
    /// 1. Gerar o conteúdo do email baseado no tipo de notificação e nos dados do evento.
    /// 2. Criar uma entidade de notificação e salvar no banco de dados.
    /// </summary>
    Task ExecutarAsync(NotificacaoMessageDto notificacaoMessageDto);
}