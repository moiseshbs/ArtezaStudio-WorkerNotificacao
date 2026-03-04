using ArtezaStudio.WorkerNotificacao.Domain.Enums;

namespace ArtezaStudio.WorkerNotificacao.Application.Interfaces;

public interface ITemplateEmailService
{
    /// <summary>
    /// Gera o assunto e conteúdo do email baseado no tipo de notificação e nos dados do evento.
    /// </summary>
    (string Assunto, string Conteudo) GerarEmail(TipoNotificacao tipo, Dictionary<string, string> dados);
}