<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MembershipItemRedemptionDetailItem.ascx.cs" Inherits="Temiang.Avicenna.Module.CRM.MembershipItemRedemptionDetailItem" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumMembershipItemRedemptionItem" runat="server" ValidationGroup="MembershipItemRedemptionItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="MembershipItemRedemptionItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%" valign="top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemReedemID" runat="server" Text="Item Reedem"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboItemReedemID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboItemReedemID_ItemDataBound"
                            OnItemsRequested="cboItemReedemID_ItemsRequested" OnSelectedIndexChanged="cboItemReedemID_SelectedIndexChanged">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemReedemName")%>
                                </b>
                                <br />
                                Group :
                                    <%# DataBinder.Eval(Container.DataItem, "ItemReedemGroup")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 10 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="rfvItemReedemID" runat="server" ErrorMessage="Item Reedem required."
                            ControlToValidate="cboItemReedemID" SetFocusOnError="True" ValidationGroup="MembershipItemRedemptionItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemReedemGroup" runat="server" Text="Group"></asp:Label></td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtItemReedemGroup" runat="server" Width="300px" ReadOnly="true" /></td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPointsUsed" runat="server" Text="Points Used"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtPointsUsed" runat="server" Width="100px" NumberFormat-DecimalDigits="0" ReadOnly="true" />
                    </td>
                    <td width="20px">
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtQty" runat="server" Width="100px" NumberFormat-DecimalDigits="0" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvQty" runat="server" ErrorMessage="Quantity required."
                            ControlToValidate="txtQty" SetFocusOnError="True" ValidationGroup="MembershipItemRedemptionItem"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="entry" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="MembershipItemRedemptionItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="MembershipItemRedemptionItem" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
            </table>
        </td>
        <td style="width=50%" valign="top">
            <table width="100%">
            </table>
        </td>
    </tr>

</table>
