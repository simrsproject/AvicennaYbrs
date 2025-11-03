<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="EmployeeAchievementList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Query.EmployeeAchievementList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="EmployeeAchievementID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmployeeAchievementID"
                    HeaderText="Employee Achievement ID" UniqueName="EmployeeAchievementID" SortExpression="EmployeeAchievementID"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="EmployeeNumber" HeaderText="Employee No"
                    UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="EmployeeName" HeaderText="Employee Name"
                    UniqueName="EmployeeName" SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="AwardName" HeaderText="Award Name"
                    UniqueName="AwardName" SortExpression="AwardName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="160px" DataField="AwardPrize" HeaderText="Award Prize"
                    UniqueName="AwardPrize" SortExpression="AwardPrize" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="AwardDate" HeaderText="Award Date"
                    UniqueName="AwardDate" SortExpression="AwardDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="Achievement" HeaderText="Achievement"
                    UniqueName="Achievement" SortExpression="Achievement" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="FinancialValue" HeaderText="FinancialValue"
                    UniqueName="FinancialValue" SortExpression="FinancialValue" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn DataField="Note" HeaderText="Notes"
                    UniqueName="Note" SortExpression="Note" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
