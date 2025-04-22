using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApplication.Common.Models.Domain.Response
{
    public class GetAllTodosResponse
    {
        public DateTime ExpirationDate { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? CompletePercent { get; set; }
    }
}
