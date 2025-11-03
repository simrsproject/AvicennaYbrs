<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ItemLaboratoryDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ItemLaboratoryDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
        function txtBarcodeEntryKeyPress(sender, eventArgs) {
            var code = eventArgs.get_keyCode();
            if (code == 13) {
                eventArgs.set_cancel(true); // Cegah enter
            }
        }
        function onSatuSehatItemClick(name) {
            var txt = $find("ctl00_ContentPlaceHolder1_grdAliasName_ctl00_ctl02_ctl03_EditFormControl_txtServiceUnitAliasName");
            txt.set_value(name);
        }
        function cboSsBridgingID_ClientItemsRequesting(sender, eventArgs) {
            var context = eventArgs.get_context();
            context["tp"] = "lab";
        }
    </script>
    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemID" runat="server" Text="Item ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemID" runat="server" Width="100px" MaxLength="10" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemName" runat="server" Text="Item Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px" MaxLength="100"
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
                        <td width="20">
                        </td>
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
                            <%--<asp:RequiredFieldValidator ID="rfvBillingGroup" runat="server" ErrorMessage="Billing Group ID required."
                                ValidationGroup="entry" ControlToValidate="cboBillingGroup" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>--%>
                        </td>
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
                            <asp:Label ID="lblItemIDExternal" runat="server" Text="Item ID External"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemIDExternal" runat="server" Width="300px" MaxLength="20" />
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
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblWaitingTimeForResults" runat="server" Text="Waiting Time For Results"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtWaitingTimeForResults" runat="server" Width="100px" MaxLength="5"
                                            MinValue="0" NumberFormat-DecimalDigits="0" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cboSRIntervalTime" runat="server" Width="197px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSRIntervalTime_ItemDataBound"
                                            OnItemsRequested="cboSRIntervalTime_ItemsRequested">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "ItemID")%>
                                                &nbsp;-&nbsp;
                                                <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                Note : Show max 10 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRExaminationClass" runat="server" Text="Examination Class"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRExaminationClass" runat="server" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSpecimenType" runat="server" Text="Specimen Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRSpecimenType" runat="server" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Laboratory Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRLabUnit" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Parent Test Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboParentTestName" runat="server" Width="300px" EnableLoadOnDemand="True"
                                HighlightTemplatedItems="True" MarkFirstMatch="False" OnItemDataBound="cboParentTestName_ItemDataBound"
                                OnItemsRequested="cboParentTestName_ItemsRequested">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="Profile Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:RadioButtonList runat="server" ID="rblProfileType" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" Text="Header" />
                                <asp:ListItem Value="1" Text="Detail" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20"></td>
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
                    <tr>
                        <td class="label">E-Klaim Faktor Penunjang
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSREklaimFactorGroup" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label" />
                        <td class="entry" colspan="3">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkIsAdminCalculation" runat="server" Text="Admin Calculation" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsAllowVariable" runat="server" Text="Allow Variable" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkIsAllowCito" runat="server" Text="Allow Cito" AutoPostBack="True"
                                            OnCheckedChanged="chkIsAllowCito_CheckedChanged" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsAllowDiscount" runat="server" Text="Allow Discount" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkIsCitoFromStandardReference" runat="server" Text="Cito From Standard Reference" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsDisplayInOrderList" runat="server" Text="Display In Order List" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkIsAssetUtilization" runat="server" Text="Asset Utilization" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsConfidential" runat="server" Text="Confidential" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkIsHasTestResults" runat="server" Text="Has Test Results" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsResultOnSepataredPage" runat="server" Text="Result On Sepatared Page" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkIsCulture" runat="server" Text="Culture Test" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkIsDelegationToNurse" runat="server" Text="Delegation To Nurse" />
                                    </td>
                                </tr>
                            </table>
                        </td>
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
            <telerik:RadTab runat="server" Text="Price History" PageViewID="pgvPriceHistory">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Service Unit" PageViewID="pgvServiceUnit">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Laboratory Result" PageViewID="pgvLabResult">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Laboratory Profile" PageViewID="pgvLabProfile">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Bridging & Integration" PageViewID="pgvAliasName" />
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
                    <EditFormSettings UserControlName="~/Module/RADT/Master/ItemService/ItemConsumptionDetail.ascx"
                        EditFormType="WebUserControl">
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
                        <telerik:GridBoundColumn DataField="ClassName" HeaderText="Class" UniqueName="ClassName"
                            SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="StartingDate" HeaderText="Starting Date"
                            UniqueName="StartingDate" SortExpression="StartingDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="Price" HeaderText="Price"
                            UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
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
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAllowDiscount"
                                    HeaderText="Discount" UniqueName="IsAllowDiscount" SortExpression="IsAllowDiscount"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAllowVariable"
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
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsVisible"
                            HeaderText="Visible" UniqueName="IsVisible" SortExpression="IsVisible"
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
        <telerik:RadPageView ID="pgvLabResult" runat="server">
            <telerik:RadGrid ID="grdLabResult" runat="server" OnNeedDataSource="grdLabResult_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdLabResult_DeleteCommand"
                OnInsertCommand="grdLabResult_InsertCommand" OnUpdateCommand="grdLabResult_UpdateCommand">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID, SequenceNo">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="Sex" HeaderText="Sex"
                            UniqueName="Sex" SortExpression="Sex" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="AgeUnitName" HeaderText="Age Unit"
                            UniqueName="AgeUnitName" SortExpression="AgeUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="AgeMin" HeaderText="Age Min"
                            UniqueName="AgeMin" SortExpression="AgeMin" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="AgeMax" HeaderText="Age Max"
                            UniqueName="AgeMax" SortExpression="AgeMax" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="AnswerTypeName" HeaderText="Value Type" UniqueName="AnswerTypeName"
                            SortExpression="AnswerTypeName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="AnswerTypeReferenceName" HeaderText="Value Reference"
                            UniqueName="AnswerTypeReferenceName" SortExpression="AnswerTypeReferenceName"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="NormalValueMin" HeaderText="Normal Value Min"
                            UniqueName="NormalValueMin" SortExpression="NormalValueMin" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="NormalValueMax" HeaderText="Normal Value Max"
                            UniqueName="NormalValueMax" SortExpression="NormalValueMax" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ItemLaboratoryResult.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ItemLabResultEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvLabProfile" runat="server">
            <telerik:RadGrid ID="grdLabProfile" runat="server" OnNeedDataSource="grdLabProfile_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdLabProfile_DeleteCommand"
                OnInsertCommand="grdLabProfile_InsertCommand" OnUpdateCommand="grdLabProfile_UpdateCommand">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ParentItemID, DetailItemID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ParentItemID" HeaderText="Parent Item ID"
                            UniqueName="ParentItemID" SortExpression="ParentItemID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DetailItemID" HeaderText="Detail Item ID"
                            UniqueName="DetailItemID" SortExpression="DetailItemID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DisplaySequence" HeaderText="Sequence"
                            UniqueName="DisplaySequence" SortExpression="DisplaySequence" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ItemLaboratoryProfile.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ItemLabProfileEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
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
                    <EditFormSettings UserControlName="~\Module\RADT\Master\ItemService\ItemAliasDetail.ascx" EditFormType="WebUserControl">
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
    </telerik:RadMultiPage>
</asp:Content>
