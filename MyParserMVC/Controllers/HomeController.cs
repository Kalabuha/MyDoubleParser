using Microsoft.AspNetCore.Mvc;
using Services;
using Models;

namespace MyParserMVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly ParserService _parser;

        public HomeController(ParserService parser)
        {
            _parser = parser;
        }

        public IActionResult Index(string? message, double? result, string? userInput)
        {
            var resultModel = new ResultModel
            {
                Input = userInput,
                Message = message,
                Value = result
            };

            return View(resultModel);
        }

        [HttpPost]
        public IActionResult TryParse(string? input)
        {
            var result = _parser.ParseToDouble(input);

            return RedirectToAction(nameof(Index), new { message = result.Message, result = result.Value, userInput = input } );
        }
    }
}