<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="ItemTariffRequestImport.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Tariff.ItemTariffRequestImport" Title="Untitled Page" %>
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