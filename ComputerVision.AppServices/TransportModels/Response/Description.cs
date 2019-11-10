using System.Collections.Generic;

namespace ComputerVision.AppServices.TransportModels.Response
{
    public class Description
    {
        public List<string> Tags { get; set; }
        public List<Caption> Captions { get; set; }
    }
}