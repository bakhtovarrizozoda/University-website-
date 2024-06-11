using Domain.Models.GroupsDTOs;
using Infrastructure.Context;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Groups
{
    public class GroupService : DataContext, IGroupService
    {
        public List<GetGroupDto> AllGroups()
        {
            List<GetGroupDto> group = new List<GetGroupDto>();

            string query = "SELECT GroupId, GroupName, GroupKod, FacultyId FROM Groups";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString)) 
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GetGroupDto dto = new GetGroupDto();
                        dto.GroupId = reader.GetInt32(reader.GetOrdinal("GroupId"));
                        dto.GroupName = reader.GetString(reader.GetOrdinal("GroupName"));
                        dto.GroupKod = reader.GetString(reader.GetOrdinal("GroupKod"));
                        dto.FacultyId = reader.GetInt32(reader.GetOrdinal("FacultyId"));
                        group.Add(dto);
                    }
                }
            }
            return group;
        }

        public List<Get530102AKurs4Dto> Group530102AKurs4()
        {
            List<Get530102AKurs4Dto> group = new List<Get530102AKurs4Dto>();

            string query = "SELECT " +
                   "s.FirstName || ' ' || s.LastName AS FullName, " +
                   "MAX(CASE WHEN f.FanId = 1 THEN r.RatingOne ELSE NULL END) AS SAKMvaI, " +
                   "MAX(CASE WHEN f.FanId = 14 THEN r.RatingOne ELSE NULL END) AS Sistemaho, " +
                   "MAX(CASE WHEN f.FanId = 16 THEN r.RatingOne ELSE NULL END) AS Etimodnoki, " +
                   "MAX(CASE WHEN f.FanId = 12 THEN r.RatingOne ELSE NULL END) AS Shabakaho, " +
                   "MAX(CASE WHEN f.FanId = 13 THEN r.RatingOne ELSE NULL END) AS Moliyavi, " +
                   "MAX(CASE WHEN f.FanId = 15 THEN r.RatingOne ELSE NULL END) AS Idorakuni " +
                   "FROM Students AS s " +
                   "JOIN Result AS r ON r.StudentId = s.StudentId " +
                   "JOIN Fans AS f ON f.FanId = r.FanId " +
                   "JOIN Groups AS g ON g.GroupId = s.GroupId " +
                   "WHERE g.GroupKod = '530102-А' " +
                   "GROUP BY s.FirstName || ' ' || s.LastName;";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Get530102AKurs4Dto dto = new Get530102AKurs4Dto();
                        dto.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                        dto.SAKMvaI = reader.GetInt32(reader.GetOrdinal("SAKMvaI"));
                        dto.Sistemaho = reader.GetInt32(reader.GetOrdinal("Sistemaho"));
                        dto.Etimodnoki = reader.GetInt32(reader.GetOrdinal("Etimodnoki"));
                        dto.Shabakaho = reader.GetInt32(reader.GetOrdinal("Shabakaho"));
                        dto.Moliyavi = reader.GetInt32(reader.GetOrdinal("Moliyavi"));
                        dto.Idorakuni = reader.GetInt32(reader.GetOrdinal("Idorakuni"));
                        group.Add(dto);
                    }
                }
            }
            return group;
        }
    }
}
