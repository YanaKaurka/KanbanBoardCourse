using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanBoardCourse.Models
{
    public class Column
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Position { get; set; }

        public int BoardId { get; set; }
        public Board? Board { get; set; }

        public List<Task> Tasks { get; set; } = new();
    }
}
