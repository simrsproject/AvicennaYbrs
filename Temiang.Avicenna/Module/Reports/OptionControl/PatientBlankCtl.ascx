<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PatientBlankCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.PatientBlankCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr style="display:none">
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Patient" />
        </td>
        <td>
            <telerik:RadTextBox runat="server" ID="txtPatientID" Width="100px" ReadOnly="true">
            </telerik:RadTextBox>
        </td>
    </tr>
</table>