﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjectDTOModel.school
{
    public class GetSchoolResponse_DTO
    {
        public int SchoolId { get; set; }

        public string? SchoolName { get; set; }

        public string? Location { get; set; }

        public string? Schoolcol { get; set; }

        public int SuserId { get; set; }

    }
}