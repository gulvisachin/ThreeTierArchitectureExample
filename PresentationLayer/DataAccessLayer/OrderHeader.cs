using System;
using System.Collections.Generic;

namespace PresentationLayer.DataAccessLayer
{
    public partial class OrderHeader
    {
        public OrderHeader()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public DateTime DateOfOrder { get; set; }
        public DateTime DateOfShipping { get; set; }
        public double OrderTotal { get; set; }
        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Carrier { get; set; }
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }
        public DateTime DateOfPayment { get; set; }
        public DateTime DueDate { get; set; }
        public string Phone { get; set; } = null!;
        public string? Address { get; set; }
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string PinCode { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
