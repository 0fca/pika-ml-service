namespace PikaMLModule.Models.Dto
{
    public class WordSentimentPredictionDto
    {
        public WordSentimentPredictionDto(bool prediction, float[] scores) 
        {
            this.Prediction = prediction;
            this.PositiveScore = scores[1];
            this.NegativeScore = scores[0];
        }

        public bool Prediction { get; set; }
        public float PositiveScore { get; set;}
        public float NegativeScore { get; set; }

        public float GetPositiveInPercent()
        {
            return this.PositiveScore * 100;
        }

        public float GetNegativeInPercent() 
        {
            return this.NegativeScore * 100;
        }
    }
}
