
using PikaMLModule.Models.Dto;
using System.Threading.Tasks;

namespace PikaMLModule.Services
{
    public interface IPredictionService
    {
        Task<WordSentimentPredictionDto> PredictSentiment(InputDto inputDto);
    }
}
