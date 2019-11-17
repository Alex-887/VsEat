﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject
{
    public class Schedule
    {
        public int IdSchedule { get; set; }
        public DateTime openingTime{ get; set; }
        public DateTime closingTime { get; set; }


        public override string ToString()
        {
            return $"{IdSchedule}|{openingTime}|{closingTime}";
        }
    }
}