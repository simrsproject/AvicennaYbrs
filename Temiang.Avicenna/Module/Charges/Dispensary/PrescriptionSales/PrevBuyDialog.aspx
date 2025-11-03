<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="PrevBuyDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.Dispensary.PrescriptionSales.RSCH.PrevBuyDialog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" 
        AllowSorting="true" ShowStatusBar="true" AllowPaging="true" PageSize="15">
            <MasterTableView DataKeyNames="PrescriptionNo,SequenceNo" AutoGenerateColumns="false">
                <Columns>
                    <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ItemName" HeaderText="Item Name"
                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PrescriptionNo" HeaderText="Prescription No"
                            UniqueName="PrescriptionNo" SortExpression="PrescriptionNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="PrescriptionDate"
                            HeaderText="Presc. Date" UniqueName="PrescriptionDate" SortExpression="PrescriptionDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="TakenQty" HeaderText="Qty"
                            UniqueName="TakenQty" SortExpression="TakenQty" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRItemUnit" HeaderText="Item Unit"
                            UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridTemplateColumn />
                </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>