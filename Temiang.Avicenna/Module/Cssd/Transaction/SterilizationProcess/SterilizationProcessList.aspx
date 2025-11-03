<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="SterilizationProcessList.aspx.cs" Inherits="Temiang.Avicenna.Module.Cssd.Transaction.SterilizationProcessList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoAddUrl(dtt) {
                var url = "SterilizationProcessDetail.aspx?md=new&dtt=" + dtt;
                window.location.href = url;
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnDetailTableDataBind="grdList_DetailTableDataBind" AllowPaging="true" PageSize="15"
        AutoGenerateColumns="false">
        <MasterTableView DataKeyNames="ProcessNo" GroupLoadMode="Client">
            <Columns>
                <telerik:GridHyperLinkColumn HeaderStyle-Width="150px" DataTextField="ProcessNo"
                    DataNavigateUrlFields="PUrl" HeaderText="Process No" UniqueName="ProcessNo" SortExpression="ProcessNo"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ProcessDate" HeaderText="Date"
                    UniqueName="ProcessDate" SortExpression="ProcessDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="ProcessStartTime" HeaderText="Start Time"
                    UniqueName="ProcessStartTime" SortExpression="ProcessStartTime" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="ProcessEndTime" HeaderText="End Time"
                    UniqueName="ProcessEndTime" SortExpression="ProcessEndTime" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="CssdProcessTypeName" HeaderText="Process Type" UniqueName="CssdProcessTypeName"
                    SortExpression="CssdProcessTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />    
                <telerik:GridBoundColumn DataField="MachineName" HeaderText="Machine" UniqueName="MachineName"
                    SortExpression="MachineName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="OperatorBy" HeaderText="Operator By"
                    UniqueName="OperatorBy" SortExpression="OperatorBy" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="ProcessTo" HeaderText="Process #"
                    UniqueName="ProcessTo" SortExpression="ProcessTo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n0}" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="ProcessNo, ProcessSeqNo" Name="grdDetail" AutoGenerateColumns="False"
                    AllowPaging="true" PageSize="15">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="ProcessSeqNo" HeaderText="Seq No"
                            UniqueName="ProcessSeqNo" SortExpression="ProcessSeqNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemNo" HeaderText="Item #"
                            UniqueName="ItemNo" SortExpression="ItemNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ItemName" HeaderText="Item Name"
                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Qty" HeaderText="Qty"
                            UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n2}" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="CssdItemUnit" HeaderText="Unit"
                            UniqueName="CssdItemUnit" SortExpression="CssdItemUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Weight" HeaderText="Right"
                            UniqueName="Weight" SortExpression="Weight" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
