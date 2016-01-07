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
    public partial class CustomerReports : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string startDate = Request.QueryString["startDate"];
                string endDate = Request.QueryString["endDate"];
                ReportParameter rp = new ReportParameter("startDate", startDate);
                ReportParameter rp1 = new ReportParameter("endDate", endDate);

                ICustomer _customerComponent = new CustomerComponent(new DbRepository());
                var customers = _customerComponent.GetCustomersForReport(startDate, endDate);
                ReportViewerCustomer.ProcessingMode = ProcessingMode.Local;
                ReportViewerCustomer.LocalReport.ReportPath = Server.MapPath("~/RDLReports/Reports/CustomerReports.rdlc");

                ReportViewerCustomer.LocalReport.SetParameters(new ReportParameter[] { rp, rp1 });
                ReportViewerCustomer.SizeToReportContent = true;
                ReportDataSource datasource = new ReportDataSource("DataSet1", customers);
                ReportViewerCustomer.LocalReport.DataSources.Clear();
                ReportViewerCustomer.LocalReport.DataSources.Add(datasource);

               // (ReportViewerCustomer.FindControl("TextboxDateRange") as TextBox).Text = "Gaurav Korla";
            }
        }
    }
}