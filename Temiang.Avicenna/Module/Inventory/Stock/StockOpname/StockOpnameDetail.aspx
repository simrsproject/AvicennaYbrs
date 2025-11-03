<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="StockOpnameDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Stock.StockOpnameDetail" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            var i = 0;

            function openWinStockOpnameAdd(initial) {
                i++;
                var oWnd = $find("<%= winStockOpnameAdd.ClientID %>");
                oWnd.setUrl("StockOpnameAdd.aspx?wid=" + i);
                oWnd.show();
            }

            //            function openWinStockOpnameAdd() {
            //                i++;
            //                var oWnd = $find("<%= winStockOpnameAdd.ClientID %>");
            //                oWnd.setUrl("StockOpnameAdd.aspx?wid=" + i);
            //                oWnd.show();
            //            }

            //            function onClientClose(oWnd) {
            //                if (oWnd.argument && oWnd.argument.command == 'ok')
            //                    __doPostBack("<%= grdItemTransactionItem.UniqueID %>", "new");
            //            }
            function onClientClose(oWnd) {
                if (oWnd.argument && oWnd.argument.command == 'ok') {
                    var url = '<%= HttpContext.Current.Request.Url.AbsolutePath %>?md=view&id=' + oWnd.argument.trno;
                    window.location = url;
                }
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }
            function Close() {
                GetRadWindow().close();
            }
            function onWinBarcode_ClientClose(oWnd) {
                if (oWnd.argument && oWnd.argument.command == 'ok')
                    __doPostBack("<%= grdItemTransactionItem.UniqueID %>", "new");
            }
            function winEditLine_ClientClose(oWnd) {
                if (oWnd.argument && oWnd.argument.command == 'ok') {
                    __doPostBack("<%= grdItemTransactionItem.UniqueID %>", "rebind");
                }
            }
            function editNote(seqno) {
                var oWnd = window.$find("<%= winEditLine.ClientID %>");
                var url = 'StockOpnameLineNote.aspx?trno=<%= txtTransactionNo.Text %>&seqno=' + seqno;
                oWnd.setUrl(url);
                oWnd.show();
                oWnd.setSize(600, 300);
                oWnd.center();
            }
            function editQty(seqno) {
                var oWnd = window.$find("<%= winEditLine.ClientID %>");
                var url = 'StockOpnameLineQty.aspx?loc=<%= txtLocationID.Text %>&trno=<%= txtTransactionNo.Text %>&seqno=' + seqno;
                oWnd.setUrl(url);
                oWnd.show();
                oWnd.setSize(600, 300);
                oWnd.center();
            }
            function txtBarcodeEntryKeyPress(sender, eventArgs) {
                var code = eventArgs.get_keyCode();
                if (code == 13) {
                    eventArgs.set_cancel(true); // Supaya tidak membuka edit grid
                    var oWnd = window.$find("<%= winBarcode.ClientID %>");
                    var barcode = sender.get_element().value;

                    var url = 'AddItemUsingBarcode.aspx?trno=<%= txtTransactionNo.Text %>&bc=' + barcode + '&it=<%= txtSRItemType.Text %>&loc=<%= txtLocationID.Text %>';
                    oWnd.setUrl(url);
                    oWnd.show();
                }
            }
            function AddItemUsingBarcode() {
                var oWnd = window.$find("<%= winBarcode.ClientID %>");
                var url = 'AddItemUsingBarcode.aspx?trno=<%= txtTransactionNo.Text %>&it=<%= txtSRItemType.Text %>&loc=<%= txtLocationID.Text %>&gcid=<%= grdItemTransactionItem.ClientID %>';
                oWnd.setUrl(url);
                oWnd.show();
            }
            function AddItemEd(type) {
                var oWnd = window.$find("<%= winBarcode.ClientID %>");
                var pageNo = $find("<%= cboPageNo.ClientID %>");
                var url = 'AddItemEd.aspx?trno=<%= txtTransactionNo.Text %>&it=<%= txtSRItemType.Text %>&loc=<%= txtLocationID.Text %>&gcid=<%= grdItemTransactionItem.ClientID %>' + '&pageNo=' + pageNo.get_value() + '&type=' + type;
                oWnd.setUrl(url);
                oWnd.show();
            }
            function RebindGridItem(arg) {
                __doPostBack("<%= grdItemTransactionItem.UniqueID %>", arg);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="500px" Height="450px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" AutoSize="false"
        ReloadOnShow="true" OnClientClose="onClientClose" ID="winStockOpnameAdd">
    </telerik:RadWindow>
    <telerik:RadWindow runat="server" Animation="None" Width="500px" Height="450px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" AutoSize="false"
        ReloadOnShow="true" OnClientClose="winEditLine_ClientClose" ID="winEditLine">
    </telerik:RadWindow>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="500px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" AutoSize="false"
        ReloadOnShow="true" OnClientClose="onWinBarcode_ClientClose" ID="winBarcode">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="fw_tbarData">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fw_tbarData" />
                    <telerik:AjaxUpdatedControl ControlID="fw_hdnDataMode" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelInfo" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelStatus" />
                    <telerik:AjaxUpdatedControl ControlID="fw_radNotif" />
                    <telerik:AjaxUpdatedControl ControlID="cboPageNo" />
                    <telerik:AjaxUpdatedControl ControlID="grdItemTransactionItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdItemTransactionItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemTransactionItem" />
                    <telerik:AjaxUpdatedControl ControlID="cboPageNo" />
                    <telerik:AjaxUpdatedControl ControlID="fw_tbarData" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelInfo" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelStatus" />
                    <telerik:AjaxUpdatedControl ControlID="fw_hdnDataMode" />
                    <telerik:AjaxUpdatedControl ControlID="fw_radNotif" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboPageNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemTransactionItem" />
                    <telerik:AjaxUpdatedControl ControlID="fw_tbarData" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelInfo" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelStatus" />
                    <telerik:AjaxUpdatedControl ControlID="cboPageNo" />
                    <telerik:AjaxUpdatedControl ControlID="fw_hdnDataMode" />
                    <telerik:AjaxUpdatedControl ControlID="fw_radNotif" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="True" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvTransactionNo" runat="server" ErrorMessage="Transaction No required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" ReadOnly="True" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnitName" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFromLocationID" runat="server" Text="Location"></asp:Label>
                        </td>
                        <td class="entry">
                            <table width="100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 50px">
                                        <telerik:RadTextBox ID="txtLocationID" runat="server" Width="80px" ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtLocationName" runat="server" Width="215px" ReadOnly="True" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRItemType" runat="server" Text="Item Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSRItemType" runat="server" Width="100px" Visible="False" />
                            <telerik:RadTextBox ID="txtSRItemTypeName" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="True" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPage" runat="server" Text="Page"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboPageNo" Width="100px" AutoPostBack="true"
                                NoWrap="true" OnSelectedIndexChanged="cboPageNo_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItemTransactionItem" runat="server" OnNeedDataSource="grdItemTransactionItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" AllowPaging="true" AllowSorting="false"
        PageSize="100">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="TransactionNo, SequenceNo">
            <CommandItemTemplate>
                &nbsp;
                <%#DataModeCurrent==AppEnum.DataMode.Read && (OnGetStatusMenuApproval() ?? false) && !(IsEnabledStockWithEdControlStatus() ?? false)?
                       (string.Format("<a href=\"#\" title=\"Barcode\" onclick=\"AddItemUsingBarcode(); return false;\"><img src=\"{0}/Images/Toolbar/insert16.png\"/>&nbsp;Entry via Barcode</a>", 
                           Helper.UrlRoot())) : string.Empty%>
                &nbsp;&nbsp;
                <%#DataModeCurrent==AppEnum.DataMode.Read && (OnGetStatusMenuApproval() ?? false) && (IsEnabledStockWithEdControlStatus() ?? false)?
                       (string.Format("<a href=\"#\" title=\"Existing\" onclick=\"AddItemEd('edit'); return false;\"><img src=\"{0}/Images/Toolbar/insert16.png\"/>&nbsp;Add Item With Existing Batch No</a>", 
                           Helper.UrlRoot())) : string.Empty%>
                &nbsp;&nbsp;
                <%#DataModeCurrent==AppEnum.DataMode.Read && (OnGetStatusMenuApproval() ?? false) && (IsEnabledStockWithEdControlStatus() ?? false)?
                       (string.Format("<a href=\"#\" title=\"New\" onclick=\"AddItemEd('new'); return false;\"><img src=\"{0}/Images/Toolbar/insert16.png\"/>&nbsp;Add Item With New Batch No</a>", 
                           Helper.UrlRoot())) : string.Empty%>
            </CommandItemTemplate>
            <CommandItemStyle Height="29px" />
            <Columns>
                <telerik:GridBoundColumn DataField="SequenceNo" UniqueName="SequenceNo" SortExpression="SequenceNo"
                    Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="500px" DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="BatchNumber" HeaderText="Batch No."
                    UniqueName="BatchNumber" SortExpression="BatchNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="ExpiredDate" HeaderText="Expired Date"
                    UniqueName="ExpiredDate" SortExpression="ExpiredDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PrevQty" HeaderText="System Qty"
                    UniqueName="PrevQty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Actual Qty" UniqueName="Quantity"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtQuantity" runat="server" Width="100px" Enabled='<%# (DataModeCurrent == AppEnum.DataMode.Edit) && Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsInventoryItem")) %>'
                            Value='<%# System.Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Quantity")) %>'
                            MinValue="0" IncrementSettings-InterceptMouseWheel="false" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Actual Qty" UniqueName="QuantityEd"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtQuantityEd" runat="server" Width="100px" Enabled='<%# (DataModeCurrent == AppEnum.DataMode.Edit) && Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsInventoryItem")) && (!Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsControlExpired")) || (Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsControlExpired")) && Convert.ToString(DataBinder.Eval(Container.DataItem, "BatchNumber")) != string.Empty) ) %>'
                            Value='<%# System.Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Quantity")) %>'
                            MinValue="0" IncrementSettings-InterceptMouseWheel="false" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="35px" HeaderText="" UniqueName="EditQty"
                    HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# cboPageNo.SelectedValue == "0" ? string.Empty: string.Format("<a href=\"#\" title=\"Qty\" style=\"position: relative;padding: 0 0 0 16px;background: transparent url('{1}/Images/Toolbar/edit16.png') no-repeat 0px 50%;\" onclick=\"editQty('{0}'); return false;\"></a>", 
                                DataBinder.Eval(Container.DataItem, "SequenceNo"), Helper.UrlRoot())%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="35px" HeaderText="" UniqueName="EditQtyEd"
                    HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# cboPageNo.SelectedValue == "0" || !(!Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsControlExpired")) || (Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsControlExpired")) && Convert.ToString(DataBinder.Eval(Container.DataItem, "BatchNumber")) != string.Empty)) ? string.Empty : string.Format("<a href=\"#\" title=\"Qty\" style=\"position: relative;padding: 0 0 0 16px;background: transparent url('{1}/Images/Toolbar/edit16.png') no-repeat 0px 50%;\" onclick=\"editQty('{0}'); return false;\"></a>", 
                                DataBinder.Eval(Container.DataItem, "SequenceNo"), Helper.UrlRoot())%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRItemUnit" HeaderText="Item Unit"
                    UniqueName="SRItemUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ItemBinName" HeaderText="Bin"
                    UniqueName="ItemBinName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="Note" HeaderText="Note"
                    UniqueName="Note" HeaderStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderText="" UniqueName=""
                    HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# (string.Format("<a href=\"#\" title=\"Note\" class=\"noti_Container\" onclick=\"editNote('{0}'); return false;\"></a>", 
                                DataBinder.Eval(Container.DataItem, "SequenceNo")))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
    </telerik:RadGrid>
</asp:Content>
