using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ComputerVision.AppServices.Configuration;
using ComputerVision.AppServices.TransportModels.Request;
using ComputerVision.AppServices.TransportModels.Response;
using Newtonsoft.Json;

namespace ComputerVision.AppServices
{
    public class VisionService : IVisionService
    {
        private readonly ComputerVisionConfiguration _computerVisionConfiguration;
        private readonly string[] _requestParams = { "Categories", "Description", "Color" };

        public VisionService(ComputerVisionConfiguration computerVisionConfiguration)
        {
            _computerVisionConfiguration = computerVisionConfiguration;
        }

        public async Task<ImageInfo> MakeAnalysisRequestAsync(UploadImageRequest model)
        {
            var imageInfo = new ImageInfo();
            try
            {
                HttpClient client = new HttpClient();
                // Request headers.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _computerVisionConfiguration.SubscriptionKey);
                // Assemble the URI for the REST API Call.
                var uri = string.Format("{0}?visualFeatures={1}", _computerVisionConfiguration.ApiUrl, string.Join(",", _requestParams));

                HttpResponseMessage response;

                using (var stream = new MemoryStream())
                {
                    await model.Body.CopyToAsync(stream);
                    var fileContent = stream.ToArray();
                    using (ByteArrayContent content = new ByteArrayContent(fileContent))
                    {
                        // This example uses content type "application/octet-stream".
                        // The other content types you can use are "application/json"
                        // and "multipart/form-data".
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                        // Make the REST API call.
                        response = await client.PostAsync(uri, content);
                    }
                }
                
                if (response.IsSuccessStatusCode)
                {
                    imageInfo = JsonConvert.DeserializeObject<ImageInfo>(await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return imageInfo;
        }
    }
}
