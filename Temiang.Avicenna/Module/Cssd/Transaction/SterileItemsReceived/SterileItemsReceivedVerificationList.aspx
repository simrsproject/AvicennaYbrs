<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="SterileItemsReceivedVerificationList.aspx.cs" Inherits="Temiang.Avicenna.Module.Cssd.Transaction.SterileItemsReceivedVerificationList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoEditUrl(ino) {
                var url = 'SterileItemsReceivedDetail.aspx?md=edit&id=' + ino + '&type=ver';
                window.location.href = url;
            }

            function gotoViewUrl(ino) {
                var url = 'SterileItemsReceivedDetail.aspx?md=view&id=' + ino + '&type=ver';
                window.location.href = url;
            }

            function viewItemDetail(rno, seqNo, itemId, itemName, srItemUnit, qty, type) {
                var oWnd = $find("<%= winDetailItem.ClientID %>");

                oWnd.setUrl('SterileItemsReceivedDetailItemInfo.aspx?itemid=' + itemId + '&itemname=' + itemName + '&unit=' + srItemUnit + '&qty=' + qty + '&rno=' + rno + '&seq=' + seqNo + '&type=' + type + '&from=rec');

                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
                oWnd.add_pageLoad(onClientPageLoad);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false" />
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterRequestDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRequest" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRequestNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRequest" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRequestFromServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRequest" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterReceivedDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterReceivedNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterReferenceNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdRequest">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRequest" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow runat="server" Animation="None" Width="600px" Height="500px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winDetailItem">
    </telerik:RadWindow>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">

        <script type="text/javascript">
            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
                switch (val) {
                    case "new":
                        var url = 'SterileItemsReceivedDetail.aspx?md=new&type=ver';
                        window.location.href = url;
                        break;
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="550px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winDialog">
    </telerik:RadWindow>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="New" Value="new" ImageUrl="~/Images/Toolbar/new16.png"
                HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png" />
        </Items>
    </telerik:RadToolBar>

    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Request List" PageViewID="pgRequest"
                Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Receive List" PageViewID="pgReceived">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgRequest" runat="server" Selected="true">
            <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" style="vertical-align: top">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRequestDate" runat="server" Text="Request Date"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtRequestFromDate" runat="server" Width="100px" />
                                                </td>
                                                <td></td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtRequestToDate" runat="server" Width="100px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:ImageButton ID="btnFilterRequestDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterRequest_Click" ToolTip="Search" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRequestNo" runat="server" Text="Request No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtRequestNo" runat="server" Width="300px" MaxLength="20" />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:ImageButton ID="btnFilterRequestNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterRequest_Click" ToolTip="Search" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" valign="top">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRequestFromServiceUnit" runat="server" Text="From Service Unit"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboRequestFromServiceUnitID" Width="300px" AllowCustomText="true"
                                            Filter="Contains">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:ImageButton ID="btnFilterRequestFromServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterRequest_Click" ToolTip="Search" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </cc:CollapsePanel>
            <telerik:RadGrid ID="grdRequest" runat="server" OnNeedDataSource="grdRequest_NeedDataSource" AllowPaging="true" PageSize="15"
                AutoGenerateColumns="false">
                <MasterTableView DataKeyNames="RequestNo" GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridHyperLinkColumn HeaderStyle-Width="150px" DataTextField="RequestNo"
                            DataNavigateUrlFields="RUrl" HeaderText="Request No" UniqueName="RequestNo"
                            SortExpression="ReceivedNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="RequestDate" HeaderText="Date"
                            UniqueName="RequestDate" SortExpression="RequestDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="FromServiceUnitName" HeaderText="From Unit" UniqueName="FromServiceUnitName"
                            SortExpression="FromServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="FromRoomName" HeaderText="From Room" UniqueName="FromRoomName"
                            SortExpression="FromRoomName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="SenderBy" HeaderText="Sender By"
                            UniqueName="SenderBy" SortExpression="SenderBy" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn />
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgReceived" runat="server">
            <cc:CollapsePanel ID="CollapsePanel2" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" style="vertical-align: top">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblReceive" runat="server" Text="Receive Date"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtReceivedFromDate" runat="server" Width="100px" />
                                                </td>
                                                <td></td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtReceivedToDate" runat="server" Width="100px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:ImageButton ID="btnFilterReceivedDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilter_Click" ToolTip="Search" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblReceivedNo" runat="server" Text="Receive No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtReceivedNo" runat="server" Width="300px" MaxLength="20" />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:ImageButton ID="btnFilterReceivedNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilter_Click" ToolTip="Search" />
                                    </td>
                                </tr>

                            </table>
                        </td>
                        <td width="50%" valign="top">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblFromServiceUnitID" runat="server" Text="From Service Unit"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboFromServiceUnitID" Width="300px" AllowCustomText="true"
                                            Filter="Contains">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:ImageButton ID="btnFilterServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilter_Click" ToolTip="Search" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblReferenceNo" runat="server" Text="Request No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtReferenceNo" runat="server" Width="300px" MaxLength="20" />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:ImageButton ID="btnFilterReferenceNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilter_Click" ToolTip="Search" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </cc:CollapsePanel>
            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                OnDetailTableDataBind="grdList_DetailTableDataBind" AllowPaging="true" PageSize="15"
                AutoGenerateColumns="false">
                <MasterTableView DataKeyNames="ReceivedNo" GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridHyperLinkColumn HeaderStyle-Width="150px" DataTextField="ReceivedNo"
                            DataNavigateUrlFields="RUrl" HeaderText="Receive No" UniqueName="ReceivedNo"
                            SortExpression="ReceivedNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ReceivedDate" HeaderText="Date"
                            UniqueName="ReceivedDate" SortExpression="ReceivedDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="ReceivedTime" HeaderText="Time"
                            UniqueName="ReceivedTime" SortExpression="ReceivedTime" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="FromServiceUnitName" HeaderText="From Unit" UniqueName="FromServiceUnitName"
                            SortExpression="FromServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="FromRoomName" HeaderText="From Room" UniqueName="FromRoomName"
                            SortExpression="FromRoomName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="SenderBy" HeaderText="Sender By"
                            UniqueName="SenderBy" SortExpression="SenderBy" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ReceivedByUserName"
                            HeaderText="Received By" UniqueName="ReceivedByUserName" SortExpression="ReceivedByUserName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsFromProductionOfGoods" HeaderText="From Production Of Goods"
                            UniqueName="IsFromProductionOfGoods" SortExpression="IsFromProductionOfGoods" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ProductionNo"
                            HeaderText="Reference No" UniqueName="ProductionNo" SortExpression="ProductionNo"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                            UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                            UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView DataKeyNames="ReceivedNo, ReceivedSeqNo" Name="grdDetail"
                            AutoGenerateColumns="False" AllowPaging="true" PageSize="15">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="listDetailView" HeaderText="" Groupable="false">
                                    <ItemTemplate>
                                        <%# (string.Format("<a href=\"#\" onclick=\"viewItemDetail('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', 'info'); return false;\">{6}</a>",
                           DataBinder.Eval(Container.DataItem, "ReceivedNo"), DataBinder.Eval(Container.DataItem, "ReceivedSeqNo"), DataBinder.Eval(Container.DataItem, "ItemID"), DataBinder.Eval(Container.DataItem, "ItemName"),
                           DataBinder.Eval(Container.DataItem, "CssdItemUnit"), DataBinder.Eval(Container.DataItem, "Qty"), "<img src=\"../../../../Images/Toolbar/details16.png\" border=\"0\" title=\"View Item Detail\" />"))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="ReceivedNo" HeaderText="Received No"
                                    UniqueName="ReceivedNo" SortExpression="ReceivedNo" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="ReceivedSeqNo" HeaderText="Seq No"
                                    UniqueName="ReceivedSeqNo" SortExpression="ReceivedSeqNo" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemNo" HeaderText="Item #"
                                    UniqueName="ItemNo" SortExpression="ItemNo" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ItemName" HeaderText="Item Name"
                                    UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Qty" HeaderText="Qty"
                                    UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="QtyProcessed" HeaderText="Processed"
                                    UniqueName="QtyProcessed" SortExpression="QtyProcessed" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="QtyReturn" HeaderText="Return"
                                    UniqueName="QtyReturn" SortExpression="QtyReturn" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="CssdItemUnit" HeaderText="Unit"
                                    UniqueName="CssdItemUnit" SortExpression="CssdItemUnit" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ExpiredDate" HeaderText="Expire Date"
                                    UniqueName="ExpiredDate" SortExpression="ExpiredDate" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="ReuseTo" HeaderText="Reuse To"
                                    UniqueName="ReuseTo" SortExpression="ReuseTo" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" Visible="false" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsNeedUltrasound"
                                    HeaderText="Need Ultrasound" UniqueName="IsNeedUltrasound" SortExpression="IsNeedUltrasound"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsDtt"
                                    HeaderText="DTT Process" UniqueName="IsDtt" SortExpression="IsDtt"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
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
