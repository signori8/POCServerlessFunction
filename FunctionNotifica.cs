using System;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FunctionNotificaParceiro
{
    public class FunctionNotifica
    {
        
        [FunctionName("FunctionNotifica")]
        public static void Run(
            [ServiceBusTrigger("propostas",AutoCompleteMessages = true, Connection = "conn")]
            string myQueueItem,
            Int32 deliveryCount,
            DateTime enqueuedTimeUtc,
            string messageId,
            ILogger log)
        {
            log.LogInformation($"Processando mensagem: {myQueueItem}");           

            HttpClient client = new HttpClient();

            try
            {

                string endpoint = Environment.GetEnvironmentVariable("endpoint")+myQueueItem;
                var data = new System.Net.Http.StringContent(myQueueItem);
                var response = client.PutAsync(endpoint, data);

                log.LogInformation($"Resposta={response.Result}");
            }
            finally
            {
                client.Dispose();
            }

        }
        
    }
}
