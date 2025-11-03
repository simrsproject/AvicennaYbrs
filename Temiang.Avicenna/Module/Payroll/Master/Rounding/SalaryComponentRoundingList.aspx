<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="SalaryComponentRoundingList.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.SalaryComponentRoundingList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="SalaryComponentRoundingID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="SalaryComponentRoundingID"
                    HeaderText="Salary Component Rounding ID" UniqueName="SalaryComponentRoundingID"
                    SortExpression="SalaryComponentRoundingID" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="400px" DataField="SalaryComponentRoundingName"
                    HeaderText="Salary Component Rounding Name" UniqueName="SalaryComponentRoundingName"
                    SortExpression="SalaryComponentRoundingName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="NominalValue" HeaderText="Nominal Value"
                    UniqueName="NominalValue" SortExpression="NominalValue" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="NearestValue" HeaderText="Nearest Value"
                    UniqueName="NearestValue" SortExpression="NearestValue" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime"
                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
