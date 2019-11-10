using System.Threading.Tasks;
using ComputerVision.AppServices.TransportModels.Request;
using ComputerVision.AppServices.TransportModels.Response;

namespace ComputerVision.AppServices
{
    public interface IVisionService
    {
        Task<ImageInfo> MakeAnalysisRequestAsync(UploadImageRequest model);
    }
}