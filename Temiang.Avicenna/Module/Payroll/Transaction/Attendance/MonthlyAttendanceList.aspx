<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="MonthlyAttendanceList.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Transaction.Attendance.MonthlyAttendanceList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function openWinImport(programID) {
                var oWnd = $find("<%= winImport.ClientID %>");
                oWnd.setUrl("../Import.aspx?id=" + programID);
                oWnd.show();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="500px" Height="350px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" AutoSize="false"
        ReloadOnShow="true" ID="winImport">
    </telerik:RadWindow>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="MonthlyAttendanceID">
            <Columns>
                <telerik:GridNumericColumn DataField="MonthlyAttendanceID" UniqueName="MonthlyAttendanceID"
                    Visible="false" />
                <telerik:GridNumericColumn DataField="PersonID" UniqueName="PersonID" Visible="false" />
                <telerik:GridBoundColumn DataField="PayrollPeriodName" HeaderText="Period Name" UniqueName="PayrollPeriodName"
                    SortExpression="PayrollPeriodName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="EmployeeNumber" HeaderText="Employee No"
                    UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                    SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="OrganizationUnitName" HeaderText="Organization Unit"
                    UniqueName="OrganizationUnitName" SortExpression="OrganizationUnitName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ItemName" HeaderText="Format"
                    UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="PayDays" HeaderText="Pay Days"
                    UniqueName="PayDays" SortExpression="PayDays" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="OvertimeDays" HeaderText="Overtime Days"
                    UniqueName="OvertimeDays" SortExpression="OvertimeDays" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="UnPayDays" HeaderText="UnPay Days"
                    UniqueName="UnPayDays" SortExpression="UnPayDays" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="WorkingDays" HeaderText="Working Days"
                    UniqueName="WorkingDays" SortExpression="WorkingDays" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="ConvertOvertimeHours"
                    HeaderText="Convert OT Hours" UniqueName="ConvertOvertimeHours" SortExpression="ConvertOvertimeHours"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="LastUpdateDateTime"
                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LastUpdateByUserID"
                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>