using Exam.Service;
using Exam.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.Web.Controllers
{
    public class RentCarController : Controller
    {
        private readonly IRentCarService rentCarService;

        public RentCarController(IRentCarService rentCarService)
        {
            this.rentCarService = rentCarService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RentCarServiceModel rentCarServiceModel)
        {
            await this.rentCarService.Create(rentCarServiceModel);

            return Redirect("/");
        }
    }
}
