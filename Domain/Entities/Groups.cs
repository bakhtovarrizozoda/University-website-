﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Groups
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupKod { get; set; }
        public int FacultyId { get; set; }
    }
}
