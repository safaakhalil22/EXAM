namespace EXAM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int typeChoice = ReadIntInRange("Enter the type of exam (1 for Practical, 2 for Final):", 1, 2);
            ExamType examType = typeChoice == 1 ? ExamType.Practical : ExamType.Final;

            int minutes = ReadIntInRange("Please enter the time for the exam (30 to 180 minutes):", 30, 180);

            int numberOfQuestions = ReadIntInRange("Please enter the number of questions:", 1, 100);

            var questions = new List<Question>();
            for (int i = 1; i <= numberOfQuestions; i++)
                questions.Add(BuildQuestion(examType, i));

            var subject = new Subject(1, "C# Fundamentals");
            subject.CreateExam(examType, TimeSpan.FromMinutes(minutes), questions);

            Console.Clear();
            Console.WriteLine("Do You Want To Start Exam (Y | N)");
            string start = Console.ReadLine();
            if (start != null && start.Trim().ToUpper() == "Y")
                subject.ExamOfSubject.ShowExam();

            Console.ReadLine();
        }

        static Question BuildQuestion(ExamType examType, int number)
        {
            Console.Clear();
            bool isTrueFalse = false;
            if (examType == ExamType.Final)
                isTrueFalse = ReadIntInRange("Enter the question type (1 for True/False, 2 for MCQ):", 1, 2) == 1;

            string body = ReadNonEmptyLine("Please enter the question body:");
            int mark = ReadIntInRange("Please enter the question mark:", 1, 100);
            string header = $"Question {number}";

            if (isTrueFalse)
            {
                int correctId = ReadIntInRange("Please enter the ID of the correct answer (1-True, 2-False):", 1, 2);
                var right = new Answer(correctId, correctId == 1 ? "True" : "False");
                return new TrueFalseQuestion(header, body, mark, right);
            }

            Console.WriteLine("Choices of Question:");
            var answers = new List<Answer>();
            for (int c = 1; c <= 4; c++)
                answers.Add(new Answer(c, ReadNonEmptyLine($"Please enter choice number {c}:")));

            int rightId = ReadIntInRange("Please enter the ID of the correct answer (1 to 4):", 1, 4);
            var rightAnswer = answers.First(a => a.AnswerId == rightId);

            return new MCQQuestion(header, body, mark, answers, rightAnswer);
        }

        static int ReadIntInRange(string prompt, int min, int max)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
                    return value;
                Console.WriteLine($"Please enter a value between {min} and {max}.");
            }
        }

        static string ReadNonEmptyLine(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string line = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                    return line;
                Console.WriteLine("Input cannot be empty.");
            }
        }
    }
}
