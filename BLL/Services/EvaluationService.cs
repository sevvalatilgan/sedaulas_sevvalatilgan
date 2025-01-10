using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class EvaluationService : Service, IService<Evaluation, EvaluationModel>
    {
        public EvaluationService(Db db) : base(db)
        {
        }

        public Service Create(Evaluation record)
        {
            record.Title = record.Title.Trim();
            record.Description = record.Description?.Trim();
            _db.Evaluations.Add(record);
            _db.SaveChanges();
            return Success("Evaluation record created successfully.");
        }

        public Service Delete(int id)
        {
            var entity = _db.Evaluations.Include(e => e.EvaluatedEvaluations).SingleOrDefault(e => e.Id == id);
            if (entity is null)
                return Error("Evaluation record not found!");

            _db.EvaluatedEvaluations.RemoveRange(entity.EvaluatedEvaluations);
            _db.Evaluations.Remove(entity);
            _db.SaveChanges();
            return Success("Evaluation record deleted successfully.");
        }

        public IQueryable<EvaluationModel> Query()
        {
            return _db.Evaluations
                .Include(e => e.EvaluatedEvaluations)
                .ThenInclude(ee => ee.Evaluated)
                .Select(e => new EvaluationModel
                {
                    Record = e
                });
        }

        public Service Update(Evaluation record)
        {
            var entity = _db.Evaluations.Include(e => e.EvaluatedEvaluations).SingleOrDefault(e => e.Id == record.Id);
            if (entity is null)
                return Error("Evaluation record not found!");

            _db.EvaluatedEvaluations.RemoveRange(entity.EvaluatedEvaluations);

            entity.Title = record.Title.Trim();
            entity.Description = record.Description?.Trim();
            entity.Score = record.Score;
            entity.Date = record.Date;
            entity.UserId = record.UserId;
            entity.EvaluatedEvaluations = record.EvaluatedEvaluations;

            _db.Evaluations.Update(entity);
            _db.SaveChanges();
            return Success("Evaluation record updated successfully.");
        }
    }
}
    
        