
<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    Codebehind="LabelDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Master.LabelDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblLabelID" runat="server" Text="Label ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtLabelID" runat="server" Width="100px" MaxLength="10" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvLabelID" runat="server" ErrorMessage="Label ID required."
                    ValidationGroup="entry" ControlToValidate="txtLabelID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblLabelName" runat="server" Text="Label Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtLabelName" runat="server" Width="300px" MaxLength="50" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvLabelName" runat="server" ErrorMessage="Label Name required."
                    ValidationGroup="entry" ControlToValidate="txtLabelName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
                        <td class="label">
                        <asp:Label ID="lblIsActive" runat="server" Text="Active"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
    </table>
    
</asp:Content>

