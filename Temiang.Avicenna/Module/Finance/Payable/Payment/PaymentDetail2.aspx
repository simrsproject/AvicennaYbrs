<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="PaymentDetail2.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Payable.PaymentDetail2" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">
            function showPendingInvoices() {
                var oWnd = $find("<%= winInvoices.ClientID %>");
                var supp = $find("<%= cboSupplierID.ClientID %>");

                oWnd.setUrl('InvoicePickListDialog.aspx?supp=' + supp.get_value());
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
                            <asp:Label ID="lblInvoicePaymentNo" runat="server" Text="Invoice Payment No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInvoicePaymentNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
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
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPaymentDate" runat="server" Text="Payment Date"></asp:Label>
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
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" TextMode="MultiLine" runat="server" Width="300px" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRInvoicePayment" runat="server" Text="Invoice Payment"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRInvoicePayment" Width="300px" AutoPostBack="True"
                                OnSelectedIndexChanged="cboSRInvoicePayment_OnSelectedIndexChanged" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRInvoicePayment" runat="server" ErrorMessage="Invoice Payment required."
                                ValidationGroup="entry" ControlToValidate="cboSRInvoicePayment" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <asp:Panel runat="server" ID="pnlBank">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBankID" runat="server" Text="Bank"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboBankID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" OnItemDataBound="cboBankID_ItemDataBound" OnItemsRequested="cboBankID_ItemsRequested">
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                   
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvBankID" runat="server" ErrorMessage="Bank required."
                                    ValidationGroup="entry" ControlToValidate="cboBankID" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBankAccountNo" runat="server" Text="Supplier Bank Account No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboBankAccountNo" runat="server" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboBankAccountNo_ItemDataBound"
                                OnItemsRequested="cboBankAccountNo_ItemsRequested">
                                <FooterTemplate>
                                    Note : Show max 10 items
                               
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <asp:Panel runat="server" ID="pnlBKU">
                        <tr>
                            <td class="label">BKU Account
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboBkuAccount" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" OnItemDataBound="cboBkuAccount_ItemDataBound"
                                    OnItemsRequested="cboBkuAccount_ItemsRequested">
                                    <FooterTemplate>
                                        Note : Show max 20 items                                  
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                    </asp:Panel>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdItem_DeleteCommand"
        OnInsertCommand="grdItem_InsertCommand" ShowFooter="True">
        <MasterTableView CommandItemDisplay="None" DataKeyNames="InvoiceNo, TransactionNo, InvoiceReferenceNo">
            <CommandItemTemplate>
                &nbsp;
               
                <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# !grdItem.MasterTableView.IsItemInserted %>'>
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/insert16.png" />
                    &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new record"></asp:Label>
                </asp:LinkButton>&nbsp;&nbsp;
               
                <asp:LinkButton ID="lbPickList" runat="server" Visible='<%# !grdItem.MasterTableView.IsItemInserted %>'
                    OnClientClick="javascript:showPendingInvoices();return false;">
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/views16.png" />&nbsp;<asp:Label
                        runat="server" ID="lblPicList" Text="Pick from Supplier Invoice Outstanding"></asp:Label>
                </asp:LinkButton>
            </CommandItemTemplate>
            <CommandItemStyle Height="29px" />
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="InvoiceReferenceNo"
                    HeaderText="Inv. No" UniqueName="InvoiceReferenceNo" SortExpression="InvoiceReferenceNo"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="TransactionNo" HeaderText="Transaction No"
                    UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                    HeaderText="Transaction Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="180px" DataField="InvoiceSupplierNo" HeaderText="Inv. Supp. No"
                    UniqueName="InvoiceSupplierNo" SortExpression="InvoiceSupplierNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="VerifyAmount" HeaderText="Amount"
                    UniqueName="VerifyAmount" SortExpression="VerifyAmount" HeaderStyle-HorizontalAlign="Right"
                    FooterStyle-HorizontalAlign="Right" Aggregate="Sum"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="PaymentAmount" HeaderText="Payment Amount"
                    UniqueName="PaymentAmount" SortExpression="PaymentAmount" HeaderStyle-HorizontalAlign="Right"
                    FooterStyle-HorizontalAlign="Right" Aggregate="Sum"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="CurrencyID" HeaderText="Currency Type"
                    UniqueName="CurrencyID" SortExpression="CurrencyID" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="CurrencyRate" HeaderText="Currency Rate"
                    UniqueName="CurrencyRate" SortExpression="CurrencyRate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="PaymentDetailItem.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="PaymentDetailItemEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="True">
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
