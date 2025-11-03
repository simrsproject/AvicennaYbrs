<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="NeedleStickInjuryFollowUpList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.K3RS.NeedleStickInjuryFollowUpList" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false" />
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterIncidentDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstanding" />
                    <telerik:AjaxUpdatedControl ControlID="grdVerification" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterTransactionNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstanding" />
                    <telerik:AjaxUpdatedControl ControlID="grdVerification" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterEmployeeNumber">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstanding" />
                    <telerik:AjaxUpdatedControl ControlID="grdVerification" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterEmployeeName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstanding" />
                    <telerik:AjaxUpdatedControl ControlID="grdVerification" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdOutstanding">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstanding" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdVerification">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdVerification" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblIncidentDate" runat="server" Text="Incident Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtIncidentFromDate" runat="server" Width="100px" />
                                        </td>
                                        <td></td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtIncidentToDate" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterIncidentDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterTransactionNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblEmployeeName" runat="server" Text="Employee Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtEmployeeName" runat="server" Width="300px" MaxLength="150" />
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterEmployeeName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblEmployeeNumber" runat="server" Text="Employee Number"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtEmployeeNumber" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterEmployeeNumber" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Incident Outstanding List" PageViewID="pgOutstanding"
                Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Verification List" PageViewID="pgVerification">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgOutstanding" runat="server" Selected="true">
            <telerik:RadGrid ID="grdOutstanding" runat="server" OnNeedDataSource="grdOutstanding_NeedDataSource"
                AllowSorting="true" ShowStatusBar="true" AllowPaging="true" PageSize="15">
                <MasterTableView DataKeyNames="TransactionNo" AutoGenerateColumns="false"
                    GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridHyperLinkColumn HeaderStyle-Width="150px" DataTextField="TransactionNo"
                            DataNavigateUrlFields="ViewUrl" HeaderText="Transaction No" UniqueName="TransactionNo"
                            SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" />
                        <telerik:GridBoundColumn DataField="IncidentDateTime" HeaderText="Incident Date"
                            UniqueName="IncidentDateTime" SortExpression="IncidentDateTime" DataType="System.DateTime"
                            DataFormatString="{0:dd/MM/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="105px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PersonID" HeaderText="Person ID"
                            UniqueName="PersonID" SortExpression="PersonID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="EmployeeNumber" HeaderText="Employee No"
                            UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                            SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="PositionName" HeaderText="Position" UniqueName="PositionName"
                            SortExpression="PositionName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ExposedArea" HeaderText="Exposed Areas"
                            UniqueName="ExposedArea" SortExpression="PuncturedAreas" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Reason" HeaderText="Reason" UniqueName="Reason"
                            SortExpression="Reason" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
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
        <telerik:RadPageView ID="pgVerification" runat="server">
            <telerik:RadGrid ID="grdVerification" runat="server" OnNeedDataSource="grdVerification_NeedDataSource"
                AllowSorting="true"
                ShowStatusBar="true" AllowPaging="true" PageSize="15">
                <MasterTableView DataKeyNames="TransactionNo" AutoGenerateColumns="false"
                    GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridHyperLinkColumn HeaderStyle-Width="150px" DataTextField="TransactionNo"
                            DataNavigateUrlFields="ViewUrl" HeaderText="Transaction No" UniqueName="TransactionNo"
                            SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" />
                        <telerik:GridBoundColumn DataField="IncidentDateTime" HeaderText="Incident Date"
                            UniqueName="IncidentDateTime" SortExpression="IncidentDateTime" DataType="System.DateTime"
                            DataFormatString="{0:dd/MM/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="105px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PersonID" HeaderText="Person ID"
                            UniqueName="PersonID" SortExpression="PersonID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="EmployeeNumber" HeaderText="Employee No"
                            UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                            SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="PositionName" HeaderText="Position" UniqueName="PositionName"
                            SortExpression="PositionName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ExposedArea" HeaderText="Exposed Areas"
                            UniqueName="ExposedArea" SortExpression="PuncturedAreas" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Reason" HeaderText="Reason" UniqueName="Reason"
                            SortExpression="Reason" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsVerified" HeaderText="Verified"
                            UniqueName="IsVerified" SortExpression="IsVerified" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="FollowUpDate" HeaderText="Follow Up Date"
                            UniqueName="FollowUpDate" SortExpression="FollowUpDate" DataType="System.DateTime"
                            DataFormatString="{0:MM/dd/yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="FollowUpBy" HeaderText="Follow Up By" UniqueName="FollowUpBy"
                            SortExpression="FollowUpBy" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
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
