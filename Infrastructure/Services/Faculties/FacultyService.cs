using Domain.Models.FacultiesDTOs;
using Infrastructure.Context;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Faculties
{
    public class FacultyService : DataContext, IFacultySevice
    {
        public List<GetFacultyDto> AllFaculties()
        {
            List<GetFacultyDto> faculty = new List<GetFacultyDto>();

            string query = "SELECT FacultyId, FacultyName, FullNameFaculty FROM Faculties";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GetFacultyDto dto = new GetFacultyDto();
                        dto.FacultyId = reader.GetInt32(reader.GetOrdinal("FacultyId"));
                        dto.FacultyName = reader.GetString(reader.GetOrdinal("FacultyName"));
                        dto.FullNameFaculty = reader.GetString(reader.GetOrdinal("FullNameFaculty"));
                        faculty.Add(dto);
                    }
                }
            }
            return faculty;
        }

        public List<GetFacultyGroupDto> GetFacultyGroupStatistics()
        {
            List<GetFacultyGroupDto> statistics = new List<GetFacultyGroupDto>();

            string query = @"
             SELECT 
                 f.FacultyName, 
                 g.GroupKod, 
                 s.Course, 
                 COUNT(s.StudentId) AS Hamagi, 
                 SUM(CASE WHEN s.Namuditahsil = 'Шартномавӣ' THEN 1 ELSE 0 END) AS Shartnomavi,
                 SUM(CASE WHEN s.Namuditahsil = 'Буҷет' THEN 1 ELSE 0 END) AS Budget,
                 SUM(CASE WHEN s.Namuditahsil = 'Квота' THEN 1 ELSE 0 END) AS Quota,
                 g.GroupName
             FROM 
                 students AS s 
             JOIN 
                 groups AS g ON g.GroupId = s.GroupId
             JOIN 
                 Faculties AS f ON f.FacultyId = s.FacultyId
             GROUP BY 
                 f.FacultyName, 
                 g.GroupKod, 
                 g.GroupName,
                 s.Course
             ORDER BY 
                 s.Course;";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GetFacultyGroupDto dto = new GetFacultyGroupDto();
                        dto.FacultyName = reader.GetString(reader.GetOrdinal("FacultyName"));
                        dto.GroupKod = reader.GetString(reader.GetOrdinal("GroupKod"));
                        dto.Course = reader.GetInt32(reader.GetOrdinal("Course"));
                        dto.Hamagi = reader.GetInt32(reader.GetOrdinal("Hamagi"));
                        dto.Shartnomavi = reader.GetInt32(reader.GetOrdinal("Shartnomavi"));
                        dto.Budget = reader.GetInt32(reader.GetOrdinal("Budget"));
                        dto.Quota = reader.GetInt32(reader.GetOrdinal("Quota"));
                        dto.GroupName = reader.GetString(reader.GetOrdinal("GroupName"));
                        statistics.Add(dto);
                    }
                }
            }
            return statistics;
        }
    }
}
