<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="DosageUnitList.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Master.DosageUnitList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="ItemID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="ItemID"
                    HeaderText="Dosage Unit ID" UniqueName="ItemID" SortExpression="ItemID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn DataField="ItemName"
                    HeaderText="Dosage Unit Name" UniqueName="ItemName" SortExpression="ItemName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>