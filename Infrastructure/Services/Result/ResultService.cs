using Domain.Models.ResultDTOs;
using Infrastructure.Context;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Result
{
    public class ResultService : DataContext, IResultService
    {
        public List<GetResultDto> AllResult()
        {
            List<GetResultDto> result = new List<GetResultDto>();

            string query = "SELECT ResultId, RatingOne, RatingTwo, Egzamin, FanId, GroupId, StudentId FROM Result";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GetResultDto dto = new GetResultDto();
                        dto.ResultId = reader.GetInt32(reader.GetOrdinal("ResultId"));
                        dto.RatingOne = reader.GetInt32(reader.GetOrdinal("RatingOne"));
                        dto.RatingTwo = reader.GetInt32(reader.GetOrdinal("RatingTwo"));
                        dto.Egzamin = reader.GetInt32(reader.GetOrdinal("Egzamin"));
                        dto.FanId = reader.GetInt32(reader.GetOrdinal("FanId"));
                        dto.GroupId = reader.GetInt32(reader.GetOrdinal("GroupId"));
                        dto.StudentId = reader.GetInt32(reader.GetOrdinal("StudentId"));
                        result.Add(dto);
                    }
                }
            }
            return result;
        }                          
    }                              
}                                  
                                   
                                   
                                   