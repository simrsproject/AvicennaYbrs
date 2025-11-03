<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pSinglePostingPeriode.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.pSinglePostingPeriode" %>
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
            <table cellpadding="0" cellspacing="">
                <tr>
                    <td>
                        <asp:DropDownList runat="server" ID="pSinglePostingPeriode_Year" AutoPostBack="true" />
                    </td>
                    <td style="width: 5px">
                    </td>
                    <td style="width: 35px">
                        <asp:Label ID="lblMonth" runat="server" Text="Month" />
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="pSinglePostingPeriode_Month" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
