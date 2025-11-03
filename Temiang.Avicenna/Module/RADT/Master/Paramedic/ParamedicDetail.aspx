<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ParamedicDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ParamedicDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/CustomControl/AddressCtl.ascx" TagName="Address" TagPrefix="uc1" %>
<%@ Register Src="~/PCareCommon/LookUp/PCareReferenceCtl.ascx" TagPrefix="uc2" TagName="PCareReferenceCtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script language="javascript" type="text/javascript">
            var fnNo = 0;
            function openItemGuarantor(itemID) {
                fnNo = 1;
                var oPar = $find("<%= txtParamedicID.ClientID %>");
                var oWnd = $find("<%= winItemGuarantor.ClientID %>");
                oWnd.SetUrl("ParamedicFeeItemGuarantorList.aspx?paramedicID=" + oPar.get_value() + "&itemID=" + itemID);
                oWnd.Show();
                oWnd.Maximize();
            }
            function openItemComp(itemID) {
                fnNo = 2; global
                var oPar = $find("<%= txtParamedicID.ClientID %>");
                var oWnd = $find("<%= winItemGuarantor.ClientID %>");
                oWnd.SetUrl("ParamedicFeeItemCompList.aspx?paramedicID=" + oPar.get_value() + "&itemID=" + itemID);
                oWnd.Show();
                oWnd.Maximize();
            }
            function openGuarantorCategoryItem(feeType) {
                fnNo = 3;
                var oPar = $find("<%= txtParamedicID.ClientID %>");
                var oWnd = $find("<%= winItemGuarantor.ClientID %>");
                oWnd.SetUrl("ParamedicFeeGuarantorCategoryItemList.aspx?paramedicID=" + oPar.get_value() + "&feeType=" + feeType);
                oWnd.Show();
                oWnd.Maximize();
            }

            function OpenWinUpload(type) {
                fnNo = 4;
                var oPar = $find("<%= txtParamedicID.ClientID %>");
                var oWnd = $find("<%= winItemGuarantor.ClientID %>");
                oWnd.SetUrl("ParamedicUploadFoto.aspx?parid=" + oPar.get_value() + "&type=" + type);
                oWnd.Show();
            }

            function win_ClientClose(oWnd, args) {
                if (fnNo == 4) {
                    var arg = args.get_argument();
                    if (arg != null) {
                        __doPostBack('<%= grdServiceUnitParamedic.UniqueID%>', "AfterUpload");
                    }
                }
            }
            function openScheduleGlobal(unitId) {
                fnNo = 5;
                var oPar = $find("<%= txtParamedicID.ClientID %>");
                var oWnd = $find("<%= winItemGuarantor.ClientID %>");
                oWnd.SetUrl("ParamedicGlobalScheduleDialog.aspx?parId=" + oPar.get_value() + "&unitId=" + unitId);
                oWnd.Show();
                oWnd.Maximize();
            }
        </script>

    </telerik:RadCodeBlock>

    <telerik:RadWindow ID="winItemGuarantor" Animation="None" Width="800px" Height="500px"
        runat="server" Behavior="Maximize,Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true" OnClientClose="win_ClientClose">
    </telerik:RadWindow>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="40%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtParamedicID" runat="server" Width="100px">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvParamedicID" runat="server" ErrorMessage="Physician ID required."
                                ControlToValidate="txtParamedicID" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trPcare">
                        <td colspan="4">
                            <uc2:PCareReferenceCtl runat="server" ID="pcareReference" ReferenceType="Dokter" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicName" runat="server" Text="Physician Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtParamedicName" runat="server" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvParamedicName" runat="server" ErrorMessage="Physician Name required."
                                ControlToValidate="txtParamedicName" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicInitial" runat="server" Text="Physician Initial"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtParamedicInitial" runat="server" Width="100px" MaxLength="5">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSsn" runat="server" Text="SSN"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSsn1" runat="server" Width="300px" MaxLength="50"></telerik:RadTextBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblDateOfBirth" runat="server" Text="Date Of Birth"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtDateOfBirth" runat="server" Width="100px">
                            </telerik:RadDatePicker>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvDateOfBirth" runat="server" ErrorMessage="Date Of Birth required."
                                ControlToValidate="txtDateOfBirth" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRParamedicType" runat="server" Text="Physician Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRParamedicType" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRParamedicType" runat="server" ErrorMessage="Physician Type required."
                                ControlToValidate="cboSRParamedicType" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRParamedicStatus" runat="server" Text="Physician Status"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRParamedicStatus" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRParamedicStatus" runat="server" ErrorMessage="Physician Status required."
                                ControlToValidate="cboSRParamedicStatus" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRReligion" runat="server" Text="Religion"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRReligion" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRReligion" runat="server" ErrorMessage="Religion required."
                                ControlToValidate="cboSRReligion" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRNationality" runat="server" Text="Nationality"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRNationality" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRNationality" runat="server" ErrorMessage="Nationality required."
                                ControlToValidate="cboSRNationality" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr">
                        <td class="label">
                            <asp:Label ID="lblSRSpecialty" runat="server" Text="Specialty"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRSpecialty" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRParamedicRL1" runat="server" Text="SMF"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRParamedicRL1" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" TextMode="MultiLine">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trParamedicQueueCode" visible="false">
                        <td class="label">
                            <asp:Label ID="lblParamedicQueueCode" runat="server" Text="Queue Code Format"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtParamedicQueueCode" runat="server" Width="100px" MaxLength="3"></telerik:RadTextBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 40%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblLicenseNo" runat="server" Text="License No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtLicenseNo" runat="server" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvLicenseNo" runat="server" ErrorMessage="License No required."
                                ControlToValidate="txtLicenseNo" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblLisenceNoPeriode" runat="server" Text="License No Periode"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtPeriodeStart" runat="server" Width="100px">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>&nbsp;To&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtPeriodeEnd" runat="server" Width="100px">
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTaxRegistrationNo" runat="server" Text="Tax Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTaxRegistrationNo" runat="server" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvTaxRegistrationNo" runat="server" ErrorMessage="Tax Registration No required."
                                ControlToValidate="txtTaxRegistrationNo" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr style="height: 24px; display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsAvailable" Text="Available" runat="server" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblNotAvailableUntil" runat="server" Text="Not Available Until"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtNotAvailableUntil" runat="server" Width="100px">
                            </telerik:RadDatePicker>
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
                                        <asp:Label ID="lblBank1" runat="server" Text="Bank 1"></asp:Label>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblBank2" runat="server" Text="Bank 2"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBank" runat="server" Text="Bank"></asp:Label>
                        </td>
                        <td class="entry">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtBank" runat="server" Width="100%" MaxLength="50">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td style="width: 1px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtBank2" runat="server" Width="100%" MaxLength="50">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBankAccount" runat="server" Text="Bank Account"></asp:Label>
                        </td>
                        <td class="entry">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtBankAccount" runat="server" Width="100%" MaxLength="50">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td style="width: 1px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtBankAccount2" runat="server" Width="100%" MaxLength="50">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBankAccountName" runat="server" Text="Bank Account Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtBankAccountName" runat="server" Width="100%" MaxLength="200">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td style="width: 1px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtBankAccountName2" runat="server" Width="100%" MaxLength="200">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <asp:Panel runat="server" ID="pnlPhyscianFeeGlobal">
                        <tr>
                            <td></td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblInfoFee" runat="server" Text="Fee"></asp:Label>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblInfoDeduction" runat="server" Text="Deduction"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label"></td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td class="entry">
                                            <asp:CheckBox ID="chkIsParamedicFeeUsingPercentage" Text="Fee Using Percentage" runat="server" />
                                        </td>
                                        <td class="entry">
                                            <asp:CheckBox ID="chkIsDeductionFeeUsePercentage" Text="Deduction Using Percentage"
                                                runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFeeAmount" runat="server" Text="Amount for Patient Direct"></asp:Label>
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtParamedicFeeAmount" runat="server" Width="100px">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtDeductionFeeAmount" runat="server" Width="100px">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20">
                                <asp:RequiredFieldValidator ID="rfvFeeAmount" runat="server" ErrorMessage="Fee Amount required."
                                    ControlToValidate="txtParamedicFeeAmount" ValidationGroup="entry" SetFocusOnError="True"
                                    Width="100%">*</asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfvDeductionAmount" runat="server" ErrorMessage="Deduction Amount required."
                                    ControlToValidate="txtDeductionFeeAmount" ValidationGroup="entry" SetFocusOnError="True"
                                    Width="100%">*</asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFeeAmountReferral" runat="server" Text="Amount for Patient Referral"></asp:Label>
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtParamedicFeeAmountReferral" runat="server" Width="100px">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtDeductionFeeAmountReferral" runat="server" Width="100px">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20">
                                <asp:RequiredFieldValidator ID="rfvFeeAmountReferral" runat="server" ErrorMessage="Fee Amount Referral required."
                                    ControlToValidate="txtParamedicFeeAmountReferral" ValidationGroup="entry" SetFocusOnError="True"
                                    Width="100%">*</asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfvDeductionAmountReferral" runat="server" ErrorMessage="Deduction Amount Referral required."
                                    ControlToValidate="txtDeductionFeeAmountReferral" ValidationGroup="entry" SetFocusOnError="True"
                                    Width="100%">*</asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                    </asp:Panel>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsUsingQue" Text="Using Que Slot" runat="server" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="COA A/P"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdAPParamedicFee" Width="300px"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdAPParamedicFee_SelectedIndexChanged"
                                OnItemDataBound="cboChartOfAccountIdAPParamedicFee_ItemDataBound" OnItemsRequested="cboChartOfAccountIdAPParamedicFee_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
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
                            <asp:Label ID="Label4" runat="server" Text="Subledger A/P"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdAPParamedicFee" Width="300px"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdAPParamedicFee_ItemDataBound" OnItemsRequested="cboSubledgerIdAPParamedicFee_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trGuaranteeFee">
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Guarantee Fee"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtGuaranteeFee" runat="server" Width="100px" NumberFormat-DecimalDigits="0">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trCoorporateGradeID">
                        <td class="label">
                            <asp:Label ID="lblCoorporateGradeID" runat="server" Text="Coorporate Grade / Value"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox runat="server" ID="cboCoorporateGradeID" Width="195px"
                                            EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                            OnItemDataBound="cboCoorporateGradeID_ItemDataBound" OnItemsRequested="cboCoorporateGradeID_ItemsRequested">
                                            <ItemTemplate>
                                                <%# string.Format("Level: {0}, Grade Min: {1}, Grade Max: {2}", 
                                                    DataBinder.Eval(Container.DataItem, "CoorporateGradeLevel"),
                                                    DataBinder.Eval(Container.DataItem, "CoorporateGradeMin"),
                                                    DataBinder.Eval(Container.DataItem, "CoorporateGradeMax")) %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                Note : Show max 20 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td style="width: 5px"></td>
                                    <td>
                                        <telerik:RadNumericTextBox runat="server" ID="txtCoorporateValue" Width="100px"></telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                            </table>

                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkIsActive" Text="Active" runat="server" />
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:CheckBox ID="chkPhysicianFee" Text="Process Fee" runat="server" />
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:CheckBox ID="chkPrintInSlip" Text="Print In Slip" runat="server" />
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:CheckBox ID="chkIsPhysicianTeam" Text="Physician Team" OnCheckedChanged="chkIsPhysicianTeam_CheckedChanged" AutoPostBack="true" Visible="false" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 20%; vertical-align: top;">
                <fieldset>
                    <legend>Photo</legend>
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Image ID="imgFoto" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClientClick="OpenWinUpload('1');return 0;" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <%--                <fieldset>
                    <legend>Siganture</legend>
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Image ID="imgSignature" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnUploadSignature" Text="Upload" runat="server" OnClientClick="OpenWinUpload('2');return 0;" />
                            </td>
                        </tr>
                    </table>
                </fieldset>--%>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Address" PageViewID="pvAddress" Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Fee Item Matrix" PageViewID="pvFeeItemMtx">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Fee Guarantor Category Matrix" PageViewID="pvFeeGuarCategory"
                Visible="False">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Auto Bill Item" PageViewID="pgvAutoBillItem">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Service Unit" PageViewID="pgvServiceUnitParamedic">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Bridging & Integration" PageViewID="pgvAliasName" />
            <telerik:RadTab runat="server" Text="Other Physician Type" PageViewID="pgvPhysicianType" />
            <telerik:RadTab runat="server" Text="Physician Team" PageViewID="pgvPhysicianTeam" Visible="false" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pvAddress" runat="server" Selected="true">
            <uc1:Address ID="ctlAddress" runat="server" />
        </telerik:RadPageView>
        <telerik:RadPageView ID="pvFeeItemMtx" runat="server">
            <telerik:RadGrid ID="grdParamedicFeeItem" runat="server" OnNeedDataSource="grdParamedicFeeItem_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdParamedicFeeItem_UpdateCommand"
                OnDeleteCommand="grdParamedicFeeItem_DeleteCommand" OnInsertCommand="grdParamedicFeeItem_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="ParamedicID,ItemID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                            SortExpression="ItemID">
                            <HeaderStyle HorizontalAlign="Left" Width="90px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                            SortExpression="ItemName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn DataField="IsParamedicFeeUsePercentage" HeaderText="Fee Using Percentage"
                            UniqueName="IsParamedicFeeUsePercentage" SortExpression="IsParamedicFeeUsePercentage">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridCheckBoxColumn>
                        <telerik:GridBoundColumn DataField="ParamedicFeeAmount" HeaderText="Fee Amount" UniqueName="ParamedicFeeAmount"
                            SortExpression="ParamedicFeeAmount" DataFormatString="{0:n2}">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ParamedicFeeAmountReferral" HeaderText="Fee Amount Referral"
                            UniqueName="ParamedicFeeAmountReferral" SortExpression="ParamedicFeeAmountReferral"
                            DataFormatString="{0:n2}">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn DataField="IsDeductionFeeUsePercentage" HeaderText="Deduction Using Percentage"
                            UniqueName="IsDeductionFeeUsePercentage" SortExpression="IsDeductionFeeUsePercentage">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridCheckBoxColumn>
                        <telerik:GridBoundColumn DataField="DeductionFeeAmount" HeaderText="Deduction Amount"
                            UniqueName="DeductionFeeAmount" SortExpression="DeductionFeeAmount" DataFormatString="{0:n2}">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DeductionFeeAmountReferral" HeaderText="Deduction Amount Referral"
                            UniqueName="DeductionFeeAmountReferral" SortExpression="DeductionFeeAmountReferral"
                            DataFormatString="{0:n2}">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                        <telerik:GridTemplateColumn UniqueName="processComp">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openItemComp('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"Setting Item - Component\" /></a>",
                                                                                                            DataBinder.Eval(Container.DataItem, "ItemID"))%>
                            </ItemTemplate>
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="process">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openItemGuarantor('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"Setting Item - Guarantor\" /></a>",
                                                                                                            DataBinder.Eval(Container.DataItem, "ItemID"))%>
                            </ItemTemplate>
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ParamedicFeeItemDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="grdParamedicFeeItemEditCommand">
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
        <telerik:RadPageView ID="pvFeeGuarCategory" runat="server">
            <telerik:RadGrid ID="grdFeeGuarantorCategory" runat="server" OnNeedDataSource="grdFeeGuarantorCategory_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdFeeGuarantorCategory_UpdateCommand"
                OnDeleteCommand="grdFeeGuarantorCategory_DeleteCommand" OnInsertCommand="grdFeeGuarantorCategory_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="ParamedicID,SRPhysicianFeeType">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="SRPhysicianFeeType" HeaderText="ID" UniqueName="SRPhysicianFeeType"
                            SortExpression="ItemID" Visible="False">
                            <HeaderStyle HorizontalAlign="Left" Width="90px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PhysicianFeeType" HeaderText="Guarantor Category"
                            UniqueName="PhysicianFeeType" SortExpression="PhysicianFeeType">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn DataField="IsParamedicFeeUsePercentage" HeaderText="Fee Using Percentage"
                            UniqueName="IsParamedicFeeUsePercentage" SortExpression="IsParamedicFeeUsePercentage">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridCheckBoxColumn>
                        <telerik:GridBoundColumn DataField="ParamedicFeeAmount" HeaderText="Fee Amount" UniqueName="ParamedicFeeAmount"
                            SortExpression="ParamedicFeeAmount" DataFormatString="{0:n2}">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ParamedicFeeAmountReferral" HeaderText="Fee Amount Referral"
                            UniqueName="ParamedicFeeAmountReferral" SortExpression="ParamedicFeeAmountReferral"
                            DataFormatString="{0:n2}">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn DataField="IsDeductionFeeUsePercentage" HeaderText="Deduction Using Percentage"
                            UniqueName="IsDeductionFeeUsePercentage" SortExpression="IsDeductionFeeUsePercentage">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridCheckBoxColumn>
                        <telerik:GridBoundColumn DataField="DeductionFeeAmount" HeaderText="Deduction Amount"
                            UniqueName="DeductionFeeAmount" SortExpression="DeductionFeeAmount" DataFormatString="{0:n2}">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DeductionFeeAmountReferral" HeaderText="Deduction Amount Referral"
                            UniqueName="DeductionFeeAmountReferral" SortExpression="DeductionFeeAmountReferral"
                            DataFormatString="{0:n2}">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                        <telerik:GridTemplateColumn UniqueName="process">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openGuarantorCategoryItem('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"Setting Guarantor Category - Item\" /></a>",
                                                                                                                                                DataBinder.Eval(Container.DataItem, "SRPhysicianFeeType"))%>
                            </ItemTemplate>
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ParamedicFeeGuarantorCategoryDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="grdParamedicFeeGuarantorCategoryEditCommand">
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
        <telerik:RadPageView runat="server" ID="pgvAutoBillItem">
            <telerik:RadGrid ID="grdServiceUnitAutoBillItem" runat="server" OnNeedDataSource="grdServiceUnitAutoBillItem_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdServiceUnitAutoBillItem_UpdateCommand"
                OnDeleteCommand="grdServiceUnitAutoBillItem_DeleteCommand" OnInsertCommand="grdServiceUnitAutoBillItem_InsertCommand"
                AllowPaging="true">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ParamedicID, ServiceUnitID, ItemID"
                    PageSize="15">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit Name"
                            UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="Quantity" HeaderText="Qty"
                            UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemUnit" HeaderText="Unit"
                            UniqueName="ItemUnit" SortExpression="ItemUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="90px" DataField="IsGenerateOnRegistration"
                            HeaderText="Generate On Registration" UniqueName="IsGenerateOnRegistration"
                            SortExpression="IsGenerateOnRegistration" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="90px" DataField="IsGenerateOnReferral"
                            HeaderText="Generate On Referral" UniqueName="IsGenerateOnReferral" SortExpression="IsGenerateOnReferral"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                            UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ParamedicAutoBillItemDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ParamedicAutoBillItemEditCommand">
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
        <telerik:RadPageView runat="server" ID="pgvServiceUnitParamedic">
            <telerik:RadGrid ID="grdServiceUnitParamedic" runat="server" OnNeedDataSource="grdServiceUnitParamedic_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdServiceUnitParamedic_UpdateCommand"
                OnDeleteCommand="grdServiceUnitParamedic_DeleteCommand" OnInsertCommand="grdServiceUnitParamedic_InsertCommand"
                AllowPaging="true">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ServiceUnitID, ParamedicID"
                    PageSize="15">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ServiceUnitID" HeaderText="Service Unit ID"
                            UniqueName="ServiceUnitID" SortExpression="ServiceUnitID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit Name"
                            UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="RoomName" HeaderText="Default Service Rooms"
                            UniqueName="RoomName" SortExpression="RoomName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsUsingQue" HeaderText="Using Que Slot"
                            UniqueName="IsUsingQue" SortExpression="IsUsingQue" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsAcceptBPJS" HeaderText="Accept BPJS"
                            UniqueName="IsAcceptBPJS" SortExpression="IsAcceptBPJS" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="120px" DataField="IsAcceptNonBPJS" HeaderText="Accept Non BPJS"
                            UniqueName="IsAcceptNonBPJS" SortExpression="IsAcceptNonBPJS" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn UniqueName="process">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openScheduleGlobal('{0}'); return false;\"><img src=\"../../../../Images/calendar16.png\" border=\"0\" title=\"Schedule Template\" /></a>",
                                                                                                                                                DataBinder.Eval(Container.DataItem, "ServiceUnitID"))%>
                            </ItemTemplate>
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ParamedicServiceUnitDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ParamedicServiceUnitEditCommand">
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
        <telerik:RadPageView runat="server" ID="pgvAliasName">
            <telerik:RadGrid ID="grdAliasName" runat="server" OnNeedDataSource="grdAliasName_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdAliasName_UpdateCommand"
                OnDeleteCommand="grdAliasName_DeleteCommand" OnInsertCommand="grdAliasName_InsertCommand"
                AllowPaging="true">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ParamedicID, SRBridgingType, BridgingID"
                    PageSize="15">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="BridgingTypeName" HeaderText="Bridging Type"
                            UniqueName="BridgingTypeName" SortExpression="BridgingTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="BridgingID" HeaderText="Bridging ID"
                            UniqueName="BridgingID" SortExpression="BridgingID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="BridgingName" HeaderText="Bridging Name" UniqueName="BridgingName"
                            SortExpression="BridgingName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                            UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ParamedicAliasDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ParamedicAliasEditCommand">
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
        <telerik:RadPageView runat="server" ID="pgvPhysicianType">
            <telerik:RadGrid ID="grdPhysicianOtherType" runat="server" OnNeedDataSource="grdPhysicianOtherType_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdPhysicianOtherType_DeleteCommand"
                OnInsertCommand="grdPhysicianOtherType_InsertCommand"
                AllowPaging="true">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="SRParamedicType"
                    PageSize="15">
                    <Columns>
                        <telerik:GridBoundColumn DataField="ParamedicTypeName" HeaderText="Physician Type"
                            UniqueName="ParamedicTypeName" SortExpression="ParamedicTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                        <telerik:GridTemplateColumn />
                    </Columns>
                    <EditFormSettings UserControlName="ParamedicOtherTypeDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ParamedicOtherTypeEditCommand">
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
        <telerik:RadPageView runat="server" ID="pgvPhysicianTeam">
            <telerik:RadGrid ID="grdPhysicianTeam" runat="server" OnNeedDataSource="grdPhysicianTeam_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdPhysicianTeam_DeleteCommand"
                OnInsertCommand="grdPhysicianTeam_InsertCommand" AllowPaging="true">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ParamedicID, ParamedicMemberID"
                    PageSize="15">
                    <Columns>
                        <telerik:GridBoundColumn DataField="ParamedicMemberName" HeaderText="Member"
                            UniqueName="ParamedicMemberName" SortExpression="ParamedicMemberName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="FeePercentage" HeaderText="Percentage"
                            UniqueName="FeePercentage" SortExpression="FeePercentage" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                        <telerik:GridTemplateColumn />
                    </Columns>
                    <EditFormSettings UserControlName="ParamedicFeeByTeamDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ParamedicTeamEditCommand">
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
    </telerik:RadMultiPage>
</asp:Content>
