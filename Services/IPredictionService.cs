
using PikaMLModule.Models.Dto;
using System.Threading.Tasks;

namespace PikaMLModule.Services
{
    public interface IPredictionService
    {
        Task<HateSpeechPredictionDto> PredictSentiment(InputDto inputDto);
    }
}
