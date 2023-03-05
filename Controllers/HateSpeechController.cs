using Microsoft.AspNetCore.Mvc;
using PikaMLModule.Models.Dto;
using System.Threading.Tasks;
using PikaMLModule.Services;
using System.Net.Mime;
using System;

namespace PikaMLModule.Controllers
{
    [ApiController]
    [Route("/api/hs/[action]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class HateSpeechController : Controller
    {
        private readonly IPredictionService _predictionService;
        public HateSpeechController(IPredictionService predictionService) 
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
    }
}
