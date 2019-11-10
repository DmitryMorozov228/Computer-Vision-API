using System.Collections.Generic;

namespace ComputerVision.AppServices.TransportModels.Response
{
    public class Color
    {
        public string DominantColorForeground { get; set; }
        public string DominantColorBackground { get; set; }
        public List<string> DominantColors { get; set; }
        public string AccentColor { get; set; }
        public bool IsBwImg { get; set; }
    }
}