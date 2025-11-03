<%@ Page Title="Text Editor" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="TextDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.Dispensary.PrescriptionSales.TextDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td class="label">
                <asp:Label ID="lblText1" runat="server" Text="Order" />
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtText1" runat="server" Width="300px" />
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblText2" runat="server" Text="Iter" />
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtText2" runat="server" Width="300px" />
            </td>
            <td width="20px" />
            <td />
        </tr>
    </table>
</asp:Content>
