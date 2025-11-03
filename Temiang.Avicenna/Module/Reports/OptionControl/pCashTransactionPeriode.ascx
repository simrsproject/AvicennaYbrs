<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pCashTransactionPeriode.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.pCashTransactionPeriode" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px"></td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="From" />
        </td>
        <td>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <telerik:RadDatePicker runat="server" ID="txtFromDate" Width="100px">
                        </telerik:RadDatePicker>
                    </td>
                    <td style="width: 20px">
                        <asp:Label ID="Label1" runat="server" Text="To" />
                    </td>
                    <td>
                        <telerik:RadDatePicker runat="server" ID="txtToDate" Width="100px">
                        </telerik:RadDatePicker>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="width: 5px"></td>
        <td style="width: 100px">
            <asp:Label ID="lblBankAccount" runat="server" Text="Bank" />
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlBankAccount" Width="100%" />
        </td>
    </tr>
</table>
