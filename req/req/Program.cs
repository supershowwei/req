using System.Net.Http.Headers;
using System.Text;

namespace req
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            if (args == null || args.Length < 2)
            {
                Console.WriteLine("參數不足");
                return;
            }

            var method = args[0];
            var url = args[1];

            var httpRequest = new HttpRequestMessage(HttpMethod.Parse(method), url);

            HttpContent httpContent = null;

            if (args.Length > 2)
            {
                var content = Encoding.UTF8.GetString(Convert.FromBase64String(args[2]));

                httpContent = new StringContent(content, Encoding.UTF8);
            }

            if (args.Length > 3)
            {
                var contentType = MediaTypeHeaderValue.Parse(args[3]);

                httpContent.Headers.ContentType = contentType;
            }

            if (httpContent != null)
            {
                httpRequest.Content = httpContent;
            }

            var httpClient = new HttpClient();

            var httpResponse = await httpClient.SendAsync(httpRequest);

            Console.WriteLine($"Status Code: {httpResponse.StatusCode}");

            var responseContent = await httpResponse.Content.ReadAsStringAsync();

            Console.WriteLine($"Response Content: \r\n```\r\n{responseContent}\r\n```");
            Console.WriteLine();
        }
    }
}