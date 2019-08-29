﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FreelanceService.DAL.Entities
{
    public class Response
    {
        public int ResponseId { get; set; }
        public int UserId_Executor { get; set; }
        public int TaskId { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeOfResponse { get; set; }
    }
}