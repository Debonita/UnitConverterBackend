using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using UnitConverter.Models;
using UnitConverter.Service;
using System;

namespace UnitConverter.Functionapp
{
    public class UnitConverterFunction
    {
        private readonly LengthUnitConverterService _lengthUnitConverterService;
        private readonly MassUnitConverterService _massUnitConverterService;
        private readonly TimeUnitConverterService _timeUnitConverterService;

        public UnitConverterFunction()
        {
            _lengthUnitConverterService = new LengthUnitConverterService();
            _massUnitConverterService = new MassUnitConverterService();
            _timeUnitConverterService= new TimeUnitConverterService();
        }

        [FunctionName("Convert")]
        public async Task<IActionResult> Length_Convert(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "convert")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Processing a length conversion request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var unitRequest = JsonConvert.DeserializeObject<Model>(requestBody);
            string fromVal=unitRequest.FromUnit;
            if(fromVal=="feet"||fromVal=="meter"||fromVal=="inch"||fromVal=="kilometer")
            {
                if (Enum.TryParse(unitRequest.FromUnit, true, out UnitTypeLength fromUnit) &&
                Enum.TryParse(unitRequest.ToUnit, true, out UnitTypeLength toUnit))
                {
                    double result = _lengthUnitConverterService.Convert((int)fromUnit, (int)toUnit, unitRequest.SourceValue);
                    Console.WriteLine(result);
                    return new OkObjectResult(result);
                }
                else
                {
                    return new BadRequestObjectResult("Invalid Input");
                }
            }
            else if (fromVal == "pound" || fromVal == "ounce" || fromVal == "gram" || fromVal == "kilogram")
            {
                if (Enum.TryParse(unitRequest.FromUnit, true, out UnitTypeMass fromUnit) &&
                Enum.TryParse(unitRequest.ToUnit, true, out UnitTypeMass toUnit))
                {
                    double result = _massUnitConverterService.Convert((int)fromUnit, (int)toUnit, unitRequest.SourceValue);
                    Console.WriteLine(result);
                    return new OkObjectResult(result);
                }
                else
                {
                    return new BadRequestObjectResult("Invalid Input");
                }
            }
            else if (fromVal == "second" || fromVal == "milisecond")
            {
                if (Enum.TryParse(unitRequest.FromUnit, true, out UnitTypeTime fromUnit) &&
                Enum.TryParse(unitRequest.ToUnit, true, out UnitTypeTime toUnit))
                {
                    double result = _timeUnitConverterService.Convert((int)fromUnit, (int)toUnit, unitRequest.SourceValue);
                    Console.WriteLine(result);
                    return new OkObjectResult(result);
                }
                else
                {
                    return new BadRequestObjectResult("Invalid Input");
                }
            }
            else
            {
                return new BadRequestObjectResult("Invalid Input");
            }

            //Testing Pipeline
        }
    }
}
