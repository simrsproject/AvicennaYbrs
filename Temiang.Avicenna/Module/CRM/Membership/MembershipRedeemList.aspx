<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="MembershipRedeemList.aspx.cs" Inherits="Temiang.Avicenna.Module.CRM.MembershipRedeemList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" OnDetailTableDataBind="grdList_DetailTableDataBind"
                    AutoGenerateColumns="False" GridLines="None" AllowPaging="true" AllowSorting="False">
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo" GroupLoadMode="Client">
                        <Columns>
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="TransactionNo" HeaderText="Transaction No"
                                UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate" HeaderText="Date"
                                UniqueName="TransactionDate" SortExpression="TransactionDate" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn DataField="PatientName" HeaderText="Redeemed By" UniqueName="PatientName"
                                SortExpression="PatientName" HeaderStyle-Width="275px">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Address" HeaderText="Address" UniqueName="Address"
                                SortExpression="Address">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PhoneNo" HeaderText="Phone No" UniqueName="PhoneNo"
                                SortExpression="PhoneNo">
                                <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MobilePhoneNo" HeaderText="Mobile Phone No" UniqueName="MobilePhoneNo"
                                SortExpression="MobilePhoneNo">
                                <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="TotalPointsUses"
                                HeaderText="Total Points Used" UniqueName="TotalPointsUses" SortExpression="TotalPointsUses"
                                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                            <telerik:GridTemplateColumn />
                        </Columns>
                        <DetailTables>
                            <telerik:GridTableView DataKeyNames="ItemReedemID" Name="grdListDetail" Width="100%"
                                AutoGenerateColumns="false">
                                <Columns>
                                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemReedemID" HeaderText="Item ID"
                                        UniqueName="ItemReedemID" SortExpression="ItemReedemID" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ItemReedemGroup" HeaderText="Group"
                                        UniqueName="ItemReedemGroup" SortExpression="ItemReedemGroup" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ItemReedemName" HeaderText="Item Name"
                                        UniqueName="ItemReedemName" SortExpression="ItemReedemName" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Qty"
                                        HeaderText="Qty" UniqueName="Qty" SortExpression="Qty"
                                        HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PointsUsed"
                                        HeaderText="Points Used" UniqueName="PointsUsed" SortExpression="PointsUsed"
                                        HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="TotalPointsUsed"
                                        HeaderText="Total" UniqueName="TotalPointsUsed" SortExpression="TotalPointsUsed"
                                        HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                                    <telerik:GridTemplateColumn />
                                </Columns>
                            </telerik:GridTableView>
                        </DetailTables>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="True">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
