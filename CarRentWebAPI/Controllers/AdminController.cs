using CarRent.Application;
using CarRentWebAPI.Filters;
using CarRentWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRentWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("admin")]
    [ExceptionFilter]
    public class AdminController : Controller
    {
      
        public AdminController(IAdministratorCarRental  administratorCarRental)
        {
            _administratorCarRental = administratorCarRental;
        }
        [HttpGet]
        [Route("cars")]
        public IActionResult ShowAllCars()
        {
            var allCars = _administratorCarRental.ListAllCars();
            return Ok(allCars);
        }
        [HttpPut]
        [Route("cars")]
        public IActionResult AddCar([FromBody]AddCarRequest request)
        {
            _administratorCarRental.AddNewCar(request.Model,request.Color);
            return Ok();
        }

        private IAdministratorCarRental _administratorCarRental;

    }
}