using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Fans
    {
        public int FanId { get; set; }
        public string FanName { get; set; }
        public int Credit { get; set; }
        public int TeacherId { get; set; }
    }
}
