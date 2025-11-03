<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="RemunerationBaseList.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Remuneration.Base.RemunerationBaseList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="WageBaseID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="WageBaseID"
                    HeaderText="ID" UniqueName="WageBaseID" SortExpression="WageBaseID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" visible="false" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="ValidFrom"
                    HeaderText="Valid From" UniqueName="ValidFrom" SortExpression="ValidFrom"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="Nominal" HeaderText="Nominal" UniqueName="Nominal"
                    SortExpression="Nominal" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
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