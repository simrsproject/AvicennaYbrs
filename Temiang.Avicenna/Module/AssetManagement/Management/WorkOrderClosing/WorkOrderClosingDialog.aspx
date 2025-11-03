<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="WorkOrderClosingDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.WorkOrderClosingDialog" Title="Edit Accepted By" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image6" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table width="100%">
        <tr id="Tr1" runat="server">
            <td class="label">
                <asp:Label ID="lblAcceptedBy" runat="server" Text="Accepted By"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtAcceptedBy" runat="server" Width="300px" MaxLength="50" />
            </td>
            <td width="20px">
            </td>
        </tr>
    </table>
</asp:Content>