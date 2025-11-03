<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="VisitDownPaymentList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.Cashier.VisitDownPaymentList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td align="center">
                <telerik:RadGrid ID="grdDownPaymentSummary" runat="server" OnNeedDataSource="grdDownPaymentSummary_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" ShowFooter="true" OnDetailTableDataBind="grdDownPayment_DetailTableDataBind">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="PaymentNo" GroupLoadMode="Client">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                        runat="server" Checked="false"></asp:CheckBox>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="detailChkbox" runat="server" Checked="true"></asp:CheckBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PaymentNo" HeaderText="Down Payment No"
                                UniqueName="PaymentNo" SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="PaymentDate" HeaderText="Payment Date"
                                UniqueName="PaymentDate" SortExpression="PaymentDate" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="PaymentTime"
                                HeaderText="Payment Time" UniqueName="PaymentTime" SortExpression="PaymentTime"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="Amount" HeaderText="Amount"
                                UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                                FooterStyle-HorizontalAlign="Right" />
                            <telerik:GridTemplateColumn HeaderStyle-Width="110px" HeaderText="Amount" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" Visible="False" UniqueName="txtAmount">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txtAmount" runat="server" Width="100px" DbValue='<%#Eval("Amount")%>'
                                        NumberFormat-DecimalDigits="2" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsVisiteDownPayment" HeaderText="Visit Package"
                                UniqueName="IsVisiteDownPayment" SortExpression="IsVisiteDownPayment" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" Visible="false" />
                            <telerik:GridTemplateColumn />
                        </Columns>
                        <DetailTables>
                            <telerik:GridTableView Name="detail" DataKeyNames="ItemID" AutoGenerateColumns="false">
                                <Columns>
                                    <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                                        SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                                        SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="100px" />
                                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                        SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="VisiteQty" HeaderText="Visite Qty"
                                        UniqueName="VisiteQty" SortExpression="VisiteQty" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RealizationQty" HeaderText="Realization Qty"
                                        UniqueName="RealizationQty" SortExpression="RealizationQty" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="ClosedQty" HeaderText="Closed Qty"
                                        UniqueName="ClosedQty" SortExpression="ClosedQty" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                                        UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" FooterAggregateFormatString="{0:n2}" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Discount" HeaderText="Discount"
                                        UniqueName="Discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" FooterAggregateFormatString="{0:n2}" Visible="false" />
                                    <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Total" UniqueName="Total"
                                        DataType="System.Double" DataFields="ClosedQty,Price,Discount" SortExpression="Total"
                                        Expression="({0} * {1}) - ({2}/100 * ({0} * {1}))" FooterStyle-HorizontalAlign="Right"
                                        Aggregate="Sum" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                        DataFormatString="{0:n2}" />
                                </Columns>
                            </telerik:GridTableView>
                        </DetailTables>
                        <ExpandCollapseColumn Visible="True" />
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
