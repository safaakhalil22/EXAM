using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXAM
{
    public abstract class Exam
    {
        public TimeSpan Time { get; set; }
        public int NumberOfQuestions => Questions.Count;
        public List<Question> Questions { get; set; }
        protected readonly Stopwatch stopwatch = new Stopwatch();

        protected Exam(TimeSpan time, List<Question> questions)
        {
            Time = time;
            Questions = questions;
        }

        public abstract void ShowExam();

        public int TotalGrade() => Questions.Sum(q => q.Mark);

        public int StudentGrade() => Questions.Where(q => q.IsCorrect()).Sum(q => q.Mark);

        protected void PresentAllQuestions(string title)
        {
            Console.Clear();
            Console.WriteLine(title);
            int number = 1;
            stopwatch.Start();
            foreach (var q in Questions)
                q.Present(number++);
            stopwatch.Stop();
        }

        protected void ShowResults(string resultsTitle)
        {
            Console.Clear();
            Console.WriteLine(resultsTitle);
            int number = 1;
            foreach (var q in Questions)
            {
                Console.WriteLine($"Question {number++}: {q.Body}");
                string yourAnswer = q.SelectedAnswer != null ? q.SelectedAnswer.AnswerText : "(no answer)";
                Console.WriteLine($"Your Answer => {yourAnswer}");
                Console.WriteLine($"Correct Answer => {q.RightAnswer.AnswerText}");
                Console.WriteLine();
            }
            Console.WriteLine($"Your Grade is {StudentGrade()} from {TotalGrade()}");
            Console.WriteLine($"Time = {stopwatch.Elapsed}");
            Console.WriteLine("Thank you");
        }

        public override string ToString() =>
            $"{GetType().Name} - Time: {Time}, Questions: {NumberOfQuestions}";
    }
}
