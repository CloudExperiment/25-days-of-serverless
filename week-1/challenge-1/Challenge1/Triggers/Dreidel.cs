using System;
using System.Collections.Generic;
using Challenge1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Challenge1.Triggers
{
    public static class Dreidel
    {
        private static readonly Random Rng = new Random();

        private static readonly List<DreidelSide> Sides = new List<DreidelSide>
        {
            new DreidelSide
            {
                Symbol = 'נ',
                Name = "Nun",
                Instructions = "Do nothing."
            },
            new DreidelSide
            {
                Symbol = 'ג',
                Name = "Gimmel",
                Instructions = "Take everything in the pot."
            },
            new DreidelSide
            {
                Symbol = 'ה',
                Name = "Hay",
                Instructions = "Take half of the pot."
            },
            new DreidelSide
            {
                Symbol = 'ש',
                Name = "Shin",
                Instructions = "Add to the pot."
            }
        };

        [FunctionName(nameof(Dreidel) + "_" + nameof(Get))]
        public static IActionResult Get(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "dreidel")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var selectedNum = Rng.Next(0, 4);
            var side = Sides[selectedNum];

            return new OkObjectResult(side);
        }
    }
}
