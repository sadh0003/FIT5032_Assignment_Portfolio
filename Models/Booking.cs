//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FIT5032_Assignment_Portfolio.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Booking
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public string Review { get; set; }
        public Nullable<short> Rating { get; set; }
        public int RoomId { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual Room Room { get; set; }
    }
}
