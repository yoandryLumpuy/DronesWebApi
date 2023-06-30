﻿namespace DronesWebApi.Models
{
    public class PaginatedListRequest
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}