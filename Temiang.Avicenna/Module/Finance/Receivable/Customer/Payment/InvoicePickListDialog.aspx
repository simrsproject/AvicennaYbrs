<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="InvoicePickListDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Receivable.Customer.InvoicePickListDialog" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
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
            <telerik:AjaxSetting AjaxControlID="btnProcessGlobalDiscount">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdInvoicesDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%">
            <tr>
                <td class="label">
                    <asp:Label ID="lblBankCost" runat="server" Text="Bank Cost"></asp:Label>
                </td>
                <td class="entry">
                    <telerik:RadNumericTextBox runat="server" ID="txtBankCost" MinValue="0" Width="153px"
                        Value="0" />
                </td>
                <td style="text-align: left">
                    <asp:ImageButton ID="btnProcessGlobalDiscount" runat="server" ImageUrl="~/Images/Toolbar/refresh16.png"
                                    OnClick="btnProcessGlobalDiscount_Click" ToolTip="Process Bank Cost" />
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
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
                    GridLines="Both" OnItemDataBound="grdInvoicesDetail_ItemDataBound" 
                    ShowFooter="True">
                    <MasterTableView DataKeyNames="InvoiceNo, TransactionNo, InvoiceReferenceNo" ClientDataKeyNames="InvoiceNo, TransactionNo, InvoiceReferenceNo">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                        runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="detailChkbox" runat="server" Checked='<%#  DataBinder.Eval(Container.DataItem, "IsChecked") %>'/>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="InvoiceNo" UniqueName="InvoiceNo" Visible="false" />
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="TransactionNo" HeaderText="Sales No"
                                UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn DataField="InvoiceReferenceNo" UniqueName="InvoiceReferenceNo"
                                Visible="false" />
                            <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate" HeaderText="Sales Date"
                                UniqueName="TransactionDate" SortExpression="TransactionDate" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="BalanceAmount" HeaderText="Amount"
                                UniqueName="BalanceAmount" SortExpression="BalanceAmount" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            <telerik:GridTemplateColumn HeaderStyle-Width="150px" HeaderText="Payment Amount"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" UniqueName="PaymentAmount"
                                FooterStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txtPaymentAmount" runat="server" Width="135px" DbValue='<%#Eval("PaymentAmount")%>'>
                                        <ClientEvents OnBlur="Blur" OnFocus="Focus" />
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <telerik:RadNumericTextBox ID="txtSumPaymentAmount" runat="server" Width="135px">
                                        <ClientEvents OnLoad="Load" />
                                    </telerik:RadNumericTextBox>
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Discount" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txtOtherAmount" runat="server" Width="85px" DbValue='<%#Eval("OtherAmount")%>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="150px" HeaderText="Bank Cost" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txtBankCost" runat="server" Width="135px" DbValue='<%#Eval("BankCost")%>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn />
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
