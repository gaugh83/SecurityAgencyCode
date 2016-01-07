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
    public partial class GuardReports : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string startDate = Request.QueryString["startDate"];
                string endDate = Request.QueryString["endDate"];
                ReportParameter rp = new ReportParameter("startDate", startDate);
                ReportParameter rp1 = new ReportParameter("endDate", endDate);

                IGuard _guardComponent = new GuardComponent(new DbRepository());
                var guards = _guardComponent.GetGuardForReport(startDate, endDate);
                ReportViewerGuards.ProcessingMode = ProcessingMode.Local;
                ReportViewerGuards.LocalReport.ReportPath = Server.MapPath("~/RDLReports/Reports/GuardReports.rdlc");

                ReportViewerGuards.LocalReport.SetParameters(new ReportParameter[] { rp, rp1 });
                ReportViewerGuards.SizeToReportContent = true;
                ReportDataSource datasource = new ReportDataSource("DataSet1", guards);
                ReportViewerGuards.LocalReport.DataSources.Clear();
                ReportViewerGuards.LocalReport.DataSources.Add(datasource);
            }
        }
    }
}