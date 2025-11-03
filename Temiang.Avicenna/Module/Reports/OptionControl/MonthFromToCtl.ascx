<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MonthFromToCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.MonthFromToCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td colspan="2">
            <asp:Label ID="lblParameterCaption" runat="server" Text="" Font-Bold="true" />
        </td>
    </tr>
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblMonth" runat="server" Text="Month" />
        </td>
        <td>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <telerik:RadComboBox ID="cboFromMonth" Width="100px" runat="server">
                        </telerik:RadComboBox>
                    </td>
                    <td style="width: 20px">
                        <asp:Label ID="lblTo" runat="server" Text="To" />
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cboToMonth" Width="100px" runat="server">
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="Label1" runat="server" Text="Year" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboPeriodYear" Width="70px" runat="server">
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
