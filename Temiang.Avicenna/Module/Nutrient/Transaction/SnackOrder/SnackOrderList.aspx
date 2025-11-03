<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="SnackOrderList.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Transaction.SnackOrderList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView DataKeyNames="SnackOrderNo">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="SnackOrderNo" HeaderText="Order No"
                    UniqueName="SnackOrderNo" SortExpression="SnackOrderNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="SnackOrderDate"
                    HeaderText="Order Date" UniqueName="SnackOrderDate" SortExpression="SnackOrderDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="SnackOrderForDate"
                    HeaderText="For Date" UniqueName="SnackOrderForDate" SortExpression="SnackOrderForDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                    SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsVoid" HeaderText="Void"
                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="SnackID" Name="grdDetail" AutoGenerateColumns="False"
                    AllowPaging="true" PageSize="10">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SnackID" HeaderText="ID"
                            UniqueName="SnackID" SortExpression="SnackID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="SnackName" HeaderText="Snack Name"
                            UniqueName="SnackName" SortExpression="SnackName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="Notes" HeaderText="Notes"
                            UniqueName="Notes" SortExpression="Notes" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="50" DataField="QtyShift1" HeaderText="Shift I"
                            UniqueName="QtyShift1" SortExpression="QtyShift1" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="50" DataField="QtyShift2" HeaderText="Shift II"
                            UniqueName="QtyShift2" SortExpression="QtyShift2" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="50" DataField="QtyShift3" HeaderText="Shift III"
                            UniqueName="QtyShift3" SortExpression="QtyShift3" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                        <telerik:GridCalculatedColumn HeaderStyle-Width="80px" HeaderText="Total" UniqueName="Total"
                            DataType="System.Double" DataFields="QtyShift1, QtyShift2, QtyShift3" Expression="{0}+{1}+{2}"
                            FooterText=" " FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true"
                            Aggregate="Sum" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n0}" />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
