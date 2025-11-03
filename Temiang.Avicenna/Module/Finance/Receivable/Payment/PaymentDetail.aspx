<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="PaymentDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Receivable.PaymentDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">
            function showPendingInvoices() {
                var oWnd = $find("<%= winInvoices.ClientID %>");
                var grr = $find("<%= cboGuarantorID.ClientID %>");

                oWnd.setUrl('InvoicePickListDialog.aspx?grr=' + grr.get_value());
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function deleteAllRow() {

            }

            function openWinGuarantorInfo() {
                var oWnd = $find("<%= winGInfo.ClientID %>");
                var oguar = $find("<%= cboGuarantorID.ClientID %>");
                var lblToBeUpdate = "<%= lblGuarantorInfo.ClientID %>";
                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/Finance/Master/GuarantorInfo/GuarantorInfoDialog.aspx?id=' + oguar.get_value() + '&lblGuarantorInfo=' + lblToBeUpdate + '")%>');
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.id != null)
                    __doPostBack("<%= grdItem.UniqueID %>", oWnd.argument.id);
            }

            function rowRecalFeePercByAjax(invoiceNo) {
                //alert("ddd");
                $.ajax({
                    type: 'POST',
                    url: "<%=Page.ResolveUrl("~/Home/ParSetProporsiJasmed")%>",
                    data: { InvoiceNo: invoiceNo },
                    success: function (data) {
                        //console.log(data);
                        if (data.status === 'OK') {
                            //console.log(data.data);

                        } else {
                            //DisplayToast(ret.data, "error");
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        //DisplayToast(xhr.responseText, "error");
                    },
                    dataType: 'json'
                });

                //var divCtl = $(document.getElementById(invoiceNo));
                //DisplayStatus(divCtl, '01', 0, invoiceNo);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="600px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="onClientClose"
        ID="winInvoices">
    </telerik:RadWindow>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="600px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true"
        ID="winGInfo">
    </telerik:RadWindow>
    <asp:HiddenField runat="server" ID="hdnGuarantorId"/>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="vertical-align: top; width: 38%">
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
                            <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox runat="server" ID="cboGuarantorID" Width="300px" EnableLoadOnDemand="true"
                                            HighlightTemplatedItems="true" OnItemDataBound="cboGuarantorID_ItemDataBound"
                                            OnItemsRequested="cboGuarantorID_ItemsRequested" AutoPostBack="true" OnSelectedIndexChanged="cboGuarantorID_SelectedIndexChanged">
                                            <ItemTemplate>
                                                <b>
                                                    <%# DataBinder.Eval(Container.DataItem, "GuarantorName")%>
                                        &nbsp;(<%# DataBinder.Eval(Container.DataItem, "GuarantorID")%>) </b>
                                                <br />
                                                <%# DataBinder.Eval(Container.DataItem, "Address") %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                Note : Show max 20 items
                               
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>&nbsp; <a href="javascript:void(0);" onclick="javascript:openWinGuarantorInfo();"
                                        class="noti_Container">
                                        <asp:Label CssClass="noti_bubble" runat="server" ID="lblGuarantorInfo" AssociatedControlID="cboGuarantorID"
                                            Text=""></asp:Label>&nbsp; </a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvGuarantorID" runat="server" ErrorMessage="Guarantor required."
                                ValidationGroup="entry" ControlToValidate="cboGuarantorID" SetFocusOnError="True"
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
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPrintReceiptAsName" runat="server" Text="Receipt As Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPrintReceiptAsName" runat="server" Width="300px" MaxLength="200" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width: 37%">
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
                        <td></td>
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
                        <td></td>
                    </tr>
                    <tr runat="server" id="pnlDiscountReason">
                        <td class="label">
                            <asp:Label ID="lblSRDiscountReason" runat="server" Text="Discount Reason"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRDiscountReason" runat="server" Width="300px" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="pnlCardProvider">
                        <td class="label">
                            <asp:Label ID="lblSRCardProvider" runat="server" Text="Card Provider"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRCardProvider" runat="server" Width="300px" AutoPostBack="True"
                                OnSelectedIndexChanged="cboSRCardProvider_SelectedIndexChanged" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <asp:Panel ID="pnlCardDetail" runat="server">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSRCardType" runat="server" Text="Card Type"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRCardType" runat="server" Width="300px" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblEDCMachineID" runat="server" Text="EDC Machine"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboEDCMachineID" runat="server" Width="300px" AutoPostBack="true"
                                    OnSelectedIndexChanged="cboEDCMachineID_SelectedIndexChanged" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblCardHolderName" runat="server" Text="Card Holder Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtCardHolderName" runat="server" Width="300px" MaxLength="100" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>                        
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlRounding">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRoundingAR" runat="server" Text="Rounding (+/-)"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtRoundingAR" runat="server" Width="300px" MaxLength="100" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
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
                                    OnItemsRequested="cboBankID_ItemsRequested" AutoPostBack="true" OnSelectedIndexChanged="cboBankID_SelectedIndexChanged">
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBankAccountNo" runat="server" Text="Bank Account No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtBankAccountNo" runat="server" Width="300px" MaxLength="100" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                    </asp:Panel>
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
            <td style="vertical-align: top; width: 25%">
                <fieldset>
                    <legend>Related Bank Inquiry
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
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None"
        OnDeleteCommand="grdItem_DeleteCommand" ShowFooter="True">
        <MasterTableView CommandItemDisplay="None" DataKeyNames="InvoiceNo, PaymentNo, InvoiceReferenceNo">
            <CommandItemTemplate>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 10px;">&nbsp;
                        </td>
                        <td style="width: 200px;">
                            <asp:LinkButton ID="lbPickList" runat="server" Visible='<%# !grdItem.MasterTableView.IsItemInserted %>'
                                OnClientClick="javascript:showPendingInvoices();return false;">
                                <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/views16.png" />&nbsp;<asp:Label
                                    runat="server" ID="lblPicList" Text="Pick from Invoice Outstanding"></asp:Label>
                            </asp:LinkButton>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td style="width: 90px;">
                            <asp:LinkButton ID="lbDeleteAll" runat="server" Visible='<%# !grdItem.MasterTableView.IsItemInserted %>'
                                OnClientClick="javascript:return confirm('Are You Sure Want To Delete All Row?');" OnClick="lbDeleteAll_OnClick">
                                <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/delete16.png" />&nbsp;<asp:Label
                                    runat="server" ID="Label1" Text="Delete All"></asp:Label>
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </CommandItemTemplate>
            <CommandItemStyle Height="29px" />
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="InvoiceReferenceNo" HeaderText="Invoice No"
                    UniqueName="InvoiceReferenceNo" SortExpression="InvoiceReferenceNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PaymentNo" HeaderText="Payment No"
                    UniqueName="PaymentNo" SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="PaymentDate" HeaderText="Payment Date"
                    UniqueName="PaymentDate" SortExpression="PaymentDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="RegistrationNo" HeaderText="Registration No"
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
                <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Amount" UniqueName="TotalVerifyAmount"
                    DataType="System.Double" DataFields="VerifyAmount"
                    SortExpression="TotalVerifyAmount" Expression="{0}" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Payment Amount" UniqueName="TotalPaymentAmount"
                    DataType="System.Double" DataFields="PaymentAmount"
                    SortExpression="TotalPaymentAmount" Expression="{0}" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Discount" UniqueName="TotalOtherAmount"
                    DataType="System.Double" DataFields="OtherAmount"
                    SortExpression="TotalOtherAmount" Expression="{0}" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="DiscountReason" HeaderText="Discount Reason"
                    UniqueName="DiscountReason" SortExpression="DiscountReason" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Bank Cost" UniqueName="TotalBankCost"
                    DataType="System.Double" DataFields="BankCost"
                    SortExpression="TotalBankCost" Expression="{0}" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
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
