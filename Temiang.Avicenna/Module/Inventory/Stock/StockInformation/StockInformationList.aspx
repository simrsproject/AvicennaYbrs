<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="StockInformationList.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Stock.StockInformationList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            var height = (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);
            function viewEdList(itemid, t) {
                var oWnd = $find("<%= winEdItem.ClientID %>");
                oWnd.setUrl('../../Warehouse/ItemExpiryDate/ItemExpiryDateList.aspx?id=' + itemid + '&t=' + t);

                oWnd.setSize(document.body.offsetWidth, height);
                oWnd.show();
            }

            function viewNewEdList(locId, itemId, type) {
                var oWnd = $find("<%= winEdItem.ClientID %>");
                oWnd.setUrl('../BalanceDetailExpiredDate/BalanceDetailExpiredDateList.aspx?iid=' + itemId + '&lid=' + locId + '&t=' + type);

                oWnd.setSize(document.body.offsetWidth, height);
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
                    <telerik:AjaxUpdatedControl ControlID="grdItemBalance" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchLocationID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemBalance" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchItemID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemBalance" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchItemID2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemBalance" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchGenerik">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemBalance" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchItemGroupID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemBalance" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="cboItemID" />
                    <telerik:AjaxUpdatedControl ControlID="txtItemName" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkBelowMinimum">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemBalance" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkOnlyInStock">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemBalance" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkHasPendingBalance">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemBalance" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdItemBalance">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemBalance" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboSRItemType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboItemGroupID" />
                    <telerik:AjaxUpdatedControl ControlID="cboItemID" />
                    <telerik:AjaxUpdatedControl ControlID="txtItemName" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchApproachingExpiration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemBalance" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 50%; vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSRItemType" runat="server" Text="Item Type" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRItemType" runat="server" Width="300px" AutoPostBack="True"
                                    OnSelectedIndexChanged="cboSRItemType_SelectedIndexChanged" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSearchSRItemType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchItemID_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblLocationID" runat="server" Text="Location" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboLocationID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboLocationID_ItemDataBound"
                                    OnItemsRequested="cboLocationID_ItemsRequested">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "LocationName")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 10 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSearchLocationID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchItemID_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label" />
                            <td class="entry">
                                <asp:CheckBox runat="server" ID="chkBelowMinimum" AutoPostBack="true" Text="Below Minimum"
                                    OnCheckedChanged="chkBelowMinimum_CheckedChanged" />
                                <asp:CheckBox runat="server" ID="chkOnlyInStock" AutoPostBack="true" Text="Only In Stock"
                                    OnCheckedChanged="chkBelowMinimum_CheckedChanged" />
                                <asp:CheckBox runat="server" ID="chkHasPendingBalance" AutoPostBack="true" Text="Has Pending Balance"
                                    OnCheckedChanged="chkBelowMinimum_CheckedChanged" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/Toolbar/print16.png"
                                    OnClick="btnPrint_Click" ToolTip="Print" />
                            </td>
                        </tr>
                        <asp:Panel runat="server" ID="pnlApproachingExpiration">
                            <tr>
                                <td class="label" />
                                <td class="entry">
                                    <asp:CheckBox runat="server" ID="chkIsApproachingExpiration" Text="Approaching Expiration"/>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblApproachingExpirationPeriod" runat="server" Text="Expiration Period" />
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtExpiredDateFrom" runat="server" Width="100px" />
                                            </td>
                                            <td>&nbsp;-&nbsp;</td>
                                            <td>
                                                <telerik:RadDatePicker ID="txtExpiredDateTo" runat="server" Width="100px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnSearchItemID_Click" ToolTip="Search" />
                                </td>
                            </tr>
                        </asp:Panel>
                    </table>
                </td>
                <td style="width: 50%; vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblItemGroupID" runat="server" Text="Item Group" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboItemGroupID" Width="300px" runat="server" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" OnItemDataBound="cboItemGroupID_ItemDataBound"
                                    OnItemsRequested="cboItemGroupID_ItemsRequested" AutoPostBack="True" OnSelectedIndexChanged="cboItemGroupID_SelectedIndexChanged">
                                    <ItemTemplate>
                                        <b>
                                            <%# DataBinder.Eval(Container.DataItem, "ItemGroupID")%>
                                            &nbsp;-&nbsp;
                                            <%# DataBinder.Eval(Container.DataItem, "ItemGroupName")%>
                                        </b>
                                        <br />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSearchItemGroupID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchItemID_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblItemID" runat="server" Text="Item Name" />
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px" Text="" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSearchItemID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchItemID_Click" ToolTip="Search" />
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
                                    OnClick="btnSearchItemID_Click" ToolTip="Search" />
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
                                    OnClick="btnSearchItemID_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdItemBalance" runat="server" AutoGenerateColumns="False" ShowGroupPanel="false"
        OnNeedDataSource="grdItemBalance_NeedDataSource" OnDetailTableDataBind="grdItemBalance_DetailTableDataBind"
        AllowSorting="True" GridLines="None">
        <MasterTableView DataKeyNames="LocationID, ItemID">
            <Columns>
                <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                    SortExpression="ItemID">
                    <HeaderStyle HorizontalAlign="Left" Width="110px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName">
                    <HeaderStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="LocationName" HeaderText="Location"
                    UniqueName="LocationName" SortExpression="LocationName">
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Minimum" HeaderText="Minimum"
                    UniqueName="Minimum" SortExpression="Minimum" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Maximum" HeaderText="Maximum"
                    UniqueName="Maximum" SortExpression="Maximum" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Balance" HeaderText="Balance"
                    UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Booking" HeaderText="Pending"
                    UniqueName="Booking" SortExpression="Booking" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                </telerik:GridNumericColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="SRItemUnit" HeaderText="Unit"
                    UniqueName="SRItemUnit" SortExpression="SRItemUnit">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ItemBin" HeaderText="Item Bin"
                    UniqueName="ItemBin" SortExpression="ItemBin">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="EdList" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsControlExpired").Equals(false) ? string.Empty : (this.IsUserEditAble.Equals(false) ? string.Format("<a href=\"#\" onclick=\"viewEdList('{0}', '0'); return false;\">{1}</a>",
                                            DataBinder.Eval(Container.DataItem, "ItemID"), "<img src=\"../../../../Images/calendar16.png\" border=\"0\" title=\"Batch No. & ED\" />") : string.Format("<a href=\"#\" onclick=\"viewEdList('{0}', '8'); return false;\">{1}</a>",
                                            DataBinder.Eval(Container.DataItem, "ItemID"), "<img src=\"../../../../Images/calendar16.png\" border=\"0\" title=\"Batch No. & ED\" />"))
                                            )%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="NewEdList" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsControlExpired").Equals(false) ? string.Empty : (this.IsUserEditAble.Equals(false) ? string.Format("<a href=\"#\" onclick=\"viewNewEdList('{0}', '{1}', '1'); return false;\">{2}</a>",
                                            DataBinder.Eval(Container.DataItem, "LocationID"), DataBinder.Eval(Container.DataItem, "ItemID"), "<img src=\"../../../../Images/calendar16.png\" border=\"0\" title=\"Batch No. & ED\" />") : string.Format("<a href=\"#\" onclick=\"viewNewEdList('{0}', '{1}', '0'); return false;\">{2}</a>",
                                            DataBinder.Eval(Container.DataItem, "LocationID"), DataBinder.Eval(Container.DataItem, "ItemID"), "<img src=\"../../../../Images/calendar16.png\" border=\"0\" title=\"Batch No. & ED\" />"))
                                            )%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="BalanceDate, ReferenceNo" Name="grdItemBalanceDetail"
                    AutoGenerateColumns="False" ShowFooter="false" AllowPaging="true" PageSize="10">
                    <Columns>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="BalanceDate" HeaderText="Date"
                            UniqueName="BalanceDate" SortExpression="BalanceDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ReferenceNo" HeaderText="Reference No"
                            UniqueName="ReferenceNo" SortExpression="ReferenceNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Balance" HeaderText="Balance"
                            UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridTemplateColumn />
                    </Columns>
                </telerik:GridTableView>
                <telerik:GridTableView DataKeyNames="ExpiredDate, BatchNumber" Name="grdItemBalanceDetailEd"
                    AutoGenerateColumns="False" ShowFooter="false" AllowPaging="true" PageSize="10">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderStyle-Width="100px" DataField="ExpiredDate" HeaderText="Expired Date" UniqueName="ExpiredDate"
                            SortExpression="ExpiredDate">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ExpiredDate", "{0:dd-MMM-yyyy}")%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="BatchNumber" HeaderText="Batch Number"
                            UniqueName="BatchNumber" SortExpression="BatchNumber" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Balance" HeaderText="Balance"
                            UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridTemplateColumn />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
            OpenInNewWindow="true" />
        <ClientSettings AllowDragToGroup="true" EnableRowHoverStyle="true" AllowExpandCollapse="true" />
    </telerik:RadGrid>
</asp:Content>
