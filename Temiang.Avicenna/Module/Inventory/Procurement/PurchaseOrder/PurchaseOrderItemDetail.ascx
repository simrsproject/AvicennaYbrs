<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrderItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Inventory.Procurement.PurchaseOrderItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemTransactionItem" runat="server" ValidationGroup="ItemTransactionItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemTransactionItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpgDetail" SelectedIndex="0">
    <Tabs>
        <telerik:RadTab runat="server" Text="Ordering Detail" PageViewID="pgvSeviceUnitVisitType"
            Selected="true" />
        <telerik:RadTab runat="server" Text="Balance Info" PageViewID="pgvServiceUnitParamedic" />
        <telerik:RadTab runat="server" Text="Purchase Info" PageViewID="pgvSupplierItem" />
        <telerik:RadTab runat="server" Text="Pricing Info" PageViewID="pgvSupplierPrice" />
    </Tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="mpgDetail" runat="server" SelectedIndex="0" BorderStyle="Solid"
    BorderColor="gray">
    <telerik:RadPageView ID="pgvSeviceUnitVisitType" runat="server">
        <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr style="display: none">
                            <td class="label">
                                <asp:Label ID="lblSequenceNo" runat="server" Text="Sequence No" />
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtSequenceNo" runat="server" Width="100px" MaxLength="3"
                                    Enabled="false" Text="d" />
                                <telerik:RadTextBox ID="txtReferenceSequenceNo" runat="server" Width="100px" Enabled="false"
                                    Text="r" />
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
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblItemID" runat="server" Text="Item" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboItemID" Width="300px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboItemID_ItemDataBound"
                                    OnItemsRequested="cboItemID_ItemsRequested" OnSelectedIndexChanged="cboItemID_SelectedIndexChanged"
                                    ValidationGroup="other">
                                    <ItemTemplate>
                                        <b>
                                            <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                                            &nbsp;(<%# DataBinder.Eval(Container.DataItem, "ItemID")%>) </b>
                                        <br />
                                        Stock :&nbsp;<%# DataBinder.Eval(Container.DataItem, "Balance", "{0:n2}")%>
                                        &nbsp;Min :&nbsp;<%# DataBinder.Eval(Container.DataItem, "Minimum", "{0:n2}")%>
                                        &nbsp;Max :&nbsp;<%# DataBinder.Eval(Container.DataItem, "Maximum", "{0:n2}")%>
                                        &nbsp;<%# DataBinder.Eval(Container.DataItem, "Unit") %>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                                <telerik:RadTextBox ID="txtDescription" runat="server" Width="300px" TextMode="MultiLine" />
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                        <tr runat="server" id="trFabricID">
                            <td class="label">
                                <asp:Label ID="lblFabricID" runat="server" Text="Factory" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboFabricID" Width="300px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" OnItemDataBound="cboFabricID_ItemDataBound"
                                    OnItemsRequested="cboFabricID_ItemsRequested" />
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblQuantity" runat="server" Text="Quantity" />
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtQuantity" runat="server" Width="100px" MaxLength="10"
                                    MinValue="0" NumberFormat-DecimalDigits="2" />
                                <telerik:RadComboBox ID="cboSRItemUnit" runat="server" Width="100px" AutoPostBack="True"
                                    OnSelectedIndexChanged="cboSRItemUnit_SelectedIndexChanged" />
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ErrorMessage="Quantity required."
                                    ControlToValidate="txtQuantity" SetFocusOnError="True" ValidationGroup="ItemTransactionItem"
                                    Width="100%">
                                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
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
                                    MinValue="1" ReadOnly="true" OnTextChanged="txtConversionFactor_TextChanged"
                                    AutoPostBack="true" />
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvConversionFactor" runat="server" ErrorMessage="Conversion Factor required."
                                    ControlToValidate="txtConversionFactor" SetFocusOnError="True" ValidationGroup="ItemTransactionItem"
                                    Width="100%">
                                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPrice" runat="server" Text="Price" />
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtPrice" runat="server" Width="100px" MaxLength="16"
                                    MinValue="0" />
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSpecs" runat="server" Text="Specification" />
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtSpecs" runat="server" Width="300px" TextMode="MultiLine" />
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                        
                    </table>
                </td>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label"></td>
                            <td class="entry">
                                <asp:CheckBox ID="chkIsDiscountInPercent" runat="server" Text="Discount In Percent"
                                    Enabled="true" OnCheckedChanged="chkIsDiscountInPercent_CheckedChanged" AutoPostBack="true"
                                    Checked="True" />
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDiscount1Percentage" runat="server" Text="Discount 1 (%)" />
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtDiscount1Percentage" runat="server" Type="Percent"
                                    Width="100px" MaxLength="5" MaxValue="100.00" MinValue="0" />
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDiscount2Percentage" runat="server" Text="Discount 2 (%)" />
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtDiscount2Percentage" runat="server" Type="Percent"
                                    Width="100px" MaxLength="5" MaxValue="100.00" MinValue="0" />
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDiscountAmount" runat="server" Text="Discount Amount" />
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtDiscountAmount" runat="server" Width="100px" MaxLength="16"
                                    MinValue="0" />
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                        <tr height="24px">
                            <td class="label" />
                            <td class="entry">
                                <asp:CheckBox ID="chkIsBonusItem" runat="server" Text="Bonus" OnCheckedChanged="chkisBonus_Changed"
                                    AutoPostBack="true" />
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                        <tr height="24px">
                            <td class="label" />
                            <td class="entry">
                                <asp:CheckBox ID="chkIsTaxable" runat="server" Text="Taxable" />
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                        <tr height="24px" style="display: none">
                            <td class="label" />
                            <td class="entry">
                                <asp:CheckBox ID="chkIsClosed" runat="server" Text="Closed" Enabled="false" />
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                        <tr style="display: none">
                            <td class="label">
                                <asp:Label ID="lblQtyPending" runat="server" Text="Quantity Pending"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtQtyPending" runat="server" Width="100px" MaxLength="10"
                                    MinValue="0" NumberFormat-DecimalDigits="2" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ItemTransactionItem"
                                    Visible='<%# !(DataItem is GridInsertionObject) %>' />
                                <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                                    ValidationGroup="ItemTransactionItem" Visible='<%# DataItem is GridInsertionObject %>' />
                                &nbsp;
                                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                    OnClick="btnCancel_ButtonClick" CommandName="Cancel" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </telerik:RadPageView>
    <telerik:RadPageView runat="server" ID="pgvServiceUnitParamedic">
        <telerik:RadGrid ID="grdItemBalance" runat="server" AutoGenerateColumns="False" ShowGroupPanel="false" OnNeedDataSource="grdItemBalance_NeedDataSource"
            AllowPaging="True" AllowSorting="True" GridLines="None">
            <MasterTableView DataKeyNames="ItemID">
                <Columns>
                    <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                        SortExpression="ItemID">
                        <HeaderStyle HorizontalAlign="Left" Width="90px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                        SortExpression="ItemName">
                        <HeaderStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="LocationName" HeaderText="Location" UniqueName="LocationName"
                        SortExpression="LocationName">
                    </telerik:GridBoundColumn>
                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Minimum" HeaderText="Minimum"
                        UniqueName="Minimum" SortExpression="Minimum" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                    </telerik:GridNumericColumn>
                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Maximum" HeaderText="Maximum"
                        UniqueName="Maximum" SortExpression="Maximum" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                    </telerik:GridNumericColumn>
                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Balance" HeaderText="Balance"
                        UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                    </telerik:GridNumericColumn>
                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Booking" HeaderText="Booking"
                        UniqueName="Booking" SortExpression="Booking" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="false">
                    </telerik:GridNumericColumn>
                    <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRItemUnit" HeaderText="Unit"
                        UniqueName="SRItemUnit" SortExpression="SRItemUnit">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings AllowDragToGroup="true" EnableRowHoverStyle="true" AllowExpandCollapse="true">
            </ClientSettings>
        </telerik:RadGrid>
    </telerik:RadPageView>
    <telerik:RadPageView runat="server" ID="pgvSupplierItem">
        <telerik:RadGrid ID="grdSupplierItem" runat="server" AutoGenerateColumns="False"
            ShowGroupPanel="false" AllowPaging="True" AllowSorting="True" GridLines="None" OnNeedDataSource="grdSupplierItem_NeedDataSource">
            <MasterTableView DataKeyNames="SupplierID">
                <Columns>
                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SupplierID" HeaderText="Supplier ID"
                        UniqueName="SupplierID" SortExpression="SupplierID" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn DataField="SupplierName" HeaderText="Supplier Name" UniqueName="SupplierName"
                        SortExpression="SupplierName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="DrugDistributionLicenseNo"
                        HeaderText="Drug Distribution License No" UniqueName="DrugDistributionLicenseNo"
                        SortExpression="DrugDistributionLicenseNo" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRPurchaseUnit" HeaderText="Purchase Unit"
                        UniqueName="SRPurchaseUnit" SortExpression="SRPurchaseUnit" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="ConversionFactor"
                        HeaderText="Conversion Factor" UniqueName="ConversionFactor" SortExpression="ConversionFactor"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="PriceInPurchaseUnit"
                        HeaderText="Price In Purchase Unit" UniqueName="PriceInPurchaseUnit" SortExpression="PriceInPurchaseUnit"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="PurchaseDiscount1"
                        HeaderText="Discount #1" UniqueName="PurchaseDiscount1" SortExpression="PurchaseDiscount1"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2} %" />
                    <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="PurchaseDiscount2"
                        HeaderText="Discount #2" UniqueName="PurchaseDiscount2" SortExpression="PurchaseDiscount2"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2} %" />
                    <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsActive" HeaderText="Active"
                        UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" Visible="False" />
                    <telerik:GridDateTimeColumn DataField="LastUpdateDateTime" HeaderText="Last Update"
                        UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime">
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LastUpdateByUserID"
                        HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="False" />
                </Columns>
            </MasterTableView>
            <ClientSettings AllowDragToGroup="true" EnableRowHoverStyle="true" AllowExpandCollapse="true">
            </ClientSettings>
        </telerik:RadGrid>
    </telerik:RadPageView>
    <telerik:RadPageView runat="server" ID="pgvSupplierPrice">
        <telerik:RadGrid ID="grdSupplierPrice" runat="server" AutoGenerateColumns="False"
            ShowGroupPanel="false" AllowPaging="True" AllowSorting="True" GridLines="None" OnNeedDataSource="grdSupplierPrice_NeedDataSource">
            <MasterTableView DataKeyNames="SupplierID">
                <Columns>
                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SupplierID" HeaderText="Supplier ID"
                        UniqueName="SupplierID" SortExpression="SupplierID" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn DataField="SupplierName" HeaderText="Supplier Name" UniqueName="SupplierName"
                        SortExpression="SupplierName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRPurchaseUnit" HeaderText="Purchase Unit"
                        UniqueName="SRPurchaseUnit" SortExpression="SRPurchaseUnit" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="ConversionFactor"
                        HeaderText="Conversion Factor" UniqueName="ConversionFactor" SortExpression="ConversionFactor"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="PriceInPurchaseUnit"
                        HeaderText="Price In Purchase Unit" UniqueName="PriceInPurchaseUnit" SortExpression="PriceInPurchaseUnit"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="PurchaseDiscount1"
                        HeaderText="Discount #1" UniqueName="PurchaseDiscount1" SortExpression="PurchaseDiscount1"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2} %" />
                    <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="PurchaseDiscount2"
                        HeaderText="Discount #2" UniqueName="PurchaseDiscount2" SortExpression="PurchaseDiscount2"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2} %" />
                    <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsActive" HeaderText="Active"
                        UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" Visible="False" />
                    <telerik:GridDateTimeColumn DataField="LastUpdateDateTime" HeaderText="Last Update"
                        UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime">
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LastUpdateByUserID"
                        HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="False" />
                </Columns>
            </MasterTableView>
            <ClientSettings AllowDragToGroup="true" EnableRowHoverStyle="true" AllowExpandCollapse="true">
            </ClientSettings>
        </telerik:RadGrid>
    </telerik:RadPageView>

</telerik:RadMultiPage>