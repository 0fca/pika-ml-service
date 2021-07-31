﻿using Microsoft.AspNetCore.Mvc;
using PikaMLModule.Models.Dto;
using System.Threading.Tasks;
using PikaMLModule.Services;
using System.Net.Mime;
using System;

namespace PikaMLModule.Controllers
{
    [ApiController]
    [Route("/api/sentiment/text/[action]")]
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
    }
}
