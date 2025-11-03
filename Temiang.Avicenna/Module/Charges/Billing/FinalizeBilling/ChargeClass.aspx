<%@ Page Title="Update Charge Class" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="ChargeClass.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.FinalizeBilling.ChargeClass" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="vsumTransChargesItem" runat="server" ValidationGroup="TransChargesItem" />
    <table width="100%">
        <tr runat="server">
            <td class="label">
                <asp:Label ID="lblChargeClass" runat="server" Text="Charge Class"></asp:Label>
            </td>
            <td class="entrydescription">
                <telerik:RadComboBox ID="cboChargeClass" runat="server" Width="300px" />
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
