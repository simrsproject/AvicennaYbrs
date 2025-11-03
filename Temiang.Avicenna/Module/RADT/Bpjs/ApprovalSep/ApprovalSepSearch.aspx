<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="ApprovalSepSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.ApprovalSepSearch" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                No Peserta
            </td>
            <td class="filter" />
            <td class="entry">
                <telerik:RadTextBox ID="txtNoPeserta" runat="server" Width="300px" />
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td class="label">
                Tanggal SEP
            </td>
            <td class="filter" />
            <td class="entry">
                <telerik:RadDatePicker ID="txtTglSep" runat="server" Width="100px" />
            </td>
            <td width="20px" />
            <td />
        </tr>
    </table>
</asp:Content>
