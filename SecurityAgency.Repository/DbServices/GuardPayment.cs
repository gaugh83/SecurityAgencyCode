//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SecurityAgency.Repository.DbServices
{
    using System;
    using System.Collections.Generic;
    
    public partial class GuardPayment
    {
        public int GuardPaymentId { get; set; }
        public int GuardId { get; set; }
        public decimal Amount { get; set; }
        public string Comments { get; set; }
        public System.DateTime PaymentDate { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int TotalHours { get; set; }
        public decimal HourlyRate { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    
        public virtual Guard Guard { get; set; }
    }
}
