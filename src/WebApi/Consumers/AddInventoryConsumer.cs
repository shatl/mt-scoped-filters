using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using WebApi.Contracts;

namespace WebApi.Consumers
{
    public class AddInventoryConsumer: IConsumer<AddInventory>
    {
        private readonly Token _token;
        private readonly ILogger<AddInventory> _logger;

        public AddInventoryConsumer(Token token, ILogger<AddInventory> logger)
        {
            _token = token ?? throw new ArgumentNullException(nameof(token));
            _logger = logger;
            _logger.LogInformation("Create consumer");
        }

        /// <inheritdoc />
        public Task Consume(ConsumeContext<AddInventory> context)
        {
            _logger.LogInformation("Consume");

            if (string.IsNullOrWhiteSpace(_token.Value))
                _logger.LogWarning("The security token was not found");

            if (context.Message.Sku == "123")
                throw new InvalidOperationException("Invalid SKU");

            return Task.CompletedTask;
        }
    }
}
