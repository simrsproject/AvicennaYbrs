<%@ Page Title="Import" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="StockOpnameImport.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Stock.StockOpnameImport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td class="label">
                Excel Path File
            </td>
            <td class="entry">
                <asp:FileUpload ID="fileuploadExcel" runat="server" />
            </td>
            <td width="20" />
            <td />
        </tr>
    </table>
</asp:Content>
