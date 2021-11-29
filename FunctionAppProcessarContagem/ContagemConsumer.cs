using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using FunctionAppProcessarContagem.Contagem;

namespace FunctionAppProcessarContagem;

public class ContagemConsumer
{
    private const string EVENTHUB_NAME = "hub-contagem";
    private readonly ILogger _logger;

    public ContagemConsumer(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<ContagemConsumer>();
    }

    [Function(nameof(ContagemConsumer))]
    public void Run([EventHubTrigger("hub-contagem",
        ConsumerGroup = "contagem",
        Connection = "AzureEventHubsConnection")] ResultadoContador[] items)
    {
        foreach (var item in items)
        {
            _logger.LogInformation($"Valor recebido do contador: {item.ValorAtual}");
            _logger.LogInformation($"Event Hub: {EVENTHUB_NAME} | Mensagem recebida: {item.Mensagem}");
            _logger.LogInformation($"Dados: {JsonSerializer.Serialize(item)}");
        }
    }
}