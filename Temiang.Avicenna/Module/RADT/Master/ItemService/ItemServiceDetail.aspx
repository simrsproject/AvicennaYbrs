<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ItemServiceDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ItemServiceDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/PCareCommon/LookUp/PCareReferenceCtl.ascx" TagPrefix="uc2" TagName="PCareReferenceCtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
        function txtBarcodeEntryKeyPress(sender, eventArgs) {
            var code = eventArgs.get_keyCode();
            if (code == 13) {
                eventArgs.set_cancel(true); // Cegah enter
            }
        }
    </script>
    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemID" runat="server" Text="Service ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemID" runat="server" Width="100px" MaxLength="10" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemName" runat="server" Text="Service Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px" MaxLength="400"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ErrorMessage="Item Name required."
                                ValidationGroup="entry" ControlToValidate="txtItemName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemGroupID" runat="server" Text="Item Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboItemGroupID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboItemGroupID_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvItemGroupID" runat="server" ErrorMessage="Item Group ID required."
                                ValidationGroup="entry" ControlToValidate="cboItemGroupID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trSRItemSubGroup">
                        <td class="label">
                            <asp:Label ID="lblSRItemSubGroup" runat="server" Text="Item Sub Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRItemSubGroup" runat="server" Width="300px" EnableLoadOnDemand="True"
                                HighlightTemplatedItems="True" MarkFirstMatch="False" OnItemDataBound="cboSRItemSubGroup_ItemDataBound"
                                OnItemsRequested="cboSRItemSubGroup_ItemsRequested">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBarcode" runat="server" Text="Barcode"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBarcode" runat="server" Width="300px" MaxLength="35" Font-Names="C30T"
                                Font-Size="43">
                                <ClientEvents OnKeyPress="txtBarcodeEntryKeyPress"></ClientEvents>
                            </telerik:RadTextBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trPcare">
                        <td colspan="4">
                            <uc2:PCareReferenceCtl runat="server" ID="pcareReference" ReferenceType="Tindakan" />
                        </td>
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
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemUnit" runat="server" Text="Item Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRItemUnit" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" TextMode="MultiLine" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReportRLID" runat="server" Text="Report RL ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboReportRLID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboReportRLID_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRlMasterReportItemID" runat="server" Text="Report RL Detail ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboRlMasterReportItemID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblPremiAmount" runat="server" Text="Premi Phaco Amount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPremiAmount" runat="server" Width="100px">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvPremiAmount" runat="server" ErrorMessage="Premi Phaco Amount required."
                                ControlToValidate="txtPremiAmount" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblPremi2Amount" runat="server" Text="Premi IOL Amount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPremi2Amount" runat="server" Width="100px">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvPremi2Amount" runat="server" ErrorMessage="Premi IOL Amount required."
                                ControlToValidate="txtPremi2Amount" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="labelcaption" colspan="4">
                            <asp:Label ID="lblItemRelated" runat="server" Text="Item Product Related" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblItemRelatedID" runat="server" Text="Item Product"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboItemRelatedID" Width="300px" AutoPostBack="False"
                                EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                                OnItemDataBound="cboItemRelatedID_ItemDataBound" OnItemsRequested="cboItemRelatedID_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                                        &nbsp;(<%# DataBinder.Eval(Container.DataItem, "ItemID")%>) </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 15 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblQtyDivider" runat="server" Text="Qty Divider"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtQtyDivider" runat="server" Width="100px" MaxLength="10"
                                MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="0" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblProductionServicesPercentage1" runat="server" Text="Production Services #1"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtProductionServicesPercentage1" runat="server" Width="100px"
                                Type="Percent">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvProductionServicesPercentage1" runat="server"
                                ErrorMessage="Production Services #1 (%) required." ControlToValidate="txtProductionServicesPercentage1"
                                ValidationGroup="entry" SetFocusOnError="True" Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblProductionServicesPercentage2" runat="server" Text="Production Services #2"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtProductionServicesPercentage2" runat="server" Width="100px"
                                Type="Percent">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvProductionServicesPercentage2" runat="server"
                                ErrorMessage="Production Services #2 (%) required." ControlToValidate="txtProductionServicesPercentage2"
                                ValidationGroup="entry" SetFocusOnError="True" Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSavingPercentage" runat="server" Text="Tabungan Kebersamaan"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtSavingPercentage" runat="server" Width="100px"
                                Type="Percent">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSavingPercentage" runat="server" ErrorMessage="Saving of Togetherness (%) required."
                                ControlToValidate="txtSavingPercentage" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
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
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">E-Klaim Item Group
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSREklaimGroup" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsPrimaryService" runat="server" Text="Primary Service" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsAdminCalculation" runat="server" Text="Admin Calculation" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsAllowVariable" runat="server" Text="Allow Variable" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkIsAllowCito" runat="server" Text="Allow Cito" AutoPostBack="True"
                                            OnCheckedChanged="chkIsAllowCito_CheckedChanged" />
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:CheckBox ID="chkIsCitoFromStandardReference" runat="server" Text="Cito From Standard Reference" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsAllowDiscount" runat="server" Text="Allow Discount" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsPrintWithDoctorName" runat="server" Text="Print With Doctor Name" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsAssetUtilization" runat="server" Text="Asset Utilization" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsHasTestResults" runat="server" Text="Has Test Results" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsDelegationToNurse" runat="server" Text="Delegation To Nurse" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblIntervalOrderWarning" runat="server" Text="Interval Order Warning"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtIntervalOrderWarning" runat="server" Width="100px" MaxLength="5"
                                MinValue="0" NumberFormat-DecimalDigits="0" />&nbsp; Hour(s)
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Inventory Consumption" PageViewID="pgvConsumption"
                Selected="true">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Tariff History" PageViewID="pgvPriceHistory">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Service Unit" PageViewID="pgvServiceUnit">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Service Unit" PageViewID="pgvServiceUnitCheckList">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Bridging & Integration" PageViewID="pgvAliasName" />
            <telerik:RadTab runat="server" Text="Procedure" PageViewID="pgvProcedure" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvConsumption" runat="server">
            <telerik:RadGrid ID="grdItemConsumption" runat="server" OnNeedDataSource="grdItemConsumption_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItemConsumption_UpdateCommand"
                OnDeleteCommand="grdItemConsumption_DeleteCommand" OnInsertCommand="grdItemConsumption_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="DetailItemID" AllowPaging="true"
                    PageSize="10">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DetailItemID" HeaderText="Item ID"
                            UniqueName="DetailItemID" SortExpression="DetailItemID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="DetailItemName" HeaderText="Item Name" UniqueName="DetailItemName"
                            SortExpression="DetailItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Qty" HeaderText="Qty"
                            UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRItemUnit" HeaderText="Unit"
                            UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ItemConsumptionDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ItemConsumptionEditCommand">
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
        <telerik:RadPageView ID="pgvPriceHistory" runat="server">
            <telerik:RadGrid ID="grdPriceHistory" runat="server" AutoGenerateColumns="False"
                GridLines="None" OnNeedDataSource="grdPriceHistory_NeedDataSource" OnDetailTableDataBind="grdPriceHistory_DetailTableDataBind">
                <MasterTableView DataKeyNames="SRTariffType, ItemID, ClassID, StartingDate" AllowPaging="true"
                    AllowSorting="true" PageSize="20">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="TariffTypeName" HeaderText="Tariff Type"
                            UniqueName="TariffTypeName" SortExpression="TariffTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ClassName" HeaderText="Class Name"
                            UniqueName="ClassName" SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="StartingDate" HeaderText="Starting Date"
                            UniqueName="StartingDate" SortExpression="StartingDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                            UniqueName="Price" SortExpression="Price" DataFormatString="{0:n2}" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsAdminCalculation"
                            HeaderText="Admin Calculation" UniqueName="IsAdminCalculation" SortExpression="IsAdminCalculation"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAllowDiscount"
                            HeaderText="Discount" UniqueName="IsAllowDiscount" SortExpression="IsAllowDiscount"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAllowVariable"
                            HeaderText="Variable" UniqueName="IsAllowVariable" SortExpression="IsAllowVariable"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAllowCito" HeaderText="Cito"
                            UniqueName="IsAllowCito" SortExpression="IsAllowCito" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CitoValue" HeaderText="Cito Value"
                            UniqueName="CitoValue" SortExpression="CitoValue" DataFormatString="{0:n2}" HeaderStyle-HorizontalAlign="Center"
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
                            Width="500px" AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridBoundColumn DataField="TariffComponentName" HeaderText="Component Name"
                                    UniqueName="TariffComponentName" SortExpression="TariffComponentName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Tariff"
                                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAllowDiscount"
                                    HeaderText="Allow Discount" UniqueName="IsAllowDiscount" SortExpression="IsAllowDiscount"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAllowVariable"
                                    HeaderText="Allow Variable" UniqueName="IsAllowVariable" SortExpression="IsAllowVariable"
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
                            HeaderText="Allow Edit" UniqueName="IsAllowEditByUserVerificated" SortExpression="IsAllowEditByUserVerificated"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsVisible"
                            HeaderText="Visible" UniqueName="IsVisible" SortExpression="IsVisible"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ItemServiceUnitDetail.ascx" EditFormType="WebUserControl">
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
        <telerik:RadPageView ID="pgvServiceUnitCheckList" runat="server">
            <telerik:RadGrid ID="grdServiceUnitCheckList" runat="server" AutoGenerateColumns="False"
                GridLines="None" OnNeedDataSource="grdServiceUnitCheckList_NeedDataSource">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ServiceUnitID">
                    <Columns>
                        <%--<telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                    runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="detailChkbox" runat="server" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>--%>
                        <telerik:GridTemplateColumn HeaderStyle-Width="50px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                    runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkIsSelect" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                                    Checked='<%#DataBinder.Eval(Container.DataItem, "IsSelect") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ServiceUnitID" HeaderText="Unit ID"
                            UniqueName="ServiceUnitID" SortExpression="ServiceUnitID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ServiceUnitName" HeaderText="Unit Name"
                            UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="80px" HeaderText="Allow Edit">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkIsAllowEditByUserVerificated" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                                    Checked='<%#DataBinder.Eval(Container.DataItem, "IsAllowEditByUserVerificated") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="80px" HeaderText="Visible">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkIsVisible" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                                    Checked='<%#DataBinder.Eval(Container.DataItem, "IsVisible") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="pgvAliasName">
            <telerik:RadGrid ID="grdAliasName" runat="server" OnNeedDataSource="grdAliasName_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdAliasName_UpdateCommand"
                OnDeleteCommand="grdAliasName_DeleteCommand" OnInsertCommand="grdAliasName_InsertCommand"
                AllowPaging="true">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID, SRBridgingType, BridgingID"
                    PageSize="15">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="BridgingTypeName" HeaderText="Bridging Type"
                            UniqueName="BridgingTypeName" SortExpression="BridgingTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="BridgingID" HeaderText="Bridging ID"
                            UniqueName="BridgingID" SortExpression="BridgingID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="BridgingName" HeaderText="Bridging Name" UniqueName="BridgingName"
                            SortExpression="BridgingName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ItemIdExternal" HeaderText="Item ID External"
                            UniqueName="ItemIdExternal" SortExpression="ItemIdExternal" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                            UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ItemAliasDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ItemAliasEditCommand">
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
        <telerik:RadPageView runat="server" ID="pgvProcedure">
            <telerik:RadGrid ID="grdProcedure" runat="server" OnNeedDataSource="grdProcedure_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdProcedure_DeleteCommand" OnInsertCommand="grdProcedure_InsertCommand"
                AllowPaging="true">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="SRProcedure"
                    PageSize="15">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRProcedure" HeaderText="ID"
                            UniqueName="SRProcedure" SortExpression="SRProcedure" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ProcedureName" HeaderText="Procedure Name"
                            UniqueName="ProcedureName" SortExpression="ProcedureName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ItemProcedureDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ItemProcedureDetailEditCommand">
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
</asp:Content>
