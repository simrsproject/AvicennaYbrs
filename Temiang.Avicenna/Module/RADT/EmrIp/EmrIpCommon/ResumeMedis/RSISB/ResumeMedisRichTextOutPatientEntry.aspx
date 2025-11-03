<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="ResumeMedisRichTextOutPatientEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.EmrIpCommon.ResumeMedis.RSISB.ResumeMedisRichTextOutPatientEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/ControlPlanCtl.ascx" TagPrefix="uc1" TagName="ControlPlanCtl" %>
<%@ Register Src="~/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/ResumeMedisProcedureCtl.ascx" TagPrefix="uc1" TagName="ResumeMedisProcedureCtl" %>
<%@ Register Src="~/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/Ctl/MdsDiagnoseOutPatientCtl.ascx" TagPrefix="uc1" TagName="MdsDiagnoseOutPatientCtl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="cb">
        <style>
            td.algn {
                padding-top: 10px;
                vertical-align: top;
            }
            .ColumnSign {
                float: left;
            }
            /* Clear floats after the columns */
            .RowSign:after {
                content: "";
                display: table;
                clear: both;
            }
        </style>
        <script type="text/javascript" language="javascript">

            function openWinDialog(url) {
                PauseAutoSave();
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight) - 40;

                var width =
                    (window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth) - 40;

                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl(url);
                oWnd.setSize(width, height);
                oWnd.show();
            }

            function openPrescriptionHist() {
                var url = 'SelectFromHistory.aspx?regno=<%= RegistrationNo %>&ccm=setvalue&cea=prescription&cet=<%=edtPrescription.ClientID %>';
                openWinDialog(url);
            }
            function openLaboratoryHist() {
                var url = 'SelectLabHistory.aspx?regno=<%= RegistrationNo %>&ccm=setvalue&cea=lab&cet=<%=edtLab.ClientID %>';
                openWinDialog(url);
            }
            function openDietHist() {
                var url = 'SelectDietHistory.aspx?regno=<%= RegistrationNo %>&ccm=setvalue&cea=lab&cet=<%=edtDiet.ClientID %>';
                openWinDialog(url);
            }
            function openAncillaryExamHist() {
                var url = 'SelectAncillaryExamHistory.aspx?regno=<%= RegistrationNo %>&ccm=setvalue&cea=lab&cet=<%=edtDiet.ClientID %>';
                openWinDialog(url);
            }
            function onWinDialogClose(oWnd, args) {
                //get the transferred arguments from MasterDialogEntry
                var arg = args.get_argument();
                if (arg != null) {
                    switch (arg.callbackMethod) {
                        case "submit":
                            {
                                __doPostBack(arg.eventTarget, arg.eventArgument);
                                break;
                            }
                        case "rebind":
                            {
                                var ctl = $find(arg.eventTarget);
                                if (typeof ctl.rebind == 'function') {
                                    ctl.rebind();
                                } else {
                                    var masterTable = $find(arg.eventTarget).get_masterTableView();
                                    masterTable.rebind();
                                }
                                break;
                            }
                        case "setvalue":
                            {
                                var ctl = $find(arg.eventTarget);
                                ctl.set_html(arg.value);
                                ContinueAutoSave();
                                break;
                            }
                    }
                }
            }




            function findDialogWindow() {
                return $find("<%= winAdvEditor.ClientID %>");
            }

            (function (global, undefined) {
                var EditorCommandList;
                var _editor;

                var registerCustomCommands = function () {
                    EditorCommandList["RichEditor"] = function (commandName, editor, args) {
                        _editor = editor;
                        editorContent = _editor.get_html(true); //get RadEditor content
                        var advancedEditorDialog = global.findDialogWindow();
                        advancedEditorDialog.show(); //open RadWindow
                    };
                };

                global.Avicenna = {
                    editor_onClientLoad: function (editor, args) {
                        editor.get_contentArea().setAttribute("spellcheck", "false");
                        EditorCommandList = Telerik.Web.UI.Editor.CommandList;
                        registerCustomCommands();
                    },

                    SetEditorContent: function (content) {
                        //set content to RadEditor on the mane page from RadWindow
                        _editor.set_html(content);
                    },

                    SetDialogContent: function (oWnd) {
                        var contentWindow = oWnd.get_contentFrame().contentWindow;
                        if (contentWindow && contentWindow.setContent) {
                            window.setTimeout(function () {
                                //pass and set the content from the mane page to RadEditor in RadWindow
                                contentWindow.setContent(editorContent);
                            }, 500);
                        }
                    }
                };
            })(window);

            function editSignature() {
                PauseAutoSave();
                var mod = 'edit';
                var imgId = '<%=rbImage.ClientID %>';
                var txtId = '<%=hdnImage.ClientID %>';
                var url = '<%=Helper.UrlRoot()%>/CustomControl/PHR/InputControl/Signature/SignatureEdit.html?mod=' + mod + '&imgId=' + imgId + '&txtId=' + txtId;
                var oWnd = $find("<%= winImage.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
            }

            function editSignature2() {
                PauseAutoSave();
                var mod = 'edit';
                var imgId = '<%=pfsImage.ClientID %>';
                var txtId = '<%=hdnImage2.ClientID %>';
                var url = '<%=Helper.UrlRoot()%>/CustomControl/PHR/InputControl/Signature/SignatureEdit.html?mod=' + mod + '&imgId=' + imgId + '&txtId=' + txtId;
                var oWnd = $find("<%= winImage.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
            }

            function winImage_ClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg != null) {
                    document.getElementById(arg.txtId).value = arg.image;
                    var img = document.getElementById(arg.imgId);

                    img.setAttribute('src', "data:image/Png;base64," + arg.image);
                }
                ContinueAutoSave();
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="620px" Behavior="Move, Close,Maximize,Resize"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="winImage_ClientClose"
        ID="winImage" />

    <telerik:RadWindow ID="winDialog" RenderMode="Lightweight" Width="680px" Height="620px" runat="server" Modal="true"
        Behaviors="Maximize,Close,Move" VisibleStatusbar="False" ShowContentDuringLoad="false"
        OnClientClose="onWinDialogClose">
    </telerik:RadWindow>

    <telerik:RadWindow RenderMode="Lightweight" OnClientShow="Avicenna.SetDialogContent" OnClientPageLoad="Avicenna.SetDialogContent"
        NavigateUrl="EditorInWindow.aspx" runat="server" Behaviors="Maximize,Close,Move" ShowContentDuringLoad="false"
        ID="winAdvEditor" VisibleStatusbar="false" Width="800px" Modal="true" Height="700px" Style="z-index: 50000">
    </telerik:RadWindow>

    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdPrescription">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdDiagnose">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDiagnose" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboProcedureID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtProcedureName" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnResetHistoryOfPresentIllness">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="edtHistoryOfPresentIllness" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnResetFinalDiag">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDiagnose" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="epDiagCtl">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="epDiagCtl" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnResetResumeMedisProcedure">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="resumeMedisProcedureCtl" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="resumeMedisProcedureCtl">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="resumeMedisProcedureCtl" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnResetPastMedicalHistory">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="edtPastMedicalHistory" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="lbtnResetPhysicalExamination">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="edtPhysicalExamination" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnResetLab">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="edtLab" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnResetAncillaryExamOther">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="edtAncillaryExamOther" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnResetDiet">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="edtDiet" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnResetMedicalProcedures">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="edtMedicalProcedures" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnResetPrescription">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="edtPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lvLocalistStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lvLocalistStatus" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>

    <asp:HiddenField ID="hdnIsNewMds" runat="server" Value="0" />
    <table width="100%">
        <tr>
            <td style="width: 50%">
                <table style="width: 100%">
                    <tr>
                        <td class="label">Registration / SEP Date
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtRegistrationDate" runat="server" Width="100px" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Discharge Date Plan
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtDischargeDate" runat="server" Width="100px" />
                                    </td>
                                    <td>
                                        <telerik:RadTimePicker ID="txtDischargeTime" runat="server" Width="93px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px">
                            <asp:RequiredFieldValidator ID="rfvDischargeDate" runat="server" ErrorMessage="Discharge Date required."
                                ValidationGroup="entry" ControlToValidate="txtDischargeDate" SetFocusOnError="false">
                                <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Present Status
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRDischargeCondition" runat="server" Width="304px" />
                        </td>
                        <td style="width: 20px">
                            <asp:RequiredFieldValidator ID="rfvSRDischargeCondition" runat="server" ErrorMessage="Present Status required."
                                ValidationGroup="entry" ControlToValidate="cboSRDischargeCondition" SetFocusOnError="false"
                                Width="100%">
                                <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Discharge Reason
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboSRDischargeMethod" runat="server" Width="304px" />
                        </td>
                        <td style="width: 20px">
                            <asp:RequiredFieldValidator ID="rfvSRDischargeMethod" runat="server" ErrorMessage="Discharge Reason required."
                                ValidationGroup="entry" ControlToValidate="cboSRDischargeMethod" SetFocusOnError="false"
                                Width="100%">
                                <asp:Image ID="Image16" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table style="width: 100%">
                    <tr>
                        <td class="label">Treating Physician
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboTreatingPhysician" runat="server" Width="304px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">(or type here)
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtTreatingPhysicianName" runat="server" Width="300px" MaxLength="250" />
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Unit Intended
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboSRUnitIntended" runat="server" Width="304px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>

    </table>

    <table width="100%">
        <tr>
            <td class="label algn">Indication for Admission<br />
                Indikasi dirawat
            </td>
            <td>
                <telerik:RadEditor RenderMode="Lightweight" ID="edtTreatmentIndications" runat="server" Width="100%"
                    OnClientLoad="Avicenna.editor_onClientLoad" Height="150px" AutoResizeHeight="true" ToolsFile="ToolBarEditorSimple.xml" NewLineMode="Br" EditModes="Design">
                </telerik:RadEditor>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label algn">Chief Complaint<br />
                Keluhan Utama
            </td>
            <td>
                <telerik:RadEditor RenderMode="Lightweight" ID="edtChiefComplaint" runat="server" Width="100%"
                    OnClientLoad="Avicenna.editor_onClientLoad" Height="150px" AutoResizeHeight="true" ToolsFile="ToolBarEditorSimple.xml" NewLineMode="Br">
                </telerik:RadEditor>
            </td>
            <td></td>
        </tr>

        <tr>
            <td class="label algn">History of Present Illness<br />
                Anamnesis Perjalanan Penyakit
                <br />
                <asp:LinkButton runat="server" ID="lbtnResetHistoryOfPresentIllness" OnClick="lbtnResetHistoryOfPresentIllness_OnClick" OnClientClick="if (!confirm('Reset this History of Present Illness')) return false;">
                    <img src="../../../../../../Images/Toolbar/refresh16.png"/>
                </asp:LinkButton>
            </td>
            <td>
                <telerik:RadEditor RenderMode="Lightweight" ID="edtHistoryOfPresentIllness" runat="server" Width="100%"
                    OnClientLoad="Avicenna.editor_onClientLoad" Height="150px" AutoResizeHeight="true" ToolsFile="ToolBarEditorSimple.xml" NewLineMode="Br">
                </telerik:RadEditor>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label algn">Comurbidity<br />
                Riwayat Penyakit Dahulu
                <br />
                <asp:LinkButton runat="server" ID="lbtnResetPastMedicalHistory" OnClick="lbtnResetPastMedicalHistory_OnClick" OnClientClick="if (!confirm('Reset this Comurbidity')) return false;">
                    <img src="../../../../../../Images/Toolbar/refresh16.png"/>
                </asp:LinkButton>
            </td>
            <td>
                <telerik:RadEditor RenderMode="Lightweight" ID="edtPastMedicalHistory" runat="server" Width="100%"
                    OnClientLoad="Avicenna.editor_onClientLoad" Height="150px" AutoResizeHeight="true" ToolsFile="ToolBarEditorSimple.xml" NewLineMode="Br">
                </telerik:RadEditor>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label algn">Prognosis
            </td>
            <td>
                <telerik:RadEditor RenderMode="Lightweight" ID="edtPrognosis" runat="server" Width="100%"
                    OnClientLoad="Avicenna.editor_onClientLoad" Height="150px" AutoResizeHeight="true" ToolsFile="ToolBarEditorSimple.xml" NewLineMode="Br">
                </telerik:RadEditor>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label algn">Physical Examination<br />
                Pemeriksaan Fisik
                <br />
                <asp:LinkButton runat="server" ID="lbtnResetPhysicalExamination" OnClick="lbtnResetPhysicalExamination_OnClick" OnClientClick="if (!confirm('Reset this Physical Examination')) return false;">
                    <img src="../../../../../../Images/Toolbar/refresh16.png"/>
                </asp:LinkButton>
            </td>
            <td>
                <telerik:RadEditor RenderMode="Lightweight" ID="edtPhysicalExamination" runat="server" Width="100%"
                    OnClientLoad="Avicenna.editor_onClientLoad" Height="150px" AutoResizeHeight="true" ToolsFile="ToolBarEditorSimple.xml" NewLineMode="Br">
                </telerik:RadEditor>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label algn">Ancillary Examination (Lab)<br />
                Pemeriksaan Penunjang (Lab)
                <br />
                <asp:LinkButton runat="server" ID="lbtnResetLab" OnClick="lbtnResetLab_OnClick" OnClientClick="if (!confirm('Reset this Ancillary Examination (Lab)')) return false;">
                    <img src="../../../../../../Images/Toolbar/refresh16.png"/>
                </asp:LinkButton>
                <asp:LinkButton runat="server" ID="lbtnLookUpLab" OnClientClick="openLaboratoryHist();return false;">
                    <img src="../../../../../../Images/Toolbar/details16.png"/>
                </asp:LinkButton>
            </td>
            <td>
                <telerik:RadEditor RenderMode="Lightweight" ID="edtLab" runat="server" Width="100%"
                    OnClientLoad="Avicenna.editor_onClientLoad" Height="150px" AutoResizeHeight="true" ToolsFile="ToolBarEditorSimple.xml" NewLineMode="Br">
                </telerik:RadEditor>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label algn">Ancillary Examination(X-rays, etc)<br />
                Pemeriksaan Penunjang(Ro, dll)
                <br />
                <asp:LinkButton runat="server" ID="lbtnResetAncillaryExamOther" OnClick="lbtnResetAncillaryExamOther_OnClick" OnClientClick="if (!confirm('Reset this Ancillary Examination (X-rays, etc)')) return false;">
                    <img src="../../../../../../Images/Toolbar/refresh16.png"/>
                </asp:LinkButton>
                <asp:LinkButton runat="server" ID="LinkButton3" OnClientClick="openAncillaryExamOtherHist();return false;" Visible="false">
                    <img src="../../../../../../Images/Toolbar/details16.png"/>
                </asp:LinkButton>
            </td>
            <td>
                <telerik:RadEditor RenderMode="Lightweight" ID="edtAncillaryExamOther" runat="server" Width="100%"
                    OnClientLoad="Avicenna.editor_onClientLoad" Height="150px" AutoResizeHeight="true" ToolsFile="ToolBarEditorSimple.xml" NewLineMode="Br">
                </telerik:RadEditor>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label algn">Diet
                <br />
                <asp:LinkButton runat="server" ID="lbtnResetDiet" OnClick="lbtnResetDiet_OnClick" OnClientClick="if (!confirm('Reset this Ancillary Examination')) return false;">
                    <img src="../../../../../../Images/Toolbar/refresh16.png"/>
                </asp:LinkButton>
                <asp:LinkButton runat="server" ID="lbtnLookUpDiet" OnClientClick="openDietHist();return false;">
                    <img src="../../../../../../Images/Toolbar/details16.png"/>
                </asp:LinkButton>
            </td>
            <td>
                <telerik:RadEditor RenderMode="Lightweight" ID="edtDiet" runat="server" Width="100%"
                    OnClientLoad="Avicenna.editor_onClientLoad" Height="150px" AutoResizeHeight="true" ToolsFile="ToolBarEditorSimple.xml" NewLineMode="Br">
                </telerik:RadEditor>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label algn">Final Diagnosis
                <br />
                <asp:LinkButton runat="server" ID="lbtnResetFinalDiag" OnClick="lbtnResetFinalDiag_OnClick" OnClientClick="if (!confirm('Delete Final Diagnose and import from Work Diagnose?')) return false;">
                    <img src="../../../../../../Images/Toolbar/refresh16.png"/>
                </asp:LinkButton></td>
            <td>
                <uc1:MdsDiagnoseOutPatientCtl runat="server" id="MdsDiagnoseOutPatientCtl" />
            </td>
        </tr>
        <tr>
            <td class="label algn">ICD 9 CM
                <br />
                <asp:LinkButton runat="server" ID="lbtnResetResumeMedisProcedure" OnClick="lbtnResetResumeMedisProcedure_OnClick" OnClientClick="if (!confirm('Reset ICD 9 CM?')) return false;">
                    <img src="../../../../../../Images/Toolbar/refresh16.png"/>
                </asp:LinkButton>
            </td>
            <td>
                <uc1:ResumeMedisProcedureCtl runat="server" ID="resumeMedisProcedureCtl" />
            </td>
        </tr>
        <tr>
            <td class="label"></td>
            <td>
                <telerik:RadTextBox ID="txtProcedureName" runat="server" Width="100%" />
            </td>
            <td></td>
        </tr>

        <tr>
            <td class="label algn">Medications<br />
                Obat yang diberikan
                
                <br />
                <asp:LinkButton runat="server" ID="lbtnResetPrescription" OnClick="lbtnResetPrescription_OnClick" OnClientClick="if (!confirm('Reset this Medications')) return false;">
                    <img src="../../../../../../Images/Toolbar/refresh16.png"/>
                </asp:LinkButton>

                <asp:LinkButton runat="server" ID="lbtnLookUpPrescription" OnClientClick="openPrescriptionHist();return false;">
                    <img src="../../../../../../Images/Toolbar/details16.png"/>
                </asp:LinkButton>

            </td>
            <td>
                <telerik:RadEditor RenderMode="Lightweight" ID="edtPrescription" runat="server" Width="100%"
                    OnClientLoad="Avicenna.editor_onClientLoad" Height="150px" AutoResizeHeight="true" ToolsFile="ToolBarEditorSimple.xml" NewLineMode="Br">
                </telerik:RadEditor>
            </td>
            <td></td>
        </tr>

        <tr>
            <td class="label algn">Instruction & Follow Up<br />
                Intruksi dan Tindak Lanjut
            </td>
            <td>
                <telerik:RadEditor RenderMode="Lightweight" ID="edtSuggestionFollowUp" runat="server" Width="100%"
                    OnClientLoad="Avicenna.editor_onClientLoad" Height="150px" AutoResizeHeight="true" ToolsFile="ToolBarEditorSimple.xml" NewLineMode="Br">
                </telerik:RadEditor>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label algn">Home Prescription<br />
                Terapi Pulang
            </td>
            <td>
                <telerik:RadGrid ID="grdPrescription" runat="server" OnNeedDataSource="grdPrescription_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None"
                    OnItemCommand="grdPrescription_ItemCommand" OnItemDataBound="grdPrescription_OnItemDataBound" AllowPaging="false">
                    <MasterTableView DataKeyNames="MedicationReceiveNo,IsBroughtHome">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px">
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lblBroughtAll" runat="server" CommandName="BroughtHomeAll" ToolTip="Set All as Home Prescription" OnClientClick="if (!confirm('Set All as Home Prescription')) return false;">
                                        <img style="border: 0px; vertical-align: middle;" src="<%= Helper.UrlRoot() %>/Images/Toolbar/post16.png" />
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lblNotBroughtAll" runat="server" CommandName="NotBroughtHomeAll" ToolTip="Set All as not Home Prescription" OnClientClick="if (!confirm('Set All as not Home Prescription')) return false;">
                                        <img style="border: 0px; vertical-align: middle;" src="<%= Helper.UrlRoot() %>/Images/Toolbar/post16_d.png" />
                                    </asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblBroughtHome" runat="server" CommandName="BroughtHome" ToolTip="Set as Home Prescription"
                                        Visible='<%#  false.Equals(DataBinder.Eval(Container.DataItem, "IsBroughtHome")) || DataBinder.Eval(Container.DataItem, "IsBroughtHome")==DBNull.Value %>'>
                        <img style="border: 0px; vertical-align: middle;" src="<%= Helper.UrlRoot() %>/Images/Toolbar/post16_d.png" />
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lblNotBroughtHome" runat="server" CommandName="NotBroughtHome" ToolTip="Set as not Home Prescription"
                                        Visible='<%#  true.Equals(DataBinder.Eval(Container.DataItem, "IsBroughtHome")) %>'>
                        <img style="border: 0px; vertical-align: middle;" src="<%= Helper.UrlRoot() %>/Images/Toolbar/post16.png" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridCheckBoxColumn DataField="IsDischargeAppropriate" UniqueName="IsDischargeAppropriate" HeaderText="Discharge Appropriate" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="70px">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </telerik:GridCheckBoxColumn>

                            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="350px" HeaderText="Item">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemDescription")%><br />
                                    <%# DataBinder.Eval(Container.DataItem, "SRConsumeMethodName")%>&nbsp;@<%# DataBinder.Eval(Container.DataItem, "ConsumeQty")%>&nbsp;<%# DataBinder.Eval(Container.DataItem, "SRConsumeUnit")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridNumericColumn DataField="BalanceQty" UniqueName="BalanceQty" HeaderText="Qty" HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Right">
                                <ItemStyle Font-Bold="True" HorizontalAlign="Right"></ItemStyle>
                            </telerik:GridNumericColumn>
                            <telerik:GridBoundColumn DataField="SRConsumeUnit" UniqueName="SRConsumeUnit" HeaderText="Unit" HeaderStyle-Width="80px" />
                            <telerik:GridBoundColumn DataField="RefTransactionNo" UniqueName="RefTransactionNo" HeaderText="Prescription No" HeaderStyle-Width="100px" />
                            <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="False">
                        <Selecting AllowRowSelect="False" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
            <td></td>
        </tr>
    </table>
    <br />

        </fieldset>
        <uc1:ControlPlanCtl runat="server" ID="controlPlanCtl" />
    </asp:Panel>--%>

    <br />

    <%--SIGN--%>
    <div class="RowSign">
        <div class="ColumnSign">
            <fieldset style="width: 128px">
                <legend>Physician Signature</legend>
                <telerik:RadBinaryImage ID="rbImage" runat="server"
                    Width="300px" Height="125px" ResizeMode="Fit"></telerik:RadBinaryImage>
                <br />
                <asp:Button runat="server" ID="btnPpaSign" Text="Sign" Width="300px" OnClientClick="javascript:editSignature();return false;" />
                <div>
                    <asp:HiddenField runat="server" ID="hdnImage" />
                </div>
            </fieldset>
        </div>
        <div class="ColumnSign" id="pfsSign" runat="server">
            <fieldset style="width: 128px" >
                <legend>Patient / Family Signature</legend>
                <telerik:RadBinaryImage ID="pfsImage" runat="server"
                    Width="300px" Height="125px" ResizeMode="Fit"></telerik:RadBinaryImage>
                <br />
                <asp:Button runat="server" ID="btnPfsSign" Text="Sign" Width="300px" OnClientClick="javascript:editSignature2();return false;" />
                <div>
                    <asp:HiddenField runat="server" ID="hdnImage2" />
                </div>
            </fieldset>
        </div>
    </div>
    

</asp:Content>
