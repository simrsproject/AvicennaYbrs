<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="WorkOrderPointList.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Management.WorkOrderPointList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchFromServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListOutstanding" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchToServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListOutstanding" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchOrderDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListOutstanding" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchOrderNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListOutstanding" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchRequiredDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListOutstanding" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchOrderDateRealization">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchOrderNoRealization">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchLastRealizationDateTime">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchSRWorkStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdListOutstanding">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListOutstanding" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdListOutstanding" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Timer1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListOutstanding" LoadingPanelID="fw_ajxLoadingPanel" />
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
                                <asp:Label ID="lblSearchFromServiceUnitID" runat="server" Text="Request Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboSearchFromServiceUnitID" Width="300px"
                                    AllowCustomText="true" Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchFromServiceUnitID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td width="50%">
                    <table>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSearchToServiceUnitID" runat="server" Text="To Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboSearchToServiceUnitID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchToServiceUnitID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="20000" Enabled="True" />
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Work Order Outstanding" PageViewID="pgWoo" Selected="True" />
            <telerik:RadTab runat="server" Text="Work Order Point" PageViewID="pgWop" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgWoo" runat="server" Selected="true">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50%" style="vertical-align: top">
                        <table>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSearchOrderDate" runat="server" Text="Work Order Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtFromOrderDate" runat="server" Width="100px">
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>&nbsp;-&nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="txtToOrderDate" runat="server" Width="100px">
                                                </telerik:RadDatePicker>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="30px">
                                    <asp:ImageButton ID="btnSearchOrderDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOrderNo" runat="server" Text="Work Order No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox runat="server" ID="txtOrderNo" Width="300px" />
                                </td>
                                <td width="20px">
                                    <asp:ImageButton ID="btnSearchOrderNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td width="50%" style="vertical-align: top">
                        <table>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdListOutstanding" runat="server" OnNeedDataSource="grdListOutstanding_NeedDataSource"
                AutoGenerateColumns="false" AllowPaging="true" PageSize="15">
                <MasterTableView DataKeyNames="OrderNo">
                    <Columns>
                        <telerik:GridHyperLinkColumn HeaderStyle-Width="150px" DataTextField="OrderNo" DataNavigateUrlFields="WoUrl"
                            HeaderText="Work Order No" UniqueName="OrderNo" SortExpression="OrderNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" />
                        <telerik:GridBoundColumn DataField="OrderDate" HeaderText="Order Date" UniqueName="OrderDate"
                            SortExpression="OrderDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RequiredDate" HeaderText="Required Date" UniqueName="RequiredDate"
                            SortExpression="RequiredDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FromServiceUnit" HeaderText="Request Unit" UniqueName="FromServiceUnit"
                            SortExpression="FromServiceUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ToServiceUnit" HeaderText="To Unit" UniqueName="ToServiceUnit"
                            SortExpression="ToServiceUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="WorkType" HeaderText="Work Type"
                            UniqueName="WorkType" SortExpression="WorkType" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="WorkStatus" HeaderText="Work Status"
                            UniqueName="WorkStatus" SortExpression="WorkStatus" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="False" />
                        <telerik:GridBoundColumn DataField="AssetName" HeaderText="Asset" UniqueName="AssetName"
                            SortExpression="AssetName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ProblemDescription" HeaderText="Problem Description"
                            UniqueName="ProblemDescription" SortExpression="ProblemDescription" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgWop" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50%" style="vertical-align: top">
                        <table>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOrderDateRealization" runat="server" Text="Work Order Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtFromOrderDateRealization" runat="server" Width="100px">
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>&nbsp;-&nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="txtToOrderDateRealization" runat="server" Width="100px">
                                                </telerik:RadDatePicker>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="30px">
                                    <asp:ImageButton ID="btnSearchOrderDateRealization" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOrderNoRealization" runat="server" Text="Work Order No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox runat="server" ID="txtOrderNoRealization" Width="300px" />
                                </td>
                                <td width="20px">
                                    <asp:ImageButton ID="btnSearchOrderNoRealization" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td width="50%">
                        <table>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSearchLastRealizationDateTime" runat="server" Text="Realization Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtSearchLastRealizationDateTime" runat="server" Width="100px">
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>&nbsp;-&nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="txtSearchToLastRealizationDateTime" runat="server" Width="100px">
                                                </telerik:RadDatePicker>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="30px">
                                    <asp:ImageButton ID="btnSearchLastRealizationDateTime" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSearchSRWorkStatus" runat="server" Text="Work Status"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSearchSRWorkStatus" Width="300px" AllowCustomText="true"
                                        Filter="Contains">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px">
                                    <asp:ImageButton ID="btnSearchSRWorkStatus" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                OnDetailTableDataBind="grdList_DetailTableDataBind" AutoGenerateColumns="false" AllowPaging="true" PageSize="15">
                <MasterTableView DataKeyNames="OrderNo">
                    <Columns>
                        <telerik:GridHyperLinkColumn HeaderStyle-Width="150px" DataTextField="OrderNo" DataNavigateUrlFields="WoUrl"
                            HeaderText="Work Order No" UniqueName="OrderNo" SortExpression="OrderNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" />
                        <telerik:GridBoundColumn DataField="OrderDate" HeaderText="Order Date" UniqueName="OrderDate"
                            SortExpression="OrderDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RequiredDate" HeaderText="Required Date" UniqueName="RequiredDate"
                            SortExpression="RequiredDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="LastRealizationDateTime" HeaderText="Realization Date" UniqueName="LastRealizationDateTime"
                            SortExpression="LastRealizationDateTime" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FromServiceUnit" HeaderText="Request Unit" UniqueName="FromServiceUnit"
                            SortExpression="FromServiceUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ToServiceUnit" HeaderText="To Unit" UniqueName="ToServiceUnit"
                            SortExpression="ToServiceUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="WorkType" HeaderText="Work Type"
                            UniqueName="WorkType" SortExpression="WorkType" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="WorkStatus" HeaderText="Work Status"
                            UniqueName="WorkStatus" SortExpression="WorkStatus" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="AssetName" HeaderText="Asset" UniqueName="AssetName"
                            SortExpression="AssetName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ProblemDescription" HeaderText="Problem Description"
                            UniqueName="ProblemDescription" SortExpression="ProblemDescription" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="WorkOrderPointName" HeaderText="Point Type"
                            UniqueName="WorkOrderPointName" SortExpression="WorkOrderPointName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="WorkOrderPoint"
                            HeaderText="Point Value" UniqueName="WorkOrderPoint" SortExpression="WorkOrderPoint"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView DataKeyNames="UserID" Name="grdDetail" AutoGenerateColumns="False"
                            ShowFooter="true" AllowPaging="true" PageSize="10">
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="UserName" HeaderText="Implemented By"
                                    UniqueName="UserName" SortExpression="UserName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false">
    </telerik:RadAjaxPanel>
</asp:Content>
