<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="RequestOrderDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Procurement.RequestOrderDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            var i = 0;

            function openWinPickList() {
                i++;

                var oWnd = $find("<%= winPickList.ClientID %>");
                var oit = $find("<%= cboSRItemType.ClientID %>");
                var olid = $find("<%= cboLocationID.ClientID %>");
                var otsu = $find("<%= cboToServiceUnitID.ClientID %>");
                var osupp = $find("<%= cboBusinessPartnerID.ClientID %>");
                var opacc = $find("<%= cboSRProductAccountID.ClientID %>");
                var oicat = $find("<%= cboSRItemCategory.ClientID %>");
                var inv = document.getElementById('<%= chkIsInventoryItem.ClientID %>').checked;
                var suppChecked = document.getElementById('<%= chkIsUsedFilterSupplier.ClientID %>').checked;
                var itemCatChecked = document.getElementById('<%= chkIsUsedFilterItemCategory.ClientID %>').checked;

                var isAssets = document.getElementById('<%= chkIsAssets.ClientID %>').checked;

                if (isAssets) {
                    alert('Picked from Usage History only for items not assets.');
                    return;
                }

                var md = "<%= Request.QueryString["md"] %>";
                var otNo = $find("<%= txtTransactionNo.ClientID %>");
                var url = "";
                if (suppChecked) {
                    if (itemCatChecked) {
                        url = "RequestOrderPickList.aspx?it=" + oit._value + "&lid=" + olid._value + "&wid=" + i + "&tu=" + otsu._value + "&inv=" + inv + "&suppid=" + osupp._value + "&itmcat=" + oicat._value + "&paid=" + opacc._value + "&tno=" + otNo._value + "&md=" + md;
                    }
                    else {
                        url = "RequestOrderPickList.aspx?it=" + oit._value + "&lid=" + olid._value + "&wid=" + i + "&tu=" + otsu._value + "&inv=" + inv + "&suppid=" + osupp._value + "&itmcat=&paid=" + opacc._value + "&tno=" + otNo._value + "&md=" + md;
                    }
                }
                else {
                    if (itemCatChecked) {
                        url = "RequestOrderPickList.aspx?it=" + oit._value + "&lid=" + olid._value + "&wid=" + i + "&tu=" + otsu._value + "&inv=" + inv + "&itmcat=" + oicat._value + "&suppid=&paid=" + opacc._value + "&tno=" + otNo._value + "&md=" + md;
                    }
                    else {
                        url = "RequestOrderPickList.aspx?it=" + oit._value + "&lid=" + olid._value + "&wid=" + i + "&tu=" + otsu._value + "&inv=" + inv + "&suppid=&itmcat=&paid=" + opacc._value + "&tno=" + otNo._value + "&md=" + md;
                    }
                }
                oWnd.setUrl(url);
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinDistReqPickList() {
                i++;
                var oWnd = $find("<%= winPickList.ClientID %>");
                var oit = $find("<%= cboSRItemType.ClientID %>");
                var ofsu = $find("<%= cboFromServiceUnitID.ClientID %>");
                var olid = $find("<%= cboLocationID.ClientID %>");
                var osupp = $find("<%= cboBusinessPartnerID.ClientID %>");
                var opacc = $find("<%= cboSRProductAccountID.ClientID %>");
                var oicat = $find("<%= cboSRItemCategory.ClientID %>");
                var inv = document.getElementById('<%= chkIsInventoryItem.ClientID %>').checked;
                var suppChecked = document.getElementById('<%= chkIsUsedFilterSupplier.ClientID %>').checked;
                var itemCategoryChecked = document.getElementById('<%= chkIsUsedFilterItemCategory.ClientID %>').checked;

                var isAssets = document.getElementById('<%= chkIsAssets.ClientID %>').checked;

                if (isAssets) {
                    alert('Picked from Unit Request only for items not assets.');
                    return;
                }

                var url = "";
                if (suppChecked) {
                    if (itemCategoryChecked) {
                        url = "RequestOrderByDistReqPickList.aspx?it=" + oit._value + "&lid=" + olid._value + "&wid=" + i + "&fu=" + ofsu._value + "&inv=" + inv + "&suppid=" + osupp._value + "&itmcat=" + oicat._value + "&paid=" + opacc._value;
                    }
                    else {
                        url = "RequestOrderByDistReqPickList.aspx?it=" + oit._value + "&lid=" + olid._value + "&wid=" + i + "&fu=" + ofsu._value + "&inv=" + inv + "&suppid=" + osupp._value + "&itmcat=&paid=" + opacc._value;
                    }
                }
                else {
                    if (itemCategoryChecked) {
                        url = "RequestOrderByDistReqPickList.aspx?it=" + oit._value + "&lid=" + olid._value + "&wid=" + i + "&fu=" + ofsu._value + "&inv=" + inv + "&itmcat=" + oicat._value + "&suppid=&paid=" + opacc._value;
                    }
                    else {
                        url = "RequestOrderByDistReqPickList.aspx?it=" + oit._value + "&lid=" + olid._value + "&wid=" + i + "&fu=" + ofsu._value + "&inv=" + inv + "&suppid=&itmcat=&paid=" + opacc._value;
                    }
                }

                oWnd.setUrl(url);
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd) {
                if (oWnd.argument && oWnd.argument.command == 'rebind')
                    __doPostBack("<%= grdItemTransactionItem.UniqueID %>", "rebind");
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="820px" Height="600px" Behavior="Close, Move, Maximize"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" AutoSize="false"
        ReloadOnShow="true" OnClientClose="onClientClose" ID="winPickList">
    </telerik:RadWindow>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Request No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Request Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                DatePopupButton-Enabled="false" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvTransactionDate" runat="server" ErrorMessage="Request Date required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Planning Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtPlanningDate" runat="server" Width="100px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvPlanningDate" runat="server" ErrorMessage="Planning Date required."
                                ValidationGroup="entry" ControlToValidate="txtPlanningDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Request Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboFromServiceUnitID" runat="server" Width="300px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboFromServiceUnitID_SelectedIndexChanged" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvFromServiceUnitID" runat="server" ErrorMessage="Request Unit required."
                                ValidationGroup="entry" ControlToValidate="cboFromServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblLocationID" runat="server" Text="Location"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboLocationID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr id="trServiceUnitCostID" runat="server">
                        <td class="label">
                            <asp:Label ID="lblServiceUnitCostID" runat="server" Text="Cost For Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnitCostID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvServiceUnitCostID" runat="server" ErrorMessage="Cost For Unit required."
                                ValidationGroup="entry" ControlToValidate="cboServiceUnitCostID" SetFocusOnError="True"
                                Width="100%" Visible="False">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemType" runat="server" Text="Item Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboSRItemType" runat="server" Width="282px" AutoPostBack="True"
                                            OnSelectedIndexChanged="cboSRItemType_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsUsedFilterItemCategory" runat="server" Enabled="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trSRItemCategory" visible="True">
                        <td class="label">
                            <asp:Label ID="lblSRItemCategory" runat="server" Text="Item Category"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRItemCategory" runat="server" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trReferenceNo">
                        <td class="label">
                            <asp:Label ID="lblReferenceNo" runat="server" Text="From Transfer No" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtReferenceNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblToServiceUnitID" runat="server" Text="Purchasing Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboToServiceUnitID" runat="server" Width="282px" AllowCustomText="true"
                                            Filter="Contains">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsUsedFilterSupplier" runat="server" Enabled="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvToServiceUnitID" runat="server" ErrorMessage="Purchasing Unit required."
                                ValidationGroup="entry" ControlToValidate="cboToServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trSupplier" visible="False">
                        <td class="label">
                            <asp:Label ID="lblBusinessPartnerID" runat="server" Text="Supplier Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboBusinessPartnerID" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboSupplierID_ItemDataBound"
                                ValidationGroup="other" AutoPostBack="true" OnSelectedIndexChanged="cboBusinessPartnerID_SelectedIndexChanged"
                                OnItemsRequested="cboSupplier_ItemsRequested" EmptyMessage="Select...">
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCategorization" runat="server" Text="Inventory Category"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboCategorization" runat="server" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="pnlProductAccountID" visible="True">
                        <td class="label">
                            <asp:Label ID="lblSRProductAccountID" runat="server" Text="Product Account"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRProductAccountID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
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
                        <td class="label"></td>
                        <td class="entry" colspan="3">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkIsInventoryItem" runat="server" Text="Inventory Item" AutoPostBack="true"
                                            OnCheckedChanged="chkIsInventoryItem_CheckedChanged" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsNonMasterOrder" runat="server" Text="Non Master Order" AutoPostBack="true"
                                            OnCheckedChanged="chkIsNonMasterOrder_CheckedChanged" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsAssets" runat="server" Text="Assets" AutoPostBack="true" OnCheckedChanged="chkIsAssets_CheckedChanged" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsConsignment" runat="server" Text="Consignment" AutoPostBack="true"
                                            OnCheckedChanged="chkIsConsignment_CheckedChanged" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsClosed" runat="server" Text="Closed" Enabled="false" />
                            <asp:CheckBox ID="chkIsConsignmentAlreadyReceived" runat="server" Text="ConsignmentAlreadyReceived" Enabled="false" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItemTransactionItem" runat="server" OnNeedDataSource="grdItemTransactionItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItemTransactionItem_UpdateCommand"
        ShowFooter="true" OnDeleteCommand="grdItemTransactionItem_DeleteCommand" OnInsertCommand="grdItemTransactionItem_InsertCommand">
        <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, SequenceNo">
            <FooterStyle Height="40px" Font-Bold="true" />
            <CommandItemTemplate>
                &nbsp;
                <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# !grdItemTransactionItem.MasterTableView.IsItemInserted %>'>
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/insert16.png" />
                    &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new record"></asp:Label>
                </asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="lbPickList" runat="server" Visible='<%# !grdItemTransactionItem.MasterTableView.IsItemInserted %>'
                    OnClientClick="javascript:openWinPickList();return false;">
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/views16.png" />&nbsp;<asp:Label
                        runat="server" ID="lblPicList" Text="Picked from Usage History"></asp:Label>
                </asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="lbPickList2" runat="server" Visible='<%# !grdItemTransactionItem.MasterTableView.IsItemInserted %>'
                    OnClientClick="javascript:openWinDistReqPickList();return false;">
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/views16.png" />&nbsp;<asp:Label
                        runat="server" ID="Label2" Text="Picked from Unit Request"></asp:Label>
                </asp:LinkButton>
            </CommandItemTemplate>
            <CommandItemStyle Height="29px" />
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Description" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                    Visible="False" />
                <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="TemplateItemName">
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' /><br />
                        <i><asp:Label ID="lblAdditionalInfo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AdditionalInfo") %>' /></i>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Specification" HeaderText="Specification" UniqueName="Specification"
                    SortExpression="Specification" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                    Visible="False" />
                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Minimum" HeaderText="Min"
                    UniqueName="Minimum" SortExpression="Minimum" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Maximum" HeaderText="Max"
                    UniqueName="Maximum" SortExpression="Maximum" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="BalanceSG" HeaderText="Balance SG"
                    UniqueName="BalanceSG" SortExpression="BalanceSG" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Balance" HeaderText="Balance Loc"
                    UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="BalanceTotal" HeaderText="Balance Total"
                    UniqueName="BalanceTotal" SortExpression="BalanceTotal" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="SRMasterBaseUnit" HeaderText="Unit"
                    UniqueName="SRMasterBaseUnit" SortExpression="SRMasterBaseUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Quantity" HeaderText="Req. Qty"
                    UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="QuantityFinishInBaseUnit"
                    HeaderText="Order Qty" UniqueName="QuantityFinishInBaseUnit" SortExpression="QuantityFinishInBaseUnit"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="SRItemUnit" HeaderText="Req. Unit"
                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="Discount1Percentage"
                    HeaderText="Disc 1 (%)" UniqueName="Discount1Percentage" SortExpression="Discount1Percentage"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="Discount2Percentage"
                    HeaderText="Disc 2 (%)" UniqueName="Discount2Percentage" SortExpression="Discount2Percentage"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Discount" HeaderText="Disc Amount"
                    UniqueName="Discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridCalculatedColumn HeaderStyle-Width="120px" HeaderText="Total" UniqueName="Total"
                    DataType="System.Double" DataFields="Price,Discount1Percentage,Discount2Percentage, Quantity"
                    Expression="({0}*{3}) - ((({0}*{1}/100) + (({0} - ({0}*{1}/100)) * {2}/100)) * {3})"
                    FooterText=" " FooterStyle-HorizontalAlign="Right" Aggregate="Sum" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsClosed" HeaderText="Closed"
                    UniqueName="IsClosed" SortExpression="IsClosed" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="RequestOrderItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="ItemTransactionItemEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
