<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="ReferToOtherUnitDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Management.WorkOrderRealization.ReferToOtherUnitDialog"
    Title="Untitled Page" %>

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
        <tr>
            <td class="label">
                <asp:Label ID="lblToUnit" runat="server" Text="Refer To Unit"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="300px" AllowCustomText="true"
                    Filter="Contains">
                </telerik:RadComboBox>
            </td>
            <td width="20px">
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblProblemDescription" runat="server" Text="Problem Description"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtProblemDescription" runat="server" Width="300px" MaxLength="500"
                    TextMode="MultiLine" />
            </td>
        </tr>
    </table>
</asp:Content>
