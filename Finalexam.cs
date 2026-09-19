using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXAM
{
    #region FinalExam
    public class FinalExam : Exam
    {
        public FinalExam(TimeSpan time, List<Question> questions) : base(time, questions)
        {
        }

        public override void ShowExam()
        {
            PresentAllQuestions("Final Exam");
            ShowResults("Final Exam Results:");
        }
    }
    #endregion
}
