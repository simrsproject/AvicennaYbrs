<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="PersonalEducationHistoryList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Query.PersonalEducationHistoryList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PersonalEducationHistoryID"
                    HeaderText="Personal Education History ID" UniqueName="PersonalEducationHistoryID"
                    SortExpression="PersonalEducationHistoryID" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="EmployeeNumber" HeaderText="Employee No"
                    UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="EmployeeName" HeaderText="Employee Name"
                    UniqueName="EmployeeName" SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="EducationLevelName"
                    HeaderText="Education Level" UniqueName="EducationLevelName" SortExpression="EducationLevelName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="InstitutionName" HeaderText="Institution Name"
                    UniqueName="InstitutionName" SortExpression="InstitutionName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="Location" HeaderText="Location"
                    UniqueName="Location" SortExpression="Location" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="StartYear" HeaderText="Start Year"
                    UniqueName="StartYear" SortExpression="StartYear" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="EndYear" HeaderText="End Year"
                    UniqueName="EndYear" SortExpression="EndYear" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="40px" DataField="Gpa" HeaderText="GPA"
                    UniqueName="Gpa" SortExpression="Gpa" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="Achievement" HeaderText="Achievement"
                    UniqueName="Achievement" SortExpression="Achievement" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Note" HeaderText="Notes"
                    UniqueName="Note" SortExpression="Note" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
