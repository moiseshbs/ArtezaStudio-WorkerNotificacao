using ArtezaStudio.WorkerNotificacao.Application.Interfaces;
using ArtezaStudio.WorkerNotificacao.Domain.Enums;

namespace ArtezaStudio.WorkerNotificacao.Infrastructure.Services;

public class TemplateEmailService : ITemplateEmailService
{
    public (string Assunto, string Conteudo) GerarEmail(
        TipoNotificacao tipo, 
        Dictionary<string, string> dados)
    {
        return tipo switch
        {
            TipoNotificacao.Comentario => GerarEmailComentario(dados),
            TipoNotificacao.Curtida => GerarEmailCurtida(dados),
            TipoNotificacao.Seguidor => GerarEmailSeguidor(dados),
            _ => throw new ArgumentException($"Tipo de notificação não suportado: {tipo}")
        };
    }

    private static (string, string) GerarEmailComentario(Dictionary<string, string> dados)
    {
        var assunto = "Novo comentário na sua publicação";
        var nomeUsuario = dados.GetValueOrDefault("NomeUsuario", "Alguém");
        var comentario = dados.GetValueOrDefault("Comentario", "");
        var linkPublicacao = dados.GetValueOrDefault("LinkPublicacao", "#");

        var conteudo = $@"
            <html>
            <body style='font-family: Arial, sans-serif;'>
                <h2>Novo Comentário!</h2>
                <p><strong>{nomeUsuario}</strong> comentou na sua publicação:</p>
                <blockquote style='background-color: #f5f5f5; padding: 15px; border-left: 3px solid #007bff;'>
                    {comentario}
                </blockquote>
                <p><a href='{linkPublicacao}' style='background-color: #007bff; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Ver Publicação</a></p>
            </body>
            </html>";

        return (assunto, conteudo);
    }

    private static (string, string) GerarEmailCurtida(Dictionary<string, string> dados)
    {
        var assunto = "Nova curtida na sua publicação";
        var nomeUsuario = dados.GetValueOrDefault("NomeUsuario", "Alguém");
        var linkPublicacao = dados.GetValueOrDefault("LinkPublicacao", "#");

        var conteudo = $@"
            <html>
            <body style='font-family: Arial, sans-serif;'>
                <h2>❤️ Nova Curtida!</h2>
                <p><strong>{nomeUsuario}</strong> curtiu sua publicação.</p>
                <p><a href='{linkPublicacao}' style='background-color: #007bff; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Ver Publicação</a></p>
            </body>
            </html>";

        return (assunto, conteudo);
    }

    private static (string, string) GerarEmailSeguidor(Dictionary<string, string> dados)
    {
        var assunto = "Você tem um novo seguidor!";
        var nomeUsuario = dados.GetValueOrDefault("NomeUsuario", "Alguém");
        var linkPerfil = dados.GetValueOrDefault("LinkPerfil", "#");

        var conteudo = $@"
            <html>
            <body style='font-family: Arial, sans-serif;'>
                <h2>🎉 Novo Seguidor!</h2>
                <p><strong>{nomeUsuario}</strong> começou a seguir você.</p>
                <p><a href='{linkPerfil}' style='background-color: #007bff; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Ver Perfil</a></p>
            </body>
            </html>";

        return (assunto, conteudo);
    }
}