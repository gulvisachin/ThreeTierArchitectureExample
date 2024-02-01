using System;
using System.Collections.Generic;

namespace PresentationLayer.DataAccessLayer
{
    public partial class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Address { get; set; }
    }
}
