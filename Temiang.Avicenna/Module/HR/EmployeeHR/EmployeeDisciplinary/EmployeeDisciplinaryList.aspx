<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="EmployeeDisciplinaryList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Employee.EmployeeDisciplinaryList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoAddUrl(type) {
                var url = "EmployeeDisciplinaryDetail.aspx?md=new&type=" + type;
                window.location.href = url;
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="EmployeeDisciplinaryID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="EmployeeDisciplinaryID"
                    HeaderText="Employee Disciplinary ID" UniqueName="EmployeeDisciplinaryID" SortExpression="EmployeeDisciplinaryID"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PersonID" HeaderText="Person ID"
                    UniqueName="PersonID" SortExpression="PersonID" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="EmployeeNumber" HeaderText="Employee No"
                    UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name"
                    UniqueName="EmployeeName" SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRWarningLevel" HeaderText="Warning Level"
                    UniqueName="SRWarningLevel" SortExpression="SRWarningLevel" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="WarningLevelName" HeaderText="Warning Level"
                    UniqueName="WarningLevelName" SortExpression="WarningLevelName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="IncidentDate" HeaderText="Incident Date"
                    UniqueName="IncidentDate" SortExpression="IncidentDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="DateIssue" HeaderText="Date Issue"
                    UniqueName="DateIssue" SortExpression="DateIssue" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="Violation" HeaderText="Violation"
                    UniqueName="Violation" SortExpression="Violation" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="AdviceGiven" HeaderText="Advice Given"
                    UniqueName="AdviceGiven" SortExpression="AdviceGiven" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRViolationDegree" HeaderText="Violation Degree"
                    UniqueName="SRViolationDegree" SortExpression="SRViolationDegree" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRViolationType" HeaderText="Violation Type"
                    UniqueName="SRViolationType" SortExpression="SRViolationType" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ViolationDegreeName"
                    HeaderText="Violation Degree" UniqueName="ViolationDegreeName" SortExpression="ViolationDegreeName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ViolationTypeName" HeaderText="Violation Type"
                    UniqueName="ViolationTypeName" SortExpression="ViolationTypeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
