﻿using GradeBook.Enums;
using System;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted)
            : base(name, isWeighted)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }

            var studentsPer20Percent = (int)Math.Round(Students.Count / 5.0);
            var orderedGrades = Students.OrderByDescending(x => x.AverageGrade).Select(x => x.AverageGrade).ToList();
            char result;

            switch (averageGrade)
            {
                case var grade when grade >= orderedGrades[studentsPer20Percent - 1]:
                    result = 'A';
                    break;
                case var grade when grade >= orderedGrades[2 * studentsPer20Percent - 1]:
                    result = 'B';
                    break;
                case var grade when grade >= orderedGrades[3 * studentsPer20Percent - 1]:
                    result = 'C';
                    break;
                case var grade when grade >= orderedGrades[4 * studentsPer20Percent - 1]:
                    result = 'D';
                    break;
                default:
                    result = 'F';
                    break;
            }

            return result;
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStudentStatistics(name);
        }
    }
}