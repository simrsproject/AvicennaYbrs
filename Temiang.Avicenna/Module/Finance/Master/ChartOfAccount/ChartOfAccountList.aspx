<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="ChartOfAccountList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.ChartOfAccountList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server"
        OnNeedDataSource="grdList_NeedDataSource"
        AllowPaging="false" AllowCustomPaging="false">
        <MasterTableView DataKeyNames="ChartOfAccountId">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ChartOfAccountCode" HeaderText="Chart Of Account Code" AllowSorting="false" UniqueName="ChartOfAccountCode" SortExpression="ChartOfAccountCode" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderText="Account Name" DataField="ChartOfAccountName" UniqueName="ChartOfAccountName" AllowSorting="false" SortExpression="ChartOfAccountName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="AccountLevel" HeaderText="Level" UniqueName="AccountLevel" AllowSorting="false" SortExpression="AccountLevel" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="NormalBalance" HeaderText="D/C" UniqueName="NormalBalance" AllowSorting="false" SortExpression="NormalBalance" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="GeneralAccount" HeaderText="General Account" UniqueName="GeneralAccount" AllowSorting="false" SortExpression="GeneralAccount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="BkuAccountCode" HeaderText="BKU Account" UniqueName="BkuAccountCode" AllowSorting="false" SortExpression="BkuAccountCode" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active" UniqueName="IsActive" AllowSorting="false" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>

