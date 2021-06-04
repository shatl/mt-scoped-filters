namespace WebApi.Contracts
{
    public record AddInventory
    {
        public string Sku { get; init; }
        public int Quantity { get; init; }
    }
}