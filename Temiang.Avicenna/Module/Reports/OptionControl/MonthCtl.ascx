<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MonthCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.MonthCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Period" />
        </td>
        <td>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <telerik:RadComboBox ID="cboMonth" Width="100px" runat="server">
                        </telerik:RadComboBox>
                    </td>
                    <td style="width: 20px">
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cboYear" Width="70px" runat="server">
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
