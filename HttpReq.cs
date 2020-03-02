using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.IO;

namespace GetHoliday
{
    public class HttpReq : IDisposable
    {
        HttpClient httpClient;
        
        public HttpReq()
        {
            httpClient = new HttpClient();
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }

        public async Task<string> GetContentString(string contentUrl)
        {
            var h = await httpClient.GetAsync(contentUrl);

            h.EnsureSuccessStatusCode();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var enc = Encoding.GetEncoding("shift_jis");

            using (var st = await h.Content.ReadAsStreamAsync())
            using (var rd = new StreamReader(st, enc, true) as TextReader)
            {
                return await rd.ReadToEndAsync();
            }
        }
        
        
    }
    
}