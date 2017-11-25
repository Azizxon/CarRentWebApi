using System;
using CarRent.Application;
using CarRent.Common;
using CarRentWebAPI.Filters;
using CarRentWebAPI.Models;
using CarRentWebAPI.Security;
using Microsoft.AspNetCore.Mvc;

namespace CarRentWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("client")]
    [ExceptionFilter]
    public class ClientController : Controller
    {
        
        public ClientController(IClientCarRental clientCarRental)
        {
            _clientCarRental = clientCarRental;
        }
     
        [HttpPost]
        [Route("cars")]
        public IActionResult RegistrClientRequest([FromBody]AddClientRequest request)
        {
            var id = _clientCarRental.RegisterClient(request.Name, 
                Credentials.FromRawData(request.Email,request.Password));
            return Ok(id);
        }
        [HttpPost]
        [Route("{clientId}/rents")]
        public IActionResult RentCar([FromRoute]int clientId, [FromBody]CarRentRequest rentRequest)
        {
            _clientCarRental.RentCar(clientId,
                new DatePeriod(
                    from:rentRequest.From,
                    to:rentRequest.To
                    ), rentRequest.CarId
                );
            return Ok();
        }

        [HttpGet]
        [Route("{clientId}/cars")]
        public IActionResult GetAvaialableCars(
            [FromRoute]int clientId,
            [FromQuery]DateTimeOffset from,
            [FromQuery]DateTimeOffset to)
        {
            var cars = _clientCarRental.ListAvailableCars(clientId, new DatePeriod(from, to));
            return Ok(cars);
        }

        private IClientCarRental _clientCarRental;
    }
}