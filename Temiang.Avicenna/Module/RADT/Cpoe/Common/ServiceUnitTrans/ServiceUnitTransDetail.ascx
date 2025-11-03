<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceUnitTransDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Cpoe.ServiceUnitTransDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemBalance" runat="server" ValidationGroup="TransChargesItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="TransChargesItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<asp:HiddenField runat="server" ID="hdnSrTariffType" />
<asp:HiddenField runat="server" ID="hdnChargeClassId" />
<table width="100%" cellspacing="0" cellpadding="0">
    <tr>
        <td width="50%">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemID" runat="server" Text="Item" />
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboItemID" Width="300px" AutoPostBack="True"
                            EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                            OnItemDataBound="cboItemID_ItemDataBound" OnItemsRequested="cboItemID_ItemsRequested"
                            OnSelectedIndexChanged="cboItemID_SelectedIndexChanged">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemName") %>
                                </b>
                                <%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Balance")) > 0 ? DataBinder.Eval(Container.DataItem, "Balance", "<br />Stock : {0:n2}") : string.Empty%>
                                <%# DataBinder.Eval(Container.DataItem, "Notes", "<br />{0}").ToString()%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 30 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item required."
                            ControlToValidate="cboItemID" SetFocusOnError="True" ValidationGroup="TransChargesItem"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr style="display: none">
                    <td class="label"></td>
                    <td class="entrydescription">
                        <asp:CheckBox ID="chkIsItemProduct" runat="server" Enabled="False" />
                        <asp:CheckBox ID="chkIsItemTypeService" runat="server" Enabled="False" />
                    </td>
                    <td width="20px"></td>
                    <td />
                </tr>
            </table>
        </td>
        <td width="50%">
            <table width="100%" id="pnlParamedic" runat="server">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblParamedicID" runat="server" Text="Physician" />
                    </td>
                    <td class="entrydescription">
                        <telerik:RadComboBox runat="server" ID="cboParamedicID" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboParamedicID_ItemDataBound"
                            OnItemsRequested="cboParamedicID_ItemsRequested">
                            <FooterTemplate>
                                Note : Show max 15 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvParamedicID" runat="server" ErrorMessage="Paramedic ID required."
                            ControlToValidate="cboParamedicID" SetFocusOnError="True" ValidationGroup="TransChargesItem"
                            Width="100%">
                            <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
            </table>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td class="label"></td>
        <td class="entrydescription">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td colspan="2" class="labelcaption">
                                    <asp:Label ID="lblChargeQuantity" runat="server" Text="Charge Quantity" />
                                </td>
                                <td colspan="2" class="labelcaption">
                                    <asp:Label ID="lblStockQuantity" runat="server" Text="Stock Quantity" />
                                </td>
                                <td class="labelcaption">
                                    <asp:Label ID="lblSRItemUnit" runat="server" Text="Unit" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtChargeQuantity" runat="server" Width="100px" Value="1"
                                        MinValue="0" AutoPostBack="True" OnTextChanged="txtChargeQuantity_TextChanged" />
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvChargeQuantity" runat="server" ErrorMessage="Charge Quantity required."
                                        ControlToValidate="txtChargeQuantity" SetFocusOnError="True" ValidationGroup="TransChargesItem"
                                        Width="100%">
                                        <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtStockQuantity" runat="server" Width="100px" Value="1"
                                        MinValue="0" ReadOnly="true" />
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvStockQuantity" runat="server" ErrorMessage="Stock Quantity required."
                                        ControlToValidate="txtStockQuantity" SetFocusOnError="True" ValidationGroup="TransChargesItem"
                                        Width="100%">
                                        <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtItemUnit" runat="server" Width="100px" ReadOnly="true" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="vertical-align: top">
                        <table id="pnlPriceJO" runat="server" visible="false">
                            <tr>
                                <td class="labelcaption">
                                    <asp:Label ID="lblPriceJO" runat="server" Text="Price" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtPriceJO" runat="server" Width="100px" Value="0"
                                        ReadOnly="true" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
        <td width="20" />
        <td />
    </tr>
    <tr runat="server" id="pnltariff" visible="false">
        <td class="label"></td>
        <td class="entrydescription">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <table id="pnlVariableAndDiscount" runat="server" visible="false">
                            <tr>
                                <td colspan="3" class="labelcaption">
                                    <asp:Label ID="lblPrice1" runat="server" Text="Price" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkIsVariable" runat="server" Enabled="False" AutoPostBack="True"
                                        OnCheckedChanged="chkIsVariable_CheckedChanged" Text="Variable" />
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtPrice1" runat="server" Width="100px" ReadOnly="true"
                                        Value="0" MinValue="0" />
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvPrice1" runat="server" ErrorMessage="Price required."
                                        ControlToValidate="txtPrice1" SetFocusOnError="True" ValidationGroup="TransChargesItem"
                                        Width="100%">
                                        <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table runat="server" id="tbDiscount">
                            <tr>
                                <td colspan="3" class="labelcaption">
                                    <asp:Label ID="Label1" runat="server" Text="Discount Amount" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkIsDiscount" runat="server" Enabled="False" AutoPostBack="True"
                                        OnCheckedChanged="chkIsDiscount_CheckedChanged" Text="Discount" />
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtPercentDiscount1" runat="server" Width="100px"
                                        MaxValue="100" AutoPostBack="true" OnTextChanged="txtPercentDiscount1_TextChanged"
                                        ReadOnly="true" Value="0" />
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtDiscountAmount1" runat="server" Width="100px" ReadOnly="true"
                                        Value="0" />
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvDiscountAmount1" runat="server" ErrorMessage="Discount Amount required."
                                        ControlToValidate="txtDiscountAmount1" SetFocusOnError="True" ValidationGroup="TransChargesItem"
                                        Width="100%">
                                        <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table id="pnlComponentTariff" runat="server" width="100%" visible="false" cellpadding="0"
                cellspacing="0">
                <tr>
                    <td>
                        <telerik:RadGrid ID="grdTariff" runat="server" OnItemCreated="grdTariff_ItemCreated"
                            AutoGenerateColumns="False" GridLines="None" Height="170px">
                            <MasterTableView DataKeyNames="TariffComponentID" CellSpacing="-1">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="TariffComponentID" UniqueName="TariffComponentID"
                                        SortExpression="TariffComponentID" Visible="False" />
                                    <telerik:GridBoundColumn DataField="TariffComponentName" HeaderText="Tariff Component Name"
                                        UniqueName="TariffComponentName" SortExpression="TariffComponentName">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Price" UniqueName="Price" SortExpression="Price"
                                        Visible="False" />
                                    <telerik:GridTemplateColumn HeaderText="Price" UniqueName="PriceText" HeaderStyle-HorizontalAlign="center">
                                        <HeaderStyle Width="120px" />
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox runat="server" ID="txtPrice" Width="100px" MinValue="0">
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn>
                                        <HeaderStyle Width="20px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:RequiredFieldValidator ID="rfvPrice" runat="server" ErrorMessage="Price required."
                                                ControlToValidate="txtPrice" SetFocusOnError="True" ValidationGroup="TransChargesItem"
                                                Width="100%">
                                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Discount (%)" UniqueName="PercentDiscountText"
                                        HeaderStyle-HorizontalAlign="center">
                                        <HeaderStyle Width="60px" />
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox runat="server" ID="txtPercentDiscount" Width="40px" MaxValue="100" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Discount (Rp)" UniqueName="DiscountText"
                                        HeaderStyle-HorizontalAlign="center">
                                        <HeaderStyle Width="120px" />
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox runat="server" ID="txtDiscount" Width="100px" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn>
                                        <HeaderStyle Width="20px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:RequiredFieldValidator ID="rfvDiscount" runat="server" ErrorMessage="Discount required."
                                                ControlToValidate="txtDiscount" SetFocusOnError="True" ValidationGroup="TransChargesItem"
                                                Width="100%">
                                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Physician" UniqueName="PhysicianText" HeaderStyle-HorizontalAlign="left">
                                        <HeaderStyle Width="310px" />
                                        <ItemTemplate>
                                            <%--                                            <telerik:RadComboBox ID="cboPhysicianID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPhysicianID_ItemDataBound"
                                                OnItemsRequested="cboPhysicianID_ItemsRequested" />--%>

                                            <telerik:RadComboBox ID="cboPhysicianID" runat="server" Width="300px" EmptyMessage="Select a Physician"
                                                EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true">
                                                <WebServiceSettings Method="Paramedics" Path="~/WebService/ComboBoxDataService.asmx" />
                                            </telerik:RadComboBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn>
                                        <HeaderStyle Width="20px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:RequiredFieldValidator ID="rfvPhysicianID" runat="server" ErrorMessage="Physician ID required."
                                                ControlToValidate="cboPhysicianID" SetFocusOnError="True" ValidationGroup="TransChargesItem"
                                                Width="100%">
                                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="IsAllowVariable" UniqueName="IsAllowVariable"
                                        SortExpression="IsAllowVariable" Visible="False" />
                                    <telerik:GridBoundColumn DataField="IsAllowDiscount" UniqueName="IsAllowDiscount"
                                        SortExpression="IsAllowDiscount" Visible="False" />
                                    <telerik:GridBoundColumn DataField="IsTariffParamedic" UniqueName="IsTariffParamedic"
                                        SortExpression="IsTariffParamedic" Visible="False" />
                                    <telerik:GridTemplateColumn HeaderText="Fee Disc (%)" UniqueName="PercentFeeDiscount"
                                        HeaderStyle-HorizontalAlign="center">
                                        <HeaderStyle Width="60px" />
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox runat="server" ID="txtPercentFeeDiscount" Width="40px"
                                                MaxValue="100" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <FilterMenu>
                            </FilterMenu>
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </td>
        <td width="20" />
        <td />
    </tr>
    <tr id="pnlAsset" runat="server" visible="false">
        <td class="label">
            <asp:CheckBox ID="chkIsAssetUtilization" runat="server" Enabled="False" AutoPostBack="True"
                OnCheckedChanged="chkIsAssetUtilization_CheckedChanged" />
            <asp:Label ID="lblIsAssetUtilization" runat="server" Text="Asset Utilization" />
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboAssetID" Width="300px" />
        </td>
        <td width="20px" />
        <td />
    </tr>
</table>
<table width="100%" cellspacing="0" cellpadding="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr id="pnlDiscountReason" runat="server">
                    <td class="label">
                        <asp:Label ID="lblSRDiscountReason" runat="server" Text="Discount Reason" />
                    </td>
                    <td class="entrydescription">
                        <telerik:RadComboBox ID="cboSRDiscountReason" runat="server" Width="300px" />
                    </td>
                    <td width="20" />
                    <td />
                </tr>
                <tr id="Tr1" runat="server">
                    <td class="label">
                        <asp:Label ID="lblNotes" runat="server" Text="Notes" />
                    </td>
                    <td class="entrydescription">
                        <telerik:RadTextBox runat="server" ID="txtNotes" Width="300px" TextMode="MultiLine"
                            MaxLength="4000" />
                    </td>
                    <td width="20" />
                    <td />
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr id="trCenterID" runat="server">
                    <td class="label">
                        <asp:Label ID="lblCenterID" runat="server" Text="Center" />
                    </td>
                    <td class="entrydescription">
                        <telerik:RadComboBox ID="cboCenterID" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvCenterID" runat="server" ErrorMessage="Center required."
                            ControlToValidate="cboCenterID" SetFocusOnError="True" ValidationGroup="TransChargesItem"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entrydescription">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 30%">
                                    <asp:CheckBox ID="chkIsCito" runat="server" Enabled="False" Text="Cito" />
                                </td>
                                <td width="4px" />
                                <td>
                                    <telerik:RadComboBox ID="cboSRCitoPercentage" runat="server" Enabled="False">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px"></td>
                    <td />
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entrydescription">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td id="pnlPackage" runat="server">
                                    <asp:CheckBox ID="chkIsPackage" runat="server" Text="Package" Enabled="false" />
                                </td>
                                <td width="4px" />
                                <td>
                                    <asp:CheckBox ID="chkIsVoid" runat="server" Text="Void" />
                                </td>
                                <td width="4px" />
                                <td>
                                    <asp:CheckBox ID="chkIsItemRoom" runat="server" Text="Item Room" Visible="False" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr id="Tr2" runat="server">
                    <td class="label">
                        <asp:Label ID="lblFilmNo" runat="server" Text="Film No" />
                    </td>
                    <td class="entrydescription">
                        <telerik:RadTextBox runat="server" ID="txtFilmNo" Width="300px" MaxLength="20" />
                    </td>
                    <td width="20" />
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemCondistionRuleID" runat="server" Text="Item Condition Rule" />
                    </td>
                    <td class="entrydescription">
                        <telerik:RadComboBox runat="server" ID="cboItemConditionRuleID" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboItemConditionRuleID_ItemDataBound"
                            OnItemsRequested="cboItemConditionRuleID_ItemsRequested">
                            <FooterTemplate>
                                Note : Show max 10 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px"></td>
                    <td />
                </tr>
                <tr style="display: none">
                    <td class="label"></td>
                    <td class="entrydescription">
                        <telerik:RadDatePicker ID="txtTariffDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                               DatePopupButton-Enabled="false">
                        </telerik:RadDatePicker>
                    </td>
                    <td width="20px"></td>
                    <td />
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="TransChargesItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>' />
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="TransChargesItem" Visible='<%# DataItem is GridInsertionObject %>' />
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
