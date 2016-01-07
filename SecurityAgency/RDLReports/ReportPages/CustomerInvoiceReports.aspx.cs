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
    public partial class CustomerInvoiceReports : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string startDate = Request.QueryString["startDate"];
                string endDate = Request.QueryString["endDate"];
                int customerId = Convert.ToInt32(Request.QueryString["customerid"]);
                string invoiceType = Request.QueryString["InvoiceType"].ToString();

                ICustomerInvoice _customerComponent = new CustomerInvoiceComponent(new DbRepository());
                var customers = _customerComponent.GetCustomersInvoiceForReport(startDate, endDate, customerId, invoiceType);
                ReportViewerCustomer.ProcessingMode = ProcessingMode.Local;
                ReportViewerCustomer.LocalReport.ReportPath = Server.MapPath("~/RDLReports/Reports/CustomerInvoiceReports.rdlc");

                ReportViewerCustomer.SizeToReportContent = true;
                ReportDataSource datasource = new ReportDataSource("DataSet1", customers);
                ReportViewerCustomer.LocalReport.DataSources.Clear();
                ReportViewerCustomer.LocalReport.DataSources.Add(datasource);
            }
        }
    }
}