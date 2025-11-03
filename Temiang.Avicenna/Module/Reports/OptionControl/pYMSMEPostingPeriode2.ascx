<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pYMSMEPostingPeriode2.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.pYMSMEPostingPeriode2" %>
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
            <asp:Label ID="lblYearStart" runat="server" Text="YearStart" />
        </td>
        <td style="width: 100px">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:DropDownList runat="server" ID="pYMSMEPostingPeriode_YearStart" AutoPostBack="true" />
                    </td>
                    <td style="width: 5px">
                    </td>
                    <td style="width: 35px">
                        <asp:Label ID="lblYearEnd" runat="server" Text="Year End" />
                    </td>
                    <td style="width: 70px">
                        <asp:DropDownList runat="server" ID="pYMSMEPostingPeriode_YearEnd" />
                    </td>
                    <td style="width: 30px">
                    </td>
                    <td style="width: 35px">
                        <asp:Label ID="lblMonth" runat="server" Text="Month" />
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="pYMSMEPostingPeriode_Month" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
