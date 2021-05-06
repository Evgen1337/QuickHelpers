using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuickHelpers.DbTest;

namespace QuickHelpers.Controllers
{
    public class CarController : Controller
    {
        private readonly CarContext _carContext;

        public CarController(CarContext carContext)
        {
            _carContext = carContext;
        }

        public IActionResult Get()
        {
            var cars = _carContext.Cars.ToArray();
            return View(cars);
        }

        [HttpGet]
        public IActionResult AddCar()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult AddCar(Car car)
        {
            var addedCar = _carContext.Cars.Add(car);
            _carContext.SaveChangesAsync();
            
            return View();
        }
    }
}