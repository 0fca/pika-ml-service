using Microsoft.AspNetCore.Mvc;
using PikaMLModule.Models.Dto;
using System.Threading.Tasks;
using PikaMLModule.Services;
using System.Net.Mime;
using System;

namespace PikaMLModule.Controllers
{
    [ApiController]
    [Route("/api/[action]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class WordSentimentController : Controller
    {
        private readonly IPredictionService _predictionService;
        public WordSentimentController(IPredictionService predictionService) 
        {
            this._predictionService = predictionService;
        }

        [HttpPost]
        [ActionName("predict")]
        public async Task<IActionResult> Predict([FromBody] InputDto predictDto) 
        {
            var prediction = await this._predictionService.PredictSentiment(predictDto);
            return Ok(prediction);
        }

        // To Be Deleted
        [HttpGet]
        [ActionName("validate")]
        public IActionResult ValidateCsv() 
        {
            var f = System.IO.File.ReadAllLines("C:\\Users\\lukas\\Desktop\\dictionary_semi.csv");
            foreach (var line in f) {
                var dataRow = line.Split(";");
                try
                {
                    Double.Parse(dataRow[1]);
                }
                catch (FormatException e)
                {
                    return BadRequest(e.Message);
                }
            }

            return Ok();
        }
    }
}
