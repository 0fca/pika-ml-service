using PikaMLModule.Models.Dto;
using System.Threading.Tasks;
using static PikaMLModule.WordSentimentModel;

namespace PikaMLModule.Services
{
    public class PredictionService : IPredictionService
    {
        public async Task<WordSentimentPredictionDto> PredictSentiment(InputDto inputDto)
        {
            return await Task.Factory.StartNew(() =>
            {
                var model = new ModelInput()
                {
                    Col0 = inputDto.Text
                };
                var prediction = Predict(model);
                return new WordSentimentPredictionDto(prediction.Prediction != 0, prediction.Score);
            });   
        }
    }
}
