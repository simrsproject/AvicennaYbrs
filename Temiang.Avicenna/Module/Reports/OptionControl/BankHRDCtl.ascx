<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BankHRDCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.BankHRDCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Bank" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboBankID" Width="100%" runat="server" EnableLoadOnDemand="true">
            
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
