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
    public class EvaluatedService : Service, IService<Evaluated, EvaluatedModel>
    {
        public EvaluatedService(Db db) : base(db)
        {
        }

        public Service Create(Evaluated record)
        {
            if (_db.Evaluateds.Any(e => e.Name.ToLower() == record.Name.ToLower().Trim() &&
                                       e.Surname.ToLower() == record.Surname.ToLower().Trim()))
                return Error("An Evaluated record with the same name and surname already exists!");

            record.Name = record.Name.Trim();
            record.Surname = record.Surname.Trim();
            _db.Evaluateds.Add(record);
            _db.SaveChanges();
            return Success("Evaluated record created successfully.");
        }

       

        public IQueryable<EvaluatedModel> Query()
        {
            return _db.Evaluateds
               .Include(e => e.EvaluatedEvaluations)
               .ThenInclude(ee => ee.Evaluation)
               .Select(e => new EvaluatedModel() { Record = e });
              
        }

        public Service Delete(int id)
        {
            throw new NotImplementedException();
        }
        public Service Update(Evaluated record)
        {
            throw new NotImplementedException();
        }
    }
}
