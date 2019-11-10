using System.Collections.Generic;

namespace ComputerVision.AppServices.TransportModels.Response
{
    public class ImageInfo
    {
        public List<Category> Categories { get; set; }
        public Description Description { get; set; }
        public Color Color { get; set; }
        public string RequestId { get; set; }
        public Metadata Metadata { get; set; }
    }
}