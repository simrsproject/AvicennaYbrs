<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="SupplierDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.SupplierDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/CustomControl/AddressCtl.ascx" TagName="Address" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSupplierID" runat="server" Text="Supplier ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSupplierID" runat="server" Width="100px" MaxLength="10" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSupplierID" runat="server" ErrorMessage="Supplier ID required."
                                ValidationGroup="entry" ControlToValidate="txtSupplierID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSupplierName" runat="server" Text="Supplier Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSupplierName" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSupplierName" runat="server" ErrorMessage="Supplier Name required."
                                ValidationGroup="entry" ControlToValidate="txtSupplierName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblShortName" runat="server" Text="Short Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtShortName" runat="server" Width="100px" MaxLength="35" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label16" runat="server" Text="Branch"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBranch" runat="server" Width="300px" MaxLength="255" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSupplierType" runat="server" Text="Supplier Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRSupplierType" runat="server" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSupplierType" runat="server" ErrorMessage="Supplier Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRSupplierType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblContractNumber" runat="server" Text="Contract Number"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtContractNumber" runat="server" Width="300px" MaxLength="50" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblContractStart" runat="server" Text="Contract Period"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtContractStart" runat="server" Width="100px" />
                                    </td>
                                    <td style="width: 15px">to
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtContractEnd" runat="server" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblContractSummary" runat="server" Text="Contract Summary"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtContractSummary" runat="server" Width="300px" TextMode="MultiLine"
                                Height="80px" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtContactPerson" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvContactPerson" runat="server" ErrorMessage="Contact Person required."
                                ValidationGroup="entry" ControlToValidate="txtContactPerson" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTaxRegistrationNo" runat="server" Text="Tax Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTaxRegistrationNo" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="License No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPBFLicenseNo" runat="server" Width="300px" MaxLength="50" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvPBFLicenseNo" runat="server" ErrorMessage="License No required."
                                ValidationGroup="entry" ControlToValidate="txtPBFLicenseNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label4" runat="server" Text="License Valid Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="dtpPBFLicenseValidDate" runat="server" Width="100px" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="Label5" runat="server" Text="Bank Account Number"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBankAccountNo" runat="server" Width="300px" MaxLength="50" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="Label6" runat="server" Text="Bank Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBankName" runat="server" Width="300px" MaxLength="50" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTermOfPayment" runat="server" Text="Term Of Payment"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtTermOfPayment" runat="server" Width="100px" MaxLength="10"
                                            MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="0" />
                                    </td>
                                    <td>&nbsp;&nbsp;Day(s)
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label9" runat="server" Text="Aging Date Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRApAgingDateType" runat="server" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRApAgingDateType" runat="server" ErrorMessage="Aging date type required."
                                ValidationGroup="entry" ControlToValidate="cboSRApAgingDateType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblLeadTime" runat="server" Text="Lead Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtLeadTime" runat="server" Width="100px" />
                                    </td>
                                    <td>&nbsp;&nbsp;Day(s)
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblCreditLimit" runat="server" Text="Credit Limit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtCreditLimit" runat="server" Width="100px" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTax" runat="server" Text="Tax Percentage"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTaxPercentage" runat="server" Width="100px" MinValue="0" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkIsPKP" runat="server" Text="PKP" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsUsingRounding" runat="server" Text="Using Rounding" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Address" Selected="True" PageViewID="pgAddress">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Related Item" PageViewID="pgSupplier">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Bank" PageViewID="pgBank">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Location For Consignment" PageViewID="pgLocation">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="COA" PageViewID="pgCoa">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderStyle="Solid" SelectedIndex="0"
                    BorderColor="Gray">
                    <telerik:RadPageView ID="pgAddress" runat="server">
                        <uc1:Address ID="ctlAddress" runat="server" />
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pgSupplier" runat="server">
                        <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="RadMultiPage2">
                            <Tabs>
                                <telerik:RadTab runat="server" Text="Item Medical" Selected="True" PageViewID="pgItemMedical">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" Text="Item Non Medical" PageViewID="pgItemNonMedical">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" Text="Item Kitchen" PageViewID="pgItemKicthen">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="RadMultiPage2" runat="server" BorderStyle="Solid" SelectedIndex="0"
                            BorderColor="Gray">
                            <telerik:RadPageView ID="pgItemMedical" runat="server">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 50%; vertical-align: top;">
                                            <table width="100%">
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblFilterItem" runat="server" Text="Item ID / Item Name"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadTextBox ID="txtFilterItem" runat="server" Width="300px" MaxLength="100" />
                                                    </td>
                                                    <td width="20">
                                                        <asp:ImageButton ID="btnFilterItem" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                                            OnClick="btnFilterItem_Click" ToolTip="Search" />
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <telerik:RadGrid ID="grdSupplierItem" AllowPaging="true" PageSize="20" runat="server"
                                    OnNeedDataSource="grdSupplierItem_NeedDataSource" AutoGenerateColumns="False"
                                    GridLines="None" OnUpdateCommand="grdSupplierItem_UpdateCommand" OnDeleteCommand="grdSupplierItem_DeleteCommand"
                                    OnInsertCommand="grdSupplierItem_InsertCommand">
                                    <HeaderContextMenu>
                                    </HeaderContextMenu>
                                    <MasterTableView CommandItemDisplay="None" DataKeyNames="SupplierID, ItemID">
                                        <Columns>
                                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                <HeaderStyle Width="35px" />
                                                <ItemStyle CssClass="MyImageButton" />
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                                                UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                                SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="DrugDistributionLicenseNo"
                                                HeaderText="Drug Distribution License No" UniqueName="DrugDistributionLicenseNo"
                                                SortExpression="DrugDistributionLicenseNo" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRPurchaseUnit" HeaderText="Purchase Unit"
                                                UniqueName="SRPurchaseUnit" SortExpression="SRPurchaseUnit" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="PriceInPurchaseUnit"
                                                HeaderText="Price In Purchase Unit" UniqueName="PriceInPurchaseUnit" SortExpression="PriceInPurchaseUnit"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="ConversionFactor"
                                                HeaderText="Conversion Factor" UniqueName="ConversionFactor" SortExpression="ConversionFactor"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="PurchaseDiscount1"
                                                HeaderText="Discount #1" UniqueName="PurchaseDiscount1" SortExpression="PurchaseDiscount1"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2} %" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="PurchaseDiscount2"
                                                HeaderText="Discount #2" UniqueName="PurchaseDiscount2" SortExpression="PurchaseDiscount2"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2} %" />
                                            <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsActive" HeaderText="Active"
                                                UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <telerik:GridDateTimeColumn DataField="LastUpdateDateTime" HeaderText="Last Update"
                                                UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime">
                                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridDateTimeColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LastUpdateByUserID"
                                                HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <EditFormSettings UserControlName="SupplierItemDetail.ascx" EditFormType="WebUserControl">
                                            <EditColumn UniqueName="SupplierItemDetailCommand">
                                            </EditColumn>
                                        </EditFormSettings>
                                    </MasterTableView>
                                    <FilterMenu>
                                    </FilterMenu>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Resizing AllowColumnResize="True" />
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="pgItemNonMedical" runat="server">
                                   <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 50%; vertical-align: top;">
                                            <table width="100%">
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblFilterItemNonMedical" runat="server" Text="Item ID / Item Name"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadTextBox ID="txtFilterItemNonMedical" runat="server" Width="300px" MaxLength="100" />
                                                    </td>
                                                    <td width="20">
                                                        <asp:ImageButton ID="btnFilterItemNonMedical" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                                            OnClick="btnFilterItem_Click" ToolTip="Search" />
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <telerik:RadGrid ID="grdSupplierItemNonMedical" AllowPaging="true" PageSize="20" runat="server"
                                    OnNeedDataSource="grdSupplierItemNonMedical_NeedDataSource" AutoGenerateColumns="False"
                                    GridLines="None" OnUpdateCommand="grdSupplierItemNonMedical_UpdateCommand" OnDeleteCommand="grdSupplierItemNonMedical_DeleteCommand"
                                    OnInsertCommand="grdSupplierItemNonMedical_InsertCommand">
                                    <HeaderContextMenu>
                                    </HeaderContextMenu>
                                    <MasterTableView CommandItemDisplay="None" DataKeyNames="SupplierID, ItemID">
                                        <Columns>
                                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                <HeaderStyle Width="35px" />
                                                <ItemStyle CssClass="MyImageButton" />
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                                                UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                                SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRPurchaseUnit" HeaderText="Purchase Unit"
                                                UniqueName="SRPurchaseUnit" SortExpression="SRPurchaseUnit" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="PriceInPurchaseUnit"
                                                HeaderText="Price In Purchase Unit" UniqueName="PriceInPurchaseUnit" SortExpression="PriceInPurchaseUnit"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="ConversionFactor"
                                                HeaderText="Conversion Factor" UniqueName="ConversionFactor" SortExpression="ConversionFactor"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="PurchaseDiscount1"
                                                HeaderText="Discount #1" UniqueName="PurchaseDiscount1" SortExpression="PurchaseDiscount1"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2} %" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="PurchaseDiscount2"
                                                HeaderText="Discount #2" UniqueName="PurchaseDiscount2" SortExpression="PurchaseDiscount2"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2} %" />
                                            <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsActive" HeaderText="Active"
                                                UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <telerik:GridDateTimeColumn DataField="LastUpdateDateTime" HeaderText="Last Update"
                                                UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime">
                                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridDateTimeColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LastUpdateByUserID"
                                                HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <EditFormSettings UserControlName="SupplierItemDetailNonMedical.ascx" EditFormType="WebUserControl">
                                            <EditColumn UniqueName="SupplierItemNonMedicalDetailCommand">
                                            </EditColumn>
                                        </EditFormSettings>
                                    </MasterTableView>
                                    <FilterMenu>
                                    </FilterMenu>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Resizing AllowColumnResize="True" />
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="pgItemKicthen" runat="server">
                                   <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 50%; vertical-align: top;">
                                            <table width="100%">
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblFilterItemKitchen" runat="server" Text="Item ID / Item Name"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadTextBox ID="txtFilterItemKitchen" runat="server" Width="300px" MaxLength="100" />
                                                    </td>
                                                    <td width="20">
                                                        <asp:ImageButton ID="btnFilterItemkitchen" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                                            OnClick="btnFilterItem_Click" ToolTip="Search" />
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <telerik:RadGrid ID="grdSupplierItemKitchen" AllowPaging="true" PageSize="20" runat="server"
                                    OnNeedDataSource="grdSupplierItemKitchen_NeedDataSource" AutoGenerateColumns="False"
                                    GridLines="None" OnUpdateCommand="grdSupplierItemKitchen_UpdateCommand" OnDeleteCommand="grdSupplierItemKitchen_DeleteCommand"
                                    OnInsertCommand="grdSupplierItemKitchen_InsertCommand">
                                    <HeaderContextMenu>
                                    </HeaderContextMenu>
                                    <MasterTableView CommandItemDisplay="None" DataKeyNames="SupplierID, ItemID">
                                        <Columns>
                                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                <HeaderStyle Width="35px" />
                                                <ItemStyle CssClass="MyImageButton" />
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                                                UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                                SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRPurchaseUnit" HeaderText="Purchase Unit"
                                                UniqueName="SRPurchaseUnit" SortExpression="SRPurchaseUnit" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="PriceInPurchaseUnit"
                                                HeaderText="Price In Purchase Unit" UniqueName="PriceInPurchaseUnit" SortExpression="PriceInPurchaseUnit"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="ConversionFactor"
                                                HeaderText="Conversion Factor" UniqueName="ConversionFactor" SortExpression="ConversionFactor"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="PurchaseDiscount1"
                                                HeaderText="Discount #1" UniqueName="PurchaseDiscount1" SortExpression="PurchaseDiscount1"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2} %" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="PurchaseDiscount2"
                                                HeaderText="Discount #2" UniqueName="PurchaseDiscount2" SortExpression="PurchaseDiscount2"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2} %" />
                                            <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsActive" HeaderText="Active"
                                                UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <telerik:GridDateTimeColumn DataField="LastUpdateDateTime" HeaderText="Last Update"
                                                UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime">
                                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridDateTimeColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LastUpdateByUserID"
                                                HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <EditFormSettings UserControlName="SupplierItemDetailKitchen.ascx" EditFormType="WebUserControl">
                                            <EditColumn UniqueName="SupplierItemKitchenDetailCommand">
                                            </EditColumn>
                                        </EditFormSettings>
                                    </MasterTableView>
                                    <FilterMenu>
                                    </FilterMenu>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Resizing AllowColumnResize="True" />
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pgBank" runat="server">
                        <telerik:RadGrid ID="grdSupplierBank" AllowPaging="true" PageSize="20" runat="server"
                            OnNeedDataSource="grdSupplierBank_NeedDataSource" AutoGenerateColumns="False"
                            GridLines="None" OnUpdateCommand="grdSupplierBank_UpdateCommand" OnDeleteCommand="grdSupplierBank_DeleteCommand"
                            OnInsertCommand="grdSupplierBank_InsertCommand">
                            <HeaderContextMenu>
                            </HeaderContextMenu>
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="SupplierID, BankAccountNo">
                                <Columns>
                                    <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                        <HeaderStyle Width="35px" />
                                        <ItemStyle CssClass="MyImageButton" />
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="BankAccountNo" HeaderText="Bank Account No"
                                        UniqueName="BankAccountNo" SortExpression="BankAccountNo" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="BankName" HeaderText="Bank Name" UniqueName="BankName"
                                        SortExpression="BankName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsActive" HeaderText="Active"
                                        UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridDateTimeColumn DataField="LastUpdateDateTime" HeaderText="Last Update"
                                        UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime">
                                        <HeaderStyle HorizontalAlign="Center" Width="130px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LastUpdateByUserID"
                                        HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                        ButtonType="ImageButton" ConfirmText="Delete this row?">
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <EditFormSettings UserControlName="SupplierBankItemDetail.ascx" EditFormType="WebUserControl">
                                    <EditColumn UniqueName="SupplierBankItemDetailCommand">
                                    </EditColumn>
                                </EditFormSettings>
                            </MasterTableView>
                            <FilterMenu>
                            </FilterMenu>
                            <ClientSettings EnableRowHoverStyle="true">
                                <Resizing AllowColumnResize="True" />
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pgLocation" runat="server">
                        <telerik:RadGrid ID="grdSupplierLocation" AllowPaging="true" PageSize="20" runat="server" OnNeedDataSource="grdSupplierLocation_NeedDataSource"
                            AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdSupplierLocation_UpdateCommand"
                            OnDeleteCommand="grdSupplierLocation_DeleteCommand" OnInsertCommand="grdSupplierLocation_InsertCommand">
                            <HeaderContextMenu>
                            </HeaderContextMenu>
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="SupplierID, LocationID">
                                <Columns>
                                    <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                        <HeaderStyle Width="35px" />
                                        <ItemStyle CssClass="MyImageButton" />
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LocationID" HeaderText="Location ID"
                                        UniqueName="LocationID" SortExpression="LocationID" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="LocationName" HeaderText="Location Name" UniqueName="LocationName"
                                        SortExpression="LocationName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsActive"
                                        HeaderText="Active" UniqueName="IsActive" SortExpression="IsActive"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                        ButtonType="ImageButton" ConfirmText="Delete this row?">
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <EditFormSettings UserControlName="SupplierLocationDetail.ascx" EditFormType="WebUserControl">
                                    <EditColumn UniqueName="SupplierLocationDetailCommand">
                                    </EditColumn>
                                </EditFormSettings>
                            </MasterTableView>
                            <FilterMenu>
                            </FilterMenu>
                            <ClientSettings EnableRowHoverStyle="true">
                                <Resizing AllowColumnResize="True" />
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pgCoa" runat="server">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 50%; vertical-align: top">
                                    <table width="100%">
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="lblChartOfAccountIdAP" runat="server" Text="Chart Of Account A/P"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdAP" Height="190px" Width="300px"
                                                    EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                                    OnSelectedIndexChanged="cboChartOfAccountIdAP_SelectedIndexChanged"
                                                    OnItemDataBound="cboChartOfAccountId_ItemDataBound"
                                                    OnItemsRequested="cboChartOfAccountId_ItemsRequested">
                                                    <ItemTemplate>
                                                        <b>
                                                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                                            &nbsp;-&nbsp;
                                                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                                        </b>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Note : Show max 20 items
                                                    </FooterTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td width="20px">
                                            </td>
                                            <td width="20px"></td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="lblSubledgerIdAP" runat="server" Text="Subledger A/P"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox runat="server" ID="cboSubledgerIdAP" Height="190px" Width="300px"
                                                    EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                                    OnItemDataBound="cboSubledgerId_ItemDataBound"
                                                    OnItemsRequested="cboSubledgerIdAP_ItemsRequested">
                                                    <ItemTemplate>
                                                        <b>
                                                            <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                                            &nbsp;-&nbsp; (<%# DataBinder.Eval(Container.DataItem, "Description")%>) </b>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Note : Show max 20 items
                                                    </FooterTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td width="20px"></td>
                                            <td></td>
                                        </tr>

                                        <tr id="trCOAApNonMedic" runat="server">
                                            <td class="label">
                                                <asp:Label ID="Label10" runat="server" Text="Chart Of Account A/P Non Medic"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdAPNonMedic" Height="190px" Width="300px"
                                                    EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                                    OnSelectedIndexChanged="cboChartOfAccountIdAPNonMedic_SelectedIndexChanged"
                                                    OnItemDataBound="cboChartOfAccountId_ItemDataBound"
                                                    OnItemsRequested="cboChartOfAccountId_ItemsRequested">
                                                    <ItemTemplate>
                                                        <b>
                                                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                                            &nbsp;-&nbsp;
                                                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                                        </b>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Note : Show max 20 items
                                                    </FooterTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td width="20px"></td>
                                            <td width="20px"></td>
                                        </tr>
                                        <tr id="trSlAPNonMedic" runat="server">
                                            <td class="label">
                                                <asp:Label ID="Label11" runat="server" Text="Subledger A/P Non Medic"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox runat="server" ID="cboSubledgerIdAPNonMedic" Height="190px" Width="300px"
                                                    EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                                    OnItemDataBound="cboSubledgerId_ItemDataBound"
                                                    OnItemsRequested="cboSubledgerIdAPNonMedic_ItemsRequested">
                                                    <ItemTemplate>
                                                        <b>
                                                            <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                                            &nbsp;-&nbsp; (<%# DataBinder.Eval(Container.DataItem, "Description")%>) </b>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Note : Show max 20 items
                                                    </FooterTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td width="20px"></td>
                                            <td></td>
                                        </tr>


                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="lblChartOfAccountIdAPInProcess" runat="server" Text="Chart Of Account A/P In Process (Consignment)"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdAPInProcess" Height="190px"
                                                    Width="300px" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                                    OnSelectedIndexChanged="cboChartOfAccountIdAPInProcess_SelectedIndexChanged"
                                                    OnItemDataBound="cboChartOfAccountId_ItemDataBound"
                                                    OnItemsRequested="cboChartOfAccountId_ItemsRequested">
                                                    <ItemTemplate>
                                                        <b>
                                                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                                            &nbsp;-&nbsp;
                                                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                                        </b>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Note : Show max 20 items
                                                    </FooterTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td width="20px"></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="lblSubledgerIdAPInProcess" runat="server" Text="Subledger A/P In Process (Consignment)"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox runat="server" ID="cboSubledgerIdAPInProcess" Height="190px"
                                                    Width="300px" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                                    OnItemDataBound="cboSubledgerId_ItemDataBound"
                                                    OnItemsRequested="cboSubledgerIdAPInProcess_ItemsRequested">
                                                    <ItemTemplate>
                                                        <b>
                                                            <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                                            &nbsp;-&nbsp; (<%# DataBinder.Eval(Container.DataItem, "Description")%>) </b>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Note : Show max 20 items
                                                    </FooterTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td width="20px"></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="lblChartOfAccountAPTemporary" runat="server" Text="Chart Of Account A/P UnInvoice"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox ID="cboChartOfAccountIdAPTemporary" runat="server" Height="190px"
                                                    Width="300px" HighlightTemplatedItems="true" AutoPostBack="true" EnableLoadOnDemand="true"
                                                    OnItemsRequested="cboChartOfAccountId_ItemsRequested" OnItemDataBound="cboChartOfAccountId_ItemDataBound"
                                                    OnSelectedIndexChanged="cboChartOfAccountIdAPTemporary_SelectedIndexChanged">
                                                    <ItemTemplate>
                                                        <b>
                                                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                                            &nbsp;-&nbsp;
                                                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                                        </b>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Note : Show max 20 items
                                                    </FooterTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="lblSubledgerTemporary" runat="server" Text="Subledger A/P UnInvoice"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox runat="server" ID="cboSubledgerIdAPTemporary" Height="190px"
                                                    Width="300px" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                                    OnItemDataBound="cboSubledgerId_ItemDataBound" OnItemsRequested="cboSubledgerIdAPTemporary_ItemsRequested">
                                                    <ItemTemplate>
                                                        <b>
                                                            <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                                            &nbsp;-&nbsp; (<%# DataBinder.Eval(Container.DataItem, "Description")%>) </b>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Note : Show max 20 items
                                                    </FooterTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td width="20px"></td>
                                            <td></td>
                                        </tr>

                                        <tr id="trCOAApTempNonMedic" runat="server">
                                            <td class="label">
                                                <asp:Label ID="Label12" runat="server" Text="Chart Of Account A/P UnInvoice Non Medical"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox ID="cboChartOfAccountIdAPTemporaryNonMedical" runat="server" Height="190px"
                                                    Width="300px" HighlightTemplatedItems="true" AutoPostBack="true" EnableLoadOnDemand="true"
                                                    OnItemsRequested="cboChartOfAccountId_ItemsRequested"
                                                    OnItemDataBound="cboChartOfAccountId_ItemDataBound"
                                                    OnSelectedIndexChanged="cboChartOfAccountIdAPTemporaryNonMedical_SelectedIndexChanged">
                                                    <ItemTemplate>
                                                        <b>
                                                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                                            &nbsp;-&nbsp;
                                                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                                        </b>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Note : Show max 20 items
                                                    </FooterTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr id="trSlApTempNonMedic" runat="server">
                                            <td class="label">
                                                <asp:Label ID="Label13" runat="server" Text="Subledger A/P UnInvoice Non Medical"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox runat="server" ID="cboSubledgerIdAPTemporaryNonMedical" Height="190px"
                                                    Width="300px" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                                    OnItemDataBound="cboSubledgerId_ItemDataBound"
                                                    OnItemsRequested="cboSubledgerIdAPTemporaryNonMedical_ItemsRequested">
                                                    <ItemTemplate>
                                                        <b>
                                                            <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                                            &nbsp;-&nbsp; (<%# DataBinder.Eval(Container.DataItem, "Description")%>) </b>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Note : Show max 20 items
                                                    </FooterTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td width="20px"></td>
                                            <td></td>
                                        </tr>


                                    </table>
                                </td>
                                <td style="width: 50%; vertical-align: top">
                                    <table width="100%">
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="Label1" runat="server" Text="Chart Of Account Cost"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox ID="cboChartOfAccountIdAPCost" runat="server" Height="190px"
                                                    Width="300px" HighlightTemplatedItems="true" AutoPostBack="true" EnableLoadOnDemand="true"
                                                    OnItemsRequested="cboChartOfAccountId_ItemsRequested" OnItemDataBound="cboChartOfAccountId_ItemDataBound"
                                                    OnSelectedIndexChanged="cboChartOfAccountIdAPCost_SelectedIndexChanged">
                                                    <ItemTemplate>
                                                        <b>
                                                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                                            &nbsp;-&nbsp;
                                                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                                        </b>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Note : Show max 20 items
                                                    </FooterTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="Label2" runat="server" Text="Subledger Cost"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox runat="server" ID="cboSubledgerIdAPCost" Height="190px" Width="300px"
                                                    EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                                    OnItemDataBound="cboSubledgerId_ItemDataBound"
                                                    OnItemsRequested="cboSubledgerIdAPCost_ItemsRequested">
                                                    <ItemTemplate>
                                                        <b>
                                                            <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                                            &nbsp;-&nbsp; (<%# DataBinder.Eval(Container.DataItem, "Description")%>) </b>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Note : Show max 20 items
                                                    </FooterTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td width="20px"></td>
                                            <td></td>
                                        </tr>

                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="Label7" runat="server" Text="Chart Of Account PO Return"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox ID="cboChartOfAccountIdPOReturn" runat="server" Height="190px"
                                                    Width="300px" HighlightTemplatedItems="true" AutoPostBack="true" EnableLoadOnDemand="true"
                                                    OnItemsRequested="cboChartOfAccountId_ItemsRequested" OnItemDataBound="cboChartOfAccountId_ItemDataBound"
                                                    OnSelectedIndexChanged="cboChartOfAccountIdPOReturn_SelectedIndexChanged">
                                                    <ItemTemplate>
                                                        <b>
                                                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                                            &nbsp;-&nbsp;
                                                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                                        </b>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Note : Show max 20 items
                                                    </FooterTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="Label8" runat="server" Text="Subledger PO Return"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox runat="server" ID="cboSubledgerIdPOReturn" Height="190px" Width="300px"
                                                    EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                                    OnItemDataBound="cboSubledgerId_ItemDataBound"
                                                    OnItemsRequested="cboSubledgerIdPOReturn_ItemsRequested">
                                                    <ItemTemplate>
                                                        <b>
                                                            <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                                            &nbsp;-&nbsp; (<%# DataBinder.Eval(Container.DataItem, "Description")%>) </b>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Note : Show max 20 items
                                                    </FooterTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td width="20px"></td>
                                            <td></td>
                                        </tr>

                                        <tr id="trCOAPOReturnNM" runat="server">
                                            <td class="label">
                                                <asp:Label ID="Label14" runat="server" Text="Chart Of Account PO Return Non Medical"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox ID="cboChartOfAccountIdPOReturnNonMedical" runat="server" Height="190px"
                                                    Width="300px" HighlightTemplatedItems="true" AutoPostBack="true" EnableLoadOnDemand="true"
                                                    OnItemsRequested="cboChartOfAccountId_ItemsRequested"
                                                    OnItemDataBound="cboChartOfAccountId_ItemDataBound"
                                                    OnSelectedIndexChanged="cboChartOfAccountIdPOReturnNonMedical_SelectedIndexChanged">
                                                    <ItemTemplate>
                                                        <b>
                                                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                                            &nbsp;-&nbsp;
                                                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                                        </b>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Note : Show max 20 items
                                                    </FooterTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr id="trSlPOReturnNM" runat="server">
                                            <td class="label">
                                                <asp:Label ID="Label15" runat="server" Text="Subledger PO Return Non Medical"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox runat="server" ID="cboSubledgerIdPOReturnNonMedical" Height="190px"
                                                    Width="300px" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                                    OnItemDataBound="cboSubledgerId_ItemDataBound"
                                                    OnItemsRequested="cboSubledgerIdPOReturnNonMedical_ItemsRequested">
                                                    <ItemTemplate>
                                                        <b>
                                                            <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                                            &nbsp;-&nbsp; (<%# DataBinder.Eval(Container.DataItem, "Description")%>) </b>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Note : Show max 20 items
                                                    </FooterTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td width="20px"></td>
                                            <td></td>
                                        </tr>

                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="Label17" runat="server" Text="Chart Of Account Grant Receive"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox ID="cboChartOfAccountIdGrantReceive" runat="server" Height="190px"
                                                    Width="300px" HighlightTemplatedItems="true" AutoPostBack="true" EnableLoadOnDemand="true"
                                                    OnItemsRequested="cboChartOfAccountId_ItemsRequested" OnItemDataBound="cboChartOfAccountId_ItemDataBound"
                                                    OnSelectedIndexChanged="cboChartOfAccountIdGrantReceive_SelectedIndexChanged">
                                                    <ItemTemplate>
                                                        <b>
                                                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                                            &nbsp;-&nbsp;
                                                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                                        </b>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Note : Show max 20 items
                                                    </FooterTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="Label18" runat="server" Text="Subledger Grant Receive"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox runat="server" ID="cboSubledgerIdGrantReceive" Height="190px" Width="300px"
                                                    EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                                    OnItemDataBound="cboSubledgerId_ItemDataBound"
                                                    OnItemsRequested="cboSubledgerIdGrantReceive_ItemsRequested">
                                                    <ItemTemplate>
                                                        <b>
                                                            <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                                            &nbsp;-&nbsp; (<%# DataBinder.Eval(Container.DataItem, "Description")%>) </b>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Note : Show max 20 items
                                                    </FooterTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td width="20px"></td>
                                            <td></td>
                                        </tr>

                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="Label19" runat="server" Text="Chart Of Account Grant Receive Non Medical"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox ID="cboChartOfAccountIdGrantReceiveNmed" runat="server" Height="190px"
                                                    Width="300px" HighlightTemplatedItems="true" AutoPostBack="true" EnableLoadOnDemand="true"
                                                    OnItemsRequested="cboChartOfAccountId_ItemsRequested" OnItemDataBound="cboChartOfAccountId_ItemDataBound"
                                                    OnSelectedIndexChanged="cboChartOfAccountIdGrantReceiveNmed_SelectedIndexChanged">
                                                    <ItemTemplate>
                                                        <b>
                                                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                                            &nbsp;-&nbsp;
                                                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                                        </b>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Note : Show max 20 items
                                                    </FooterTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="Label20" runat="server" Text="Subledger Grant Receive Non Medical"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox runat="server" ID="cboSubledgerIdGrantReceiveNmed" Height="190px" Width="300px"
                                                    EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                                    OnItemDataBound="cboSubledgerId_ItemDataBound"
                                                    OnItemsRequested="cboSubledgerIdGrantReceiveNmed_ItemsRequested">
                                                    <ItemTemplate>
                                                        <b>
                                                            <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                                            &nbsp;-&nbsp; (<%# DataBinder.Eval(Container.DataItem, "Description")%>) </b>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Note : Show max 20 items
                                                    </FooterTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td width="20px"></td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </td>
        </tr>
    </table>
</asp:Content>
