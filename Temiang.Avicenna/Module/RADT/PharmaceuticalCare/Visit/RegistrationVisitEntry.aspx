<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="RegistrationVisitEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PharmaceuticalCare.RegistrationVisitEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">Date 
            </td>
            <td class="entry">
                <telerik:RadDateTimePicker ID="txtVisitDateTime" runat="server" Width="170px" />
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Visit Time required."
                    ValidationGroup="entry" ControlToValidate="txtVisitDateTime" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label7" runat="server" Text="Visit Notes"></asp:Label>
            </td>
            <td>
                <telerik:RadTextBox ID="txtVisitNotes" TextMode="MultiLine" runat="server" Width="500px" Height="300px" MaxLength="800" Resize="Vertical" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="Visit Note required."
                    ValidationGroup="entry" ControlToValidate="txtVisitNotes" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>

    </table>
</asp:Content>
