using Domain.Models.TeachersDTOs;
using Infrastructure.Context;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Teachers
{
    public class TeacherService : DataContext, ITeacherService
    {

        public List<GetTeacherDto> AllTeachers()
        {
            List<GetTeacherDto> teachers = new List<GetTeacherDto>();

            string query = "SELECT TeacherId, FirstName, LastName, BirthDay, Vasifa, Ixtisos, Email FROM Teachers";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GetTeacherDto dto = new GetTeacherDto();
                        dto.TeacherId = reader.GetInt32(reader.GetOrdinal("TeacherId"));
                        dto.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        dto.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                        dto.BirthDay = reader.GetString(reader.GetOrdinal("BirthDay"));
                        dto.Vasifa = reader.GetString(reader.GetOrdinal("Vasifa"));
                        dto.Ixtisos = reader.GetString(reader.GetOrdinal("Ixtisos"));
                        dto.Email = reader.GetString(reader.GetOrdinal("Email"));
                        teachers.Add(dto);
                    }
                }
            }
            return teachers;
        }
    }
}
