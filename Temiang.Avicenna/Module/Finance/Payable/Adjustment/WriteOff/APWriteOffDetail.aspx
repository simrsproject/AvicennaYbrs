<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="APWriteOffDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Payable.APWriteOffDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">
            function showPendingInvoices() {
                var oWnd = $find("<%= winInvoices.ClientID %>");
                var supp = $find("<%= cboSupplierID.ClientID %>");

                oWnd.setUrl('ApWriteOffPickListDialog.aspx?supp=' + supp.get_value());
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.id != null)
                    __doPostBack("<%= grdItem.UniqueID %>", oWnd.argument.id);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="600px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="onClientClose"
        ID="winInvoices">
    </telerik:RadWindow>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="vertical-align: top; width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblInvoicePaymentNo" runat="server" Text="A/P Write Off No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInvoicePaymentNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSupplierID" runat="server" Text="Supplier"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSupplierID" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboSupplierID_ItemDataBound"
                                OnItemsRequested="cboSupplierID_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SupplierName")%>
                                        &nbsp;(<%# DataBinder.Eval(Container.DataItem, "SupplierID")%>) </b>
                                    <br />
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSupplierID" runat="server" ErrorMessage="Supplier required."
                                ValidationGroup="entry" ControlToValidate="cboSupplierID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblInvoiceNo" runat="server" Text="Invoice No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInvoiceNo" runat="server" Width="300px" AutoPostBack="true"
                                ClientEvents-OnButtonClick="showPendingInvoices" OnTextChanged="txtInvoiceNo_TextChanged" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvInvoiceNo" runat="server" ErrorMessage="Invoice No required."
                                ValidationGroup="entry" ControlToValidate="txtInvoiceNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblInvoiceDate" runat="server" Text="Invoice Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtInvoiceDate" runat="server" Width="100px" DateInput-ReadOnly="True"
                                DatePopupButton-Enabled="False" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPaymentDate" runat="server" Text="Write Off Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtPaymentDate" runat="server" Width="100px" Enabled="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPaymentDate" runat="server" ErrorMessage="Payment Date required."
                                ValidationGroup="entry" ControlToValidate="txtPaymentDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSRInvoicePayment" runat="server" Text="Invoice Payment"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRInvoicePayment" Width="300px" AutoPostBack="True"
                                OnSelectedIndexChanged="cboSRInvoicePayment_OnSelectedIndexChanged" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <asp:Panel runat="server" ID="pnlBank" Visible="False">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBankID" runat="server" Text="Bank"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboBankID" Width="300px" />
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvBankID" runat="server" ErrorMessage="Bank required."
                                    ValidationGroup="entry" ControlToValidate="cboBankID" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBankAccountNo" runat="server" Text="Bank Account No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtBankAccountNo" runat="server" Width="300px" MaxLength="100" />
                            </td>
                            <td width="20px">
                            </td>
                            <td>
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRARWriteOffType" runat="server" Text="Reason " />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRARWriteOffType" runat="server" Width="300px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRARWriteOffType" runat="server" ErrorMessage="Reason required."
                                ValidationGroup="entry" ControlToValidate="cboSRARWriteOffType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" TextMode="MultiLine" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdItem_DeleteCommand">
        <MasterTableView DataKeyNames="InvoiceNo, TransactionNo, InvoiceReferenceNo">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Transaction No"
                    UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                    HeaderText="Transaction Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="VerifyAmount" HeaderText="Amount"
                    UniqueName="VerifyAmount" SortExpression="VerifyAmount" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PaymentAmount" HeaderText="Payment Amount"
                    UniqueName="PaymentAmount" SortExpression="PaymentAmount" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="CurrencyID" HeaderText="Currency Type"
                    UniqueName="CurrencyID" SortExpression="CurrencyID" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CurrencyRate" HeaderText="Currency Rate"
                    UniqueName="CurrencyRate" SortExpression="CurrencyRate" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="True">
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
