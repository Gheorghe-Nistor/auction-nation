using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cegeka.Auction.Application.WeatherForecasts.Queries;
using Cegeka.Auction.WebUI.Shared.WeatherForecasts;

namespace Cegeka.Auction.WebUI.Server.Controllers;

[Authorize]
public class WeatherForecastController : ApiControllerBase
{
    [HttpGet]
    public async Task<IList<WeatherForecast>> Get()
    {
        return await Mediator.Send(new GetWeatherForecastsQuery());
    }
}
