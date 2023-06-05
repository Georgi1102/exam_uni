using Exam.Service;
using Exam.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Exam.Web.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService carService;

        public CarsController(ICarService carService)
        {
            this.carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> AllCars()
        {
            IEnumerable<CarServiceModel> allCars = null;

            string userId = this.User.FindFirst(ClaimTypes.Name).Value;

            if (this.User.IsInRole("Client"))
            {
                allCars = (await this.carService.GetAll());       
            }
            else
            {
                return View();
            }

            return View(allCars);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarServiceModel carServiceModel)
        {
            await this.carService.Create(carServiceModel);

            return Redirect("/");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.carService.Delete(id);

            return Redirect("/");
        }
    }
}
