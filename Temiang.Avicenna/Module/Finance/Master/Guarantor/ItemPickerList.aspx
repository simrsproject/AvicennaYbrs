<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="ItemPickerList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.GuarantorItemPickerList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server" />
    <table width="100%" id="table1" runat="server" cellpadding="0" cellspacing="0">
        <tr>
            <td valign="top" width="20%">
            </td>
            <td valign="top" width="20%">
            </td>
            <td valign="top" width="20%">
            </td>
            <td valign="top" width="20%">
            </td>
            <td valign="top" width="20%">
            </td>
        </tr>
    </table>
</asp:Content>
