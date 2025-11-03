<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="SafetyCultureIncidentReportsConclusionList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.KEHRS.SafetyCultureIncidentReportsConclusionList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false" />
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterReportDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRequest" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterTransactionNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRequest" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterEmployeeNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRequest" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterVerifiedDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRequest" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterReportStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRequest" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterClosedDate">
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
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblReportDate" runat="server" Text="Report Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtReportFromDate" runat="server" Width="100px" />
                                        </td>
                                        <td></td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtReportToDate" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterReportDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblTransactionNo" runat="server" Text="Report No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterTransactionNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblEmployeeNo" runat="server" Text="Employee No / Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtEmployeeNo" runat="server" Width="300px" MaxLength="100" />
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterEmployeeNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label2" runat="server" Text="Verified Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtVerfiedFromDate" runat="server" Width="100px" />
                                        </td>
                                        <td></td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtVerfiedToDate" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterVerifiedDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSRIncidentReportStatus" runat="server" Text="Report Status"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRIncidentReportStatus" runat="server" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterReportStatus" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
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
            <telerik:RadTab runat="server" Text="Oustanding List" PageViewID="pgOutstanding"
                Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Conslusion & Recommendation List" PageViewID="pgVerification">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgOutstanding" runat="server" Selected="true">
            <telerik:RadGrid ID="grdOutstanding" runat="server" OnNeedDataSource="grdOutstanding_NeedDataSource" AllowPaging="true" PageSize="15"
                AutoGenerateColumns="false">
                <MasterTableView DataKeyNames="TransactionNo" GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridHyperLinkColumn HeaderStyle-Width="150px" DataTextField="TransactionNo"
                            DataNavigateUrlFields="EditUrl" HeaderText="Report No" UniqueName="TransactionNo"
                            SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" />
                        <telerik:GridBoundColumn DataField="ReportDate" HeaderText="Report Date"
                            UniqueName="ReportDate" SortExpression="ReportDate" DataType="System.DateTime"
                            DataFormatString="{0:dd/MM/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="105px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ReportDescription" HeaderText="Report Subject" UniqueName="ReportDescription"
                            SortExpression="ReportDescription" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="IncidentReportStatus" HeaderText="Report Status" UniqueName="IncidentReportStatus"
                            SortExpression="IncidentReportStatus" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="VerifiedDateTime" HeaderText="Verified Date/Time"
                            UniqueName="VerifiedDateTime" SortExpression="VerifiedDateTime" DataType="System.DateTime"
                            DataFormatString="{0:dd/MM/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="105px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
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
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label1" runat="server" Text="Closing Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtClosedFromDate" runat="server" Width="100px" />
                                            </td>
                                            <td></td>
                                            <td>
                                                <telerik:RadDatePicker ID="txtClosedToDate" runat="server" Width="100px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="text-align: left;">
                                    <asp:ImageButton ID="btnFilterClosedDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilterVerif_Click" ToolTip="Search" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" AllowPaging="true" PageSize="15"
                AutoGenerateColumns="false">
                <MasterTableView DataKeyNames="TransactionNo" GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridHyperLinkColumn HeaderStyle-Width="150px" DataTextField="TransactionNo"
                            DataNavigateUrlFields="ViewUrl" HeaderText="Transaction No" UniqueName="TransactionNo"
                            SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" />
                        <telerik:GridBoundColumn DataField="ReportDate" HeaderText="Report Date"
                            UniqueName="ReportDate" SortExpression="ReportDate" DataType="System.DateTime"
                            DataFormatString="{0:dd/MM/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="105px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="ReportDescription" HeaderText="Report Subject" UniqueName="ReportDescription"
                            SortExpression="ReportDescription" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="IncidentReportStatus" HeaderText="Report Status" UniqueName="IncidentReportStatus"
                            SortExpression="IncidentReportStatus" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridBoundColumn DataField="VerifiedDateTime" HeaderText="Verified Date/Time"
                            UniqueName="VerifiedDateTime" SortExpression="VerifiedDateTime" DataType="System.DateTime"
                            DataFormatString="{0:dd/MM/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="105px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsClosed" HeaderText="Closed"
                            UniqueName="IsClosed" SortExpression="IsClosed" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ClosedDateTime" HeaderText="Closed Date/Time"
                            UniqueName="ClosedDateTime" SortExpression="ClosedDateTime" DataType="System.DateTime"
                            DataFormatString="{0:dd/MM/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="105px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
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
