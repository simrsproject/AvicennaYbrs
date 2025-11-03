<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="MembershipItemRedemptionList.aspx.cs" Inherits="Temiang.Avicenna.Module.CRM.MembershipItemRedemptionList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:radgrid id="grdList" runat="server" onneeddatasource="grdList_NeedDataSource"
        ondetailtabledatabind="grdList_DetailTableDataBind">
        <MasterTableView DataKeyNames="TransactionNo">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="TransactionNo" HeaderText="Transaction No"
                    UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="TransactionDate" HeaderText="Date" UniqueName="TransactionDate"
                    SortExpression="TransactionDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="MembershipNo" HeaderText="Member No"
                    UniqueName="MembershipNo" SortExpression="MembershipNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="JoinDate" HeaderText="Join Date" UniqueName="JoinDate"
                    SortExpression="JoinDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PatientName" HeaderText="Member Name" UniqueName="PatientName"
                    SortExpression="PatientName" HeaderStyle-Width="275px">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Address" HeaderText="Address" UniqueName="Address"
                    SortExpression="Address">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PhoneNo" HeaderText="Phone No" UniqueName="PhoneNo"
                    SortExpression="PhoneNo">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RedeemedBy" HeaderText="Redeemed By" UniqueName="RedeemedBy"
                    SortExpression="RedeemedBy">
                    <HeaderStyle HorizontalAlign="Left" Width="275px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved" UniqueName="IsApproved"
                    SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsVoid" HeaderText="Void" UniqueName="IsVoid"
                    SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="ItemReedemID" Name="grdListItem" Width="100%"
                    AutoGenerateColumns="false">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemReedemID" HeaderText="ID"
                            UniqueName="ItemReedemID" SortExpression="ItemReedemID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ItemReedemName" HeaderText="Item Reedem Name" UniqueName="ItemReedemName"
                            SortExpression="ItemReedemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ItemReedemGroup" HeaderText="Group"
                            UniqueName="ItemReedemGroup" SortExpression="ItemReedemGroup" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Qty" HeaderText="Qty"
                            UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PointsUsed" HeaderText="Points Used"
                            UniqueName="PointsUsed" SortExpression="PointsUsed" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="TotalPointsUsed" HeaderText="Total Points Used"
                            UniqueName="TotalPointsUsed" SortExpression="TotalPointsUsed" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                        <telerik:GridTemplateColumn />
                    </Columns>
                </telerik:GridTableView>
                <telerik:GridTableView DataKeyNames="MembershipDetailID" Name="grdListDetail" Width="100%"
                    AutoGenerateColumns="false">
                    <Columns>
                        <telerik:GridBoundColumn DataField="StartDate" HeaderText="Active Date" UniqueName="StartDate"
                            SortExpression="StartDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EndDate" HeaderText="Valid Thru" UniqueName="EndDate"
                            SortExpression="EndDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="ClaimedPoint" HeaderText="Redeemed Points"
                            UniqueName="ClaimedPoint" SortExpression="ClaimedPoint" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                        <telerik:GridTemplateColumn />
                    </Columns>
                </telerik:GridTableView>
                
            </DetailTables>
        </MasterTableView>
    </telerik:radgrid>
</asp:Content>
