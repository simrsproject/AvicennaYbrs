<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReturnDistributionDetailItem.ascx.cs" Inherits="Temiang.Avicenna.Module.Laundry.Transaction.ReturnDistributionDetailItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumLaundryReturnDistributionItem" runat="server" ValidationGroup="LaundryReturnDistributionItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="LaundryReturnDistributionItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSeqNo" runat="server" Text="Sequence No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtSeqNo" runat="server" Width="100px" MaxLength="5"
                            Enabled="false" Text="" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSeqNo" runat="server" ErrorMessage="Sequence No required."
                            ControlToValidate="txtSeqNo" SetFocusOnError="True" ValidationGroup="LaundryReturnDistributionItem"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboItemID" Height="190px" Width="350px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" OnItemDataBound="cboItemID_ItemDataBound" OnItemsRequested="cboItemID_ItemsRequested"
                            AutoPostBack="True" OnSelectedIndexChanged="cboItemID_SelectedIndexChanged">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr height="30">
                    <td class="label">
                        <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtQty" runat="server" Width="100px" MaxLength="10"
                                        MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="0" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <telerik:RadComboBox runat="server" ID="cboSRItemUnit" Width="100px" AllowCustomText="true"
                                        Filter="Contains">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ErrorMessage="Quantity required."
                            ControlToValidate="txtQty" SetFocusOnError="True" ValidationGroup="LaundryReturnDistributionItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvSRItemUnit" runat="server" ErrorMessage="Unit required."
                            ControlToValidate="cboSRItemUnit" SetFocusOnError="True" ValidationGroup="LaundryReturnDistributionItem"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="LaundryReturnDistributionItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="LaundryReturnDistributionItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
            </table>
        </td>
    </tr>
</table>