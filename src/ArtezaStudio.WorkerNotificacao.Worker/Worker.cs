using System.Text.Json;
using ArtezaStudio.WorkerNotificacao.Application.DTOs;
using ArtezaStudio.WorkerNotificacao.Application.Interfaces;
using ArtezaStudio.WorkerNotificacao.Configuration;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace ArtezaStudio.WorkerNotificacao.Worker;

public class Worker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<Worker> _logger;
    private readonly IConsumer<string, string> _consumer;
    private readonly KafkaSettings _kafkaSettings;

    public Worker(
        IServiceProvider serviceProvider,
        ILogger<Worker> logger,
        IOptions<KafkaSettings> kafkaSettings)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _kafkaSettings = kafkaSettings.Value;

        var config = new ConsumerConfig
        {
            BootstrapServers = _kafkaSettings.BootstrapServers,
            GroupId = _kafkaSettings.GroupId,
            AutoOffsetReset = _kafkaSettings.AutoOffsetReset ? AutoOffsetReset.Earliest : AutoOffsetReset.Latest,
            EnableAutoCommit = false
        };

        _consumer = new ConsumerBuilder<string, string>(config).Build();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe(_kafkaSettings.TopicNotificacoes);

        _logger.LogInformation("Conectado ao Kafka. Aguardando mensagens do tópico: {Topic}", _kafkaSettings.TopicNotificacoes);


        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var consumeResult = _consumer.Consume(stoppingToken);

                using var scope = _serviceProvider.CreateScope();
                var useCase = scope.ServiceProvider.GetRequiredService<IProcessarNotificacaoUseCase>();

                var notificacaoMessageDto = JsonSerializer.Deserialize<NotificacaoMessageDto>(consumeResult.Message.Value);

                if (notificacaoMessageDto != null)
                {
                    await useCase.ExecutarAsync(notificacaoMessageDto);
                    _consumer.Commit(consumeResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar mensagem do Kafka");
            }
        }
    }

    public override void Dispose()
    {
        try
        {
            _consumer?.Close();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao fechar consumer do Kafka");
        }
        finally
        {
            _consumer?.Dispose();
            base.Dispose();
        }
    }
}
