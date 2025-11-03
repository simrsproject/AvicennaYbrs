<%@ Page Title="Import" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="VoucherImport.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.VoucherImport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
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
                <tr>
            <td class="label">
                
            </td>
            <td class="entry">
            <asp:CheckBox runat="server" ID="chkIsFirstRowAsHeader" Text="Set every first row as journal description" />
            </td>
            <td width="20" />
            <td />
        </tr>
    </table>
</asp:Content>
