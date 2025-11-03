<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="ItemInformationList.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Information.ItemInformationList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script language="javascript" type="text/javascript">
            function viewEdDetail(transNo, seqNo) {
                var oWnd = $find("<%= winEdItem.ClientID %>");
                var oit = $find("<%= cboSRItemType.ClientID %>");
                oWnd.setUrl('../../Warehouse/ItemExpiryDate/ItemExpiryDateDetail.aspx?trn=' + transNo + '&sqn=' + seqNo + '&itype=' + oit._value + '&read=yes');

                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }
            function viewEdList(itemId) {
                var oWnd = $find("<%= winEdItem.ClientID %>");
                var oit = $find("<%= cboSRItemType.ClientID %>");
                oWnd.setUrl('../../Warehouse/ItemExpiryDate/ItemExpiryDateList.aspx?id=' + itemId + '&t=0');

                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="600px" Height="800px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winEdItem">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchSRItemType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchItemID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchItemID2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchBatchNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchGenerik">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSRItemType" runat="server" Text="Item Type"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRItemType" runat="server" Width="300px" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSearchSRItemType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearch_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBatchNo" runat="server" Text="Batch No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtBatchNo" runat="server" Width="300px" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSearchBatchNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearch_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblItemID" runat="server" Text="Item Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px" Text="" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSearchItemID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearch_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="Or" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboItemID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboItemID_ItemDataBound"
                                    OnItemsRequested="cboItemID_ItemsRequested">
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSearchItemID2" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearch_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label2" runat="server" Text="Generic" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboZatActiveID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboZatActiveID_ItemDataBound"
                                    OnItemsRequested="cboZatActiveID_ItemsRequested">
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSearchGenerik" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearch_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdItem" runat="server" AutoGenerateColumns="False" ShowGroupPanel="false"
        OnNeedDataSource="grdItem_NeedDataSource" OnDetailTableDataBind="grdItemDetail_DetailTableDataBind"
        AllowPaging="True" PageSize="15" AllowSorting="True" GridLines="None">
        <MasterTableView DataKeyNames="ItemID">
            <Columns>
                <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                    SortExpression="ItemID">
                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName">
                    <HeaderStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRItemUnit" HeaderText="Unit"
                    UniqueName="SRItemUnit" SortExpression="SRItemUnit">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="PriceInBaseUnit"
                    HeaderText="Price In Based Unit" UniqueName="PriceInBaseUnit" SortExpression="PriceInBaseUnit"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="LastPriceInBaseUnit"
                    HeaderText="Last Price In Based Unit (Price - Disc + Tax)" UniqueName="LastPriceInBaseUnit"
                    SortExpression="LastPriceInBaseUnit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="HighestPriceInBasedUnit"
                    HeaderText="Highest Price In Based Unit" UniqueName="HighestPriceInBasedUnit" SortExpression="HighestPriceInBasedUnit"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="Price" HeaderText="Sales Price"
                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                </telerik:GridNumericColumn>
                <telerik:GridTemplateColumn UniqueName="listED" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsControlExpired").Equals(false) ? string.Empty :
                                            string.Format("<a href=\"#\" onclick=\"viewEdList('{0}'); return false;\">{1}</a>",
                                            DataBinder.Eval(Container.DataItem, "ItemID"), "<img src=\"../../../../Images/calendar16.png\" border=\"0\" title=\"Batch No. & ED\" />"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="TransactionNo, SequenceNo" Name="grdItemDetail"
                    Width="100%" AutoGenerateColumns="false" ShowFooter="true" AllowPaging="true"
                    PageSize="10">
                    <Columns>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="70px" DataField="TransactionDate"
                            HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="TransactionNo" HeaderText="Transaction No"
                            UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="SupplierName" HeaderText="Supplier" UniqueName="SupplierName"
                            SortExpression="SupplierName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="InvoiceNo" HeaderText="Supp. Invoice No"
                            UniqueName="InvoiceNo" SortExpression="InvoiceNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ReferenceNo" HeaderText="PO No"
                            UniqueName="ReferenceNo" SortExpression="ReferenceNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Quantity" HeaderText="Qty"
                            UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="SRItemUnit" HeaderText="Unit"
                            UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                            UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Discount1Percentage"
                            HeaderText="Disc #1 (%)" UniqueName="Discount1Percentage" SortExpression="Discount1Percentage"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Discount2Percentage"
                            HeaderText="Disc #2 (%)" UniqueName="Discount2Percentage" SortExpression="Discount2Percentage"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Discount" HeaderText="Disc Amount"
                            UniqueName="Discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridTemplateColumn UniqueName="editED" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsControlExpired").Equals(false) ? string.Empty :
                                            string.Format("<a href=\"#\" onclick=\"viewEdDetail('{0}','{1}'); return false;\">{2}</a>",
                                            DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"), "<img src=\"../../../../Images/calendar16.png\" border=\"0\" title=\"Batch No. & ED\" />"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
        <ClientSettings AllowDragToGroup="true" EnableRowHoverStyle="true" AllowExpandCollapse="true">
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
