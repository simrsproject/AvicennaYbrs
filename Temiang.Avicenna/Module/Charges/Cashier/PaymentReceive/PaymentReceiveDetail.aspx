<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="PaymentReceiveDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.Cashier.PaymentReceiveDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow runat="server" Animation="None" Behavior="Close, Move" ShowContentDuringLoad="False"
        VisibleStatusbar="False" Modal="true" ID="winPayment" OnClientClose="onClientClose">
    </telerik:RadWindow>
    <telerik:RadWindow runat="server" Animation="None" Behavior="Close, Move" ShowContentDuringLoad="False"
        VisibleStatusbar="False" Modal="true" ID="winIntermBillGuarantor" OnClientClose="onClientClose">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadWindow runat="server" Animation="None" Width="500px" Height="400px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" Title="Document Checklist"
        ID="winDocsOption">
    </telerik:RadWindow>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">

        <script type="text/javascript">
            function openWinRegistrationInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationInfo.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }

            function openWinQuestionFormCheckList() {
                var oWnd = $find("<%= winDocsOption.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationInfo2.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/Registration/RegistrationDocumentCheckList.aspx?regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.set_title('Document Checklist');
                oWnd.show();
            }
        </script>

    </telerik:RadCodeBlock>
    <asp:HiddenField runat="server" ID="hdnPatientId" />
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image6" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text="There are ORDER TRANSACTIONS that have been paid but not yet realized. Please check back in before making the payment."></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlInfo2" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image4" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="There are transactions that have not been INTERIM BILL processed. Please check back in before making the payment."></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPaymentNo" runat="server" Text="Payment No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPaymentNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvPaymentNo" runat="server" ErrorMessage="Payment No required."
                                ValidationGroup="entry" ControlToValidate="txtPaymentNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPaymentDate" runat="server" Text="Payment Date / Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtPaymentDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                            DatePopupButton-Enabled="false">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPaymentTime" runat="server" Width="50px" MaxLength="5"
                                            ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnit" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100px" MaxLength="20"
                                ReadOnly="true" />
                            &nbsp;
                            <asp:Label runat="server" ID="lblServiceUnitName"></asp:Label>
                        </td>
                        <td width="20"></td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvServiceUnit" runat="server" ErrorMessage="Service Unit required."
                                ValidationGroup="entry" ControlToValidate="txtServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                            <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();" class="noti_Container">
                                <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo"
                                    Text=""></asp:Label>&nbsp; </a>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvRegistrationNo" runat="server" ErrorMessage="Registration No required."
                                ValidationGroup="entry" ControlToValidate="txtRegistrationNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtSalutation" runat="server" Width="28px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="270px" ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                        <a href="javascript:void(0);" onclick="javascript:openWinQuestionFormCheckList();"
                                            class="noti2_Container">
                                            <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo2" AssociatedControlID="txtRegistrationNo"
                                                Text=""></asp:Label>&nbsp; </a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
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
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantor" runat="server" Text="Guarantor"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtGuarantorID" runat="server" Width="100px" ReadOnly="true" />
                            &nbsp;
                            <asp:Label runat="server" ID="lblGuarantorName"></asp:Label>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblChargeToGuarantorID" runat="server" Text="Charged To Guarantor"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboGuarantorID" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboGuarantorID_ItemDataBound"
                                OnItemsRequested="cboGuarantorID_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "GuarantorID")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "GuarantorName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMemberID" runat="server" Text="Member ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMemberID" runat="server" Width="100px" MaxLength="10"
                                ReadOnly="True" />
                            &nbsp;
                            <asp:Label ID="lblMemberName" runat="server" CssClass="labeldescription"></asp:Label>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" TextMode="MultiLine" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPaymentTo" runat="server" Text="Payment Status"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rblToGuarantor" runat="server" RepeatDirection="Horizontal"
                                OnTextChanged="rblToGuarantor_OnTextChanged" AutoPostBack="true" Enabled="False">
                                <asp:ListItem Selected="true">To Guarantor</asp:ListItem>
                                <asp:ListItem>To Patient</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr id="trPromotion" runat="server">
                        <td class="label">
                            <asp:Label ID="lblPromotion" runat="server" Text="Promotion"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPromotion" runat="server" Width="300px" AllowCustomText="true">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trInitial">
                        <td class="label">
                            <asp:Label ID="lblInitial" runat="server" Text="Initial"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInitial" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td></td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPatientAmount" runat="server" Text="Patient Amount"></asp:Label>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblGuarantorAmount" runat="server" Text="Guarantor Amount"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTotalPaymentAmount" runat="server" Text="Total Payment Amount"></asp:Label>
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtTotalPaymentAmountPatient" runat="server" Width="120px"
                                            ReadOnly="True" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtTotalPaymentAmountGuarantor" runat="server" Width="120px"
                                            ReadOnly="True" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label4" runat="server" Text="Total Payment Discount Amount"></asp:Label>
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtPaymentDiscountPatient" runat="server" Width="120px"
                                            ReadOnly="True" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtPaymentDiscountGuarantor" runat="server" Width="120px"
                                            ReadOnly="True" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRemainingAmount" runat="server" Text="Remaining Amount"></asp:Label>
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtRemainingAmountPatient" runat="server" Width="120px"
                                            ReadOnly="True" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtRemainingAmountGuarantor" runat="server" Width="120px"
                                            ReadOnly="True" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAdmAmt" runat="server" Text="Administration Fee Amount"></asp:Label>
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtPatientAdm" runat="server" Width="120px" ReadOnly="True" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtGuarantorAdm" runat="server" Width="120px" ReadOnly="True" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label12" runat="server" Text="Transaction / Interim Bill Amount"></asp:Label>
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td class="entry">
                                        <telerik:RadNumericTextBox ID="txtTransPatientAmount" runat="server" Width="120px"
                                            ReadOnly="True" />
                                    </td>
                                    <td class="entry">
                                        <telerik:RadNumericTextBox ID="txtTransGuarantorAmount" runat="server" Width="120px"
                                            ReadOnly="True" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label2" runat="server" Text="Plafond Amount"></asp:Label>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label3" runat="server" Text="Down Payment Amount"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtPlafonAmount" runat="server" Width="120px" ReadOnly="True" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtDownPaymentAmount" runat="server" Width="120px"
                                            ReadOnly="True" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRoundingAmount" runat="server" Text="Rounding Amount"></asp:Label>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="Label5" runat="server" Text="Total Discount Amount"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtRoundingAmount" runat="server" Width="120px" ReadOnly="True" />
                                        <telerik:RadNumericTextBox ID="txtAdminCal" runat="server" Width="120px" ReadOnly="True"
                                            Visible="False" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtDiscountAmount" runat="server" Width="120px" ReadOnly="True" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td class="entry">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSummary" runat="server" Text="Summary" OnClientClick="openWinProcess();return false;" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnOrderItem" runat="server" Text="Transaction List" OnClientClick="openWinOrderItem();return false;" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnIntermBill" runat="server" Text="Interim Bill Patient List" OnClientClick="openWinIB();return false;" />
                                        <asp:Button ID="btnIntermBillGuarantor" runat="server" Text="Interim Bill Guarantor List"
                                            OnClientClick="openWinIBG();return false;" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnDownPayment" runat="server" Text="Down Payment" OnClientClick="openWinDP();return false;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Payment" PageViewID="pgPayment" Selected="True" />
            <telerik:RadTab runat="server" Text="Transaction Item" PageViewID="pgOrderItem" />
            <telerik:RadTab runat="server" Text="Interim Bill Patient" PageViewID="pgIntermBill" />
            <telerik:RadTab runat="server" Text="Interim Bill Guarantor" PageViewID="pgIntermBillGuarantor" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgPayment" runat="server" Selected="true">
            <telerik:RadGrid ID="grdTransPaymentItem" runat="server" OnNeedDataSource="grdTransPaymentItem_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdTransPaymentItem_UpdateCommand"
                OnDeleteCommand="grdTransPaymentItem_DeleteCommand" OnInsertCommand="grdTransPaymentItem_InsertCommand"
                OnItemDataBound="grdTransPaymentItem_ItemDataBound">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="PaymentNo, SequenceNo">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="PaymentTypeName" HeaderText="Payment Type" UniqueName="PaymentTypeName"
                            SortExpression="PaymentTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="PaymentMethodName" HeaderText="Payment Method"
                            UniqueName="PaymentMethodName" SortExpression="PaymentMethodName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Amount" HeaderText="Amount"
                            UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                            FooterStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="AmountReceived" HeaderText="Received"
                            UniqueName="AmountReceived" SortExpression="AmountReceived" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                            FooterStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Change" HeaderText="Change"
                            UniqueName="Change" SortExpression="Change" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                            FooterStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RoundingAmount" HeaderText="Rounding"
                            UniqueName="RoundingAmount" SortExpression="RoundingAmount" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                            FooterStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="CardFeeAmount" HeaderText="Card Fee Amount"
                            UniqueName="CardFeeAmount" SortExpression="CardFeeAmount" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                            FooterStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Balance" HeaderText="Return"
                            UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsFromDownPayment" HeaderText="From DP"
                            UniqueName="IsFromDownPayment" SortExpression="IsFromDownPayment" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn HeaderText="Back Office Return" UniqueName="IsBackOfficeReturn"
                            HeaderStyle-Width="80px">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkIsBackOfficeReturn" runat="server" OnCheckedChanged="chkIsBackOfficeReturn_CheckedChanged"
                                    AutoPostBack="true" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ItemPaymentReceive.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ItemPaymentReceiveEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgOrderItem" runat="server">
            <telerik:RadGrid ID="grdOrderItem" runat="server" OnNeedDataSource="grdOrderItem_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" ShowFooter="true">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, SequenceNo"
                    GroupLoadMode="Client">
                    <CommandItemTemplate>
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbClearItem" runat="server" OnClientClick="javascript:OnClientClickClearList();return false;">
                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/delete16.png" />
                            &nbsp;<asp:Label runat="server" ID="lblClearItem" Text="Clear List Item"></asp:Label>
                        </asp:LinkButton>
                    </CommandItemTemplate>
                    <CommandItemStyle Height="29px" />
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="PaymentNo" HeaderText="PaymentNo"
                            UniqueName="PaymentNo" SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="ServiceUnitName" HeaderText="Service Unit"
                            UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Transaction No"
                            UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                            HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" />
                        <%--<telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                            Aggregate="Count" FooterAggregateFormatString="Total :" FooterStyle-HorizontalAlign="Right" />--%>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Qty" HeaderText="Qty"
                            UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                            UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Total" UniqueName="TotalToDisplay"
                            DataType="System.Double" DataFields="TotalToDisplay" SortExpression="TotalToDisplay"
                            FooterStyle-HorizontalAlign="Right" Aggregate="Sum" FooterAggregateFormatString="{0:n2}"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <%--<telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Total" UniqueName="Total"
                            DataType="System.Double" DataFields="Qty,Price" SortExpression="Total" Expression="{0} * {1}"
                            FooterStyle-HorizontalAlign="Right" Aggregate="Sum" FooterAggregateFormatString="{0:n2}"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />--%>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgIntermBill" runat="server">
            <telerik:RadGrid ID="grdIntermBill" runat="server" OnNeedDataSource="grdIntermBill_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" ShowFooter="true">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="IntermBillNo" GroupLoadMode="Client">
                    <CommandItemTemplate>
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbClearIntermBill" runat="server" OnClientClick="javascript:OnClientClickClearIntermBillList();return false;">
                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/delete16.png" />
                            &nbsp;<asp:Label runat="server" ID="lblClearIntermBill" Text="Clear List Interim Bill Patient"></asp:Label>
                        </asp:LinkButton>
                    </CommandItemTemplate>
                    <CommandItemStyle Height="29px" />
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="PaymentNo" HeaderText="PaymentNo"
                            UniqueName="PaymentNo" SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="IntermBillNo" HeaderText="Interim Bill No"
                            UniqueName="IntermBillNo" SortExpression="IntermBillNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="IntermBillDate" HeaderText="Date"
                            UniqueName="IntermBillDate" SortExpression="IntermBillDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="StartDate" HeaderText="Start Date"
                            UniqueName="StartDate" SortExpression="StartDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="False" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="EndDate" HeaderText="End Date"
                            UniqueName="EndDate" SortExpression="EndDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="False" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PatientAmount" HeaderText="Amount"
                            UniqueName="PatientAmount" SortExpression="PatientAmount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridTemplateColumn>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgIntermBillGuarantor" runat="server">
            <telerik:RadGrid ID="grdIntermBillGuarantor" runat="server" OnNeedDataSource="grdIntermBillGuarantor_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" ShowFooter="true">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="IntermBillNo" GroupLoadMode="Client">
                    <CommandItemTemplate>
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbClearIntermBillGuarantor" runat="server" OnClientClick="javascript:OnClientClickClearIntermBillGuarantorList();return false;">
                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/delete16.png" />
                            &nbsp;<asp:Label runat="server" ID="lblClearIntermBillGuarantor" Text="Clear List Interim Bill Guarantor"></asp:Label>
                        </asp:LinkButton>
                    </CommandItemTemplate>
                    <CommandItemStyle Height="29px" />
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="PaymentNo" HeaderText="PaymentNo"
                            UniqueName="PaymentNo" SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="IntermBillNo" HeaderText="Interim Bill No"
                            UniqueName="IntermBillNo" SortExpression="IntermBillNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="IntermBillDate" HeaderText="Date"
                            UniqueName="IntermBillDate" SortExpression="IntermBillDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="StartDate" HeaderText="Start Date"
                            UniqueName="StartDate" SortExpression="StartDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="False" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="EndDate" HeaderText="End Date"
                            UniqueName="EndDate" SortExpression="EndDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="False" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="GuarantorAmount"
                            HeaderText="Amount" UniqueName="GuarantorAmount" SortExpression="GuarantorAmount"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridTemplateColumn>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">

            function openWinProcess() {
                var oWnd = $find("<%= winPayment.ClientID %>");
                oWnd.setUrl('PaymentReceiveSummaryList.aspx?regNo=' + '<%= Request.QueryString["regNo"] %>');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinDP() {
                var oWnd = $find("<%= winPayment.ClientID %>");
                var otot = $find("<%= txtTransPatientAmount.ClientID %>");

                if (otot.get_value() == '0.00') {
                    alert('Transaction / Interim Bill amount required.');
                    return;
                }

                oWnd.setUrl('DownPaymentSummaryList.aspx?regNo=' + '<%= Request.QueryString["regNo"] %>' + "&tot=" + otot.get_value());
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinOrderItem() {
                var oWnd = $find("<%= winPayment.ClientID %>");
                var oreg = $find("<%= txtRegistrationNo.ClientID %>");
                var opy = $find("<%= txtPaymentNo.ClientID %>");
                oWnd.setUrl("DetailItemList.aspx?regno=" + oreg.get_value() + "&pyno=" + opy.get_value());
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinIB() {
                var oWnd = $find("<%= winPayment.ClientID %>");
                var opy = $find("<%= txtPaymentNo.ClientID %>");
                oWnd.setUrl('IntermBillList.aspx?regNo=' + '<%= Request.QueryString["regNo"] %>' + "&pyno=" + opy.get_value());
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinIBG() {
                var oWnd = $find("<%= winIntermBillGuarantor.ClientID %>");
                var opy = $find("<%= txtPaymentNo.ClientID %>");
                var oguar = $find("<%= cboGuarantorID.ClientID %>");
                oWnd.setUrl('IntermBillGuarantorList.aspx?regNo=' + '<%= Request.QueryString["regNo"] %>' + "&pyno=" + opy.get_value() + "&guarid=" + oguar.get_value());
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument) {
                    var val = oWnd.argument.split('|');
                    if (val[0] == 'rebind') {
                        __doPostBack("<%= grdTransPaymentItem.UniqueID %>", oWnd.argument);
                        oWnd.argument = 'undefined';
                    }
                    if (val[0] == 'refresh') {
                        __doPostBack("<%= grdTransPaymentItem.UniqueID %>", oWnd.argument);
                        oWnd.argument = 'undefined';
                    }
                    if (val[0] == 'rebindo') {
                        __doPostBack("<%= grdOrderItem.UniqueID %>", oWnd.argument);
                        oWnd.argument = 'undefined';
                    }
                    if (val[0] == 'rebindib') {
                        __doPostBack("<%= grdIntermBill.UniqueID %>", oWnd.argument);
                        oWnd.argument = 'undefined';
                    }
                    if (val[0] == 'rebindibg') {
                        __doPostBack("<%= grdIntermBillGuarantor.UniqueID %>", oWnd.argument);
                        oWnd.argument = 'undefined';
                    }
                }
            }

            function OnClientClickClearList() {
                __doPostBack("<%= grdOrderItem.UniqueID %>", 'clearlist');
            }

            function OnClientClickClearIntermBillList() {
                __doPostBack("<%= grdIntermBill.UniqueID %>", 'clearlistib');
            }
            function OnClientClickClearIntermBillGuarantorList() {
                __doPostBack("<%= grdIntermBillGuarantor.UniqueID %>", 'clearlistibg');
            }
        </script>

    </telerik:RadCodeBlock>
</asp:Content>
