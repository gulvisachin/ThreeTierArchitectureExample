using System;
using System.Collections.Generic;

namespace PresentationLayer.DataAccessLayer
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int OrderHeaderId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }

        public virtual OrderHeader OrderHeader { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
