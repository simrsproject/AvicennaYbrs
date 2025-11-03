<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ItemPackageDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ItemPackageDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script language="javascript" type="text/javascript">
            function openItemComponent(itemID, isPercent, discValue) {
                var oWnd = $find("<%= winItemComponent.ClientID %>");
                oWnd.SetUrl("ItemPackageTariffComponentDetail.aspx?itemID=" + itemID + "&ip=" + isPercent + "&dv=" + discValue);
                oWnd.Show();
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument) {
                    if (oWnd.argument == 'rebind') {
                        __doPostBack("<%= grdItemPackage.UniqueID %>", 'rebind');
                        oWnd.argument = 'undefined';
                    }
                }
            }

            function LoadFromTemplate() {
                var oWnd = $find('ctl00_ContentPlaceHolder1_grdItemPackage_ctl00_ctl02_ctl00_cboTemplate');
                if (oWnd.get_value() != '') {
                    if (confirm('Are you sure to copy from selected template?'))
                        __doPostBack("<%= grdItemPackage.UniqueID %>", 'copy|' + oWnd.get_value());
                }
            }

            function ClearDetail() {
                if (confirm('Are you sure to clear inserted data?'))
                    __doPostBack("<%= grdItemPackage.UniqueID %>", 'clear');
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winItemComponent" Animation="None" Width="800px" Height="500px"
        OnClientClose="onClientClose" runat="server" Behavior="Close" ShowContentDuringLoad="false"
        VisibleStatusbar="false" Modal="true">
    </telerik:RadWindow>
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemID" runat="server" Text="Item ID" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemID" runat="server" Width="100px" MaxLength="10" />
                        </td>
                        <td width="20px">
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemName" runat="server" Text="Item Name" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px" MaxLength="200"/>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ErrorMessage="Item Name required."
                                ValidationGroup="entry" ControlToValidate="txtItemName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemGroupID" runat="server" Text="Item Group" />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboItemGroupID" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvItemGroupID" runat="server" ErrorMessage="Item Group ID required."
                                ValidationGroup="entry" ControlToValidate="cboItemGroupID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor" />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboGuarantorID" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboGuarantorID_ItemDataBound"
                                OnItemsRequested="cboGuarantorID_ItemsRequested">
                                <FooterTemplate>
                                    Note : Show max 30 result
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblValidityPeriod" runat="server" Text="Validity Period From" />
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtValidityPeriodFrom" runat="server" Width="100px">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>&nbsp;to&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtValidityPeriodTo" runat="server" Width="100px">
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" TextMode="MultiLine" Height="50" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBillingGroup" runat="server" Text="Billing Statement Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboBillingGroup" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvBillingGroup" runat="server" ErrorMessage="Billing Group ID required."
                                ValidationGroup="entry" ControlToValidate="cboBillingGroup" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr runat="server" id="trBpjsItemGroup">
                        <td class="label">
                            <asp:Label ID="lblSRBpjsItemGroup" runat="server" Text="BPJS Item Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRBpjsItemGroup" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td class="label">
                            E-Klaim Item Group
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSREklaimGroup" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr heigth="24px">
                        <td class="label" />
                        <td class="entry">
                            <asp:CheckBox ID="chkIsDelegationToNurse" runat="server" Text="Delegation To Nurse" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr heigth="24px">
                        <td class="label" />
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Item Package" PageViewID="pgvItemPackage" Selected="true" />
            <telerik:RadTab runat="server" Text="Price History" PageViewID="pgvPriceHistory" />
            <telerik:RadTab runat="server" Text="Service Unit" PageViewID="pgvServiceUnit">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvItemPackage" runat="server">
            <telerik:RadGrid ID="grdItemPackage" runat="server" OnNeedDataSource="grdItemPackage_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItemPackage_UpdateCommand"
                OnDeleteCommand="grdItemPackage_DeleteCommand" OnInsertCommand="grdItemPackage_InsertCommand"
                ShowFooter="True">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="DetailItemID">
                    <CommandItemTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <tr>
                                    <td width="50%">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# !grdItemPackage.MasterTableView.IsItemInserted %>'>
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/insert16.png" />
                                            &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new record"></asp:Label>
                                        </asp:LinkButton>
                                    </td>
                                    <td width="50%" align="right">
                                        <asp:Label runat="server" ID="lblCopyTemplate" Text="Copy template"></asp:Label>&nbsp;
                                        <telerik:RadComboBox runat="server" ID="cboTemplate" Width="300px" Visible='<%# !grdItemPackage.MasterTableView.IsItemInserted %>'
                                            EnableLoadOnDemand="true" MarkFirstMatch="true" HighlightTemplatedItems="true"
                                            OnItemDataBound="cboTemplate_ItemDataBound" OnItemsRequested="cboTemplate_ItemsRequested">
                                            <FooterTemplate>
                                                Note : Show max 15 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                        &nbsp;&nbsp;
                                        <asp:LinkButton ID="lbPickList" runat="server" Visible='<%# !grdItemPackage.MasterTableView.IsItemInserted %>'
                                            OnClientClick="javascript:LoadFromTemplate();return false;">
                                            <img style="border: 0px; vertical-align: middle;" alt="Copy" src="../../../../Images/Toolbar/download16.png" />
                                        </asp:LinkButton>
                                        &nbsp;&nbsp;
                                        <asp:LinkButton ID="lblClearAll" runat="server" Visible='<%# !grdItemPackage.MasterTableView.IsItemInserted %>'
                                            OnClientClick="javascript:ClearDetail();return false;">
                                            <img style="border: 0px; vertical-align: middle;" alt="Clear" src="../../../../Images/Toolbar/row_delete16.png" />
                                        </asp:LinkButton>
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </tr>
                        </table>
                    </CommandItemTemplate>
                    <CommandItemStyle Height="29px" />
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DetailItemID" HeaderText="Item ID"
                            UniqueName="DetailItemID" SortExpression="DetailItemID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="refToItem_ItemName" HeaderText="Item Name" UniqueName="DetailItemName"
                            SortExpression="DetailItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="refToServiceUnit_ServiceUnitName" HeaderText="Service Unit Name"
                            UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Quantity" HeaderText="Qty"
                            UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRItemUnit" HeaderText="Unit"
                            UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="DiscountValue" HeaderText="Disc %"
                            UniqueName="DiscountValue" SortExpression="DiscountValue" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsDiscountInPercent"
                            HeaderText="(%)" UniqueName="IsDiscountInPercent" SortExpression="IsDiscountInPercent"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="refToItemPackage_Price"
                            HeaderText="Price" UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="refToItemPackage_Discount"
                            HeaderText="Discount" UniqueName="Discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="refToItemPackage_Total"
                            HeaderText="Total" UniqueName="Total" SortExpression="Total" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsStockControl"
                            HeaderText="Stock Control" UniqueName="IsStockControl" SortExpression="IsStockControl"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsExtraItem" HeaderText="Extra"
                            UniqueName="IsExtraItem" SortExpression="IsExtraItem" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsActive" HeaderText="Active"
                            UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsAutoApprove" HeaderText="Auto Approve"
                            UniqueName="IsAutoApprove" SortExpression="IsAutoApprove" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn UniqueName="component">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openItemComponent('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/Toolbar/details16.png\" border=\"0\" title=\"Open Tariff Component\" /></a>",
                                            DataBinder.Eval(Container.DataItem, "DetailItemID"), DataBinder.Eval(Container.DataItem, "IsDiscountInPercent"), DataBinder.Eval(Container.DataItem, "DiscountValue"))%>
                            </ItemTemplate>
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ItemPackageItemDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ItemPackageEditCommand">
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
        <telerik:RadPageView ID="pgvPriceHistory" runat="server">
            <telerik:RadGrid ID="grdPriceHistory" runat="server" AutoGenerateColumns="False"
                GridLines="None" OnNeedDataSource="grdPriceHistory_NeedDataSource" OnDetailTableDataBind="grdPriceHistory_DetailTableDataBind">
                <MasterTableView DataKeyNames="SRTariffType, ItemID, ClassID, StartingDate" AllowPaging="true"
                    AllowSorting="true" PageSize="20">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="TariffTypeName" HeaderText="Tariff Type"
                            UniqueName="TariffTypeName" SortExpression="TariffTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ClassName" HeaderText="Class"
                            UniqueName="ClassName" SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="StartingDate" HeaderText="Starting Date"
                            UniqueName="StartingDate" SortExpression="StartingDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                            UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsAdminCalculation"
                            HeaderText="Admin Calculation" UniqueName="IsAdminCalculation" SortExpression="IsAdminCalculation"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsAllowDiscount"
                            HeaderText="Discount" UniqueName="IsAllowDiscount" SortExpression="IsAllowDiscount"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsAllowVariable"
                            HeaderText="Variable" UniqueName="IsAllowVariable" SortExpression="IsAllowVariable"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsAllowCito" HeaderText="Cito"
                            UniqueName="IsAllowCito" SortExpression="IsAllowCito" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CitoValue" HeaderText="Cito Value"
                            UniqueName="CitoValue" SortExpression="CitoValue" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ReferenceNo" HeaderText="Reference No"
                            UniqueName="ReferenceNo" SortExpression="ReferenceNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="UpdateBy" HeaderText="Update By"
                            UniqueName="UpdateBy" SortExpression="UpdateBy" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView DataKeyNames="TariffComponentID" Name="grdItemTariffRequestItemComp"
                            AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridBoundColumn DataField="TariffComponentName" HeaderText="Component Name"
                                    UniqueName="TariffComponentName" SortExpression="TariffComponentName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Tariff"
                                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsAllowDiscount"
                                    HeaderText="Discount" UniqueName="IsAllowDiscount" SortExpression="IsAllowDiscount"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsAllowVariable"
                                    HeaderText="Variable" UniqueName="IsAllowVariable" SortExpression="IsAllowVariable"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvServiceUnit" runat="server">
            <telerik:RadGrid ID="grdServiceUnit" runat="server" OnNeedDataSource="grdServiceUnit_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdServiceUnit_DeleteCommand"
                OnInsertCommand="grdServiceUnit_InsertCommand" OnUpdateCommand="grdServiceUnit_UpdateCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ServiceUnitID" AllowPaging="true"
                    PageSize="10">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ServiceUnitID" HeaderText="Unit ID"
                            UniqueName="ServiceUnitID" SortExpression="ServiceUnitID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Unit Name" UniqueName="ServiceUnitName"
                            SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAllowEditByUserVerificated"
                            HeaderText="Allow Edit" UniqueName="IsAllowEditByUserVerificated" SortExpression="IsAutoPayment"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="~\Module\RADT\Master\ItemService\ItemServiceUnitDetail.ascx"
                        EditFormType="WebUserControl">
                        <EditColumn UniqueName="ItemServiceUnitEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
