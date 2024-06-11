using Domain.Models.FansDTOs;
using Infrastructure.Context;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Fans
{
    public class FanService : DataContext, IFanService
    {
        public List<GetFanDto> AllFans()
        {
            List<GetFanDto> fan = new List<GetFanDto>();

            string query = "SELECT FanId, FanName, Credit, TeacherId FROM Fans";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GetFanDto dto = new GetFanDto();
                        dto.FanId = reader.GetInt32(reader.GetOrdinal("FanId"));
                        dto.FanName = reader.GetString(reader.GetOrdinal("FanName"));
                        dto.Credit = reader.GetInt32(reader.GetOrdinal("Credit"));
                        dto.TeacherId = reader.GetInt32(reader.GetOrdinal("TeacherId"));
                        fan.Add(dto);
                    }
                }
            }
            return fan;
        }
    }
}
