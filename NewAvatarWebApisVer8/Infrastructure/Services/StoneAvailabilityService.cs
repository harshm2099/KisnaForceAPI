using System.Security;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

public class SoapResponse
{
    [JsonPropertyName("Data")]
    public List<StoneAvailability> Data { get; set; } = new List<StoneAvailability>();
}

public class StoneAvailability
{
    [JsonPropertyName("Status")]
    public string Status { get; set; } = string.Empty;
}

public interface ISoapService
{
    Task<bool> CheckStoneAvailabilityAsync(string packetNo);
    Task<bool> CheckStoneAvailabilityWithRetryAsync(string packetNo, int maxRetries = 2);
}

public class SoapService : ISoapService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<SoapService> _logger;

    public SoapService(HttpClient httpClient, ILogger<SoapService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<bool> CheckStoneAvailabilityAsync(string packetNo)
    {
        try
        {
            var soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" 
                              xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
                              xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                    <soap:Body>
                        <CheckStoneAvailability xmlns=""http://tempuri.org/"">
                            <CustomerID>b19f6709-deb6-4da6-8c74-6f2ea9e49f3f</CustomerID>
                            <PacketNo>{SecurityElement.Escape(packetNo)}</PacketNo>
                        </CheckStoneAvailability>
                    </soap:Body>
                </soap:Envelope>";

            var content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", "http://tempuri.org/IHKE.WCFService/CheckStoneAvailability");

            var response = await _httpClient.PostAsync("http://service.hkerp.co/HKE.WCFService.svc", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return ParseSoapResponse(responseContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SOAP call failed for packet: {PacketNo}", packetNo);
            return false;
        }
    }

    public async Task<bool> CheckStoneAvailabilityWithRetryAsync(string packetNo, int maxRetries = 2)
    {
        for (int retry = 0; retry <= maxRetries; retry++)
        {
            try
            {
                return await CheckStoneAvailabilityAsync(packetNo);
            }
            catch (Exception ex) when (retry < maxRetries)
            {
                _logger.LogWarning(ex, "SOAP call failed, retry {RetryCount} for packet: {PacketNo}", retry + 1, packetNo);
                await Task.Delay(TimeSpan.FromSeconds(1 * (retry + 1)));
            }
        }
        return false;
    }

    private bool ParseSoapResponse(string soapResponse)
    {
        try
        {
            var doc = new XmlDocument();
            doc.LoadXml(soapResponse);

            var nsManager = new XmlNamespaceManager(doc.NameTable);
            nsManager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            nsManager.AddNamespace("a", "http://schemas.datacontract.org/2004/07/HKE.WCFService");

            var resultNode = doc.SelectSingleNode("//soap:Envelope/soap:Body//CheckStoneAvailabilityResponse/CheckStoneAvailabilityResult", nsManager);

            if (resultNode != null)
            {
                var jsonResult = resultNode.InnerText;
                var result = JsonSerializer.Deserialize<SoapResponse>(jsonResult);
                return result?.Data?.FirstOrDefault()?.Status?.Equals("Available", StringComparison.OrdinalIgnoreCase) == true;
            }

            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to parse SOAP response");
            return false;
        }
    }
}