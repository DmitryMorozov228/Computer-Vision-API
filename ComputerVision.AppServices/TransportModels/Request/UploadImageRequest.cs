using System.IO;

namespace ComputerVision.AppServices.TransportModels.Request
{
    public class UploadImageRequest
    {
        public Stream Body { get; set; }
    }
}