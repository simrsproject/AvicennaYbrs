<%@ Page Title="Balance Info" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="ItemBalanceInfo.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.Dispensary.PrescriptionSales.ItemBalanceInfo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdBalance" runat="server" OnNeedDataSource="grdBalance_NeedDataSource" 
        AllowSorting="true" ShowStatusBar="true" AllowPaging="true" PageSize="15">
            <MasterTableView DataKeyNames="LocationID" AutoGenerateColumns="false">
                <Columns>
                    <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name"
                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LocationID" HeaderText="Location ID"
                            UniqueName="LocationID" SortExpression="LocationID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn DataField="LocationName" HeaderText="Location Name"
                            UniqueName="LocationName" SortExpression="LocationName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="Balance" HeaderText="Balance"
                            UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                    <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRItemUnit" HeaderText="Item Unit"
                            UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
