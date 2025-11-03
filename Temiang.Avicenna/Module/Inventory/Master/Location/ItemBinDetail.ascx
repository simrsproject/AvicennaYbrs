<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemBinDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Master.ItemBinDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemBalance" runat="server" ValidationGroup="ItemBinSetting" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemBinSetting"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblItemID" runat="server" Text="ID"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtItemID" runat="server" Width="100px" MaxLength="50"/>
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblItemName" runat="server" Text="Item Bin"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px" MaxLength="100" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ItemBinSetting"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ItemBinSetting" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
