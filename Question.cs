using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXAM
{
    #region Question
    public abstract class Question : ICloneable, IComparable<Question>
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public int Mark { get; set; }
        public List<Answer> AnswerList { get; set; }
        public Answer RightAnswer { get; set; }
        public Answer SelectedAnswer { get; set; }

        public abstract string TypeLabel { get; }

        protected Question(string header, string body, int mark, List<Answer> answerList, Answer rightAnswer)
        {
            Header = header;
            Body = body;
            Mark = mark;
            AnswerList = answerList;
            RightAnswer = rightAnswer;
        }

        public void Present(int number)
        {
            Console.WriteLine($"Question {number}: {Body}");
            Console.WriteLine($"{TypeLabel} Question:   Mark {Mark}");
            foreach (var a in AnswerList)
                Console.WriteLine($"{a.AnswerId}- {a.AnswerText}");

            Console.WriteLine("Enter your answer ID:");
            if (int.TryParse(Console.ReadLine(), out int id))
                SelectedAnswer = AnswerList.FirstOrDefault(a => a.AnswerId == id);
        }

        public bool IsCorrect() => SelectedAnswer != null && RightAnswer != null
            && SelectedAnswer.AnswerId == RightAnswer.AnswerId;

        public virtual object Clone()
        {
            Question copy = (Question)MemberwiseClone();
            copy.AnswerList = AnswerList.Select(a => (Answer)a.Clone()).ToList();
            copy.RightAnswer = (Answer)RightAnswer.Clone();
            copy.SelectedAnswer = null;
            return copy;
        }

        public int CompareTo(Question other) => other == null ? 1 : Mark.CompareTo(other.Mark);

        public override string ToString() =>
            $"[{TypeLabel}] {Header} ({Mark} mark(s))\n{Body}\n" +
            string.Join("\n", AnswerList.Select(a => a.ToString()));
    }
    #endregion
}
