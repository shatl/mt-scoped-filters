namespace WebApi.Consumers
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using Filters;
    using MassTransit;
    using Microsoft.Extensions.Logging;

    public class AddInventoryConsumer : IConsumer<AddInventory>
    {
        private readonly ILogger<AddInventory> _logger;
        private readonly ITokenProvider _tokenProvider;

        public AddInventoryConsumer(ITokenProvider tokenProvider, ILogger<AddInventory> logger)
        {
            _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.LogInformation("Create consumer");
        }

        /// <inheritdoc />
        public Task Consume(ConsumeContext<AddInventory> context)
        {
            var token = _tokenProvider.GetToken();
            if (token == null)
                _logger.LogWarning("The security token was not found");

            _logger.LogInformation("Consume: {Token}", token);

            if (context.Message.Sku == "123")
                throw new InvalidOperationException("Invalid SKU");

            return Task.CompletedTask;
        }
    }
}