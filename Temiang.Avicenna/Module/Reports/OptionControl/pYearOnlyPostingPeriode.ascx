<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pYearOnlyPostingPeriode.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.pYearOnlyPostingPeriode" %>
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
            <asp:DropDownList runat="server" ID="pYearOnlyPostingPeriode_Year" />
        </td>
    </tr>
</table>
