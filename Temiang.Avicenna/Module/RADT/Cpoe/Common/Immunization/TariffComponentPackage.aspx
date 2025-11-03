<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="TariffComponentPackage.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.TariffComponentPackage" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script language="javascript" type="text/javascript">
            function RowSelected(sender, args) {
                __doPostBack("<%= grdTariff.UniqueID %>", args.getDataKeyValue("TransactionNo") + '|' + args.getDataKeyValue("SequenceNo"));
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTariff" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdTariff">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTariff" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="25%" valign="top">
                <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="False" GridLines="None"
                    Height="500px" OnNeedDataSource="grdList_NeedDataSource" OnDeleteCommand="grdList_DeleteCommand">
                    <MasterTableView DataKeyNames="TransactionNo, SequenceNo, ItemID" ClientDataKeyNames="TransactionNo, SequenceNo, ItemID">
                        <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Unit "></telerik:GridGroupByField>
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="ServiceUnitName" SortOrder="Ascending"></telerik:GridGroupByField>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <telerik:GridBoundColumn DataField="ItemName" UniqueName="ItemName" SortExpression="ItemName"
                                HeaderText="Item Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="false">
                        <Scrolling AllowScroll="True" />
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnRowSelected="RowSelected" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
            <td>
                <asp:Panel ID="Panel1" runat="server" Width="3px" />
            </td>
            <td width="75%" valign="top">
                <telerik:RadGrid ID="grdTariff" runat="server" OnItemCreated="grdTariff_ItemCreated"
                    Height="500px" AutoGenerateColumns="False" GridLines="None" OnItemCommand="grdTariff_ItemCommand">
                    <MasterTableView DataKeyNames="TransactionNo, SequenceNo, TariffComponentID" ClientDataKeyNames="TransactionNo, SequenceNo, TariffComponentID"
                        CellSpacing="-1">
                        <Columns>
                            <telerik:GridBoundColumn DataField="TransactionNo" UniqueName="TransactionNo" SortExpression="TransactionNo"
                                Visible="false" />
                            <telerik:GridBoundColumn DataField="SequenceNo" UniqueName="SequenceNo" SortExpression="SequenceNo"
                                Visible="false" />
                            <telerik:GridBoundColumn DataField="TariffComponentID" UniqueName="TariffComponentID"
                                SortExpression="TariffComponentID" Visible="False" />
                            <telerik:GridBoundColumn DataField="TariffComponentName" HeaderText="Tariff Component Name"
                                UniqueName="TariffComponentName" SortExpression="TariffComponentName">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Price" DataField="Price" UniqueName="Price"
                                SortExpression="Price" Visible="False" />
                            <telerik:GridTemplateColumn HeaderText="Price" UniqueName="PriceText" HeaderStyle-HorizontalAlign="center">
                                <HeaderStyle Width="100px" />
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox runat="server" ID="txtPrice" Width="80px">
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
                            <telerik:GridTemplateColumn HeaderText="Discount" UniqueName="DiscountText" HeaderStyle-HorizontalAlign="center">
                                <HeaderStyle Width="100px" />
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox runat="server" ID="txtDiscount" Width="80px" />
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
                                    <telerik:RadComboBox ID="cboPhysicianID" runat="server" Width="297px" EnableLoadOnDemand="true"
                                        MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPhysicianID_ItemDataBound"
                                        OnItemsRequested="cboPhysicianID_ItemsRequested">
                                        <FooterTemplate>
                                            Note : Show max 15 items
                                        </FooterTemplate>
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
                            <telerik:GridTemplateColumn>
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CommandName="Save" />
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
</asp:Content>
