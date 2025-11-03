<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssetSortByCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.AssetSortByCtl" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Short By" />
        </td>
        <td>
            <asp:RadioButtonList ID="rbtAssetShortBy" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                <asp:ListItem Value="ID" Text="ID" />
                <asp:ListItem Value="Name" Text="Name" />
                <asp:ListItem Value="Location" Text="Location" />
            </asp:RadioButtonList>
        </td>
    </tr>
</table>