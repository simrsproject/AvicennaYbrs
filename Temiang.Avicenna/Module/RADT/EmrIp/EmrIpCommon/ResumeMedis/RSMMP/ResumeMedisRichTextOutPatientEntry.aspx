<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="ResumeMedisRichTextOutPatientEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.EmrIpCommon.ResumeMedis.RSMMP.ResumeMedisRichTextOutPatientEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/ControlPlanCtl.ascx" TagPrefix="uc1" TagName="ControlPlanCtl" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/EpisodeDiagnoseCtl.ascx" TagPrefix="uc1" TagName="EpisodeDiagnoseCtl" %>
<%@ Register Src="~/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/ResumeMedisProcedureCtl.ascx" TagPrefix="uc1" TagName="ResumeMedisProcedureCtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="cb">
        <style>
            td.algn {
                padding-top: 10px;
                vertical-align: top;
            }
        </style>
        <script type="text/javascript" language="javascript">
            function entryLocalistStatus(mod, rimid, bodyId, parid) {
                var url = '<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/Common/LocalistStatus/LocalistStatusEntry.aspx?mod=' + mod + '&parid=' + parid + '&patid=<%= PatientID %>&rimid=' + rimid + '&regno=<%= RegistrationNo %>&bodyId=' + bodyId + '&ccm=rebind&cet=<%=lvLocalistStatus.ClientID %>';
                openWinDialog(url);
            }

            function openWinDialog(url) {
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
        </script>
    </telerik:RadCodeBlock>
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
                    <telerik:AjaxUpdatedControl ControlID="epDiagCtl" />
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
                </table>
            </td>
        </tr>

    </table>

    <table width="100%">
        <tr>
            <td class="label">Vital Sign
            </td>
            <td>
                <telerik:RadGrid ID="grdVitalSign" runat="server"
                    AutoGenerateColumns="False">
                    <MasterTableView DataKeyNames="VitalSignID" ShowHeader="True">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText="Vital Sign">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "VitalSignName")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <%--                            <telerik:GridDateTimeColumn DataField="RecordDate" UniqueName="RecordDate" HeaderText="Date" HeaderStyle-Width="72px"></telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn DataField="RecordTime" UniqueName="RecordTime" HeaderText="Time" HeaderStyle-Width="43px"></telerik:GridBoundColumn>--%>

                            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="Value" HeaderStyle-Width="80px">
                                <ItemTemplate>
                                    <span>
                                        <%#DataBinder.Eval(Container.DataItem, "QuestionAnswerFormatted")%></span>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText="">
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="false">
                        <Selecting AllowRowSelect="false" />
                        <Resizing AllowColumnResize="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
            <td></td>
        </tr>
    </table>

    <table width="100%">
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
                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
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
            <td class="label algn">Physical Examination<br />
                Pemeriksaan Fisik
                <br />
                <asp:LinkButton runat="server" ID="lbtnResetPhysicalExamination" OnClick="lbtnResetPhysicalExamination_OnClick" OnClientClick="if (!confirm('Reset this Physical Examination')) return false;">
                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
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
                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
                </asp:LinkButton>
                <asp:LinkButton runat="server" ID="lbtnLookUpLab" OnClientClick="openLaboratoryHist();return false;">
                    <img src="../../../../../Images/Toolbar/details16.png"/>
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
                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
                </asp:LinkButton>
                <asp:LinkButton runat="server" ID="LinkButton3" OnClientClick="openAncillaryExamOtherHist();return false;" Visible="false">
                    <img src="../../../../../Images/Toolbar/details16.png"/>
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
            <td class="label algn">Final Diagnosis
                <br />
                <asp:LinkButton runat="server" ID="lbtnResetFinalDiag" OnClick="lbtnResetFinalDiag_OnClick" OnClientClick="if (!confirm('Delete Final Diagnose and import from Work Diagnose?')) return false;">
                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
                </asp:LinkButton></td>
            <td>
                <uc1:EpisodeDiagnoseCtl runat="server" ID="finalDiagCtl" IsDischargeSummaryMode="true" />
            </td>
        </tr>
        <tr>
            <td class="label algn">ICD 9 CM
                <br />
                <asp:LinkButton runat="server" ID="lbtnResetResumeMedisProcedure" OnClick="lbtnResetResumeMedisProcedure_OnClick" OnClientClick="if (!confirm('Reset ICD 9 CM?')) return false;">
                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
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
                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
                </asp:LinkButton>

                <asp:LinkButton runat="server" ID="lbtnLookUpPrescription" OnClientClick="openPrescriptionHist();return false;">
                    <img src="../../../../../Images/Toolbar/details16.png"/>
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
    </table>
    <br />
    <telerik:RadListView ID="lvLocalistStatus" runat="server" RenderMode="Lightweight"
        ItemPlaceholderID="BodyImageContainer" OnNeedDataSource="lvLocalistStatus_NeedDataSource">
        <LayoutTemplate>
            <fieldset>
                <legend>LOCALIST STATUS</legend>
                <asp:PlaceHolder ID="BodyImageContainer" runat="server"></asp:PlaceHolder>
            </fieldset>

        </LayoutTemplate>
        <ItemTemplate>
            <fieldset style="height: 100px;">
                <table>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Edit"
                                OnClientClick='<%# string.Format("entryLocalistStatus(\"{0}\", \"{1}\", \"{2}\", \"{3}\");return false;", 
                                                        DataBinder.Eval(Container.DataItem, "EntryMode"),        
                                                        RegistrationNo,DataBinder.Eval(Container.DataItem, "BodyID"),
                                                        ParamedicID)%>'>
                                <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server"
                                    Width="90px" Height="90px" ResizeMode="Fit" DataValue='<%# Eval("BodyImage") == DBNull.Value? new System.Byte[0]: Eval("BodyImage") %>'></telerik:RadBinaryImage>
                            </asp:LinkButton>
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="2"><%#DataBinder.Eval(Container.DataItem, "BodyName")%></td>
                                </tr>
                                <tr>
                                    <td style="width: 50px;">Add:</td>
                                    <td><%#string.Format("{0}",Eval("CreatedDateTime") == DBNull.Value? string.Empty:  Convert.ToDateTime(Eval("CreatedDateTime")).ToString(AppConstant.DisplayFormat.Date))%></td>
                                </tr>
                                <tr>
                                    <td>Upd:</td>
                                    <td><%#string.Format("{0}",Eval("LastUpdateDateTime") == DBNull.Value? string.Empty:  Convert.ToDateTime(Eval("LastUpdateDateTime")).ToString(AppConstant.DisplayFormat.Date))%></td>
                                </tr>
                            </table>
                        </td>

                    </tr>
                </table>

            </fieldset>
            <br />
        </ItemTemplate>
    </telerik:RadListView>

    <asp:Panel runat="server" ID="pnlRefer" Width="100%">
        <uc1:ControlPlanCtl runat="server" ID="controlPlanCtl" />
    </asp:Panel>
</asp:Content>
