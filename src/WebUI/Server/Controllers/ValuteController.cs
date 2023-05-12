using Cegeka.Auction.Application.Bids.Queries;
using Cegeka.Auction.WebUI.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace Cegeka.Auction.WebUI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuteController : ApiControllerBase
    {

        private readonly IConfiguration _configuration;

        public ValuteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<decimal> ConvertCurrency(string from, string to, decimal amount)
        {
            string matrixString = _configuration["CurrencyMatrix"];
            string[] rows = matrixString.Split(';');
            int rowCount = rows.Length;
            decimal[,] matrix = new decimal[rowCount, rowCount];

            for (int i = 0; i < rowCount; i++)
            {
                string[] values = rows[i].Split(',');
                int valueCount = values.Length;

                if (valueCount != rowCount)
                {
                    throw new Exception("Invalid currency matrix.");
                }

                for (int j = 0; j < valueCount; j++)
                {
                    if (!decimal.TryParse(values[j], out decimal value))
                    {
                        throw new Exception("Invalid currency matrix.");
                    }

                    matrix[i, j] = value;
                }
            }

            string currencyPair = $"{from.ToUpper()}-{to.ToUpper()}";
            string[] pairIndices = _configuration.GetSection("CurrencyPairs")
                .GetValue<string>(currencyPair)
                .Split(',');
            int row = int.Parse(pairIndices[0]);
            int col = int.Parse(pairIndices[1]);
            decimal ratio = matrix[row, col];

            return amount * ratio;
        }
    }
}
