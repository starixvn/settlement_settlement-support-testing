public class MarketResponse
{
    public bool IsSuccess { get; set; }

    public string Message { get; set; }

    public IEnumerable<Market> Data { get; set; }
}