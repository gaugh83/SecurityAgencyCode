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
    
    public partial class Error_Log
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
        public string TargetSite { get; set; }
        public Nullable<System.DateTime> ErrorDate { get; set; }
        public string ExceptionDetail { get; set; }
    }
}