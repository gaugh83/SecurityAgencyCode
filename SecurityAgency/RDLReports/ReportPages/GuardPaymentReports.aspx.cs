using Microsoft.Reporting.WebForms;
using SecurityAgency.Common.Interfaces;
using SecurityAgency.Component;
using SecurityAgency.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SecurityAgency.RDLReports.ReportPages
{
    public partial class GuardPaymentReports : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string guardid = Request.QueryString["guardid"];
                string type = Request.QueryString["type"];

                IGuardPayment _guardPaymentComponent = new GuardPaymentComponent(new DbRepository());
                var guards = _guardPaymentComponent.GetGuardPaymentReport(Convert.ToInt32(guardid));
                ReportViewerGuards.ProcessingMode = ProcessingMode.Local;
                ReportViewerGuards.LocalReport.ReportPath = Server.MapPath("~/RDLReports/Reports/GuardPaymentReport.rdlc");

                ReportViewerGuards.SizeToReportContent = true;
                ReportDataSource datasource = new ReportDataSource("DataSet1", guards);
                ReportViewerGuards.LocalReport.DataSources.Clear();
                ReportViewerGuards.LocalReport.DataSources.Add(datasource);
            }
        }
    }
}