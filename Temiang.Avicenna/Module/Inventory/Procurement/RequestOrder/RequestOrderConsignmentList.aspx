<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="RequestOrderConsignmentList.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Procurement.RequestOrderConsignmentList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchTransferDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListCt" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchTransferNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListCt" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchSearchSupplierIDCt">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListCt" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchToServiceUnitIDCt">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListCt" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterSRItemTypeCt">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListCt" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdListCt">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListCt" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchTransactionDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchRequestNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchReferenceNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterToServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchSupplierID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchItemType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function onClientTabSelected(sender, eventArgs) {
                var tabIndex = eventArgs.get_tab().get_index();
                switch (tabIndex) {
                    case 0:
                        __doPostBack("<%= grdList.UniqueID %>", "rebind");
                        break;
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="550px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winDialog">
    </telerik:RadWindow>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop" OnClientTabSelected="onClientTabSelected">
        <Tabs>
            <telerik:RadTab runat="server" Text="Consignmnet Transfer Outstanding" PageViewID="pgCT"
                Selected="True" />
            <telerik:RadTab runat="server" Text="Purchase Request" PageViewID="pgPR" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgCT" runat="server" Selected="true">
            <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" style="vertical-align: top">
                            <table>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblSearchTransferDate" runat="server" Text="Transfer Date"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadDatePicker ID="txtTransferDate" runat="server" Width="100px">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td width="30px">
                                        <asp:ImageButton ID="btnSearchTransferDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterCT_Click" ToolTip="Search" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblTransferNo" runat="server" Text="Transfer No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox runat="server" ID="txtTransferNo" Width="300px" MaxLength="20" />
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchTransferNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterCT_Click" ToolTip="Search" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%">
                            <table>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblSearchSupplierIDCt" runat="server" Text="Supplier"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboSearchSupplierIDCt" Width="300px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboSearchSupplierIDCt_ItemDataBound"
                                            OnItemsRequested="cboSearchSupplierIDCt_ItemsRequested">
                                            <FooterTemplate>
                                                Note : Show max 10 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchSearchSupplierIDCt" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterCT_Click" ToolTip="Search" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblToServiceUnitIDCt" runat="server" Text="To Service Unit"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboSearchToServiceUnitIDCt" Width="300px"
                                            AllowCustomText="true" Filter="Contains">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchToServiceUnitIDCt" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterCT_Click" ToolTip="Search" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblSRItemTypeCt" runat="server" Text="Item Type"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboSRItemTypeCt" Width="300px" />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:ImageButton ID="btnFilterSRItemTypeCt" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterCT_Click" ToolTip="Search" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </cc:CollapsePanel>
            <telerik:RadGrid ID="grdListCt" runat="server" OnNeedDataSource="grdListCt_NeedDataSource"
                OnDetailTableDataBind="grdListCt_DetailTableDataBind" AutoGenerateColumns="false"
                AllowPaging="true" PageSize="15">
                <MasterTableView DataKeyNames="TransactionNo">
                    <Columns>
                        <telerik:GridHyperLinkColumn HeaderStyle-Width="150px" DataTextField="TransactionNo"
                            DataNavigateUrlFields="PrUrl" HeaderText="Transfer No" UniqueName="TransactionNo"
                            SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                            HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="SupplierName" HeaderText="Supplier"
                            UniqueName="SupplierName" SortExpression="SupplierName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="FromLocationName" HeaderText="From Location"
                            UniqueName="FromLocationName" SortExpression="FromLocationName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ToServiceUnitName"
                            HeaderText="To Service Unit" UniqueName="ToServiceUnitName" SortExpression="ToServiceUnitName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ToLocationName" HeaderText="To Location"
                            UniqueName="ToLocationName" SortExpression="ToLocationName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemType" HeaderText="Item Type"
                            UniqueName="ItemType" SortExpression="ItemType" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                            UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                            UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView DataKeyNames="SequenceNo" Name="grdDetailCt" Width="100%"
                            AutoGenerateColumns="false" ShowFooter="true" AllowPaging="true" PageSize="10">
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Quantity" HeaderText="Qty Transfer"
                                    UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" />
                                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="QtyFinish" HeaderText="Qty Request"
                                    UniqueName="QtyFinish" SortExpression="QtyFinish" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" />    
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SRItemUnit" HeaderText="Unit"
                                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgPR" runat="server">
            <cc:CollapsePanel ID="CollapsePanel4" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" valign="top">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblTransactionDate" runat="server" Text="Request Date"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtFromRequestDate" runat="server" Width="100px">
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td>
                                                    &nbsp;-&nbsp;
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtToRequestDate" runat="server" Width="100px">
                                                    </telerik:RadDatePicker>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchTransactionDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterPR_Click" ToolTip="Search" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRequestNo" runat="server" Text="Request No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox runat="server" ID="txtRequestNo" Width="300px" />
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchRequestNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterPR_Click" ToolTip="Search" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblReferenceNo" runat="server" Text="Transfer No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox runat="server" ID="txtReferenceNo" Width="300px" />
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchReferenceNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterPR_Click" ToolTip="Search" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblSearchStatus" runat="server" Text="Status"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboSearchStatus" Width="300px" />
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchStatus" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterPR_Click" ToolTip="Search" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" valign="top">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblToServiceUnitID" runat="server" Text="Purchasing Unit"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboToServiceUnitID" Width="300px" AllowCustomText="true"
                                            Filter="Contains">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:ImageButton ID="btnFilterToServiceUnitID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterPR_Click" ToolTip="Search" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblSearchSupplierID" runat="server" Text="Supplier"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboSearchSupplierID" Width="300px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboSearchSupplierID_ItemDataBound"
                                            OnItemsRequested="cboSearchSupplierID_ItemsRequested">
                                            <FooterTemplate>
                                                Note : Show max 10 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchSupplierID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterPR_Click" ToolTip="Search" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblItemType" runat="server" Text="Item Type"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboSRItemType" Width="300px" runat="server">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchItemType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterPR_Click" ToolTip="Search" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </cc:CollapsePanel>
            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                OnDetailTableDataBind="grdList_DetailTableDataBind" AutoGenerateColumns="false"
                AllowPaging="true" PageSize="15">
                <MasterTableView DataKeyNames="TransactionNo">
                    <Columns>
                        <telerik:GridHyperLinkColumn HeaderStyle-Width="150px" DataTextField="TransactionNo"
                            DataNavigateUrlFields="PrUrl" HeaderText="Request No" UniqueName="TransactionNo"
                            SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                            HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="FServiceUnitID" HeaderText="Request Unit" UniqueName="FServiceUnitID"
                            SortExpression="FServiceUnitID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="CostUnit" HeaderText="Cost For Unit" UniqueName="CostUnit"
                            SortExpression="CostUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="TServiceUnitID" HeaderText="Purchasing Unit"
                            UniqueName="TServiceUnitID" SortExpression="TServiceUnitID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemName" HeaderText="Item Type"
                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsInventoryItem"
                            HeaderText="Inventory Item" UniqueName="IsInventoryItem" SortExpression="IsInventoryItem"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                            UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsClosed" HeaderText="Closed"
                            UniqueName="IsClosed" SortExpression="IsClosed" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                            UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView DataKeyNames="TransactionNo,SequenceNo" Name="grdDetail" AutoGenerateColumns="False"
                            AllowPaging="true" PageSize="15" ShowFooter="True">
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="Description" HeaderText="Item Name" UniqueName="Description"
                                    SortExpression="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Minimum" HeaderText="Min Qty"
                                    UniqueName="Minimum" SortExpression="Minimum" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Maximum" HeaderText="Max Qty"
                                    UniqueName="Maximum" SortExpression="Maximum" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="BalanceSG" HeaderText="Balance SG"
                                    UniqueName="BalanceSG" SortExpression="BalanceSG" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Balance" HeaderText="Balance Loc"
                                    UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="BalanceTotal" HeaderText="Balance Total"
                                    UniqueName="BalanceTotal" SortExpression="BalanceTotal" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRMasterBaseUnit" HeaderText="Unit"
                                    UniqueName="SRMasterBaseUnit" SortExpression="SRMasterBaseUnit" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Quantity" HeaderText="Request Qty"
                                    UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="QtyApproved" HeaderText="Approved Qty"
                                    UniqueName="QtyApproved" SortExpression="QtyApproved" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="QuantityFinishInBaseUnit"
                                    HeaderText="Order Qty" UniqueName="QuantityFinishInBaseUnit" SortExpression="QuantityFinishInBaseUnit"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="QuantityReceived"
                                    HeaderText="Received Qty" UniqueName="QuantityReceived" SortExpression="QuantityReceived"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRItemUnit" HeaderText="Unit"
                                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Price" HeaderText="Price"
                                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Discount1Percentage"
                                    HeaderText="Disc #1(%)" UniqueName="Discount1Percentage" SortExpression="Discount1Percentage"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Discount2Percentage"
                                    HeaderText="Disc #2(%)" UniqueName="Discount2Percentage" SortExpression="Discount2Percentage"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Discount" HeaderText="Disc Amount"
                                    UniqueName="Discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Total" UniqueName="Total"
                                    DataType="System.Double" DataFields="Price,Discount1Percentage,Discount2Percentage, Quantity"
                                    Expression="({0}*{3}) - ((({0}*{1}/100) + (({0} - ({0}*{1}/100)) * {2}/100)) * {3})"
                                    FooterText=" " FooterStyle-HorizontalAlign="Right" Aggregate="Sum" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsClosed" HeaderText="Closed"
                                    UniqueName="IsClosed" SortExpression="IsClosed" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
