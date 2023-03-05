using PikaMLModule.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MLTest.HateSpeechPL;

namespace PikaMLModule.Services
{
    public class PredictionService : IPredictionService
    {
        public async Task<HateSpeechPredictionDto> PredictSentiment(InputDto inputDto)
        {

            return await Task.Factory.StartNew(() =>
            {
                var model = new ModelInput()
                {
                    Text = inputDto.Text,
                    Sarcasm_irony = SetIsIrony(inputDto).Irony
                };
                var prediction = Predict(model);
                return new HateSpeechPredictionDto(prediction.PredictedLabel, prediction.Score);
            });   
        }

        private static InputDto SetIsIrony(InputDto inputDto)
        {
            var keywords = new List<string>()
            {
                "XD",
                "2137",
                "JPII",
                "JP II",
                "dzban",
                "debyl",
                "hehe",
                "kek",
                "kekw",
                "lol"
            };
            inputDto.Irony = !string.IsNullOrEmpty(
                keywords.Find(k => inputDto.Text.ToLower().Contains(k.ToLower()))
            ) ? 1f : 0f;
            return inputDto;
        }
    }
}
