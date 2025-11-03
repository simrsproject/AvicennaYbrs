<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PaymentTypeDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.CheckDetailTransaction.PaymentTypeDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td align="center">
                <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpagDetail">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Payment Item" Selected="True" PageViewID="pgPaymentItem" />
                        <telerik:RadTab runat="server" Text="Transaction Item" PageViewID="pgTransactionItem" />
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="mpagDetail" runat="server" BorderStyle="Solid" SelectedIndex="0"
                    BorderColor="Gray">
                    <telerik:RadPageView ID="pgPaymentItem" runat="server">
                        <telerik:RadGrid ID="grdDetail" runat="server" OnNeedDataSource="grdDetail_NeedDataSource"
                            AutoGenerateColumns="False" GridLines="None" ShowFooter="true" AllowPaging="true"
                            PageSize="100">
                            <HeaderContextMenu>
                            </HeaderContextMenu>
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, SequenceNo"
                                GroupLoadMode="Client">
                                <Columns>
                                    <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Transaction No"
                                        UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="PaymentDate" HeaderText="Payment Date"
                                        UniqueName="PaymentDate" SortExpression="PaymentDate" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridBoundColumn DataField="RegistrationNo" HeaderStyle-Width="150px" HeaderText="Registration No"
                                        UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridBoundColumn DataField="PaymentType" HeaderText="Payment Type" HeaderStyle-Width="120px"
                                        UniqueName="PaymentType" SortExpression="PaymentType" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Right" />
                                    <telerik:GridBoundColumn DataField="PaymentMethod" HeaderText="Payment Method" HeaderStyle-Width="120px"
                                        UniqueName="PaymentMethod" SortExpression="PaymentMethod" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Right" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="CardProvider" HeaderText="Card Provider"
                                        UniqueName="CardProvider" SortExpression="CardProvider" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="EDCMachine" HeaderText="EDC Machine"
                                        UniqueName="EDCMachine" SortExpression="EDCMachine" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right" Aggregate="Count"
                                        FooterAggregateFormatString="Total :" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="Amount" HeaderText="Amount"
                                        UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" FooterStyle-HorizontalAlign="Right"
                                        Aggregate="Sum" />
                                </Columns>
                            </MasterTableView>
                            <FilterMenu>
                            </FilterMenu>
                            <ClientSettings EnableRowHoverStyle="true">
                                <Resizing AllowColumnResize="True" />
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pgTransactionItem" runat="server">
                        <telerik:RadGrid ID="grdTransItem" runat="server" OnNeedDataSource="grdTransItem_NeedDataSource"
                            AutoGenerateColumns="False" GridLines="None" ShowFooter="true" AllowPaging="true"
                            PageSize="100">
                            <HeaderContextMenu>
                            </HeaderContextMenu>
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, SequenceNo"
                                GroupLoadMode="Client">
                                <GroupByExpressions>
                                    <telerik:GridGroupByExpression>
                                        <SelectFields>
                                            <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Service Unit " />
                                        </SelectFields>
                                        <GroupByFields>
                                            <telerik:GridGroupByField FieldName="ServiceUnitName" SortOrder="None" />
                                        </GroupByFields>
                                    </telerik:GridGroupByExpression>
                                </GroupByExpressions>
                                <Columns>
                                    <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Transaction No"
                                        UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridBoundColumn DataField="ItemID" HeaderStyle-Width="100px" HeaderText="Item ID"
                                        UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                        SortExpression="ItemName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Qty" HeaderText="Qty"
                                        UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                        DataFormatString="{0:n2}"/>
                                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                                        UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Discount" HeaderText="Discount"
                                        UniqueName="Discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="PatientAmount" HeaderText="Patient Amount"
                                        UniqueName="PatientAmount" SortExpression="PatientAmount" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" FooterStyle-HorizontalAlign="Right"
                                        Aggregate="Sum" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="GuarantorAmount"
                                        HeaderText="Guarantor Amount" UniqueName="GuarantorAmount" SortExpression="GuarantorAmount"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"
                                        FooterStyle-HorizontalAlign="Right" Aggregate="Sum" />
                                </Columns>
                            </MasterTableView>
                            <FilterMenu>
                            </FilterMenu>
                            <ClientSettings EnableRowHoverStyle="true">
                                <Resizing AllowColumnResize="True" />
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </td>
        </tr>
    </table>
</asp:Content>
