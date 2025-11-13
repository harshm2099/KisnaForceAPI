using NewAvatarWebApis.Core.Application.DTOs;
using Newtonsoft.Json;
using System.Text;
using System.Xml;
using static System.Net.WebRequestMethods;

namespace NewAvatarWebApis.Core.Application.Common
{
    public class SoapHelper
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<string> CheckStoneAvailabilityAsync(string packetNo)
        {
            const string soapEnvelope = @"
            <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
               <soapenv:Header/>
               <soapenv:Body>
                  <tem:CheckStoneAvailability>
                     <tem:request>
                        <tem:CustomerID>b19f6709-deb6-4da6-8c74-6f2ea9e49f3f</tem:CustomerID>
                        <tem:PacketNo>{0}</tem:PacketNo>
                     </tem:request>
                  </tem:CheckStoneAvailability>
               </soapenv:Body>
            </soapenv:Envelope>";

            var content = new StringContent(
                string.Format(soapEnvelope, packetNo),
                Encoding.UTF8,
                "text/xml");

            content.Headers.Add("SOAPAction", "\"http://tempuri.org/IHKEWCFService/CheckStoneAvailability\"");

            var response = await _httpClient.PostAsync(
                "http://service.hkerp.co/HKE.WCFService.svc",
                content);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public static StoneResponse? ParseSoapResponse(string soapXml)
        {
            if (string.IsNullOrWhiteSpace(soapXml))
                return null;

            var doc = new XmlDocument();
            doc.LoadXml(soapXml);

            var nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("tem", "http://tempuri.org/");

            var resultNode = doc.SelectSingleNode("//tem:CheckStoneAvailabilityResult", nsmgr);
            if (resultNode == null || string.IsNullOrWhiteSpace(resultNode.InnerText))
                return null;

            var jsonString = resultNode.InnerText;
            return JsonConvert.DeserializeObject<StoneResponse>(jsonString);
        }

        public async Task<string?> CallStockApproveAsync(string pktCsv, string remark)
        {
            if (string.IsNullOrWhiteSpace(pktCsv)) return null;

            var url = $"https://service.hk.co/StockApprove?user=B19F6709-DEB6-4DA6-8C74-6F2EA9E49F3F&pkts={pktCsv}&remark={remark}";
            var resp = await _httpClient.GetAsync(url);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadAsStringAsync();
        }
    }
}
