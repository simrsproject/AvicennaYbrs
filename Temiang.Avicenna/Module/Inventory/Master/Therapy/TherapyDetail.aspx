<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="TherapyDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Master.TherapyDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblTherapyID" runat="server" Text="Therapy ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtTherapyID" runat="server" Width="100px" MaxLength="10" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvTherapyID" runat="server" ErrorMessage="Therapy ID required."
                    ValidationGroup="entry" ControlToValidate="txtTherapyID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblTherapyName" runat="server" Text="Therapy Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtTherapyName" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvTherapyName" runat="server" ErrorMessage="Therapy Name required."
                    ValidationGroup="entry" ControlToValidate="txtTherapyName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRTherapyGroup" runat="server" Text="Therapy Group"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRTherapyGroup" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvSRTherapyGroup" runat="server" ErrorMessage="Therapy Group required."
                    ValidationGroup="entry" ControlToValidate="cboSRTherapyGroup" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
