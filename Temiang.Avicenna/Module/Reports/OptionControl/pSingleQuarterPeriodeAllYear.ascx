<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pSingleQuarterPeriodeAllYear.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.pSingleQuarterPeriodeAllYear" %>
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
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:DropDownList runat="server" ID="pSingleQuarterPeriode_Year" />
                    </td>
                    <td style="width: 5px">
                    </td>
                    <td style="width: 35px">
                        <asp:Label ID="lblQuarter" runat="server" Text="Quarter" />
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="pSingleQuarterPeriode_Quarter" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
