<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="VoucherEntryDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.VoucherEntryDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">
            function validate() {
                var oWnd = $find("<%= winDetailTrans.ClientID %>");
                var jt = '<%= Request.QueryString["jt"] %>';

                switch (jt) {
                    case "02": //Income (Registrasi, Service Unit Entry & JO Realization)
                        oWnd.setUrl('CheckDetailTransaction/IncomeTypeDetail.aspx?ivd=' + '<%= Request.QueryString["ivd"] %>');
                        break;
                    case "03": //Prescription
                        oWnd.setUrl('CheckDetailTransaction/IncomeTypeDetail.aspx?ivd=' + '<%= Request.QueryString["ivd"] %>');
                        break;
                    case "04": //Prescription return
                        oWnd.setUrl('CheckDetailTransaction/IncomeTypeDetail.aspx?ivd=' + '<%= Request.QueryString["ivd"] %>');
                        break;
                    case "05": //Spectacle Prescription
                        break;
                    case "07": //Payment Return
                        oWnd.setUrl('CheckDetailTransaction/PaymentTypeDetail.aspx?ivd=' + '<%= Request.QueryString["ivd"] %>');
                        break;
                    case "08": //Down Payment Return
                        oWnd.setUrl('CheckDetailTransaction/PaymentTypeDetail.aspx?ivd=' + '<%= Request.QueryString["ivd"] %>');
                        break;
                    case "09": //Down Payment
                        oWnd.setUrl('CheckDetailTransaction/PaymentTypeDetail.aspx?ivd=' + '<%= Request.QueryString["ivd"] %>');
                        break;
                    case "13":
                    case "10": //Payment
                        oWnd.setUrl('CheckDetailTransaction/PaymentTypeDetail.aspx?ivd=' + '<%= Request.QueryString["ivd"] %>');
                        break;
                    case "11": //AR Payment
                        oWnd.setUrl('CheckDetailTransaction/ARPaymentTypeDetail.aspx?ivd=' + '<%= Request.QueryString["ivd"] %>');
                        break;
                    case "12": //Cash Transaction
                        break;
                    case "14": //AR Invoice

                        break;
                    case "15": //PO Received
                        oWnd.setUrl('CheckDetailTransaction/POReceivedTypeDetail.aspx?ivd=' + '<%= Request.QueryString["ivd"] %>');
                        break;
                    case "17": //Consignment Received
                        break;
                    case "19": //Consignment Invoicing
                        break;
                    case "20": //Distribution
                        oWnd.setUrl('CheckDetailTransaction/DistributionTypeDetail.aspx?ivd=' + '<%= Request.QueryString["ivd"] %>');
                        break;
                    case "22": //Inventory Issue
                        oWnd.setUrl('CheckDetailTransaction/InventoryIssueTypeDetail.aspx?ivd=' + '<%= Request.QueryString["ivd"] %>');
                        break;
                    case "25":
                    case "26": //AP Payment
                        oWnd.setUrl('CheckDetailTransaction/APPaymentTypeDetail.aspx?ivd=' + '<%= Request.QueryString["ivd"] %>');
                        break;
                    case "27": //Paramedic Fee Payment
                        oWnd.setUrl('CheckDetailTransaction/ParamedicFeePaymentTypeDetail.aspx?ivd=' + '<%= Request.QueryString["ivd"] %>');
                        break;
                    case "28": //Paramedic Fee Verification 
                    case "48": //Paramedic fee payable
                        oWnd.setUrl('CheckDetailTransaction/ParamedicFeeVerificationTypeDetail.aspx?ivd=' + '<%= Request.QueryString["ivd"] %>');
                        break;
                }

                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function recalculated() {
                var jt = '<%= Request.QueryString["jt"] %>';

                switch (jt) {
                    case "02": //Income (Registrasi, Service Unit Entry & JO Realization)
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '02');
                        break;
                    case "03": //Prescription
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '03');
                        break;
                    case "04": //Prescription return
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '04');
                        break;
                    case "05": //Spectacle Prescription
                        break;
                    case "07": //Payment Return
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '07');
                        break;
                    case "08": //Down Payment Return
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '08');
                        break;
                    case "09": //Down Payment
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '09');
                        break;
                    case "13":
                    case "10": //Payment
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '10');
                        break;
                    case "11": //AR Payment
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '11');
                        break;
                    case "12": //Cash Transaction
                        break;
                    case "14": //AR Invoice
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '14');
                        break;
                    case "15": //PO Received
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '15');
                        break;
                    case "17": //Consignment Received
                        break;
                    case "19": //Consignment Invoicing
                        break;
                    case "20": //Distribution
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '20');
                        break;
                    case "22": //Inventory Issue
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '22');
                        break;
                    case "25": //AP
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '25');
                        break;
                    case "26": //AP Payment
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '26');
                        break;
                    case "27": //Paramedic Fee Payment
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '27');
                        break;
                    case "28": //Paramedic Fee Verification
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '28');
                        break;
                    case "38": //Payroll
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '38');
                        break;
                    case "39": //THR
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '39');
                        break;
                    case "40": //Sales to Customer
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '40');
                        break;
                    case "41": //Sales to Customer Return
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '41');
                        break;
                    case "42": //Customer A/R
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '42');
                        break;
                    case "43": //Customer A/R Payment
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '43');
                        break;
                    case "44": //AssetAuction
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '44');
                        break;
                    case "45": //AssetDestruction
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '45');
                        break;
                    case "46": //AssetMovement
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '46');
                        break;
                    case "47": //Closing Visit DP
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", '47');
                        break;
                    default: //Make it Simple!!
                        __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", jt);
                        break;
                }
            }

            function onClientClose(oWnd, args) {
                var arg = oWnd.argument;
                if (arg) {
                    $find("<%= RadAjaxManager.GetCurrent(Page).ClientID %>").ajaxRequest("");
                }
            }
            
            function ShowDialogFullReference(RefNo){
                var oWnd = $find("<%= winFullReference.ClientID %>");
                oWnd.setUrl('FullReference/PORDetailJournalDialog.aspx?RefNo=' + RefNo);
                oWnd.show();
                //oWnd.maximize();      
                oWnd.add_pageLoad(onClientPageLoad);  
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindow runat="server" Animation="None" Width="850px" Height="600px" ShowContentDuringLoad="False"
        VisibleStatusbar="False" Title="Detail Transaction" OnClientClose="onClientClose"
        ID="winDetailTrans" Behaviors="Close">
    </telerik:RadWindow>
    <telerik:RadWindow runat="server" Animation="None" Width="850px" Height="600px" ShowContentDuringLoad="False"
        VisibleStatusbar="False" Title="Detail Journal Reference" OnClientClose="onClientClose"
        ID="winFullReference" Behaviors="Close">
    </telerik:RadWindow>
    <asp:Label ID="lblJournalId" runat="server" Visible="false"></asp:Label>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Journal Code
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="txtJournalCode" TabIndex="0" runat="server" Height="190px"
                                Width="290px" AutoPostBack="true" HighlightTemplatedItems="true" DataTextField="JournalCode"
                                DataValueField="JournalCodeId" NoWrap="true">
                                <ItemTemplate>
                                    <div>
                                        <b>
                                            <%# Eval("JournalCode")%></b><%# Eval("Description", " - {0}") %>
                                    </div>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                            <asp:Label ID="lblJournalCode" runat="server"></asp:Label>
                        </td>
                        <td style="width: 20px;">
                            <asp:RequiredFieldValidator ID="rfvJournalCode" runat="server" ErrorMessage="Journal Code required."
                                ValidationGroup="entry" ControlToValidate="txtJournalCode" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Transaction Number
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNumber" runat="server" Width="287px" MaxLength="10" />
                            <asp:Label ID="lblTransactionNumber" runat="server"></asp:Label>
                        </td>
                        <td style="width: 20px;">
                            <asp:RequiredFieldValidator ID="rfvTransactionNumber" runat="server" ErrorMessage="Transaction Number required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionNumber" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Transaction Date
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="105px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                                <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
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
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Reference Number
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td><telerik:RadTextBox ID="txtRefferenceNumber" runat="server" Width="287px" MaxLength="10"
                                        Enabled="false" /></td>
                                    <td>
                                        <div id="divRefferenceNumber" runat="server">
                                            <asp:Label ID="lblRefferenceNumber" runat="server"></asp:Label>
                                        </div>
                                    </td>
                                    <td>&nbsp;&nbsp;</td>
                                    <td><asp:ImageButton ID="btnDetailTransView" runat="server" ImageUrl="../../../../Images/Toolbar/print_preview16_h.png"
                                        CausesValidation="False" OnClientClick="validate(); return false;" ToolTip="Check Transaction"
                                        Visible="False" /></td>
                                    <td><asp:ImageButton ID="btnRecalculate" runat="server" ImageUrl="../../../../Images/Toolbar/process16_h.png"
                                        CausesValidation="False" OnClientClick="recalculated(); return false;" ToolTip="Reprocessing Automated Journal"
                                        Visible="false" /></td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px;">
                            <asp:Label ID="lblJournalType" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Description
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
                        <td>
                        </td>        
                    </tr>
                    <tr id="trBudgettingCode" runat="server">
                        <td class="label">
                            Budgeting Code
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBudgeting" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>        
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTotalAmountDebit" runat="server" Text="Total Debit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTotalAmountDebit" runat="server" ReadOnly="true"
                                Value="0" Width="150px">
                                <DisabledStyle HorizontalAlign="Right" Font-Bold="true" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td style="width: 20px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTotalAmountCredit" runat="server" Text="Total Credit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTotalAmountCredit" runat="server" ReadOnly="true"
                                Value="0" Width="150px">
                                <DisabledStyle HorizontalAlign="Right" Font-Bold="true" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td style="width: 20px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="LblSelisih" runat="server" Text="Difference"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtSelisih" runat="server" ReadOnly="true" Value="0"
                                Width="150px">
                                <DisabledStyle HorizontalAlign="Right" Font-Bold="true" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td style="width: 20px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblApprove" runat="server" Text="Status"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td><asp:CheckBox ID="chkIsApproved" Text="Approved" runat="server" Enabled="false" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:CheckBox ID="chkIsVoid" Text="Void" runat="server" Enabled="false" /></td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpagDetail">
        <Tabs>
            <telerik:RadTab runat="server" Text="Detail" Selected="True" PageViewID="pgJournalDetail" />
            <telerik:RadTab runat="server" Text="Reference" PageViewID="pgInfoDetail" />
            <telerik:RadTab runat="server" Text="Setting Chart Of Account" PageViewID="pgInfoSettingCoa"
                Visible="false" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpagDetail" runat="server" BorderStyle="Solid" SelectedIndex="0"
        BorderColor="Gray">
        <telerik:RadPageView ID="pgJournalDetail" runat="server">
            <telerik:RadGrid ID="grdVoucherEntryItem" runat="server" AllowPaging="true" AllowCustomPaging="true"
                PageSize="18" ShowFooter="True" OnNeedDataSource="grdVoucherEntryItem_NeedDataSource"
                AutoGenerateColumns="False" GridLines="Horizontal" OnUpdateCommand="grdVoucherEntryItem_UpdateCommand"
                OnDeleteCommand="grdVoucherEntryItem_DeleteCommand" OnInsertCommand="grdVoucherEntryItem_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView DataKeyNames="DetailId" ShowGroupFooter="True">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="25px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn ItemStyle-Wrap="false" HeaderStyle-Width="100px" DataField="ChartOfAccountCode"
                            HeaderText="Code" UniqueName="ChartOfAccountCode" SortExpression="ChartOfAccountCode" />
                        <telerik:GridBoundColumn ItemStyle-Wrap="true" HeaderStyle-Width="300px" DataField="ChartOfAccountName"
                            HeaderText="Account Name" UniqueName="ChartOfAccountName" />
                        <telerik:GridBoundColumn DataField="SubLedgerId" HeaderText="Subledger ID" UniqueName="SubLedgerId"
                            Visible="false" />

                        <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No" UniqueName="RegistrationNo"
                            Visible="false" />

                        <telerik:GridBoundColumn ItemStyle-Wrap="True" HeaderStyle-Width="150px" DataField="SubLedgerName"
                            HeaderText="Subledger" UniqueName="SubLedgerName" />
                        <telerik:GridBoundColumn ItemStyle-Wrap="true" HeaderStyle-Width="150px" DataField="DocumentNumber"
                            HeaderText="Reference#" UniqueName="DocumentNumber" FooterStyle-HorizontalAlign="Right"
                            Aggregate="Count" FooterAggregateFormatString="Total :" />
                        <telerik:GridBoundColumn DataField="Debit" HeaderText="Debit" UniqueName="Debit"
                            DataFormatString="{0:N2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                            FooterStyle-HorizontalAlign="Right">
                            <HeaderStyle HorizontalAlign="Center" Width="115px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Credit" HeaderText="Credit" UniqueName="Credit"
                            DataFormatString="{0:N2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                            FooterStyle-HorizontalAlign="Right">
                            <HeaderStyle HorizontalAlign="Center" Width="115px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Description" HeaderText="Description" UniqueName="Description"
                            ItemStyle-Wrap="True">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="25px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="VoucherEntryItemDetail.ascx" EditFormType="WebUserControl">
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
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView1" runat="server">
            <div>
                <iframe id="mainiFrame" name="mainiFrame" scrolling="auto" height="400px" width="100%"
                    runat="server" style="border-width: 0px" />
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView2" runat="server">
            <div>
                <iframe id="mainiFrame2" name="mainiFrame2" scrolling="auto" height="400px" width="100%"
                    runat="server" style="border-width: 0px" />
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>