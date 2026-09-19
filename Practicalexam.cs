using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXAM
{
    public class PracticalExam : Exam
    {
        public PracticalExam(TimeSpan time, List<Question> questions) : base(time, questions)
        {
        }

        public override void ShowExam()
        {
            PresentAllQuestions("Practical Exam");
            ShowResults("Practical Exam Results:");
        }
    }
}
