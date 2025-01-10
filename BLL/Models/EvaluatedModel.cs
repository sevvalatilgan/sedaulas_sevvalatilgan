using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;

using System.ComponentModel;


namespace BLL.Models
{
    public class EvaluatedModel 
    {
        public Evaluated Record { get; set; }

        public string Name => Record.Name;

        public string Surname => Record.Surname;

        [DisplayName("Full Name")]
        public string FullName => Record.Name + " " + Record.Surname;

        [DisplayName("Evaluations")]
        public string Evaluations => string.Join("<br>", Record.EvaluatedEvaluations?.Select(ee => ee.Evaluation?.Title));

        [DisplayName("Evaluation IDs")]
        public List<int> EvaluationIds
        {
            get => Record.EvaluatedEvaluations?.Select(ee => ee.EvaluationId).ToList();
            set => Record.EvaluatedEvaluations = value.Select(v => new EvaluatedEvaluation { EvaluationId = v }).ToList();
        }
    }
}
