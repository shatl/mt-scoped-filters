namespace WebApi.Consumers
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using MassTransit;
    using Microsoft.Extensions.Logging;

    public class AddInventoryFaultComsumer: IConsumer<Fault<AddInventory>>
    {
        private readonly ILogger<AddInventoryFaultComsumer> _logger;

        public AddInventoryFaultComsumer(ILogger<AddInventoryFaultComsumer> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Consume(ConsumeContext<Fault<AddInventory>> context)
        {
            _logger.LogError("Failed to add inventory {@Inventory}", context.Message.Message);
            return Task.CompletedTask;
        }
    }
}