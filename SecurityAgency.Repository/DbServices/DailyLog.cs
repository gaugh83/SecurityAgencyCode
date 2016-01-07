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
    
    public partial class DailyLog
    {
        public int DailyLogId { get; set; }
        public int CustomerId { get; set; }
        public int GuardId { get; set; }
        public int Hours { get; set; }
        public System.DateTime Dated { get; set; }
        public string Comments { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Guard Guard { get; set; }
    }
}
