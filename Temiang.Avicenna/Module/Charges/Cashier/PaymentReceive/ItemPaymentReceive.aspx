<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="ItemPaymentReceive.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.PaymentReceive.ItemPaymentReceive" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <telerik:RadGrid ID="grdTransPaymentItem" runat="server" OnNeedDataSource="grdTransPaymentItem_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" ShowFooter="true">
                    <HeaderContextMenu>
                        
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="PaymentNo, SequenceNo">
                        <Columns>
                            <telerik:GridBoundColumn DataField="PaymentTypeName" HeaderText="Payment Type Name"
                                UniqueName="PaymentTypeName" SortExpression="PaymentTypeName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="PaymentMethodName" HeaderText="Payment Method Name"
                                UniqueName="PaymentMethodName" SortExpression="PaymentMethodName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" Aggregate="count" FooterAggregateFormatString="Total :"
                                FooterStyle-HorizontalAlign="Right" />
                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Amount" HeaderText="Amount"
                                UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum"
                                FooterAggregateFormatString="{0:n2}" FooterStyle-HorizontalAlign="Right" />
                        </Columns>
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
