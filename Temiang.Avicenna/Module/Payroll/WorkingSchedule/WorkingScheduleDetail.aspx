<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="WorkingScheduleDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.WorkingScheduleDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" style="position: fixed; background-color: white;">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">Organization Unit
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboOrganizationUnit" runat="server" Width="300px" AllowCustomText="true" Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboOrganizationUnit_SelectedIndexChanged" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvOrganizationUnit" runat="server" ErrorMessage="Organization Unit required."
                                ValidationGroup="entry" ControlToValidate="cboOrganizationUnit" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Payroll Period Name
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPayrollPeriod" runat="server" Width="300px" AutoPostBack="true" AllowCustomText="true" Filter="Contains" OnSelectedIndexChanged="cboPeriodMonth_SelectedIndexChanged" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPayrollPeriod" runat="server" ErrorMessage="Payroll Period Name required."
                                ValidationGroup="entry" ControlToValidate="cboPayrollPeriod" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">Entry Type
                        </td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rbtEntryType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" OnSelectedIndexChanged="rbtEntryType_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="0" Text="All Employee" />
                                <asp:ListItem Value="1" Text="Selected Employee" Selected="True" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Employee Name
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboEmployeeName" runat="server" Width="300px" AllowCustomText="true" Filter="Contains" />
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Period Day
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboDayFrom" runat="server" Width="100px" />
                                    </td>
                                    <td>&nbsp;To&nbsp;</td>
                                    <td>
                                        <telerik:RadComboBox ID="cboDayTo" runat="server" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Working Hour
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboWorkingHour" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvWorkingHour" runat="server" ErrorMessage="Working Hour required."
                                ValidationGroup="entry" ControlToValidate="cboPayrollPeriod" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:Button ID="btnInsert" runat="server" Text="Insert" OnClick="btnInsert_Click" />
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="padding-top: 142px;">
        <telerik:RadGrid ID="grdWorkingScheduleDetail" runat="server" OnNeedDataSource="grdWorkingScheduleDetail_NeedDataSource"
            AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdWorkingScheduleDetail_UpdateCommand"
            OnDeleteCommand="grdWorkingScheduleDetail_DeleteCommand" OnInsertCommand="grdWorkingScheduleDetail_InsertCommand">
            <MasterTableView CommandItemDisplay="None" DataKeyNames="WorkingScheduleDetailID, WorkingScheduleID, PersonID">
                <Columns>
                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="EmployeeNumber" HeaderText="Employee No"
                        UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name"
                        UniqueName="EmployeeName" SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridTemplateColumn HeaderText="Working Hour" UniqueName="WorkingHourName" SortExpression="WorkingHourName" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <table width="100%">
                                <tr>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday1")) ? "background-color: orangered;color:white;" : "" %>">Day 1&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName1") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday2")) ? "background-color: orangered;color:white;" : "" %>">Day 2&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName2") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday3")) ? "background-color: orangered;color:white;" : "" %>">Day 3&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName3") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday4")) ? "background-color: orangered;color:white;" : "" %>">Day 4&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName4") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday5")) ? "background-color: orangered;color:white;" : "" %>">Day 5&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName5") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday6")) ? "background-color: orangered;color:white;" : "" %>">Day 6&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName6") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday7")) ? "background-color: orangered;color:white;" : "" %>">Day 7&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName7") %></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName1") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName2") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName3") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName4") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName5") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName6") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName7") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday8")) ? "background-color: orangered;color:white;" : "" %>">Day 8&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName8") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday9")) ? "background-color: orangered;color:white;" : "" %>">Day 9&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName9") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday10")) ? "background-color: orangered;color:white;" : "" %>">Day 10&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName10") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday11")) ? "background-color: orangered;color:white;" : "" %>">Day 11&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName11") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday12")) ? "background-color: orangered;color:white;" : "" %>">Day 12&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName12") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday13")) ? "background-color: orangered;color:white;" : "" %>">Day 13&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName13") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday14")) ? "background-color: orangered;color:white;" : "" %>">Day 14&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName14") %></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName8") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName9") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName10") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName11") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName12") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName13") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName14") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday15")) ? "background-color: orangered;color:white;" : "" %>">Day 15&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName15") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday16")) ? "background-color: orangered;color:white;" : "" %>">Day 16&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName16") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday17")) ? "background-color: orangered;color:white;" : "" %>">Day 17&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName17") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday18")) ? "background-color: orangered;color:white;" : "" %>">Day 18&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName18") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday19")) ? "background-color: orangered;color:white;" : "" %>">Day 19&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName19") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday20")) ? "background-color: orangered;color:white;" : "" %>">Day 20&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName20") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday21")) ? "background-color: orangered;color:white;" : "" %>">Day 21&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName21") %></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName15") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName16") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName17") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName18") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName19") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName20") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName21") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday22")) ? "background-color: orangered;color:white;" : "" %>">Day 22&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName22") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday23")) ? "background-color: orangered;color:white;" : "" %>">Day 23&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName23") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday24")) ? "background-color: orangered;color:white;" : "" %>">Day 24&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName24") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday25")) ? "background-color: orangered;color:white;" : "" %>">Day 25&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName25") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday26")) ? "background-color: orangered;color:white;" : "" %>">Day 26&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName26") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday27")) ? "background-color: orangered;color:white;" : "" %>">Day 27&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName27") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday28")) ? "background-color: orangered;color:white;" : "" %>">Day 28&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName28") %></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName22") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName23") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName24") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName25") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName26") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName27") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName28") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday29")) ? "background-color: orangered;color:white;" : "" %>">Day 29&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName29") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday30")) ? "background-color: orangered;color:white;" : "" %>">Day 30&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName30") %></b>
                                    </td>
                                    <td class="label" style="text-align: center;<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "WorkingDayHoliday31")) ? "background-color: orangered;color:white;" : "" %>">Day 31&nbsp;<b><%# DataBinder.Eval(Container.DataItem, "WorkingDayName31") %></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName29") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName30") %>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "WorkingHourName31") %>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true">
            </ClientSettings>
        </telerik:RadGrid>
    </div>
</asp:Content>
