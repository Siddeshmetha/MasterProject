﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MasterProjectCommonUtility.Response
{
    public class ResultWithDataDTO<T> : ResultDTO
    {
        public T Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public string previousPage { get; set; }
        public string nextPage { get; set; }

    
    }
}
