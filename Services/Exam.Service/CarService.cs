using Exam.Data;
using Exam.DataModels;
using Exam.Service.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service
{
    public class CarService : ICarService
    {
        private readonly ExamDbContext dbContext;

        public CarService(ExamDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<CarServiceModel> Create(CarServiceModel model)
        {
            Car car = new Car
            {
                Id = Guid.NewGuid().ToString(),
                Mark = model.Mark,
                Model = model.Model,
                YearOfProduction = model.YearOfProduction,
                Seats = model.Seats,
                Descritption = model.Descritption,
                Price = model.Price,
                PictureURL = model.PictureURL
            };

            car = (await this.dbContext.AddAsync(car)).Entity;
            await this.dbContext.SaveChangesAsync();

            return new CarServiceModel
            {
                Id = Guid.NewGuid().ToString(),
                Mark = car.Mark,
                Model = car.Model,
                YearOfProduction = car.YearOfProduction,
                Seats = car.Seats,
                Descritption = car.Descritption,
                Price = car.Price,
                PictureURL = car.PictureURL
            };
        }

        public async Task<CarServiceModel> Delete(string id)
        {
            Car car = await this.dbContext.Cars.SingleOrDefaultAsync(car => car.Id == id);

            this.dbContext.Remove(car);
            await this.dbContext.SaveChangesAsync();

            return new CarServiceModel {
                Id = car.Id,
                Mark = car.Mark,
                Model = car.Model,
                YearOfProduction = car.YearOfProduction,
                Seats = car.Seats,
                Descritption = car.Descritption,
                Price = car.Price,
                PictureURL = car.PictureURL
            };
        }

        public async Task<IEnumerable<CarServiceModel>> GetAll()
        {
            return await this.dbContext.Cars
                .Select(car => new CarServiceModel
                {
                    Id = car.Id,
                    Mark = car.Mark,
                    Model = car.Model,
                    YearOfProduction = car.YearOfProduction,
                    Seats = car.Seats,
                    Descritption = car.Descritption,
                    Price = car.Price,
                    PictureURL = car.PictureURL
                }).ToListAsync();
        }

        public async Task<CarServiceModel> GetById(string id)
        {

            Car car = await this.dbContext.Cars
                .SingleOrDefaultAsync(car => car.Id == id);

            return new CarServiceModel
            {
                Id = car.Id,
                Mark = car.Mark,
                Model = car.Model,
                YearOfProduction = car.YearOfProduction,
                Seats = car.Seats,
                Descritption = car.Descritption,
                Price = car.Price,
                PictureURL = car.PictureURL
            };
        }

        public async Task<CarServiceModel> Update(string id, CarServiceModel model)
        {
            Car car = await this.dbContext.FindAsync<Car>(id);

            car.Mark = model.Mark;
            car.Model = model.Model;       
            car.YearOfProduction = model.YearOfProduction;
            car.Seats = model.Seats;
            car.Descritption = model.Descritption;
            car.Descritption = model.Descritption;
            car.Price = model.Price;
            car.PictureURL = model.PictureURL;

            this.dbContext.Update(car);
            await this.dbContext.SaveChangesAsync();

            return new CarServiceModel
            {
                Id = car.Id,
                Mark = car.Mark,
                Model = car.Model,
                YearOfProduction = car.YearOfProduction,
                Seats = car.Seats,
                Descritption = car.Descritption,
                Price = car.Price,
                PictureURL = car.PictureURL
            };
        }
    }
}
