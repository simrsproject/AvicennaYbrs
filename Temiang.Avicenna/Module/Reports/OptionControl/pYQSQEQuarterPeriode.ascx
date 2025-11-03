<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pYQSQEQuarterPeriode.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.pYQSQEQuarterPeriode" %>
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
        <td>
            <table cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <asp:DropDownList runat="server" ID="pYQSQEQuarterPeriode_Year" />
                    </td>
                    <td style="width: 5px">
                    </td>
                    <td style="width: 35px">
                        <asp:Label ID="lblQuarter" runat="server" Text="Quarter Start" />
                    </td>
                    <td style="width: 70px">
                        <asp:DropDownList runat="server" ID="pYQSQEQuarterPeriode_QuarterStart" />
                    </td>
                    <td style="width: 30px">
                    </td>
                    <td style="width: 35px">
                        <asp:Label ID="lblQuarterEnd" runat="server" Text="Quarter End" />
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="pYQSQEQuarterPeriode_QuarterEnd" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
