using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;

using System.ComponentModel;

namespace BLL.Models
{
    public class EvaluationModel // DTO: Data Transfer Object
    {
        public Evaluation Record { get; set; }

        public string Title => Record.Title;

        public string Score => Record.Score.ToString("N1");

        [DisplayName("Date")]
        public string Date => Record.Date.ToString("MM/dd/yyyy");

        public string Description => Record.Description;
        
        public string User => Record.User?.UserName;


        [DisplayName("Evaluateds")]
        public string Evaluateds => string.Join("<br>", Record.EvaluatedEvaluations?.Select(ee => ee.Evaluated?.Name + " " + ee.Evaluated?.Surname));

        [DisplayName("Evaluated IDs")]
        public List<int> EvaluatedIds
        {
            get => Record.EvaluatedEvaluations?.Select(ee => ee.EvaluatedId).ToList();
            set => Record.EvaluatedEvaluations = value.Select(v => new EvaluatedEvaluation { EvaluatedId = v }).ToList();
        }
    }
}