using System;
using System.Collections.Generic;
using System.Linq;

public class Student
{
    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public int Age { get; set; }
    public string Major { get; set; }
    public double Tuition { get; set; }
}

public class StudentClubs
{
    public int StudentID { get; set; }
    public string ClubName { get; set; }
}

public class StudentGPA
{
    public int StudentID { get; set; }
    public double GPA { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        IList<Student> studentList = new List<Student>()
        {
            new Student() { StudentID = 1, StudentName = "Frank Furter", Age = 55, Major = "Hospitality", Tuition = 3500.00 },
            new Student() { StudentID = 2, StudentName = "Gina Host", Age = 21, Major = "Hospitality", Tuition = 4500.00 },
            new Student() { StudentID = 3, StudentName = "Cookie Crumb", Age = 21, Major = "CIT", Tuition = 2500.00 },
            new Student() { StudentID = 4, StudentName = "Ima Script", Age = 48, Major = "CIT", Tuition = 5500.00 },
            new Student() { StudentID = 5, StudentName = "Cora Coder", Age = 35, Major = "CIT", Tuition = 1500.00 },
            new Student() { StudentID = 6, StudentName = "Ura Goodchild", Age = 40, Major = "Marketing", Tuition = 500.00 },
            new Student() { StudentID = 7, StudentName = "Take Mewith", Age = 29, Major = "Aerospace Engineering", Tuition = 5500.00 }
        };

        IList<StudentGPA> studentGPAList = new List<StudentGPA>()
        {
            new StudentGPA() { StudentID = 1, GPA = 4.0 },
            new StudentGPA() { StudentID = 2, GPA = 3.5 },
            new StudentGPA() { StudentID = 3, GPA = 2.0 },
            new StudentGPA() { StudentID = 4, GPA = 1.5 },
            new StudentGPA() { StudentID = 5, GPA = 4.0 },
            new StudentGPA() { StudentID = 6, GPA = 2.5 },
            new StudentGPA() { StudentID = 7, GPA = 1.0 }
        };

        IList<StudentClubs> studentClubList = new List<StudentClubs>()
        {
            new StudentClubs() { StudentID = 1, ClubName = "Photography" },
            new StudentClubs() { StudentID = 1, ClubName = "Game" },
            new StudentClubs() { StudentID = 2, ClubName = "Game" },
            new StudentClubs() { StudentID = 5, ClubName = "Photography" },
            new StudentClubs() { StudentID = 6, ClubName = "Game" },
            new StudentClubs() { StudentID = 7, ClubName = "Photography" },
            new StudentClubs() { StudentID = 3, ClubName = "PTK" }
        };

        // Group by GPA and display the student's IDs
        var query1 = studentGPAList.GroupBy(g => g.GPA).Select(s => new { GPA = s.Key, StudentIDs = string.Join(", ", s.Select(x => x.StudentID)) });
        Console.WriteLine("Group by GPA and display the student's IDs:");
        foreach (var item in query1)
        {
            Console.WriteLine($"GPA: {item.GPA}, StudentIDs: {item.StudentIDs}");
        }
        Console.WriteLine();

        // Sort by Club, then group by Club and display the student's IDs
        var query2 = studentClubList.OrderBy(o => o.ClubName).GroupBy(g => g.ClubName).Select(s => new { Club = s.Key, StudentIDs = string.Join(", ", s.Select(x => x.StudentID)) });
        Console.WriteLine("Sort by Club, then group by Club and display the student's IDs:");
        foreach (var item in query2)
        {
            Console.WriteLine($"Club: {item.Club}, StudentIDs: {item.StudentIDs}");
        }
        Console.WriteLine();

        // Count the number of students with a GPA between 2.5 and 4.0
        var query3 = studentGPAList.Where(w => w.GPA >= 2.5 && w.GPA <= 4.0).Count();
        Console.WriteLine($"Number of students with a GPA between 2.5 and 4.0: {query3}");
        Console.WriteLine();

        // Average all student's tuition
        var query4 = studentList.Average(a => a.Tuition);
        Console.WriteLine($"Average all student's tuition: {query4}");
        Console.WriteLine();

        // Find the student paying the most tuition and display their name, major and tuition
        var highestTuition = studentList.Max(m => m.Tuition);
        var studentWithHighestTuition = studentList.First(f => f.Tuition == highestTuition);
        Console.WriteLine($"Student paying the most tuition: {studentWithHighestTuition.StudentName}, Major: {studentWithHighestTuition.Major}, Tuition: {studentWithHighestTuition.Tuition}");
        Console.WriteLine();

        // Join the student list and student GPA list on student ID and display the student's name, major and gpa
        var query6 = from s in studentList
                     join g in studentGPAList on s.StudentID equals g.StudentID
                     select new { s.StudentName, s.Major, g.GPA };
        Console.WriteLine("Join the student list and student GPA list on student ID and display the student's name, major and gpa:");
        foreach (var item in query6)
        {
            Console.WriteLine($"Name: {item.StudentName}, Major: {item.Major}, GPA: {item.GPA}");
        }
        Console.WriteLine();

        // Join the student list and student club list. Display the names of only those students who are in the Game club.
        var query7 = from s in studentList
                     join c in studentClubList on s.StudentID equals c.StudentID
                     where c.ClubName == "Game"
                     select s.StudentName;
        Console.WriteLine("Join the student list and student club list. Display the names of only those students who are in the Game club:");
        foreach (var name in query7)
        {
            Console.WriteLine(name);
        }
    }
}
