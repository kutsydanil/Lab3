﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class StaffCasts
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public int ListEventId { get; set; }
    }
}
