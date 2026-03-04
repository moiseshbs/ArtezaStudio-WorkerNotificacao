using ArtezaStudio.WorkerNotificacao.Application.Interfaces;
using ArtezaStudio.WorkerNotificacao.Application.Services;
using ArtezaStudio.WorkerNotificacao.Application.UseCases;
using ArtezaStudio.WorkerNotificacao.Configuration;
using ArtezaStudio.WorkerNotificacao.Domain.Interfaces;
using ArtezaStudio.WorkerNotificacao.Infrastructure.Data;
using ArtezaStudio.WorkerNotificacao.Infrastructure.Repositories;
using ArtezaStudio.WorkerNotificacao.Infrastructure.Services;
using ArtezaStudio.WorkerNotificacao.Worker;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySqlConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySqlConnection"))
));

// Configuration
builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("Kafka"));

// Repositories
builder.Services.AddScoped<INotificacaoRepository, NotificacaoRepository>();

// Services
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ITemplateEmailService, TemplateEmailService>();

// Use Cases
builder.Services.AddScoped<IProcessarNotificacaoUseCase, ProcessarNotificacaoUseCase>();

// Worker
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
await host.RunAsync();