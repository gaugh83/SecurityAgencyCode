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
    
    public partial class getGuardHourlyRate_Result
    {
        public int GuardId { get; set; }
        public string Name { get; set; }
        public System.DateTime EffectiveFrom { get; set; }
        public System.DateTime EffectiveTo { get; set; }
        public decimal HourlyRate { get; set; }
    }
}
