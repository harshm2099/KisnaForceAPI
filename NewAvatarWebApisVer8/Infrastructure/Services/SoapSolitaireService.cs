using NewAvatarWebApis.Core.Application.Responses;

public interface ISoapSolitaireService
{
    Task ProcessSolitaireAvailabilityAsync(CartItemResponse item);
}

public class SoapSolitaireService : ISoapSolitaireService
{
    private readonly ISoapService _soapService;
    private readonly ILogger<SoapSolitaireService> _logger;

    public SoapSolitaireService(ISoapService soapService, ILogger<SoapSolitaireService> logger)
    {
        _soapService = soapService;
        _logger = logger;
    }

    public async Task ProcessSolitaireAvailabilityAsync(CartItemResponse item)
    {
        if (item.ItemSoliter != "Y" && item.ItemIllumine != "Y")
        {
            item.IsSolitaireAvailable = "N/A";
            return;
        }

        if (string.IsNullOrEmpty(item.CartSoliStkNo))
        {
            item.IsSolitaireAvailable = "U";
            return;
        }

        var stockNumbers = item.CartSoliStkNo.Split(',', StringSplitOptions.RemoveEmptyEntries);
        var availableCount = 0;
        var notAvailableCount = 0;

        foreach (var stockNo in stockNumbers)
        {
            try
            {
                var isAvailable = await _soapService.CheckStoneAvailabilityWithRetryAsync(stockNo.Trim());

                if (isAvailable)
                    availableCount++;
                else
                    notAvailableCount++;

                _logger.LogInformation("Stock {StockNo} availability: {IsAvailable}", stockNo, isAvailable);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to check availability for stock: {StockNo}", stockNo);
                notAvailableCount++;
            }
        }

        if (notAvailableCount > 0 && availableCount == 0)
            item.IsSolitaireAvailable = "N";
        else if (notAvailableCount > 0 && availableCount > 0)
            item.IsSolitaireAvailable = "P";
        else
            item.IsSolitaireAvailable = "Y";

        _logger.LogInformation("Solitaire availability for item {CartAutoId}: {Availability} (Available: {Available}, Not Available: {NotAvailable})",
            item.CartAutoId, item.IsSolitaireAvailable, availableCount, notAvailableCount);
    }
}