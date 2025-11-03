<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialogHistEntry.Master"
    AutoEventWireup="true" CodeBehind="VisiteRealizationEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.ServiceUnitTransaction.VisiteRealizationEntry" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphEntry" runat="server">
    <fieldset>
        <legend>Realization for Visite Order</legend>
        <telerik:RadGrid ID="grdVisite" runat="server" OnNeedDataSource="grdVisite_NeedDataSource"
            OnItemCommand="grdVisite_ItemCommand" GridLines="None" AutoGenerateColumns="false"
            Height="150px" AllowMultiRowSelection="true">
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
            </ClientSettings>
            <MasterTableView Name="master" DataKeyNames="TransactionNo,ItemID">
                <Columns>
                    <telerik:GridClientSelectColumn HeaderStyle-Width="50px" HeaderText="" UniqueName="SelectColumn" />
                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                        SortExpression="ItemName">
                        <HeaderStyle HorizontalAlign="Left" Width="150px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="VisiteQty" HeaderText="Order Qty"
                        UniqueName="VisiteQty" SortExpression="VisiteQty" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right" />
                    <telerik:GridNumericColumn HeaderStyle-Width="90px" DataField="RealizationQty" HeaderText="Realization"
                        UniqueName="RealizationQty" SortExpression="RealizationQty" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right" />
                    <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="BalanceQty" HeaderText="Available"
                        UniqueName="BalanceQty" SortExpression="BalanceQty" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right" />
                    <telerik:GridDateTimeColumn DataField="RegistrationDate" HeaderText="Reg. Date" UniqueName="RegistrationDate"
                        SortExpression="RegistrationDate">
                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                        UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                        <HeaderStyle HorizontalAlign="Center" Width="130px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="ItemName">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn UniqueName="CloseVisite" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnCloseVisite" runat="server" CommandName="CloseVisite" ToolTip='Close this visite'
                                OnClientClick="javascript:if (!confirm('Close this visite order?')) return false;"
                                CommandArgument='<%#string.Format("{0}_{1}", DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "ItemID")) %>'>
                            <img src="../../../../../Images/cancel16.png" border="0" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </fieldset>
    <fieldset>
        <legend>Transaction Charge</legend>
        <telerik:RadGrid ID="grdTransCharges" runat="server" OnNeedDataSource="grdTransCharges_NeedDataSource" 
            GridLines="None" AutoGenerateColumns="false" Height="150px" AllowMultiRowSelection="true">
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
            </ClientSettings>
            <MasterTableView Name="master" DataKeyNames="TransactionNo,SequenceNo, FromServiceUnitID">
                <Columns>
                    <telerik:GridClientSelectColumn HeaderStyle-Width="50px" HeaderText="Void" UniqueName="SelectColumn" />
                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="TransactionNo" HeaderText="Transaction No"
                        UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridDateTimeColumn HeaderStyle-Width="75px" DataField="TransactionDate"
                        HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                        SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                        Visible="false" />
                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item" UniqueName="ItemName"
                        SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn DataField="ParamedicCollectionName" HeaderText="Physician"
                        UniqueName="ParamedicCollectionName" SortExpression="ParamedicCollectionName"
                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="ChargeQuantity" HeaderText="Qty"
                        UniqueName="ChargeQuantity" SortExpression="ChargeQuantity" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right" />
                    <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Price" HeaderText="Price"
                        UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Total" UniqueName="Total"
                        DataType="System.Double" DataFields="ChargeQuantity,Price,DiscountAmount,CitoAmount"
                        SortExpression="Total" Expression="(({0} * {1}) - {2}) + {3}" FooterStyle-HorizontalAlign="Right"
                        Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                        DataFormatString="{0:n2}" />
                    <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsBillProceed" HeaderText="Process"
                        UniqueName="IsBillProceed" SortExpression="IsBillProceed" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                        UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsPackage" HeaderText="Package"
                        UniqueName="IsPackage" SortExpression="IsPackage" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" Visible="false" />
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </fieldset>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphList" runat="server">
    <fieldset>
        <legend>Visite History</legend>
        <telerik:RadGrid ID="grdHist" runat="server" OnNeedDataSource="grdHist_NeedDataSource"
            OnDetailTableDataBind="grdHist_DetailTableDataBind" OnItemCommand="grdHist_ItemCommand" GridLines="None" AutoGenerateColumns="false"
            AllowPaging="true" PageSize="15">
            <MasterTableView Name="header" DataKeyNames="TransactionNo,ItemID">
                <Columns>
                    <telerik:GridDateTimeColumn DataField="RegistrationDate" HeaderText="Reg. Date" UniqueName="RegistrationDate"
                        SortExpression="RegistrationDate">
                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                        UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                        <HeaderStyle HorizontalAlign="Center" Width="130px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                        SortExpression="ItemName">
                        <HeaderStyle HorizontalAlign="Left" Width="150px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="VisiteQty" HeaderText="Order Qty"
                        UniqueName="VisiteQty" SortExpression="VisiteQty" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right" />
                    <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="RealizationQty" HeaderText="Realization"
                        UniqueName="RealizationQty" SortExpression="RealizationQty" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right" />
                    <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="AvailableQty" HeaderText="Available"
                        UniqueName="AvailableQty" SortExpression="AvailableQty" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right" />
                    <telerik:GridCheckBoxColumn HeaderText="Closed" DataField="IsClosed" HeaderStyle-Width="40px" />
                    <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="ItemName">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                </Columns>
                <DetailTables>
                    <telerik:GridTableView Name="detail" AutoGenerateColumns="false">
                        <Columns>
                            <telerik:GridDateTimeColumn DataField="RegistrationDate" HeaderText="Reg. Date" UniqueName="RegistrationDate"
                                SortExpression="RegistrationDate">
                                <HeaderStyle HorizontalAlign="Center" Width="75px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                                UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                                <HeaderStyle HorizontalAlign="Center" Width="130px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                                SortExpression="ServiceUnitName">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                                SortExpression="ParamedicName">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="CancelRealization" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnCancelRealization" runat="server" CommandName="CancelRealization" ToolTip='Cancel this realization'
                                        OnClientClick="javascript:if (!confirm('Cancel this visite realization?')) return false;"
                                        CommandArgument='<%#string.Format("{0}_{1}_{2}", DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "ItemID"), DataBinder.Eval(Container.DataItem, "RegistrationNo")) %>'>
                            <img src="../../../../../Images/cancel16.png" border="0" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </telerik:GridTableView>
                </DetailTables>
            </MasterTableView>
        </telerik:RadGrid>
    </fieldset>
</asp:Content>
