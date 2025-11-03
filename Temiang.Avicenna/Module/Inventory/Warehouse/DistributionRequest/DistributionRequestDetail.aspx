<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="DistributionRequestDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Warehouse.DistributionRequestDetail"
    Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            var i = 0;
            function openWinPickList() {
                i++;

                var cbofromLoc = $find("<%= cboFromLocationID.ClientID %>");
                if (cbofromLoc != null) {
                    if (cbofromLoc.get_value() == '') {
                        alert('From Location is required.');
                        return;
                    }
                }

                var cbotoLoc = $find("<%= cboToLocationID.ClientID %>");
                if (cbotoLoc != null) {
                    if (cbotoLoc.get_value() == '') {
                        alert('To Location is required.');
                        return;
                    }
                }

                var oWnd = $find("<%= winPickList.ClientID %>");
                var oit = $find("<%= cboSRItemType.ClientID %>");
                var olid = $find("<%= cboFromLocationID.ClientID %>");
                var otlid = $find("<%= cboToLocationID.ClientID %>");
                var oigid = $find("<%= cboItemGroupID.ClientID %>");

                oWnd.setUrl("DistributionRequestPickList.aspx?it=" + oit.get_selectedItem().get_value() + "&lid=" + olid.get_selectedItem().get_value() + "&tlid=" + otlid.get_selectedItem().get_value() + "&igid=" + (oigid.get_selectedItem() === null ? "" : oigid.get_selectedItem().get_value()) + "&wid=" + i);
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinUsedPickList() {
                i++;

                var cbofromLoc = $find("<%= cboFromLocationID.ClientID %>");
                if (cbofromLoc != null) {
                    if (cbofromLoc.get_value() == '') {
                        alert('From Location is required.');
                        return;
                    }
                }

                var cbotoLoc = $find("<%= cboToLocationID.ClientID %>");
                if (cbotoLoc != null) {
                    if (cbotoLoc.get_value() == '') {
                        alert('To Location is required.');
                        return;
                    }
                }


                var oWnd = $find("<%= winPickList.ClientID %>");
                var oit = $find("<%= cboSRItemType.ClientID %>");
                var olid = $find("<%= cboFromLocationID.ClientID %>");
                var oigid = $find("<%= cboItemGroupID.ClientID %>");
                var otlid = $find("<%= cboToLocationID.ClientID %>");

                oWnd.setUrl("DistributionRequestByUsedPickList.aspx?it=" + oit.get_selectedItem().get_value() + "&lid=" + olid.get_selectedItem().get_value() + "&tlid=" + otlid.get_selectedItem().get_value() + "&igid=" + (oigid.get_selectedItem() === null ? "" : oigid.get_selectedItem().get_value()) + "&wid=" + i);
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinTemplatePickList() {
                i++;

                var cbofromLoc = $find("<%= cboFromLocationID.ClientID %>");
                if (cbofromLoc != null) {
                    if (cbofromLoc.get_value() == '') {
                        alert('From Location is required.');
                        return;
                    }
                }

                var oWnd = $find("<%= winTemplatePickList.ClientID %>");
                var oit = $find("<%= cboSRItemType.ClientID %>");
                var olid = $find("<%= cboFromLocationID.ClientID %>");
                var oigid = $find("<%= cboItemGroupID.ClientID %>");

                oWnd.setUrl("DistributionRequestByTemplatePickList.aspx?it=" + oit.get_selectedItem().get_value() + "&lid=" + olid.get_selectedItem().get_value() + "&igid=" + (oigid.get_selectedItem() === null ? "" : oigid.get_selectedItem().get_value()) + "&wid=" + i);
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd) {
                //Jika apply di click
                var arg = oWnd.argument;
                if (arg) {
                    $find("<%= RadAjaxManager.GetCurrent(Page).ClientID %>").ajaxRequest("");
                }
            }

            function txtBarcodeEntryKeyPress(sender, eventArgs) {
                var code = eventArgs.get_keyCode();
                if (code == 13) {
                    eventArgs.set_cancel(true); // Supaya tidak membuka edit grid
                    __doPostBack(sender._clientID.replace(/_/g, "$"), "addwithbarcode|" + sender.get_value());
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="900px" Height="605px" Behavior="Close, Move, Maximize"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" AutoSize="false"
        Title="Request Order Pending" ReloadOnShow="true" OnClientClose="onClientClose"
        ID="winPickList">
    </telerik:RadWindow>
    <telerik:RadWindow runat="server" Animation="None" Width="900px" Height="605px" Behavior="Close, Move, Maximize"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" AutoSize="false"
        Title="Template List" ReloadOnShow="true" OnClientClose="onClientClose"
        ID="winTemplatePickList">
    </telerik:RadWindow>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="True" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                DatePopupButton-Enabled="false" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvTransactionDate" runat="server" ErrorMessage="Transaction Date required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboFromServiceUnitID" runat="server" Width="300px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboFromServiceUnitID_SelectedIndexChanged" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvFromServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                                ValidationGroup="entry" ControlToValidate="cboFromServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFromLocationID" runat="server" Text="Location"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboFromLocationID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvFromLocationID" runat="server" ErrorMessage="From Location required."
                                ValidationGroup="entry" ControlToValidate="cboFromLocationID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemType" runat="server" Text="Item Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRItemType" runat="server" Width="300px" AutoPostBack="True"
                                OnSelectedIndexChanged="cboSRItemType_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemGroupID" runat="server" Text="Item Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboItemGroupID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboItemGroupID_ItemDataBound"
                                OnItemsRequested="cboItemGroupID_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemGroupID")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "ItemGroupName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblToServiceUnitID" runat="server" Text="Request To Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboToServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboToServiceUnitID_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvToServiceUnitID" runat="server" ErrorMessage="Request To required."
                                ValidationGroup="entry" ControlToValidate="cboToServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblToLocationID" runat="server" Text="Location"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboToLocationID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvToLocationID" runat="server" ErrorMessage="To Location required."
                                ValidationGroup="entry" ControlToValidate="cboToLocationID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trServiceUnitCostID" visible="False">
                        <td class="label">
                            <asp:Label ID="lblServiceUnitCostID" runat="server" Text="Cost To Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnitCostID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvServiceUnitCostID" runat="server" ErrorMessage="Cost To Unit required."
                                ValidationGroup="entry" ControlToValidate="cboToServiceUnitID" SetFocusOnError="True"
                                Width="100%" Visible="False">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
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
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsVoid" runat="server" Text="Void" Enabled="false" Visible="false" />
                            <asp:CheckBox ID="chkIsApproved" runat="server" Text="Approved" Enabled="false" Visible="false" />
                            <asp:CheckBox ID="chkIsClosed" runat="server" Text="Closed" Enabled="false" />
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
        OnDeleteCommand="grdItemTransactionItem_DeleteCommand" OnInsertCommand="grdItemTransactionItem_InsertCommand">
        <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, SequenceNo">
            <CommandItemTemplate>
                &nbsp;&nbsp;&nbsp;Barcode Scan&nbsp;&nbsp;
                <telerik:RadTextBox ID="txtBarcodeEntry" runat="server" Width="300px" AutoCompleteType="Disabled"
                    SelectionOnFocus="SelectAll">
                    <ClientEvents OnKeyPress="txtBarcodeEntryKeyPress"></ClientEvents>
                </telerik:RadTextBox>
                &nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# !grdItemTransactionItem.MasterTableView.IsItemInserted %>'>
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/insert16.png" />
                    &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new record"></asp:Label>
                </asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="lbPickList" runat="server" Visible='<%# !grdItemTransactionItem.MasterTableView.IsItemInserted %>'
                    OnClientClick="javascript:openWinPickList();return false;">
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/views16.png" />&nbsp;<asp:Label
                        runat="server" ID="lblPicList" Text="Pick from Stock Minimum"></asp:Label>
                </asp:LinkButton>
                <asp:LinkButton ID="lbUsedPickList" runat="server" Visible='<%# !grdItemTransactionItem.MasterTableView.IsItemInserted %>'
                    OnClientClick="javascript:openWinUsedPickList();return false;">
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/views16.png" />&nbsp;<asp:Label
                        runat="server" ID="Label1" Text="Pick from Usage History"></asp:Label>
                </asp:LinkButton>
                <asp:LinkButton ID="lbTemplatePickList" runat="server" Visible='<%# !grdItemTransactionItem.MasterTableView.IsItemInserted %>'
                    OnClientClick="javascript:openWinTemplatePickList();return false;">
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/views16.png" />&nbsp;<asp:Label
                        runat="server" ID="Label2" Text="Pick from Template"></asp:Label>
                </asp:LinkButton>
            </CommandItemTemplate>
            <CommandItemStyle Height="29px" />
            <ColumnGroups>
                <telerik:GridColumnGroup HeaderText="Balance Info" Name="BalanceInfo" HeaderStyle-HorizontalAlign="Center">
                </telerik:GridColumnGroup>
            </ColumnGroups>
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="SRItemBinName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRItemBinName" HeaderText="Bin"
                    UniqueName="ItemBin" SortExpression="SRItemBinName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Minimum" HeaderText="Min"
                    UniqueName="Minimum" SortExpression="Minimum" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Maximum" HeaderText="Max"
                    UniqueName="Maximum" SortExpression="Maximum" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Quantity" HeaderText="Qty"
                    UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="QuantityFinishInBaseUnit"
                    HeaderText="Distribution" UniqueName="QuantityFinishInBaseUnit" SortExpression="QuantityFinishInBaseUnit"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"
                    Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRItemUnit" HeaderText="Unit"
                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />

                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Balance" HeaderText="From Location"
                    UniqueName="BalanceFrom" SortExpression="Balance" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" ColumnGroupName="BalanceInfo" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Balance2" HeaderText="To Location"
                    UniqueName="BalanceTo" SortExpression="Balance2" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" ColumnGroupName="BalanceInfo" />

                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsClosed" HeaderText="Closed"
                    UniqueName="IsClosed" SortExpression="IsClosed" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="DistributionRequestItemDetail.ascx" EditFormType="WebUserControl">
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
