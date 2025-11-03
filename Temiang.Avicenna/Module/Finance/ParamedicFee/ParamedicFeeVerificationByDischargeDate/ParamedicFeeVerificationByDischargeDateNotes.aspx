<%@ Page Title="Notes" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="ParamedicFeeVerificationByDischargeDateNotes.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.ParamedicFee.ParamedicFeeVerificationByDischargeDateNotes" %>
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
                <asp:Label ID="Label2" runat="server" Text="Notes"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNotes" runat="server" Width="250" Height="50" TextMode="MultiLine">
                </telerik:RadTextBox>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvNotes" runat="server" ErrorMessage="Note is required."
                    ValidationGroup="entry" ControlToValidate="txtNotes" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
</asp:Content>