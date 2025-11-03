<%@ Page AutoEventWireup="true" CodeBehind="CashEntryDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.CashManagement.CashEntryV2.CashEntryDetail"
    Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">
            function openWinPickList() {
                var cboJO = $find("<%= cboNormalBalance.ClientID %>");
                if (cboJO.get_value() == '') {
                    alert('Transaction Type is required.');
                    return;
                }

                var oWnd = $find("<%= winCharges.ClientID %>");
                oWnd.setUrl('../CashEntry/VariableCashTemplateDialog.aspx?id=0&nb=' + cboJO.get_value());
                oWnd.set_title('Variable Cash Template');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }
            
            function openWinVariableCashEntry() {
                var cboJO = $find("<%= cboNormalBalance.ClientID %>");
                if (cboJO.get_value() == '') {
                    alert('Transaction Type is required.');
                    return;
                }

                var oWnd = $find("<%= winCharges.ClientID %>");
                oWnd.setUrl('CashEntryVariableDialog.aspx?id=0&nb=' + cboJO.get_value());
                oWnd.set_title('Variable Cash Entry');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }


            function openWinCrossRefference() {
                var cboBank = $find("<%= txtBankAccount.ClientID %>");
                var bankid = cboBank.get_value();
                if (bankid == '') {
                    alert('Bank Account is required.');
                    return;
                }

                var oWnd = $find("<%= winCharges.ClientID %>");
                oWnd.setUrl('CashEntryCrossRefference.aspx?bankid=' + bankid);
                oWnd.set_title('Cross Reference');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinPaymentReceive() {
                var cboBank = $find("<%= txtBankAccount.ClientID %>");
                var bankid = cboBank.get_value();
                if (bankid == '') {
                    alert('Bank Account is required.');
                    return;
                }

                var oWnd = $find("<%= winCharges.ClientID %>");
                oWnd.setUrl('CashEntryRefferenceToPaymentReceive.aspx');
                oWnd.set_title('Payment Receive Reference');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinPaymentAR() {
                var cboBank = $find("<%= txtBankAccount.ClientID %>");
                var bankid = cboBank.get_value();
                if (bankid == '') {
                    alert('Bank Account is required.');
                    return;
                }

                var oWnd = $find("<%= winCharges.ClientID %>");
                oWnd.setUrl('CashEntryRefferenceToARPayment.aspx');
                oWnd.set_title('Payment AR');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinPaymentAP() {
                var cboBank = $find("<%= txtBankAccount.ClientID %>");
                var bankid = cboBank.get_value();
                if (bankid == '') {
                    alert('Bank Account is required.');
                    return;
                }

                var oWnd = $find("<%= winCharges.ClientID %>");
                oWnd.setUrl('CashEntryRefferenceToAPPayment.aspx');
                oWnd.set_title('Payment AP');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinPurchaseOrder() {
                var cboBank = $find("<%= txtBankAccount.ClientID %>");
                var bankid = cboBank.get_value();
                if (bankid == '') {
                    alert('Bank Account is required.');
                    return;
                }

                var oWnd = $find("<%= winCharges.ClientID %>");
                oWnd.setUrl('CashEntryRefferenceToPurchaseOrder.aspx');
                oWnd.set_title('Purchase Order');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinPayroll() {
                var cboBank = $find("<%= txtBankAccount.ClientID %>");
                var bankid = cboBank.get_value();
                if (bankid == '') {
                    alert('Bank Account is required.');
                    return;
                }

                var oWnd = $find("<%= winCharges.ClientID %>");
                oWnd.setUrl('CashEntryRefferenceToPayroll.aspx');
                oWnd.set_title('Payroll');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.init == "rebind") {
                    __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", "rebind");
                } else if (oWnd.argument) {
                    __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", "rebind|" + oWnd.argument.init);
                }
            }

            function openWinRefference() {
                var type = $find("<%= cboReferenceType.ClientID %>").get_value();
                if (type == '') {
                    alert('Reference Type is required.');
                    return;
                }

                switch (type) {
                    case "01":
                        openWinCrossRefference();
                        break;
                    case "02":
                        openWinPaymentReceive();
                        break;
                    case "03":
                        openWinPaymentAR();
                        break;
                    case "04":
                        openWinPaymentAP();
                        break;
                    case "05":
                        var cboBank = $find("<%= txtBankAccount.ClientID %>");
                        var bankid = cboBank.get_value();
                        if (bankid == '') {
                            alert('Bank Account is required.');
                            return;
                        }

                        var oWnd = $find("<%= winCharges.ClientID %>");
                        oWnd.setUrl('CashEntryRefferenceToPOReturn.aspx?type=3');
                        oWnd.set_title('Purchase Order');
                        oWnd.show();
                        //oWnd.maximize();
                        oWnd.add_pageLoad(onClientPageLoad);
                        break;
                    case "06":
                        var cboBank = $find("<%= txtBankAccount.ClientID %>");
                        var bankid = cboBank.get_value();
                        if (bankid == '') {
                            alert('Bank Account is required.');
                            return;
                        }

                        var oWnd = $find("<%= winCharges.ClientID %>");
                        oWnd.setUrl('CashEntryRefferenceToPOReturn.aspx?type=1');
                        oWnd.set_title('Purchase Order Return (Cash)');
                        oWnd.show();
                        //oWnd.maximize();
                        oWnd.add_pageLoad(onClientPageLoad);
                        break;
                    case "07":
                        var cboBank = $find("<%= txtBankAccount.ClientID %>");
                        var bankid = cboBank.get_value();
                        if (bankid == '') {
                            alert('Bank Account is required.');
                            return;
                        }

                        var oWnd = $find("<%= winCharges.ClientID %>");
                        oWnd.setUrl('CashEntryRefferenceToPOReturn.aspx?type=2');
                        oWnd.set_title('Purchase Order Receive');
                        oWnd.show();
                        //oWnd.maximize();
                        oWnd.add_pageLoad(onClientPageLoad);
                        break;
                    case "08":
                        var cboBank = $find("<%= txtBankAccount.ClientID %>");
                        var bankid = cboBank.get_value();
                        if (bankid == '') {
                            alert('Bank Account is required.');
                            return;
                        }

                        var oWnd = $find("<%= winCharges.ClientID %>");
                        oWnd.setUrl('CashEntryRefferenceToPaymentReceiveReturn.aspx');
                        oWnd.set_title('Payment Return (Patient)');
                        oWnd.show();
                        //oWnd.maximize();
                        oWnd.add_pageLoad(onClientPageLoad);
                        break;
                    case "09":
                        openWinPayroll();
                        break;
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="600px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="onClientClose"
        ID="winCharges">
    </telerik:RadWindow>
    <asp:Label ID="lblJournalId" runat="server" Visible="false"></asp:Label>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">Transaction Date
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="105px">
                                <DateInput ID="DateInput1" runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                                <Calendar ID="Calendar1" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                    ViewSelectorText="x">
                                </Calendar>
                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                        </td>
                        <td style="width: 20px;">
                            <asp:RequiredFieldValidator ID="rfvTransactionDate" runat="server" ErrorMessage="Transaction Date required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Bank Account
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="txtBankAccount" TabIndex="0" runat="server" Height="190px"
                                Width="90%" AutoPostBack="true" HighlightTemplatedItems="true" DataTextField="AccountName"
                                DataValueField="BankId" NoWrap="true" OnSelectedIndexChanged="txtBankAccount_SelectedIndexChanged">
                                <ItemTemplate>
                                    <div>
                                        <b>
                                            <%# Eval("BankName")%></b><%# Eval("NoRek", " - {0}") %>
                                    </div>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                            <asp:Label ID="lblBankId" runat="server"></asp:Label>
                        </td>
                        <td style="width: 20px;">
                            <asp:RequiredFieldValidator ControlToValidate="txtBankAccount" ErrorMessage="Bank Account required."
                                ID="rfvBankAccount" runat="server" SetFocusOnError="True" ValidationGroup="entry"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Module Name
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboModuleName" runat="server" Width="25%" AutoPostBack="true"
                                OnSelectedIndexChanged="cboModuleName_SelectedIndexChanged">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="GL" Value="GL" />
                                    <telerik:RadComboBoxItem runat="server" Text="A/R" Value="AR" />
                                    <telerik:RadComboBoxItem runat="server" Text="A/P" Value="AP" />
                                </Items>
                            </telerik:RadComboBox>
                            <telerik:RadComboBox ID="cboTransactionType" runat="server" Width="64%" AutoPostBack="false" />
                        </td>
                        <td style="width: 20px;"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Transaction Type
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboNormalBalance" runat="server" Width="90%" AutoPostBack="true"
                                OnSelectedIndexChanged="cboNormalBalance_SelectedIndexChanged">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="Cash/Bank In" Value="D" />
                                    <telerik:RadComboBoxItem runat="server" Text="Cash/Bank Out" Value="K" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td style="width: 20px;">
                            <asp:RequiredFieldValidator ID="rfvNormalBalance" runat="server" ErrorMessage="Transaction Type required."
                                ValidationGroup="entry" ControlToValidate="cboNormalBalance" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">Mata Uang
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboCurrency" runat="server" Width="25%" AutoPostBack="false" />
                            <telerik:RadNumericTextBox ID="txtCurrencyRate" runat="server" Value="0" Width="63%">
                                <EnabledStyle HorizontalAlign="Right" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td style="width: 20px;"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Cheque / Giro Number
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtChequeNumber" runat="server" Width="90%" MaxLength="50" />
                        </td>
                        <td style="width: 20px;"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Due Date
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtDueDate" runat="server" Width="100px" />
                        </td>
                        <td style="width: 20px;"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Document Number
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtDocumentNumber" runat="server" Width="90%" MaxLength="50" />
                        </td>
                        <td style="width: 20px;"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Reference Type
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboReferenceType" runat="server" Width="90%" />
                        </td>
                        <td style="width: 20px;">
                            <asp:ImageButton ID="btnLoadRef" runat="server" OnClientClick="javascript:openWinRefference();return false;"
                                ImageUrl="~/Images/Toolbar/print_preview16.png" ToolTip="Open Reference" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Reference
                        </td>
                        <td class="entry">
                            <table width="100%">
                                <tr>
                                    <td valign="top">
                                        <asp:Label ID="lblRef1" runat="server"></asp:Label><asp:HiddenField ID="hfRef1" runat="server" />
                                        <asp:HiddenField ID="hfSequenceNo" runat="server" />
                                    </td>
                                    <td valign="top">
                                        <asp:Label ID="lblRef2" runat="server"></asp:Label><asp:HiddenField ID="hfRef2" runat="server" />
                                        <asp:HiddenField ID="hfDetailIdRef" runat="server" />
                                    </td>
                                    <td valign="top">
                                        <asp:Label ID="lblRef3" runat="server"></asp:Label><asp:HiddenField ID="hfRef3" runat="server" />
                                        <asp:HiddenField ID="hfIdent" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px;">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnCrossRefference" runat="server" OnClientClick="javascript:openWinCrossRefference();return false;"
                                            ImageUrl="~/Images/Toolbar/link16.png" ToolTip="Cross Refference Cash Entry" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnRefferencePaymentReceive" runat="server" OnClientClick="javascript:openWinPaymentReceive();return false;"
                                            ImageUrl="~/Images/Toolbar/cards_16x16.png" ToolTip="Refference To Patient Receive" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnRefferencePaymentAR" runat="server" OnClientClick="javascript:openWinPaymentAR();return false;"
                                            ImageUrl="~/Images/Toolbar/AR_16x16.png" ToolTip="Refference To AR Payment" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnRefferencePaymentAP" runat="server" OnClientClick="javascript:openWinPaymentAP();return false;"
                                            ImageUrl="~/Images/Toolbar/AP_16x16.png" ToolTip="Refference To AP Payment" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnDownPayment" runat="server" Visible="false" OnClientClick="javascript:openWinPurchaseOrder();return false;"
                                            ImageUrl="~/Images/Toolbar/downpayment16.png" ToolTip="Refference To Purchase Order (Down Payment)" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">Journal Number:
                        </td>
                        <td class="entry">
                            <asp:Label ID="lblJournalNumber" runat="server"></asp:Label>
                        </td>
                        <td style="width: 20px;"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">Payment Type
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPaymentType" runat="server" Width="300px" AutoPostBack="false" />
                        </td>
                        <td style="width: 20px;"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Payment Method
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPaymentMethod" runat="server" Width="300px" AutoPostBack="false" />
                        </td>
                        <td style="width: 20px;"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Description
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtDescription" runat="server" Width="300px" MaxLength="250"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ErrorMessage="Description required."
                                ValidationGroup="entry" ControlToValidate="txtDescription" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr id="trFromTo" runat="server" visible="false">
                        <td class="label">
                            <asp:Label ID="lblFromTo" runat="server">Received From / Paid To</asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtFromTo" runat="server" Width="300px" MaxLength="255" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr id="trBudgettingCode" runat="server">
                        <td class="label">Budgeting Code
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBudgeting" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblApprove" runat="server" Text="Status"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsCleared" Text="Cleared" runat="server" />&nbsp;
                            <asp:CheckBox ID="chkIsApproved" Text="Approved" runat="server" Enabled="false" />&nbsp;
                            <asp:CheckBox ID="chkIsVoid" Text="Void" runat="server" Enabled="false" />
                        </td>
                        <td style="width: 20px;"></td>
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
    <telerik:RadGrid ID="grdVoucherEntryItem" runat="server" AllowPaging="true" AllowCustomPaging="true"
        PageSize="18" ShowFooter="True" OnNeedDataSource="grdVoucherEntryItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="Horizontal" OnUpdateCommand="grdVoucherEntryItem_UpdateCommand"
        OnDeleteCommand="grdVoucherEntryItem_DeleteCommand" OnInsertCommand="grdVoucherEntryItem_InsertCommand">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView DataKeyNames="DetailId" GroupLoadMode="Client" CommandItemDisplay="None"
            ShowGroupFooter="True">
            <CommandItemTemplate>
                &nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# !grdVoucherEntryItem.MasterTableView.IsItemInserted %>'>
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/insert16.png" />
                    &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new record"></asp:Label>
                </asp:LinkButton>
                &nbsp;&nbsp;
                <asp:LinkButton ID="lbPickList" runat="server" Visible="false"
                    OnClientClick="javascript:openWinPickList();return false;">
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/views16.png" />
                    &nbsp;<asp:Label runat="server" ID="lblPicList" Text="Variable cash template"></asp:Label>
                </asp:LinkButton>
                &nbsp;&nbsp;
                <asp:LinkButton ID="LinkButton1" runat="server" Visible="true"
                    OnClientClick="javascript:openWinVariableCashEntry();return false;">
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/views16.png" />
                    &nbsp;<asp:Label runat="server" ID="Label1" Text="Variable cash entry"></asp:Label>
                </asp:LinkButton>
            </CommandItemTemplate>
            <CommandItemStyle Height="29px" />
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="25px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Width="80px" DataField="ListId"
                    HeaderText="Cash List ID" UniqueName="ListId" SortExpression="ListId" />
                <telerik:GridBoundColumn ItemStyle-Wrap="false" HeaderStyle-Width="110px" DataField="ChartOfAccountCode"
                    HeaderText="Code" UniqueName="ChartOfAccountCode" SortExpression="ChartOfAccountCode" />
                <telerik:GridBoundColumn ItemStyle-Wrap="false" HeaderStyle-Width="350px" DataField="ChartOfAccountName"
                    HeaderText="Account Name" UniqueName="ChartOfAccountName" />
                <telerik:GridBoundColumn ItemStyle-Wrap="true" HeaderStyle-Width="200px" DataField="SubLedgerName"
                    HeaderText="Subledger" UniqueName="SubLedgerName" Aggregate="Count" FooterAggregateFormatString="Total :"
                    FooterStyle-HorizontalAlign="Right" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="Amount" HeaderText="Amount"
                    UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" FooterStyle-HorizontalAlign="Right"
                    Aggregate="sum" />
                <telerik:GridBoundColumn DataField="Description" HeaderText="Description" UniqueName="Description">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn HeaderText="Ref" DataField="IsParentRefference" UniqueName="IsParentRefference">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="25px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="CashEntryItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="EditCommandColumn1">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="True">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
