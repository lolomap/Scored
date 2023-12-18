using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ScoredBackend.Controllers
{
    [ApiController]
    public class GradesController : Controller
    {
        [HttpPost]
        [Route("/auth")]
        public AuthResponse Auth(AuthArgs args)
        {
            DBTable query;
            AuthResponse response;
            UserType type = UserType.Teacher;

            query = DBConnection.Self!.Query(
                "SELECT Id, Surname, Name, Patronymic FROM teacher WHERE Login = @0",
                [args.Login]
            );

            if (query.IsError)
            {
                HttpContext.Response.StatusCode = 502;
                return new AuthResponse();
            }

            if (query.rows.Count == 0)
            {
                query = DBConnection.Self!.Query(
                    "SELECT Id, Surname, Name, Patronymic FROM student WHERE Login = @0",
                    [args.Login]
                );

                if (query.IsError)
                {
                    HttpContext.Response.StatusCode = 502;
                    return new AuthResponse();
                }

                type = UserType.Student;
            }

            if (query.rows.Count > 0)
            {
                response = new()
                {
                    Id = (int)(query.rows[0]["Id"] ?? 0),
                    Type = type,
                    Surname = (string)(query.rows[0]["Surname"] ?? ""),
                    Name = (string)(query.rows[0]["Name"] ?? ""),
                    Patronymic = (string)(query.rows[0]["Patronymic"] ?? "")
                };
            }
            else
            {
                response = new();
            }

            return response;
        }

        [HttpPost]
        [Route("/grades")]
        public List<DBTable> RequestStudentGrades(RequestStudentGradesArgs args)
        {
            List<DBTable> result = [];

            DBTable studentDisciplines = DBConnection.Self!.Query(
                "CALL GetDisciplines(@0, @1)",
                [args.StudentId, args.TeacherId ?? -1]
            );
            if (studentDisciplines.IsError)
            {
                HttpContext.Response.StatusCode = 502;
                return result;
            }

            foreach (int? disciplineId in studentDisciplines.Select("Discipline"))
            {
                DBTable grades = DBConnection.Self!.Query(
                    "CALL GetStudentDisciplineGrades(@0, @1)" ,
                    [args.StudentId, disciplineId ?? -1]
                );

                if (grades.IsError)
                {
                    HttpContext.Response.StatusCode = 502;
                    return [grades];
                }

                result.Add(grades);
            }

            return result;
        }

        [HttpPost]
        [Route("/works")]
        public DBTable RequestDisciplineWorks(RequestDisciplineWorksArgs args)
        {
            DBTable disciplineWorks = DBConnection.Self!.Query(
                "CALL GetDisciplineWorks(@0, @1)",
                [args.DisciplineId, args.StudentId ?? -1]
            );

            if (disciplineWorks.IsError)
            {
                HttpContext.Response.StatusCode = 502;
            }

            return disciplineWorks;
        }

        [HttpPost]
        [Route("/change_score")]
        public DBTable RequestChangeStudentsScore(RequestChangeStudentScoreArgs args)
        {
            DBTable error = DBConnection.Self!.Query(
                "CALL ChangeStudentScore(@0, @1, @2)",
                [args.StudentId, args.WorkId, args.Score]
            );
            if (error.IsError)
            {
                HttpContext.Response.StatusCode = 502;
            }
            return error;
        }
    }

    public struct AuthArgs
    {
        public string Login { get; set; }
    }

    public enum UserType
    {
        Student,
        Teacher
    }

    public struct AuthResponse
    {
        public int Id { get; set; }
        public UserType Type { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
    }

    public struct RequestStudentGradesArgs
    {
        public int StudentId { get; set; }
        public int? TeacherId { get; set; }
    }

    public struct RequestDisciplineWorksArgs
    {
        public int DisciplineId { get; set; }
        public int? StudentId { get; set; }
    }

    public struct RequestChangeStudentScoreArgs
    {
        public int StudentId { get; set; }
        public int WorkId { get; set; }
        public int Score { get; set; }
    }

    public struct RequestChangeWorkArgs
    {
        public int WorkId { get; set; }
        public string Name { get; set; }
        public int MaxScore { get; set; }
    }

    public struct RequestAddWorkArgs
    {
        public int DisciplineId { get; set; }
        public string Name { get; set; }
        public int MaxScore { get; set; }
    }
}
