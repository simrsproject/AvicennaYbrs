<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="DisciplinarySanctionsList.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.DisciplinarySanctionsList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="DisciplinarySanctionsID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="DisciplinarySanctionsID" HeaderText="ID"
                    UniqueName="DisciplinarySanctionsID" SortExpression="DisciplinarySanctionsID" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center"  />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SREmploymentType" HeaderText="Code"
                    UniqueName="SREmploymentType" SortExpression="SREmploymentType" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false"/>
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="EmploymentTypeName"
                    HeaderText="Employment Type" UniqueName="EmploymentTypeName" SortExpression="EmploymentTypeName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="StartValue" HeaderText="Start Value"
                    UniqueName="StartValue" SortExpression="StartValue" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DecimalDigits="0"/>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EndValue" HeaderText="End Value"
                    UniqueName="EndValue" SortExpression="EndValue" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DecimalDigits="0"/>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CutPercentage" HeaderText="Cut (%)"
                    UniqueName="CutPercentage" SortExpression="CutPercentage" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DecimalDigits="2" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFromDate" HeaderText="Valid From"
                    UniqueName="ValidFromDate" SortExpression="ValidFromDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
