<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GuarantorItemGroupProductMarginDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.GuarantorItemGroupProductMarginDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumGuarantorItemGroupProductMargin" runat="server" ValidationGroup="GuarantorItemGroupProductMargin" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="GuarantorItemGroupProductMargin"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemGroupID" runat="server" Text="Item Group"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboItemGroupID" Height="190px" Width="300px"
                            EnableLoadOnDemand="true" HighlightTemplatedItems="true" OnItemDataBound="cboItemGroupID_ItemDataBound"
                            OnItemsRequested="cboItemGroupID_ItemsRequested">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ItemGroupName")%>
                                <br />
                                Item Type : <%# DataBinder.Eval(Container.DataItem, "ItemTypeName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 10 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvItemGroupID" runat="server" ErrorMessage="Item Group required."
                            ControlToValidate="cboItemGroupID" SetFocusOnError="True" ValidationGroup="GuarantorItemGroupProductMargin"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblMargin" runat="server" Text="Margin"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtMarginPercentage" runat="server" Type="Percent"
                                        Width="100px" MaxLength="5" MaxValue="999.99" MinValue="0" />
                                </td>
                                <td>&nbsp;&nbsp;
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboMarginID" runat="server" Width="190px">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvMarginPercentage" runat="server" ErrorMessage="Margin Percentage required."
                            ValidationGroup="entry" ControlToValidate="txtMarginPercentage" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="GuarantorItemGroupProductMargin"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="GuarantorItemGroupProductMargin" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" style="vertical-align: top"></td>
    </tr>
</table>
