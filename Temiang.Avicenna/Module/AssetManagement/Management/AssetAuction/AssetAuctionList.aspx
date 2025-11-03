<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="AssetAuctionList.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Management.AssetAuctionList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function openWinView(seqNo) {
                var url = 'AssetAuctionDetail.aspx?md=view&id=' + seqNo + '&refNo=-1';
                window.location.href = url;
            }

            function openWinAdd(refNo) {
                var url = 'AssetAuctionDetail.aspx?md=new&id=-1&refNo=' + refNo;
                window.location.href = url;
            }

        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDisposed" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnRoom">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDisposed" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAsset">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDisposed" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdDisposed">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDisposed" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboLocation" />
                    <telerik:AjaxUpdatedControl ControlID="cboAssetID" />
                    <telerik:AjaxUpdatedControl ControlID="grdDisposed" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboLocation">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboAssetID" />
                    <telerik:AjaxUpdatedControl ControlID="grdDisposed" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" style="vertical-align: top">
                    <table>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblServiceUnit" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboServiceUnit" runat="server" Width="304px" HighlightTemplatedItems="True"
                                    AutoPostBack="True" MarkFirstMatch="false" EnableLoadOnDemand="true" OnItemDataBound="cboServiceUnit_ItemDataBound"
                                    OnItemsRequested="cboServiceUnit_ItemsRequested" OnSelectedIndexChanged="cboServiceUnit_SelectedIndexChanged">
                                    <FooterTemplate>
                                        Note : Show max 20 result
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="30px">
                                <asp:ImageButton ID="btnServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRoom" runat="server" Text="Room"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboLocation" Width="304px" AllowCustomText="true"
                                    Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboLocation_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnRoom" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td width="50%" style="vertical-align: top">
                    <table>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblAsset" runat="server" Text="Asset"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboAssetID" runat="server" Width="304px" HighlightTemplatedItems="True"
                                    MarkFirstMatch="false" EnableLoadOnDemand="true" OnItemDataBound="cboAssetID_ItemDataBound"
                                    OnItemsRequested="cboAssetID_ItemsRequested">
                                    <ItemTemplate>
                                        <b>
                                            <%# DataBinder.Eval(Container.DataItem, "AssetName")%>
                                        </b>
                                        <br />
                                        Serial No :
                                        <%# DataBinder.Eval(Container.DataItem, "SerialNumber")%>
                                        <br />
                                        Location :&nbsp;<%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                                        <br />
                                        Unit Maintenance :&nbsp;<%# DataBinder.Eval(Container.DataItem, "MaintenanceServiceUnitName")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 20 result
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnAsset" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Inactive or Damaged Assets" PageViewID="pgDisposed"
                Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Auctioned List" PageViewID="pgAuctioned">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgDisposed" runat="server" Selected="true">
            <telerik:RadGrid ID="grdDisposed" runat="server" OnNeedDataSource="grdDisposed_NeedDataSource" AllowPaging="true" PageSize="15"
                AutoGenerateColumns="false">
                <MasterTableView DataKeyNames="SeqNo" GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="">
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" onclick=\"openWinAdd('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"New\" /></a>",
                                                            DataBinder.Eval(Container.DataItem, "SeqNo")))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="SeqNo" HeaderText="No"
                            UniqueName="SeqNo" SortExpression="SeqNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" Visible="False" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate" HeaderText="Date"
                            UniqueName="TransactionDate" SortExpression="TransactionDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="AssetID" HeaderText="Asset ID"
                            UniqueName="AssetID" SortExpression="AssetID" />
                        <telerik:GridBoundColumn DataField="AssetName" HeaderText="Asset Name" UniqueName="AssetName"
                            SortExpression="AssetName" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="BrandName" HeaderText="Model No" UniqueName="BrandName"
                            SortExpression="BrandName" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="SerialNumber" HeaderText="Serial No" UniqueName="SerialNumber"
                            SortExpression="SerialNumber" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="AssetGroupName" HeaderText="Asset Group"
                            UniqueName="AssetGroupName" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ServiceUnitName" HeaderText="Service Unit"
                            UniqueName="ServiceUnitName" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="LastUpdateDateTime" HeaderText="Last Update"
                            UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime" DataType="System.DateTime"
                            DataFormatString="{0:MM/dd/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="105px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
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
        <telerik:RadPageView ID="pgAuctioned" runat="server">
            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" AllowPaging="true" PageSize="15"
                AutoGenerateColumns="false">
                <MasterTableView DataKeyNames="SeqNo" GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="">
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" onclick=\"openWinView('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>",
                                                            DataBinder.Eval(Container.DataItem, "SeqNo")))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="SeqNo" HeaderText="No"
                            UniqueName="SeqNo" SortExpression="SeqNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" Visible="False" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate" HeaderText="Date"
                            UniqueName="TransactionDate" SortExpression="TransactionDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="AssetID" HeaderText="Asset ID"
                            UniqueName="AssetID" SortExpression="AssetID" />
                        <telerik:GridBoundColumn DataField="AssetName" HeaderText="Asset Name" UniqueName="AssetName"
                            SortExpression="AssetName" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="BrandName" HeaderText="Model No" UniqueName="BrandName"
                            SortExpression="BrandName" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="SerialNumber" HeaderText="Serial No" UniqueName="SerialNumber"
                            SortExpression="SerialNumber" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="AssetGroupName" HeaderText="Asset Group"
                            UniqueName="AssetGroupName" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                            UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                            UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="LastUpdateDateTime" HeaderText="Last Update"
                            UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime" DataType="System.DateTime"
                            DataFormatString="{0:MM/dd/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="105px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
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
    </telerik:RadMultiPage>
</asp:Content>
