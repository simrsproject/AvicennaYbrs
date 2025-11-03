<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="ApWriteOffPickListDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Payable.ApWriteOffPickListDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script language="javascript" type="text/javascript">
            var sumInput = null;
            var tempValue = 0.0;

            function Load(sender, args) {
                sumInput = sender;
            }

            function Blur(sender, args) {
                sumInput.set_value(tempValue + sender.get_value());
            }

            function Focus(sender, args) {
                tempValue = sumInput.get_value() - sender.get_value();
            }

            function RowSelected(sender, args) {
                __doPostBack("<%= grdInvoicesDetail.UniqueID %>", args.getDataKeyValue("InvoiceNo"));
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdInvoicesDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdInvoicesDetail">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdInvoicesDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="15%" valign="top">
                <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="False" GridLines="None"
                    OnNeedDataSource="grdList_NeedDataSource">
                    <MasterTableView DataKeyNames="InvoiceNo" ClientDataKeyNames="InvoiceNo">
                        <Columns>
                            <telerik:GridBoundColumn DataField="InvoiceNo" UniqueName="InvoiceNo" SortExpression="InvoiceNo"
                                HeaderText="Invoice No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="false">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnRowSelected="RowSelected" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
            <td>
                <asp:Panel ID="Panel1" runat="server" Width="3px" />
            </td>
            <td width="85%" valign="top">
                <telerik:RadGrid ID="grdInvoicesDetail" runat="server" AutoGenerateColumns="False"
                    OnItemDataBound="grdInvoicesDetail_ItemDataBound" GridLines="Both" ShowFooter="True">
                    <MasterTableView DataKeyNames="InvoiceNo, TransactionNo, InvoiceReferenceNo" ClientDataKeyNames="InvoiceNo, TransactionNo, InvoiceReferenceNo">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="30px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                        runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="detailChkbox" runat="server" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="InvoiceNo" UniqueName="InvoiceNo" Visible="false" />
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Transaction No"
                                UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn DataField="InvoiceReferenceNo" UniqueName="InvoiceReferenceNo"
                                Visible="false" />
                            <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                                HeaderText="Transaction Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="BalanceAmount" HeaderText="Amount"
                                UniqueName="BalanceAmount" SortExpression="BalanceAmount" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Payment Amount"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" UniqueName="PaymentAmount"
                                FooterStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txtPaymentAmount" runat="server" Width="85px" DbValue='<%#Eval("BalanceAmount")%>'>
                                        <ClientEvents OnBlur="Blur" OnFocus="Focus" />
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <telerik:RadNumericTextBox ID="txtSumPaymentAmount" runat="server" Width="85px">
                                        <ClientEvents OnLoad="Load" />
                                    </telerik:RadNumericTextBox>
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="CurrencyID" HeaderText="Currency Type"
                                UniqueName="CurrencyID" SortExpression="CurrencyID" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Currancy Rate"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txtCurrencyRate" runat="server" Width="85px" DbValue='<%#Eval("CurrencyRate")%>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="false">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
