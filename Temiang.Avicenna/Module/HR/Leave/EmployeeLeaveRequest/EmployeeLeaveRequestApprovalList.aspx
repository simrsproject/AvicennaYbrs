<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="EmployeeLeaveRequestApprovalList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Leave.EmployeeLeaveRequestApprovalList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoViewUrl(id) {
                var url = 'EmployeeLeaveRequestDetail.aspx?md=view&id=' + id + "&type=" + '<%= Request.QueryString["type"] %>' + "&role=" + '<%= Request.QueryString["role"] %>';
                window.location.href = url;
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchTransactionDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListOutstanding" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchLeaveDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListOutstanding" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchEmployeeName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListOutstanding" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchSupervisor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListOutstanding" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchStatus">
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
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" style="vertical-align: top">
                    <table>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblTransactionDate" runat="server" Text="Request Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtFromRequestDate" runat="server" Width="100px" />
                                        </td>
                                        <td>to &nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtToRequestDate" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="30px">
                                <asp:ImageButton ID="btnSearchRequestDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Leave Date
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtFromLeaveDate" runat="server" Width="100px" />
                                        </td>
                                        <td>to &nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtToLeaveDate" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="30px">
                                <asp:ImageButton ID="btnSearchLeaveDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
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
                                <asp:Label ID="lblEmployeeName" runat="server" Text="Employee Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboPersonID" runat="server" Width="304px" EnableLoadOnDemand="true"
                                    MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPersonID_ItemDataBound"
                                    OnItemsRequested="cboPersonID_ItemsRequested">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchEmployeeName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSupervisor" runat="server" Text="Supervisor"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSupervisorID" runat="server" Width="304px" EnableLoadOnDemand="true"
                                    MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboSupervisorID_ItemDataBound"
                                    OnItemsRequested="cboSupervisorID_ItemsRequested">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchSupervisor" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
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
        Orientation="HorizontalTop" OnClientTabSelected="onClientTabSelected">
        <Tabs>
            <telerik:RadTab runat="server" Text="Outstanding List" PageViewID="pgOutstanding"
                Selected="True" />
            <telerik:RadTab runat="server" Text="Employee Leave" PageViewID="pgEmployeeLeave" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgOutstanding" runat="server" Selected="true">
            <telerik:RadGrid ID="grdListOutstanding" runat="server" OnNeedDataSource="grdListOutstanding_NeedDataSource" AutoGenerateColumns="false" AllowSorting="true"
                AllowPaging="true" PageSize="15">
                <MasterTableView DataKeyNames="LeaveRequestID">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="View" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>", 
                                    DataBinder.Eval(Container.DataItem, "LeaveRequestID")) %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="LeaveRequestID" HeaderText="ID"
                            UniqueName="LeaveRequestID" SortExpression="LeaveRequestID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="False" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="RequestDate" HeaderText="Request Date"
                            UniqueName="RequestDate" SortExpression="RequestDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="130px" DataField="EmployeeNumber" HeaderText="Employee No"
                            UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="250px" DataField="EmployeeName" HeaderText="Employee Name"
                            UniqueName="EmployeeName" SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="EmployeeLeaveTypeName" HeaderText="Leave Type"
                            UniqueName="EmployeeLeaveTypeName" SortExpression="EmployeeLeaveTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="RequestLeaveDateFrom"
                            HeaderText="From Date" UniqueName="RequestLeaveDateFrom" SortExpression="RequestLeaveDateFrom"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="RequestLeaveDateTo"
                            HeaderText="To Date" UniqueName="RequestLeaveDateTo" SortExpression="RequestLeaveDateTo"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RequestDays" HeaderText="Request Days"
                            UniqueName="RequestDays" SortExpression="Fee" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                            UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsValidated" HeaderText="Validated"
                            UniqueName="IsValidated" SortExpression="IsValidated" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidatedDateTime" HeaderText="Validated Date/Time"
                            UniqueName="ValidatedDateTime" SortExpression="ValidatedDateTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ValidatedBy" HeaderText="Validated By"
                            UniqueName="ValidatedBy" SortExpression="ValidatedBy" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                         <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsValidated1" HeaderText="Validated #1"
                            UniqueName="IsValidated1" SortExpression="IsValidated1" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="Validated1DateTime" HeaderText="Validated #1 Date/Time"
                            UniqueName="Validated1DateTime" SortExpression="Validated1DateTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="Validated1By" HeaderText="Validated #1 By"
                            UniqueName="Validated1By" SortExpression="Validated1By" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsValidated2" HeaderText="Validated #2"
                            UniqueName="IsValidated2" SortExpression="IsValidated2" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="Validated2DateTime" HeaderText="Validated #2 Date/Time"
                            UniqueName="Validated2DateTime" SortExpression="Validated2DateTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="Validated2By" HeaderText="Validated #2 By"
                            UniqueName="Validated2By" SortExpression="Validated2By" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgEmployeeLeave" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50%" valign="top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboStatus" Width="300px" />
                                </td>
                                <td width="20px">
                                    <asp:ImageButton ID="btnSearchStatus" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnExportToExcel" runat="server" ImageUrl="~/Images/Toolbar/imp_exp_excel16.png"
                                        OnClick="btnExportToExcel_Click" ToolTip="Export" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="50%" valign="top">
                        <table width="100%">
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" AutoGenerateColumns="false" AllowSorting="true"
                AllowPaging="true" PageSize="15">
                <MasterTableView DataKeyNames="LeaveRequestID">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="View" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>", 
                                    DataBinder.Eval(Container.DataItem, "LeaveRequestID")) %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="LeaveRequestID" HeaderText="ID"
                            UniqueName="LeaveRequestID" SortExpression="LeaveRequestID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="False" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="RequestDate" HeaderText="Request Date"
                            UniqueName="RequestDate" SortExpression="RequestDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="130px" DataField="EmployeeNumber" HeaderText="Employee No"
                            UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="250px" DataField="EmployeeName" HeaderText="Employee Name"
                            UniqueName="EmployeeName" SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="EmployeeLeaveTypeName" HeaderText="Leave Type"
                            UniqueName="EmployeeLeaveTypeName" SortExpression="EmployeeLeaveTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="RequestLeaveDateFrom"
                            HeaderText="From Date" UniqueName="RequestLeaveDateFrom" SortExpression="RequestLeaveDateFrom"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="RequestLeaveDateTo"
                            HeaderText="To Date" UniqueName="RequestLeaveDateTo" SortExpression="RequestLeaveDateTo"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="RequestDays" HeaderText="Request Days"
                            UniqueName="RequestDays" SortExpression="Fee" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="LeaveStatus" HeaderText="Leave Status" UniqueName="LeaveStatus"
                            SortExpression="LeaveStatus" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsValidated" HeaderText="Validated"
                            UniqueName="IsValidated" SortExpression="IsValidated" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidatedDateTime" HeaderText="Validated Date/Time"
                            UniqueName="ValidatedDateTime" SortExpression="ValidatedDateTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ValidatedBy" HeaderText="Validated By"
                            UniqueName="ValidatedBy" SortExpression="ValidatedBy" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsValidated1" HeaderText="Validated #1"
                            UniqueName="IsValidated1" SortExpression="IsValidated1" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="Validated1DateTime" HeaderText="Validated #1 Date/Time"
                            UniqueName="Validated1DateTime" SortExpression="Validated1DateTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="Validated1By" HeaderText="Validated #1 By"
                            UniqueName="Validated1By" SortExpression="Validated1By" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsValidated2" HeaderText="Validated #2"
                            UniqueName="IsValidated2" SortExpression="IsValidated2" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="Validated2DateTime" HeaderText="Validated #2 Date/Time"
                            UniqueName="Validated2DateTime" SortExpression="Validated2DateTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="Validated2By" HeaderText="Validated #2 By"
                            UniqueName="Validated2By" SortExpression="Validated2By" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsVerified" HeaderText="Verified"
                            UniqueName="IsVerified" SortExpression="IsVerified" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="VerifiedDateTime" HeaderText="Verified Date/Time"
                            UniqueName="VerifiedDateTime" SortExpression="VerifiedDateTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="VerifiedBy" HeaderText="Verified By"
                            UniqueName="VerifiedBy" SortExpression="VerifiedBy" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
