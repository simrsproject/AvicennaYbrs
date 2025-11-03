<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="StandardSalaryList.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.StandardSalaryList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="StandardSalaryID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="StandardSalaryID"
                    HeaderText="Standard Salary ID" UniqueName="StandardSalaryID" SortExpression="StandardSalaryID"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PositionGradeID" HeaderText="Grade ID"
                    UniqueName="PositionGradeID" SortExpression="PositionGradeID" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PositionGradeCode"
                    HeaderText="Code" UniqueName="PositionGradeCode" SortExpression="PositionGradeCode"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="250px" DataField="PositionGradeName"
                    HeaderText="Position Grade" UniqueName="PositionGradeName" SortExpression="PositionGradeName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFrom" HeaderText="Valid From"
                    UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidTo" HeaderText="Valid To"
                    UniqueName="ValidTo" SortExpression="ValidTo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
