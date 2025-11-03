<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemSubGroupDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ItemSubGroupDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumAppStandardReferenceItem" runat="server" ValidationGroup="AppStandardReferenceItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="AppStandardReferenceItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="Label1" runat="server" Text="ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtItemID" runat="server" Width="300px" ReadOnly="true">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item ID required."
                            ControlToValidate="txtItemID" SetFocusOnError="True" ValidationGroup="AppStandardReferenceItem"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label2" runat="server" Text="Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ErrorMessage="Item Name required."
                            ControlToValidate="txtItemName" SetFocusOnError="True" ValidationGroup="AppStandardReferenceItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkIsActive" Text="Active" runat="server" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="AppStandardReferenceItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="AppStandardReferenceItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
        </td>
    </tr>
</table>