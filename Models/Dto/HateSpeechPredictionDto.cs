namespace PikaMLModule.Models.Dto
{
    public class HateSpeechPredictionDto
    {
        public HateSpeechPredictionDto(float prediction, float[] scores) 
        {
            this.Prediction = prediction;
            this.Neutral = scores[0];
            this.Negative = scores[1];
            this.Offensive = scores[2];
            this.Abusive = scores[4];
            this.Hatred = scores[3];
        }

        public float Prediction { get; set; }
        public float Neutral { get; set;}
        public float Negative { get; set; }

        public float Offensive { get; set; }
        
        public float Abusive { get; set; }
        public float Hatred { get; set; }
    }
}
