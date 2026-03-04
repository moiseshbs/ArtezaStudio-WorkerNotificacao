namespace ArtezaStudio.WorkerNotificacao.Configuration;

public class KafkaSettings
{
    /// <summary>
    /// Endereço do servidor Kafka.
    /// </summary>
    public string BootstrapServers { get; set; } = "localhost:9092";

    /// <summary>
    /// Identificador do grupo de consumidores para garantir que as mensagens sejam processadas por apenas um consumidor dentro do grupo.
    /// </summary>
    public string GroupId { get; set; } = "worker-notificacao-group";

    /// <summary>
    /// Nome do tópico Kafka onde as mensagens de notificações serão publicadas e consumidas.
    /// </summary>
    public string TopicNotificacoes { get; set; } = "notificacoes";
    
    /// <summary>
    /// Configuração para definir o comportamento de leitura de mensagens quando não há um offset inicial ou quando o offset atual não está mais disponível.
    /// </summary>
    public bool AutoOffsetReset { get; set; } = true;
}