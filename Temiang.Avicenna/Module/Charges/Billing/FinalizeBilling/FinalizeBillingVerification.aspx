<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="FinalizeBillingVerification.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Charges.FinalizeBillingVerification" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <style type="text/css">
            @keyframes blinkingText {
                0% {
                    opacity: 1;
                }

                40% {
                    opacity: 0;
                }

                60% {
                    opacity: 0;
                }

                100% {
                    opacity: 1;
                }

                100% {
                    opacity: 1;
                }

                100% {
                    opacity: 1;
                }
            }

            .blinking {
                animation: blinkingText 1.4s infinite;
            }
        </style>
        <script language="javascript" type="text/javascript">
            function UpdateStatusVerification(transNo, seqNo, unitID, locationNo) {
                var param = transNo + "|" + seqNo + "|" + unitID + "|" + locationNo;
                __doPostBack("<%= grdTransChargesItem.UniqueID %>", param);
            }

            function openWinProcess(type, regNo, transNo, seqNo, itemID, hold) {
                var grrID = $find("<%= cboGuarantorID.ClientID %>");

                var oWnd = $find("<%= winProcess.ClientID %>");
                oWnd.SetUrl("TariffComponent.aspx?regNo=" + regNo + "&grrID=" + grrID.get_value() + "&transNo=" + transNo + "&seqNo=" + seqNo + "&itemID=" + itemID + "&type=" + type + "&hold=" + hold + "&formId=2");
                oWnd.Show();
            }

            function openWinChargeClass(transNo) {
                var oWnd = $find("<%= winProcess.ClientID %>");
                oWnd.SetUrl("ChargeClass.aspx?transNo=" + transNo);
                oWnd.Show();
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument) {
                    if (oWnd.argument == 'rebind') {
                        __doPostBack("<%= grdTransChargesItem.UniqueID %>", 'rebind');
                        oWnd.argument = 'undefined';
                    }
                    if (oWnd.argument == 'payment') {
                        __doPostBack("<%= grdGuarantorReceipt.UniqueID %>", 'payment');
                        oWnd.argument = 'undefined';
                    }
                    if (oWnd.argument == 'paymentPersonalAr') {
                        __doPostBack("<%= grdGuarantorReceipt.UniqueID %>", 'paymentPersonalAr');
                        oWnd.argument = 'undefined';
                    }
                    if (oWnd.argument == 'voidpayment') {
                        __doPostBack("<%= grdGuarantorReceipt.UniqueID %>", 'voidpayment');
                        oWnd.argument = 'undefined';
                    }
                }
            }

            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();

                var tabStrip;
                var tab;
                if (val != 'list') {
                    tabStrip = $find("<%= RadTabStrip1.ClientID %>");
                    tab = tabStrip.findTabByText('Verified Transaction');
                }

                switch (val) {
                    case "list":
                        if ('<%= Request.QueryString["from"]%>' == '1')
                            location.replace('FinalizeBillingList.aspx');
                        else
                            location.replace('../../Billing/PatientFinancialControl/PatientFinancialControlList.aspx');
                        break;
                    case "lock":
                        if (confirm('Are you sure want to lock / unlock this registration?'))
                            __doPostBack("<%= grdCostCalculation.UniqueID %>", 'lock');
                        break;
                    case "process":
                        tab.select();
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'process');
                        break;
                    case "save":
                        if (confirm('Are you sure want to save billing and transaction process?'))
                            __doPostBack("<%= grdCostCalculation.UniqueID %>", 'save');
                        break;
                    case "printd":
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'printd');
                        break;
                    case "printd2_d":
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'printd2_d');
                        break;
                    case "printd2_d_en":
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'printd2_d_en');
                        break;
                    case "printd2_g":
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'printd2_g');
                        break;
                    case "printd2_g_en":
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'printd2_g_en');
                        break;
                    case "printre_g":
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'printre_g');
                        break;
                    case "printre_g_en":
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'printre_g_en');
                        break;
                    case "printdLabFarLog_g":
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'printdLabFarLog_g');
                        break;
                    case "printd2_p":
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'printd2_p');
                        break;
                    case "printd2_p_en":
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'printd2_p_en');
                        break;
                    case "printre_p":
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'printre_p');
                        break;
                    case "printre_p_en":
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'printre_p_en');
                        break;
                    case "printre_p_rkp":
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'printre_p_rkp');
                        break;
                    case "printBpjs":
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'printBpjs');
                        break;
                    case "printBpjs2":
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'printBpjs2');
                        break;
                    case "printdLabFarLog_p":
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'printdLabFarLog_p');
                        break;
                    case "printR":
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'printR');
                        break;
                    case "printOt":
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'printOt');
                        break;
                    case "printg":
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'printg');
                        break;
                    case "refresh":
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'rebind');
                        break;
                    case "closed":
                        if (confirm('Are you sure want to closed / open this registration?'))
                            __doPostBack("<%= grdCostCalculation.UniqueID %>", 'closed');
                        break;
                    case "adjust":
                        window.location = "../BillingAdjust/BillingAdjustDetail.aspx?<%= Request.QueryString.ToString() %>"; //regNo=&regType=&md=&from=";
                        break;
                    case "checkout":
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'checkout');
                        break;
                    case "printpatpermit":
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'printpatpermit');
                        break;
                    case "printpaymentpermit":
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'printpaymentpermit');
                        break;
                }
            }
            function openWinPayment(guarIdBuff, seqNo) {
                var oWnd = $find("<%= winPayment.ClientID %>");
                var oguar = $find("<%= cboGuarantorID.ClientID %>");
                oWnd.setUrl('GuarantorPaymentReceiveExcludeDiscount.aspx?regNo=' + '<%= Request.QueryString["regNo"] %>' + "&guarId=" + oguar.get_value() + "&guarIdBuff=" + guarIdBuff + "&seqNo=" + seqNo);
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }
            function openWinPersonalAr() {
                var oWnd = $find("<%= winPayment.ClientID %>");
                var oguar = $find("<%= cboGuarantorID.ClientID %>");
                oWnd.setUrl('PersonalPaymentReceive.aspx?regNo=' + '<%= Request.QueryString["regNo"] %>' + "&guarId=" + oguar.get_value());
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onVoidIntermBill(ib) {
                if (confirm('Are you sure to void this interim bill?')) {
                    __doPostBack("<%= grdIntermBill.UniqueID %>", 'voidib|' + ib);
                }
            }
            function onVoidPayment(py) {
                if (confirm('Are you sure to void this payment?')) {
                    __doPostBack("<%= grdGuarantorReceipt.UniqueID %>", 'voidpy|' + py);
                }
            }
            function onDeleteBuffer(guarId, regNo) {
                if (confirm('Are you sure to delete this buffer?')) {
                    __doPostBack("<%= grdBuffer.UniqueID %>", 'deletebuff|' + guarId + '|' + regNo);
                }
            }
            function openWinGuarantorDetail() {
                var oWnd = $find("<%= winGuarInfo.ClientID %>");
                oWnd.setUrl('GuarantorInfo.aspx?regNo=' + '<%= Request.QueryString["regNo"] %>');
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }
            function openWinPlafondDetail() {
                var oWnd = $find("<%= winGuarInfo.ClientID %>");
                oWnd.setUrl('PlafondInfo.aspx?regNo=' + '<%= Request.QueryString["regNo"] %>');
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }
            function openWinSplitBillInfo() {
                var oWnd = $find("<%= winGuarInfo.ClientID %>");
                oWnd.setUrl('PrescriptionSplitBillInfo.aspx?regNo=' + '<%= Request.QueryString["regNo"] %>');
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }
            function openWinPlafondCoverage() {
                var oWnd = $find("<%= winGuarInfo.ClientID %>");
                var obm = $find("<%= cboSRBusinessMethod.ClientID %>");
                if (obm.get_value() != "BusinessMethod-002") {
                    alert('Plafond Coverage only for Guarantor Method Plafond.');
                }
                else {
                    oWnd.setUrl('PlafondCoverageDetail.aspx?regNo=' + '<%= Request.QueryString["regNo"] %>');
                    oWnd.set_width(document.body.offsetWidth);
                    oWnd.show();
                }
            }
            function openWinGuarantorInfo() {
                var oWnd = $find("<%= winPayment.ClientID %>");
                var oguar = $find("<%= cboGuarantorID.ClientID %>");
                var lblToBeUpdate = "<%= lblGuarantorInfo.ClientID %>";
                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/Finance/Master/GuarantorInfo/GuarantorInfoDialog.aspx?id=' + oguar.get_value() + '&lblGuarantorInfo=' + lblToBeUpdate + '")%>');
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }
            function openWinBpjsPackage() {
                var oWnd = $find("<%= winBpjsPackage.ClientID %>");
                oWnd.setUrl('BpjsPackageItem.aspx?regNo=' + '<%= Request.QueryString["regNo"] %>' + "&formId=2");
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }
            function openVoidPayment(payno) {
                var oWnd = $find("<%= winVoid.ClientID %>");
                oWnd.setUrl('VoidGuarantorPaymentReceiveDialog.aspx?payno=' + payno);
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }
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
            function gotoAlertNonPatientCustomer(type) {
                if (type == "tr") {
                    alert('This is Non Patient Customer. Add service unit transaction not allowed from this link.');
                }
                else if (type == "ds") {
                    alert('This is Non Patient Customer. Add ancillary services not allowed from this link.');
                }
                else if (type == "cr") {
                    alert('This is Non Patient Customer. Add correction transaction not allowed from this link.');
                }
            }
            function gotoAlertPatientNotBeenCheckinConfirmed(type) {
                if (type == "tr") {
                    alert('This patient has not been check in confirmed. Add service unit transaction not allowed from this link.');
                }
                else if (type == "ds") {
                    alert('This patient has not been check in confirmed. Add ancillary services not allowed from this link.');
                }
                else if (type == "cr") {
                    alert('This patient has not been check in confirmed. Add correction transaction not allowed from this link.');
                }
            }
            function gotoAlertUserIsNotEditable() {
                alert('You do not have authorization to access this menu. Please contact Administrator.');
            }
            function gotoAddTransactionUrl(type) {
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var unit = $find("<%= txtServiceUnitID.ClientID %>");
                var par = $find("<%= txtParamedicID.ClientID %>");
                var url = '../../ServiceUnit/ServiceUnitTransaction/ServiceUnitTransactionDetail.aspx?md=new&type=' + type + '&regno=' + regNo.get_value() + '&pid=' + par.get_value() + '&cid=' + unit.get_value() + '&resp=0&disch=1&verif=1';
                window.location.href = url;
            }
            function gotoAddCorrectionUrl() {
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var unit = $find("<%= txtServiceUnitID.ClientID %>");
                var par = $find("<%= txtParamedicID.ClientID %>");
                var url = '../../ServiceUnit/ServiceUnitCorrection/ServiceUnitCorrectionDetail.aspx?md=new&regno=' + regNo.get_value() + '&pid=' + par.get_value() + '&cid=' + unit.get_value() + '&resp=0&disch=1&verif=1';
                window.location.href = url;
            }
            function openWinPatientTransferInfo() {
                var oWnd = $find("<%= winBpjsPackage.ClientID %>");
                oWnd.setUrl('PatientTransferInfo.aspx?regNo=' + '<%= Request.QueryString["regNo"] %>');
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }
            function openWinPaymentInfo() {
                var oWnd = $find("<%= winBpjsPackage.ClientID %>");
                oWnd.setUrl('../../Cashier/PaymentReceive/PaymentReceiveHistory.aspx?type=2&rno=' + '<%= Request.QueryString["regNo"] %>');
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }
            function openWinPhysicianTeamInfo() {
                var oWnd = $find("<%= winBpjsPackage.ClientID %>");
                oWnd.setUrl('PhysicianTeamInfo.aspx?regNo=' + '<%= Request.QueryString["regNo"] %>');
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }
            function openWinMergeBillingInfo() {
                var oWnd = $find("<%= winBpjsPackage.ClientID %>");
                oWnd.setUrl('MergeBillingInfo.aspx?regNo=' + '<%= Request.QueryString["regNo"] %>');
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }
            function openWinSurgeryCostPreview() {
                var oWnd = $find("<%= winCostSurgery.ClientID %>");
                oWnd.setUrl('SurgeryCostEstimationPreview.aspx?regNo=' + '<%= Request.QueryString["regNo"] %>');
                //oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }
            function onClientCloseCostSurgery(oWnd, args) {
                if (oWnd.argument && oWnd.argument.print != null) {
                    var oWnd = $find("<%= winPrint.ClientID %>");
                    oWnd.setUrl('<%=Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx")%>');
                    oWnd.show();
                }
            }
            function gotoAddPaymentReceivedUrl() {
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var url = '../../Cashier/PaymentReceive/PaymentReceiveDetail.aspx?md=new&regno=' + regNo.get_value() + '&pc=0&from=verif&rtype=' + '<%= Request.QueryString["regType"] %>' + '&utype=';
                window.location.href = url;
            }
            function openWinRegistrationInfoX() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationInfo.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("http://192.168.0.21/avc/print_bill.php?md=new&regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }
            function setCustomPosition(sender, args) {
                sender.moveTo(sender.get_left(), sender.get_top());
            }
            function CloseOpenFilterTransactionList() {
                __doPostBack("<%= grdCostCalculation.UniqueID %>", 'closeopenfiltertxlist');
            }
            function openWinPatient() {
                var oWnd = $find("<%= winPatientInfo.ClientID %>");
                var regType = "<%= Request.QueryString["regType"]%>";
                oWnd.setUrl("/Module/RADT/Registration/PatientDetail.aspx?md=edit&md2=edit&pid=<%= PatientID %>&pt=patient&rt=" + regType);
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboFilterByServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdCostCalculation" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboFilterByPaymentStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboFilterByIntermBillStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboFilterByItemType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdCostCalculation" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboFilterByCheckedStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdCostCalculation" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboFilterByItemGroupID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdCostCalculation" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterTransDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdCostCalculation" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkIsIncludePrescription">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCostCalculation" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboGuarantorID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboSRBusinessMethod" />
                    <telerik:AjaxUpdatedControl ControlID="txtPlafonValue" />
                    <telerik:AjaxUpdatedControl ControlID="txtPlavonChargeValue" />
                    <telerik:AjaxUpdatedControl ControlID="chkIsGlobalFlavon" />
                    <telerik:AjaxUpdatedControl ControlID="btnBpjsPackage" />
                    <telerik:AjaxUpdatedControl ControlID="hdnBpjsLabel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboGuarantorGroupID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboGuarantorID" />
                    <telerik:AjaxUpdatedControl ControlID="cboSRBusinessMethod" />
                    <telerik:AjaxUpdatedControl ControlID="txtPlafonValue" />
                    <telerik:AjaxUpdatedControl ControlID="txtPlavonChargeValue" />
                    <telerik:AjaxUpdatedControl ControlID="chkIsGlobalFlavon" />
                    <telerik:AjaxUpdatedControl ControlID="btnBpjsPackage" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboSRBusinessMethod">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtPlafonValue" />
                    <telerik:AjaxUpdatedControl ControlID="txtPlavonChargeValue" />
                    <telerik:AjaxUpdatedControl ControlID="chkIsGlobalFlavon" />
                    <telerik:AjaxUpdatedControl ControlID="btnBpjsPackage" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCalculateAdmin">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAdminValue" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnRegistrationRule">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistrationItemRule" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSaveGuarantor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboSRBusinessMethod" />
                    <telerik:AjaxUpdatedControl ControlID="txtPlafonValue" />
                    <telerik:AjaxUpdatedControl ControlID="txtPlavonChargeValue" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo2" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo3" />
                    <telerik:AjaxUpdatedControl ControlID="btnPaymentReceive" />
                    <telerik:AjaxUpdatedControl ControlID="btnPersonalAr" />
                    <telerik:AjaxUpdatedControl ControlID="chkIsGlobalFlavon" />
                    <telerik:AjaxUpdatedControl ControlID="btnBpjsPackage" />
                    <telerik:AjaxUpdatedControl ControlID="lblNeedRecalculation" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnRecalculated">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCostCalculation" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo2" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo3" />
                    <telerik:AjaxUpdatedControl ControlID="btnBpjsPackage" />
                    <telerik:AjaxUpdatedControl ControlID="btnPersonalAr" />
                    <telerik:AjaxUpdatedControl ControlID="btnPaymentReceive" />
                    <telerik:AjaxUpdatedControl ControlID="lblNeedRecalculation" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSRDiscountReason">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo2" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAddStamp">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo2" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo3" />
                    <telerik:AjaxUpdatedControl ControlID="grdCostCalculation" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnIntermBill">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCostCalculation" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdIntermBill" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo2" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo3" />
                    <telerik:AjaxUpdatedControl ControlID="txtAdminValue" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnProcessBillToClass">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo2" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnPrintBillToClass">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdIntermBill" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnAdmDisc">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdIntermBill" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnSaveToBuffer">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdBuffer" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo2" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnPrintPreviewVerified">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCostCalculation" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdBuffer">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdBuffer" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdTransChargesItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdIntermBill" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdCostCalculation" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdBuffer" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdGuarantorReceipt" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdAskesCovered" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtPlafonValue" />
                    <telerik:AjaxUpdatedControl ControlID="txtPlavonChargeValue" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rblToGuarantor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdIntermBill" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="rblToGuarantor" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdIntermBill">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdIntermBill" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="rblToGuarantor" />
                    <telerik:AjaxUpdatedControl ControlID="grdCostCalculation" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtAdminValue" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo2" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdRegistrationItemRule">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistrationItemRule" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnSaveDiscountRule">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCostCalculation" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo2" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSaveAdmin">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCostCalculation" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo2" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnSaveTariffCompDiscountRule">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCostCalculation" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo2" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnSavePlafondRule">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCostCalculation" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo2" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdAskesCovered">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAskesCovered" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtPlafonValue" />
                    <telerik:AjaxUpdatedControl ControlID="txtPlavonChargeValue" />
                    <telerik:AjaxUpdatedControl ControlID="grdIntermBill" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo2" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo3" />
                    <telerik:AjaxUpdatedControl ControlID="cboSRBusinessMethod" />
                    <telerik:AjaxUpdatedControl ControlID="chkIsGlobalFlavon" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnProcessChecked">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCostCalculation" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnSaveVerified">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCostCalculation" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo2" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnProcessPatientToGuarantor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCostCalculation" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo2" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnProcessGuarantorToPatient">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCostCalculation" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo2" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadToolBar2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdCostCalculation">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadToolBar2" />
                    <telerik:AjaxUpdatedControl ControlID="txtAdminValue" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdIntermBill" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdAskesCovered" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdGuarantorReceipt" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo2" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo3" />
                    <telerik:AjaxUpdatedControl ControlID="pnlTransactionFilter" />

                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdGuarantorReceipt">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdGuarantorReceipt" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdIntermBill" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdBuffer" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdAskesCovered" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCheckGrouper">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtPlafonValue" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="List" Value="list" ImageUrl="~/Images/Toolbar/details16.png"
                HoveredImageUrl="~/Images/Toolbar/details16_h.png" DisabledImageUrl="~/Images/Toolbar/details16_d.png" />
            <telerik:RadToolBarButton IsSeparator="True" runat="server" />
            <telerik:RadToolBarButton runat="server" Text="Lock" Value="lock" ImageUrl="~/Images/Toolbar/lock16.png"
                HoveredImageUrl="~/Images/Toolbar/lock16_h.png" DisabledImageUrl="~/Images/Toolbar/lock16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Process" Value="process" ImageUrl="~/Images/Toolbar/process16.png"
                HoveredImageUrl="~/Images/Toolbar/process16_h.png" DisabledImageUrl="~/Images/Toolbar/process16_d.png"
                Visible="False" />
            <telerik:RadToolBarButton runat="server" Text="Save" Value="save" ImageUrl="~/Images/Toolbar/save16.png"
                HoveredImageUrl="~/Images/Toolbar/save16_h.png" DisabledImageUrl="~/Images/Toolbar/save16_d.png"
                Visible="False" />
            <telerik:RadToolBarButton IsSeparator="True" runat="server" />
            <telerik:RadToolBarButton runat="server" Text="Print (Detail)" Value="printd" ImageUrl="~/Images/Toolbar/print16.png"
                HoveredImageUrl="~/Images/Toolbar/print16_h.png" DisabledImageUrl="~/Images/Toolbar/print16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Print (Detail 2)" Value="printd2_g"
                ImageUrl="~/Images/Toolbar/print16.png" HoveredImageUrl="~/Images/Toolbar/print16_h.png"
                DisabledImageUrl="~/Images/Toolbar/print16_d.png" />
            <telerik:RadToolBarDropDown runat="server" Text="Guarantor & Patient" ImageUrl="~/Images/Toolbar/print16.png"
                HoveredImageUrl="~/Images/Toolbar/print16_h.png" DisabledImageUrl="~/Images/Toolbar/print16_d.png">
                <Buttons>
                    <telerik:RadToolBarButton runat="server" Text="Print Detail" Value="printd2_d" ImageUrl="~/Images/Toolbar/print16.png"
                        HoveredImageUrl="~/Images/Toolbar/print16_h.png" DisabledImageUrl="~/Images/Toolbar/print16_d.png">
                    </telerik:RadToolBarButton>
                    <telerik:RadToolBarButton runat="server" Text="Print Group" Value="printd2_g" ImageUrl="~/Images/Toolbar/print16.png"
                        HoveredImageUrl="~/Images/Toolbar/print16_h.png" DisabledImageUrl="~/Images/Toolbar/print16_d.png">
                    </telerik:RadToolBarButton>
                    <telerik:RadToolBarButton runat="server" Text="Print Rekap" Value="printre_g" ImageUrl="~/Images/Toolbar/print16.png"
                        HoveredImageUrl="~/Images/Toolbar/print16_h.png" DisabledImageUrl="~/Images/Toolbar/print16_d.png">
                    </telerik:RadToolBarButton>
                    <telerik:RadToolBarButton runat="server" Text="Print Rincian Lab, Item Medic, Item Non Medic"
                        Value="printdLabFarLog_g" ImageUrl="~/Images/Toolbar/print16.png" HoveredImageUrl="~/Images/Toolbar/print16_h.png"
                        DisabledImageUrl="~/Images/Toolbar/print16_d.png">
                    </telerik:RadToolBarButton>

                    <telerik:RadToolBarButton runat="server" Text="Print Detail - EN" Value="printd2_d_en" ImageUrl="~/Images/Toolbar/print16.png"
                        HoveredImageUrl="~/Images/Toolbar/print16_h.png" DisabledImageUrl="~/Images/Toolbar/print16_d.png">
                    </telerik:RadToolBarButton>
                    <telerik:RadToolBarButton runat="server" Text="Print Group - EN" Value="printd2_g_en" ImageUrl="~/Images/Toolbar/print16.png"
                        HoveredImageUrl="~/Images/Toolbar/print16_h.png" DisabledImageUrl="~/Images/Toolbar/print16_d.png">
                    </telerik:RadToolBarButton>
                    <telerik:RadToolBarButton runat="server" Text="Print Rekap - EN" Value="printre_g_en" ImageUrl="~/Images/Toolbar/print16.png"
                        HoveredImageUrl="~/Images/Toolbar/print16_h.png" DisabledImageUrl="~/Images/Toolbar/print16_d.png">
                    </telerik:RadToolBarButton>
                    <telerik:RadToolBarButton runat="server" Text="Print Rekap 2" Value="printre_p_rkp" ImageUrl="~/Images/Toolbar/print16.png"
                        HoveredImageUrl="~/Images/Toolbar/print16_h.png" DisabledImageUrl="~/Images/Toolbar/print16_d.png">
                    </telerik:RadToolBarButton>
                </Buttons>
            </telerik:RadToolBarDropDown>
            <telerik:RadToolBarDropDown runat="server" Text="Patient" ImageUrl="~/Images/Toolbar/print16.png"
                HoveredImageUrl="~/Images/Toolbar/print16_h.png" DisabledImageUrl="~/Images/Toolbar/print16_d.png">
                <Buttons>
                    <telerik:RadToolBarButton runat="server" Text="Print Detail" Value="printd2_p" ImageUrl="~/Images/Toolbar/print16.png"
                        HoveredImageUrl="~/Images/Toolbar/print16_h.png" DisabledImageUrl="~/Images/Toolbar/print16_d.png">
                    </telerik:RadToolBarButton>
                    <telerik:RadToolBarButton runat="server" Text="Print Rekap" Value="printre_p" ImageUrl="~/Images/Toolbar/print16.png"
                        HoveredImageUrl="~/Images/Toolbar/print16_h.png" DisabledImageUrl="~/Images/Toolbar/print16_d.png">
                    </telerik:RadToolBarButton>
                    <telerik:RadToolBarButton runat="server" Text="Print Rincian Lab, Item Medic, Item Non Medic"
                        Value="printdLabFarLog_p" ImageUrl="~/Images/Toolbar/print16.png" HoveredImageUrl="~/Images/Toolbar/print16_h.png"
                        DisabledImageUrl="~/Images/Toolbar/print16_d.png">
                    </telerik:RadToolBarButton>

                    <telerik:RadToolBarButton runat="server" Text="Print Detail - EN" Value="printd2_p_en" ImageUrl="~/Images/Toolbar/print16.png"
                        HoveredImageUrl="~/Images/Toolbar/print16_h.png" DisabledImageUrl="~/Images/Toolbar/print16_d.png">
                    </telerik:RadToolBarButton>
                    <telerik:RadToolBarButton runat="server" Text="Print Rekap - EN" Value="printre_p_en" ImageUrl="~/Images/Toolbar/print16.png"
                        HoveredImageUrl="~/Images/Toolbar/print16_h.png" DisabledImageUrl="~/Images/Toolbar/print16_d.png">
                    </telerik:RadToolBarButton>
                </Buttons>
            </telerik:RadToolBarDropDown>
            <telerik:RadToolBarDropDown runat="server" Text="INACBG" ImageUrl="~/Images/Toolbar/print16.png"
                HoveredImageUrl="~/Images/Toolbar/print16_h.png" DisabledImageUrl="~/Images/Toolbar/print16_d.png">
                <Buttons>
                    <telerik:RadToolBarButton runat="server" Text="Print" Value="printBpjs" ImageUrl="~/Images/Toolbar/print16.png"
                        HoveredImageUrl="~/Images/Toolbar/print16_h.png" DisabledImageUrl="~/Images/Toolbar/print16_d.png" />
                    <telerik:RadToolBarButton runat="server" Text="Print (With Price)" Value="printBpjs2"
                        ImageUrl="~/Images/Toolbar/print16.png" HoveredImageUrl="~/Images/Toolbar/print16_h.png"
                        DisabledImageUrl="~/Images/Toolbar/print16_d.png">
                    </telerik:RadToolBarButton>
                </Buttons>
            </telerik:RadToolBarDropDown>
            <telerik:RadToolBarButton runat="server" Text="Print (Prescription)" Value="printR"
                ImageUrl="~/Images/Toolbar/print16.png" HoveredImageUrl="~/Images/Toolbar/print16_h.png"
                DisabledImageUrl="~/Images/Toolbar/print16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Print (Deposit Statement)" Value="printg"
                ImageUrl="~/Images/Toolbar/print16.png" HoveredImageUrl="~/Images/Toolbar/print16_h.png"
                DisabledImageUrl="~/Images/Toolbar/print16_d.png" />
            <telerik:RadToolBarButton IsSeparator="True" runat="server" />
            <telerik:RadToolBarButton runat="server" Text="Refresh" Value="refresh" ImageUrl="~/Images/Toolbar/refresh16.png"
                HoveredImageUrl="~/Images/Toolbar/refresh16_h.png" DisabledImageUrl="~/Images/Toolbar/refresh16_d.png" />
            <telerik:RadToolBarButton IsSeparator="True" runat="server" />
            <telerik:RadToolBarButton runat="server" Text="Closed" Value="closed" ImageUrl="~/Images/Toolbar/lock16.png"
                HoveredImageUrl="~/Images/Toolbar/lock16_h.png" DisabledImageUrl="~/Images/Toolbar/lock16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Internal Adjust" Value="adjust" ImageUrl="~/Images/Toolbar/process16.png"
                HoveredImageUrl="~/Images/Toolbar/process16.png" DisabledImageUrl="~/Images/Toolbar/process16.png" />
            <telerik:RadToolBarButton runat="server" Text="Checkout" Value="checkout" ImageUrl="~/Images/RpY.png"
                HoveredImageUrl="~/Images/RpY.png" DisabledImageUrl="~/Images/RpY.png" />
            <telerik:RadToolBarButton runat="server" Text="Discharge Patient Permit" Value="printpatpermit" ImageUrl="~/Images/Toolbar/print16.png"
                HoveredImageUrl="~/Images/Toolbar/print16_h.png" DisabledImageUrl="~/Images/Toolbar/print16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Payment Permit" Value="printpaymentpermit" ImageUrl="~/Images/Toolbar/print16.png"
                HoveredImageUrl="~/Images/Toolbar/print16_h.png" DisabledImageUrl="~/Images/Toolbar/print16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Print (Surgical Billing)" Value="printOt"
                ImageUrl="~/Images/Toolbar/print16.png" HoveredImageUrl="~/Images/Toolbar/print16_h.png"
                DisabledImageUrl="~/Images/Toolbar/print16_d.png" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false">
    </telerik:RadAjaxPanel>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winCostSurgery" Animation="None" Width="800px" Height="400px"
        runat="server" Behavior="Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="True" Modal="true" OnClientClose="onClientCloseCostSurgery" Title="Cost Surgery Estimation">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winProcess" Animation="None" Width="1000px" Height="500px"
        Top="10px" runat="server" Behavior="Maximize,Close,Move" ShowContentDuringLoad="false"
        VisibleStatusbar="false" Modal="true" OnClientClose="onClientClose" OnClientShow="setCustomPosition">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winPayment" Animation="None" Width="1000px" Height="500px"
        runat="server" Behavior="Maximize,Close,Move" ShowContentDuringLoad="False" VisibleStatusbar="False"
        Modal="true" OnClientClose="onClientClose">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winGuarInfo" Animation="None" Width="800px" Height="400px"
        runat="server" Behavior="Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true" OnClientClose="onClientClose">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winBpjsPackage" Animation="None" Width="800px" Height="600px"
        runat="server" Behavior="Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true" OnClientClose="onClientClose">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winVoid" Animation="None" Width="700px" Height="300px" runat="server"
        Behavior="Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true" OnClientClose="onClientClose">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadWindow runat="server" Animation="None" Width="500px" Height="400px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" Title="Document Checklist"
        ID="winDocsOption">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winPatientInfo" Animation="None" Width="800px" Height="600px"
        runat="server" Behavior="Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true" OnClientClose="onClientClose">
    </telerik:RadWindow>
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
                    <asp:Image ID="Image2" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo2" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlInfo3" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
            <table width="100%">
                <tr>
                    <td width="10px" valign="top">
                        <asp:Image ID="Image4" ImageUrl="~/Images/boundleft.gif" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="lblInfo3" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    <asp:HiddenField runat="server" ID="hdnBpjsLabel" />
    <asp:HiddenField runat="server" ID="hdnPatientID" />
    <asp:HiddenField runat="server" ID="hdnRegType" />
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Patient Information">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 50%; vertical-align: top;">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20"
                                                ReadOnly="True" />
                                        </td>
                                        <td>
                                            <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();" class="noti_Container">
                                                <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo"
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
                                <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="300px" MaxLength="15"
                                                ReadOnly="True" />
                                        </td>
                                        <td>
                                            <a href="javascript:void(0);" onclick="javascript:openWinQuestionFormCheckList();"
                                                class="noti2_Container">
                                                <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo2" AssociatedControlID="txtRegistrationNo"
                                                    Text=""></asp:Label>&nbsp; </a>
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td>
                                            <asp:ImageButton ID="btnPatient" runat="server" ImageUrl="../../../../Images/Toolbar/edit16.png"
                                            CausesValidation="False" OnClientClick="openWinPatient();return false;"
                                            ToolTip="Edit Patient" />
                                        </td>
                                    </tr>
                                </table>
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
                                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="238px" ReadOnly="true" />
                                        </td>
                                        <td style="width: 3px"></td>
                                        <td>
                                            <telerik:RadTextBox ID="txtGender" runat="server" Width="29px" ReadOnly="true" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr style="display: none">
                            <td class="label">
                                <asp:Label ID="lblSex" runat="server" Text="Gender"></asp:Label>
                            </td>
                            <td class="entry">
                                <asp:RadioButton ID="optSexMale" runat="server" Text="Male" GroupName="Sex" />
                                <asp:RadioButton ID="optSexFemale" runat="server" Text="Female" GroupName="Sex" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPlaceDOB" runat="server" Text="City / Date Of Birth"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="300px" MaxLength="20"
                                    ReadOnly="true" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtAgeYear" runat="server" Width="50px" ReadOnly="True">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                                &nbsp;Y&nbsp;
                                <telerik:RadNumericTextBox ID="txtAgeMonth" runat="server" Width="50px" ReadOnly="True">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                                &nbsp;M&nbsp;
                                <telerik:RadNumericTextBox ID="txtAgeDay" runat="server" Width="50px" ReadOnly="True">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                                &nbsp;D
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%; vertical-align: top;">
                    <table width="100%">
                        <tr style="display: none">
                            <td class="label">
                                <asp:Label ID="lblDepartmentID" runat="server" Text="Department"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtDepartmentID" runat="server" Width="100px" MaxLength="10"
                                    ReadOnly="True" />
                                &nbsp;
                                <asp:Label ID="lblDepartmentName" runat="server" CssClass="labeldescription"></asp:Label>
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                            </td>
                            <td class="entry" colspan="3">
                                <telerik:RadTextBox ID="txtParamedicID" runat="server" Width="100px" MaxLength="10"
                                    ReadOnly="True" />
                                &nbsp;
                                <asp:Label ID="lblParamedicName" runat="server" CssClass="labeldescription"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry" colspan="3">
                                <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100px" MaxLength="10"
                                    ReadOnly="True" />
                                &nbsp;
                                <asp:Label ID="lblServiceUnitName" runat="server" CssClass="labeldescription"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRoomID" runat="server" Text="Room & Bed No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtRoomID" runat="server" Width="155px" ReadOnly="True" />
                                <telerik:RadTextBox ID="txtBedID" runat="server" Width="143px" ReadOnly="True" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblChargeClass" runat="server" Text="Charge Class"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtChargeClassID" runat="server" Width="100px" MaxLength="10"
                                    ReadOnly="True" />
                                &nbsp;
                                <asp:Label ID="lblChargeClassName" runat="server" CssClass="labeldescription"></asp:Label>
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegistrationDate" runat="server" Text="Registration Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 103px">
                                            <telerik:RadDatePicker ID="txtRegistrationDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                                DatePopupButton-Enabled="false">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td>
                                            <telerik:RadMaskedTextBox ID="txtRegistrationTime" runat="server" Mask="<00..23>:<00..59>"
                                                PromptChar="_" RoundNumericRanges="false" Width="50px">
                                            </telerik:RadMaskedTextBox>
                                        </td>

                                    </tr>
                                </table>
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <asp:Panel runat="server" ID="pnlForInpatient">
                            <tr id="trPatientInType" runat="server">
                                <td class="label">
                                    <asp:Label ID="lblPatientInType" runat="server" Text="Patient In Type"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadComboBox ID="cboSRPatientInType" runat="server" Width="300px" Enabled="False" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr id="DischargePlanContent" runat="server">
                                <td class="label">
                                    <asp:Label ID="lblDischargePlanDate" runat="server" Text="Discharge Plan Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtDischargePlanDate" runat="server" Width="100px" Enabled="false" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblLengthOfStay" runat="server" Text="Length Of Stay"></asp:Label>
                                                <telerik:RadNumericTextBox ID="txtLengthOfStay" runat="server" Width="50px" ReadOnly="True">
                                                    <NumberFormat DecimalDigits="0" />
                                                </telerik:RadNumericTextBox>
                                                &nbsp;Day(s)
                                            </td>
                                        </tr>
                                    </table>

                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </asp:Panel>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table style="width: 100%" cellpadding="0" cellspacing="5">
                    <tr>
                        <td>
                            <fieldset>
                                <legend>
                                    <asp:LinkButton ID="lbtnCloseOpenFilterTransactionList" runat="server" OnClientClick="CloseOpenFilterTransactionList();return false;">
                                        <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/cpUpDownArrow.png" />
                                        &nbsp;<asp:Label runat="server" ID="Label13" Text="TRANSACTION LIST FILTER"
                                            Font-Bold="True" ForeColor="Blue" Font-Size="12px"></asp:Label>
                                    </asp:LinkButton>
                                </legend>
                                <asp:Panel runat="server" ID="pnlTransactionFilter">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="4">
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="label" style="width: 25%">
                                                            <asp:Label ID="lblFilterByServiceUnitID" runat="server" Text="By Service Unit"></asp:Label>
                                                        </td>
                                                        <td style="width: 1%"></td>
                                                        <td class="label" style="width: 24%">
                                                            <asp:Label ID="lblFilterByPaymentStatus" runat="server" Text="By Payment Status"></asp:Label>
                                                        </td>
                                                        <td style="width: 1%"></td>
                                                        <td class="label" style="width: 24%">
                                                            <asp:Label ID="lblFilterByIntermBillStatus" runat="server" Text="By Interim Bill Status"></asp:Label>
                                                        </td>
                                                        <td style="width: 1%"></td>
                                                        <td class="label" style="width: 24%">
                                                            <asp:Label ID="lblFilterByItemType" runat="server" Text="By Item Type"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table width="100%" cellpadding="1" cellspacing="0">
                                                    <tr>
                                                        <td class="entry" style="width: 25%">
                                                            <telerik:RadComboBox ID="cboFilterByServiceUnitID" runat="server" AutoPostBack="true"
                                                                OnSelectedIndexChanged="cboFilterByServiceUnitID_SelectedIndexChanged" Width="100%">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td style="width: 1%"></td>
                                                        <td class="entry" style="width: 24%">
                                                            <telerik:RadComboBox ID="cboFilterByPaymentStatus" runat="server" AutoPostBack="true"
                                                                OnSelectedIndexChanged="cboFilterByPaymentStatus_SelectedIndexChanged" Width="100%">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td style="width: 1%"></td>
                                                        <td class="entry" style="width: 24%">
                                                            <telerik:RadComboBox ID="cboFilterByIntermBillStatus" runat="server" AutoPostBack="true"
                                                                OnSelectedIndexChanged="cboFilterByIntermBillStatus_SelectedIndexChanged" Width="100%">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td style="width: 1%"></td>
                                                        <td class="entry" style="width: 24%">
                                                            <telerik:RadComboBox ID="cboFilterByItemType" runat="server" AutoPostBack="true"
                                                                OnSelectedIndexChanged="cboFilterByItemType_SelectedIndexChanged" Width="100%">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table width="100%" cellpadding="1" cellspacing="0">
                                                    <tr>
                                                        <td class="label" style="width: 25%" runat="server" id="tdLblFilterByCheckedStatusYes"
                                                            visible="False">
                                                            <asp:Label ID="lblFilterByCheckedStatus" runat="server" Text="By Checked Status"></asp:Label>
                                                        </td>
                                                        <td style="width: 25%" runat="server" id="tdLblFilterByCheckedStatusNo"></td>
                                                        <td style="width: 1%"></td>
                                                        <td class="label" style="width: 49%" runat="server" id="tdLblFilterByItemGroupIdYes"
                                                            visible="False">
                                                            <asp:Label ID="lblFilterByItemGroupID" runat="server" Text="By Item Group"></asp:Label>
                                                        </td>
                                                        <td style="width: 49%" runat="server" id="tdLblFilterByItemGroupIdNo"></td>
                                                        <td style="width: 1%"></td>
                                                        <td class="entry" style="width: 24%">
                                                            <asp:CheckBox ID="chkIsIncludePrescription" runat="server" Text="Include Prescription"
                                                                Checked="True" AutoPostBack="true" OnCheckedChanged="chkIsIncludePrescription_CheckedChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table width="100%" cellpadding="1" cellspacing="0">
                                                    <tr>
                                                        <td class="entry" style="width: 25%" runat="server" id="tdCboFilterByCheckedStatusYes"
                                                            visible="False">
                                                            <telerik:RadComboBox ID="cboFilterByCheckedStatus" runat="server" AutoPostBack="true"
                                                                OnSelectedIndexChanged="cboFilterByCheckedStatus_SelectedIndexChanged" Width="100%">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td class="entry" style="width: 25%" runat="server" id="tdCboFilterByCheckedStatusNo"></td>
                                                        <td style="width: 1%"></td>
                                                        <td class="entry" style="width: 49%" runat="server" id="tdCboFilterByItemGroupIdYes"
                                                            visible="False">
                                                            <telerik:RadComboBox ID="cboFilterByItemGroupID" runat="server" AutoPostBack="true"
                                                                OnSelectedIndexChanged="cboFilterByItemGroupID_SelectedIndexChanged" Width="100%">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td class="entry" style="width: 49%" runat="server" id="tdCboFilterByItemGroupIdNo"></td>
                                                        <td style="width: 1%"></td>
                                                        <td class="entry" style="width: 24%"></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%">
                                                <table width="100%">
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblTransDate" runat="server" Text="Transaction Date"></asp:Label>
                                                        </td>
                                                        <td class="entry">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <telerik:RadDatePicker ID="txtTransDate1" runat="server" Width="100px" />
                                                                    </td>
                                                                    <td>&nbsp;to&nbsp;&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadDatePicker ID="txtTransDate2" runat="server" Width="100px" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td width="20">
                                                            <asp:ImageButton ID="btnFilterTransDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                                                OnClick="btnFilterTransDate_Click" ToolTip="Filter" />
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 50%"></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </fieldset>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
                                <%=PlafondStatus()%>
                                <%--                                <%=hdnBpjsLabel.Value != "bpjs"? string.Empty: string.Format(@"<table cellpadding='1' cellspacing='1' style='width:100%;border:1px; color:white;'>
    <tr align='center' style='background:gray;'><td style='width:150px;'>Plafond</td><td>Plafond Status</td><td style='width:154px;'>Guarantor + Remaining Patient</td></tr>
    <tr style='font-weight:bold;'>
        <td style='background:gray;' align='center'>{0:n2}</td>
        <td>
            <table cellpadding='0' cellspacing='0' style='width:100%'>
                <tr align='center'>
                    <td id='usedplafond' style='background:{4};width:{2}%;'>{3:n2}%{6}</td>
                    <td id='remaining' style='background:black;'/>
                </tr>
            </table>                
            <table cellpadding='0' cellspacing='0' style='width:100%'>                
                <tr align='center'>
                    <td style='background:gray;width:{5}%;'>100% ({0:n2})</td>
                    <td style='background:black;'></td>
                </tr>                                                        
            </table>
                                                
        </td>
        <td style='background:gray;' align='center'>{1:n2}</td>
    </tr>
</table>",
            TotalPlafond,
                                                     TotalGuarantorAndRemainingPatientAmount,
             PlafondValueUsedInPercent() > 100 ? 100 : PlafondValueUsedInPercent(),
             PlafondValueUsedInPercent(),
             PlafondValueUsedInPercent() > 100 ? "red" : PlafondValueUsedInPercent() > 75 ? "yellow":"green",
             PlafondValueUsedInPercent() < 100 ? 100 : 100 / (PlafondValueUsedInPercent() / (decimal)100),
                                                     PlafondValueUsedInPercent() > 100 ? string.Format(" ({0:n2})", TotalGuarantorAndRemainingPatientAmount - TotalPlafond) : string.Empty                                     
         )%>--%>
                            </telerik:RadCodeBlock>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;&nbsp;<asp:Label ID="lblHasMergeTo" runat="server" Text="" ForeColor="Red" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;&nbsp;<asp:Label ID="lblNeedRecalculation" runat="server" Text="" ForeColor="Red" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server" id="trSplitBillInfo">
                        <td colspan="4">&nbsp;&nbsp;<asp:Label ID="lblSplitBillInfo" runat="server" Text="" ForeColor="Red" Font-Size="Small"></asp:Label>
                            &nbsp;
                            <asp:ImageButton ID="imgSplitBillInfo" runat="server" ImageUrl="../../../../Images/Toolbar/details16.png"
                                CausesValidation="False" OnClientClick="openWinSplitBillInfo();return false;" ToolTip="Separate Prescription Bill List" />

                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr runat="server" id="trGuarantorHeader" visible="False">
                        <td class="label">
                            <asp:Label ID="lblGuarantorGroup" runat="server" Text="Guarantor Group"></asp:Label>
                        </td>
                        <td class="entry2Column">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboGuarantorGroupID" runat="server" Width="300px" HighlightTemplatedItems="True"
                                            AutoPostBack="True" MarkFirstMatch="False" EnableLoadOnDemand="true" NoWrap="False"
                                            OnItemDataBound="cboGuarantorID_ItemDataBound" OnItemsRequested="cboGuarantorGroupID_ItemsRequested"
                                            OnSelectedIndexChanged="cboGuarantorGroupID_SelectedIndexChanged">
                                            <FooterTemplate>
                                                Note : Show max 30 result
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 60px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                        </td>
                        <td class="entry">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboGuarantorID" runat="server" Width="300px" AutoPostBack="True"
                                            EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                                            OnItemDataBound="cboGuarantorID_ItemDataBound" OnItemsRequested="cboGuarantorID_ItemsRequested"
                                            OnSelectedIndexChanged="cboGuarantorID_SelectedIndexChanged">
                                            <FooterTemplate>
                                                Note : Show max 30 result
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td style="width: 35px">
                                        <asp:ImageButton ID="btnSaveGuarantor" runat="server" ImageUrl="../../../../Images/Toolbar/save16.png"
                                            CausesValidation="False" OnClick="btnSaveGuarantor_Click" ToolTip="Save Guarantor" />
                                    </td>
                                    <td style="width: 35px">
                                        <asp:ImageButton ID="btnGuarantorInfo" runat="server" ImageUrl="../../../../Images/Toolbar/details16.png"
                                            CausesValidation="False" OnClientClick="openWinGuarantorDetail();return false;"
                                            ToolTip="Guarantor Detail & History Update" />
                                    </td>
                                    <td>&nbsp; <a href="javascript:void(0);" onclick="javascript:openWinGuarantorInfo();"
                                        class="noti_Container" title="Guarantor Info">
                                        <asp:Label CssClass="noti_bubble" runat="server" ID="lblGuarantorInfo" AssociatedControlID="cboGuarantorID"
                                            Text=""></asp:Label>&nbsp;</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvGuarantorID" runat="server" ErrorMessage="Guarantor required."
                                ValidationGroup="entry" ControlToValidate="cboGuarantorID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <asp:Panel ID="pnlEmployeeInfo" runat="server">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblEmployeeID" runat="server" Text="Employee ID"></asp:Label>
                            </td>
                            <td class="entry2Column">
                                <telerik:RadComboBox ID="cboEmployeeID" runat="server" Width="300px" AutoPostBack="False"
                                    EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="True"
                                    OnItemDataBound="cboEmployeeID_ItemDataBound" OnItemsRequested="cboEmployeeID_ItemsRequested">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber") %>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "FirstName")%>
                                        &nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "MiddleName")%>
                                        &nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "LastName")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 15 result
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblGuarSRRelationship" runat="server" Text="Relation"></asp:Label>
                            </td>
                            <td class="entry2Column">
                                <telerik:RadComboBox ID="cboGuarSRRelationship" runat="server" Width="300px" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBusinessMethod" runat="server" Text="Guarantor Method"></asp:Label>
                        </td>
                        <td class="entry">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboSRBusinessMethod" runat="server" Width="300px" AutoPostBack="true"
                                            OnSelectedIndexChanged="cboSRBusinessMethod_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td style="width: 35px">
                                        <asp:ImageButton ID="btnBpjsPackage" runat="server" Visible="False" ImageUrl="../../../../Images/Toolbar/details16.png"
                                            CausesValidation="False" OnClientClick="openWinBpjsPackage();return false;" ToolTip="BPJS Package List" />
                                    </td>
                                    <td style="width: 35px"></td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPlafond" runat="server" Text="Plafond Coverage Amount"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtPlafonValue" runat="server" Width="145px" Value="0" />
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnPlafondCoverage" runat="server" ImageUrl="../../../../Images/Toolbar/details16.png"
                                            CausesValidation="False" OnClientClick="openWinPlafondCoverage();return false;"
                                            ToolTip="Plafond Coverage" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnCheckGrouper" runat="server" ImageUrl="~/Images/Toolbar/refresh16.png"
                                            CausesValidation="False" OnClick="btnCheckGrouper_Click" ToolTip="Check Grouper" />
                                    </td>
                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnPlafondHistory" runat="server" ImageUrl="../../../../Images/infoblue16.png"
                                            CausesValidation="False" OnClientClick="openWinPlafondDetail();return false;"
                                            ToolTip="Plafond Update History" />
                                    </td>
                                    <td>
                                        <asp:CheckBox runat="server" ID="chkIsGlobalFlavon" Text="Global Plafond" Enabled="False"
                                            Width="156px" Visible="False" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">Plafond Charge
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtPlavonChargeValue" runat="server" Width="145px"
                                            Value="0" />
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnPlafondCoverageXx" runat="server" ImageUrl="../../../../Images/Toolbar/details16.png"
                                            CausesValidation="False" OnClientClick="openWinPlafondCoverage();return false;"
                                            ToolTip="Plafond Coverage" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCoverageClassID" runat="server" Text="Coverage Class"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboCoverageClassID" runat="server" Width="300px" Enabled="false" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvCoverageClassID" runat="server" ErrorMessage="Coverage Class required."
                                ValidationGroup="entry" ControlToValidate="cboCoverageClassID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr id="pnlProcedureClass" runat="server">
                        <td class="label">
                            <asp:Label ID="lblProcedureClassID" runat="server" Text="Procedure Charge Class"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboProcedureClassID" runat="server" Width="300px" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr id="pnlCalculateAdmin" runat="server">
                        <td class="label">
                            <asp:Label ID="lblAdminValue" runat="server" Text="Administration Amount"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtAdminValue" runat="server" Width="145px" Value="0" />
                                    </td>
                                    <td>&nbsp;
                                        <asp:ImageButton ID="btnCalculateAdmin" runat="server" ImageUrl="../../../../Images/Toolbar/process16.png"
                                            CausesValidation="False" OnClick="btnCalculateAdmin_Click" ToolTip="Administration Charge Calculation" />
                                    </td>
                                    <td style="display: none">&nbsp;
                                        <asp:ImageButton ID="btnSaveAdmin" runat="server" ImageUrl="../../../../Images/Toolbar/save16.png"
                                            CausesValidation="False" OnClick="btnSaveAdmin_Click" ToolTip="Save Administration" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr id="trSRDiscountReason" runat="server" visible="False">
                        <td class="label">
                            <asp:Label ID="lblSRDiscountReason" runat="server" Text="Discount Category"></asp:Label>
                        </td>
                        <td class="entry">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboSRDiscountReason" runat="server" Width="300px">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td style="width: 35px">
                                        <asp:ImageButton ID="btnSRDiscountReason" runat="server" ImageUrl="../../../../Images/Toolbar/save16.png"
                                            CausesValidation="False" OnClick="btnSRDiscountReason_Click" ToolTip="Save Discount Category" />
                                    </td>
                                    <td style="width: 35px"></td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblItemMateraiID" runat="server" Text="Stamp"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboItemMateraiID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                            HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboItemMateraiID_ItemDataBound"
                                            OnItemsRequested="cboItemMateraiID_ItemsRequested">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>&nbsp;
                                        <asp:ImageButton ID="btnAddStamp" runat="server" ImageUrl="../../../../Images/Toolbar/insert16_h.png"
                                            CausesValidation="False" OnClick="btnAddStamp_Click" ToolTip="Add Stamp" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20%"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblRegistrationRule" runat="server" Text="Registration Rule Discount"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtDiscountAmount" runat="server" Width="127px" Value="0" />
                                    </td>
                                    <td>&nbsp;%
                                    </td>
                                    <td>&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnRegistrationRule" runat="server" ImageUrl="../../../../Images/Toolbar/refresh16.png"
                                            CausesValidation="False" OnClick="btnRegistrationRule_Click" ToolTip="Insert Registration Rule Discount" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="4" class="label">
                            <asp:Label ID="lblAnamnesa" runat="server" Text=""></asp:Label>
                            <br />
                            <asp:Label ID="lblPatientCategoryLbl" runat="server" Text="Patient Category : "></asp:Label>
                            <asp:Label ID="lblPatientCategory" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table style="width: 100%" cellpadding="0" cellspacing="5">
        <tr>
            <td>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label12" runat="server" Text="PROCESS" Font-Bold="true" Font-Size="12px"
                            ForeColor="Blue"></asp:Label></legend>
                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 50%">
                                <table>
                                    <tr>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:Button runat="server" ID="btnRecalculated" Text="Recalculate" OnClick="btnRecalculated_Click" />
                                                    </td>
                                                    <td>&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Button runat="server" ID="btnIntermBill" Text="Interim Bill" OnClick="btnIntermBill_Click" />
                                                    </td>
                                                    <td>&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Button runat="server" ID="btnPaymentReceive" Text="Guarantor Receipt (Corporate A/R)"
                                                            OnClientClick="openWinPayment('', '');return false;" />
                                                    </td>
                                                    <td>&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Button runat="server" ID="btnPersonalAr" Text="Personal A/R" OnClientClick="openWinPersonalAr();return false;" />
                                                    </td>
                                                    <td>&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Button runat="server" ID="btnPaymentProcess" Text="Payment Received" OnClientClick="gotoAddPaymentReceivedUrl();return false;" />
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Button runat="server" ID="btnPaymentProcessRSSM" Text="Print Out Billing" OnClientClick="openWinRegistrationInfoX();return false;" />
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
                </fieldset>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td></td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
        ShowBaseLine="true">
        <Tabs>
            <telerik:RadTab runat="server" Text="Transaction List" PageViewID="pgVerification"
                Selected="True" />
            <telerik:RadTab runat="server" Text="Interim Bill List" PageViewID="pgIntermBill" />
            <telerik:RadTab runat="server" Text="Verified Transaction" PageViewID="pgVerified" />
            <telerik:RadTab runat="server" Text="Registration Item Rule" PageViewID="pgItemRule" />
            <telerik:RadTab runat="server" Text="Tariff Component Discount Rule" PageViewID="pgTariffCompDiscount" />
            <telerik:RadTab runat="server" Text="Plafond Rule" PageViewID="pgPlafondRule" />
            <telerik:RadTab runat="server" Text="A/R Receipt" PageViewID="pgGuarantorReceipt" />
            <telerik:RadTab runat="server" Text="E-Claim" PageViewID="pgEklaim" Visible="false" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderStyle="Solid" SelectedIndex="0"
        BorderColor="Gray">
        <telerik:RadPageView ID="pgVerification" runat="server">
            <table style="width: 100%" cellpadding="0" cellspacing="5">
                <tr>
                    <td>
                        <fieldset>
                            <legend></legend>
                            <table style="width: 100%" cellpadding="0" cellspacing="5">
                                <tr>
                                    <td style="width: 15%">
                                        <asp:LinkButton ID="lbtnNewTransaction" runat="server" OnClientClick="gotoAddTransactionUrl('tr');return false;">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/new16.png" />
                                            &nbsp;<asp:Label runat="server" ID="lblTransactionEntry" Text="Transaction Entry"
                                                Font-Bold="True" ForeColor="Blue"></asp:Label>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnAlertNewTransaction" Visible="False" runat="server" OnClientClick="gotoAlertNonPatientCustomer('tr');return false;">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/new16.png" />
                                            &nbsp;<asp:Label runat="server" ID="Label6" Text="Transaction Entry" Font-Bold="True"
                                                ForeColor="Blue"></asp:Label>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnAlertNewTransactionCheckinConfirmed" Visible="False" runat="server"
                                            OnClientClick="gotoAlertPatientNotBeenCheckinConfirmed('tr');return false;">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/new16.png" />
                                            &nbsp;<asp:Label runat="server" ID="Label9" Text="Transaction Entry" Font-Bold="True"
                                                ForeColor="Blue"></asp:Label>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnAlertNewTransactionUserAuthorization" Visible="False" runat="server"
                                            OnClientClick="gotoAlertUserIsNotEditable();return false;">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/new16_d.png" />
                                            &nbsp;<asp:Label runat="server" ID="Label14" Text="Transaction Entry" Font-Bold="True"
                                                ForeColor="DarkGray"></asp:Label>
                                        </asp:LinkButton>
                                    </td>
                                    <td style="width: 15%; display: none">
                                        <asp:LinkButton ID="lbtnNewAncillary" runat="server" OnClientClick="gotoAddTransactionUrl('ds');return false;">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/new16.png" />
                                            &nbsp;<asp:Label runat="server" ID="lblAncillaryEntry" Text="Ancillary Entry" Font-Bold="True"
                                                ForeColor="Blue"></asp:Label>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnAlertNewAncillary" runat="server" Visible="False" OnClientClick="gotoAlertNonPatientCustomer('ds');return false;">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/new16.png" />
                                            &nbsp;<asp:Label runat="server" ID="Label7" Text="Ancillary Entry" Font-Bold="True"
                                                ForeColor="Blue"></asp:Label>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnAlertNewAncillaryCheckinConfirmed" runat="server" Visible="False"
                                            OnClientClick="gotoAlertPatientNotBeenCheckinConfirmed('ds');return false;">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/new16.png" />
                                            &nbsp;<asp:Label runat="server" ID="Label10" Text="Ancillary Entry" Font-Bold="True"
                                                ForeColor="Blue"></asp:Label>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnAlertNewAncillaryUserAuthorization" Visible="False" runat="server"
                                            OnClientClick="gotoAlertUserIsNotEditable();return false;">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/new16_d.png" />
                                            &nbsp;<asp:Label runat="server" ID="Label15" Text="Ancillary Entry" Font-Bold="True"
                                                ForeColor="DarkGray"></asp:Label>
                                        </asp:LinkButton>
                                    </td>
                                    <td style="width: 15%">
                                        <asp:LinkButton ID="lbtnCorrection" runat="server" OnClientClick="gotoAddCorrectionUrl();return false;">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/delete16.png" />
                                            &nbsp;<asp:Label runat="server" ID="lblCorrectionEntry" Text="Correction Entry" Font-Bold="True"
                                                ForeColor="Blue"></asp:Label>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnAlertCorrection" runat="server" Visible="False" OnClientClick="gotoAlertNonPatientCustomer('cr');return false;">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/new16.png" />
                                            &nbsp;<asp:Label runat="server" ID="Label8" Text="Correction Entry" Font-Bold="True"
                                                ForeColor="Blue"></asp:Label>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnAlertCorrectionCheckinConfirmed" runat="server" Visible="False"
                                            OnClientClick="gotoAlertPatientNotBeenCheckinConfirmed('cr');return false;">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/new16.png" />
                                            &nbsp;<asp:Label runat="server" ID="Label11" Text="Correction Entry" Font-Bold="True"
                                                ForeColor="Blue"></asp:Label>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnAlertCorrectionUserAuthorization" Visible="False" runat="server"
                                            OnClientClick="gotoAlertUserIsNotEditable();return false;">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/new16_d.png" />
                                            &nbsp;<asp:Label runat="server" ID="Label16" Text="Correction Entry" Font-Bold="True"
                                                ForeColor="DarkGray"></asp:Label>
                                        </asp:LinkButton>
                                    </td>
                                    <td style="width: 15%">
                                        <asp:LinkButton ID="lbtnInfoPatientTransfer" runat="server" OnClientClick="openWinPatientTransferInfo();return false;">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/details16.png" />
                                            &nbsp;<asp:Label runat="server" ID="lblInfoPatientTransfer" Text="Patient Transfer"
                                                Font-Bold="True" ForeColor="Blue"></asp:Label>
                                        </asp:LinkButton>
                                    </td>
                                    <td style="width: 15%">
                                        <asp:LinkButton ID="lbtnInfoPayment" runat="server" OnClientClick="openWinPaymentInfo();return false;">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/details16.png" />
                                            &nbsp;<asp:Label runat="server" ID="lblInfoPayment" Text="Payment" Font-Bold="True"
                                                ForeColor="Blue"></asp:Label>
                                        </asp:LinkButton>
                                    </td>
                                    <td style="width: 15%">
                                        <asp:LinkButton ID="lbtnInfoPysicianTeam" runat="server" OnClientClick="openWinPhysicianTeamInfo();return false;">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/details16.png" />
                                            &nbsp;<asp:Label runat="server" ID="lblInfoPysicianTeam" Text="Physician Team" Font-Bold="True"
                                                ForeColor="Blue"></asp:Label>
                                        </asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtnInfoMergeBilling" runat="server" OnClientClick="openWinMergeBillingInfo();return false;">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/details16.png" />
                                            &nbsp;<asp:Label runat="server" ID="lblInfoMergeBilling" Text="Merge Billing History"
                                                Font-Bold="True" ForeColor="Blue"></asp:Label>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdTransChargesItem" runat="server" OnNeedDataSource="grdTransChargesItem_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" ShowFooter="true">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView DataKeyNames="RegistrationNo,TransactionNo,SequenceNo">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="Group" HeaderText="Service Unit " />
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="IsApprove" SortOrder="Ascending" />
                                <telerik:GridGroupByField FieldName="ExecutionDate" SortOrder="Ascending" />
                                <telerik:GridGroupByField FieldName="Group" SortOrder="None" />
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridBoundColumn DataField="RegistrationNo" UniqueName="RegistrationNo" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TransactionNo" UniqueName="TransactionNo" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SequenceNo" UniqueName="SequenceNo" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IsApprove" UniqueName="IsApprove" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="ApproveTemplateColumn" HeaderText="App">
                            <ItemTemplate>
                                <%# GetStatus(DataBinder.Eval(Container.DataItem, "IsOrder"), DataBinder.Eval(Container.DataItem, "IsOrderRealization"), DataBinder.Eval(Container.DataItem, "IsApprove")) %>
                            </ItemTemplate>
                            <HeaderStyle Width="25px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="IsVoid" UniqueName="IsVoid" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IsBillProceed" UniqueName="IsBillProceed" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="BillingTemplateColumn">
                            <ItemTemplate>
                                <asp:CheckBox ID="detailChkbox" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsBillProceed") %>'
                                    Enabled='false'></asp:CheckBox>
                            </ItemTemplate>
                            <HeaderStyle Width="20px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="IsOrder" UniqueName="IsOrder" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IsOrderRealization" UniqueName="IsOrderRealization"
                            Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="OrderTemplateColumn" HeaderText="Ord | Sta">
                            <HeaderStyle Width="65px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox ID="orderChkbox" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsOrder") %>'
                                    Enabled="false"></asp:CheckBox>
                                <asp:CheckBox ID="realizationChkbox" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsOrderRealization") %>'
                                    Enabled="false"></asp:CheckBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="StatusTemplateColumn" HeaderText="Paid | IBill">
                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox ID="paymentChkbox" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsPaymentProceed") %>'
                                    Enabled="false"></asp:CheckBox>
                                <asp:CheckBox ID="intermbillChkbox" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsIntermBillProceed") %>'
                                    Enabled="false"></asp:CheckBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="DiscountTemplateColumn" HeaderText="Setting">
                            <HeaderStyle Width="50px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# this.IsUserEditAble.Equals(false) || DataBinder.Eval(Container.DataItem, "IsApprove").Equals(false) || DataBinder.Eval(Container.DataItem, "IsPaymentProceed").Equals(true) || DataBinder.Eval(Container.DataItem, "IsIntermBillProceed").Equals(true) || DataBinder.Eval(Container.DataItem, "IsCorrected").Equals(true) ? string.Empty :
                                                                        string.Format("<a href=\"#\" onclick=\"openWinProcess('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')\">Setting</a>", DataBinder.Eval(Container.DataItem, "TYPE"), DataBinder.Eval(Container.DataItem, "RegistrationNo"), 
                                                                                                        DataBinder.Eval(Container.DataItem, "TransactionNo"),
                                        DataBinder.Eval(Container.DataItem, "SequenceNo"), DataBinder.Eval(Container.DataItem, "ItemID"), DataBinder.Eval(Container.DataItem, "IsHoldTransactionEntry")) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="ExecutionDate" HeaderText="Execution Date" UniqueName="ExecutionDate"
                            SortExpression="ExecutionDate" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                            SortExpression="ItemName">
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridTemplateColumn UniqueName="ItemName" HeaderText="Item Name" Groupable="false">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ItemName")%>&nbsp;<%# DataBinder.Eval(Container.DataItem, "ItemConditionRuleName")%>
                                <br/>
                                Notes: <%# DataBinder.Eval(Container.DataItem, "Notes")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridNumericColumn DataField="ChargeQuantity" HeaderText="Qty" UniqueName="ChargeQuantity"
                            SortExpression="ChargeQuantity">
                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridNumericColumn>
                        <telerik:GridBoundColumn DataField="SRItemUnit" HeaderText="Unit" UniqueName="SRItemUnit"
                            SortExpression="SRItemUnit">
                            <HeaderStyle HorizontalAlign="Left" Width="40px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridNumericColumn DataField="Price" HeaderText="Price" UniqueName="Price"
                            SortExpression="Price" DataFormatString="{0:n2}">
                            <HeaderStyle HorizontalAlign="Center" Width="85px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridNumericColumn>
                        <telerik:GridTemplateColumn UniqueName="TemplateColumn1" HeaderText="Discount">
                            <HeaderStyle HorizontalAlign="Center" Width="85px" />
                            <ItemStyle HorizontalAlign="Right" />
                            <ItemTemplate>
                                <label title='<%# DataBinder.Eval(Container.DataItem, "DiscountReason")%>'>
                                    <%# String.Format("{0:n2}", DataBinder.Eval(Container.DataItem, "DiscountAmount"))%></label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridNumericColumn DataField="CitoAmount" HeaderText="Cito" UniqueName="CitoAmount"
                            SortExpression="CitoAmount" DataFormatString="{0:n2}" Aggregate="Count" FooterAggregateFormatString="Total :">
                            <FooterStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" Width="75px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridNumericColumn>
                        <telerik:GridNumericColumn DataField="Total" HeaderText="Total" UniqueName="Total"
                            SortExpression="Total" DataFormatString="{0:n2}" Aggregate="Sum" FooterAggregateFormatString="{0:n2}">
                            <FooterStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" Width="95px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridNumericColumn>
                        <telerik:GridBoundColumn DataField="LastUpdateByUserID" HeaderText="Update By" UniqueName="LastUpdateByUserID"
                            SortExpression="LastUpdateByUserID">
                            <HeaderStyle HorizontalAlign="Left" Width="80px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="LastUpdateDateTime" HeaderText="Last Process"
                            UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime" DataType="System.DateTime"
                            DataFormatString="{0:dd/MM/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="105px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="ChargeClassTemplateColumn" HeaderText="">
                            <HeaderStyle Width="50px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# this.IsPowerUser.Equals(false) || DataBinder.Eval(Container.DataItem, "IsApprove").Equals(false) || DataBinder.Eval(Container.DataItem, "IsPaymentProceed").Equals(true) || DataBinder.Eval(Container.DataItem, "IsIntermBillProceed").Equals(true) || DataBinder.Eval(Container.DataItem, "IsCorrected").Equals(true) ? string.Empty :
                                                                        string.Format("<a href=\"#\" onclick=\"openWinChargeClass('{0}')\"><img src=\"../../../../Images/edit.png\" border=\"0\" title=\"Charge Class\" /></a>", DataBinder.Eval(Container.DataItem, "TransactionNo")) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="TYPE" UniqueName="TYPE" Visible="false">
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgIntermBill" runat="server">
            <table style="width: 100%" cellpadding="0" cellspacing="5">
                <tr>
                    <td>
                        <fieldset>
                            <legend></legend>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblBillTo" runat="server" Text="Bill Status"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <asp:RadioButtonList ID="rblToGuarantor" runat="server" RepeatDirection="Horizontal"
                                            OnTextChanged="rblToGuarantor_OnTextChanged" AutoPostBack="true">
                                            <asp:ListItem Selected="true">To Guarantor</asp:ListItem>
                                            <asp:ListItem>To Patient</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblCreateBilling" runat="server" Text="Bill To Class"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cboBillToClassID" runat="server" Width="150px" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnProcessBillToClass" runat="server" ImageUrl="../../../../Images/Toolbar/refresh16.png"
                                            CausesValidation="False" OnClick="btnProcessBillToClass_Click" ToolTip="Process Billing to Selected Class" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnPrintBillToClass" runat="server" ImageUrl="../../../../Images/Toolbar/print_preview16.png"
                                            CausesValidation="False" OnClick="btnPrintBillToClass_Click" ToolTip="Print Billing to Selected Class" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnSurgeryCostPreview" runat="server" ImageUrl="../../../../Images/Toolbar/print_preview16.png"
                                            OnClientClick="openWinSurgeryCostPreview();return false;" ToolTip="Surgery Cost Estimation Preview" />
                                    </td>
                                    <td runat="server" id="tdSaveAdmDisc">
                                        <asp:LinkButton ID="lbtnAdmDisc" runat="server" OnClick="lbtnSaveAdmDisc_Click">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/save16.png" />
                                            &nbsp;<asp:Label runat="server" ID="lblSaveAdmDisc" Text="Save Adm. Discounts" Font-Bold="True"
                                                ForeColor="Blue"></asp:Label>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdIntermBill" runat="server" OnNeedDataSource="grdIntermBill_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" ShowFooter="true" OnDetailTableDataBind="grdIntermBill_DetailTableDataBind">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView DataKeyNames="IntermBillNo">
                    <Columns>
                        <telerik:GridBoundColumn DataField="RegistrationNo" UniqueName="RegistrationNo" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="IntermBillTemplateColumn" HeaderText="Print">
                            <ItemTemplate>
                                <asp:CheckBox ID="detailChkbox" runat="server" Checked="True" Enabled="true"></asp:CheckBox>
                            </ItemTemplate>
                            <HeaderStyle Width="20px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="PaymentTemplateColumn" HeaderText="Paid">
                            <HeaderStyle Width="50px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox ID="paymentChkbox" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsPaid") %>'
                                    Enabled="false"></asp:CheckBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="PaymentTemplateColumn" HeaderText="Patient Paid"
                            Visible="False">
                            <HeaderStyle Width="50px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox ID="paymentPatientChkbox" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsPatientPaid") %>'
                                    Enabled="false"></asp:CheckBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Interim Bill No" UniqueName="TemplateIntermBillNo">
                            <ItemTemplate>
                                <b>
                                    <asp:Label ID="lblIntermBillNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IntermBillNo") %>' /></b><br />
                                <i>Payment No : </i>
                                <asp:Label ID="lblIntervention" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PaymentNo") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="IntermBillDate" HeaderText="Date" UniqueName="IntermBillDate"
                            SortExpression="IntermBillDate" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="StartDate" HeaderText="Start Date" UniqueName="StartDate"
                            SortExpression="StartDate" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EndDate" HeaderText="End Date" UniqueName="EndDate"
                            SortExpression="EndDate" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridNumericColumn DataField="PatientAmount" HeaderText="Patient Amount"
                            UniqueName="PatientAmount" SortExpression="PatientAmount" DataFormatString="{0:n2}"
                            FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridNumericColumn>
                        <telerik:GridNumericColumn DataField="GuarantorAmount" HeaderText="Guarantor Amount"
                            UniqueName="GuarantorAmount" SortExpression="GuarantorAmount" DataFormatString="{0:n2}"
                            FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridNumericColumn>
                        <telerik:GridNumericColumn DataField="AdministrationAmount" HeaderText="Patient Adm."
                            UniqueName="AdministrationAmount" SortExpression="AdministrationAmount" DataFormatString="{0:n2}"
                            FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridNumericColumn>
                        <telerik:GridNumericColumn DataField="GuarantorAdministrationAmount" HeaderText="Guarantor Adm."
                            UniqueName="GuarantorAdministrationAmount" SortExpression="GuarantorAdministrationAmount"
                            DataFormatString="{0:n2}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridNumericColumn>
                        <telerik:GridTemplateColumn UniqueName="TemplateDiscAdmPatient" HeaderStyle-Width="130px"
                            DataField="DiscAdmPatient" ItemStyle-HorizontalAlign="Center" HeaderText="Patient Adm. Disc."
                            HeaderStyle-HorizontalAlign="center" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                            FooterStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txtDiscAdmPatient" runat="server" Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "DiscAdmPatient")) %>'
                                    ReadOnly='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsNotAllowEdit")) %>'
                                    MaxValue='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "AdministrationAmount")) %>'
                                    Width="90px">
                                </telerik:RadNumericTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="TemplateDiscAdmGuarantor" HeaderStyle-Width="130px"
                            DataField="DiscAdmGuarantor" ItemStyle-HorizontalAlign="Center" HeaderText="Guarantor Adm. Disc."
                            HeaderStyle-HorizontalAlign="center" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                            FooterStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txtDiscAdmGuarantor" runat="server" Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "DiscAdmGuarantor")) %>'
                                    ReadOnly='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsNotAllowEdit")) %>'
                                    MaxValue='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "GuarantorAdministrationAmount")) %>'
                                    Width="90px">
                                </telerik:RadNumericTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="AskesCoveredSeqNo" HeaderText="ASKES No" UniqueName="AskesCoveredSeqNo"
                            SortExpression="AskesCoveredSeqNo" Visible="False">
                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CreatedByUserID" HeaderText="Created By" UniqueName="CreatedByUserID"
                            SortExpression="CreatedByUserID">
                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CreatedDateTime" HeaderText="Created Date/Time"
                            UniqueName="CreatedDateTime" SortExpression="CreatedDateTime" DataType="System.DateTime"
                            DataFormatString="{0:dd/MM/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="115px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsPaid").Equals(true) || DataBinder.Eval(Container.DataItem, "IsPaid2").Equals(true) ? string.Empty :
                                    ( this.IsUserVoidAble.Equals(false) ? string.Format("<a href=\"#\"><img src=\"../../../../Images/Toolbar/row_delete16_d.png\" border=\"0\" title=\"Void\" /></a>") : 
                                    string.Format("<a href=\"#\" onclick=\"onVoidIntermBill('{0}'); return false;\">{1}</a>",
                                                                            DataBinder.Eval(Container.DataItem, "IntermBillNo"),
                                                                            DataBinder.Eval(Container.DataItem, "IsPaid").Equals(false) && DataBinder.Eval(Container.DataItem, "IsPaid2").Equals(false) && DataBinder.Eval(Container.DataItem, "AskesCoveredSeqNo").Equals(string.Empty) ? "<img src=\"../../../../Images/Toolbar/row_delete16.png\" border=\"0\" title=\"Void\" />" : string.Empty))
                                                                            )%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="grdIntermBillDetail" DataKeyNames="RegistrationNo, TransactionNo, SequenceNo"
                            AutoGenerateColumns="false">
                            <GroupByExpressions>
                                <telerik:GridGroupByExpression>
                                    <SelectFields>
                                        <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Service Unit "></telerik:GridGroupByField>
                                    </SelectFields>
                                    <GroupByFields>
                                        <telerik:GridGroupByField FieldName="ServiceUnitName" SortOrder="Ascending"></telerik:GridGroupByField>
                                    </GroupByFields>
                                </telerik:GridGroupByExpression>
                            </GroupByExpressions>
                            <Columns>
                                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                                    SortExpression="ServiceUnitName" Visible="False">
                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TransactionDate" HeaderText="Date" UniqueName="TransactionDate"
                                    SortExpression="TransactionDate" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TransactionNo" HeaderText="Transaction No" UniqueName="TransactionNo"
                                    SortExpression="TransactionNo">
                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                                    SortExpression="ItemID" HeaderStyle-Width="90px">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                    SortExpression="ItemName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PatientAmount" HeaderText="Patient Amount" UniqueName="PatientAmount"
                                    SortExpression="PatientAmount" DataFormatString="{0:n2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                                    FooterAggregateFormatString="{0:n2}">
                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="GuarantorAmount" HeaderText="Guarantor Amount"
                                    UniqueName="GuarantorAmount" SortExpression="GuarantorAmount" DataFormatString="{0:n2}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right" FooterAggregateFormatString="{0:n2}">
                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridCalculatedColumn HeaderStyle-Width="120px" HeaderText="Discount Amount"
                                    UniqueName="TotalDiscount" DataType="System.Double" DataFields="DiscountAmount,DiscountAmount2"
                                    SortExpression="TotalDiscount" Expression="{0} + {1}" FooterStyle-HorizontalAlign="Right"
                                    Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n2}" />
                                <telerik:GridBoundColumn DataField="IntermBillNo" HeaderText="IntermBillNo" UniqueName="IntermBillNo"
                                    SortExpression="IntermBillNo" Visible="false">
                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgVerified" runat="server">
            <table style="width: 100%" cellpadding="0" cellspacing="5">
                <tr>
                    <td>
                        <fieldset>
                            <table style="width: 100%" cellpadding="0" cellspacing="5">
                                <tr>
                                    <td style="width: 15%" runat="server" id="tdProcessChecked">&nbsp;&nbsp;
                                        <asp:LinkButton ID="lbtnProcessChecked" runat="server" OnClick="lbtnProcessChecked_Click">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/todolist16.png" />
                                            &nbsp;<asp:Label runat="server" ID="Label4" Text="Checked" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                        </asp:LinkButton>
                                    </td>
                                    <td style="width: 15%" runat="server" id="tdSaveVerified">&nbsp;&nbsp;
                                        <asp:LinkButton ID="lbtnSaveVerified" runat="server" OnClick="lbtnSaveVerified_Click">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/save16.png" />
                                            &nbsp;<asp:Label runat="server" ID="lblSaveVerified" Text="Save" Font-Bold="True"
                                                ForeColor="Blue"></asp:Label>
                                        </asp:LinkButton>
                                    </td>
                                    <td style="width: 15%" runat="server" id="tdSaveBuffer" visible="False">&nbsp;&nbsp;
                                        <asp:LinkButton ID="lbtnSaveToBuffer" runat="server" OnClick="lbtnSaveToBuffer_Click">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/save16.png" />
                                            &nbsp;<asp:Label runat="server" ID="lblSaveToBuffer" Text="Save To Buffer" Font-Bold="True"
                                                ForeColor="Blue"></asp:Label>
                                        </asp:LinkButton>
                                    </td>
                                    <td style="width: 15%" runat="server" id="tdProcessPatientToGuarantor">&nbsp;&nbsp;
                                        <asp:LinkButton ID="lbtnProcessPatientToGuarantor" runat="server" OnClick="lbtnProcessVerifiedPatientToGuarantor_Click">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/process16.png" />
                                            &nbsp;<asp:Label runat="server" ID="Label1" Text="Patient to Guarantor" Font-Bold="True"
                                                ForeColor="Blue"></asp:Label>
                                        </asp:LinkButton>
                                    </td>
                                    <td style="width: 15%" runat="server" id="tdProcessGuarantorToPatient">&nbsp;&nbsp;
                                        <asp:LinkButton ID="lbtnProcessGuarantorToPatient" runat="server" OnClick="lbtnProcessVerifiedGuarantorToPatient_Click">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/process16.png" />
                                            &nbsp;<asp:Label runat="server" ID="Label3" Text="Guarantor to Patient" Font-Bold="True"
                                                ForeColor="Blue"></asp:Label>
                                        </asp:LinkButton>
                                    </td>
                                    <td>&nbsp;&nbsp;
                                        <asp:LinkButton ID="lbtnPrintPreviewVerified" runat="server" OnClick="lbtnPrintPreviewVerified_Click">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/print_preview16.png" />
                                            &nbsp;<asp:Label runat="server" ID="lblPrintPreviewVerified" Text="Print Preview"
                                                Font-Bold="True" ForeColor="Blue"></asp:Label>
                                        </asp:LinkButton>
                                    </td>

                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
            <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="RadMultiPage2"
                ShowBaseLine="true">
                <Tabs>
                    <telerik:RadTab runat="server" Text="Verified Item List" PageViewID="pgVerifiedList"
                        Selected="True" />
                    <telerik:RadTab runat="server" Text="Buffer Item List" PageViewID="pgBufferList"
                        Visible="False" />
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="RadMultiPage2" runat="server" BorderStyle="Solid" SelectedIndex="0"
                BorderColor="Gray">
                <telerik:RadPageView ID="pgVerifiedList" runat="server">

                    <telerik:RadGrid ID="grdCostCalculation" runat="server" OnNeedDataSource="grdCostCalculation_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" ShowFooter="true">
                        <HeaderContextMenu>
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="RegistrationNo, TransactionNo, SequenceNo"
                            FilterExpression="ParentNo IS NULL OR ParentNo = '' ">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="30px">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                            runat="server" Checked="false"></asp:CheckBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="detailChkbox" runat="server" Checked="False"></asp:CheckBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="30px" DataField="IsChecked" HeaderImageUrl="../../../../Images/todolist16.png"
                                    UniqueName="IsChecked" SortExpression="IsChecked" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="RegistrationNo" UniqueName="RegistrationNo" Visible="False" />
                                <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="TransactionNo" HeaderText="Transaction No"
                                    UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="ReferenceNo" HeaderText="Correction For"
                                    UniqueName="ReferenceNo" SortExpression="ReferenceNo" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="SequenceNo" UniqueName="SequenceNo" Visible="False" />
                                <telerik:GridBoundColumn DataField="TransactionDate" HeaderText="Date" UniqueName="TransactionDate"
                                    SortExpression="TransactionDate" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                                    SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ItemID" HeaderText="Item ID"
                                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ClassName" HeaderText="Class"
                                    UniqueName="ClassName" SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Aggregate="count" FooterAggregateFormatString="Total :"
                                    FooterStyle-HorizontalAlign="Right" />
                                <telerik:GridCalculatedColumn HeaderStyle-Width="90px" HeaderText="Total" UniqueName="Total"
                                    DataType="System.Double" DataFields="PatientAmount,GuarantorAmount" SortExpression="Total"
                                    Expression="{0} + {1}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridTemplateColumn UniqueName="TemplateColumn1" HeaderStyle-Width="100px"
                                    DataField="PatientAmount" ItemStyle-HorizontalAlign="Center" HeaderText="Patient Amt"
                                    HeaderStyle-HorizontalAlign="center" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                                    FooterStyle-HorizontalAlign="right" Visible="False">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtPatientAmount" runat="server" Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "PatientAmount")) %>'
                                            ReadOnly="True" Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridNumericColumn HeaderStyle-Width="90px" DataField="PatientAmount" HeaderText="Patient Amt"
                                    UniqueName="PatientAmount" SortExpression="PatientAmount" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                                    FooterStyle-HorizontalAlign="right" />
                                <telerik:GridTemplateColumn UniqueName="TemplateColumn2" HeaderStyle-Width="100px"
                                    DataField="GuarantorAmount" ItemStyle-HorizontalAlign="Center" HeaderText="Guarantor Amt"
                                    HeaderStyle-HorizontalAlign="center" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                                    FooterStyle-HorizontalAlign="right">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtGuarantorAmount" runat="server" Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "GuarantorAmount")) %>'
                                            ReadOnly='<%# (Request.QueryString["md"] == "view") %>' Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridNumericColumn HeaderStyle-Width="90px" DataField="DiscountAmount" HeaderText="Discount Amt"
                                    UniqueName="DiscountAmount" SortExpression="DiscountAmount" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                                    FooterStyle-HorizontalAlign="right" />
                                <telerik:GridTemplateColumn UniqueName="TemplateColumn3" HeaderStyle-Width="100px"
                                    DataField="DiscountAmount2" ItemStyle-HorizontalAlign="Center" HeaderText="Add Disc. Amt"
                                    HeaderStyle-HorizontalAlign="center" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                                    FooterStyle-HorizontalAlign="right">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtDiscountAmount2" runat="server" Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "DiscountAmount2")) %>'
                                            ReadOnly='<%# (Request.QueryString["md"] == "view") %>' Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="LastUpdateDateTime" HeaderText="Last Process"
                                    UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime" DataType="System.DateTime"
                                    DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ParentNo" HeaderText="Parent No" UniqueName="ParentNo"
                                    SortExpression="ParentNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    Visible="false" />
                            </Columns>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView ID="pgBufferList" runat="server">
                    <telerik:RadGrid ID="grdBuffer" runat="server" OnNeedDataSource="grdBuffer_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" ShowFooter="true" OnDetailTableDataBind="grdBuffer_DetailTableDataBind">
                        <HeaderContextMenu>
                        </HeaderContextMenu>
                        <MasterTableView DataKeyNames="RegistrationNo, GuarantorID">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="BufferPaymentTemplateColumn" HeaderText="Paid">
                                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="BufferPaymentChkbox" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsPaid") %>'
                                            Enabled="false"></asp:CheckBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="RegistrationNo" UniqueName="RegistrationNo" Visible="False" />
                                <telerik:GridNumericColumn DataField="GuarantorID" HeaderText="Guarantor ID" UniqueName="GuarantorID"
                                    SortExpression="GuarantorID">
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridNumericColumn>
                                <telerik:GridNumericColumn DataField="GuarantorName" HeaderText="Guarantor Name"
                                    UniqueName="GuarantorName" SortExpression="GuarantorName">
                                </telerik:GridNumericColumn>
                                <telerik:GridBoundColumn DataField="PatientAmount" HeaderText="Patient Amount" UniqueName="PatientAmount"
                                    SortExpression="PatientAmount" DataFormatString="{0:n2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                                    FooterAggregateFormatString="{0:n2}">
                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridNumericColumn DataField="GuarantorAmount" HeaderText="Guarantor Amount"
                                    UniqueName="GuarantorAmount" SortExpression="GuarantorAmount" DataFormatString="{0:n2}"
                                    FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridNumericColumn>
                                <telerik:GridBoundColumn DataField="DiscountAmount" HeaderText="Discount Amount"
                                    UniqueName="DiscountAmount" SortExpression="DiscountAmount" DataFormatString="{0:n2}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right" FooterAggregateFormatString="{0:n2}">
                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# (DataBinder.Eval(Container.DataItem, "IsPaid").Equals(true) ? string.Empty :
                                            string.Format("<a href=\"#\" onclick=\"onDeleteBuffer('{0}', '{1}'); return false;\">{2}</a>",
                                                                            DataBinder.Eval(Container.DataItem, "GuarantorID"),
                                                                            DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                                                            DataBinder.Eval(Container.DataItem, "IsPaid").Equals(true) ? string.Empty : "<img src=\"../../../../Images/Toolbar/delete16.png\" border=\"0\" title=\"Delete\" />"))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# (DataBinder.Eval(Container.DataItem, "IsPaid").Equals(true) ? string.Empty :
                                            string.Format("<a href=\"#\" onclick=\"openWinPayment('{0}', '{1}'); return false;\">{2}</a>",
                                                                            DataBinder.Eval(Container.DataItem, "GuarantorID"),
                                                                                                                    string.Empty,
                                                                        DataBinder.Eval(Container.DataItem, "IsPaid").Equals(true) ? string.Empty : "<img src=\"../../../../Images/Toolbar/process16.png\" border=\"0\" title=\"Guarantor Payment Receipt\" />"))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <DetailTables>
                                <telerik:GridTableView Name="grdBufferDetail" DataKeyNames="TransactionNo, SequenceNo"
                                    AutoGenerateColumns="false">
                                    <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Service Unit "></telerik:GridGroupByField>
                                            </SelectFields>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="ServiceUnitName" SortOrder="Ascending"></telerik:GridGroupByField>
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                                            SortExpression="ServiceUnitName" Visible="False">
                                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridDateTimeColumn HeaderStyle-Width="90px" DataField="TransactionDate"
                                            HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                        <telerik:GridBoundColumn DataField="TransactionNo" HeaderText="Transaction No" UniqueName="TransactionNo"
                                            SortExpression="TransactionNo">
                                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                                            SortExpression="ItemID" HeaderStyle-Width="90px">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                            SortExpression="ItemName">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PatientAmount" HeaderText="Patient Amount" UniqueName="PatientAmount"
                                            SortExpression="PatientAmount" DataFormatString="{0:n2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                                            FooterAggregateFormatString="{0:n2}">
                                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="GuarantorAmount" HeaderText="Guarantor Amount"
                                            UniqueName="GuarantorAmount" SortExpression="GuarantorAmount" DataFormatString="{0:n2}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right" FooterAggregateFormatString="{0:n2}">
                                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DiscountAmount" HeaderText="Discount Amount"
                                            UniqueName="DiscountAmount" SortExpression="DiscountAmount" DataFormatString="{0:n2}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right" FooterAggregateFormatString="{0:n2}">
                                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </telerik:GridTableView>
                            </DetailTables>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgItemRule" runat="server">
            <telerik:RadGrid ID="grdRegistrationItemRule" runat="server" AllowPaging="true" OnNeedDataSource="grdRegistrationItemRule_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdRegistrationItemRule_UpdateCommand"
                OnDeleteCommand="grdRegistrationItemRule_DeleteCommand" OnInsertCommand="grdRegistrationItemRule_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="RegistrationNo,ItemID">
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
                        <telerik:GridCheckBoxColumn DataField="IsInclude" HeaderText="Include" UniqueName="IsInclude"
                            SortExpression="IsInclude">
                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridCheckBoxColumn>
                        <telerik:GridCheckBoxColumn DataField="IsToGuarantor" HeaderText="To Guarantor" UniqueName="IsToGuarantor"
                            SortExpression="IsToGuarantor">
                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridCheckBoxColumn>
                        <telerik:GridBoundColumn DataField="GuarantorRuleTypeName" HeaderText="Rule Type Name"
                            UniqueName="GuarantorRuleTypeName" SortExpression="GuarantorRuleTypeName">
                            <HeaderStyle HorizontalAlign="Left" Width="120px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn DataField="IsValueInPercent" HeaderText="In Percent"
                            UniqueName="IsValueInPercent" SortExpression="IsValueInPercent">
                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridCheckBoxColumn>
                        <telerik:GridBoundColumn DataField="AmountValue" HeaderText="Amount Value (IPR/Default)"
                            UniqueName="AmountValue" SortExpression="AmountValue" DataFormatString="{0:n2}">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="OutpatientAmountValue" HeaderText="Amount Value (OPR)"
                            UniqueName="OutpatientAmountValue" SortExpression="OutpatientAmountValue" DataFormatString="{0:n2}">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EmergencyAmountValue" HeaderText="Amount Value (EMR)"
                            UniqueName="EmergencyAmountValue" SortExpression="EmergencyAmountValue" DataFormatString="{0:n2}">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="RegistrationItemRuleDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="RegistrationItemRuleEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgTariffCompDiscount" runat="server">
            <table style="width: 100%" cellpadding="0" cellspacing="5" runat="server" id="tblSaveTariffCompDiscountRule">
                <tr>
                    <td>
                        <fieldset>
                            <table style="width: 100%" cellpadding="0" cellspacing="5">
                                <tr>
                                    <td>&nbsp;&nbsp;
                                        <asp:LinkButton ID="lbtnSaveTariffCompDiscountRule" runat="server" OnClick="lbtnSaveTariffCompDiscountRule_Click">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/save16.png" />
                                            &nbsp;<asp:Label runat="server" ID="lblSaveTariffCompDiscountRule" Text="Save" Font-Bold="True"
                                                ForeColor="Blue"></asp:Label>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td style="width: 50%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblTariffCompDiscountGlobalAmount" runat="server" Text="Discount Global Amount"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtTariffCompDiscountGlobalAmount" runat="server"
                                        Width="127px" />
                                    &nbsp;
                                    <asp:CheckBox ID="chkIsTariffCompDiscountGlobalInPercent" runat="server" Text="In Percentage" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblTariffCompDiscountResep" runat="server" Text="Presciption"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtTariffCompDiscountResep" runat="server" Width="127px"
                                        Value="0" Type="Percent" MinValue="0" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblTariffCompDiscountItemMedical" runat="server" Text="Item Medical"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtTariffCompDiscountItemMedical" runat="server" Width="127px"
                                        Value="0" Type="Percent" MinValue="0" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblTariffCompDiscountItemNonMedical" runat="server" Text="Item Non Medical"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtTariffCompDiscountItemNonMedical" runat="server"
                                        Width="127px" Value="0" Type="Percent" MinValue="0" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%">
                        <table width="100%">
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdTariffCompDiscountRule" runat="server" AutoGenerateColumns="False"
                GridLines="None" OnNeedDataSource="grdTariffCompDiscountRule_NeedDataSource">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="TariffComponentID">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="TariffComponentID" HeaderText="ID"
                            UniqueName="TariffComponentID" SortExpression="TariffComponentID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="TariffComponentName"
                            HeaderText="Tariff Component" UniqueName="TariffComponentName" SortExpression="TariffComponentName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="120px" HeaderText="Discount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txtDiscount" runat="server" Width="100px" DbValue='<%#Eval("Amount")%>'
                                    NumberFormat-DecimalDigits="2" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="150px" HeaderText="In Percentage">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkIsDiscountInPercentage" Checked='<%#DataBinder.Eval(Container.DataItem, "IsDiscountInPercentage") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgPlafondRule" runat="server">
            <table style="width: 100%" cellpadding="0" cellspacing="5" runat="server" id="tblSavePlafondRule">
                <tr>
                    <td>
                        <fieldset>
                            <table style="width: 100%" cellpadding="0" cellspacing="5">
                                <tr>
                                    <td>&nbsp;&nbsp;
                                        <asp:LinkButton ID="lbtnSavePlafondRule" runat="server" OnClick="lbtnSavePlafondRule_Click">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/save16.png" />
                                            &nbsp;<asp:Label runat="server" ID="Label2" Text="Save" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td style="width: 50%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPlafondRuleAmount" runat="server" Text="Plafond Amount"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPlafondRuleAmount" runat="server" Width="127px" />
                                    &nbsp;
                                    <asp:CheckBox ID="chkIsPlafondRuleInPercent" runat="server" Text="In Percentage" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="entry">
                                    <asp:RadioButtonList ID="rblIsPlafondRuleToGuarantor" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="true">To Guarantor</asp:ListItem>
                                        <asp:ListItem>To Patient</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%">
                        <table width="100%">
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgGuarantorReceipt" runat="server">
            <telerik:RadGrid ID="grdGuarantorReceipt" runat="server" AllowPaging="true" OnNeedDataSource="grdGuarantorReceipt_NeedDataSource"
                OnItemCommand="grdGuarantorReceipt_ItemCommand" OnInsertCommand="grdGuarantorReceipt_InsertCommand"
                AutoGenerateColumns="False" GridLines="None" ShowFooter="true">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="PaymentNo">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="Print" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnPrintGuarantorReceipt" runat="server" CommandName="PrintGuarantorReceipt"
                                    Visible='<%# DataBinder.Eval(Container.DataItem, "IsApproved") %>' ToolTip='Print Guarantor Receipt'
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PaymentNo") %>'>
                                    <img src="../../../../Images/Toolbar/print16.png" border="0" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="PrintD" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnPrintGuarantorBillingStatement" runat="server" CommandName="PrintGuarantorBillingStatement"
                                    Visible='<%# DataBinder.Eval(Container.DataItem, "IsApproved") %>' ToolTip='Print Guarantor Billing Statement'
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PaymentNo") %>'>
                                    <img src="../../../../Images/Toolbar/print16.png" border="0" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="PrintP" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnPrintPackageDetailStatement" runat="server" CommandName="PrintPackageDetailStatement"
                                    Visible='<%# DataBinder.Eval(Container.DataItem, "IsApproved") %>' ToolTip='Print Package Detail Statement'
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PaymentNo") %>'>
                                    <img src="../../../../Images/Toolbar/print16.png" border="0" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="PrintDetailOp" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnPrintDetailOpStatement" runat="server" CommandName="PrintDetailOpStatement"
                                    Visible='<%# DataBinder.Eval(Container.DataItem, "IsApproved") %>' ToolTip='Print Detail Outpatient Billing Statement'
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PaymentNo") %>'>
                                    <img src="../../../../Images/Toolbar/print16.png" border="0" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="PrintDetailBpjs" HeaderStyle-Width="35px"
                            ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnPrintDetailBpjsStatement" runat="server" CommandName="PrintDetailBpjsStatement"
                                    Visible='<%# DataBinder.Eval(Container.DataItem, "IsApproved") %>' ToolTip='Print Detail INACBG Billing Statement'
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PaymentNo") %>'>
                                    <img src="../../../../Images/Toolbar/print16.png" border="0" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="PrintGuarantorOnlyBillingStatementNoDiscNoDP"
                            HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnPrintGuarantorOnlyBillingStatementNoDiscNoDP" runat="server"
                                    CommandName="PrintGuarantorOnlyBillingStatementNoDiscNoDP" Visible='<%# DataBinder.Eval(Container.DataItem, "IsApproved") %>'
                                    ToolTip='Print Guarantor Billing Statement (Guarantor Only Without Discount and Down Payment)'
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PaymentNo") %>'>
                                    <img src="../../../../Images/Toolbar/print16.png" border="0" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="PrintGuarantorOnlyBillingStatement" HeaderStyle-Width="35px"
                            ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnPrintGuarantorOnlyBillingStatement" runat="server" CommandName="PrintGuarantorOnlyBillingStatement"
                                    Visible='<%# DataBinder.Eval(Container.DataItem, "IsApproved") %>' ToolTip='Print Guarantor Billing Statement (Guarantor Only)'
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PaymentNo") %>'>
                                    <img src="../../../../Images/Toolbar/print16.png" border="0" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="PrintD_EN" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnPrintGuarantorBillingStatement_EN" runat="server" CommandName="PrintGuarantorBillingStatement_EN"
                                    Visible='<%# DataBinder.Eval(Container.DataItem, "IsApproved") %>' ToolTip='Print Guarantor Billing Statement - EN'
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PaymentNo") %>'>
                                    <img src="../../../../Images/Toolbar/print16.png" border="0" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="PrintGuarantorOnlyBillingStatementNoDiscNoDP_EN"
                            HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnPrintGuarantorOnlyBillingStatementNoDiscNoDP_EN" runat="server"
                                    CommandName="PrintGuarantorOnlyBillingStatementNoDiscNoDP_EN" Visible='<%# DataBinder.Eval(Container.DataItem, "IsApproved") %>'
                                    ToolTip='Print Guarantor Billing Statement (Guarantor Only Without Discount and Down Payment) - EN'
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PaymentNo") %>'>
                                    <img src="../../../../Images/Toolbar/print16.png" border="0" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>


                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="PaymentDate" HeaderText="Payment Date"
                            UniqueName="PaymentDate" SortExpression="PaymentDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="PaymentTime" HeaderText="Time"
                            UniqueName="PaymentTime" SortExpression="PaymentTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PaymentNo" HeaderText="Payment No"
                            UniqueName="PaymentNo" SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Receipt To" UniqueName="GuarantorName"
                            SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="130px" DataField="PaymentAmount" HeaderText="Amount"
                            UniqueName="PaymentAmount" SortExpression="PaymentAmount" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                            FooterStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="130px" DataField="DiscountAmount" HeaderText="Discount"
                            UniqueName="DiscountAmount" SortExpression="DiscountAmount" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                            FooterStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            Visible="False" />
                        <telerik:GridCheckBoxColumn DataField="IsApproved" HeaderText="Approved" UniqueName="IsApproved"
                            SortExpression="IsApproved">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridCheckBoxColumn>
                        <telerik:GridCheckBoxColumn DataField="IsVoid" HeaderText="Void" UniqueName="IsVoid"
                            SortExpression="IsVoid" Visible="False">
                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridCheckBoxColumn>
                        <telerik:GridBoundColumn DataField="LastUpdateByUserID" HeaderText="Update By" UniqueName="LastUpdateByUserID"
                            SortExpression="LastUpdateByUserID">
                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="False">
                            <ItemTemplate>
                                <%# (this.IsUserVoidAble.Equals(false))  ? string.Empty :
                                    (string.Format("<a href=\"#\" onclick=\"onVoidPayment('{0}'); return false;\">{1}</a>",
                                                                            DataBinder.Eval(Container.DataItem, "PaymentNo"),
                                                                   "<img src=\"../../../../Images/Toolbar/row_delete16.png\" border=\"0\" title=\"Void\" />"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true) || DataBinder.Eval(Container.DataItem, "IsProceed").Equals(true)) ? string.Empty :
                                    (this.IsUserVoidAble.Equals(false) ? string.Format("<a href=\"#\"><img src=\"../../../../Images/Toolbar/row_delete16_d.png\" border=\"0\" title=\"Void\" /></a>") :
                                    string.Format("<a href=\"#\" onclick=\"openVoidPayment('{0}')\">{1}</a>", DataBinder.Eval(Container.DataItem, "PaymentNo"), 
                                                                    "<img src=\"../../../../Images/Toolbar/row_delete16.png\" border=\"0\" title=\"Void\" />"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <EditFormSettings UserControlName="GuarantorReceiptDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="GuarantorReceiptEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgEklaim" runat="server">
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
