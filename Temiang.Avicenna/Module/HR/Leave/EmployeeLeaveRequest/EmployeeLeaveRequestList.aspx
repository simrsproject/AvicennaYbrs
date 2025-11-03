<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="EmployeeLeaveRequestList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Leave.EmployeeLeaveRequestList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoAddUrl(type) {
                var url = "EmployeeLeaveRequestDetail.aspx?md=new&type=" + type;
                window.location.href = url;
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="LeaveRequestID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="LeaveRequestID" HeaderText="ID"
                    UniqueName="LeaveRequestID" SortExpression="LeaveRequestID" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" Visible="False" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="RequestDate" HeaderText="Request Date"
                    UniqueName="RequestDate" SortExpression="RequestDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmployeeNumber" HeaderText="Employee No"
                    UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                    SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="EmployeeLeaveTypeName" HeaderText="Employee Leave Type"
                    UniqueName="EmployeeLeaveTypeName" SortExpression="EmployeeLeaveTypeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="RequestLeaveDateFrom"
                    HeaderText="Request Leave Date From" UniqueName="RequestLeaveDateFrom" SortExpression="RequestLeaveDateFrom"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="RequestLeaveDateTo"
                    HeaderText="Request Leave Date To" UniqueName="RequestLeaveDateTo" SortExpression="RequestLeaveDateTo"
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
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="VerifiedDateTime" HeaderText="Verified Date"
                    UniqueName="VerifiedDateTime" SortExpression="VerifiedDateTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="VerifiedBy" HeaderText="Verified By"
                    UniqueName="VerifiedBy" SortExpression="VerifiedBy" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsVoid" HeaderText="Void"
                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />    
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
