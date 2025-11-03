<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    Codebehind="VisitTypeDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.VisitTypeDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblVisitTypeID" runat="server" Text="Visit Type ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtVisitTypeID" runat="server" Width="100px" MaxLength="10" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvVisitTypeID" runat="server" ErrorMessage="Visit Type ID required."
                    ValidationGroup="entry" ControlToValidate="txtVisitTypeID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblVisitTypeName" runat="server" Text="Visit Type Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtVisitTypeName" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvVisitTypeName" runat="server" ErrorMessage="Visit Type Name required."
                    ValidationGroup="entry" ControlToValidate="txtVisitTypeName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="1000" TextMode="MultiLine" />
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
