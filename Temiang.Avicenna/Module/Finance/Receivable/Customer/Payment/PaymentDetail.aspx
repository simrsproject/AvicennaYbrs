<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="PaymentDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Receivable.Customer.PaymentDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">
            function showPendingInvoices() {
                var oWnd = $find("<%= winInvoices.ClientID %>");
                var grr = $find("<%= cboCustomerID.ClientID %>");

                oWnd.setUrl('InvoicePickListDialog.aspx?grr=' + grr.get_value());
                oWnd.show();
                oWnd.maximize();
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
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCustomerID" runat="server" Text="Customer"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboCustomerID" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboCustomerID_ItemDataBound"
                                OnItemsRequested="cboCustomerID_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "CustomerName")%>
                                        &nbsp;(<%# DataBinder.Eval(Container.DataItem, "CustomerID")%>) </b>
                                    <br />
                                    <%# DataBinder.Eval(Container.DataItem, "Address") %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvCustomerID" runat="server" ErrorMessage="Customer required."
                                ValidationGroup="entry" ControlToValidate="cboCustomerID" SetFocusOnError="True"
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
                        <td>
                        </td>
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
            <td style="vertical-align: top; width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRPaymentType" runat="server" Text="Payment Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRPaymentType" runat="server" Width="300px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboSRPaymentType_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRPaymentType" runat="server" ErrorMessage="Transaction Type required."
                                ValidationGroup="TransPaymentItem" ControlToValidate="cboSRPaymentType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr runat="server" id="pnlPaymentMethod">
                        <td class="label">
                            <asp:Label ID="lblSRPaymentMethod" runat="server" Text="Payment Method"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRPaymentMethod" runat="server" Width="300px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboSRPaymentMethod_SelectedIndexChanged" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRPaymentMethod" runat="server" ErrorMessage="Payment Method required."
                                ControlToValidate="cboSRPaymentMethod" SetFocusOnError="True" ValidationGroup="TransPaymentItem"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr runat="server" id="pnlDiscountReason">
                        <td class="label">
                            <asp:Label ID="lblSRDiscountReason" runat="server" Text="Discount Reason"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRDiscountReason" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr runat="server" id="pnlCardProvider">
                        <td class="label">
                            <asp:Label ID="lblSRCardProvider" runat="server" Text="Card Provider"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRCardProvider" runat="server" Width="300px" AutoPostBack="True"
                                OnSelectedIndexChanged="cboSRCardProvider_SelectedIndexChanged" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <asp:Panel ID="pnlCardDetail" runat="server">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSRCardType" runat="server" Text="Card Type"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRCardType" runat="server" Width="300px" />
                            </td>
                            <td width="20px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblEDCMachineID" runat="server" Text="EDC Machine"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboEDCMachineID" runat="server" Width="300px" AutoPostBack="true"
                                    OnSelectedIndexChanged="cboEDCMachineID_SelectedIndexChanged" />
                            </td>
                            <td width="20px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblCardHolderName" runat="server" Text="Card Holder Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtCardHolderName" runat="server" Width="300px" MaxLength="100" />
                            </td>
                            <td width="20px">
                            </td>
                            <td>
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlBank">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBankID" runat="server" Text="Bank"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboBankID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" OnItemDataBound="cboBankID_ItemDataBound"
                                    OnItemsRequested="cboBankID_ItemsRequested">
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
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
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" 
        OnDeleteCommand="grdItem_DeleteCommand" ShowFooter="True">
        <MasterTableView DataKeyNames="InvoiceNo, TransactionNo, InvoiceReferenceNo">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="TransactionNo" HeaderText="Transaction No"
                    UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate" HeaderText="Date"
                    UniqueName="TransactionDate" SortExpression="TransactionDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCalculatedColumn HeaderStyle-Width="150px" HeaderText="Amount" UniqueName="TotalVerifyAmount"
                    DataType="System.Double" DataFields="VerifyAmount"
                    SortExpression="TotalVerifyAmount" Expression="{0}" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridCalculatedColumn HeaderStyle-Width="150px" HeaderText="Payment Amount" UniqueName="TotalPaymentAmount"
                    DataType="System.Double" DataFields="PaymentAmount"
                    SortExpression="TotalPaymentAmount" Expression="{0}" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Discount" UniqueName="TotalOtherAmount"
                    DataType="System.Double" DataFields="OtherAmount"
                    SortExpression="TotalOtherAmount" Expression="{0}" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" Visible="false" />
                <telerik:GridCalculatedColumn HeaderStyle-Width="150px" HeaderText="Bank Cost" UniqueName="TotalBankCost"
                    DataType="System.Double" DataFields="BankCost"
                    SortExpression="TotalBankCost" Expression="{0}" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" /> 
                <telerik:GridTemplateColumn />   
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
