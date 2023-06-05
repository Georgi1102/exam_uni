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
    public class StatusService : IStatusService
    {
        private readonly ExamDbContext dbContext;

        public StatusService(ExamDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<StatusServiceModel> Create(StatusServiceModel model)
        {
            Status status = new Status
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
            };

            status = (await this.dbContext.AddAsync(status)).Entity;
            await this.dbContext.SaveChangesAsync();

            return new StatusServiceModel 
            {
                Id = status.Id,
                Name = status.Name
            };
        }

        public async Task<StatusServiceModel> Delete(string id)
        {
            Status status = await this.dbContext.Statuses
                 .SingleOrDefaultAsync(status => status.Id == id);

            this.dbContext.Remove(status);
            await this.dbContext.SaveChangesAsync();

            return new StatusServiceModel
            {
                Id = status.Id,
                Name = status.Name
            };
        }

        public async Task<IEnumerable<StatusServiceModel>> GetAll()
        {
            return await this.dbContext.Statuses.Select(status => new StatusServiceModel
            {
                Id = status.Id,
                Name = status.Name,
            }).ToListAsync();
        }

        public async Task<StatusServiceModel> GetById(string id)
        {
           Status status = await this.dbContext.Statuses
                .SingleOrDefaultAsync(status => status.Id == id);

            return new StatusServiceModel
            {
                Id = status.Id,
                Name = status.Name
            };
        }

        public async Task<StatusServiceModel> Update(string id, StatusServiceModel model)
        {
            Status status = await this.dbContext.FindAsync<Status>(id);

            status.Name = model.Name;

            this.dbContext.Update(status);
            await this.dbContext.SaveChangesAsync();

            return new StatusServiceModel
            {
                Id = status.Id,
                Name = status.Name
            };
        }
    }
}
