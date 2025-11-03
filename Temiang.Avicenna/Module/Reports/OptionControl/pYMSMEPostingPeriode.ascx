<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pYMSMEPostingPeriode.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.pYMSMEPostingPeriode" %>
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
            <asp:Label ID="lblYear" runat="server" Text="Year" />
        </td>
        <td style="width: 100px">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:DropDownList runat="server" ID="pYMSMEPostingPeriode_Year" AutoPostBack="true" />
                    </td>
                    <td style="width: 5px">
                    </td>
                    <td style="width: 35px">
                        <asp:Label ID="lblMonthStart" runat="server" Text="Month Start" />
                    </td>
                    <td style="width: 70px">
                        <asp:DropDownList runat="server" ID="pYMSMEPostingPeriode_MonthStart" />
                    </td>
                    <td style="width: 30px">
                    </td>
                    <td style="width: 35px">
                        <asp:Label ID="lblMonthEnd" runat="server" Text="Month End" />
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="pYMSMEPostingPeriode_MonthEnd" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
