<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="WorkScheduleDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.WorkScheduleDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table width="150px">
                    <tr>
                        <td style="vertical-align: top">
                            <fieldset id="FieldSet1" style="width: 150px; min-height: 150px;">
                                <legend>Photo</legend>
                                <asp:Image runat="server" ID="imgPhoto" Width="150px" Height="150px" />
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblPersonID" runat="server" Text="Person ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPersonID" runat="server" Width="300px" AutoPostBack="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEmployeeNumber" runat="server" Text="Employee No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEmployeeNumber" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEmployeeName" runat="server" Text="Employee Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEmployeeName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Date Of Birth"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtBirthDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                DatePopupButton-Enabled="false" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSREmployeeStatus" runat="server" Text="Employee Status"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEmployeeStatusName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr runat="server">
                        <td class="label">
                            <asp:Label ID="lblSREmployeeType" runat="server" Text="Employee Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEmployeeTypeName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEmployeeRegistrationNo" runat="server" Text="Employee Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEmployeeRegistrationNo" runat="server" Width="300px" MaxLength="50" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSupervisorID" runat="server" Text="Supervisor"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSupervisorName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAbsenceCardNo" runat="server" Text="Absence Card No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAbsenceCardNo" runat="server" Width="300px" MaxLength="40" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblJoinDate" runat="server" Text="Join - (Est.) Resign Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtJoinDate" runat="server" Width="100px" Enabled="false" />
                                    </td>
                                    <td style="width: 10px">
                                        <asp:Label ID="lblResignDate" runat="server" Text=" - "></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtResignDate" runat="server" Width="100px" Enabled="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblOrganizationName" runat="server" Text="Department"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtOrganizationName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPositionTitle" runat="server" Text="Position Title"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPositionTitle" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPositionGradeID" runat="server" Text="Position Grade / Year"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtPositionGradeID" runat="server" Width="200px" ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtGradeYear" runat="server" Width="96px" ReadOnly="true" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="Service Year"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtServiceYear" runat="server" Width="100px" ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtServiceYearText" runat="server" Width="196px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSREmploymentType" runat="server" Text="Employment Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSREmploymentType" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>

                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSREducationLevel" runat="server" Text="Education Level"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSREducationLevel" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1">
        <Tabs>
            <telerik:RadTab runat="server" Text="Work Schedule" Selected="True" PageViewID="pgWorkSchedule">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="History" PageViewID="pgHistory">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderStyle="Solid" SelectedIndex="0"
        BorderColor="Gray">
        <telerik:RadPageView ID="pgWorkSchedule" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPayrollPeriod" runat="server" Text="Payroll Period"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboPayrollPeriodID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                        MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPayrollPeriodID_ItemDataBound"
                                        OnItemsRequested="cboPayrollPeriodID_ItemsRequested">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "PayrollPeriodCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "PayrollPeriodName")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 12 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDateRange" runat="server" Text="Schedule Date Range"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtStartDate" runat="server" Width="100px" Enabled="false">
                                    </telerik:RadDatePicker>
                                    to
                                    <telerik:RadDatePicker ID="txtEndDate" runat="server" Width="100px" Enabled="false">
                                    </telerik:RadDatePicker>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>

                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdWorkSchedule" runat="server" OnNeedDataSource="grdWorkSchedule_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdWorkSchedule_UpdateCommand"
                OnDeleteCommand="grdWorkSchedule_DeleteCommand" OnInsertCommand="grdWorkSchedule_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="MonthlyAttendanceDetailID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn DataField="MonthlyAttendanceDetailID" UniqueName="MonthlyAttendanceDetailID"
                            Visible="false" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="ScheduleInDate" HeaderText="Schedule In Date"
                            UniqueName="ScheduleInDate" SortExpression="ScheduleInDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ScheduleInTime" HeaderText="Schedule In Time" UniqueName="ScheduleInTime"
                            SortExpression="ScheduleInTime">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="ScheduleOutDate" HeaderText="Schedule Out Date"
                            UniqueName="ScheduleOutDate" SortExpression="ScheduleOutDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ScheduleOutTime" HeaderText="Schedule Out Time" UniqueName="ScheduleOutTime"
                            SortExpression="ScheduleOutTime">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="WorkScheduleDetailItem.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="grdWorkScheduleEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="True">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgHistory" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPayrollPeriodID2" runat="server" Text="Payroll Period"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboPayrollPeriodID2" runat="server" Width="300px" EnableLoadOnDemand="true"
                                        MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPayrollPeriodID_ItemDataBound"
                                        OnItemsRequested="cboPayrollPeriodID_ItemsRequested">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "PayrollPeriodCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "PayrollPeriodName")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 12 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20">
                                    <asp:ImageButton ID="btnFilter" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdWorkSchedule2" runat="server" OnNeedDataSource="grdWorkSchedule2_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="MonthlyAttendanceDetailID">
                    <Columns>
                        <telerik:GridNumericColumn DataField="MonthlyAttendanceDetailID" UniqueName="MonthlyAttendanceDetailID"
                            Visible="false" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="ScheduleInDate" HeaderText="Schedule In Date"
                            UniqueName="ScheduleInDate" SortExpression="ScheduleInDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ScheduleInTime" HeaderText="Schedule In Time" UniqueName="ScheduleInTime"
                            SortExpression="ScheduleInTime">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="ScheduleOutDate" HeaderText="Schedule Out Date"
                            UniqueName="ScheduleOutDate" SortExpression="ScheduleOutDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ScheduleTimeOut" HeaderText="Schedule Out Time" UniqueName="ScheduleOutTime"
                            SortExpression="ScheduleOutTime">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn />
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="True">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
