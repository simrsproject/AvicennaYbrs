<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="EmrDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrDetail" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>
<%@ Import Namespace="Temiang.Avicenna.Module.RADT.EmrIp" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/MainContent/MedicalHistCtl.ascx" TagPrefix="cc" TagName="MedicalHistCtl" %>
<%@ Register TagPrefix="cc" TagName="NursingCareCtl" Src="~/Module/RADT/EmrIp/EmrIpCommon/MainContent/NursingCare/NursingCareCtl.ascx" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/MainContent/ExamOrderHistCtl.ascx" TagPrefix="cc" TagName="ExamOrderHistCtl" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/MainContent/SurgicalHistCtl.ascx" TagPrefix="cc" TagName="SurgicalHistCtl" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/MainContent/HealthRecordHistCtl.ascx" TagPrefix="cc" TagName="HealthRecordHistCtl" %>
<%@ Register Src="~/CustomControl/VitalSignInfoCtl.ascx" TagPrefix="cc" TagName="VitalSignInfoCtl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .reToolCell, .reLeftVerticalSide, .reRightVerticalSide {
            display: none !important;   
        }

        .childMenu {
            padding: 5px 5px;
            height: 29px;
            color: yellow;
        }

        .wrapper {
            margin: 0 auto;
            border: 1px solid blue;
            overflow: hidden;
        }

            .wrapper > div {
                display: inline-block;
                vertical-align: middle;
            }

        .l {
            float: left;
        }

        .r {
            float: right;
        }

        .nav {
            position: fixed;
            top: 73px;
            width: 100%;
            z-index: 98;
        }

        .RadToolBar .buttonRightALign {
            position: absolute;
            right: 11px;
            top: 7px;
        }

        .multiPage {
            display: inline-block;
            *display: inline;
            zoom: 1;
        }
    </style>

    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <%=JavascriptOpenPrintPreview()%>
        <script type="text/javascript" language="javascript">
            function applyGridHeightMax() {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                var splitter = $find('<%= splMain.ClientID  %>');
                splitter.set_height(height - 74);

                // set height to the whole RadGrid control
                var grid = $find("<%= grdAssessment.ClientID %>");
                grid.get_element().style.height = height - 186 + "px";
                grid.repaint();

                grid = $find("<%= medicalHistCtl.GridDiagAndPrescriptionClientID %>");
                grid.get_element().style.height = height - 156 + "px";
                grid.repaint();

                grid = $find("<%= grdPastMedicalHist.ClientID %>");
                grid.get_element().style.height = height - 156 + "px";
                grid.repaint();

                grid = $find("<%= surgicalHistCtl.GrdEpisodeProcedureClientID %>");
                grid.get_element().style.height = height - 200 + "px";
                grid.repaint();

                grid = $find("<%= examOrderHistCtl.GridExamOrderOther.ClientID %>");
                grid.get_element().style.height = height - 200 + "px";
                grid.repaint();

                grid = $find("<%= examOrderHistCtl.GridLaboratory.ClientID %>");
                grid.get_element().style.height = height - 200 + "px";
                grid.repaint();

                grid = $find("<%= examOrderHistCtl.GridRadiology.ClientID %>");
                grid.get_element().style.height = height - 186 + "px";
                grid.repaint();

                grid = $find("<%= examOrderHistCtl.GridPathology.ClientID %>");
                grid.get_element().style.height = height - 186 + "px";
                grid.repaint();

                grid = $find("<%= healthRecordHistCtl.GridPhrClientID %>");
                grid.get_element().style.height = height - 186 + "px";
                grid.repaint();

                grid = $find("<%= nursingCareCtl.GridFormPengkajian.ClientID %>");
                grid.get_element().style.height = height - 186 + "px";
                grid.repaint();

                grid = $find("<%= nursingCareCtl.GridNursingDiagnosa.ClientID %>");
                grid.get_element().style.height = height - 186 + "px";
                grid.repaint();
            }
            window.onload = function () {
                applyGridHeightMax();
            }
            window.onresize = function () {
                applyGridHeightMax();
            }

            // After postback
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function (s, e) {
                applyGridHeightMax();
            });

            function OnClientButtonClicked(sender, args) {
                switch (args.get_item().get_value()) {
                    case "save":
                        if (!confirm('Are you sure to save?')) args.set_cancel(true);
                        break;
                    case "cancel":
                        if (!confirm('Are you sure to cancel?')) args.set_cancel(true);
                        break;
                }
            }

            function tbMenu_ClientClicked(sender, args) {
                var value = args.get_item().get_value();
                switch (value) {
                    case "icare":
                        var noka = document.getElementById("ctl00_ContentPlaceHolder1_hdnGuarantorCardNo").value;
                        if (noka == '') {
                            alert('No pesetya JKN tidak ditemukan.')
                            return;
                        }
                        var kodeDokter = document.getElementById("ctl00_ContentPlaceHolder1_hdnPhysicianID").value;
                        if (kodeDokter == '') {
                            alert('Kode dokter HFIS tidak ditemukan.')
                            return;
                        }
                        $.ajax({
                            type: "POST",
                            url: "../../../WebService/Applicares.asmx/GetTokenIcare",
                            data: "{'noka':'" + noka + "','kodeDokter':'" + kodeDokter + "'}", // if ur method take parameters
                            contentType: "application/json; charset=utf-8",
                            success: function (response) {
                                var data = response.d;
                                console.log(data);
                                if (data.MetaData.IsValid) {
                                    window.open(data.Response.Url, '_blank');
                                }
                                else {
                                    alert('Code : ' + data.MetaData.Code + ', Message : ' + data.MetaData.Message);
                                }
                            },
                            dataType: "json",
                            failure: function (response) {
                                console.log(response);
                            }
                        });
                        break;
                    case "list":
                        location.replace('EmrList.aspx?rt=<%= Request.QueryString["rt"] %>');
                        break;
                    case "SwitchRegistration":
                        var url = "<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/EmrCommon/SwitchRegistration.aspx?patid=<%=PatientID%>";
                        //openWindow(url, 1000, 800)
                        openWinEntryMaxHeight(url, 1000);
                        break;
                    case "Letter":
                        //entrySickLetter();
                        entryPatientLetter();
                        break;
                    case "Document":
                        entryPatientDocument();
                        break;
                    case "Discharge":
                        entryDischarge();
                        break;
                    case "drugfrom_patient":
                        var url = "<%=Helper.UrlRoot()%>/Module/RADT/EmrIp/EmrIpCommon/Medication/MedicationReceiveFromPatientEntry.aspx?mod=read&patid=<%=PatientID%>&regno=<%=RegistrationNo%>&fregno=<%=ReferFromRegistrationNo%>&rectype=";
                        openWinEntryMaximize(url);
                        break;
                    case "Billing":
                        showBilling();
                        break;
                    case "FluidBalance":
                        entryFluidBalance();
                        break;
                    case "udd_maintenance":
                    case "drugfrom_prescription":
                    case "drugfrom_patient":
                    case "drugfrom_serviceunit":
                    case "adm_recon":
                    case "trf_recon":
                    case "dcg_recon":
                        var aspxForm = "";
                        var rectype = "";
                        var url = "";
                        switch (value) {
                            case "udd_maintenance":
                                url = "<%=Helper.UrlRoot()%>/Module/RADT/EmrIp/EmrIpCommon/Medication/MedicationReceiveMaintenance.aspx?mod=new&patid=<%=PatientID%>&regno=<%=RegistrationNo%>&fregno=<%=ReferFromRegistrationNo%>&rectype=" +
                                    rectype;
                                break;
                            case "drugfrom_prescription":
                                url = "<%=Helper.UrlRoot()%>/Module/RADT/EmrIp/EmrIpCommon/Medication/MedicationReceiveFromPrescription.aspx?mod=new&patid=<%=PatientID%>&regno=<%=RegistrationNo%>&fregno=<%=ReferFromRegistrationNo%>";
                                break;
                            case "drugfrom_patient":
                                url = "<%=Helper.UrlRoot()%>/Module/RADT/EmrIp/EmrIpCommon/Medication/MedicationReceiveFromPatientEntry.aspx?mod=new&patid=<%=PatientID%>&regno=<%=RegistrationNo%>&fregno=<%=ReferFromRegistrationNo%>";
                                break;
                            case "drugfrom_serviceunit":
                                url = "<%=Helper.UrlRoot()%>/Module/RADT/EmrIp/EmrIpCommon/Medication/MedicationReceiveFromServiceUnit.aspx?mod=new&patid=<%=PatientID%>&regno=<%=RegistrationNo%>&fregno=<%=ReferFromRegistrationNo%>";
                                break;
                            case "adm_recon":
                                //aspxForm = "MedicationReceiveReconciliaton.aspx";
                                //rectype = "adm";
                                url = "<%=Helper.UrlRoot()%>/Module/RADT/PharmaceuticalCare/MedicationRecon/MedicationReconEntry.aspx?mod=view&prgid=<%= ProgramID %>&patid=<%=PatientID%>&regno=<%=RegistrationNo%>&fregno=<%=ReferFromRegistrationNo%>&rectype=ADM";
                                break;
                            case "trf_recon":
                                //aspxForm = "MedicationReceiveReconciliaton.aspx";
                                //rectype = "trf";
                                url = "<%=Helper.UrlRoot()%>/Module/RADT/PharmaceuticalCare/MedicationRecon/MedicationReconEntry.aspx?mod=view&prgid=<%= ProgramID %>&patid=<%=PatientID%>&regno=<%=RegistrationNo%>&fregno=<%=ReferFromRegistrationNo%>&rectype=TRF";

                                break;
                            case "dcg_recon":
                                //aspxForm = "MedicationReceiveReconciliaton.aspx";
                                //rectype = "dcg";
                                url = "<%=Helper.UrlRoot()%>/Module/RADT/PharmaceuticalCare/MedicationRecon/MedicationReconEntry.aspx?mod=view&prgid=<%= ProgramID %>&patid=<%=PatientID%>&regno=<%=RegistrationNo%>&fregno=<%=ReferFromRegistrationNo%>&rectype=DCG";
                                break;
                        }
<%--                        var url = "<%=Helper.UrlRoot()%>/Module/RADT/EmrIp/EmrIpCommon/Medication/" +
                            aspxForm +
                        "?mod=new&patid=<%=PatientID%>&regno=<%=RegistrationNo%>&fregno=<%=ReferFromRegistrationNo%>&rectype=" +
                            rectype;--%>
                        openWinEntryMaxHeight(url, 1400);
                        break;
                    case "udd_setup":
                    case "udd_verification":
                    case "udd_realization":
                        var progId = "";
                        var stat = "";
                        switch (value) {
                            case "udd_setup":
                                stat = "S";
                                break;
                            case "udd_verification":
                                stat = "V";
                                break;
                            case "udd_realization":
                                stat = "R";
                                break;
                        }

                        var url = "<%=Helper.UrlRoot()%>/Module/RADT/MedicationStatus/MedicationStatusPerPatient.aspx?stat=" + stat + "&progid=&wintype=max&patid=<%=PatientID%>&regno=<%=RegistrationNo%>&fregno=<%=ReferFromRegistrationNo%>";
                        openWinEntryMaxWindow(url);
                        break;
                    case "MedicationHist":
                        openMedicationHist();
                        break;
                    case "Nosocomial_Infus":
                        entryNosocomial("infus");
                        break;
                    case "Nosocomial_InfusCentral":
                        entryNosocomial("infuscentral");
                        break;
                    case "Nosocomial_Catheter":
                        entryNosocomial("catheter");
                        break;
                    case "Nosocomial_Ngt":
                        entryNosocomial("ngt");
                        break;
                    case "Nosocomial_Surgery":
                        entryNosocomial("surgery");
                        break;
                    case "Nosocomial_Ett":
                        entryNosocomial("ett");
                        break;
                    case "Nosocomial_BedRest":
                        entryNosocomial("bedrest");
                        break;
                    case "oews":
                        openVitalSignChartEws("OEWS");
                        break;
                    case "ews":
                        openVitalSignChartEws("EWS");
                        break;
                    case "meows":
                        openVitalSignChartEws("MEOWS");
                        break;
                    case "pews":
                        openVitalSignChartEws("PEWS");
                        break;
                    case "ptg":
                        openPartograph();
                        break
                    case "gc":
                        openGrowthChart();
                        break;
                    case "PRMRJ":
                        showPRMRJ("<%=RegistrationNo%>", "<%=ParamedicID%>");
                        break;
                    case "dicom":
                        openDicom("");
                        break;
                    case "deceased":
                        openWindow("<%= Helper.UrlRoot() %>/Module/RADT/EmrIP/EmrIpCommon/Deceased/Deceased.aspx?id=<%= RegistrationNo %>", 600, 400);
                        break;
                    case "ClinicalPathway":
                        openClinicalPathway();
                    default:
                        if (value.includes('rpt_')) {
                            var vals = value.split('_');
                            var obj = {};
                            obj.programID = vals[1];
                            obj.registrationNo = '<%=RegistrationNo%>';
                            $.ajax({
                                url: "EmrWebService.asmx/PopulatePrintParameter",
                                data: JSON.stringify(obj), //ur data to be sent to server
                                contentType: "application/json; charset=utf-8",
                                type: "POST",
                                success: function (data) {
                                    var url = '<%= Helper.UrlRoot() %>/Module/Reports/ReportViewer.aspx';
                                    openWindowMax(url, "");
                                },
                                error: function (x, y, z) {
                                    alert(x.responseText + "  " + x.status);
                                }
                            });
                        }
                        break;
                }
            }

            function openClinicalPathway() {
                var url =
                    '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ClinicalPathway/ClinicalPathwayDetail.aspx?mod=view&regno=<%=RegistrationNo %>&patid=<%= PatientID %>';
                url = AddRegistrationType(url);
                var oWnd = radopen(url, 'winDialog');
                oWnd.maximize();
            }

            function printPreviewIntegratedNotes(parameterValue) {
                var obj = {};
                obj.parameterValue = parameterValue;
                openPrintPreview("PopulatePrintParameterIntegratedNotes", obj);

            }

            //0003994 daniel
            function printPreviewTemplatePpaNotes(TemplateID, RegistrationNo) {
                var obj = {};
                obj.registrationNo = RegistrationNo;
                obj.templateId = TemplateID;
                openPrintPreview("PopulatePrintParameterTemplatePpaNotes", obj);
            }
            //0003994 daniel



            function printPreviewQuestionForm(tno, regNo, formId) {
                var obj = {};
                obj.transactionNo = tno;
                obj.registrationNo = regNo;
                obj.questionFormID = formId;
                openPrintPreview("PopulatePrintQuestionForm", obj);
            }

            function tbarAssessment_OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
                switch (val) {
                    case "refresh":
                        var grd = $find('<%=grdAssessment.ClientID %>').get_masterTableView();
                        grd.rebind();
                        break;
                    case "print":
                        args.set_cancel(true);
                        break;
                    case "ResumeMedis":
                        entryResumeMedis('<%= RegistrationNo %>', '<%= ReferFromRegistrationNo%>', '<%= ParamedicID %>');
                        args.set_cancel(true);
                        break;
                    case "Education":
                        entryEducation();
                        break;
                    case "ReferToSpecialist":
                        entryReferToSpecialist();
                        break;
                    case "add_IntNotes":
                        {
                            var abortAdd = '<%= GetAbortAddMessage() %>'
                            if (abortAdd === '') {
                                entryAssessment('new',
                                    '<%= RegistrationNo %>',
                                    '<%= ServiceUnitID %>',
                                    '',
                                    '',
                                    '<%= IsNewPatient %>', 'SOAP', '', '<%= ReferFromRegistrationNo %>', '<%= ParamedicID %>');
                            }
                            else
                                alert(abortAdd);
                            break;
                        }
                    default:
                        if (val.includes("filter_")) {
                            document.getElementById("hdnFilterIntegratedNotes").value = val.split('_')[1];

                            var grd = $find('<%=grdAssessment.ClientID %>').get_masterTableView();
                            grd.rebind();
                        } else {

                            var abortAdd = '<%= GetAbortAddMessage() %>'
                            if (abortAdd === '') {
                                var assessmentType = val.split('_')[1];
                                entryAssessment('new',
                                    '<%= RegistrationNo %>',
                                    '<%= ServiceUnitID %>',
                                    assessmentType,
                                    '',
                                '<%= IsNewPatient %>', 'SOAP', '', '<%= ReferFromRegistrationNo %>', '<%= ParamedicID %>');
                            }
                            else
                                alert(abortAdd);
                        }
                        break;
                }
            }


            function radWindowManager_ClientClose(oWnd, args) {
                oWnd.setUrl("about:blank"); // Sets url to blank for release variable
                //get the transferred arguments from MasterDialogEntry
                var arg = args.get_argument();
                if (arg != null) {
                    switch (arg.callbackMethod) {
                        case "submit":
                            __doPostBack(arg.eventTarget, arg.eventArgument);
                            break;
                        case "rebind":
                            var ctl = $find(arg.eventTarget);
                            if (typeof ctl.rebind == 'function') {
                                ctl.rebind();
                            } else {
                                var masterTable = $find(arg.eventTarget).get_masterTableView();
                                masterTable.rebind();
                            }

                            // Activate tab
                            var ids = arg.eventTarget.split('_');
                            var tabValue = ids[ids.length - 1]; // Nama tab dibuat sama dg grid yg akan di rebind
                            setActiveTab(tabValue);
                            break;
                        case "redirect":
                            window.location = arg.url;
                            break;
                        default:
                            break;
                    }

                }
            }


            function openWindow(url, width, height) {
                openWindow(url, width, height, "winDialog")
            }
            function openWindow(url, width, height, winName) {
                var oWnd;
                oWnd = radopen(url, winName);
                oWnd.setSize(width, height);
                oWnd.center();

                // Cek position
                var pos = oWnd.getWindowBounds();
                if (pos.y < 0)
                    oWnd.moveTo(pos.x, 0);
            }

            function openWinEntry(url, width, height) {
                url = AddRegistrationType(url);
                openWindow(url, width, height);
            }
            function openWinEntryMaximize(url) {
                var oWnd;
                url = AddRegistrationType(url);
                oWnd = radopen(url, 'winDialog');
                oWnd.maximize();
            }
            function AddRegistrationType(url) {
                if (!(url.includes("&rt=") || url.includes("?rt=")))
                    url = url + '&rt=<%= Request.QueryString["rt"] %>';

                return url;
            }
            function openWinEntryNoTitleBarMaximize(url) {
                var oWnd;
                url = AddRegistrationType(url);
                oWnd = radopen(url, 'winNoTitlebar');
                oWnd.maximize();
            }
            function openWinEntryMaxHeight(url, width) {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                url = AddRegistrationType(url);

                openWindow(url, width, height - 40);
            }
            function openWinEntryMaxWindow(url) {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                var width =
                    (window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth);

                url = AddRegistrationType(url);
                openWindow(url, width - 40, height - 40);
            }
            function entryEducation() {
                ////////var url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/PatientEducation/PatientEducationEntry.aspx?mod=view&regno=<%= RegistrationNo %>&patid=<%= PatientID %>&parid=<%= ParamedicID %>';

                // Ganti tampilan PatientEducation yg lebih user friendy dan karena isian yg banyak sehingga tertutup di program yg sebelumnya(Handono 2303)
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/PatientEducation/PatientEducationHist.aspx?regno=<%= RegistrationNo %>&patid=<%= PatientID %>&parid=<%= ParamedicID %>';
                openWinEntryMaxWindow(url);
            }
            function entryPatientAllergy() {
                var url = 'Common/PatientAllergyEntry.aspx?md=edit&patid=<%= PatientID %>&ccm=submit&cea=allergy&cet=<%=grdAssessment.ClientID %>';
                openWinEntry(url, 1400, 400);
            }
            function entryImunization() {
                var url = 'Common/PatientImunizationHistEntry.aspx?md=edit&patid=<%= PatientID %>&ccm=submit&cea=Imunization&cet=<%=grdAssessment.ClientID %>';
                openWinEntry(url, 1200, 700);
            }
            function entryPatientDialysis() {
                var url = 'Common/PatientDialysisEntry.aspx?md=edit&patid=<%= PatientID %>&ccm=submit&cea=dialysis&cet=<%=grdAssessment.ClientID %>';
                openWinEntry(url, 1300, 700);
            }

            function openExamOrderLabResultChart(id, nm) {
                var url = '<%=Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/ExamOrderResult/ExamOrderLabResultChart.aspx?patid=<%= PatientID %>&regno=<%= RegistrationNo %>&frid=' + id + '&frnm=' + nm;
                openWindow(url, 1000, 600);
            }

            function entryAssessment(mod, regno, unit, astp, rimid, isinitial, inputType, refNo, fregno, parid) {
                if (unit === null || unit === "")
                    unit = '<%= ServiceUnitID %>';

                var qriscontinued = "0";
                if (isinitial === "False") {
                    qriscontinued = "1";
                }

                var url = '';
                if (astp === 'PHR') {
                    mod = "view";
                    url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/Common/Phr/PatientHealthRecordDetail.aspx?md=' + mod + '&id=' + refNo + '&regno=' + regno + '&unit=' + unit + '&fid=&menu=su' +'&bookingno=&ccm=rebind&cet=<%=grdAssessment.ClientID %>';
                    openWinEntryMaxWindow(url);
                }
                else if ((inputType === 'SOAP' || inputType === 'SBAR' || inputType === 'ADIME' || inputType === 'Notes') && astp === '') {

                    if (mod === "new" && <%=(AppSession.Parameter.IsEmrPhysicianAssessmentMandatory && IsUserParamedicDpjp()).ToString().ToLower()%>) {
                        var obj = {};
                        obj.registrationNo = "<%= RegistrationNo %>";
                        $.ajax({
                            url: '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrWebService.asmx/AddProgressNotesAbortStatus',
                            data: JSON.stringify(obj), //ur data to be sent to server
                            contentType: "application/json; charset=utf-8",
                            type: "POST",
                            dataType: "json",
                            success: function (data) {
                                var abortMsg = decodeURI(data.d);
                                if (abortMsg === "")
                                    entryProgressNotes(mod, regno, unit, astp, rimid, isinitial, inputType, refNo, fregno, parid, qriscontinued);
                                else
                                    radalert(abortMsg, 400, 120, "Confirmation");
                            },
                            error: function (x, y, z) {
                                //alert(x.responseText + "  " + x.status);
                            }
                        });
                    } else
                        entryProgressNotes(mod, regno, unit, astp, rimid, isinitial, inputType, refNo, fregno, parid, qriscontinued);


                } else if (astp === 'NurseNotes') {
                    url =
                        '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/IntegratedNote/PpaNoteEntry.aspx?mod=edit&patid=<%= PatientID %>&regno=' +
                        regno +
                        '&fregno=<%= ReferFromRegistrationNo %>&parid=<%= ParamedicID %>&unit=' +
                    unit +
                    '&astp=' +
                    astp +
                    '&rimid=' +
                    rimid +
                        '&ccm=rebind&cet=<%=grdAssessment.ClientID %>&iscontinued=' +
                        qriscontinued;
                    openWinEntryMaxWindow(url);
                } else if (inputType === 'SOAP' && astp !== '') {
                    url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentEntry.aspx?mod=' +
                        mod +
                        '&patid=<%= PatientID %>&regno=' + regno +
                        '&fregno=<%= ReferFromRegistrationNo %>' +
                        '&parid=<%= ParamedicID %>&unit=' + unit +
                        '&astp=' + astp +
                        '&rimid=' + rimid +
                        '&rt=<%=RegistrationType%>' +
                        '&ccm=rebind&cet=<%=grdAssessment.ClientID %>&iscontinued=' +
                        qriscontinued;
                    openWinSoap(url);
                } else if (inputType === 'CON' || inputType === 'REF') {
                    entryParamedicConsultAnswer(refNo, regno);
                } else if (inputType === 'MDS') {
                    entryResumeMedis(regno, fregno, parid);
                }
            }

            function entryProgressNotes(mod, regno, unit, astp, rimid, isinitial, inputType, refNo, fregno, parid, qriscontinued) {
                url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/IntegratedNote/IntegratedNoteEntry.aspx?mod=' +
                    mod +
                    '&patid=<%= PatientID %>' +
                    '&regno=' +
                    regno +
                    '&fregno=<%= ReferFromRegistrationNo %>' +
                    '&parid=<%= ParamedicID %>&unit=' +
                    unit +
                    '&astp=' +
                    astp +
                    '&rimid=' +
                    rimid +
                    '&ccm=rebind&cet=<%=grdAssessment.ClientID %>&iscontinued=' +
                    qriscontinued;
                openWinSoap(url);
            }

            function entryParamedicConsultAnswer(crno, regno) {
                var url =
                    '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ParamedicConsult/ParamedicConsultAnswerEntry.aspx?mod=edit&regno=' + regno +'&patid=<%= PatientID %>&crno=' + crno +'&parid=<%= ParamedicID %>';
                //openWinEntry(url, 1020, 800);
                openWinEntryMaxHeight(url, 1020);
            }
<%--            function entryResumeMedis(regno, fregno, parid) {
                var url =
                    '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/ResumeMedisInPatientEntry.aspx?mod=view&regno=' + regno + '&fregno=' + fregno +'&patid=<%= PatientID %>&parid=' + parid;
                openWinEntryMaxWindow(url);
            }--%>
<%--            function entryResumeMedis() {
                var url =
                    '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/ResumeMedisInPatientEntry.aspx?mod=view&regno=<%= RegistrationNo %>&fregno=&patid=<%= PatientID %>&parid=<%= ParamedicID %>';
                openWinEntryMaxWindow(url);
            }--%>
            function entryResumeMedis(regno, fregno, parid) {
                var url = '';
                if ('<%=AppSession.Parameter.HealthcareInitial%>' === 'RSMMP') {
                    switch ('<%=RegistrationType%>') {

                        case 'EMR':
                            url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/RSMMP/ResumeMedisRichTextEmPatientEntry.aspx?mod=view&regno=' + regno + '&fregno=' + fregno + '&patid=<%= PatientID %>&parid=' + parid;
                            break;
                        case 'OPR':
                            url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/RSMMP/ResumeMedisRichTextOutPatientEntry.aspx?mod=view&regno=' + regno + '&fregno=' + fregno + '&patid=<%= PatientID %>&parid=' + parid;
                            break;
                        default:
                            url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/RSMMP/ResumeMedisRichTextInPatientEntry.aspx?mod=view&regno=' + regno + '&fregno=' + fregno + '&patid=<%= PatientID %>&parid=' + parid;
                            break;
                    }

                }
                else if ('<%=AppSession.Parameter.HealthcareInitial%>' === 'RSISB') {
                    switch ('<%=RegistrationType%>') {

                        case 'EMR':
                            url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/RSISB/ResumeMedisRichTextEmPatientEntry.aspx?mod=view&regno=' + regno + '&fregno=' + fregno + '&patid=<%= PatientID %>&parid=' + parid;
                            break;
                        case 'OPR':
                            url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/RSISB/ResumeMedisRichTextOutPatientEntry.aspx?mod=view&regno=' + regno + '&fregno=' + fregno + '&patid=<%= PatientID %>&parid=' + parid;
                            break;
                        default:
                            url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/RSISB/ResumeMedisRichTextInPatientEntry.aspx?mod=view&regno=' + regno + '&fregno=' + fregno + '&patid=<%= PatientID %>&parid=' + parid;
                            break;
                    }

                }
                else
                    url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/ResumeMedisRichTextOutPatientEntry.aspx?mod=view&regno=' + regno + '&fregno=' + fregno + '&patid=<%= PatientID %>&parid=' + parid;
                openWinEntryMaxHeight(url, 1200);
            }

            function entryNursingResume() {
                var url =
                    '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/NursingResumeInPatientEntry.aspx?mod=view&regno=<%= RegistrationNo %>&patid=<%= PatientID %>&parid=<%= ParamedicID %>';

                openWinEntryMaxWindow(url);
            }

            function openWinSoap(url) {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                var width =
                    (window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth);

                url = AddRegistrationType(url);

                var oWnd = radopen(url, 'winSoap');
                oWnd.setSize(width - 40, height - 40);
                oWnd.center();
            }

            function directPrescription() {
                var url = '<%=Helper.UrlRoot() %>/Module/Charges/Dispensary/PrescriptionSales/PrescriptionSalesDetail.aspx?md=new&fromemr=1&emrregno=<%= RegistrationNo %>&regno=&type=sales&mode=direct&rt=opr&ono=';
                openWinEntryMaxWindow(url);
            }
            function showPrescriptionHist(regno, regtype) {
                alert("Sorry, page under construction")
            }
            function entryPrescription(mod, presno, parid) {
                if (parid === "")
                    parid = '<%= ParamedicID %>';
                var url = 'EmrCommon/Medication/PrescriptionEntry.aspx?mod=' + mod + '&patid=<%= PatientID %>&regno=<%= lblRegistrationNo.Text %>&presno=' + presno + '&parid=' + parid + "&ccm=rebind&cet=<%=medicalHistCtl.GridDiagAndPrescriptionClientID %>";

                // Check status add first
                if (mod == "new") {
                    var obj = {};
                    obj.registrationNo = "<%= RegistrationNo %>";
                    $.ajax({
                        url: '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/Medication/PrescriptionWebService.asmx/AddPrescriptionAbortStatus',
                        data: JSON.stringify(obj), //ur data to be sent to server
                        contentType: "application/json; charset=utf-8",
                        type: "POST",
                        dataType: "json",
                        success: function (data) {
                            var abortMsg = decodeURI(data.d);
                            if (abortMsg == "")
                                openWinEntryMaxWindow(url);

                            else
                                radalert(abortMsg, 400, 120, "Confirmation");
                        },
                        error: function (x, y, z) {
                            //alert(x.responseText + "  " + x.status);
                        }
                    });

                }
                else
                    openWinEntryMaxWindow(url);
            }

            function entryLaboratory(mod, trno) {
                entryJobOrder(mod, trno, 'LAB', '<%=examOrderHistCtl.GridLaboratory.ClientID %>');
            }
            function entryRadiology(mod, trno) {
                entryJobOrder(mod, trno, 'RAD', '<%=examOrderHistCtl.GridRadiology.ClientID %>');
            }
            function entryExamOrderOther(mod, trno) {
                entryJobOrder(mod, trno, 'OTH', '<%=examOrderHistCtl.GridExamOrderOther.ClientID %>');
            }
            function entryPa(mod, trno) {
                entryJobOrder(mod, trno, 'PAT', '<%=examOrderHistCtl.GridPathology.ClientID %>');
            }
            function entryJobOrder(mod, trno, orderType, grdId) {
                // Exam order dg entrian detil
<%--                var url =
                    '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/Common/ServiceUnitTrans/ServiceUnitTransEntry.aspx?mod=' +
                    mod + '&ordertype=' + orderType + '&type=jo&resp=0&disch=0&id=' + trno +
                        '&regno=<%= RegistrationNo %>&parid=<%= ParamedicID %>&cid=<%= ServiceUnitID %>&ccm=rebind&cet=' + grdId;--%>

                var md = mod.split('-');
                if (md.length > 1) mod = md[0];

                var url =
                    '<%= Helper.UrlRoot() %>/Module/Charges/ServiceUnit/ServiceUnitTransaction/ServiceUnitTransactionDetail.aspx?emr=1&md=' +
                    mod + '&ordertype=' + orderType + '&type=jo&resp=0&disch=0&id=' + trno +
                    '&regno=<%= RegistrationNo %>&pid=<%= ParamedicID %>&cid=<%= ServiceUnitID %>&ccm=rebind&cet=' + grdId;

                if (md.length > 1) url = url + "&casemix=1";

                // Check status add first
                if (mod == "new") {
                    var obj = {};
                    obj.registrationNo = "<%= RegistrationNo %>";
                    $.ajax({
                        url: '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrWebService.asmx/AddExamOrderAbortStatus',
                        data: JSON.stringify(obj), //ur data to be sent to server
                        contentType: "application/json; charset=utf-8",
                        type: "POST",
                        dataType: "json",
                        success: function (data) {
                            var abortMsg = decodeURI(data.d);
                            if (abortMsg == "")
                                openWinEntryMaximize(url);

                            else
                                radalert(abortMsg, 400, 120, "Confirmation");
                        },
                        error: function (x, y, z) {
                            //alert(x.responseText + "  " + x.status);
                        }
                    });

                }
                else
                    openWinEntryMaxWindow(url);
            }

            function openWinPatient() {
                var url = "<%=Helper.UrlRoot()+"/Module/RADT/Registration/PatientDetail.aspx?md=view&pid="+PatientID+"&emr=1" %>";
                openWinEntryMaxWindow(url);
            }
            function entrySickLetter() {
                var url = 'EmrCommon/SickLetter/SickLetterHistEnt.aspx?mod=view&patid=<%= PatientID %>&parid=<%= ParamedicID %>&regno=<%= RegistrationNo %>';
                openWinEntryMaximize(url);
            }
            function entryPatientLetter() {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/PatientLetter/PatientLetterHist.aspx?patid=<%= PatientID %>&parid=<%= ParamedicID %>&regno=<%= RegistrationNo %>&fregno=<%=ReferFromRegistrationNo%>';
                openWinEntryMaxWindow(url);
            }
            function entryReferToSpecialist() {
                //var url = 'EmrCommon/ReferToSpecialist/ReferToSpecialistHistEnt.aspx?mod=view&patid=<%= PatientID %>&parid=<%= ParamedicID %>&regno=<%= RegistrationNo %>&pit=<%= IsUserEntryReferConsulToSpecialist()?"1":"0" %>';
                //openWinEntryMaximize(url);
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ParamedicConsult/ParamedicConsultHist.aspx?mod=view&patid=<%= PatientID %>&parid=<%=ParamedicID %>&regno=<%= RegistrationNo %>&fregno=<%= ReferFromRegistrationNo %>&addable=<%= IsUserEntryReferConsulToSpecialist()%>&ccm=rebind&cet=<%=medicalHistCtl.GridDiagAndPrescriptionClientID %>';
                openWinEntryMaxWindow(url);
            }
            function entryPatientDocument() {
                //var url = 'EmrCommon/PatientDocument/PatientDocumentHistEnt.aspx?mod=view&patid=<%= PatientID %>&regno=<%= RegistrationNo %>';
                var url = 'EmrCommon/PatientDocument/PatientDocumentHist.aspx?patid=<%= PatientID %>&regno=<%= RegistrationNo %>';
                openWinEntryMaxWindow(url);
            }
            function openAuditLogView(tbnm, fnm, pkd) {
                var url = "<%=Helper.UrlRoot()%>/Module/ControlPanel/AuditLog/Common/AuditLogViewHist.aspx?tbnm=" + tbnm + "&fnm=" + fnm + "&pkd=" + pkd;
                openWinEntryMaxHeight(url, 800);
            }
            function entryDischarge() {
                var url = 'EmrCommon/Discharge/DischargeHistEnt.aspx?mod=view&patid=<%= PatientID %>&parid=<%= ParamedicID %>&regno=<%= RegistrationNo %>&pit=<%= IsUserEntryDischarge()?"1":"2" %>';
                openWinEntryMaximize(url);
            }
            function entryPrmrjConfirm(rim) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/PRMRJ/PrmrjConfirmEntry.aspx?mod=edit&patid=<%= PatientID %>&regno=<%= lblRegistrationNo.Text %>&rim=' + rim +'&ccm=rebind&cet=<%=grdAssessment.ClientID %>';
                url = AddRegistrationType(url);
                var oWnd = radopen(url, "winPrmrjConfirm");
            }

            function winPrmrjConfirm_ClientClose(oWnd, args) {
                oWnd.setUrl("about:blank"); // Sets url to blank for release variable
                <%=string.IsNullOrWhiteSpace(ParamedicFirstTransChargesItemIds)?"grdAssessmentRebind();":"entryParamedicTransCharges();"%>
            }
            function setActiveTab(tabValue) {
                var tabStrip = $find("<%= mainRadTabStrip.ClientID %>");
                var tab = tabStrip.findTabByValue(tabValue);
                //Get tab object  
                if (tab != null) {
                    tab.select(); //Select tab 
                }
            }

            function winSoap_ClientClose(oWnd, args) {
                oWnd.setUrl("about:blank"); // Sets url to blank for release variable
                //get the transferred arguments from MasterDialogEntry
                var arg = args.get_argument();
                if (arg != null) {
                    if (arg.callbackMethod === 'rebind') {
                        var el = document.getElementById('<%= lblChronicDisease.ClientID %>');
                        var disease = (el.innerText || el.textContent);
                        if (disease !== '') {
                            entryPrmrjConfirm(arg.rimid);
                        }
                        else {
                        <%=string.IsNullOrWhiteSpace(ParamedicFirstTransChargesItemIds)?"grdAssessmentRebind();":"entryParamedicTransCharges();"%>
                        }
                    }
                }
            }
            function grdAssessmentRebind() {
                var masterTable = $find("<%=grdAssessment.ClientID%>").get_masterTableView();
                masterTable.rebind();
            }
            function entryParamedicTransCharges() {
                // Munculkan popup pemilihan biaya tindakan jika belum pernah diisi oleh dokter bersangkutan
                var urlEntry = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/Billing/ParamedicTransChargesEntry.aspx?regno=<%= RegistrationNo %>';
                var obj = {};
                obj.registrationNo = "<%= RegistrationNo %>";
                obj.paramedicID = "<%= AppSession.UserLogin.ParamedicID %>";
                $.ajax({
                    url: '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrWebService.asmx/AddParamedicTransChargesStatus',
                    data: JSON.stringify(obj), //ur data to be sent to server
                    contentType: "application/json; charset=utf-8",
                    type: "POST",
                    dataType: "json",
                    success: function (data) {
                        var stat = decodeURI(data.d);
                        if (stat == "show") {
                            openWindow(urlEntry, 600, 400, "winParTransCharges");
                        }
                        else {
                            grdAssessmentRebind();
                        }
                    },
                    error: function (x, y, z) {
                    }
                });
            }

            function winParTransCharges_ClientClose(oWnd, args) {
                oWnd.setUrl("about:blank"); // Sets url to blank for release variable
                grdAssessmentRebind();
            }


            function entryFluidBalance() {
                var url =
                    '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/FluidBalance/FluidBalanceDesktop.aspx?regno=<%=RegistrationNo %>&patid=<%= PatientID %>';
                openWinEntryMaxWindow(url);
            }
            function openMedicationHist() {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Medication/MedicationHist.aspx?patid=<%= PatientID %>&regno=<%=RegistrationNo %>&fregno=<%=ReferFromRegistrationNo %>';
                openWinEntryMaxWindow(url);
            }
            function openMedicationHist() {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Medication/MedicationHist.aspx?patid=<%= PatientID %>&regno=<%=RegistrationNo %>&fregno=<%=ReferFromRegistrationNo %>';
                openWinEntryMaxWindow(url);
            }
            function entryNosocomial(montype) {
                var url =
                    '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Nosocomial/NosocomialMain.aspx?regno=<%=RegistrationNo %>&patid=<%= PatientID %>&montype=' + montype;
                url = AddRegistrationType(url);
                openWinEntryMaxWindow(url);
            }
            function openVitalSignChartEws(ewsType) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/Common/VitalSignChart/VitalSignChartEws.aspx?patid=<%= PatientID %>&regno=<%= RegistrationNo %>&fregno=<%= ReferFromRegistrationNo %>&ewstype=' + ewsType;
                openWinEntryMaximize(url);
            }
            function openPartograph() {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/Common/VitalSignChart/Partograph.aspx?patid=<%= PatientID %>&regno=<%= RegistrationNo %>&fregno=<%= ReferFromRegistrationNo %>';
                openWinEntryMaximize(url);
            }
            function openGrowthChart() {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/Common/GrowthChart.aspx?patid=<%= PatientID %>';
                openWinEntryMaximize(url);
            }
            function showPRMRJ() {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/PRMRJ/Prmrj.aspx?&patid=<%= PatientID %>';
                openWinEntryMaxWindow(url);
            }
            function showBilling() {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/Billing/Billing.aspx?&patid=<%= PatientID %>&regno=<%=RegistrationNo%>&fregno=<%=ReferFromRegistrationNo%>&parid=<%= ParamedicID %>&unit=<%= ServiceUnitID %>&room=<%= RoomID %>';
                openWinEntryMaxWindow(url);
            }


            $.download = function (url, data, method) {
                //url and data options required
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


            function openVitalSignChart(vitalSignID) {
                var url = 'Common/VitalSignChart.aspx?patid=<%= PatientID %>&regno=<%= RegistrationNo %>&vid=' + vitalSignID;
                openWinEntryMaxWindow(url);
            }

            function examOrderResult(serviceUnitID, trno) {
                if ((serviceUnitID == '<%=Temiang.Avicenna.Common.AppSession.Parameter.ServiceUnitLaboratoryID%>') ||
                    (serviceUnitID == '<%=Temiang.Avicenna.Common.AppSession.Parameter.ServiceUnitRadiologyID%>') ||
                    (serviceUnitID == '<%=Temiang.Avicenna.Common.AppSession.Parameter.ServiceUnitRadiologyID2%>')) {

                    var url = 'EmrCommon/ExamOrderResult/ExamOrderLabResult.aspx?patid=<%= PatientID %>&regno=<%= RegistrationNo %>&trno=' + trno;
                    if (serviceUnitID != '<%=Temiang.Avicenna.Common.AppSession.Parameter.ServiceUnitLaboratoryID%>') {
                        var url = 'EmrCommon/ExamOrderResult/ExamOrderRadiologyResult.aspx?patid=<%= PatientID %>&regno=<%= RegistrationNo %>&trno=' + trno;
                    }
                    openWinEntryMaxWindow(url);
                }
            }

            function entryEpisodeProcedure(mod, regno, seqno, bookingno) {
                var url = 'Common/EpisodeProcedure/EpisodeProcedureEntry.aspx?md=' + mod + '&parid=<%=ParamedicID %>&patid=<%= PatientID %>&regno=' + regno + '&seqno=' + seqno + '&bno=' + bookingno + '&unit=<%=Request.QueryString["unit"]%>&ccm=rebind&cet=<%=surgicalHistCtl.GrdEpisodeProcedureClientID %>';
                openWinEntryMaxWindow(url);
            }
            function entryOperatingNotes(mod, bno) {
                var url = 'EmrCommon/OperatingNotes/ServiceUnitBookingOperationNotesEntry.aspx?mod=' + mod + '&bno=' + bno + '&patid=<%= PatientID %>&parid=<%= ParamedicID %>&regno=<%= RegistrationNo %>&ccm=rebind&cet=<%=surgicalHistCtl.GrdEpisodeProcedureClientID %>';
                openWinEntryMaxWindow(url);
            }
            function ZoomViewImage(trno, seqno, no) {
                var url = "<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/ExamOrderResult/ExamOrderImageZoomView.aspx?trno=" + trno + "&seqno=" + seqno + "&imgno=" + no;
                openWinEntryMaxWindow(url);
            }
            function examOrderRadilogyResultEdit(trno, seqno, pid) {
                var url =
                    '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/ExamOrderResult/ExamOrderRadiologyResultEntry.aspx?md=view&patid=<%= PatientID %>&regno=<%= RegistrationNo %>&trno=' + trno + '&pid=' + pid + '&seqno=' + seqno + '&ccm=rebind&cet=<%=examOrderHistCtl.GrdRadiologyClientID %>';
                openWinEntryMaxWindow(url);
            }
            function openExamOrderlabHist() {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/ExamOrderResult/ExamOrderLabHist.aspx?patid=<%= PatientID %>&regno=<%=RegistrationNo %>&fregno=<%=ReferFromRegistrationNo %>';
                openWinEntryMaxWindow(url);
            }
            function examOrderOtherResultEdit(trno, seqno, pid) {
                var url =
                    '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/ExamOrderResult/ExamOrderRadiologyResultEntry.aspx?md=view&patid=<%= PatientID %>&regno=<%= RegistrationNo %>&trno=' + trno + '&pid=' + pid + '&seqno=' + seqno + '&ccm=rebind&cet=<%=examOrderHistCtl.GrdExamOrderOtherClientID %>' +'&type=oth';
                openWinEntryMaxWindow(url);
            }
            function openDicom(accessionNumber) {
                var url = "";
                if (accessionNumber == "") return;
                else url = '<%= ConfigurationManager.AppSettings.Get("DcomUrlLocation") %>' + accessionNumber;

                window.open(url, '_blank');

                //console.log(url);
                //var oWnd = radopen(url, 'winDialog');
                //oWnd.maximize();
                //setTimeout(function () { oWnd.close(); }, 2000);
            }
            function openCpptVerification(id, parid, isdpjp) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/IntegratedNote/CpptVerification.aspx?patid=<%= PatientID %>&regno=<%= RegistrationNo %>&fregno=<%= ReferFromRegistrationNo %>&cpptid=' + id + '&parid=' + parid + '&isdpjp=' + isdpjp +'&ccm=submit&cea=allergy&cet=<%=grdAssessment.ClientID %>';
                openWindow(url, 600, 290);
            }

            function openPrescriptionHist() {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/PrescriptionHist.aspx?patid=<%= PatientID %>&regno=<%=RegistrationNo %>&fregno=<%=ReferFromRegistrationNo %>';
                openWinEntryMaxHeight(url, 1000);
            }

            function OpenTableRespond(TemplateID) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/Common/TableRespond/TableRespondDialog.aspx?regno=<%= RegistrationNo %>&tid=' + TemplateID;
                openWindow(url, 1000, 600);
            }
            function openLocalist(rimid) {
                var url = "<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/LocalistViewer.aspx?rimid=" + rimid;
                openWindow(url, 620, 560);
            }

            function UpdateStateEmrList(statType, divCtl, regNo, fromRegNo, regType, patID, dob, parID) {
                var obj = {};
                obj.statType = statType;
                obj.regNo = regNo;
                obj.fromRegNo = fromRegNo;
                obj.regType = regType;
                obj.patID = patID;
                obj.dob = dob;
                obj.parID = parID;
                $.ajax({
                    url: '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrWebService.asmx/UpdateStateEmrList',
                    data: JSON.stringify(obj), //ur data to be sent to server
                    contentType: "application/json; charset=utf-8",
                    type: "POST",
                    dataType: "json",
                    success: function (data) {
                        // update div
                        document.getElementById(divCtl).innerHTML = data.d;

                    },
                    error: function (x, y, z) {
                        //alert(x.responseText + "  " + x.status);
                    }
                });
            }

            function UpdateStateAuditLogCppt(spanId, itype, astp, rimid, fnm) {
                var obj = {};
                obj.itype = itype;
                obj.astp = astp;
                obj.rimid = rimid;
                obj.fnm = fnm;

                $.ajax({
                    url: '<%= Helper.UrlRoot() %>/Module/ControlPanel/AuditLog/Common/AuditLogWs.asmx/GetStateCppt',
                    data: JSON.stringify(obj), //ur data to be sent to server
                    contentType: "application/json; charset=utf-8",
                    type: "POST",
                    dataType: "json",
                    success: function (data) {
                        // update span
                        document.getElementById(spanId).innerHTML = data.d;
                    },
                    error: function (x, y, z) {
                        //alert(x.responseText + "  " + x.status);
                    }
                });
            }
        </script>
        <script type="text/javascript" language="javascript">
            function onWinWebCamClose(sender, eventArgs) {
                oWnd.setUrl("about:blank"); // Sets url to blank for release variable
                // Get retval
                var arg = eventArgs.get_argument();
                if (arg) {
                    var img = document.getElementById("<%=imgPatientPhoto.ClientID%>");
                    img.setAttribute('src', arg);
                    var hdnImgData = document.getElementById("<%=hdnImgData.ClientID%>");
                    hdnImgData.value = arg;

                    // Save photo
                    __doPostBack("<%= btnSavePhoto.UniqueID %>", '');
                }
            }


            function openWinWebCam() {
                var oWnd = $find("<%= winWebCam.ClientID %>");
                oWnd.setUrl("<%=Helper.UrlRoot()%>/Module/RADT/Registration/PatientPhoto2/WebCam.aspx");
                oWnd.setSize(240 + 50, 320 + 50);
                oWnd.center();
                oWnd.show();
            }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="ajaxManagerProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdPhr">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAssessment" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdAssessment">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAssessment" />
                    <telerik:AjaxUpdatedControl ControlID="litPatientAllergy" />
                    <telerik:AjaxUpdatedControl ControlID="litDiagnosis" />
                    <telerik:AjaxUpdatedControl ControlID="litPatientImunization" />
                    <telerik:AjaxUpdatedControl ControlID="litPatientDialysis" />
                    <telerik:AjaxUpdatedControl ControlID="tbarAssessment" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdEpisodeProcedure">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEpisodeProcedure" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCaptureImage">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="imgPatientPhoto" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="tbarPhr">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPhr" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPhr">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="tbarPhr" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <%--            <telerik:AjaxSetting AjaxControlID="tbarAssessment">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAssessment" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <telerik:AjaxSetting AjaxControlID="btnSavePhoto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="imgPatientPhoto" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPastMedicalHist">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPastMedicalHist" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindowManager ID="radWindowManager" runat="server" Style="z-index: 7001"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
        ReloadOnShow="True" ShowContentDuringLoad="false" OnClientClose="radWindowManager_ClientClose">
        <Windows>
            <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server"
                ShowContentDuringLoad="false" Behaviors="Close,Move,Maximize" Modal="True">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winPrmrjConfirm" Width="800px" Height="420px" runat="server"
                ShowContentDuringLoad="false" Behaviors="Move" Modal="True" OnClientClose="winPrmrjConfirm_ClientClose">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winNoTitlebar" runat="server" Behaviors="None" VisibleTitlebar="False"
                ShowContentDuringLoad="false" Modal="True">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winPrintPreview" Animation="None" Width="680px" Height="200px" runat="server"
                ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
                Modal="true">
            </telerik:RadWindow>
            <%--            <telerik:RadWindow ID="winCaptureImage" Width="720px" Height="380px" runat="server"
                OnClientClose="onWinCaptureImageClose">
            </telerik:RadWindow>--%>
            <telerik:RadWindow ID="winWebCam" Width="680px" Height="620px" runat="server" Modal="true"
                ShowContentDuringLoad="false" Behaviors="None" VisibleStatusbar="False"
                OnClientClose="onWinWebCamClose" />
            <telerik:RadWindow ID="winSoap" Width="900px" Height="600px" runat="server"
                ShowContentDuringLoad="false" Behaviors="Move" Modal="True" OnClientClose="winSoap_ClientClose">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winParTransCharges" Width="900px" Height="600px" runat="server"
                ShowContentDuringLoad="false" Behaviors="Move" Modal="True" OnClientClose="winParTransCharges_ClientClose">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <asp:HiddenField runat="server" ID="hdnRegistrationInfoMedicID" Value="" />
    <div id="content" style="top: 1px; position: relative; z-index: 97;">
        <telerik:RadSplitter ID="splMain" runat="server" EnableEmbeddedScripts="True" Height="590px" Width="100%" HeightOffset="30">
            <telerik:RadPane ID="registrationInfoPane" runat="server" Width="370px" Scrolling="Y">
                <telerik:RadCodeBlock ID="radCodeBlock1" runat="server">
                    <div style="font-weight: bold; font-size: large; color: yellow; background-color: black; text-align: center;">
                        <%=string.Format("<table width='100%'><tr><td>{0}</td><td valign='middle' style='text-align: left;'>{1}</td><td style='text-align: right;'>{2}</td></tr></table>",IsNewPatient?"<img src='../../../Images/label_new_blue48.png' alt=''/>":"",PatientName, string.Format( "<a href='#' onclick='javascript:openWinPatient(\"{0}\"); event.cancelBubble = true;if(event.stopPropagation) event.stopPropagation();'><img src='../../../Images/Toolbar/{1}16.png' alt='edit' /></a>&nbsp;", IsUserEditAble?"edit":"view", IsUserEditAble?"edit":"views")) %>
                    </div>
                </telerik:RadCodeBlock>
                <table width="100%">
                    <tr>
                        <td style="vertical-align: top;">
                            <fieldset id="FieldSet1" style="min-height: 150px;">
                                <legend>Picture</legend>
                                <div>
                                    <asp:Image runat="server" ID="imgPatientPhoto" Width="120px" Height="120px" />

                                    <div id="divEditPhoto" style='float: right; padding: 4px'>
                                        <a href='#' onclick='javascript:openWinWebCam(); event.cancelBubble = true;if(event.stopPropagation) event.stopPropagation();'>
                                            <img src='../../../Images/Toolbar/edit16.png' alt='edit' /></a>

                                    </div>
                                    <asp:HiddenField runat="server" ID="hdnImgData" />
                                    <div style="display: none;">
                                        <asp:Button runat="server" ID="btnSavePhoto" />
                                    </div>
                                </div>
                            </fieldset>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td width="50px">MRN
                                    </td>
                                    <td width="4px">:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblMedicalNo" Font-Bold="true" />
                                        <asp:Image runat="server" ID="imgRip" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Reg. No
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblRegistrationNo" Font-Bold="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Reg. Date
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblRegistrationDate" Font-Bold="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Gender
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblGender" Font-Bold="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">DOB
                                    </td>
                                    <td style="vertical-align: top;">:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblDateOfBirth" Font-Bold="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Guarantor
                                    </td>
                                    <td style="vertical-align: top;">:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblGuarantor" Font-Bold="true" />
                                    </td>
                                </tr>
                                <tr runat="server" id="trBpjsSepNo">
                                    <td style="vertical-align: top;">BPJS SEP No
                                    </td>
                                    <td style="vertical-align: top;">:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblBpjsSepNo" Font-Bold="true" />
                                        <asp:HiddenField runat="server" ID="hdnGuarantorCardNo" />
                                    </td>
                                </tr>
                                <tr runat="server" id="trTglRujukan">
                                    <td style="vertical-align: top;">Tgl Akhir Rujukan
                                    </td>
                                    <td style="vertical-align: top;">:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblTglRujukan" Font-Bold="true" />
                                    </td>
                                </tr>
                                <tr runat="server" id="trCovClass">
                                    <td style="vertical-align: top;">Cov. Class
                                    </td>
                                    <td style="vertical-align: top;">:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblCovClass" Font-Bold="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Unit
                                    </td>
                                    <td style="vertical-align: top;">:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblServiceUnit" Font-Bold="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Physician
                                    </td>
                                    <td style="vertical-align: top;">:
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblPhysician" Font-Bold="true" />
                                        <asp:HiddenField runat="server" ID="hdnPhysicianID" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                </table>
                <div runat="server" id="divChronicDisease" style="background-color: red; text-align: center; color: white; font-weight: bold;">
                    Chronic Disease :<asp:Label runat="server" ID="lblChronicDisease" Width="100%" Font-Bold="true" />
                </div>
                <div runat="server" id="divPatientRiskStatus" style="background-color: red; text-align: center; color: white; font-weight: bold;">
                    <asp:Label runat="server" ID="lblPatientRiskStatus" Width="100%" Font-Bold="true" />
                </div>
                <div runat="server" id="divPatientRiskColor" style="background-color: gray; text-align: center; color: black; font-weight: bold">
                    <asp:Label runat="server" ID="lblPatientRiskColor" Width="100%" Font-Bold="true" />
                </div>
                <div runat="server" id="divClinicalPathway" style="background-color: green; text-align: center; color: white; font-weight: bold;">
                    Clinical Pathway :<asp:Label runat="server" ID="lblClinicalPathway" Width="100%" Font-Bold="true" />
                </div>
                <div>
                    <cc:CollapsePanel ID="cpnPlafond" runat="server" Width="100%" Title="Patient Billing">
                        <%= string.Format("<div id=\"plafondt{0}\"></div>", RegistrationNo.ToString().Replace("/","_")) %>
                        <script type="text/javascript">
                            <%= AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsEmrListShowPlafondProgress) ? 
                                    string.Format("UpdateStateEmrList(\"{6}\",\"{6}{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{7}\");", RegistrationNo.ToString().Replace("/","_"),
                                        RegistrationNo, "", "", "", "", "plafondt",""):string.Empty%>
                        </script>
                        <div style="height: 10px">
                        </div>
                    </cc:CollapsePanel>
                    <cc:CollapsePanel ID="cpnAllergies" runat="server" Width="100%" Title="Allergies<div id='divEditAlg' style='float: right;padding:4px'><a  href='#' onclick='javascript:entryPatientAllergy(); event.cancelBubble = true;if(event.stopPropagation) event.stopPropagation();'> <img src='../../../Images/Toolbar/edit16.png'  alt='edit' /></a></div>">
                        <asp:Literal runat="server" ID="litPatientAllergy"></asp:Literal>

                        <div style="height: 10px">
                        </div>
                    </cc:CollapsePanel>
                    <cc:CollapsePanel ID="cpnImunization" runat="server" Width="100%" Title="Imunization & Vaccine History<div id='divEditImmu' style='float: right;padding:4px'><a  href='#' onclick='javascript:entryImunization(); event.cancelBubble = true;if(event.stopPropagation) event.stopPropagation();'> <img src='../../../Images/Toolbar/edit16.png'  alt='edit' /></a></div>">
                        <asp:Literal runat="server" ID="litPatientImunization"></asp:Literal>

                        <div style="height: 10px">
                        </div>
                    </cc:CollapsePanel>
                    <cc:CollapsePanel ID="cpnDialysis" runat="server" Width="100%" IsCollapsed="true" Title="Dialysis History<div id='divEditDialy' style='float: right;padding:4px'><a  href='#' onclick='javascript:entryPatientDialysis(); event.cancelBubble = true;if(event.stopPropagation) event.stopPropagation();'> <img src='../../../Images/Toolbar/edit16.png'  alt='edit' /></a></div>">
                        <fieldset>
                            <legend>General Information</legend>
                            <asp:Literal runat="server" ID="litPatientDialysisGeneralInfo"></asp:Literal>
                        </fieldset>
                        <fieldset>
                            <legend>Examination Result</legend>
                            <asp:Literal runat="server" ID="litPatientDialysisExamination"></asp:Literal>
                        </fieldset>
                        <fieldset>
                            <legend>Hemodialisa</legend>
                            <asp:Literal runat="server" ID="litPatientDialysisHemodialisa"></asp:Literal>
                        </fieldset>
                        <fieldset>
                            <legend>Peritoneal</legend>
                            <asp:Literal runat="server" ID="litPatientDialysisPeritoneal"></asp:Literal>
                        </fieldset>
                        <fieldset>
                            <legend>Kidney Transplant</legend>
                            <asp:Literal runat="server" ID="litPatientDialysisKidney"></asp:Literal>
                        </fieldset>
                        <div style="height: 10px">
                        </div>
                    </cc:CollapsePanel>
                    <cc:CollapsePanel ID="cpnDiagnosis" runat="server" Width="100%" Title="">
                        <asp:Literal runat="server" ID="litDiagnosis"></asp:Literal>

                        <div style="height: 10px">
                        </div>
                    </cc:CollapsePanel>

                    <cc:CollapsePanel ID="cpnVitalSign" runat="server" Width="100%" Title="Vital Sign">
                        <%--                        <telerik:RadGrid ID="grdVitalSign" runat="server" OnNeedDataSource="grdVitalSign_NeedDataSource"
                            AutoGenerateColumns="False" GridLines="None">
                            <MasterTableView DataKeyNames="VitalSignID" ShowHeader="False">
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText="Question">
                                        <ItemTemplate>
                                            <a href="javascript:void(0);" onclick='<%# string.Format("javascript:openVitalSignChart(\"{0}\")", DataBinder.Eval(Container.DataItem, "VitalSignID")) %>'>
                                                <%#DataBinder.Eval(Container.DataItem, "VitalSignName")%>
                                            </a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridDateTimeColumn DataField="RecordDate" UniqueName="RecordDate" HeaderText="Date" HeaderStyle-Width="72px"></telerik:GridDateTimeColumn>
                                    <telerik:GridBoundColumn DataField="RecordTime" UniqueName="RecordTime" HeaderText="Time" HeaderStyle-Width="43px"></telerik:GridBoundColumn>

                                    <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="Answer" HeaderStyle-Width="80px">
                                        <ItemTemplate>
                                            <span>
                                                <%#DataBinder.Eval(Container.DataItem, "QuestionAnswerFormatted")%></span>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText="">
                                        <HeaderStyle Width="30px"></HeaderStyle>
                                        <ItemTemplate>
                                            <a href="javascript:void(0);" onclick='<%# string.Format("javascript:openVitalSignChart(\"{0}\")", DataBinder.Eval(Container.DataItem, "VitalSignID")) %>'>
                                                <img src='../../../Images/Toolbar/barchart.bmp' alt='chart' /></a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="false">
                                <Selecting AllowRowSelect="false" />
                                <Resizing AllowColumnResize="True" />
                            </ClientSettings>
                        </telerik:RadGrid>--%>
                        <cc:VitalSignInfoCtl runat="server" ID="vitalSignInfoCtl" />
                    </cc:CollapsePanel>
                </div>
            </telerik:RadPane>
            <telerik:RadSplitBar ID="RadSplitbar1" runat="server" CollapseMode="Forward">
            </telerik:RadSplitBar>
            <telerik:RadPane ID="contentPane" runat="server" Scrolling="None" Height="100%">
                <telerik:RadToolBar runat="server" ID="tbMenu" Width="100%" OnClientButtonClicked="tbMenu_ClientClicked">
                    <Items>
                        <telerik:RadToolBarButton runat="server" Text="Back to List" Value="list" ImageUrl="~/Images/Toolbar/details16.png"
                            HoveredImageUrl="~/Images/Toolbar/details16_h.png" DisabledImageUrl="~/Images/Toolbar/details16_d.png" />
                        <telerik:RadToolBarButton runat="server" Text="JKN i-Care" Value="icare" ImageUrl="~/Images/BPJS.png" Visible="false" />
                        <telerik:RadToolBarButton runat="server" Text="Switch" Value="SwitchRegistration"
                            ImageUrl="~/Images/Toolbar/ordering16.png" />
                        <telerik:RadToolBarButton runat="server" Text="Healthcare Letter" Value="Letter"
                            ImageUrl="~/Images/Toolbar/mail16.png" />
                        <telerik:RadToolBarButton runat="server" Text="Attachment" Value="Document"
                            ImageUrl="~/Images/Toolbar/views16.png" />
                        <telerik:RadToolBarButton runat="server" Text="Discharge" Value="Discharge"
                            ImageUrl="~/Images/Toolbar/post16.png" />
                        <telerik:RadToolBarButton runat="server" Text="PRMRJ" Value="PRMRJ"
                            ImageUrl="~/Images/Toolbar/mail16.png" />
                        <telerik:RadToolBarButton runat="server" Text="Billing" Value="Billing"
                            ImageUrl="~/Images/Toolbar/ordering16.png" />
                        <telerik:RadToolBarButton ID="RadToolBarButton2" runat="server" Text="Fluid Balance" Value="FluidBalance"
                            ImageUrl="~/Images/Toolbar/ordering16.png" />
                        <telerik:RadToolBarDropDown runat="server" Text="UDD" ImageUrl="~/Images/Toolbar/drugs16.png">
                        </telerik:RadToolBarDropDown>
                        <telerik:RadToolBarDropDown runat="server" Text="HAIs Monitoring" ImageUrl="~/Images/Toolbar/ordering16.png">
                        </telerik:RadToolBarDropDown>
                        <telerik:RadToolBarDropDown runat="server" Text="HAIs Monitoring" ImageUrl="~/Images/Toolbar/ordering16.png" Visible="false">
                            <Buttons>
                                <telerik:RadToolBarButton runat="server" Text="Vena Verifier" Value="Nosocomial_Infus" />
                                <telerik:RadToolBarButton runat="server" Text="Central (Infus)" Value="Nosocomial_InfusCentral" />
                                <telerik:RadToolBarButton runat="server" Text="Dower Catheter" Value="Nosocomial_Catheter" />
                                <telerik:RadToolBarButton runat="server" Text="NGT (Naso Gastric Tube)" Value="Nosocomial_Ngt" />
                                <telerik:RadToolBarButton runat="server" Text="Surgery" Value="Nosocomial_Surgery" />
                                <telerik:RadToolBarButton runat="server" Text="Mechanic Ventilation" Value="Nosocomial_Ett" />
                                <telerik:RadToolBarButton runat="server" Text="Bed Rest" Value="Nosocomial_BedRest" />
                            </Buttons>
                        </telerik:RadToolBarDropDown>
                        <telerik:RadToolBarDropDown runat="server" Text="Graph" ImageUrl="~/Images/Toolbar/barchart.bmp">
                            <Buttons>
                                <telerik:RadToolBarButton runat="server" Text="EWS (All TTV)" Value="oews" />
                                <telerik:RadToolBarButton runat="server" Text="EWS" Value="ews" />
                                <telerik:RadToolBarButton runat="server" Text="PEWS" Value="pews" />
                                <telerik:RadToolBarButton runat="server" Text="MEOWS" Value="meows" />
                                <telerik:RadToolBarButton runat="server" Text="Growth Chart" Value="gc" />
                                <telerik:RadToolBarButton runat="server" Text="Partograph" Value="ptg" />
                                <%--                                <telerik:RadToolBarButton runat="server" Text="Cardex Monitoring C3" Value="mc3" />
                                <telerik:RadToolBarButton runat="server" Text="Cardex Monitoring C3 Neonatus" Value="mc3n" />
                                <telerik:RadToolBarButton runat="server" Text="Cardex Monitoring CTCU" Value="mcctcu" />--%>
                            </Buttons>
                        </telerik:RadToolBarDropDown>
                        <telerik:RadToolBarButton Text="Clinical Pathway" Value="ClinicalPathway"
                            ImageUrl="~/Images/Toolbar/ordering16.png" />
                        <telerik:RadToolBarButton runat="server" Text="DICOM" Value="dicom"
                            ImageUrl="~/Images/Toolbar/ordering16.png" />
                        <telerik:RadToolBarButton runat="server" Text="Deceased" Value="deceased"
                            ImageUrl="~/Images/black-ribbon.png" />
                        <telerik:RadToolBarDropDown runat="server" Text="Print" ImageUrl="~/Images/Toolbar/print16.png"
                            HoveredImageUrl="~/Images/Toolbar/print16_h.png" DisabledImageUrl="~/Images/Toolbar/print16_d.png" />
                        <telerik:RadToolBarButton CssClass="buttonRightALign">
                            <ItemTemplate>
                                <div style="text-align: right; font-weight: bold;">
                                </div>
                            </ItemTemplate>
                        </telerik:RadToolBarButton>
                    </Items>
                </telerik:RadToolBar>
                <telerik:RadTabStrip ID="mainRadTabStrip" runat="server" MultiPageID="mainRadMultiPage" ShowBaseLine="true" Skin="BlackMetroTouch"
                    Align="Left" PerTabScrolling="True"
                    SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab runat="server" Value="grdAssessment" Text="Integrated Notes" PageViewID="pgAssessment"
                            Selected="True" />
                        <telerik:RadTab runat="server" Value="MedicalHist" Text="Medication" PageViewID="pgMedicalHist" />
                        <telerik:RadTab runat="server" Value="grdEpisodeProcedure" Text="Surgical History" PageViewID="pgSurgicalHist" />
                        <telerik:RadTab runat="server" Value="grdJobOrder" Text="Exam Order" PageViewID="pgJobOrder" />
                        <telerik:RadTab runat="server" Value="grdPhr" Text="Health Record" PageViewID="pgPhr" />
                        <telerik:RadTab runat="server" Value="grdNursingCare" Text="Care Plan" PageViewID="pgNursingCare" />
                    </Tabs>
                </telerik:RadTabStrip>

                <telerik:RadMultiPage ID="mainRadMultiPage" runat="server" SelectedIndex="0" ScrollBars="Auto"
                    CssClass="multiPage">

                    <telerik:RadPageView ID="pgAssessment" runat="server">
                        <telerik:RadToolBar ID="tbarAssessment" runat="server" Width="100%" EnableEmbeddedScripts="false"
                            OnClientButtonClicking="tbarAssessment_OnClientButtonClicking">
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                            <Items>
                                <telerik:RadToolBarDropDown runat="server" Text="Add" ImageUrl="~/Images/Toolbar/new16.png"
                                    HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png" />
                                <telerik:RadToolBarButton runat="server" Text="Progress Notes" Value="add_IntNotes"
                                    ImageUrl="~/Images/Toolbar/new16.png" />
                                <telerik:RadToolBarButton runat="server" Text="PPA Notes" Value="add_NurseNotes"
                                    ImageUrl="~/Images/Toolbar/refresh16.png" />
                                <telerik:RadToolBarButton runat="server" Text="Medical Discharge Summary" Value="ResumeMedis"
                                    ImageUrl="~/Images/Toolbar/ordering16.png" />
                                <telerik:RadToolBarButton runat="server" Text="Refer / Consult to Specialist" Value="ReferToSpecialist"
                                    ImageUrl="~/Images/Toolbar/ordering16.png" />
                                <telerik:RadToolBarButton ID="RadToolBarButton1" runat="server" Text="Education" Value="Education"
                                    ImageUrl="~/Images/Toolbar/ordering16.png" />
                                <telerik:RadToolBarButton runat="server" Text="Refresh" Value="refresh"
                                    ImageUrl="~/Images/Toolbar/refresh16.png" />
                            </Items>
                        </telerik:RadToolBar>
                        <telerik:RadGrid ID="grdAssessment" runat="server" EnableViewState="False" Height="533px"
                            OnNeedDataSource="grdAssessment_NeedDataSource"
                            OnDeleteCommand="grdAssessment_DeleteCommand"
                            OnItemCommand="grdAssessment_ItemCommand"
                            OnItemCreated="grdAssessment_ItemCreated"
                            AutoGenerateColumns="False" GridLines="None">
                            <MasterTableView DataKeyNames="RegistrationInfoMedicID"
                                ShowHeader="True"
                                ShowGroupFooter="True"
                                HierarchyDefaultExpanded="True" AllowFilteringByColumn="True">
                                <NestedViewTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 15px; vertical-align: top;">
                                                <%# EmrIpDetail.IntegratedNoteVerifPrintEditLink(Container,IsUserParamedicDpjp())%>
                                                <asp:LinkButton ID="lblDelete" runat="server" CommandName="Delete" ToolTip="Void"
                                                    CommandArgument='<%#string.Format("{0}_{1}", DataBinder.Eval(Container.DataItem, "RegistrationInfoMedicID"),DataBinder.Eval(Container.DataItem, "IsFromAskep"))%>'
                                                    Visible='<%#EmrIpDetail.IntegratedNoteDeleteable(Container) %>'
                                                    OnClientClick="javascript: if (!confirm('Are you sure void this ?')) return false;">
                                                            <img style="border: 0px; vertical-align: middle;" src="../../../Images/Toolbar/row_delete16.png" alt=""/>
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="lblUnDelete" runat="server" CommandName="Delete" ToolTip="Unvoid"
                                                    CommandArgument='<%#string.Format("{0}_{1}", DataBinder.Eval(Container.DataItem, "RegistrationInfoMedicID"),DataBinder.Eval(Container.DataItem, "IsFromAskep"))%>'
                                                    Visible='<%#EmrIpDetail.IntegratedNoteUnDeleteable(Container) %>'
                                                    OnClientClick="javascript: if (!confirm('Are you sure unvoid this ?')) return false;">
                                                            <img style="border: 0px; vertical-align: middle;" src="../../../Images/Toolbar/refresh16.png" alt=""/>
                                                </asp:LinkButton>
                                            </td>

                                            <td style="vertical-align: top;">
                                                <%#EmrIpDetail.IntegratedNoteScript(Container)%>
                                                <%#EmrIpDetail.AdditionalNoteScript(Container)%>
                                            </td>
                                        </tr>
                                    </table>
                                </NestedViewTemplate>
                                <CommandItemStyle Height="29px" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="DatetimeInfoStr" UniqueName="DatetimeInfoStr" HeaderText="Assessment Time" HeaderStyle-Width="130px" ItemStyle-Font-Bold="True"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ParamedicName" UniqueName="ParamedicName" HeaderText="Physician" HeaderStyle-Width="170px"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CreatedByUserName" UniqueName="CreatedByUserName" HeaderText="Create By" HeaderStyle-Width="170px"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="SRUserType" UniqueName="SRUserType" HeaderText="PPA" HeaderStyle-Width="120px"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="RegistrationNo" UniqueName="RegistrationNo" HeaderText="Registration No" HeaderStyle-Width="170px"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ServiceUnitName" UniqueName="ServiceUnitName" HeaderText="Service Unit" HeaderStyle-Width="170px"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AssessmentTypeName" UniqueName="AssessmentTypeName" HeaderText="Assessment" HeaderStyle-Width="170px"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="SRMedicalNotesInputType" UniqueName="SRMedicalNotesInputType" HeaderText="Notes Type" HeaderStyle-Width="120px"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IsInitialAssessment" UniqueName="IsInitialAssessment" Display="False"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" />
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="False">
                                <Selecting AllowRowSelect="false" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            </ClientSettings>
                            <GroupingSettings ShowUnGroupButton="False" />
                        </telerik:RadGrid>

                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pgMedicalHist" runat="server">
                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 60%; vert-align: top">
                                    <cc:MedicalHistCtl runat="server" ID="medicalHistCtl" />
                                </td>
                                <td style="width: 4px">&nbsp;</td>
                                <td style="width: 40%;" valign="top">
                                    <telerik:RadGrid ID="grdPastMedicalHist" runat="server" EnableViewState="False" Height="560px"
                                        OnNeedDataSource="grdPastMedicalHist_NeedDataSource" OnItemCommand="grdPastMedicalHist_ItemCommand" OnItemCreated="grdPastMedicalHist_ItemCreated"
                                        AutoGenerateColumns="False" GridLines="None">
                                        <MasterTableView ShowHeader="True" CommandItemDisplay="Top" ShowHeadersWhenNoRecords="True" EnableGroupsExpandAll="True" GroupsDefaultExpanded="True">
                                            <CommandItemTemplate>
                                                <div>
                                                    <div class="l" style="font-weight: bold;">
                                                        &nbsp;Medical History
                                        
                                                    </div>
                                                    <div class="r">
                                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="refresh" ImageUrl="~/Images/Toolbar/refresh16.png">
                                            <img src="../../../Images/Toolbar/refresh16.png" alt=""/>&nbsp;&nbsp;
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </CommandItemTemplate>
                                            <CommandItemStyle Height="29px" />
                                            <GroupHeaderItemStyle Wrap="True" Height="25px" Font-Bold="True" Font-Size="9" />
                                            <GroupByExpressions>
                                                <telerik:GridGroupByExpression>
                                                    <GroupByFields>
                                                        <telerik:GridGroupByField FieldName="GroupName" SortOrder="Ascending" />
                                                    </GroupByFields>
                                                    <SelectFields>
                                                        <telerik:GridGroupByField FieldName="GroupName" HeaderText="." />
                                                    </SelectFields>
                                                </telerik:GridGroupByExpression>
                                            </GroupByExpressions>
                                            <Columns>
                                                <telerik:GridTemplateColumn UniqueName="Notes" HeaderText="">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "PastMedical")%>
                                                    </ItemTemplate>

                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="False">
                                            <Selecting AllowRowSelect="False" />
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                </td>

                            </tr>
                        </table>

                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pgSurgicalHist" runat="server">
                        <cc:SurgicalHistCtl runat="server" ID="surgicalHistCtl" />
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pgJobOrder" runat="server" Width="100%">
                        <cc:ExamOrderHistCtl runat="server" ID="examOrderHistCtl" />
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pgPhr" runat="server">
                        <cc:HealthRecordHistCtl runat="server" ID="healthRecordHistCtl" />
                    </telerik:RadPageView>

                    <telerik:RadPageView ID="pgNursingCare" runat="server">
                        <cc:NursingCareCtl runat="server" ID="nursingCareCtl" />
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <telerik:RadCodeBlock runat="server" ID="cbEnd">
        <script type="text/javascript">
            document.getElementById("divEditAlg").style.display = "<%=IsUserEditAble?"block":"none" %>";
            document.getElementById("divEditPhoto").style.display = "<%=IsUserEditAble?"block":"none" %>";
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
