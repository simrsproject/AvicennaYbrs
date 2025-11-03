<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="WorkingScheduleInterventionList.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.WorkingScheduleInterventionList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoAddUrl(role) {
                var url = "WorkingScheduleInterventionDetail.aspx?md=new&role=" + role;
                window.location.href = url;
            }
        </script>

    </telerik:RadCodeBlock>

    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView DataKeyNames="WorkingSchduleInterventionID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="WorkingSchduleInterventionID" HeaderText="ID"
                    UniqueName="WorkingSchduleInterventionID" SortExpression="WorkingSchduleInterventionID" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="OrganizationUnitCode" HeaderText="Code"
                    UniqueName="OrganizationUnitCode" SortExpression="OrganizationUnitCode" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn DataField="OrganizationUnitName" HeaderText="Organization Unit"
                    UniqueName="OrganizationUnitName" SortExpression="OrganizationUnitName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="PayrollPeriodName" HeaderText="Payroll Period Name"
                    UniqueName="PayrollPeriodName" SortExpression="PayrollPeriodName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes"
                    UniqueName="Notes" SortExpression="Notes" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsVoid" HeaderText="Void"
                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="LastUpdateDateTime"
                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="UserName" HeaderText="Update By"
                    UniqueName="UserName" SortExpression="UserName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="WorkingSchduleInterventionDetailID" Name="grdDetail"
                    AutoGenerateColumns="False" AllowPaging="true" PageSize="15">
                    <Columns>
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PersonID" HeaderText="Person ID"
                            UniqueName="PersonID" SortExpression="PersonID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridNumericColumn HeaderStyle-Width="130px" DataField="EmployeeNumber" HeaderText="Employee No"
                            UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                            SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
