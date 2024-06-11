using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.ResultDTOs
{
    public class GetResultDto
    {
        public int ResultId { get; set; }
        public int RatingOne { get; set; }
        public int RatingTwo { get; set; }
        public int Egzamin { get; set; }
        public int FanId { get; set; }
        public int GroupId { get; set; }
        public int StudentId { get; set; }
    }
}
