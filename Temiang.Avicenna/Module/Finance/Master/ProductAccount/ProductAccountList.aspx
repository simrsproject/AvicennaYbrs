<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="ProductAccountList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.ProductAccountList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="ProductAccountID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ProductAccountID" HeaderText="Product Account ID"
                    UniqueName="ProductAccountID" SortExpression="ProductAccountID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ProductAccountName"
                    HeaderText="Product Account Name" UniqueName="ProductAccountName" SortExpression="ProductAccountName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ItemTypeName"
                    HeaderText="Item Type" UniqueName="ItemTypeName" SortExpression="ItemTypeName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsPpnOpr" HeaderText="IsPpnOpr"
                    UniqueName="IsPpnOpr" SortExpression="IsPpnOpr" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsPpnEmr" HeaderText="IsPpnEmr"
                    UniqueName="IsPpnEmr" SortExpression="IsPpnEmr" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
