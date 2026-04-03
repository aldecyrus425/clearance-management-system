using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTO.Pagination
{
    public class PaginationDTO
    {
        private const int MaxPageSize = 50;

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set
            {
                if(value <= 0)
                    _pageSize = 10;
                else
                    _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }

        public string? Search { get; set; }

    }
}
