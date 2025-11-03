<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="RegistrationList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.RegistrationList" %>

<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="rcb1" runat="server">
        <%=JavascriptOpenPrintPreview()%>
        <style type="text/css">
            .viewWrap {
                padding: 5px;
                background-color: gray;
            }

            .childMenu {
                padding: 5px 5px;
                background-color: rgb(54, 72, 91);
                height: 17px;
            }
        </style>
        <style type="text/css">
            .xnoti_Container {
                position: relative; /*border:1px solid blue;*/ /* This is just to show you where the container ends */
                width: 16px;
                height: 16px;
                cursor: pointer;
            }

            .xnoti_bubble {
                position: absolute;
                top: -8px;
                right: -6px;
                padding-right: 2px;
                background-color: red;
                color: white;
                font-weight: bold;
                font-size: 0.80em;
                border-radius: 2px;
                box-shadow: 1px 1px 1px gray;
            }
        </style>

        <script type="text/javascript" src="../../../JavaScript/jQuery.js"></script>

        <script type="text/javascript">
            // Varible untuk diisi di page patient, bila dari save new
            var _lastProcessID = '';

            function tbarPhr_OnClientButtonClicking(sender, args) {
                _lastProcessID = 'phr';
                var val = args.get_item().get_value();
                if (val.includes('refresh')) {
                    var grdPhr = $find(val.split('_')[1]).get_masterTableView();
                    grdPhr.rebind();
                } else {
                    var vals = val.split('^');
                    entryPhr('new', '', vals[1], vals[2], vals[3], vals[4], vals[5]);
                }
            }

            function entryPhr(md, id, fid, suId, patId, regNo, grdPhrClientId) {
                // Data for grid rebind
                document.getElementById("<%=hdnPatientID.ClientID%>").value = patId;
                document.getElementById("<%=hdnRegistrationNo.ClientID%>").value = regNo;
                document.getElementById("<%=hdnServiceUnitID.ClientID%>").value = suId;

                var url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/Common/Phr/PatientHealthRecordDetail.aspx?md=' + md + '&id=' + id + '&regno=' + regNo + '&patid=' + patId + '&unit=' + suId + '&fid=' + fid + '&menu=su&prgid=<%=ProgramID%>&refno=&ccm=rebind&cet=' + grdPhrClientId;
                window.openWinEntryMaximize(url);
            }

            function printPreviewQuestionForm(tno, regNo, formId) {
                var obj = {};
                obj.transactionNo = tno;
                obj.registrationNo = regNo;
                obj.questionFormID = formId;
                openPrintPreview("PopulatePrintQuestionForm", obj);
            }

            function openWinEntryMaximize(url) {
                var oWnd;
                if (!(url.includes("&rt=") || url.includes("?rt=")))
                    url = url + '&rt=<%= Request.QueryString["rt"] %>';

                oWnd = radopen(url, 'winDialogPhr');
                oWnd.maximize();
            }

            function openWinDukcapil() {
                _lastProcessID = '0';
                var nik = $find("<%= txtDisdukcapil.ClientID %>");
                if (nik.get_value() != '') {
                    $.ajax({
                        type: "POST",
                        url: "../../../WebService/RegistrationWS.asmx/GetPasienByNikDukcapil",
                        data: "{'nik':'" + nik.get_value() + "'}", // if ur method take parameters
                        contentType: "application/json; charset=utf-8",
                        success: function (response) {
                            var result = response.d;
                            if (result == '') radopen("DukcapilDialog.aspx?nik=" + nik.get_value() + "&type=<%= RegistrationType %>" + "&info=0", "winPatient");
                        },
                        dataType: "json",
                        failure: function (response) {
                            var result = response.d;
                        }
                    });
                }
            }

            function openWinRegAppt(mode, apptNo, recID, lastProcessID) {
                _lastProcessID = lastProcessID;
                var oWnd;
                oWnd = radopen("RegistrationDetail.aspx?md=" + mode + "&apptNo=" + apptNo + "&id=" + recID + "&rt=<%= RegistrationType %>" + "&gd=<%= BuildingID %>", "winReg");
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinReg(mode, patientId, lastProcessID) {
                if ("<%=AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPaymentCheckBeforePatientTrans).ToString()%>" == "True") {
                    openWinRegWithCheckPayment(mode, patientId, lastProcessID);
                } else {
                    continueReg(mode, patientId, lastProcessID);
                }
            }

            function openWinRegWithCheckPayment(mode, patientId, lastProcessID) {
                $.ajax({
                    type: "POST",
                    url: "../../../WebService/BillingChargeService.asmx/RemainingAmountConfirm",
                    data: "{'patientId':'" + patientId + "','beforeRegNo':''}", // if ur method take parameters
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        var result = response.d;
                        if (result != '') {
                            if (confirm('This patient has remain amount. Continue Registration ?' + result)) {
                                continueReg(mode, patientId, lastProcessID);
                            }
                        }
                        else {
                            continueReg(mode, patientId, lastProcessID);
                        }
                    },
                    dataType: "json",
                    failure: function (response) {
                        var result = response.d;
                    }
                });
            }
            function continueReg(mode, patientId, lastProcessID) {
                _lastProcessID = lastProcessID;
                var oWnd;
                oWnd = radopen("RegistrationDetail.aspx?md=" + mode + "&id=" + patientId + "&rt=<%= RegistrationType %>" + "&gd=<%= BuildingID %>", "winReg");
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinRegBPJS(mode, recID, sep, lastProcessID) {
                _lastProcessID = lastProcessID;
                var oWnd;
                oWnd = radopen("RegistrationDetail.aspx?md=" + mode + "&id=" + recID + "&sep=" + sep + "&type=bpjs&rt=<%= RegistrationType %>" + "&gd=<%= BuildingID %>", "winReg");
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinRegByNoRMFromLokadok(mode, recID, norm, lokaApptId, lastProcessID) {
                _lastProcessID = lastProcessID;
                var oWnd;
                oWnd = radopen("RegistrationDetail.aspx?md=" + mode + "&id=" + recID + "&norm=" + norm + "&lokaapptid=" + lokaApptId + "&rt=<%= RegistrationType %>" + "&gd=<%= BuildingID %>", "winReg");
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinTransfer(mode, recID, regNo, lastProcessID, regType) {
                _lastProcessID = lastProcessID;
                var oWnd;
                oWnd = radopen("RegistrationDetail.aspx?md=" + mode + "&id=" + recID + "&reg=" + regNo + "&rt=" + regType + "&trans=1" + "&gd=<%= BuildingID %>", "winReg");
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }
            function openWinReferToOtherServiceUnit(mode, recId, regNo) {
                var oWnd = window.$find("<%= winReg.ClientID %>");
                oWnd.setUrl("../../../Module/Charges/ServiceUnit/ServiceUnitTransaction/ReferToOtherServiceUnit.aspx?md=" + mode + "&id=" + recId + "&reg=" + regNo + "&rt=OPR&trans=1" + '&type=<%= Page.Request.QueryString["type"] %>');
                oWnd.show();
            }
            function openWinPatient(mode, recID, patient, lastProcessID) {
                _lastProcessID = lastProcessID;
                oWnd = radopen("PatientDetail.aspx?md=" + mode + "&md2=" + mode + "&pid=" + recID + "&pt=" + patient + "&rt=<%= RegistrationType %>", "winPatient");
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinChangePatient(apptno) {
                _lastProcessID = '10';
                oWnd = radopen("AppointmentChangePatientDialog.aspx?AppointmentNo=" + apptno, "winPatient");
                //oWnd.maximize();
            }

            function openWinQue(RegistrationNo, lastProcessID) {
                _lastProcessID = lastProcessID;
                var que = prompt("Please enter new que number");
                if (que != null) {
                    __doPostBack("<%= grdRegisteredList.UniqueID %>", "rebind:" + RegistrationNo + "|" + que);
                }
            }

            function openWinPatientBPJS(mode, sep, unit, lastProcessID) {
                _lastProcessID = lastProcessID;
                oWnd = radopen("PatientDetail.aspx?md=" + mode + "&md2=" + mode + "&sep=" + sep + "&unit=" + unit + "&type=bpjs&rt=<%= RegistrationType %>", "winPatient");
                //oWnd.maximize();
            }

            function openWinRegFromAppt(apptNo) {
                _lastProcessID = '1';
                var oWnd;
                oWnd = radopen("RegistrationDetail.aspx?md=new&apptNo=" + apptNo + "&rt=<%= RegistrationType %>" + "&gd=<%= BuildingID %>", "winReg");
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinPatientFromAppt(apptNo) {
                _lastProcessID = '1';
                radopen("PatientDetail.aspx?md=new&md2=new&apptNo=" + apptNo + "&pt=patient&rt=<%= RegistrationType %>", "winPatient");
            }

            function openRegPrintOpt(regNo) {
                _lastProcessID = 'printopt';
                radopen("RegistrationPrint.aspx?regno=" + regNo + "&rt=<%= RegistrationType %>", "winPrintOpt");
            }

            function openPatientPrintOpt(patientID) {
                _lastProcessID = 'printopt';
                radopen("PatientPrint.aspx?patientID=" + patientID, "winPrintOpt");
            }

            function openVerifKYC() {
                _lastProcessID = 'KYC';
                var oWnd = $find("<%= winDialog.ClientID %>");
                            var url = '<%= Temiang.Avicenna.Common.Helper.UrlRoot() %>/Module/RADT/SatuSehat/SatuSehatKYC.aspx'

                            oWnd.setUrl(url);
                            oWnd.setSize(700, 600);
                            oWnd.set_title('Satusehat KYC Verification Profile');
                            oWnd.show();
            }

            function rowConfirmed(patientID) {
                if (confirm('Are you sure to verify status success for the selected patient?')) {
                    __doPostBack("<%= grdPatient.UniqueID %>", 'confirmed:' + patientID);
                }
            }

            function openAppointmentPrint(appointmentNo) {
                _lastProcessID = 'printopt';
                radopen("AppointmentPrint.aspx?appointmentNo=" + appointmentNo, "winPrintOpt");
            }

            function openReservationAppt(mode, reservationtNo) {
                _lastProcessID = '1';
                radopen("ReservationDetail.aspx?md=" + mode + "&reservationtNo=" + reservationtNo + "&rt=<%= RegistrationType %>", "winReservation");
            }

            function openWinRegFromReservation(reservationtNo) {
                _lastProcessID = '1';
                var oWnd;
                oWnd = radopen("RegistrationDetail.aspx?md=new&reseNo=" + reservationtNo + "&rt=<%= RegistrationType %>" + "&gd=<%= BuildingID %>", "winReg");
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function resendHL7Message(regNo) {
                if (confirm('Are you sure to resend HL7 message for this registration?'))
                    __doPostBack("<%= grdRegisteredList.UniqueID %>", "resend:" + regNo);
            }

            function sendToLokadok(pid) {
                __doPostBack("<%= grdPatient.UniqueID %>", "resend:" + pid);
            }

            function viewPrescHistory(patientID) {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl('../../../Module/Charges/Dispensary/PrescriptionSales/PrescriptionHistoryDialog.aspx?pid=' + patientID + "&rt=reg");

                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function onClientClose(oWnd, args) {
                switch (_lastProcessID) {
                    case '0': // win Patient, openWinDukcapil, openWinReg('new') dicodebehind
                        if (oWnd.argument && oWnd.argument.mode != null) {
                            var arg = oWnd.argument.mode.split('|');
                            if (arg[0] == 'new') {
                                if (confirm('Continue to Registration Menu?'))
                                    openWinReg('new', arg[1], '0');
                            }
                            else if (arg[0] == 'reg' && arg[1] == 'new') // Setelah save new Registration
                            {
                                if ("<%= AppParameter.IsYes(AppParameter.ParameterItem.IsShowScanDocumentConfirm).ToString().ToLower() %>" == "true" && confirm('Continue to Scan Guarantor Document?')) {
                                    var regno = arg[3];
                                    var patid = arg[2];
                                    openWinQuestionFormCheckList(regno, patid)
                                }
                            }

                            __doPostBack("<%= grdPatient.UniqueID %>", "rebind");
                            oWnd.argument = '|';
                        }
                        break;
                    case '1':
                        if (oWnd.argument && oWnd.argument.mode != null) {
                            var arg = oWnd.argument.mode.split('|');
                            if (arg[0] == 'new')
                                if (confirm('Continue to Registration Menu?')) {
                                    if (arg.length == 2)
                                        openWinReg('new', arg[1], '1');
                                    else
                                        openWinRegAppt('new', arg[1], arg[2], '1');
                                }
                            __doPostBack("<%= grdAppointment.UniqueID %>", "rebind");
                        }
                        break;
                    case '2': // openWinReg(edit), openWinQue, openWinTransfer
                        __doPostBack("<%= grdRegisteredList.UniqueID %>", "rebind:");
                        break;
                    case '3': // onVoidRegistration
                        if (oWnd.argument && oWnd.argument.result != null) {
                            if (oWnd.argument.result == '1') {
                                __doPostBack("<%= grdRegisteredList.UniqueID %>", "rebind:");
                            }
                            else {
                                alert('Registration is unavailable to void.');
                            }
                        }
                        break;
                    case '4': // onEditGuarantor
                        __doPostBack("<%= grdRegisteredList.UniqueID %>", "rebind:");
                        break;
                    case '5': // onEditBedNo
                        __doPostBack("<%= grdRegisteredList.UniqueID %>", "rebind:");
                        break;
                    case '6': // onEditPhysician 
                        __doPostBack("<%= grdRegisteredList.UniqueID %>", "rebind:");
                        break;
                    case '7':
                        if (oWnd.argument && oWnd.argument.mode != null) {
                            var arg = oWnd.argument.mode.split('|');
                            if (arg[0] == 'new')
                                if (confirm('Continue to Registration Menu?'))
                                    openWinRegBPJS('new', arg[1], arg[2], '0');

                            __doPostBack("<%= grdBPJS.UniqueID %>", "rebind");
                            oWnd.argument = '|';
                        }
                        break;
                    case '8':
                        __doPostBack("<%= gridApptLokadok.UniqueID %>", "rebind:");
                        break;
                    case 'printopt':
                        if (oWnd.argument && oWnd.argument.print != null) {
                            radopen('<%=Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx")%>', "winPrintPreview");
                        }
                        break;
                    case '9': // openWinQuestionFormCheckList
                        __doPostBack("<%= grdRegisteredList.UniqueID %>", "rebind:");
                        break;
                    case '10':
                        __doPostBack("<%= grdAppointment.UniqueID %>", "rebind");
                        break;
                    case 'googleform':
                        if (oWnd.argument && oWnd.argument.mode != null) {
                            var arg = oWnd.argument.mode.split('|');
                            if (arg[0] == 'new')
                                if (confirm('Continue to Registration?')) {
                                    openWinRegGoogleForm('new', arg[1], arg[2], 'googleform');
                                }
                            __doPostBack("<%= grdGoogleForm.UniqueID %>", "rebind");
                        }
                        break;
                    case 'reg': // openWinPatient from registration list
                        __doPostBack("<%= grdRegisteredList.UniqueID %>", "rebind:");
                        break;
                    case 'appt': // openWinPatient from appointment list
                        __doPostBack("<%= grdAppointment.UniqueID %>", "rebind");
                        break;
                    case 'phr': // openWinPHR from registration list
                        {
                            oWnd.setUrl("about:blank"); // Sets url to blank for release variable

                            //get the transferred arguments from MasterDialogEntry
                            var arg = args.get_argument();
                            if (arg != null) {
                                if (arg.callbackMethod === 'submit') {
                                    __doPostBack(arg.eventTarget, arg.eventArgument);
                                } else {
                                    if (arg.callbackMethod === 'rebind') {
                                        var ctl = $find(arg.eventTarget);
                                        if (typeof ctl.rebind == 'function') {
                                            ctl.rebind();
                                        } else {
                                            var masterTable = $find(arg.eventTarget).get_masterTableView();
                                            masterTable.rebind();
                                        }
                                    }
                                }
                            }
                        }
                        break;
                }
                oWnd = null;
            }

            function onClientTabSelected(sender, eventArgs) {
                var tabIndex = eventArgs.get_tab().get_index();
                var tabCount = sender.get_tabs().get_count();

                switch (tabIndex) {
                    case 0:
                        __doPostBack("<%= grdPatient.UniqueID %>", "rebind");
                        break;
                    case 1:
                        if (tabCount == 3)
                            __doPostBack("<%= grdAppointment.UniqueID %>", "rebind");
                        else
                            __doPostBack("<%= grdRegisteredList.UniqueID %>", "rebind:");
                        break;
                    case 2:
                        __doPostBack("<%= grdRegisteredList.UniqueID %>", "rebind:");
                        break;
                    case 3:
                        __doPostBack("<%= grdRegisteredList.UniqueID %>", "rebind:");
                        break;
                    case 4:
                        __doPostBack("<%= grdRegisteredList.UniqueID %>", "rebind:");
                        break;
                    case 5:
                        __doPostBack("<%= grdRegisteredList.UniqueID %>", "rebind:");
                        break;
                }
            }

            function onVoidRegistration(voidInfo, regNo) {
                _lastProcessID = '3';
                radopen("VoidRegistrationDialog.aspx?regNo=" + regNo + "&rt=<%= RegistrationType %>", "winVoid");
            }
            function onEditGuarantor(regNo) {
                _lastProcessID = '4';
                radopen("EditGuarantorDialog.aspx?regNo=" + regNo + "&rt=<%= RegistrationType %>", "winReg");
            }
            function onEditFromRegistrationNo(patientID, regNo) {
                var oWnd = window.$find("<%= winOpen.ClientID %>");
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/EditFromRegistrationNo.aspx?patid=' + patientID + '&regno=' + regNo;
                oWnd.setUrl(url);
                oWnd.show();

                oWnd.setSize(500, 300);
                oWnd.center();
            }

            function editFromRegistrationNo() {
                openWinEntry(url, 700, 400);
            }
            function onEditBedNo(regNo) {
                _lastProcessID = '5';
                radopen("EditBedNoDialog.aspx?regNo=" + regNo, "winEdit");
            }
            function onEditPhysician(regNo, unitID) {
                _lastProcessID = '6';
                radopen("EditPhysicianDialog.aspx?rt=<%= RegistrationType %>&regNo=" + regNo + "&unitID=" + unitID, "winEdit");
            }

            function openWinRegistrationInfo(regNo) {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var lblToBeUpdate = "noti_" + regNo;

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }

            /*function openWinQuestionFormCheckList(regNo, unitID, regType) {
            radopen("RegistrationDocumentCheckList.aspx?regNo=" + regNo + "&unitID=" + unitID + "&regType=" + regType, "winPrint");
            }*/

            function openWinQuestionFormCheckList(regno, patid) {
                _lastProcessID = '9';
                var oWnd = $find("<%= winDocsOption.ClientID %>");
                var lblToBeUpdate = "noti2_" + regno;

                oWnd.setUrl('RegistrationDocumentCheckList.aspx?regno=' + regno + '&patid=' + patid + '&lblRegistrationInfo=' + lblToBeUpdate);
                oWnd.set_title('Document Checklist');
                oWnd.setSize(900, 730);
                oWnd.center();
                oWnd.show();
            }

            function openWinPatientDocument(patid) {
                _lastProcessID = 'PD';
                var oWnd = $find("<%= winDialog.ClientID %>");

                var url = '<%=string.Format("{0}/Module/RADT/Cpoe/EmrCommon/PatientDocument/PatientDocumentHist.aspx?progid=01.15.07&patid=' + patid+ '&logid={1}&uid={2}'", "" ,AppSession.UserLogin.UserLogID,AppSession.UserLogin.UserID)%>;
                oWnd.setUrl(url);
                oWnd.setSize(1000, 500);
                oWnd.set_title('Patient Document');
                oWnd.show();
            }
            function openWinSatusehatConsent(patid) {
                _lastProcessID = 'SSC';
                var oWnd = $find("<%= winDialog.ClientID %>");

                var url = '<%= Helper.UrlRoot() %>/Module/RADT/SatuSehat/SatuSehatConsent.aspx?patid=' + patid;
                oWnd.setUrl(url);
                oWnd.setSize(700, 300);
                oWnd.set_title('Satusehat Consent Status');
                oWnd.show();
            }
            function openWinPhysicinTeamOp(regno) {
                var oWnd = window.$find("<%= winOpen.ClientID %>");
                var url = '<%= Helper.UrlRoot() %>/Module/Charges/ServiceUnit/ServiceUnitTransaction/PhysicianTeamDialog.aspx?reg=' + regno;
                oWnd.setUrl(url);
                oWnd.show();

                oWnd.setSize(1000, 600);
                oWnd.center();
            }

            function generateRegistration() {
                if (confirm('Are you sure to generate registration from all pending appointment?'))
                    __doPostBack("<%= grdAppointment.UniqueID %>", "generate");
            }


            var delay = (function () {
                var timer = 0;
                return function (callback, ms) {
                    clearTimeout(timer);
                    timer = setTimeout(callback, ms);
                };
            })();

            function QuickSearchPatientKeypress(sender, args) {
                var c = args.get_keyCode();
                if (c == 13) {
                    ApplyQuickSearchPatient();
                }
                else {
                    delay(function () { ApplyQuickSearchPatient(); }, 1000);
                }
            }
            function ApplyQuickSearchPatient() {
                __doPostBack("<%=grdPatient.UniqueID %>", "rebind:");
            }
        </script>

        <script type="text/javascript">
            var docOriginalUrl = '<%=Page.ResolveUrl("~/Module/Charges/ServiceUnit/ServiceUnitTransaction/")%>';
            var BASEUrl = '<%=Page.ResolveUrl("~/Module/")%>';

            $.download = function (url, data, method) {
                //url and data options required
                url = docOriginalUrl + url;
                if (url && data) {
                    //data can be string of parameters or array/object
                    data = typeof data == 'string' ? data : $.param(data);
                    //split params into form inputs
                    var inputs = '';
                    $.each(data.split('&'), function () {
                        var pair = this.split('=');
                        inputs += '<input type="hidden" name="' + pair[0] + '" value="' + pair[1] + '" />';
                    });
                    //send request
                    $('<form action="' + url + '" method="' + (method || 'post') + '">' + inputs + '</form>').appendTo('body').submit().remove();
                };
            };

            function gotoAddUrl(type, regno, pid, unit, room) {
                var url = BASEUrl + 'RADT/EpisodeAndHistory/EpisodeAndHistoryDetail.aspx?rt=' + type + '&regno=' + regno + '&pid=' + pid + '&unit=' + unit + '&room=' + room;
                window.location.href = url;
            }


            function openWinQuestionForm(regno, sid) {
                var oWnd = window.$find("<%= winQuestionForm.ClientID %>");
                oWnd.setUrl(docOriginalUrl + "QuestionFormSelection.aspx?regno=" + regno + "&sid=" + sid + "&rwndrnd=" + Math.random());
                oWnd.show();
            }

            function openWinUpload(pid, regno, fromGridClientId) {
                var oWnd = window.$find("<%= winUpload.ClientID %>");
                oWnd.setUrl(docOriginalUrl + "PatientDocumentUpload.aspx?pid=" + pid + "&regno=" + regno + "&gcid=" + fromGridClientId);
                oWnd.show();
            }

            function onWinUploadClientClose(oWnd) {
                //Jika apply di click
                var arg = oWnd.argument;
                if (arg) {
                    var id = oWnd.argument;
                    __doPostBack(id, '');
                    var grvPatientDoc = window.$find(id).get_masterTableView();
                    grvPatientDoc.rebind();
                }
                oWnd = null;
            }

            function openWinForm(rt, patid, fid) {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl('PatientFormDialog.aspx?rt=' + rt + '&patid=' + patid + '&fid=' + fid);
                oWnd.set_title('Patient Health Record');
                oWnd.show();
            }

            function openPatientDocument(patid, regno) {
                var url = '<%= Temiang.Avicenna.Common.Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/PatientDocument/PatientDocumentHist.aspx?lockreg=1&progid=<%= ProgramID %>&patid=' + patid + '&regno=' + regno;
                openWinMaxWindow(url);
            }

            function openWinMaxWindow(url) {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                var width =
                    (window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth);

                openWindow(url, width - 40, height - 40);
            }

            function openWindow(url, width, height) {
                var oWnd = $find("<%= winOpen.ClientID %>");
                oWnd.setUrl(url);
                oWnd.setSize(width, height);
                oWnd.center();

                // Cek position
                var pos = oWnd.getWindowBounds();
                if (pos.y < 0)
                    oWnd.moveTo(pos.x, 0);
                oWnd.show();
            }

            function openWinPatientFromGoogleForm(gfid) {
                _lastProcessID = 'googleform';
                radopen("PatientDetail.aspx?md=new&md2=new&gfid=" + gfid + "&pt=patient&rt=<%= RegistrationType %>", "winPatient");
            }

            function openWinRegGoogleForm(mode, gfid, recID, lastProcessID) {
                _lastProcessID = lastProcessID;
                var oWnd;
                oWnd = radopen("RegistrationDetail.aspx?md=" + mode + "&gfid=" + gfid + "&id=" + recID + "&rt=<%= RegistrationType %>", "winReg");
                oWnd.maximize();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" ID="winDocsOption" Animation="None" Behaviors="Move, Close"
        Width="900px" Height="700px" VisibleStatusbar="false" ShowContentDuringLoad="False"
        Modal="true" />
    <telerik:RadWindow runat="server" ID="winDialog" Animation="None" Behaviors="Move, Close"
        Width="800px" Height="600px" VisibleStatusbar="false" ShowContentDuringLoad="False"
        Modal="true" />
    <telerik:RadWindow runat="server" Animation="None" Width="600px" Height="500px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" Title="Upload Patient Document"
        OnClientClose="onWinUploadClientClose" ID="winUpload">
    </telerik:RadWindow>
    <telerik:RadWindow runat="server" Animation="None" Width="1000px" Height="500px"
        Behavior="Close, Move" ShowContentDuringLoad="False" VisibleStatusbar="False"
        Modal="true" Title="Question Form" ID="winQuestionForm">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadAjaxPanel ID="radAjaxPanel" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false">
    </telerik:RadAjaxPanel>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>

    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadWindowManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdAppointment" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdRegisteredList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchPatient">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchDateOfBirth">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchPhoneNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchAddress">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnDisdukcapil">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchSsn">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchGuarantorCardNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPatient">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchClusterAppointment">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAppointment" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchAppointmentParamedicID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAppointment" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchNameMedical">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAppointment" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchAppointmentNote">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAppointment" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAppointmentStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAppointment" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBpjsCallerGroup">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAppointment" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAppointment" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdAppointment">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAppointment" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchClusterAppointment">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdReservation" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchAppointment">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdReservation" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdReservation" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdReservation">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdReservation" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisteredList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchRegistrationTime">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisteredList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchRegistrationNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisteredList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchRegPatient">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisteredList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchRegPhysician">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisteredList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchInclCheckOut">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisteredList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchConfirmedAttendence">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisteredList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchServiceUnit_Click">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisteredList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdRegisteredList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisteredList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchReservationDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdReservation" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchReservation">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdReservation" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchBPJS_BPJSNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdBPJS" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchBPJS_SEPDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdBPJS" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchBPJS_PatientNameBPJS">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdBPJS" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchBPJS_ServiceUnitBPJS">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdBPJS" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnUpdateApptLokadok">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridApptLokadok" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfoApptLoka" />
                    <telerik:AjaxUpdatedControl ControlID="lblLokaDownloadInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchApptLokadok">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridApptLokadok" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfoApptLoka" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchApptLokaByPhysician">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridApptLokadok" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfoApptLoka" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdGoogleForm">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdGoogleForm" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchGF1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdGoogleForm" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchGF2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdGoogleForm" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Style="z-index: 7001"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
        ReloadOnShow="True" OnClientClose="onClientClose" ShowContentDuringLoad="false">
        <Windows>
            <telerik:RadWindow ID="winVoid" Width="1000px" Height="500px" runat="server">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winEdit" Width="1000px" Height="500px" runat="server">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winReservation" Width="1100px" Height="350px" runat="server">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winPatient" Width="1200px" Height="640px" runat="server">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winReg" Width="1200px" Height="640px" runat="server">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winPrintOpt" Width="400px" Height="300px" runat="server" Title="Select report then click Ok button">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winOpen" Width="1000px" Height="600px" runat="server">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winPrintPreview" Behavior="Maximize,Move,Close" Width="1000px"
                Height="630px" runat="server" Title="Preview">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winDialogPhr" Width="900px" Height="600px" runat="server"
                ShowContentDuringLoad="false" Behaviors="Maximize,Close,Move" Modal="True">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <div style="height: 2px;">
    </div>
    <asp:HiddenField runat="server" ID="hdnServiceUnitID" />
    <asp:HiddenField runat="server" ID="hdnRegistrationNo" />
    <asp:HiddenField runat="server" ID="hdnPatientID" />
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop" OnClientTabSelected="onClientTabSelected">
        <Tabs>
            <telerik:RadTab runat="server" Text="Patient" PageViewID="pgPatientList" Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Google Form" PageViewID="pgGoogleForm">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Pending Appointment" PageViewID="pgAppointment"
                Visible="false">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Reservation" PageViewID="pgReservation" Visible="false">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="BPJS" PageViewID="pgBPJS">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Registration" PageViewID="pgOutPatientList">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Appointment Lokadok" PageViewID="pgAppointmentLokadok"
                Visible="false">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgPatientList" runat="server" Selected="true">
            <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" valign="top">
                            <table>
                                <tr style="display: none">
                                    <td class="label">
                                        <asp:Label ID="Label10" runat="server" Text="Date Appointment"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadDatePicker ID="txtDateAppt" runat="server" Width="110px">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td width="30px">
                                        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchPatient_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPatientSearch" runat="server" Text="Patient Name / Medical No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox runat="server" ID="txtPatientSearch" Width="300px">
                                            <ClientEvents OnKeyPress="QuickSearchPatientKeypress"></ClientEvents>
                                        </telerik:RadTextBox>
                                    </td>
                                    <td width="30px">
                                        <asp:ImageButton ID="btnSearchPatient" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchPatient_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblDateOfBirth" runat="server" Text="Date Of Birth"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtDateOfBirth" runat="server" Width="110px">
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="30px">
                                        <asp:ImageButton ID="btnSearchDateOfBirth" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchPatient_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblGuarantorCardNo" runat="server" Text="BPJS No / Guarantor Card No"></asp:Label></td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtGuarantorCardNo" runat="server" Width="300px">
                                        </telerik:RadTextBox></td>
                                    <td width="30px">
                                        <asp:ImageButton ID="btnSearchGuarantorCardNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchPatient_Click" ToolTip="Search" />
                                    </td>
                                    <td />
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Button ID="btnNewPatient" runat="server" Text="New Patient" Width="100px" OnClientClick="javascript:openWinPatient('new','','patient','0');return false;" />
                                        &nbsp;<asp:Button ID="btnNewDirectPatient" runat="server" Text="New Direct Patient"
                                            Visible="false" Width="120px" OnClientClick="javascript:openWinPatient('new','','directpatient','0');return false;" />
                                        &nbsp;<asp:LinkButton ID="btnKYCVerification" runat="server" CssClass="kyc-button" OnClientClick="javascript:openVerifKYC();return false;">
                                            <img src="../../../Images/SatuSehatSmall.png" alt="KYC" style="vertical-align: middle; width: 16px; height: 16px;" />
                                            KYC Verification
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" valign="top">
                            <table>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtPhoneNo" runat="server" Width="100px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td width="30px">
                                        <asp:ImageButton ID="btnSearchPhoneNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchPatient_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtAddress" runat="server" Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td width="30px">
                                        <asp:ImageButton ID="btnSearchAddress" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchPatient_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr runat="server" id="trDisdukcapil">
                                    <td class="label">NIK/KTP (DUKCAPIL)</td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtDisdukcapil" runat="server" Width="300px">
                                        </telerik:RadTextBox></td>
                                    <td width="30px">
                                        <asp:ImageButton ID="btnDisdukcapil" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchPatient_Click" OnClientClick="openWinDukcapil();" ToolTip="Search" />
                                    </td>
                                    <td />
                                </tr>
                                <tr runat="server" id="trSsn">
                                    <td class="label">
                                        <asp:Label ID="lblSsn" runat="server" Text="SSN"></asp:Label></td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtSsn" runat="server" Width="300px">
                                        </telerik:RadTextBox></td>
                                    <td width="30px">
                                        <asp:ImageButton ID="btnSearchSsn" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchPatient_Click" ToolTip="Search" />
                                    </td>
                                    <td />
                                </tr>

                            </table>
                        </td>
                    </tr>
                </table>
            </cc:CollapsePanel>
            <telerik:RadGrid ID="grdPatient" runat="server" OnNeedDataSource="grdPatient_NeedDataSource"
                AutoGenerateColumns="False" AllowPaging="True" PageSize="15" AllowSorting="True"
                GridLines="None" OnDetailTableDataBind="grdPatient_DetailTableDataBind">
                <MasterTableView DataKeyNames="PatientID" GroupLoadMode="client">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TemplateColumn" Groupable="false">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsAlive").Equals(false) ? string.Format("<a href=\"#\" return false;\"><img src=\"../../../Images/Rip16.png\" border=\"0\" title=\"Decease\" /></a>")
                                     : (DataBinder.Eval(Container.DataItem, "IsBlackList").Equals(true) ? string.Format("<a href=\"#\" return false;\"><img src=\"../../../Images/Toolbar/blacklist.png\" border=\"0\" title=\"Blacklist\" /></a>")
                                     : (DataBinder.Eval(Container.DataItem, "IsActive").Equals(false) ? string.Format("<a href=\"#\" return false;\"><img src=\"../../../Images/Toolbar/new16_d.png\" border=\"0\" title=\"Non-Active\" /></a>")
                                     : GetUrlForNewRegistration(Container))))%>
                            </ItemTemplate>
                            <HeaderStyle Width="35px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle Width="35px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "PatientID").Equals(true) || DataBinder.Eval(Container.DataItem, "IsActive").Equals(false) ? string.Empty
                                                                   : string.Format("<a href=\"#\" onclick=\"openPatientPrintOpt('{0}'); return false;\"><img src=\"../../../Images/Toolbar/print16.png\" border=\"0\" title=\"Print\" /></a>",
                               DataBinder.Eval(Container.DataItem, "PatientID"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" UniqueName="KYC" HeaderText="KYC">
                            <HeaderStyle Width="35px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# Convert.ToBoolean(Eval("IsActive")) ? (Convert.ToBoolean(Eval("IsKYC"))
                                        ? "<img src='../../../Images/SatuSehatSmall.png' border='0' alt='KYC' title='Already Verified' style='filter: invert(100%) grayscale(100%); cursor: not-allowed;' />"
                                        : "<a href='#' onclick=\"rowConfirmed('" + Eval("PatientID") + "'); return false;\"><img src='../../../Images/SatuSehatSmall.png' border='0' alt='KYC' title='SatuSehat KYC Verification' /></a>") : "" 
                                %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderText="Patient ID">
                            <ItemTemplate>
                                <%#GetItemTemplatePatientID(Container)%>
                            </ItemTemplate>
                            <HeaderStyle Width="120px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Salutation" HeaderText="Salutation" UniqueName="Salutation"
                            SortExpression="Salutation" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="250px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <ItemTemplate>
                                <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "Salutation"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                            SortExpression="MedicalNo">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="OldMedicalNo" HeaderText="Old Medical No" UniqueName="OldMedicalNo"
                            SortExpression="OldMedicalNo">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Ssn" HeaderText="SSN" UniqueName="Ssn"
                            SortExpression="Ssn">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn DataField="DateOfBirth" HeaderText="Date Of Birth" UniqueName="DateOfBirth"
                            SortExpression="DateOfBirth">
                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridDateTimeColumn>
                        <telerik:GridBoundColumn DataField="Address" HeaderText="Address" UniqueName="Address"
                            SortExpression="Address">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PhoneNo" HeaderText="Phone No" UniqueName="PhoneNo"
                            SortExpression="PhoneNo">
                            <HeaderStyle HorizontalAlign="Left" Width="120px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MobilePhoneNo" HeaderText="Mobile Phone No" UniqueName="MobilePhoneNo"
                            SortExpression="MobilePhoneNo">
                            <HeaderStyle HorizontalAlign="Left" Width="120px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="TemplateColumnToLokadok" Groupable="false">
                            <ItemTemplate>
                                <%# (IsStoredToLokadok(Container) ? "" :
                                     string.Format("<a href=\"#\" onclick=\"sendToLokadok('{0}'); return false;\"><img src=\"../../../Images/Toolbar/arrowup_blue16.png\" border=\"0\" title=\"Send To Lokadok\" /></a>",
                                     DataBinder.Eval(Container.DataItem, "PatientID")))%>
                            </ItemTemplate>
                            <HeaderStyle Width="35px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="PrescriptionHistory" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <a href="#" onclick="viewPrescHistory('<%# DataBinder.Eval(Container.DataItem, "PatientID") %>'); return false;">
                                    <img src="../../../Images/Medical/prescription16.png" border="0" alt="Prescription History List" title="Prescription History List" /></a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="grdDetail" DataKeyNames="PatientID" AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="500px" DataField="Notes" HeaderText="Notes"
                                    UniqueName="Notes" SortExpression="Notes" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridTemplateColumn />
                            </Columns>
                        </telerik:GridTableView>
                        <telerik:GridTableView Name="grdBlacklistHist" DataKeyNames="PatientID,IsBlackList,LastUpdateDateTime"
                            AutoGenerateColumns="false" PageSize="5">
                            <Columns>
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsBlackList" HeaderText="Blacklist"
                                    UniqueName="IsBlackList" SortExpression="IsBlackList" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="LastUpdateDateTime" HeaderText="Update Date"
                                    UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime" DataType="System.DateTime"
                                    DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                    <HeaderStyle HorizontalAlign="Center" Width="155px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="UserName" HeaderText="Update By"
                                    UniqueName="UserName" SortExpression="UserName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgAppointment" runat="server">
            <cc:CollapsePanel ID="CollapsePanel2" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" valign="top">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblAppointmentDate" runat="server" Text="Appointment Date"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtAppointmentDate" runat="server" Width="110px">
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchAppointment_Click" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblAppointmentSearch" runat="server" Text="Medical No / Patient Name"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtAppointmentSearch" runat="server" Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchNameMedical" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchAppointment_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblAppointmentNote" runat="server" Text="Notes"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtAppointmentNote" runat="server" Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchAppointmentNote" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchAppointment_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">Appointment Status
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboAppointmentStatus" runat="server" Width="300px" AllowCustomText="true"
                                            Filter="Contains">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnAppointmentStatus" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchAppointment_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                                            Filter="Contains">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchClusterAppointment" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchAppointment_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblAppointmentParamedicID" runat="server" Text="Physician"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboAppointmentParamedicID" Width="300px"
                                            EnableLoadOnDemand="true" MarkFirstMatch="true" HighlightTemplatedItems="true"
                                            OnItemDataBound="cboParamedicID_ItemDataBound" OnItemsRequested="cboAppointmentParamedicID_ItemsRequested">
                                            <FooterTemplate>
                                                Note : Show max 10 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchAppointmentParamedicID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchAppointment_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblAppointmentGuarantor" runat="server" Text="Guarantor"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboAppointmentGuarantor" Width="300px"
                                            EnableLoadOnDemand="true" MarkFirstMatch="true" HighlightTemplatedItems="true"
                                            OnItemDataBound="cboGuarantor_ItemDataBound" OnItemsRequested="cboAppointmentGuarantorID_ItemsRequested">
                                            <FooterTemplate>
                                                Note : Show max 10 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchAppointmentGuarantorID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchAppointment_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">BPJS Caller Group
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboCallerGroup" runat="server" Width="300px">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="" Value="" Selected="true" />
                                                <telerik:RadComboBoxItem Text="Mobile JKN" Value="mobilejkn" />
                                                <telerik:RadComboBoxItem Text="Briging Antrian Online" Value="antrol" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnBpjsCallerGroup" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchAppointment_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </cc:CollapsePanel>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Button ID="btnGenerateRegistration" runat="server" Text="Generate Registration"
                            Width="150px" OnClientClick="javascript:generateRegistration();return false;" />
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdAppointment" runat="server" OnNeedDataSource="grdAppointment_NeedDataSource"
                AutoGenerateColumns="False" ShowGroupPanel="false" AllowPaging="True" PageSize="15"
                AllowSorting="True" GridLines="None">
                <MasterTableView DataKeyNames="AppointmentNo" GroupLoadMode="client">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="AppointmentDate" HeaderText="Appointment Date "
                                    FormatString="{0:dd/MM/yyyy}"></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="AppointmentDate" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridBoundColumn DataField="Status" HeaderText="Status"
                            UniqueName="Status" SortExpression="Status" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle Width="35px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openAppointmentPrint('{0}'); return false;\"><img src=\"../../../Images/Toolbar/print16.png\" border=\"0\" title=\"Print Label\" /></a>", DataBinder.Eval(Container.DataItem, "AppointmentNo"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="TemplateColumn" Groupable="false">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsAlive").Equals(false) ? string.Format("<a href=\"#\" return false;\"><img src=\"../../../Images/Rip16.png\" border=\"0\" title=\"Decease\" /></a>")
                                     : (DataBinder.Eval(Container.DataItem, "IsBlackList").Equals(true) ? string.Format("<a href=\"#\" return false;\"><img src=\"../../../Images/Toolbar/blacklist.png\" border=\"0\" title=\"Blacklist\" /></a>")
                                     : (DataBinder.Eval(Container.DataItem, "IsActive").Equals(false) ? string.Format("<a href=\"#\" return false;\"><img src=\"../../../Images/Toolbar/new16_d.png\" border=\"0\" title=\"Non-Active\" /></a>")
                                     : GetUrlForRegistration(Container))))%>
                            </ItemTemplate>
                            <HeaderStyle Width="35px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <%--<telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="" Groupable="false">
                            <ItemStyle HorizontalAlign="center" />
                            <ItemTemplate>
                                <%# GetUrlForRegistration(Container) %>
                            </ItemTemplate>
                            <HeaderStyle Width="40px" />
                        </telerik:GridTemplateColumn>--%>
                        <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="" Groupable="false">
                            <ItemStyle HorizontalAlign="center" />
                            <ItemTemplate>
                                <%# GetUrlForChangePatient(Container) %>
                            </ItemTemplate>
                            <HeaderStyle Width="40px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="AppointmentNo" HeaderText="Appointment No"
                            UniqueName="AppointmentNo" SortExpression="AppointmentNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px" />
                        <telerik:GridBoundColumn DataField="FormattedNo" HeaderText="Queueing No"
                            UniqueName="FormattedNo" SortExpression="FormattedNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" />
                        <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="PatientID" HeaderText="Patient ID"
                            UniqueName="PatientID" SortExpression="PatientID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="MedicalNo" HeaderText="Medical No"
                            UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn Groupable="false" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <%#GetUrlForEditPatientAppt(Container)%>
                            </ItemTemplate>
                            <HeaderStyle Width="250px" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Address" HeaderText="Address" UniqueName="Address"
                            SortExpression="Address" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="AppointmentNo" HeaderText="Appoint. No"
                            UniqueName="AppointmentNo" SortExpression="AppointmentNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                            SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                            SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="50px" DataField="AppointmentTime"
                            HeaderText="Time" UniqueName="AppointmentDate" SortExpression="AppointmentTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="VisitTypeName" HeaderText="Visit Type"
                            UniqueName="VisitTypeName" SortExpression="VisitTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="AppoinmentTypeName" HeaderText="Appt. Type"
                            UniqueName="AppoinmentTypeName" SortExpression="AppoinmentTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="170px" DataField="Notes" HeaderText="Notes"
                            UniqueName="Notes" SortExpression="Notes" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="LastCreateByUserID" HeaderText="Created by"
                            UniqueName="LastCreateByUserID" SortExpression="LastCreateByUserID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </MasterTableView>
                <ClientSettings AllowDragToGroup="true" EnableRowHoverStyle="true" AllowExpandCollapse="true">
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgReservation" runat="server">
            <cc:CollapsePanel ID="CollapsePanel3" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" valign="top">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblReservationDate" runat="server" Text="Reservation Date"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtReservationDate" runat="server" Width="110px">
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchReservationDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchReservation_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" valign="top">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblReservationSearch" runat="server" Text="Patient Name / Medical No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtReservationSearch" runat="server" Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchReservation" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchReservation_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblReservationServiceUnitSearch" runat="server" Text="Service Unit"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboReservationServiceUnitSearch" runat="server" Width="300px"
                                            AllowCustomText="true" Filter="Contains">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchReservationServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchReservation_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </cc:CollapsePanel>
            <telerik:RadGrid ID="grdReservation" runat="server" OnNeedDataSource="grdReservation_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ReservationNo">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderStyle-Width="50px" UniqueName="TemplateColumn"
                            Groupable="false">
                            <ItemTemplate>
                                <%#GetUrlForNewRegistrationFromReservation(Container)%>
                            </ItemTemplate>
                            <HeaderStyle Width="35px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ReservationNo" HeaderText="Reservation No"
                            UniqueName="ReservationNo" SortExpression="ReservationNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ReservationDate"
                            HeaderText="Date" UniqueName="ReservationDate" SortExpression="ReservationDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="MedicalNo" HeaderText="Medical No"
                            UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="250px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <ItemTemplate>
                                <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Address" HeaderText="Address" UniqueName="Address"
                            SortExpression="Address" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                            SortExpression="RoomName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="BedID" HeaderText="Bed No"
                            UniqueName="BedID" SortExpression="BedID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                    <EditFormSettings UserControlName="ReservationDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ReservationEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgBPJS" runat="server">
            <cc:CollapsePanel ID="CollapsePanel5" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" valign="top">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label4" runat="server" Text="No SEP"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtNoSEP" runat="server" Width="300px" />
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchBPJS_ServiceUnitBPJS" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchBPJS_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label2" runat="server" Text="Tgl SEP"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadDatePicker ID="txtSEPDate" runat="server" Width="110px">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchBPJS_SEPDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchBPJS_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" valign="top">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label5" runat="server" Text="No Kartu"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtBPJSNo" runat="server" Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchBPJS_BPJSNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchBPJS_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label3" runat="server" Text="Nama Peserta"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtPatientNameBPJS" runat="server" Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchBPJS_PatientNameBPJS" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchBPJS_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </cc:CollapsePanel>
            <telerik:RadGrid ID="grdBPJS" runat="server" OnNeedDataSource="grdBPJS_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" AllowPaging="true" PageSize="15">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="NoSEP">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderStyle-Width="50px" UniqueName="TemplateColumn"
                            Groupable="false">
                            <ItemTemplate>
                                <%#GetUrlForNewRegistrationFromBPJS(Container)%>
                            </ItemTemplate>
                            <HeaderStyle Width="35px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="NoSEP" HeaderText="No. SEP"
                            UniqueName="NoSEP" SortExpression="NoSEP" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TanggalSEP" HeaderText="Tgl. SEP"
                            UniqueName="TanggalSEP" SortExpression="TanggalSEP" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="NomorKartu" HeaderText="No Peserta"
                            UniqueName="NomorKartu" SortExpression="NomorKartu" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="NoMR" HeaderText="No. MR"
                            UniqueName="NoMR" SortExpression="NoMR" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn Groupable="false" HeaderText="Nama Pasien">
                            <ItemTemplate>
                                <%#GetItemTemplatePatientName(Container)%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Sex" HeaderText="JK" UniqueName="Sex" SortExpression="Sex">
                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TanggalLahir" HeaderText="Tgl. Lahir"
                            UniqueName="TanggalLahir" SortExpression="TanggalLahir" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="BridgingName" HeaderText="Poli Tujuan" UniqueName="BridgingName"
                            SortExpression="BridgingName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsLakaLantas" HeaderText="Laka Lantas"
                            HeaderStyle-HorizontalAlign="Center" UniqueName="IsLakaLantas" SortExpression="IsLakaLantas"
                            ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgOutPatientList" runat="server">
            <cc:CollapsePanel ID="CollapsePanel4" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" style="vertical-align: top">
                            <table width="100%">
                                <tr id="pnlFilterDate" runat="server">
                                    <td class="label">
                                        <asp:Label ID="lblRegistrationDate" runat="server" Text="Registration Date"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtFromDate" runat="server" Width="110px">
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td>&nbsp;-&nbsp;
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtToDate" runat="server" Width="110px">
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchRegistration" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchRegistration_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRegistrationTime" runat="server" Text="Registration Time"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadMaskedTextBox ID="txtFromRegistrationTime" runat="server" Mask="<00..23>:<00..59>"
                                                        PromptChar="_" RoundNumericRanges="false" Width="50px">
                                                    </telerik:RadMaskedTextBox>
                                                </td>
                                                <td>&nbsp;-&nbsp;
                                                </td>
                                                <td>
                                                    <telerik:RadMaskedTextBox ID="txtToRegistrationTime" runat="server" Mask="<00..23>:<00..59>"
                                                        PromptChar="_" RoundNumericRanges="false" Width="50px">
                                                    </telerik:RadMaskedTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchRegistrationTime" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchRegistration_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchRegistrationNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchRegistration_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblFilterServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboFilterServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                                            Filter="Contains" />
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchServiceUnit_Click" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchRegistration_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" style="vertical-align: top">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblSearchRegPatient" runat="server" Text="Patient Name / Medical No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtSearchRegPatient" runat="server" Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchRegPatient" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchRegistration_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblSearchRegPhysician" runat="server" Text="Physician"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboParamedicID" Width="300px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboParamedicID_ItemDataBound"
                                            OnItemsRequested="cboParamedicID_ItemsRequested">
                                            <FooterTemplate>
                                                Note : Show max 10 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchRegPhysician" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchRegistration_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRegGuarantor" runat="server" Text="Guarantor"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboRegGuarantor" Width="300px"
                                            EnableLoadOnDemand="true" MarkFirstMatch="true" HighlightTemplatedItems="true"
                                            OnItemDataBound="cboGuarantor_ItemDataBound" OnItemsRequested="cboAppointmentGuarantorID_ItemsRequested">
                                            <FooterTemplate>
                                                Note : Show max 10 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchRegGuarantor" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchRegistration_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <asp:Panel runat="server" ID="pnlIncludeCheckedOut" Visible="False">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkIncludeCheckedOut" runat="server" Text="Include Checked Out" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20px">
                                            <asp:ImageButton ID="btnSearchInclCheckOut" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                                OnClick="btnSearchRegistration_Click" ToolTip="Search" />
                                        </td>
                                        <td></td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlConfirmedAttendance" Visible="False">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblConfirmedAttendance" runat="server" Text="Confirmed Attendance Status"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboConfirmedAttendanceStatus" runat="server" Width="300px">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20px">
                                            <asp:ImageButton ID="btnSearchConfirmedAttendence" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                                OnClick="btnSearchRegistration_Click" ToolTip="Search" />
                                        </td>
                                        <td></td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="pnlRSUI" runat="server">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="Label13" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <a href='http://192.168.0.36/onemedic_kso_ui/includes/avicennaintergrasibedadmin.php'
                                                target="_blank" border="0">System BED BPJS</a>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                </asp:Panel>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
                                BorderColor="#FFC080" BorderStyle="Solid">
                                <table width="100%">
                                    <tr>
                                        <td width="10px" valign="top">
                                            <asp:Image ID="Image1" ImageUrl="~/Images/boundleft.gif" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </cc:CollapsePanel>
            <telerik:RadGrid ID="grdRegisteredList" runat="server" OnNeedDataSource="grdRegisteredList_NeedDataSource"
                GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
                AllowSorting="true" OnItemCreated="grdRegisteredList_ItemCreated" OnItemCommand="grdRegisteredList_ItemCommand"
                OnPreRender="grdRegisteredList_PreRender">
                <MasterTableView DataKeyNames="PatientID,RegistrationNo,ServiceUnitID" ClientDataKeyNames="PatientID,RegistrationNo,ServiceUnitID"
                    GroupLoadMode="Server" HierarchyLoadMode="ServerOnDemand">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="ParamedicName" HeaderText="Physician "></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="ParamedicName" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>

                    <NestedViewTemplate>
                        <asp:Panel runat="server" ID="pnlPhr" CssClass="viewWrap" Visible="false">
                            <telerik:RadTabStrip runat="server" ID="tabStrip" MultiPageID="mpg" SelectedIndex="0">
                                <Tabs>
                                    <telerik:RadTab runat="server" Text="Health Record" PageViewID="pvPhr" />
                                </Tabs>
                            </telerik:RadTabStrip>
                            <telerik:RadMultiPage runat="server" ID="mpg" SelectedIndex="0" RenderSelectedPageOnly="false"
                                BorderColor="Black" BorderStyle="Solid">
                                <telerik:RadPageView runat="server" ID="pvPhr">
                                    <asp:Literal runat="server" ID="litPhrMessage"></asp:Literal>
                                    <telerik:RadToolBar ID="tbarPhr" runat="server" Width="100%" EnableEmbeddedScripts="true"
                                        OnClientButtonClicking="tbarPhr_OnClientButtonClicking">
                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                        <Items>
                                            <telerik:RadToolBarDropDown ID="tbiAdd" runat="server" Text="Add" ImageUrl="~/Images/Toolbar/new16.png"
                                                HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png" />
                                            <telerik:RadToolBarButton ID="tbiRefresh" runat="server" Text="Refresh" Value="refresh"
                                                ImageUrl="~/Images/Toolbar/refresh16.png" Visible="False" />
                                        </Items>
                                    </telerik:RadToolBar>
                                    <telerik:RadGrid ID="grdPhr" runat="server" AllowSorting="true" OnItemCommand="grdPhr_OnItemCommand"
                                        EnableLinqExpressions="false">
                                        <MasterTableView DataKeyNames="ServiceUnitID,PatientID,RegistrationNo,QuestionFormID" AllowMultiColumnSorting="true" AllowFilteringByColumn="False" AutoGenerateColumns="False">
                                            <Columns>
                                                <telerik:GridTemplateColumn UniqueName="editPhr" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center" AllowFiltering="False">
                                                    <ItemTemplate>
                                                        <%# Eval("UrlEdit")%>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn UniqueName="colMenu" HeaderText="" HeaderStyle-Width="30px">
                                                    <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                                    <ItemTemplate>
                                                        <%#string.Format("<a href=\"#\" onclick=\"printPreviewQuestionForm( '{0}','{1}','{2}'); return false;\"><img src=\"../../../Images/Toolbar/print16.png\" border=\"0\" /></a>", Eval("TransactionNo"), Eval("RegistrationNo"), Eval("QuestionFormID"))%>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn UniqueName="TransactionNo" HeaderText="Document No" HeaderStyle-Width="120px">
                                                    <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                                    <ItemTemplate>
                                                        <%# Eval("UrlView") %>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="RmNO" HeaderText="Form ID" UniqueName="RmNO" SortExpression="RmNO" HeaderStyle-Width="80px">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="QuestionFormName" HeaderText="Form Name" UniqueName="QuestionFormName" SortExpression="QuestionFormName">
                                                    <ItemTemplate>
                                                        <%# Eval("QuestionFormName") %>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="ReferenceNo" HeaderText="Ref No" UniqueName="ReferenceNo"
                                                    SortExpression="ReferenceNo" AllowFiltering="False">
                                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridDateTimeColumn DataField="RecordDateTime" HeaderText="Create Date" UniqueName="RecordDateTime"
                                                    SortExpression="RecordDateTime" AllowFiltering="False">
                                                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridDateTimeColumn>
                                                <telerik:GridBoundColumn DataField="CreatedByUserName" HeaderText="Create By" UniqueName="CreatedByUserName"
                                                    SortExpression="CreatedByUserName">
                                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit"
                                                    UniqueName="ServiceUnitName" SortExpression="ServiceUnitName">
                                                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>

                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="False">
                                            <Selecting AllowRowSelect="False" />
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />

                                        </ClientSettings>
                                    </telerik:RadGrid>
                                </telerik:RadPageView>
                            </telerik:RadMultiPage>
                        </asp:Panel>
                    </NestedViewTemplate>

                    <Columns>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            HeaderText="Registration No" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsClosed").Equals(true) || DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true) || DataBinder.Eval(Container.DataItem, "IsFromDispensary").Equals(true) ? string.Format("{0}{2}<br /><i>{1}</i>", DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "AppoinmentTypeName"))
                                    : string.Format("<a href=\"#\" onclick=\"openWinReg('edit', '{0}', '2'); return false;\"><b>{0}{2}</b><br/><i>{1}</i></a>", 
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "AppoinmentTypeName"),
                                        Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsVipMember")) ? "<img src=\"../../../Images/Animated/vipmember16.gif\" border=\"0\" alt=\"VIP\" title=\"VIP\" />" : string.Empty
                                        ))%>
                            </ItemTemplate>
                            <HeaderStyle Width="160px" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridDateTimeColumn HeaderStyle-Width="50px" DataField="RegistrationDate"
                            HeaderText="Reg. Date" UniqueName="RegistrationDate" SortExpression="RegistrationDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="RegistrationTime" HeaderText="Time"
                            UniqueName="RegistrationTime" SortExpression="RegistrationTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            HeaderText="Que" ItemStyle-HorizontalAlign="Center" SortExpression="RegistrationQue">
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" onclick=\"openWinQue('{1}', '2'); return false;\"><b>{0}<br />{2}</b></a>",
                                    DataBinder.Eval(Container.DataItem, "RegistrationQue"),
                                    DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                    DataBinder.Eval(Container.DataItem, "FormattedNo")))%>
                            </ItemTemplate>
                            <HeaderStyle Width="20px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="MedicalNo" HeaderText="Medical No"
                            UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />

                        <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SalutationName" HeaderText=""
                            UniqueName="SalutationName" SortExpression="SalutationName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridTemplateColumn Groupable="false" DataField="PatientName" HeaderText="Patient Name"
                            UniqueName="PatientName" SortExpression="PatientName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <%# GetUrlForEditPatient(Container)%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="20px" DataField="Sex" HeaderText="Gender"
                            UniqueName="Sex" SortExpression="Sex" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                            SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                            SortExpression="RoomName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ClassName" HeaderText="Class Name" UniqueName="ClassName"
                            SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            Visible="false" />
                        <telerik:GridBoundColumn DataField="BedID" HeaderText="Bed No" UniqueName="BedID"
                            SortExpression="BedID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                            SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="LastCreateUserID" HeaderText="Created By" UniqueName="LastCreateUserID"
                            SortExpression="LastCreateUserID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="20px" DataField="IsConsul" HeaderText="Consul"
                            UniqueName="IsConsul" SortExpression="IsConsul" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="20px" DataField="IsConfirmedAttendance"
                            HeaderText="Conf." UniqueName="IsConfirmedAttendance" SortExpression="IsConfirmedAttendance"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="20px" DataField="IsNewPatient" HeaderText="New"
                            UniqueName="IsNewPatient" SortExpression="IsNewPatient" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20px" HeaderText="Grr">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsDischarged").ToString().Equals("0") ? 
                                    (string.Format("<a href=\"#\" onclick=\"onEditGuarantor('{0}'); return false;\">{1}</a>", 
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                        "<img src=\"../../../Images/Toolbar/edit16.png\" border=\"0\" alt=\"Edit Guarantor\" title=\"Edit Guarantor\" />")) : "")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20px" HeaderText="RF">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsDischarged").ToString().Equals("0") ? 
                                        (string.Format("<a href=\"#\" onclick=\"onEditFromRegistrationNo('{0}','{1}'); return false;\">{2}</a>", 
                                            DataBinder.Eval(Container.DataItem, "PatientID"),DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                            "<img src=\"../../../Images/Toolbar/edit16.png\" border=\"0\" alt=\"Edit Guarantor\" title=\"Edit From Registration No\" />")) : "")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20px">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsDischarged").ToString().Equals("0") ? 
                                    (string.Format("<a href=\"#\" onclick=\"onEditBedNo('{0}'); return false;\">{1}</a>", 
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                        "<img src=\"../../../Images/Toolbar/bed.png\" border=\"0\" alt=\"Edit Bed No\" title=\"Edit Bed No\" />")) : "")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20px">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsDischarged").ToString().Equals("0") ? 
                                    (string.Format("<a href=\"#\" onclick=\"onEditPhysician('{0}', '{1}'); return false;\">{2}</a>",
                                    DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "ServiceUnitID"),
                                    "<img src=\"../../../Images/Toolbar/dokter.png\" border=\"0\" alt=\"Edit Physician\" title=\"Edit Physician\" />")) :"")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20px">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsDischarged").ToString().Equals("0") ? 
                                    (string.Format("<a href=\"#\" onclick=\"openWinPhysicinTeamOp('{0}'); return false;\">{1}</a>",
                                    DataBinder.Eval(Container.DataItem, "RegistrationNo"), "<img src=\"../../../Images/doctors.png\" border=\"0\" alt=\"Substitute Physician\" title=\"Substitute Physician\" />")) :"")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20px">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsClosed").Equals(true) || DataBinder.Eval(Container.DataItem, "IsFromDispensary").Equals(true) ? string.Empty :
                                    string.Format("<a href=\"#\" onclick=\"onVoidRegistration('{0}|{1}', '{2}'); return false;\">{3}</a>", 
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"), 
                                        DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true) ? "0" : "1",
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                                                            DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true) ? string.Empty : "<img src=\"../../../Images/Toolbar/row_delete16.png\" border=\"0\" alt=\"Void\" title=\"Void\" />"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20px">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsDischarged").ToString().Equals("0") ?
                                                                                                            (DataBinder.Eval(Container.DataItem, "IsClosed").Equals(true) || DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true) || DataBinder.Eval(Container.DataItem, "IsFromDispensary").Equals(true) || DataBinder.Eval(Container.DataItem, "IsNewVisible").Equals(false) ? string.Empty
                                                                                                            : string.Format("<a href=\"#\" onclick=\"openWinTransfer('new', '{0}', '{1}', '2', 'IPR'); return false;\"><img src=\"../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"Transfer To Inpatient\" title=\"Transfer To Inpatient\" /></a>",
                                                                                                            DataBinder.Eval(Container.DataItem, "PatientID"), DataBinder.Eval(Container.DataItem, "RegistrationNo"))):"") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20px">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsDischarged").ToString().Equals("0") ?
                                                                        (DataBinder.Eval(Container.DataItem, "IsClosed").Equals(true) || DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true) || DataBinder.Eval(Container.DataItem, "IsFromDispensary").Equals(true) || DataBinder.Eval(Container.DataItem, "IsNewVisible").Equals(false) ? string.Empty
                                                                                                            : string.Format("<a href=\"#\" onclick=\"openWinTransfer('new', '{0}', '{1}', '2', 'EMR'); return false;\"><img src=\"../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"Transfer To Emergency\" title=\"Transfer To Emergency\" /></a>",
                                                                                                            DataBinder.Eval(Container.DataItem, "PatientID"), DataBinder.Eval(Container.DataItem, "RegistrationNo"))):"") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" Visible="False">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsNewVisible").Equals(false) ? string.Empty :
                                                        string.Format("<a href=\"#\" onclick=\"openWinReferToOtherServiceUnit('new', '{0}', '{1}'); return false;\"><img src=\"../../../Images/Toolbar/transactions16.png\" border=\"0\" title=\"Refer To Other Unit\" /></a>",
                                DataBinder.Eval(Container.DataItem, "PatientID"), DataBinder.Eval(Container.DataItem, "RegistrationNo"))) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20px">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "IsFromDispensary").Equals(true) ? string.Empty
                                                                                                            : string.Format("<a href=\"#\" onclick=\"openRegPrintOpt('{0}'); return false;\"><img src=\"../../../Images/Toolbar/print16.png\" border=\"0\" alt=\"Print\" title=\"Print\" /></a>",
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"resendHL7Message('{0}'); return false;\"><img src=\"../../../Images/Toolbar/refresh16.png\" border=\"0\" alt=\"Resend HL7 message\" /></a>", DataBinder.Eval(Container.DataItem, "RegistrationNo"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20px">
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" title=\"Note\" class=\"noti_Container\" onclick=\"openWinRegistrationInfo('{0}'); return false;\"><span id=\"noti_{0}\" class=\"noti_bubble\">{1}</span></a>", 
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                                                                                                DataBinder.Eval(Container.DataItem, "NoteCount")))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" title=\"Document Check List\" class=\"noti2_Container\" onclick=\"openWinQuestionFormCheckList('{0}'); return false;\"><span id=\"noti2_{0}\" class=\"noti_bubble\">{1}</span></a>&nbsp;",
                                                                           DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                                                           DataBinder.Eval(Container.DataItem, "DocumentCheckListCountRemains")))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" HeaderText="Csnt" UniqueName="SatusehatConsent">
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" onclick=\"openWinSatusehatConsent('{0}'); return false;\"><img src=\"../../../Images/doc_upload16.png\" border=\"0\" alt=\"Satusehat Consent\" title=\"Satusehat Consent\" /></a>",
                                                                           DataBinder.Eval(Container.DataItem, "PatientID")))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" HeaderText="">
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" title=\"Patient Document\"  onclick=\"openWinPatientDocument('{0}'); return false;\"><img src=\"../../../Images/doc_upload16.png\" border=\"0\" alt=\"Patient Document\" title=\"Patient Document\" /></a>",
                                                                           DataBinder.Eval(Container.DataItem, "PatientID")))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgAppointmentLokadok" runat="server">
            <cc:CollapsePanel ID="CollapsePanel6" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlInfoApptLoka" runat="server" Visible="false" BackColor="#FFFFC0"
                                Font-Size="Small" BorderColor="#FFC080" BorderStyle="Solid">
                                <table width="100%">
                                    <tr>
                                        <td width="10px" valign="top">
                                            <asp:Image ID="Image2" ImageUrl="~/Images/boundleft.gif" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblInfoApptLoka" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" valign="top">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label11" runat="server" Text="Date Appointment"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadDatePicker ID="txtDateApptLokadok" runat="server" Width="110px">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td width="30px">
                                        <asp:ImageButton ID="btnSearchApptLokadok" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchApptLokadok_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label12" runat="server" Text="Physician"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboPhyApptLokadok" Width="300px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboParamedicID_ItemDataBound"
                                            OnItemsRequested="cboAppointmentParamedicID_ItemsRequested">
                                            <FooterTemplate>
                                                Note : Show max 10 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="30px">
                                        <asp:ImageButton ID="btnSearchApptLokaByPhysician" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchApptLokadok_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnUpdateApptLokadok" runat="server" ImageUrl="~/Images/Toolbar/download16.png"
                                            OnClick="btnUpdateApptLokadok_Click" ToolTip="Download From Lokadok" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblLokaDownloadInfo" runat="server" Text="" Visible="true"></asp:Label>
                                    </td>
                                    <td width="30px"></td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </cc:CollapsePanel>
            <telerik:RadGrid ID="gridApptLokadok" runat="server" OnNeedDataSource="gridApptLokadok_NeedDataSource"
                OnItemDataBound="gridApptLokadok_ItemDataBound" AutoGenerateColumns="False" ShowGroupPanel="false"
                AllowPaging="True" PageSize="15" AllowSorting="True" GridLines="None">
                <MasterTableView DataKeyNames="apptid">
                    <Columns>
                        <telerik:GridCheckBoxColumn DataField="PatientNotFound" HeaderText="PatientNotFound"
                            UniqueName="PatientNotFound" SortExpression="PatientNotFound" Visible="false">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridCheckBoxColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="apptid" HeaderText="Appoint. No"
                            UniqueName="apptid" SortExpression="apptid" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="pid" HeaderText="Medical No"
                            UniqueName="pid" SortExpression="pid" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                            SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="AppointmentDate" DataFormatString="{0:dd/MM/yyyy HH:mm}"
                            DataType="System.DateTime" HeaderText="Date Time" HeaderStyle-Width="110px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="reasonvisit" HeaderText="Visit Reason" UniqueName="reasonvisit"
                            SortExpression="reasonvisit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            Visible="true" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="RegistrationNoRef" HeaderText="Registration No"
                            UniqueName="RegistrationNoRef" SortExpression="RegistrationNoRef" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="RegistrationQue" HeaderText="Que" HeaderStyle-Width="30px"
                            UniqueName="RegistrationQue" SortExpression="RegistrationQue" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="status" HeaderText="Status"
                            UniqueName="status" SortExpression="status" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="bookingcode" HeaderText="Booking Code"
                            UniqueName="bookingcode" SortExpression="bookingcode" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn UniqueName="TemplateColumn" Groupable="false">
                            <ItemTemplate>
                                <%# ((DataBinder.Eval(Container.DataItem, "RegistrationNoRef") ?? string.Empty).Equals(string.Empty) ? GetUrlForNewRegistrationFromLokadok(Container)
                                    : string.Format("<span>{0}</span>", string.Empty))%>
                            </ItemTemplate>
                            <HeaderStyle Width="35px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgGoogleForm" runat="server">
            <cc:CollapsePanel ID="CollapsePanel7" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" valign="top">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label6" runat="server" Text="Date"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadDatePicker ID="txtGFDate" runat="server" Width="100px">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchGF1" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchGoogleForm_Click" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label7" runat="server" Text="Name"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtGFName" runat="server" Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchGF2" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchGoogleForm_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%"></td>
                </table>
            </cc:CollapsePanel>
            <telerik:RadGrid ID="grdGoogleForm" runat="server" OnNeedDataSource="grdGoogleForm_NeedDataSource"
                AutoGenerateColumns="False" ShowGroupPanel="false" AllowPaging="True" PageSize="15"
                AllowSorting="True" GridLines="None">
                <CommandItemStyle />
                <MasterTableView>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="" Groupable="false">
                            <ItemStyle HorizontalAlign="center" />
                            <ItemTemplate>
                                <%# NewRegFromGoogleForm(Container) %>
                            </ItemTemplate>
                            <HeaderStyle Width="40px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Timestamp" UniqueName="Timestamp" HeaderText="Time">
                            <HeaderStyle HorizontalAlign="Left" Width="140px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Name" UniqueName="Name" HeaderText="Name">
                            <HeaderStyle HorizontalAlign="Left" Width="150px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="HpNumber" UniqueName="HpNumber" HeaderText="HP No ">
                            <HeaderStyle HorizontalAlign="Left" Width="100px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SSN" UniqueName="SSN" HeaderText="SSN">
                            <HeaderStyle HorizontalAlign="Left" Width="100px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="DOB" HeaderText="City & Date of Birth">
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <%#string.Format("{0}, {1}", Eval("CityOfBirth"), Convert.ToDateTime( Eval("DateOfBirth")).ToString("dd/mm/yyyy")) %>
                            </ItemTemplate>
                            <HeaderStyle Width="150px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Gender" UniqueName="Gender" HeaderText="Gender">
                            <HeaderStyle HorizontalAlign="Left" Width="100px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Email" UniqueName="Email" HeaderText="Email">
                            <HeaderStyle HorizontalAlign="Left" Width="100px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TestType" UniqueName="TestType" HeaderText="Test Type">
                            <HeaderStyle HorizontalAlign="Left" Width="180px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="Address" HeaderText="Address">
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <%#string.Format("{0} {1}", Eval("AddressStreet"), Eval("AddressCity")) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="false" AllowGroupExpandCollapse="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="false"></Selecting>
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>

    </telerik:RadMultiPage>
</asp:Content>
