<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="SalaryScaleList.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.SalaryScaleList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="SalaryScaleID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="SalaryScaleID"
                    HeaderText="ID" UniqueName="SalaryScaleID" SortExpression="SalaryScaleID"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SalaryScaleCode"
                    HeaderText="Code" UniqueName="SalaryScaleCode" SortExpression="SalaryScaleCode"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="SalaryScaleName"
                    HeaderText="Salary Scale" UniqueName="SalaryScaleName" SortExpression="SalaryScaleName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="PositionGradeName"
                    HeaderText="Position Grade" UniqueName="PositionGradeName" SortExpression="PositionGradeName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="EmploymentTypeName"
                    HeaderText="Employment Type" UniqueName="EmploymentTypeName" SortExpression="EmploymentTypeName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ProfessionGroupName"
                    HeaderText="Profession Group" UniqueName="ProfessionGroupName" SortExpression="ProfessionGroupName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="EducationGroupName"
                    HeaderText="Education Group" UniqueName="EducationGroupName" SortExpression="EducationGroupName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

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