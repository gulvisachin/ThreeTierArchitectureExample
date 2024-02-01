using System;
using System.Collections.Generic;

namespace PresentationLayer.DataAccessLayer
{
    public partial class Note
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Tag { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UserId { get; set; }
    }
}
