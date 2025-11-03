<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashTransactionListList.aspx.cs" MasterPageFile="~/MasterPage/MasterList.Master"
Inherits="Temiang.Avicenna.Module.Finance.Master.CashTransactionListList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="ListId">
            <Columns>
                <telerik:GridBoundColumn DataField="ListId" HeaderText="ID" UniqueName="ListId"
                    SortExpression="ListId" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px"/>
                <telerik:GridBoundColumn DataField="Description" HeaderText="Description" UniqueName="Description"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="CashManagementType" HeaderText="Cash Type" UniqueName="CashType"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="120px"/>
                <telerik:GridBoundColumn DataField="ChartOfAccountCode" HeaderText="Chart of Account Code" UniqueName="ChartOfAccountCode"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="180px"/>
                <telerik:GridBoundColumn DataField="ChartOfAccountName" HeaderText="Chart of Account Name" UniqueName="ChartOfAccountName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="400px"/>
                <telerik:GridBoundColumn DataField="SubledgerName" HeaderText="Subledger Name" UniqueName="SubledgerName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="250px"/>                
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>


