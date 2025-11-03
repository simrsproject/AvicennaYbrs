<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="EmployeeEmploymentPeriodList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Query.EmployeeEmploymentPeriodList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="EmployeeEmploymentPeriodID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmployeeEmploymentPeriodID"
                    HeaderText="Employee Employment Period ID" UniqueName="EmployeeEmploymentPeriodID"
                    SortExpression="EmployeeEmploymentPeriodID" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="EmployeeNumber" HeaderText="Employee No"
                    UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="EmployeeName" HeaderText="Employee Name"
                    UniqueName="EmployeeName" SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="EmploymentTypeName"
                    HeaderText="Employment Type" UniqueName="EmploymentTypeName" SortExpression="EmploymentTypeName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFrom" HeaderText="Valid From"
                    UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidTo" HeaderText="Valid To"
                    UniqueName="ValidTo" SortExpression="ValidTo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="Note" HeaderText="Notes"
                    UniqueName="Note" SortExpression="Note" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
