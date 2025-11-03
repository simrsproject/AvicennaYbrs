<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="SalaryComponentList.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.SalaryComponentList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="SalaryComponentID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="SalaryComponentID"
                    HeaderText="Salary Component ID" UniqueName="SalaryComponentID" SortExpression="SalaryComponentID"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SalaryComponentCode"
                    HeaderText="Salary Code" UniqueName="SalaryComponentCode" SortExpression="SalaryComponentCode"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="270px" DataField="SalaryComponentName"
                    HeaderText="Salary Component Name" UniqueName="SalaryComponentName" SortExpression="SalaryComponentName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="SalaryTypeName" HeaderText="Salary Type"
                    UniqueName="SRSalaryType" SortExpression="SRSalaryType" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="SalaryCategoryName"
                    HeaderText="Salary Category" UniqueName="SRSalaryCategory" SortExpression="SRSalaryCategory"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="IncomeTaxMethodName"
                    HeaderText="Income Tax Method" UniqueName="IncomeTaxMethodName" SortExpression="IncomeTaxMethodName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="DeductionTypeName"
                    HeaderText="Deduction Type" UniqueName="DeductionTypeName" SortExpression="DeductionTypeName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false"/>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="JamsostekTypeName"
                    HeaderText="Jamsostek Type" UniqueName="JamsostekTypeName" SortExpression="JamsostekTypeName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFrom" HeaderText="Valid From"
                    UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidTo" HeaderText="Valid To"
                    UniqueName="ValidTo" SortExpression="ValidTo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
