<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="WorkingHourList.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.WorkingHourList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="WorkingHourID">
            <Columns>
                <telerik:GridBoundColumn DataField="WorkingHourName" HeaderText="Working Hour Name"
                    UniqueName="WorkingHourName" SortExpression="WorkingHourName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Shift Category"
                    UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="WorkingDayName" HeaderText="Working Day"
                    UniqueName="WorkingDayName" SortExpression="WorkingDayName" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="StartTime" HeaderText="Check In"
                    UniqueName="StartTime" SortExpression="StartTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="MinimumStartTime" HeaderText="Min. Check In"
                    UniqueName="MinimumStartTime" SortExpression="MinimumStartTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="MaximumStartTime" HeaderText="Max. Check In"
                    UniqueName="MaximumStartTime" SortExpression="MaximumStartTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="EndTime" HeaderText="Check Out"
                    UniqueName="EndTime" SortExpression="EndTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="MinimumEndTime" HeaderText="Min. Check Out"
                    UniqueName="MinimumEndTime" SortExpression="MinimumEndTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="110px" DataField="MaximumEndTime" HeaderText="Max. Check Out"
                    UniqueName="MaximumEndTime" SortExpression="MaximumEndTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="MealQty" HeaderText="Meal Qty"
                    UniqueName="MealQty" SortExpression="MealQty" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsOvertimeWorkingHour" HeaderText="Overtime"
                    UniqueName="IsOvertimeWorkingHour" SortExpression="IsOvertimeWorkingHour" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsNotValidated" HeaderText="Not Validated"
                    UniqueName="IsNotValidated" SortExpression="IsNotValidated" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsOffDay" HeaderText="Off Day"
                    UniqueName="IsOffDay" SortExpression="IsOffDay" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsCrossDay" HeaderText="Cross Day"
                    UniqueName="IsCrossDay" SortExpression="IsCrossDay" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
