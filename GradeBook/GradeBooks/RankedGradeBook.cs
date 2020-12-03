using System;
using System.Linq;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted) : base(name, isWeighted)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if(Students == null || Students.Count < 5)
                throw new InvalidOperationException();

            var grades = Students.OrderByDescending(e => e.AverageGrade).Select(e => e.AverageGrade).ToList();
            var aGrades = grades.Take(grades.Count * 20 / 100);
            var bGrades = grades.Take(grades.Count * 40 / 100);
            var CGrades = grades.Take(grades.Count * 60 / 100);
            var DGrades = grades.Take(grades.Count * 80 / 100);

            if (aGrades.Any(x => averageGrade >= x))
                return 'A';
            if (bGrades.Any(x => averageGrade >= x))
                return 'B';
            if (CGrades.Any(x => averageGrade >= x))
                return 'C';
            if (DGrades.Any(x => averageGrade >= x))
                return 'D';

            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students == null || Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStatistics();
        }


        public override void CalculateStudentStatistics(string name)
        {
            if (Students == null || Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStudentStatistics(name);
        }
    }
}
