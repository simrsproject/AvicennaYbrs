<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" 
	CodeBehind="AcctSubGroupList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.AcctSubGroupList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
            <MasterTableView DataKeyNames="SubLedgerGroupId">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="GroupCode" HeaderText="Group Code" UniqueName="GroupCode" SortExpression="GroupCode" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="GroupName" HeaderText="Group Name" UniqueName="GroupName" SortExpression="GroupName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="Description" HeaderText="Description" UniqueName="Description" AllowSorting="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>

