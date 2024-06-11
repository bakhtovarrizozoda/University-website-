using Domain.Entities;
using Domain.Models.StudentsDTOs;
using Infrastructure.Context;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.Services.Students
{
    public class StudentService : DataContext, IStudentService
    {

        public List<GetStudentDto> AllStudents()
        {
            List<GetStudentDto> student = new List<GetStudentDto>();

            string query = "SELECT StudentId, FirstName, LastName, Course, Address, Millat, NamudiTahsil, Shuba, BirthDay, FacultyId, GroupId FROM Students";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GetStudentDto dto = new GetStudentDto();
                        dto.StudentId = reader.GetInt32(reader.GetOrdinal("StudentId"));
                        dto.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        dto.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                        dto.Course = reader.GetInt32(reader.GetOrdinal("Course"));
                        dto.Address = reader.GetString(reader.GetOrdinal("Address"));
                        dto.Millat = reader.GetString(reader.GetOrdinal("Millat"));
                        dto.NamudiTahsil = reader.GetString(reader.GetOrdinal("NamudiTahsil"));
                        dto.Shuba = reader.GetString(reader.GetOrdinal("Shuba"));
                        dto.BirthDay = reader.GetString(reader.GetOrdinal("BirthDay"));
                        dto.FacultyId = reader.GetInt32(reader.GetOrdinal("FacultyId"));
                        dto.GroupId = reader.GetInt32(reader.GetOrdinal("GroupId"));
                        student.Add(dto);
                    }
                }
            }
            return student;
        }

        public List<GetStudentFulDto> GetStudentFull()
        {
            List<GetStudentFulDto> students = new List<GetStudentFulDto>();

            string query = @"
            SELECT 
                s.StudentId,
                s.firstname || ' ' || s.lastname AS fullname,
                g.groupkod,
                s.course,
                s.address,
                s.millat,
                s.namuditahsil,
                f.facultyname,
                s.shuba,
                s.birthday
            FROM 
                students AS s
            JOIN 
                groups AS g ON g.groupid = s.groupid
            JOIN 
            faculties AS f ON f.facultyid = s.facultyid;";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GetStudentFulDto student = new GetStudentFulDto();
                        student.StudentId = reader.GetInt32(reader.GetOrdinal("StudentId"));
                        student.Fullname = reader.GetString(reader.GetOrdinal("fullname"));
                        student.GroupKod = reader.GetString(reader.GetOrdinal("groupkod"));
                        student.Course = reader.GetInt32(reader.GetOrdinal("course"));
                        student.Address = reader.GetString(reader.GetOrdinal("address"));
                        student.Millat = reader.GetString(reader.GetOrdinal("millat"));
                        student.NamudiTahsil = reader.GetString(reader.GetOrdinal("namuditahsil"));
                        student.FacultyName = reader.GetString(reader.GetOrdinal("facultyname"));
                        student.Shuba = reader.GetString(reader.GetOrdinal("shuba"));
                        student.BirthDay = reader.GetString(reader.GetOrdinal("birthday"));
                        students.Add(student);
                    }
                }
            }

            return students;
        }

        public List<HisobotiRetinghoStudentDto> hisobotiRetinghoStudent()
        {
            List<HisobotiRetinghoStudentDto> student = new List<HisobotiRetinghoStudentDto>();

            string query = "SELECT " +
                "f.FacultyName, " +
                "g.GroupKod, " +
                "s.Course, " +
                "COUNT(g.GroupKod) AS Hamagi, " +
                "SUM(CASE WHEN r.RatingOne > 50 THEN 1 ELSE 0 END) AS SuporidR1, " +
                "((SUM(CASE WHEN r.RatingOne > 50 THEN 1 ELSE 0 END) * 100) / COUNT(s.StudentId)) AS ProtcentR1, " +
                "SUM(CASE WHEN r.RatingTwo > 50 THEN 1 ELSE 0 END) AS SuporidR2, " +
                "((SUM(CASE WHEN r.RatingTwo > 50 THEN 1 ELSE 0 END) * 100) / COUNT(s.StudentId)) AS ProtcentR2, " +
                "SUM(CASE WHEN ((r.Egzamin / 2) + ((r.RatingOne + r.RatingTwo) / 4)) > 50 THEN 1 ELSE 0 END) AS Imtihon, " +
                "((SUM(CASE WHEN ((r.Egzamin / 2) + ((r.RatingOne + r.RatingTwo) / 4)) > 50 THEN 1 ELSE 0 END) * 100) / COUNT(s.StudentId)) AS ProtcentIMT, " +
                "fn.FanName, " +
                "t.FirstName || ' ' || t.LastName AS Omuzgor " +
                "FROM " +
                "Faculties AS f " +
                "JOIN Groups AS g ON g.FacultyId = f.FacultyId " +
                "JOIN Students AS s ON s.GroupId = g.GroupId " +
                "JOIN Result AS r ON r.StudentId = s.StudentId " +
                "JOIN Fans AS fn ON fn.FanId = r.FanId " +
                "JOIN Teachers AS t ON t.TeacherId = fn.TeacherId " +
                "GROUP BY " +
                "f.FacultyName, " +
                "g.GroupKod, " +
                "s.Course, " +
                "fn.FanName, " +
                "t.FirstName, " +
                "t.LastName;";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        HisobotiRetinghoStudentDto stu = new HisobotiRetinghoStudentDto();
                        stu.FacultyName = reader.GetString(reader.GetOrdinal("FacultyName"));
                        stu.GroupKod = reader.GetString(reader.GetOrdinal("GroupKod"));
                        stu.Course = reader.GetInt32(reader.GetOrdinal("Course"));
                        stu.Hamagi = reader.GetInt32(reader.GetOrdinal("Hamagi"));
                        stu.SuporidR1 = reader.GetInt32(reader.GetOrdinal("SuporidR1"));
                        stu.ProtcentR1 = reader.GetInt32(reader.GetOrdinal("ProtcentR1"));
                        stu.SuporidR2 = reader.GetInt32(reader.GetOrdinal("SuporidR2"));
                        stu.ProtcentR2 = reader.GetInt32(reader.GetOrdinal("ProtcentR2"));
                        stu.Imtohon = reader.GetInt32(reader.GetOrdinal("Imtihon"));
                        stu.ProtcentIMT = reader.GetInt32(reader.GetOrdinal("ProtcentIMT"));
                        stu.FanName = reader.GetString(reader.GetOrdinal("FanName"));
                        stu.Omuzgor = reader.GetString(reader.GetOrdinal("Omuzgor"));
                        student.Add(stu);
                    }
                }
            }
            return student;
        }

        public List<RuyxatDarsStudentDto> AllRuyxatDarsStudent()
        {
            List<RuyxatDarsStudentDto> student = new List<RuyxatDarsStudentDto>();

            string query = "SELECT " +
                "f.FanName, " +
                "f.Credit, " +
                "r.RatingOne, " +
                "r.RatingTwo, " +
                "r.Egzamin, " +
                "(((r.RatingOne + r.RatingTwo) / 4)) + (r.Egzamin / 2) as BahoiUmumi, " + 
                "t.FirstName || ' ' || t.LastName as Omuzgor " + 
                "FROM " +
                "Students as s " + 
                "JOIN " +
                "Result as r ON r.StudentId = s.StudentId " +
                "JOIN " +
                "Fans as f ON f.FanId = r.FanId " + 
                "JOIN " +
                "Teachers as t ON t.TeacherId = f.TeacherId " + 
                "JOIN " +
                "Groups as g ON g.GroupId = s.GroupId;";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RuyxatDarsStudentDto stu = new RuyxatDarsStudentDto();
                        stu.FanName = reader.GetString(reader.GetOrdinal("FanName"));
                        stu.Credit = reader.GetInt32(reader.GetOrdinal("Credit"));
                        stu.RatingOne = reader.GetInt32(reader.GetOrdinal("RatingOne"));
                        stu.RatingTwo = reader.GetInt32(reader.GetOrdinal("RatingTwo"));
                        stu.Egzamin = reader.GetInt32(reader.GetOrdinal("Egzamin"));
                        stu.BahoiUmumi = reader.GetInt32(reader.GetOrdinal("BahoiUmumi"));
                        stu.Omuzgor = reader.GetString(reader.GetOrdinal("Omuzgor"));
                        student.Add(stu);
                    }
                }
            }
            return student;
        }

        public List<RuyxatDarsStudentDto> SearchRuyxatiDarshoiStudent(SearchRuyxatiDarsStudentDto search)
        {
            string query = @"
                SELECT 
                    f.FanName, 
                    f.Credit, 
                    r.RatingOne, 
                    r.RatingTwo, 
                    r.Egzamin, 
                    (((r.RatingOne + r.RatingTwo) / 4)) + (r.Egzamin / 2) AS BahoiUmumi, 
                    t.FirstName || ' ' || t.LastName AS Omuzgor 
                FROM 
                    Students AS s 
                JOIN 
                    Result AS r ON r.StudentId = s.StudentId 
                JOIN 
                    Fans AS f ON f.FanId = r.FanId 
                JOIN 
                    Teachers AS t ON t.TeacherId = f.TeacherId 
                JOIN 
                    Groups AS g ON g.GroupId = s.GroupId 
                WHERE 
                    s.FirstName LIKE @FirstName AND s.LastName LIKE @LastName;";

            List<RuyxatDarsStudentDto> students = new List<RuyxatDarsStudentDto>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FirstName", search.FirstName + "%");
                command.Parameters.AddWithValue("@LastName", search.LastName + "%");

                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RuyxatDarsStudentDto student = new RuyxatDarsStudentDto
                        {
                            FanName = reader.GetString(reader.GetOrdinal("FanName")),
                            Credit = reader.GetInt32(reader.GetOrdinal("Credit")),
                            RatingOne = reader.GetInt32(reader.GetOrdinal("RatingOne")),
                            RatingTwo = reader.GetInt32(reader.GetOrdinal("RatingTwo")),
                            Egzamin = reader.GetInt32(reader.GetOrdinal("Egzamin")),
                            BahoiUmumi = reader.GetInt32(reader.GetOrdinal("BahoiUmumi")),
                            Omuzgor = reader.GetString(reader.GetOrdinal("Omuzgor"))
                        };
                        students.Add(student);
                    }
                }
            }
            return students;
        }

        public List<GetStudentFulDto> SearchStudent(SearchStudentDto student)
        {
            List<GetStudentFulDto> students = new List<GetStudentFulDto>();
            string query = @"
                SELECT 
                    s.StudentId,
                    s.FirstName || ' ' || s.LastName AS FullName,
                    g.GroupKod,
                    s.Course,
                    s.Address,
                    s.Millat,
                    s.NamudiTahsil,
                    f.FacultyName,
                    s.Shuba,
                    s.BirthDay
                FROM 
                    Students AS s
                JOIN 
                    Groups AS g ON g.GroupId = s.GroupId
                JOIN 
                    Faculties AS f ON f.FacultyId = s.FacultyId
                WHERE 
                    s.FirstName LIKE @FirstName AND s.LastName LIKE @LastName";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                if (string.IsNullOrEmpty(student.FirstName))
                {
                    throw new ArgumentException("FirstName cannot be null or empty", nameof(student.FirstName));
                }
                command.Parameters.AddWithValue("@FirstName", student.FirstName + "%");
                command.Parameters.AddWithValue("@LastName", student.LastName + "%");

                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GetStudentFulDto stu = new GetStudentFulDto
                        {
                            StudentId = reader.GetInt32(reader.GetOrdinal("StudentId")),
                            Fullname = reader.GetString(reader.GetOrdinal("FullName")),
                            Course = reader.GetInt32(reader.GetOrdinal("Course")),
                            Address = reader.GetString(reader.GetOrdinal("Address")),
                            Millat = reader.GetString(reader.GetOrdinal("Millat")),
                            NamudiTahsil = reader.GetString(reader.GetOrdinal("NamudiTahsil")),
                            Shuba = reader.GetString(reader.GetOrdinal("Shuba")),
                            BirthDay = reader.GetString(reader.GetOrdinal("BirthDay")),
                            FacultyName = reader.GetString(reader.GetOrdinal("FacultyName")),
                            GroupKod = reader.GetString(reader.GetOrdinal("GroupKod"))
                        };
                        students.Add(stu);
                    }
                }
            }
            return students;
        }

        public List<RuyxatiQarzdorhoDto> RuyxatiQarzdorho()
        {
            List<RuyxatiQarzdorhoDto> students = new List<RuyxatiQarzdorhoDto>();

            string query = "SELECT  " +
                     "s.StudentId, " +
                     "s.FirstName || ' ' || s.LastName AS FullName, " +
                     "SUM(fn.Credit) AS Credit, " +
                     "f.FacultyName AS FacultyName,  " +
                     "s.Course AS Course, " +
                     "g.GroupKod AS GroupKod " +
                 "FROM " +
                     "Faculties AS f " +
                     "JOIN Groups AS g ON g.FacultyId = f.FacultyId " +
                     "JOIN Students AS s ON s.GroupId = g.GroupId " +
                     "JOIN Result AS r ON r.StudentId = s.StudentId " +
                     "JOIN Fans AS fn ON fn.FanId = r.FanId " +
                     "JOIN Teachers AS t ON t.TeacherId = fn.TeacherId " +
                " WHERE " +
                     "((r.Egzamin / 2) + ((r.RatingOne + r.RatingTwo) / 4)) < 50 " +
                 "GROUP BY " +
                     "s.StudentId," +
                     "s.FirstName,  " +
                     "s.LastName,  " +
                     "f.FacultyName,  " +
                     "s.Course,  " +
                     "g.GroupKod; ";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RuyxatiQarzdorhoDto student = new RuyxatiQarzdorhoDto
                        {
                            StudentId = reader.GetInt32(reader.GetOrdinal("StudentId")),
                            FullName = reader.GetString(reader.GetOrdinal("FullName")),
                            Credit = reader.GetInt32(reader.GetOrdinal("Credit")),
                            FacultyName = reader.GetString(reader.GetOrdinal("FacultyName")),
                            Course = reader.GetInt32(reader.GetOrdinal("Course")),
                            GroupKod = reader.GetString(reader.GetOrdinal("GroupKod")),
                        };
                        students.Add(student);
                    }
                }
            }
            return students;
        }

        /*public List<RuyxatiQarzdorhoDto> SearchRuyxatiQarzdorho(SearchRuyxatiDarsStudentDto search)
        {
            string query = "SELECT " +
                     "s.StudentId, " +
                     "s.FirstName || ' ' || s.LastName AS FullName, " +
                     "SUM(fn.Credit) AS Credit, " +
                     "f.FacultyName AS FacultyName,  " +
                     "s.Course AS Course, " +
                     "g.GroupKod AS GroupKod " +
                "FROM " +
                "Faculties AS f " +
                "JOIN Groups AS g ON g.FacultyId = f.FacultyId " +
                "JOIN Students AS s ON s.GroupId = g.GroupId " +
                "JOIN Result AS r ON r.StudentId = s.StudentId " +
                "JOIN Fans AS fn ON fn.FanId = r.FanId " +
                "JOIN Teachers AS t ON t.TeacherId = fn.TeacherId " +
                "WHERE ((r.Egzamin / 2) + ((r.RatingOne + r.RatingTwo) / 4)) < 50 " +
                "AND s.FirstName LIKE @FirstName " +
                "AND s.LastName LIKE @LastName " +
                "GROUP BY " +
                     "s.StudentId," + 
                     "s.FirstName,  " +
                     "s.LastName,  " +
                     "f.FacultyName,  " +
                     "s.Course,  " +
                     "g.GroupKod; ";

            List<RuyxatiQarzdorhoDto> students = new List<RuyxatiQarzdorhoDto>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FirstName", search.FirstName + "%");
                command.Parameters.AddWithValue("@LastName", search.LastName + "%");

                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RuyxatiQarzdorhoDto student = new RuyxatiQarzdorhoDto
                        {
                            StudentId = reader.GetInt32(reader.GetOrdinal("StudentId")),
                            FullName = reader.GetString(reader.GetOrdinal("FullName")),
                            Credit = reader.GetInt32(reader.GetOrdinal("Credit")),
                            FacultyName = reader.GetString(reader.GetOrdinal("FacultyName")),
                            Course = reader.GetInt32(reader.GetOrdinal("Course")),
                            GroupKod = reader.GetString(reader.GetOrdinal("GroupKod"))
                        };
                        students.Add(student);
                    }
                }
            }
            return students;
        }*/

        public List<FanRuyxatQarzdorDto> fanRuyxatQarzdor(int studentId)
        {
            List<FanRuyxatQarzdorDto> student = new List<FanRuyxatQarzdorDto>();

            string query = "SELECT " +
                "fn.FanId, " +
                "fn.FanName, " +
                "fn.Credit, " +
                "s.StudentId " +
                "FROM " +
                "Students AS s " +
                "JOIN Result AS r ON r.StudentId = s.StudentId " +
                "JOIN Fans AS fn ON fn.FanId = r.FanId " +
                "WHERE " +
                "((r.Egzamin / 2) + ((r.RatingOne + r.RatingTwo) / 4)) < 50 " +
                "and s.StudentId = @StudentId;";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@StudentId", studentId);

                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FanRuyxatQarzdorDto studentRecord = new FanRuyxatQarzdorDto
                        {
                            FanId = reader.GetInt32(reader.GetOrdinal("FanId")),
                            FanName = reader.GetString(reader.GetOrdinal("FanName")),
                            Credit = reader.GetInt32(reader.GetOrdinal("Credit")),
                            StudentId = reader.GetInt32(reader.GetOrdinal("StudentId"))
                        };
                        student.Add(studentRecord);
                    }
                }
            }
            return student;
        }
    }
}
