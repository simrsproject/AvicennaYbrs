<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    Codebehind="TransactionCodeList.aspx.cs" Inherits="Temiang.Avicenna.ControlPanel.Setting.TransactionCodeList"
    Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
            <MasterTableView DataKeyNames="SRTransactionCode">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRTransactionCode" HeaderText="Transaction Code" UniqueName="SRTransactionCode" SortExpression="SRTransactionCode" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="refToAppStandardReferenceItem_ItemName" HeaderText="Transaction Name" UniqueName="TransactionCodeName" SortExpression="refToAppStandardReferenceItem_ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn DataField="SRAutoNumber" HeaderText="Auto Number" UniqueName="SRAutoNumber" SortExpression="SRAutoNumber" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
