using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanBoardCourse.Models
{
    public class Board
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int CreatorId { get; set; }
        public User? Creator { get; set; }

        public List<Column> Columns { get; set; } = new();

    }
}
