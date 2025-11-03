<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="InvoicePickListDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Receivable.InvoicePickListDialog" %>

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

            function OpenWinInquiry() {
                var oWnd = $find("<%= winInquiry.ClientID %>");
                oWnd.setUrl("BankInquiryDialog.aspx");
                oWnd.show();
                oWnd.add_pageLoad(onClientPageLoad);

                return false;
            }

            function winInquiryClosed(oWnd, args) {
                //Jika apply di click
                var arg = oWnd.argument;
                if (arg != null) {
                    if (oWnd.argument.transId != '0') {
                        __doPostBack("<%= grdList.UniqueID %>", "loadInquiry|" + oWnd.argument.transId);
                    }
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdInvoicesDetail" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblSelectedCount" />
                    <telerik:AjaxUpdatedControl ControlID="lblSumPaymentAmount" />
                    <telerik:AjaxUpdatedControl ControlID="lblSumDiscount" />
                    <telerik:AjaxUpdatedControl ControlID="lblSumBankCost" />

                    <telerik:AjaxUpdatedControl ControlID="btnCancelRelatedBankInquiry" />
                    <telerik:AjaxUpdatedControl ControlID="hfRelatedBankInquiryID" />
                    <telerik:AjaxUpdatedControl ControlID="lblRelatedBankInquiryAmount" />
                    <telerik:AjaxUpdatedControl ControlID="lblRelatedBankInquiryDesc" />
                    <telerik:AjaxUpdatedControl ControlID="lblRelatedBankInquiryDiff" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdInvoicesDetail">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdInvoicesDetail" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblSelectedCount" />
                    <telerik:AjaxUpdatedControl ControlID="lblSumPaymentAmount" />
                    <telerik:AjaxUpdatedControl ControlID="lblSumDiscount" />
                    <telerik:AjaxUpdatedControl ControlID="lblSumBankCost" />
                    <telerik:AjaxUpdatedControl ControlID="lblRelatedBankInquiryDiff" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnProcessGlobalDiscount">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdInvoicesDetail" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblSelectedCount" />
                    <telerik:AjaxUpdatedControl ControlID="lblSumPaymentAmount" />
                    <telerik:AjaxUpdatedControl ControlID="lblSumDiscount" />
                    <telerik:AjaxUpdatedControl ControlID="lblSumBankCost" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCancelRelatedBankInquiry">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnCancelRelatedBankInquiry" />
                    <telerik:AjaxUpdatedControl ControlID="hfRelatedBankInquiryID" />
                    <telerik:AjaxUpdatedControl ControlID="lblRelatedBankInquiryAmount" />
                    <telerik:AjaxUpdatedControl ControlID="lblRelatedBankInquiryDesc" />
                    <telerik:AjaxUpdatedControl ControlID="lblRelatedBankInquiryDiff" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="600px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="winInquiryClosed"
        ID="winInquiry">
    </telerik:RadWindow>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%">
            <tr>
                <td style="width:50%; vertical-align:top;">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblGlobalDiscount" runat="server" Text="Global Discount"></asp:Label>
                            </td>
                            <td class="entry">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 53px">
                                            <telerik:RadComboBox runat="server" ID="cboDiscountTypeID" Width="50px">
                                                <Items>
                                                    <telerik:RadComboBoxItem runat="server" Value="0" Text="%" Selected="True" />
                                                    <telerik:RadComboBoxItem runat="server" Value="1" Text="Rp" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadNumericTextBox runat="server" ID="txtDiscountValue" Width="100px"
                                                            Value="0" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkIsProRata" runat="server" Text="Share Equally" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnProcessGlobalDiscount" runat="server" ImageUrl="~/Images/Toolbar/refresh16.png"
                                                OnClick="btnProcessGlobalDiscount_Click" ToolTip="Process Discount & Bank Cost" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="text-align: left">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDiscountReason" runat="server" Text="Discount Reason"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboDiscountReason" runat="server" Width="153px" EnableLoadOnDemand="true"
                                    MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboSRDiscountReason_ItemDataBound"
                                    OnItemsRequested="cboSRDiscountReason_ItemsRequested">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBankCost" runat="server" Text="Bank Cost"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox runat="server" ID="txtBankCost" MinValue="0" Width="153px"
                                    Value="0" />
                            </td>
                            <td style="text-align: left">
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width:50%; vertical-align:top;">
                    <table cellpadding="0"  cellspacing="0" class="info success" style="font-size:medium; width:100%;">
                        <tr>
                            <td style="width:50%; vertical-align:top;">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width:150px">Selected Rows</td>
                                        <td>:</td>
                                        <td><asp:Label ID="lblSelectedCount" runat="server"></asp:Label></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>Payment Amount</td>
                                        <td>:</td>
                                        <td><asp:Label ID="lblSumPaymentAmount" runat="server"></asp:Label></td>
                                        <td>&nbsp;</td>
                                        
                                    </tr>
                                    <tr>
                                        <td>Discount</td>
                                        <td>:</td>
                                        <td><asp:Label ID="lblSumDiscount" runat="server"></asp:Label></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>Bank Cost</td>
                                        <td>:</td>
                                        <td><asp:Label ID="lblSumBankCost" runat="server"></asp:Label></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width:50%; vertical-align:top;">
                                <fieldset>
                                    <legend>
                                        <asp:Table CellPadding="0" CellSpacing="0" runat="server" ID="gakpenting">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                     <asp:LinkButton ID="lbtnRelatedBankInquiry" runat="server" OnClientClick="return OpenWinInquiry();">Related Bank Inquiry</asp:LinkButton>
                                                </asp:TableCell>
                                                <asp:TableCell>&nbsp;&nbsp;</asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:ImageButton ID="btnCancelRelatedBankInquiry" runat="server" ImageUrl="~/Images/Toolbar/close16.png"
                                                OnClick="btnCancelRelatedBankInquiry_Click" ToolTip="Cancel Related Bank Inquiry" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </legend>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td colspan="4">
                                                <asp:Label ID="lblRelatedBankInquiryDesc" runat="server" Font-Size="Smaller"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Amount</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblRelatedBankInquiryAmount" runat="server"></asp:Label>
                                                <asp:HiddenField ID="hfRelatedBankInquiryID" runat="server" />
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Different</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblRelatedBankInquiryDiff" runat="server"></asp:Label>
                                            </td>
                                            <td></td>
                                        </tr>
                                </table>
                                </fieldset>
                                
                            </td>
                        </tr>
                    </table>
                    
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="15%" valign="top">
                <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="False" GridLines="None"
                    OnNeedDataSource="grdList_NeedDataSource" OnItemDataBound="grdList_ItemDataBound"
                    OnDataBound="grdList_DataBound">
                    <MasterTableView DataKeyNames="InvoiceNo" ClientDataKeyNames="InvoiceNo">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="headerChkbox" OnCheckedChanged="grdList_HeaderChkBoxCheckedChanged"
                                        AutoPostBack="True" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="itemChkbox" runat="server" AutoPostBack="True" OnCheckedChanged="grdList_ItemChkBoxCheckedChanged" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="InvoiceNo" UniqueName="InvoiceNo" SortExpression="InvoiceNo"
                                HeaderText="Invoice No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="false">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
            <td>
                <asp:Panel ID="Panel1" runat="server" Width="3px" />
            </td>
            <td width="85%" valign="top">
                <telerik:RadGrid ID="grdInvoicesDetail" runat="server" AutoGenerateColumns="False"
                    GridLines="Both" OnItemDataBound="grdInvoicesDetail_ItemDataBound"
                    OnPageSizeChanged="grdInvoicesDetail_PageSizeChanged"
                    OnPageIndexChanged="grdInvoicesDetail_PageIndexChanged"
                    OnSortCommand="grdInvoicesDetail_SortCommand"
                    ShowFooter="True" AllowPaging="true"  PagerStyle-Mode="NextPrevNumericAndAdvanced" AllowSorting="true"
                    OnItemCreated="grdInvoicesDetail_ItemCreated">
                    <MasterTableView DataKeyNames="InvoiceReferenceNo, PaymentNo" ClientDataKeyNames="InvoiceReferenceNo, PaymentNo">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="30px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" 
                                        AutoPostBack="True" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="detailChkbox" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsChecked") %>' 
                                        AutoPostBack="true" OnCheckedChanged="ToggleSelectedState"/>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="InvoiceNo" UniqueName="InvoiceNo" Visible="false" />
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PaymentNo" HeaderText="Payment No"
                                UniqueName="PaymentNo" SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn DataField="InvoiceReferenceNo" UniqueName="InvoiceReferenceNo"
                                Visible="false" />
                            <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="PaymentDate" HeaderText="Payment Date"
                                UniqueName="PaymentDate" SortExpression="PaymentDate" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="RegistrationNo" HeaderText="Registration No"
                                UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn DataField="PatientID" HeaderText="Patient ID" UniqueName="PatientID"
                                HeaderStyle-Width="80px" SortExpression="PatientID" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" Visible="false" />
                            <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                                HeaderStyle-Width="80px" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                                SortExpression="PatientName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="BalanceAmount" HeaderText="Amount"
                                UniqueName="BalanceAmount" SortExpression="BalanceAmount" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Payment Amount"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" UniqueName="PaymentAmount"
                                FooterStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txtPaymentAmount" runat="server" Width="85px" DbValue='<%#Eval("PaymentAmount")%>'
                                        OnTextChanged="txtPaymentAmount_TextChanged" AutoPostBack="true"
                                    >
                                        <ClientEvents OnBlur="Blur" OnFocus="Focus" />
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <telerik:RadNumericTextBox ID="txtSumPaymentAmount" runat="server" Width="85px">
                                        <ClientEvents OnLoad="Load" />
                                    </telerik:RadNumericTextBox>
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="85px" HeaderText="Discount" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txtOtherAmount" runat="server" Width="77px" DbValue='<%#Eval("OtherAmount")%>' 
                                        OnTextChanged="txtOtherAmount_TextChanged" AutoPostBack="true"
                                    />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="50px" HeaderText="Disc In %" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkIsDiscInPercent" runat="server" Width="45px" Checked='<%#Eval("IsDiscountInPercent")%>'
                                        AutoPostBack="true" OnCheckedChanged="ToggleDiscSelectedState" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="Reason" HeaderText="Reason">
                                <HeaderStyle Width="130px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <telerik:RadComboBox ID="cboSRDiscountReason" runat="server" Width="123px" EnableLoadOnDemand="true"
                                        MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboSRDiscountReason_ItemDataBound"
                                        OnItemsRequested="cboSRDiscountReason_ItemsRequested" 
                                        OnSelectedIndexChanged="cboSRDiscountReason_SelectedIndexChanged" AutoPostBack="true" >
                                    </telerik:RadComboBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="SRDiscountReason" UniqueName="SRDiscountReason" Visible="False" />
                            <telerik:GridTemplateColumn HeaderStyle-Width="85px" HeaderText="Bank Cost" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txtBankCost" runat="server" Width="77px" DbValue='<%#Eval("BankCost")%>'
                                        OnTextChanged="txtPaymentAmount_TextChanged" AutoPostBack="true"
                                    />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="85px" HeaderText="Remaining" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" UniqueName="remaining">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txtRemaining" runat="server" Width="77px" Enabled="false" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="ClaimDifferenceAmount" HeaderText="Claim Diff."
                                UniqueName="ClaimDifferenceAmount" SortExpression="ClaimDifferenceAmount" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="false" />
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
