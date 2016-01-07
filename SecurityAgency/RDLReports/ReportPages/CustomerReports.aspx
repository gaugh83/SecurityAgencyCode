<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerReports.aspx.cs" Inherits="SecurityAgency.RDLReports.ReportPages.CustomerReports" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>
<style>
 body{ overflow-x:hidden;overflow-y:hidden; }
</style>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height:550px;overflow-y:auto;overflow-x:hidden">
         <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewerCustomer" runat="server" Width="1050px"></rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
