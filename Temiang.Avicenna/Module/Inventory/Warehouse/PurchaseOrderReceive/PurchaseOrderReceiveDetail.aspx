<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="PurchaseOrderReceiveDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Warehouse.PurchaseOrderReceiveDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function openWinPr() {
                var oWnd = $find("<%= winPr.ClientID %>");
                var unit = $find("<%= cboToServiceUnitID.ClientID %>");

                oWnd.setUrl("PurchaseOrderReceivePickList.aspx?unitId=" + unit.get_value() + '&cons=' + '<%= Request.QueryString["cons"] %>');
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

            function viewEdDetail(seqNo) {
                var oWnd = $find("<%= winEdItem.ClientID %>");
                var otrn = $find("<%= txtTransactionNo.ClientID %>");
                var oit = $find("<%= cboSRItemType.ClientID %>");
                oWnd.setUrl('../ItemExpiryDate/ItemExpiryDateDetail.aspx?trn=' + otrn.get_value() + '&sqn=' + seqNo + '&itype=' + oit._value + '&ccm=submitEd&cet=<%=grdItemTransactionItem.UniqueID %>');

                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }
            function onClientCloseEd(oWnd, args) {
                <%--if (oWnd.argument && oWnd.argument.command != null) {
                    __doPostBack("<%= grdItemTransactionItem.UniqueID %>", "rebind:");
                    oWnd.argument = null;
                }--%>
                var arg = args.get_argument();
                if (arg != null) {
                    if (arg.callbackMethod === 'submitEd') {
                        __doPostBack(arg.eventTarget, arg.eventArgument);
                    }
                }
            }
            function openWinBarcode(id, bc) {
                var oWnd = $find("<%= winBarcode.ClientID %>");
                oWnd.setUrl('UpdateItemBarcode.aspx?id=' + id + '&bc=' + bc + '&ccm=submit&cet=<%=grdItemTransactionItem.UniqueID %>');

                oWnd.set_width(340);
                oWnd.set_height(240);
                oWnd.show();
            }
            function winBarcode_ClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg != null) {
                    if (arg.callbackMethod === 'submit') {
                        __doPostBack(arg.eventTarget, arg.eventArgument);
                    }
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="1000px" Height="600px"
        Behavior="Close, Move, Maximize" ShowContentDuringLoad="False" VisibleStatusbar="false"
        Modal="true" Title="Purchase Order Outstanding" OnClientClose="onClientClose"
        ID="winPr">
    </telerik:RadWindow>
    <telerik:RadWindow runat="server" Animation="None" Width="600px" Height="500px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winEdItem"
        OnClientClose="onClientCloseEd">
    </telerik:RadWindow>
    <telerik:RadWindow runat="server" Animation="None" Width="340px" Height="240px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winBarcode"
        OnClientClose="winBarcode_ClientClose">
    </telerik:RadWindow>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top; width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Received No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Received Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" />
                                    </td>
                                    <td style="display: none">
                                        <asp:CheckBox ID="chkIsTaxable" runat="server" Text="Tax" Enabled="true" AutoPostBack="true"
                                            OnCheckedChanged="chkIsTaxable_CheckedChanged" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTransactionDate" runat="server" ErrorMessage="Transaction Date required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblToServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboToServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" OnSelectedIndexChanged="cboToServiceUnitID_OnSelectedIndexChanged"
                                AutoPostBack="true">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvToServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                                ValidationGroup="entry" ControlToValidate="cboToServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
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
                            <asp:RequiredFieldValidator ID="rfvToLocationID" runat="server" ErrorMessage="Location required."
                                ValidationGroup="entry" ControlToValidate="cboToLocationID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr id="trFromServiceUnitID" runat="server">
                        <td class="label">
                            <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Cost For Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboFromServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr id="trRefNo" runat="server">
                        <td class="label">
                            <asp:Label ID="lblReferenceNo" runat="server" Text="Purchase Order No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtReferenceNoRef" runat="server" Width="300px" MaxLength="20"
                                Visible="false" />
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtReferenceNo" runat="server" Width="157px" MaxLength="20"
                                            ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnGetItem" runat="server" Text="Outstanding" OnClientClick="javascript:openWinPr();return false;" />
                                        <asp:Button ID="btnResetItem" runat="server" Text="Reset" OnClick="btnResetItem_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr id="trCurrType" runat="server">
                        <td class="label">
                            <asp:Label ID="lblCurrencyType" runat="server" Text="Currency Type / Rate" />
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboCurrencyType" runat="server" Width="179px" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtCurrencyRate" runat="server" Width="114px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRItemType" runat="server" Text="Item Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRItemType" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr id="trPOType" runat="server">
                        <td class="label">
                            <asp:Label ID="lblSRPurchaseOrderType" runat="server" Text="Purchase Order Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRPurchaseOrderType" runat="server" Width="300px" Enabled="false" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRPurchaseOrderType" runat="server" ErrorMessage="Term required."
                                ValidationGroup="entry" ControlToValidate="cboSRPurchaseOrderType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBusinessPartnerID" runat="server" Text="Supplier Name" />
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="cboBusinessPartnerID" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboSupplierID_ItemDataBound" 
                                AutoPostBack="true" OnSelectedIndexChanged="cboBusinessPartnerID_OnSelectedIndexChanged"
                                ValidationGroup="other"
                                OnItemsRequested="cboSupplier_ItemsRequested" EmptyMessage="Select..." >
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvBusinessPartnerID" runat="server" ErrorMessage="Supplier Name required."
                                ValidationGroup="entry" ControlToValidate="cboBusinessPartnerID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr id="trTaxType" runat="server">
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rblTypesOfTaxes" runat="server" RepeatDirection="Horizontal"
                                OnTextChanged="rblTypesOfTaxes_OnTextChanged" AutoPostBack="True" >
                                <asp:ListItem>Exclude Tax</asp:ListItem>
                                <asp:ListItem>Include Tax</asp:ListItem>
                                <asp:ListItem>No Tax</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblInvoiceNo" runat="server" Text="Invoice Supplier No / Date" />
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtInvoiceNo" runat="server" Width="200px" MaxLength="100" />
                                    </td>
                                    <td style="width: 5px">
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtInvoiceSupplierDate" runat="server" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvInvoiceNo" runat="server" ErrorMessage="Invoice Supplier No. Required."
                                ValidationGroup="entry" ControlToValidate="txtInvoiceNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr id="trDeliveryNo" runat="server">
                        <td class="label">
                            <asp:Label ID="lblDeliveryOrdersNo" runat="server" Text="Delivery Orders No" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtDeliveryOrdersNo" runat="server" Width="300px" MaxLength="50" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <asp:Panel runat="server" ID="pnlPphNonFixedValue">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPphType" runat="server" Text="PPh Type"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadComboBox runat="server" ID="cboSRPph" Width="154px" AllowCustomText="true"
                                                Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboSRPph_OnSelectedIndexChanged">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtPphPercentage" runat="server" Width="80px" MaxLength="10"
                                                MaxValue="99.99" MinValue="0" NumberFormat-DecimalDigits="2" Type="Percent" ReadOnly="True" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPphAmount" runat="server" Text="PPh Amount"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtPphAmount" runat="server" Width="150px" MaxLength="10"
                                    MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                            </td>
                            <td width="20">
                            </td>
                            <td>
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry" colspan="3">
                            <table width="100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 18%">
                                        <asp:CheckBox ID="chkIsInventoryItem" runat="server" Text="Inventory Item" Enabled="False" />
                                    </td>
                                    <td style="width: 18%">
                                        <asp:CheckBox ID="chkIsNonMasterOrder" runat="server" Text="Non Master Order" Enabled="False" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsAssets" runat="server" Text="Assets" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trCons" runat="server">
                        <td class="label">
                        </td>
                        <td class="entry" colspan="3">
                            <table width="100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 18%">
                                        <asp:CheckBox ID="chkIsConsignment" runat="server" Text="Consignment" Enabled="False" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsConsignmentAlreadyReceived" runat="server" Text="Item Already Received"
                                            Enabled="False" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server" id="trProductAccount">
                        <td class="label">
                            <asp:Label ID="lblSRProductAccountID" runat="server" Text="Product Account"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRProductAccountID" runat="server" Width="300px" Enabled="false" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr id="trConsLoc" runat="server">
                        <td class="label">
                            <asp:Label ID="lblFromLocationID" runat="server" Text="Location For Consignment"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboFromLocationID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsApproved" runat="server" Text="Approved" Enabled="false" />
                            <asp:CheckBox ID="chkIsVoid" runat="server" Text="Void" Enabled="false" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItemTransactionItem" AllowPaging="true" PageSize="10" runat="server"
        OnNeedDataSource="grdItemTransactionItem_NeedDataSource" AutoGenerateColumns="False"
        GridLines="None" OnUpdateCommand="grdItemTransactionItem_UpdateCommand" OnDeleteCommand="grdItemTransactionItem_DeleteCommand"
        OnInsertCommand="grdItemTransactionItem_InsertCommand">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, SequenceNo">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsBonusItem" HeaderText="Bonus"
                    UniqueName="IsBonusItem" SortExpression="IsBonusItem" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Description" HeaderText="Item Name" UniqueName="Description"
                    SortExpression="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="FabricName" HeaderText="Factory" UniqueName="FabricName"
                    SortExpression="FabricName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="ConversionFactor"
                    HeaderText="Conversion" UniqueName="ConversionFactor" SortExpression="ConversionFactor"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="75px" DataField="Quantity" HeaderText="Qty"
                    UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRItemUnit" HeaderText="Unit"
                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="110px" DataField="Price" HeaderText="Price"
                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Discount1Percentage"
                    HeaderText="Disc 1 (%)" UniqueName="Discount1Percentage" SortExpression="Discount1Percentage"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Discount2Percentage"
                    HeaderText="Disc 2 (%)" UniqueName="Discount2Percentage" SortExpression="Discount2Percentage"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Discount" HeaderText="Disc Amount"
                    UniqueName="Discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridCalculatedColumn HeaderStyle-Width="120px" HeaderText="Total" UniqueName="TotalPrice"
                    DataType="System.Double" DataFields="Price, Discount, Quantity" Expression="(({0}-{1}) * {2})"
                    FooterText=" " FooterStyle-HorizontalAlign="Right" Aggregate="Sum" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="BatchNumber" HeaderText="Batch No."
                    UniqueName="BatchNumber" SortExpression="BatchNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ExpiredDate" HeaderText="Expiry Date"
                    UniqueName="ExpiredDate" SortExpression="ExpiredDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn UniqueName="editED" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsControlExpired").Equals(false) ? string.Format("<img src=\"../../../../Images/calendar16_d.png\" border=\"0\" />") :
                                            string.Format("<a href=\"#\" onclick=\"viewEdDetail('{0}'); return false;\">{1}</a>",
                                            DataBinder.Eval(Container.DataItem, "SequenceNo"), "<img src=\"../../../../Images/calendar16.png\" border=\"0\" title=\"Batch No. & ED\" />"))%>
                        <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsControlExpired")) && Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsNotCompleteED")) ? "<img src=\"../../../../Images/Animated/warning16.gif\" border=\"0\" alt=\"ED\" title=\"ED Not Complete\" />" : string.Empty%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    HeaderText="Barcode" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="60px">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"openWinBarcode('{0}','{1}'); return false;\">{1}&nbsp;<img src=\"../../../../Images/Toolbar/ordering16.png\" border=\"0\"  /></a>",
                                                                                                DataBinder.Eval(Container.DataItem, "ItemID"), DataBinder.Eval(Container.DataItem, "Barcode"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="PurchaseOrderReceiveItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="ItemTransactionItemEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="false" />
        </ClientSettings>
    </telerik:RadGrid>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionAmount" runat="server" Text="Transaction Amount" />
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTransactionAmount" runat="server" Width="100px"
                                MaxLength="16" MinValue="0" ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblDiscountAmount" runat="server" Text="Global Discount Amount" />
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtDiscountAmount" runat="server" Width="100px" MaxLength="16"
                                MinValue="0" AutoPostBack="true" OnTextChanged="txtDiscountAmount_TextChanged" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvDiscountAmount" runat="server" ErrorMessage="Discount Amount required."
                                ValidationGroup="entry" ControlToValidate="txtDiscountAmount" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReceive" runat="server" Text="Received Amount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtReceiveAmount" runat="server" Width="100px" MaxLength="16"
                                MaxValue="9999999999999.99" MinValue="0" ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAmountTaxed" runat="server" Text="Amount Taxed"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtAmountTaxed" runat="server" Width="100px" MaxLength="16"
                                MaxValue="9999999999999.99" MinValue="0" ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTaxPercentage" runat="server" Text="Tax Percentage"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTaxPercentage" runat="server" Type="Percent" Width="100px"
                                MaxLength="5" MaxValue="999.99" MinValue="0" ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTaxAmount" runat="server" Text="Tax Amount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTaxAmount" runat="server" Width="100px" MaxLength="16"
                                MaxValue="9999999999999.99" MinValue="0" AutoPostBack="False" OnTextChanged="txtTaxAmount_TextChanged" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr runat="server" id="trShippingCharges">
                        <td class="label">
                            <asp:Label ID="lblShippingCharges" runat="server" Text="Shipping Charges"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtShippingCharges" runat="server" Width="100px" MaxLength="16"
                                MaxValue="9999999999999.99" MinValue="0" AutoPostBack="True" OnTextChanged="txtShippingCharges_TextChanged" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblStampAmount" runat="server" Text="Stamp Amount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtStampAmount" runat="server" Width="100px" MaxLength="16"
                                MaxValue="9999999999999.99" MinValue="0" AutoPostBack="True" OnTextChanged="txtStampAmount_TextChanged" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblAdvanceAmount" runat="server" Text="Down Payment Amount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtAdvanceAmount" runat="server" Width="100px" MaxLength="16"
                                MaxValue="9999999999999.99" MinValue="0" AutoPostBack="True" OnTextChanged="AdvanceAmount_TextChanged" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTotal" runat="server" Width="100px" MaxLength="16"
                                MaxValue="9999999999999.99" MinValue="0" ReadOnly="True" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
