<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="FormsList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.K3RS.FormsList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="TransactionNo">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="120px"  DataField="TransactionNo"
                    HeaderText="Transaction No" UniqueName="TransactionNo" SortExpression="TransactionNo"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate" HeaderText="Date"
                    UniqueName="TransactionDate" SortExpression="TransactionDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="TemplateName"
                    HeaderText="Form Name" UniqueName="TemplateName" SortExpression="TemplateName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Notes"
                    HeaderText="Notes" UniqueName="Notes" SortExpression="Notes"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>