<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkOrderRealizationItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.AssetManagement.WorkOrderRealizationItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumAssetWorkOrderItem" runat="server" ValidationGroup="AssetWorkOrderItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="AssetWorkOrderItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="vertical-align: top">
            <table width="100%">
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblSequenceNo" runat="server" Text="Sequence No" />
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtSequenceNo" runat="server" Width="100px" MaxLength="3"
                            Enabled="false" Text="d" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSequenceNo" runat="server" ErrorMessage="Sequence No required."
                            ControlToValidate="txtSequenceNo" SetFocusOnError="True" ValidationGroup="ItemTransactionItem"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr runat="server" id="trIsInventory">
                    <td class="label" />
                    <td class="entry">
                        <asp:CheckBox ID="chkIsMasterItem" runat="server" Text="Master Item" OnCheckedChanged="chkIsMasterItem_Changed"
                            AutoPostBack="true" />
                        <asp:CheckBox ID="chkIsInventoryItem" runat="server" Text="Inventory Item" />
                    </td>
                    <td width="20px" />
                    <td />
                    <td>
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="label" />
                    <td class="entry">
                        <asp:CheckBox ID="chkIsGeneratePrDr" runat="server" Text="Generate PR/DR" />
                        <asp:CheckBox ID="chkIsGenerateIr" runat="server" Text="Generate IR" />
                    </td>
                    <td width="20px" />
                    <td />
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboItemID" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboItemID_ItemDataBound"
                            OnSelectedIndexChanged="cboItemID_SelectedIndexChanged" OnItemsRequested="cboItemID_ItemsRequested"
                            Enabled="False">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                                    &nbsp;(<%# DataBinder.Eval(Container.DataItem, "ItemID")%>) </b>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                        <telerik:RadTextBox ID="txtDescription" runat="server" Width="300px" TextMode="MultiLine"
                            ReadOnly="True" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr runat="server" id="trSpecification">
                    <td class="label">
                        <asp:Label ID="lblSpecification" runat="server" Text="Specification"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtSpecification" runat="server" Width="300px" MaxLength="250" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblQuantity" runat="server" Text="Qty Request" />
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtQuantity" runat="server" Width="100px" MaxLength="10"
                            MinValue="0" NumberFormat-DecimalDigits="2" AutoPostBack="True" OnTextChanged="txtQuantity_TextChanged" />
                        <telerik:RadComboBox ID="cboSRItemUnit" runat="server" Width="100px" Enabled="False" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ErrorMessage="Quantity required."
                            ControlToValidate="txtQuantity" SetFocusOnError="True" ValidationGroup="AssetWorkOrderItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblQuantityRealization" runat="server" Text="Qty Realization" />
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtQuantityRealization" runat="server" Width="100px"
                            MaxLength="10" MinValue="0" NumberFormat-DecimalDigits="2" />
                        <telerik:RadComboBox ID="cboSRItemUnitRealization" runat="server" Width="100px" Enabled="False" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Quantity required."
                            ControlToValidate="txtQuantity" SetFocusOnError="True" ValidationGroup="AssetWorkOrderItem"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblConversionFactor" runat="server" Text="Conversion Factor" />
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtConversionFactor" runat="server" Width="100px"
                            MinValue="1" Enabled="False" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvConversionFactor" runat="server" ErrorMessage="Conversion Factor required."
                            ControlToValidate="txtConversionFactor" SetFocusOnError="True" ValidationGroup="AssetWorkOrderItem"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblPrice" runat="server" Text="Price" />
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtPrice" runat="server" Width="100px" MaxLength="16"
                            MinValue="0" />
                        <telerik:RadNumericTextBox ID="txtCostPrice" runat="server" Width="100px" MaxLength="16"
                            MinValue="0" />
                    </td>
                    <td width="20px" />
                    <td />
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="AssetWorkOrderItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="AssetWorkOrderItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            OnClick="btnCancel_ButtonClick" CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table style="width: 100%">
                <tr>
                    <td class="labelcaption" colspan="4">
                        <asp:Label ID="Label1" runat="server" Text="Stock Information" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr height="30">
                    <td class="label">
                        <asp:Label ID="Label6" runat="server" Text="Balance"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtStockBalance" runat="server" Width="100px" ReadOnly="true"
                            NumberFormat-DecimalDigits="2" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlBudgetPlan" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td class="labelcaption" colspan="4">
                            <asp:Label ID="Label5" runat="server" Text="Budget Plan Information" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr height="30">
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="Quota"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtQuota" runat="server" Width="100px" ReadOnly="true"
                                NumberFormat-DecimalDigits="2" />
                            <asp:Label ID="lblQuota" runat="server" Text="" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr height="30">
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Offered Qty"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtQtyOffered" runat="server" Width="100px" ReadOnly="true"
                                NumberFormat-DecimalDigits="2" />
                            <asp:Label ID="lblQtyOffered" runat="server" Text="" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr height="30">
                        <td class="label">
                            <asp:Label ID="Label4" runat="server" Text="Balance"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtBalance" runat="server" Width="100px" ReadOnly="true"
                                NumberFormat-DecimalDigits="2" />
                            <asp:Label ID="lblBalance" runat="server" Text="" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
