<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="WageStructureAndScalePointsList.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.WageStructureAndScalePointsList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="WageStructureAndScaleID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="WageStructureAndScaleID"
                    HeaderText="ID" UniqueName="WageStructureAndScaleID" SortExpression="WageStructureAndScaleID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="WageStructureAndScaleTypeName"
                    HeaderText="Type" UniqueName="WageStructureAndScaleTypeName" SortExpression="WageStructureAndScaleTypeName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="WageStructureAndScaleCode"
                    HeaderText="Code" UniqueName="WageStructureAndScaleCode" SortExpression="WageStructureAndScaleCode"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="WageStructureAndScaleName"
                    HeaderText="Wage Structure And Scale" UniqueName="WageStructureAndScaleName" SortExpression="WageStructureAndScaleName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>