<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="EpisodeProcedureEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.EpisodeProcedureEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Src="~/Module/RADT/Cpoe/Common/EpisodeProcedure/ServiceUnitBookingBodyImageCtl.ascx" TagPrefix="uc1" TagName="ServiceUnitBookingBodyImageCtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script type="text/javascript" language="javascript">
            var _noteType;
            function onWinLookUpClose(oWnd, args) {
                if (oWnd.argument) {
                    var txt;
                    if (_noteType == "operating")
                        txt = $find("<%= txtOperatingNotes.ClientID %>");
                    else if (_noteType == "anesthetist")
                        txt = $find("<%= txtAnestesyNotes.ClientID %>");
                    else if (_noteType == "operating_post_op")
                        txt = $find("<%= txtPostSurgeryInstructions.ClientID %>");
                    else
                        txt = $find("<%= txtAnestPostSurgeryInstructions.ClientID %>");

                    var textValue = txt.get_textBoxValue();
                    if (oWnd.argument.notes !== "undefined" && oWnd.argument.notes !== "" && textValue !== "undefined" && textValue !== "") {
                        if (!confirm("Existing " + _noteType + " notes will replaced. Continue ?"))
                            return;
                    }

                    if (oWnd.argument.notes !== "") {
                        var notes = decodeURIComponent((oWnd.argument.notes + '').replace(/\+/g, '%20'));
                        txt.set_value(notes);
                    }

                    oWnd.argument.notes = "";
                }
            }

            function openOperatingNotesTemplate() {
                _noteType = "operating";
                var url = '<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/EmrCommon/OperatingNotes/OperatingNotesTemplateCopy.aspx?parid=<%= ParamedicID %>&tp=opr&ccm=submit&cet=<%=txtOperatingNotes.ClientID %>';
                var oWnd = $find("<%= winLookUp.ClientID %>");
                oWnd.setUrl(url);
                oWnd.center();
                oWnd.show();
            }

            function openOperatingNotesTemplateNew() {
                var txt = $find("<%= txtOperatingNotes.ClientID %>");
                var textValue = txt.get_textBoxValue();
                var url =
                    '<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/EmrCommon/OperatingNotes/OperatingNotesTemplateNew.aspx?parid=<%= ParamedicID %>&tp=opr&notes=' +
                    encodeURIComponent(textValue);
                openWindow(url, 900, 600);
            }

            function openAnestesyNotesTemplate() {
                _noteType = "anesthetist";
                var url =
                    '<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/EmrCommon/OperatingNotes/OperatingNotesTemplateCopy.aspx?parid=<%= ParamedicID %>&tp=ans&ccm=submit&cet=<%=txtAnestesyNotes.ClientID %>';
                var oWnd = $find("<%= winLookUp.ClientID %>");
                oWnd.setUrl(url);
                oWnd.center();
                oWnd.show();
            }

            function openAnestesyNotesTemplateNew() {
                var txt = $find("<%= txtAnestesyNotes.ClientID %>");
                var textValue = txt.get_textBoxValue();
                var url =
                    '<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/EmrCommon/OperatingNotes/OperatingNotesTemplateNew.aspx?parid=<%= ParamedicID %>&tp=ans&notes=' +
                    encodeURIComponent(textValue);

                openWindow(url, 900, 600);
            }

            function openPostOpNotesTemplate() {
                _noteType = "operating_post_op";
                var url =
                    '<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/EmrCommon/OperatingNotes/OperatingNotesTemplateCopy.aspx?parid=<%= ParamedicID %>&tp=psi&ccm=submit&cet=<%=txtPostSurgeryInstructions.ClientID %>';
                var oWnd = $find("<%= winLookUp.ClientID %>");
                oWnd.setUrl(url);
                oWnd.center();
                oWnd.show();
            }

            function openPostOpNotesTemplateNew() {
                var txt = $find("<%= txtPostSurgeryInstructions.ClientID %>");
                var textValue = txt.get_textBoxValue();
                var url =
                    '<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/EmrCommon/OperatingNotes/OperatingNotesTemplateNew.aspx?parid=<%= ParamedicID %>&tp=psi&notes=' +
                    encodeURIComponent(textValue);

                openWindow(url, 900, 600);
            }

            function openAnestPostOpNotesTemplate() {
                _noteType = "anesthetist_post_op";
                var url =
                    '<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/EmrCommon/OperatingNotes/OperatingNotesTemplateCopy.aspx?parid=<%= ParamedicID %>&tp=apsi&ccm=submit&cet=<%=txtAnestPostSurgeryInstructions.ClientID %>';
                var oWnd = $find("<%= winLookUp.ClientID %>");
                oWnd.setUrl(url);
                oWnd.center();
                oWnd.show();
            }

            function openAnestPostOpNotesTemplateNew() {
                var txt = $find("<%= txtAnestPostSurgeryInstructions.ClientID %>");
                var textValue = txt.get_textBoxValue();
                var url =
                    '<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/EmrCommon/OperatingNotes/OperatingNotesTemplateNew.aspx?parid=<%= ParamedicID %>&tp=apsi&notes=' +
                    encodeURIComponent(textValue);

                openWindow(url, 900, 600);
            }

            function openWindow(url, width, height) {
                var oWnd = $find("<%= winEntry.ClientID %>");
                oWnd.setUrl(url);
                oWnd.setSize(width, height);
                oWnd.center();
                oWnd.show();
            }

            function winEntry_ClientClose(oWnd, args) {
                //get the transferred arguments from MasterDialogEntry
                var arg = args.get_argument();
                if (arg != null) {
                    if (arg.callbackMethod == 'submit') {
                        __doPostBack(arg.eventTarget, arg.eventArgument);
                    } else {
                        if (arg.callbackMethod == 'rebind') {
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

            function showDropDown(sender, eventArgs) {
                sender.showDropDown();
                sender.requestItems("[showall]", false);
            }

            function editSignature1() {
                var mod = 'edit';
                var imgId = '<%=surgImage.ClientID %>';
                var txtId = '<%=hdnImageSurgSign.ClientID %>';
                var url = '<%=Helper.UrlRoot()%>/CustomControl/PHR/InputControl/Signature/SignatureEdit.html?mod=' + mod + '&imgId=' + imgId + '&txtId=' + txtId;
                var oWnd = $find("<%= winImage.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
            }

            function editSignature2() {
                var mod = 'edit';
                var imgId = '<%=anestImage.ClientID %>';
                var txtId = '<%=hdnImageAnestSign.ClientID %>';
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
            }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdEpisodeProcedure">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEpisodeProcedure" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtProcedureDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtIncisionDate" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtProcedureTime">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtIncisionTime" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="620px" Behavior="Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="winImage_ClientClose"
        ID="winImage" />
    <telerik:RadWindowManager ID="radWindowManager" runat="server" Style="z-index: 7001"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move" Animation="Fade"
        ReloadOnShow="True" ShowContentDuringLoad="false">
        <Windows>
            <telerik:RadWindow ID="winEntry" Width="900px" Height="600px" runat="server"
                ShowContentDuringLoad="false" Behaviors="Maximize,Close,Move" Modal="True"
                OnClientClose="winEntry_ClientClose">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winLookUp" Animation="None" Width="800px" Height="450px"
                runat="server" Behavior="Maximize,Close,Move"
                Modal="true" OnClientClose="onWinLookUpClose">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <asp:HiddenField runat="server" ID="hdnSeqNo" />
    <table width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Booking No"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtBookingNo" runat="server" Width="250px" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsCito" runat="server" Text="Cito" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblProcedureDate" runat="server" Text="Date & Time Started"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="100px">
                                        <telerik:RadDatePicker ID="txtProcedureDate" runat="server" Width="100px" DateInput-DateFormat="dd/MM/yyyy" AutoPostBack="true"
                                            OnSelectedDateChanged="txtProcedureDate_SelectedDateChanged" />
                                    </td>
                                    <td width="50px">
                                        <telerik:RadTimePicker ID="txtProcedureTime" runat="server" TimeView-Interval="00:30"
                                            TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                            TimeView-Columns="4" TimeView-StartTime="00:00" TimeView-EndTime="23:59" AutoPostBack="true"
                                            OnSelectedDateChanged="txtProcedureTime_SelectedDateChanged" Width="90px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvProcedureDate" runat="server" ErrorMessage="Procedure Date Started required."
                                ControlToValidate="txtProcedureDate" SetFocusOnError="True" ValidationGroup="entry"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Date & Time Finished"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="100px">
                                        <telerik:RadDatePicker ID="txtProcedureDate2" runat="server" Width="100px" Enabled="true"
                                            DateInput-DateFormat="dd/MM/yyyy" />
                                    </td>
                                    <td width="50px">
                                        <telerik:RadTimePicker ID="txtProcedureTime2" runat="server" TimeView-Interval="00:30"
                                            TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                            TimeView-Columns="4" TimeView-StartTime="00:00" TimeView-EndTime="23:59" Width="90px" />

                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvProcedureDate2" runat="server" ErrorMessage="Procedure Date Finished required."
                                ControlToValidate="txtProcedureDate2" SetFocusOnError="True" ValidationGroup="entry"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="Date & Time Incision"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="100px">
                                        <telerik:RadDatePicker ID="txtIncisionDate" runat="server" Width="100px" Enabled="true"
                                            DateInput-DateFormat="dd/MM/yyyy" />
                                    </td>
                                    <td width="50px">
                                        <telerik:RadTimePicker ID="txtIncisionTime" runat="server" TimeView-Interval="00:30"
                                            TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                            TimeView-Columns="4" TimeView-StartTime="00:00" TimeView-EndTime="23:59" Width="90px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvIncisionDateTime" runat="server" ErrorMessage="Date Incision required."
                                ControlToValidate="txtIncisionDate" SetFocusOnError="True" ValidationGroup="entry"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRProcedureCategory" runat="server" Text="Procedure Category"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSRProcedureCategoryName" runat="server" Width="300px" />
                            <telerik:RadTextBox ID="txtSRProcedureCategory" runat="server" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRAnestesi" runat="server" Text="Anesthetic Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSRAnestesiName" runat="server" Width="300px" />
                            <telerik:RadTextBox ID="txtSRAnestesi" runat="server" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRoomID" runat="server" Text="Room"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRoomName" runat="server" Width="300px" />
                            <telerik:RadTextBox ID="txtRoomID" runat="server" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPreDiagnosis" runat="server" Text="Pre Diagnosis"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPreDiagnosis" runat="server" Width="300px" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPostDiagnosis" runat="server" Text="Post Diagnosis"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPostDiagnosis" runat="server" Width="300px" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAmountOfBleeding" runat="server" Text="Amount Of Bleeding"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtAmountOfBleeding" runat="server" Width="100px">
                                <NumberFormat DecimalDigits="2" />
                            </telerik:RadNumericTextBox>&nbsp;cc
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAmountOfTransfusions" runat="server" Text="Amount Of Transfusions"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtAmountOfTransfusions" runat="server" Width="100px">
                                <NumberFormat DecimalDigits="2" />
                            </telerik:RadNumericTextBox>&nbsp;cc
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsVoid" runat="server" Text="Void" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td style="width: 100%; vertical-align: top">
                            <fieldset style="width: 95%">
                                <legend>REGIO </legend>
                                <telerik:RadTextBox ID="txtRegio" runat="server" Width="100%" MaxLength="200" TextMode="MultiLine" />
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID" runat="server" Text="Surgeon #1"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPhysicianID" runat="server" Width="300px" EmptyMessage="Select a Item"
                                EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                OnClientFocus="showDropDown">
                                <WebServiceSettings Method="Paramedics" Path="~/WebService/ComboBoxDataService.asmx" />
                            </telerik:RadComboBox>
                            <telerik:RadTextBox ID="txtParamedicName" runat="server" Width="300px" ReadOnly="true" />
                            <telerik:RadTextBox ID="txtParamedicID" runat="server" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <%--                            <asp:RequiredFieldValidator ID="rfvParamedicID" runat="server" ErrorMessage="Physician required."
                                ControlToValidate="cboPhysicianID" SetFocusOnError="True" ValidationGroup="entry"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>--%>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID2a" runat="server" Text="Surgeon #2"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtParamedicName2a" runat="server" Width="300px" ReadOnly="true" />
                            <telerik:RadTextBox ID="txtParamedicID2a" runat="server" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID3a" runat="server" Text="Surgeon #3"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtParamedicName3a" runat="server" Width="300px" ReadOnly="true" />
                            <telerik:RadTextBox ID="txtParamedicID3a" runat="server" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID4a" runat="server" Text="Surgeon #4"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtParamedicName4a" runat="server" Width="300px" ReadOnly="true" />
                            <telerik:RadTextBox ID="txtParamedicID4a" runat="server" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAssistantID1" runat="server" Text="Assistant #1"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAssistantName1" runat="server" Width="300px" ReadOnly="true" />
                            <telerik:RadTextBox ID="txtAssistantID1" runat="server" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAssistantID2" runat="server" Text="Assistant #2"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAssistantName2" runat="server" Width="300px" ReadOnly="true" />
                            <telerik:RadTextBox ID="txtAssistantID2" runat="server" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicIDAnestesi" runat="server" Text="Anesthesiologist"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtParamedicName2" runat="server" Width="300px" ReadOnly="true" />
                            <telerik:RadTextBox ID="txtParamedicID2" runat="server" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAssistantIDAnestesi" runat="server" Text="Assist. Anesthesiologist #1"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAssistantNameAnestesi" runat="server" Width="300px" ReadOnly="true" />
                            <telerik:RadTextBox ID="txtAssistantIDAnestesi" runat="server" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAssistantIDAnestesi2" runat="server" Text="Assist. Anesthesiologist #2"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAssistantNameAnestesi2" runat="server" Width="300px" ReadOnly="true" />
                            <telerik:RadTextBox ID="txtAssistantIDAnestesi2" runat="server" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblInstrumentatorID1" runat="server" Text="Instrumentator #1"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInstrumentatorName1" runat="server" Width="300px" />
                            <telerik:RadTextBox ID="txtInstrumentatorID1" runat="server" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblInstrumentatorID2" runat="server" Text="Instrumentator #2"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInstrumentatorName2" runat="server" Width="300px" />
                            <telerik:RadTextBox ID="txtInstrumentatorID2" runat="server" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblInstrumentatorAssistant" runat="server" Text="Assistant Instru / Circular #1"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAssistantInstrumentatorName" runat="server" Width="300px" />
                            <telerik:RadTextBox ID="txtAssistantInstrumentatorId" runat="server" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <telerik:RadTabStrip ID="tabHeader" runat="server" MultiPageID="mpgHeader" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Procedure" PageViewID="pgvProcedure"
                Selected="true" />
            <telerik:RadTab runat="server" Text="Operating Notes" PageViewID="pgvOperatingNotes" />
            <telerik:RadTab runat="server" Text="Anesthetist Notes" PageViewID="pgvAnesthetistNotes" />
            <telerik:RadTab runat="server" Text="Implant Installation" PageViewID="pgvImplantInstallation" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpgHeader" runat="server" SelectedIndex="0" BorderWidth="1" BorderColor="gray">
        <telerik:RadPageView ID="pgvProcedure" runat="server" SelectedIndex="0">
            <telerik:RadGrid ID="grdEpisodeProcedure" runat="server" OnNeedDataSource="grdEpisodeProcedure_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEpisodeProcedure_UpdateCommand"
                OnDeleteCommand="grdEpisodeProcedure_DeleteCommand" OnInsertCommand="grdEpisodeProcedure_InsertCommand"
                AllowSorting="true" OnItemCreated="grdEpisodeProcedure_ItemCreated">
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="RegistrationNo, SequenceNo, BookingNo, OpNotesSeqNo">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ProcedureID" HeaderText="Code" UniqueName="ProcedureID"
                            SortExpression="ProcedureID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            AllowSorting="false" />
                        <telerik:GridBoundColumn DataField="ProcedureName" HeaderText="Procedure Name" UniqueName="ProcedureName"
                            SortExpression="ProcedureName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            AllowSorting="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ProcedureSynonym" HeaderText="Synonym" UniqueName="ProcedureSynonym"
                            SortExpression="ProcedureSynonym" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            AllowSorting="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ProcedureCategoryName" HeaderText="Procedure Category" UniqueName="ProcedureCategoryName"
                            SortExpression="ProcedureCategoryName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            AllowSorting="false" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="EpisodeProcedureEntryDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EpisodeProcedureEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true" />
            </telerik:RadGrid>

        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvOperatingNotes" runat="server">
            <%--PRE/POST DIAGNOSIS--%>
            <table width="100%">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <fieldset>
                            <legend>PRE DIAGNOSIS</legend>
                            <telerik:RadTextBox ID="txtPreDiagnosisNotes" runat="server" Width="100%" Resize="Vertical" MaxLength="200"
                                TextMode="MultiLine" />
                        </fieldset>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <fieldset>
                            <legend>POST DIAGNOSIS</legend>
                            <telerik:RadTextBox ID="txtPostDiagnosisNotes" runat="server" Width="100%" Resize="Vertical" MaxLength="200"
                                TextMode="MultiLine" />
                        </fieldset>
                    </td>
                </tr>
            </table>

            <%--OPERATIN NOTES--%>
            <table width="100%">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <fieldset>
                            <legend>OPERATING NOTES </legend>
                            <div style="height: 35px; vertical-align: top; padding-top: 4px; padding-left: 4px;">
                                <%= ScriptCopyTemplate("OPR") %>&nbsp;&nbsp;<%= ScriptNewTemplate("OPR") %>
                            </div>
                            <telerik:RadTextBox ID="txtOperatingNotes" runat="server" Width="100%" Height="330px" Resize="Vertical"
                                TextMode="MultiLine" />
                        </fieldset>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <fieldset>
                            <legend>POST SURGERY INSTRUCTIONS</legend>
                            <div style="height: 35px; vertical-align: top; padding-top: 4px; padding-left: 4px;">
                                <%= ScriptCopyTemplate("PSI") %>&nbsp;&nbsp;<%= ScriptNewTemplate("PSI") %>
                            </div>
                            <telerik:RadTextBox ID="txtPostSurgeryInstructions" runat="server" Width="100%" Height="330px" Resize="Vertical"
                                TextMode="MultiLine" />
                        </fieldset>
                    </td>
                </tr>
            </table>

            <%--SIGN--%>
            <table width="100%">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <div class="RowSign">
                            <div class="ColumnSign">
                                <fieldset style="width: 128px">
                                    <legend>SIGNATURE</legend>
                                    <telerik:RadBinaryImage ID="surgImage" runat="server"
                                        Width="300px" Height="125px" ResizeMode="Fit"></telerik:RadBinaryImage>
                                    <br />
                                    <asp:Button runat="server" ID="btnSurgSign" Text="Sign" Width="300px" OnClientClick="javascript:editSignature1();return false;" />
                                    <div>
                                        <asp:HiddenField runat="server" ID="hdnImageSurgSign" />
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <fieldset>
                            <legend>COMPLICATIONS NOTES</legend>
                            <telerik:RadTextBox ID="txtComplicationsNotes" runat="server" Width="100%" Height="140px" Resize="Vertical"
                                TextMode="MultiLine" />
                        </fieldset>
                    </td>
                </tr>
            </table>

            <table width="100%">
                <tr>
                    <td style="width: 100%; vertical-align: top">
                        <uc1:ServiceUnitBookingBodyImageCtl runat="server" ID="imageCtl" />
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvAnesthetistNotes" runat="server">
            <table width="100%">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <fieldset>
                            <legend>ANESTHETIST NOTES </legend>
                            <div style="height: 35px; vertical-align: top; padding-top: 4px; padding-left: 4px;">
                                <%= ScriptCopyTemplate("ANS") %>&nbsp;&nbsp;<%= ScriptNewTemplate("ANS") %>
                            </div>
                            <telerik:RadTextBox ID="txtAnestesyNotes" runat="server" Width="100%" Height="366px" Resize="Vertical"
                                TextMode="MultiLine" />
                        </fieldset>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <fieldset>
                            <legend>POST SURGERY INSTRUCTIONS</legend>
                            <div style="height: 35px; vertical-align: top; padding-top: 4px; padding-left: 4px;">
                                <%= ScriptCopyTemplate("APSI") %>&nbsp;&nbsp;<%= ScriptNewTemplate("APSI") %>
                            </div>
                            <telerik:RadTextBox ID="txtAnestPostSurgeryInstructions" runat="server" Width="100%" Height="366px" Resize="Vertical"
                                TextMode="MultiLine" />
                        </fieldset>
                    </td>
                </tr>
            </table>

            <%--SIGN--%>
            <div class="RowSign">
                <div class="ColumnSign">
                    <fieldset style="width: 128px">
                        <legend>Signature</legend>
                        <telerik:RadBinaryImage ID="anestImage" runat="server"
                            Width="300px" Height="125px" ResizeMode="Fit"></telerik:RadBinaryImage>
                        <br />
                        <asp:Button runat="server" ID="btnAnestSign" Text="Sign" Width="300px" OnClientClick="javascript:editSignature2();return false;" />
                        <div>
                            <asp:HiddenField runat="server" ID="hdnImageAnestSign" />
                        </div>
                    </fieldset>
                </div>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvImplantInstallation" runat="server">
            <telerik:RadGrid ID="grdImplantInstallation" runat="server" OnNeedDataSource="grdImplantInstallation_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdImplantInstallation_UpdateCommand"
                OnDeleteCommand="grdImplantInstallation_DeleteCommand" OnInsertCommand="grdImplantInstallation_InsertCommand"
                AllowPaging="true">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="BookingNo, SeqNo"
                    PageSize="15">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SeqNo" HeaderText="Seq No"
                            UniqueName="SeqNo" SortExpression="SeqNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="False" />
                        <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ImplantType" HeaderText="Implant Type" UniqueName="ImplantType"
                            SortExpression="ImplantType" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="SerialNo" HeaderText="Serial No" UniqueName="SerialNo"
                            SortExpression="SerialNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Qty" HeaderText="Qty"
                            UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn DataField="PlacementSite" HeaderText="Placement Site" UniqueName="PlacementSite"
                            SortExpression="PlacementSite" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ImplantInstallationDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ImplantInstallationEditCommand">
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
