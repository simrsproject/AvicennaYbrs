<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="MembershipItemRedeemDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.CRM.Master.MembershipItemRedeemDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemReedemID" runat="server" Text="Item Reedem ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemReedemID" runat="server" Width="100px" MaxLength="10" ReadOnly="true" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvItemReedemID" runat="server" ErrorMessage="Item Reedem ID required."
                                ValidationGroup="entry" ControlToValidate="txtItemReedemID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemReedemName" runat="server" Text="Item Reedem Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemReedemName" runat="server" Width="300px" MaxLength="200" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvItemReedemName" runat="server" ErrorMessage="Item Reedem Name required."
                                ValidationGroup="entry" ControlToValidate="txtItemReedemName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRItemReedemGroup" runat="server" Text="Item Reedem Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRItemReedemGroup" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRItemReedemGroup" runat="server" ErrorMessage="Item Reedem Group required."
                                ValidationGroup="entry" ControlToValidate="cboSRItemReedemGroup" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPointsUsed" runat="server" Text="Points Used"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPointsUsed" runat="server" Width="100px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvPointsUsed" runat="server" ErrorMessage="Points Used required."
                                ValidationGroup="entry" ControlToValidate="txtPointsUsed" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td width="20"></td>
                    </tr>

                </table>
            </td>
            <td style="width: 50%" valign="top">
                <table>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
