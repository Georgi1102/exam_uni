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
    public class RentCarService : IRentCarService
    {
        private readonly ExamDbContext dbContext;

        public RentCarService(ExamDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<RentCarServiceModel> Create(RentCarServiceModel model)
        {
            RentCar rentCar = new RentCar
            {
                Id = Guid.NewGuid().ToString(),
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                CarModel = model.CarModel,
                CarId = model.CarId,
                Price = model.Price,
                StatusId = model.StatusId
            };

            rentCar = (await this.dbContext.AddAsync(rentCar)).Entity;
            await this.dbContext.SaveChangesAsync();

            return new RentCarServiceModel
            {
                Id = rentCar.Id,
                StartDate = rentCar.StartDate,
                EndDate = rentCar.EndDate,
                CarModel = rentCar.CarModel,
                CarId = rentCar.CarId,
                Price = rentCar.Price,
                StatusId = rentCar.StatusId
            };
        }

        public async Task<RentCarServiceModel> Delete(string id)
        {
            RentCar rentCar = await this.dbContext.RentCars.SingleOrDefaultAsync(car => car.Id == id);

            this.dbContext.Remove(rentCar);
            await this.dbContext.SaveChangesAsync();

            return new RentCarServiceModel
            {
                Id = rentCar.Id,
                StartDate = rentCar.StartDate,
                EndDate = rentCar.EndDate,
                CarModel = rentCar.CarModel,
                CarId = rentCar.CarId,
                Price = rentCar.Price,
                StatusId = rentCar.StatusId
            };
        }

        public async Task<IEnumerable<RentCarServiceModel>> GetAll()
        {
            return this.dbContext.RentCars
                .Include(car => car.Car)
                .Include(car => car.Status)
                .Select(car => new RentCarServiceModel
                {
                    Id = car.Id,
                    StartDate = car.StartDate,
                    EndDate = car.EndDate,
                    CarModel = car.CarModel,
                    CarId = car.CarId,
                    Car = new CarServiceModel
                    {
                        Id = car.Car.Id,
                        Mark = car.Car.Mark,
                        Model = car.Car.Model,
                        YearOfProduction = car.Car.YearOfProduction,
                        Seats = car.Car.Seats,
                        Descritption = car.Car.Descritption,
                        Price = car.Car.Price,
                        PictureURL = car.Car.PictureURL
                    },
                    Price = car.Price,
                    StatusId = car.StatusId,
                    Status = new StatusServiceModel
                    {
                        Id = car.Status.Id,
                        Name = car.Status.Name
                    }
                });
        }

        public async Task<RentCarServiceModel> GetById(string id)
        {
           RentCar rentCar = await this.dbContext.RentCars
                .Include(car => car.Car)
                .Include(car => car.Status)
                .SingleOrDefaultAsync(car => car.Id == id);

            //Basicly the stupidest thing. Sorry Ivo or Galin or who read this

            return new RentCarServiceModel
            {
                Id = rentCar.Id,
                StartDate = rentCar.StartDate,
                EndDate = rentCar.EndDate,
                CarModel = rentCar.CarModel,
                CarId = rentCar.CarId,
                Car = new CarServiceModel
                {
                    Id = rentCar.Car.Id,
                    Mark = rentCar.Car.Mark,
                    Model = rentCar.Car.Model,
                    YearOfProduction = rentCar.Car.YearOfProduction,
                    Seats = rentCar.Car.Seats,
                    Descritption = rentCar.Car.Descritption,
                    Price = rentCar.Car.Price,
                    PictureURL = rentCar.Car.PictureURL
                },
                Price = rentCar.Price,
                StatusId = rentCar.StatusId,
                Status = new StatusServiceModel
                {
                    Id = rentCar.Status.Id,
                    Name = rentCar.Status.Name
                }
            };
        }

        public async Task<RentCarServiceModel> Update(string id, RentCarServiceModel model)
        {
            RentCar rentCar = await this.dbContext.FindAsync<RentCar>(id);

            rentCar.StartDate = model.StartDate;
            rentCar.EndDate = model.EndDate;
            rentCar.CarModel = model.CarId;
            rentCar.CarId = model.CarId;
            rentCar.Price = model.Price;
            rentCar.StatusId = model.StatusId;

            this.dbContext.Update(rentCar);
            await this.dbContext.SaveChangesAsync();

            return new RentCarServiceModel
            {
                Id = rentCar.Id,
                StartDate = rentCar.StartDate,
                EndDate = rentCar.EndDate,
                CarModel = rentCar.CarModel,
                CarId = rentCar.CarId,
                Price = rentCar.Price,
                StatusId = rentCar.StatusId
            };
        }
    }
}
