<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="StockOpnameLineNote.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.StockOpname.RSCH.StockOpnameLineNote"
    Title="Note" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <tr>
                <td class="label">Item ID
                </td>
                <td>
                    <telerik:RadTextBox ID="txtItemID" runat="server" Width="100px" ReadOnly="True"/>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Item Name
                </td>
                <td>
                    <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px" ReadOnly="True"/>
                </td>
                <td></td>
            </tr>
            <td class="label">
                Note
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNote" runat="server" Width="300px" TextMode="MultiLine" MaxLength="500" />
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
