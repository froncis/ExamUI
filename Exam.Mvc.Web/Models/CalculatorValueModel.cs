using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.Mvc.Web.Models
{
    public class CalculatorValueModel
    {
        public decimal PresentValue { get; set; }
        public decimal FutureValue { get; set; }
        public int Year { get; set; }
        public float LowerBoundInterestRate { get; set; }
    }
}
