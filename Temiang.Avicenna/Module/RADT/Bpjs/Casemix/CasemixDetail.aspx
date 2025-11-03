<%@  Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="CasemixDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.CasemixDetail" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">
            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.mode != null) {
                    __doPostBack("<%= grdEpisodeDiagnose.UniqueID %>", "rebindDiagnose");
                }
            }

            function openWinRegistrationInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationInfo.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }

            function openWinGuarantorInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var oguar = $find("<%= txtGuarantorID.ClientID %>");
                var lblToBeUpdate = "<%= lblGuarantorInfo.ClientID %>";
                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/Finance/Master/GuarantorInfo/GuarantorInfoDialog.aspx?id=' + oguar.get_value() + '&lblGuarantorInfo=' + lblToBeUpdate + '")%>');
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();

                switch (val) {
                    case "list":
                        var appst = "<%= Request.QueryString["appst"] ?? "" %>";
                        var tab = (appst == "1") ? "outstanding" : "history";
                        location.replace("CasemixList.aspx?tab=" + tab);
                        break;
                    case "reload":
                        __doPostBack("<%= grdTransChargesItem.UniqueID %>", "rebindAll");
                        break;
                    case "mds":
                        entryResumeMedis('<%= RegistrationNo %>', '', '');
                        break;
                }
            }
            function entryResumeMedis(regno, fregno, parid) {
                var url = "";
                if ("<%=RegistrationType%>" == "IPR") {
                    if ('<%=AppSession.Parameter.HealthcareInitial%>' === 'RSMMP')
                        url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/RSMMP/ResumeMedisRichTextInPatientEntry.aspx?mod=view&regno=' + regno + '&fregno=' + fregno + '&patid=<%= PatientID %>&parid=' + parid;
                    else if ('<%=AppSession.Parameter.HealthcareInitial%>' === 'RSISB')
                        url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/RSISB/ResumeMedisRichTextInPatientEntry.aspx?mod=view&regno=' + regno + '&fregno=' + fregno + '&patid=<%= PatientID %>&parid=' + parid;
                    else
                        url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/ResumeMedisRichTextInPatientEntry.aspx?mod=view&regno=' + regno + '&fregno=' + fregno + '&patid=<%= PatientID %>&parid=' + parid;
                }
                else if ("<%=RegistrationType%>" == "OPR") {
                    if ('<%=AppSession.Parameter.HealthcareInitial%>' === 'RSMMP')
                        url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/RSMMP/ResumeMedisRichTextOutPatientEntry.aspx?mod=view&regno=' + regno + '&fregno=' + fregno + '&patid=<%= PatientID %>&parid=' + parid;
                    else if ('<%=AppSession.Parameter.HealthcareInitial%>' === 'RSISB')
                        url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/RSISB/ResumeMedisRichTextOutPatientEntry.aspx?mod=view&regno=' + regno + '&fregno=' + fregno + '&patid=<%= PatientID %>&parid=' + parid;
                    else
                        url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/ResumeMedisRichTextOutPatientEntry.aspx?mod=view&regno=' + regno + '&fregno=' + fregno + '&patid=<%= PatientID %>&parid=' + parid;
                }
                else if ("<%=RegistrationType%>" == "EMR") {
                    if ('<%=AppSession.Parameter.HealthcareInitial%>' === 'RSMMP')
                        url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/RSMMP/ResumeMedisRichTextEMPatientEntry.aspx?mod=view&regno=' + regno + '&fregno=' + fregno + '&patid=<%= PatientID %>&parid=' + parid;
                    else if ('<%=AppSession.Parameter.HealthcareInitial%>' === 'RSISB')
                        url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/RSISB/ResumeMedisRichTextEMPatientEntry.aspx?mod=view&regno=' + regno + '&fregno=' + fregno + '&patid=<%= PatientID %>&parid=' + parid;
                    else
                        url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/ResumeMedisRichTextOutPatientEntry.aspx?mod=view&regno=' + regno + '&fregno=' + fregno + '&patid=<%= PatientID %>&parid=' + parid;
                }
                if (url != "") {
                    url = url + "&csmix=1";

                    var oWnd = $find("<%= winRegInfo.ClientID %>");
                    oWnd.setUrl(url);
                    oWnd.show();
                    oWnd.maximize();
                }
            }

            function ReloadBilling() {
                __doPostBack("<%= grdTransChargesItem.UniqueID %>", "rebindBilling");
            }

            function ReloadPathway(args) {
                if (args == 0) __doPostBack("<%= grdList.UniqueID %>", "rebindPathway");
                else if (confirm("Are you sure?")) __doPostBack("<%= grdList.UniqueID %>", "clearPathway");
            }

            function ReloadDocument() {
                __doPostBack("<%= grdDocument.UniqueID %>", "rebindDocument");
            }

            function MergeDocument() {
                __doPostBack("<%= grdDocument.UniqueID %>", "mergeDocuments");
            }

            function ReloadEKlaim() {
                __doPostBack("<%= grdDocument.UniqueID %>", "rebindEklaim");
            }

            function ApproveRejectDialog(id, type, transNo, seqNo) {
                switch (id) {
                    case '0':
                        var msg = prompt('Approve selected transaction?\nNotes');
                        if (msg != null) {
                            var param = 'approve|' + type + '|' + transNo + '|' + seqNo + '|' + msg;
                            __doPostBack("<%= grdTransChargesItem.UniqueID %>", param);
                        }
                        break;
                    case '1':
                        var msg = prompt('Reject selected transaction?\nNotes');
                        if (msg != null) {
                            var param = 'reject|' + type + '|' + transNo + '|' + seqNo + '|' + msg;
                            __doPostBack("<%= grdTransChargesItem.UniqueID %>", param);
                        }
                        break;
                }
            }

            function ApproveRejectDialog2(id, transNo) {
                switch (id) {
                    case '0':
                        var msg = prompt('Approve selected transaction?\nNotes');
                        if (msg != null) {
                            var param = 'approve2|' + transNo + '|' + msg;
                            __doPostBack("<%= grdBloodRequest.UniqueID %>", param);
                        }
                        break;
                    case '1':
                        var msg = prompt('Reject selected transaction?\nNotes');
                        if (msg != null) {
                            var param = 'reject2|' + transNo + '|' + msg;
                            __doPostBack("<%= grdBloodRequest.UniqueID %>", param);
                        }
                        break;
                }
            }

            function OpenEklaimPage() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>").get_value(); // ambil value

                <% if (AppSession.Parameter.IsiDRGIntegration) { %>
                var baseUrl = '<%= Page.ResolveUrl("~/Module/Finance/Integration/Eklaim/iDRGDetail.aspx") %>';
                <% } else { %>
                    var baseUrl = '<%= Page.ResolveUrl("~/Module/Finance/Integration/Eklaim/eKlaimDetail.aspx") %>';
                <% } %>

                var md = '<%= IsEklaimProcessed ? "edit" : "new" %>';
                oWnd.setUrl(baseUrl + '?md=' + md + '&regNo=' + encodeURIComponent(regNo));

                oWnd.show();
                oWnd.maximize();
            }

            function DocumentFileAction(type, url) {
                window.open(url, '_blank');
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboClassID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCoverageAmount" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtProsedurNonBedah" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtTenagaAhli" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtRadiologi" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtRehabilitasi" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtObat" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtSewaAlat" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtProsedurBedah" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtKeperawatan" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtLaboratorium" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtKamarAkomodasi" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtAlkes" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtKonsultasi" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtPenunjang" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtPelayananDarah" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtRawatIntensifTarif" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtBMHP" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="cboFilterByServiceUnitID" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdBloodRequest" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdEpisodeDiagnose" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdEpisodeProcedure" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdDocument" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdRegistrationGuarantor" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAdd">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboClassID" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtCoverageAmount" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdPlafondHistory" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboFilterByServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboFilterByApprovalStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdTransChargesItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdBloodRequest" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboFilterApprovalBloodRequest">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdBloodRequest" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdBloodRequest">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdBloodRequest" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdEpisodeDiagnose">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEpisodeDiagnose" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdEpisodeProcedure">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEpisodeProcedure" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdDocument">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDocument" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnReloadEklaim">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblCoverageEklaim" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblStatusEklaim" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblStatusKemenkes" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblGroupTarif" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblGroupDesc" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblGroupCode" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnSaveSep">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtSepNo" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtNoBpjs" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdRegistrationGuarantor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistrationGuarantor" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtEstimatedPlafon" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblPercentagePlafon" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblCoverageCob" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblAmountPlafon" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblBillingTotal" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="usedplafond" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="usedPlafond2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnSaveEstimaedPlafon">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtEstimatedPlafon" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblPercentagePlafon" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblCoverageCob" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblAmountPlafon" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblBillingTotal" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="usedplafond" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="usedPlafond2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterItemService">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistrationRule" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px" OnClientClose="onClientClose"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton ID="RadToolBarButton1" runat="server" Text="List" Value="list" ImageUrl="~/Images/Toolbar/details16.png"
                HoveredImageUrl="~/Images/Toolbar/details16_h.png" DisabledImageUrl="~/Images/Toolbar/details16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="MDS" Value="mds" ImageUrl="~/Images/Toolbar/ordering16.png" DisabledImageUrl="~/Images/Toolbar/ordering16_d.png" />
            <telerik:RadToolBarButton ID="RadToolBarButton2" runat="server" Text="Refresh" Value="reload" ImageUrl="~/Images/Toolbar/refresh16.png"
                HoveredImageUrl="~/Images/Toolbar/refresh16_h.png" DisabledImageUrl="~/Images/Toolbar/refresh16_d.png" />
        </Items>
    </telerik:RadToolBar>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 42%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="150px" MaxLength="20"
                                            ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="150px" ReadOnly="true" />
                                    </td>
                                    <td>&nbsp; <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();"
                                        class="noti_Container">
                                        <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo"
                                            Text=""></asp:Label>&nbsp; </a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">BPJS No. / SEP No.
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtNoBpjs" runat="server" Width="150px" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtSepNo" runat="server" Width="150px" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                        <asp:ImageButton ID="lbtnSaveSep" runat="server" ImageUrl="../../../../Images/Toolbar/save16.png"
                                            ToolTip="Save" OnClick="lbtnSaveSep_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
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
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="245px" ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="25px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPlaceDOB" runat="server" Text="City / Date Of Birth"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="198px" ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtDateOfBirth" runat="server" Width="100px" DateInput-ReadOnly="true"
                                            DatePopupButton-Enabled="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                        </td>
                        <td class="entry2Column">
                            <telerik:RadNumericTextBox ID="txtAgeInYear" runat="server" Width="49px" ReadOnly="true">
                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;Y&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeInMonth" runat="server" Width="49px" ReadOnly="true">
                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;M&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeInDay" runat="server" Width="49px" ReadOnly="true">
                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;D
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100px" MaxLength="20"
                                            ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblServiceUnitName" runat="server" CssClass="labeldescription" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRoomID" runat="server" Text="Room"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtRoomID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblRoomName" runat="server" CssClass="labeldescription" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                            <telerik:RadNumericTextBox ID="txtTariffDiscForRoomIn" runat="server" Width="100px"
                                ReadOnly="true" Visible="False" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBedID" runat="server" Text="Bed"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtBedID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox runat="server" ID="chkIsRoomIn" Enabled="False" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
            <td style="width: 42%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">Coverage Class
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtCoverageClass" runat="server" Width="100px" MaxLength="20"
                                            ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCoverageClassName" runat="server" CssClass="labeldescription" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblClassID" runat="server" Text="Charge Class"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtClassID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblClassName" runat="server" CssClass="labeldescription" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPhysician" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtParamedicID" runat="server" Width="100px" MaxLength="20"
                                            ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblParamedicName" runat="server" CssClass="labeldescription" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantor" runat="server" Text="Guarantor"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadTextBox ID="txtGuarantorID" runat="server" Width="100px" MaxLength="20"
                                                        ReadOnly="true" />
                                                </td>
                                                <td>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblGuarantorName" runat="server" CssClass="labeldescription" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>&nbsp; <a href="javascript:void(0);" onclick="javascript:openWinGuarantorInfo();"
                                        class="noti_Container">
                                        <asp:Label CssClass="noti_bubble" runat="server" ID="lblGuarantorInfo" AssociatedControlID="txtGuarantorID"
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
                            <asp:Label ID="lblLengthOfStay" runat="server" Text="Length Of Stay"></asp:Label>
                        </td>
                        <td class="entry2Column">
                            <telerik:RadNumericTextBox ID="txtLengthOfStay" runat="server" Width="100px" ReadOnly="True">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;Day(s)
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Height
                        </td>
                        <td class="entry2Column">
                            <telerik:RadNumericTextBox ID="txtHeight" runat="server" Width="100px" ReadOnly="True">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;cm
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Weight
                        </td>
                        <td class="entry2Column">
                            <telerik:RadNumericTextBox ID="txtWeight" runat="server" Width="100px" ReadOnly="True">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;kg
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">BMI
                        </td>
                        <td class="entry2Column">
                            <telerik:RadNumericTextBox ID="txtBMI" runat="server" Width="100px" ReadOnly="True">
                                <NumberFormat DecimalDigits="2" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 16%; vertical-align: top" rowspan="2">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="right">
                            <fieldset id="FieldSet1" style="width: 200px; min-height: 200px;">
                                <legend>Photo</legend>
                                <asp:Image runat="server" ID="imgPatientPhoto" Width="200px" Height="200px" />
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td width="50%">
                            <table cellpadding='0' cellspacing='1' style='width: 100%; border: 1px; color: white;'>
                                <tr align='center' style='background: gray; font-weight: bold;'>
                                    <td style='width: 150px;'>Plafon Projection
                                    </td>
                                    <td>Plafond Status
                                    </td>
                                    <td style='width: 150px;'>Total Billing + Admin
                                    </td>
                                </tr>
                                <tr style='font-weight: bold;'>
                                    <td style='background: gray;' align='center' rowspan='2'>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtEstimatedPlafon" runat="server" Width="100px" />
                                                </td>
                                                <td>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="lbtnSaveEstimaedPlafon" runat="server" ImageUrl="../../../../Images/Toolbar/save16.png"
                                                        ToolTip="Save" OnClick="lbtnSaveEstimaedPlafon_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table cellpadding='0' cellspacing='0' style='width: 100%'>
                                            <tr align='center'>
                                                <td id='usedplafond' runat='server'>
                                                    <asp:Label runat="server" ID="lblPercentagePlafon" />
                                                </td>
                                                <td id='remaining' style='background: black;' />
                                            </tr>
                                        </table>
                                    </td>
                                    <td style='background: gray;' align='center' rowspan='2'>
                                        <asp:Label runat="server" ID="lblBillingTotal" />
                                    </td>
                                </tr>
                                <tr style='font-weight: bold;'>
                                    <td>
                                        <table cellpadding='0' cellspacing='0' style='width: 100%'>
                                            <tr align='center'>
                                                <td id='usedPlafond2' runat='server' style='background: gray;'>
                                                    <asp:Label runat="server" ID="lblEstimatedPlafon2" />
                                                </td>
                                                <td style='background: black;'></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%">
                            <table cellpadding='0' cellspacing='1' style='width: 100%; border: 1px; color: white;'>
                                <tr align='center' style='background: gray; font-weight: bold;'>
                                    <td>C O B
                                    </td>
                                    <td colspan='4'>E-KLAIM
                                    </td>
                                    <td rowspan="3">
                                         <asp:Button ID="btnEklaim" runat="server" Text='<%# AppSession.Parameter.IsiDRGIntegration ? "iDRG" : "E-KLAIM" %>' Height="100%" OnClientClick="OpenEklaimPage();return false;" />
                                    </td>
                                    <td>KLAIM Status
                                    </td>
                                </tr>
                                <tr style='font-weight: bold;'>
                                    <td style='width: 150px; background: gray;' align='center' rowspan='2'>
                                        <asp:Label runat="server" ID="lblCoverageCob" />
                                    </td>
                                    <td style='width: 150px; background: gray;' align='center'>Coverage
                                    </td>
                                    <td style='width: 150px; background: gray;' align='center'>E-KLAIM Status
                                    </td>
                                    <td style='width: 150px; background: gray;' align='center'>KEMENKES Status
                                    </td>
                                    <td style='width: 25px; background: gray;' align='center' rowspan='2'>
                                        <asp:ImageButton ID="btnReloadEklaim" runat="server" ImageUrl="../../../../Images/Toolbar/refresh16.png"
                                            ToolTip="Load E-Klaim" OnClick="btnReloadEklaim_Click" Enabled='<%# Temiang.Avicenna.Common.Helper.IsInacbgIntegration %>' />
                                    </td>
                                    <td style='width: 150px; background: gray;' align='center' rowspan='2'>
                                        <asp:Label runat="server" ID="lblPaymentStatus" />
                                    </td>
                                </tr>
                                <tr style='font-weight: bold;'>
                                    <td style='width: 150px; background: gray;' align='center'>
                                        <asp:Label runat="server" ID="lblCoverageEklaim" />
                                    </td>
                                    <td style='width: 150px; background: gray;' align='center'>
                                        <asp:Label runat="server" ID="lblStatusEklaim" />
                                    </td>
                                    <td style='width: 150px; background: gray;' align='center'>
                                        <asp:Label runat="server" ID="lblStatusKemenkes" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <!-- NEW ROW: iDRG & INACBG -->
                <table cellpadding="0" cellspacing="0" width="100%" style="margin-top: 3px;">
                    <tr>
                        <!-- iDRG -->
                        <td width="50%">
                            <table cellpadding='0' cellspacing='1' style='width: 100%; border: 1px solid gray; color: white;'>
                                <tr>
                                    <td colspan="3" align="center"
                                        class="breathingText"
                                        style="font-weight: bold; background: gray; color: #fff; transition: color 0.5s ease-in-out;">iDRG Information
                                    </td>
                                </tr>
                                <tr align='center' style='background: gray; font-weight: bold;'>
                                    <td>iDRG - MDC</td>
                                    <td>iDRG - DRG</td>
                                    <td>iDRG - Cost Weight</td>
                                    <td>iDRG - NBR</td>
                                    <td>iDRG Status</td>
                                </tr>
                                <tr style='font-weight: bold; background: #f5f5f5; color: black;'>
                                    <td align="center">
                                        <asp:Label runat="server" ID="lblMdc" /></td>
                                    <td align="center">
                                        <asp:Label runat="server" ID="lblDrg" /></td>
                                    <td align="center">
                                        <asp:Label runat="server" ID="lblCostWeight" /></td>
                                    <td align="center">
                                        <asp:Label runat="server" ID="lblNBR" /></td>
                                    <td align="center">
                                        <asp:Label runat="server" ID="lblIdrgStatus" /></td>
                                </tr>
                            </table>
                        </td>
                        <!-- INACBG -->
                        <td width="50%">
                            <table cellpadding='0' cellspacing='1' style='width: 100%; border: 1px solid gray; color: white;'>
                                <tr>
                                    <td colspan="3" align="center"
                                        class="breathingText"
                                        style="font-weight: bold; background: gray; color: #fff; transition: color 0.5s ease-in-out;">INACBG Information
                                    </td>
                                </tr>
                                <tr align='center' style='background: gray; font-weight: bold;'>
                                    <td>Group Code</td>
                                    <td>Group Desc</td>
                                    <td>Group Tarif</td>
                                </tr>
                                <tr style='font-weight: bold; background: #f5f5f5; color: black;'>
                                    <td align="center">
                                        <asp:Label runat="server" ID="lblGroupCode" /></td>
                                    <td align="center">
                                        <asp:Label runat="server" ID="lblGroupDesc" /></td>
                                    <td align="center">
                                        <asp:Label runat="server" ID="lblGroupTarif" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
        ShowBaseLine="true">
        <Tabs>
            <telerik:RadTab runat="server" Text="Patient Billing List" PageViewID="pgIntermBill"
                Selected="true" />
            <telerik:RadTab runat="server" Text="Blood Transfusion Request" PageViewID="pgBloodRequest" />
            <telerik:RadTab runat="server" Text="C O B" Selected="True" PageViewID="pgOtherGuarantor"
                Visible="false" />
            <telerik:RadTab runat="server" Text="Diagnose ICD-10" PageViewID="pgDiagnosa" />
            <telerik:RadTab runat="server" Text="Procedure ICD-9CM" PageViewID="pgProcedure" />
            <telerik:RadTab runat="server" Text="Clinical Pathway" PageViewID="pgPathway" />
            <telerik:RadTab runat="server" Text="Document" PageViewID="pgDocument" />
            <telerik:RadTab runat="server" Text="Registration Rule Item" PageViewID="pgRegRule" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgIntermBill" runat="server" Selected="true">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="25%" valign="top">
                        <table>
                            <tr>
                                <td class="label">Filter By Service Unit
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboFilterByServiceUnitID" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboFilterByServiceUnitID_SelectedIndexChanged" Width="304px">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px" />
                            </tr>
                            <tr>
                                <td class="label">Filter By Approval Status
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboFilterByApprovalStatus" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboFilterByServiceUnitID_SelectedIndexChanged" Width="304px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="--All--" Value="" />
                                            <telerik:RadComboBoxItem Text="Outstanding" Value="1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px" />
                            </tr>
                        </table>
                    </td>
                    <td width="75%" valign="top">
                        <table width="100%" cellspacing="1">
                            <tr>
                                <td>
                                    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Total Billing by Group"
                                        IsCollapsed="true">
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="33%" valign="top">
                                                    <table>
                                                        <tr>
                                                            <td class="label">Prosedur Non Bedah
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtProsedurNonBedah" runat="server" ReadOnly="true" />
                                                            </td>
                                                            <td width="20px" />
                                                        </tr>
                                                        <tr>
                                                            <td class="label">Tenaga Ahli
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtTenagaAhli" runat="server" ReadOnly="true" />
                                                            </td>
                                                            <td width="20px" />
                                                        </tr>
                                                        <tr>
                                                            <td class="label">Radiologi
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtRadiologi" runat="server" ReadOnly="true" />
                                                            </td>
                                                            <td width="20px" />
                                                        </tr>
                                                        <tr>
                                                            <td class="label">Rehabilitasi
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtRehabilitasi" runat="server" ReadOnly="true" />
                                                            </td>
                                                            <td width="20px" />
                                                        </tr>
                                                        <tr>
                                                            <td class="label">Obat
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtObat" runat="server" ReadOnly="true" />
                                                            </td>
                                                            <td width="20px" />
                                                        </tr>
                                                        <tr>
                                                            <td class="label">Sewa Alat
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtSewaAlat" runat="server" ReadOnly="true" />
                                                            </td>
                                                            <td width="20px" />
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="34%" valign="top">
                                                    <table>
                                                        <tr>
                                                            <td class="label">Prosedur Bedah
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtProsedurBedah" runat="server" ReadOnly="true" />
                                                            </td>
                                                            <td width="20px" />
                                                        </tr>
                                                        <tr>
                                                            <td class="label">Keperawatan
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtKeperawatan" runat="server" ReadOnly="true" />
                                                            </td>
                                                            <td width="20px" />
                                                        </tr>
                                                        <tr>
                                                            <td class="label">Laboratorium
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtLaboratorium" runat="server" ReadOnly="true" />
                                                            </td>
                                                            <td width="20px" />
                                                        </tr>
                                                        <tr>
                                                            <td class="label">Kamar / Akomodasi
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtKamarAkomodasi" runat="server" ReadOnly="true" />
                                                            </td>
                                                            <td width="20px" />
                                                        </tr>
                                                        <tr>
                                                            <td class="label">Alkes
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtAlkes" runat="server" ReadOnly="true" />
                                                            </td>
                                                            <td width="20px" />
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="33%" valign="top">
                                                    <table>
                                                        <tr>
                                                            <td class="label">Konsultasi
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtKonsultasi" runat="server" ReadOnly="true" />
                                                            </td>
                                                            <td width="20px" />
                                                        </tr>
                                                        <tr>
                                                            <td class="label">Penunjang
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtPenunjang" runat="server" ReadOnly="true" />
                                                            </td>
                                                            <td width="20px" />
                                                        </tr>
                                                        <tr>
                                                            <td class="label">Pelayanan Darah
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtPelayananDarah" runat="server" ReadOnly="true" />
                                                            </td>
                                                            <td width="20px" />
                                                        </tr>
                                                        <tr>
                                                            <td class="label">Rawat Intensif
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtRawatIntensifTarif" runat="server" ReadOnly="true" />
                                                            </td>
                                                            <td width="20px" />
                                                        </tr>
                                                        <tr>
                                                            <td class="label">BMHP
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtBMHP" runat="server" ReadOnly="true" />
                                                            </td>
                                                            <td width="20px" />
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </cc:CollapsePanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdTransChargesItem" runat="server" OnNeedDataSource="grdTransChargesItem_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" ShowFooter="true">
                <MasterTableView DataKeyNames="RegistrationNo,TransactionNo,SequenceNo,TYPE" CommandItemDisplay="None">
                    <CommandItemStyle Height="29px" />
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="Group" HeaderText="Service Unit " />
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="Group" SortOrder="None" />
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="CommandTemplateColumn" HeaderStyle-Width="40px"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "IsCasemixApproved").Equals(true) || (DataBinder.Eval(Container.DataItem, "IsCasemixApproved").Equals(false) && !DataBinder.Eval(Container.DataItem, "CasemixApprovedByUserID").Equals(DBNull.Value)) ? string.Empty : 
                                    string.Format("<a href=\"#\" onclick=\"ApproveRejectDialog('0', '{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/Toolbar/post_green_16.png\" border=\"0\" title=\"Approve\" /></a>", 
                                    DataBinder.Eval(Container.DataItem, "TYPE"), DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"))%>
                                <hr />
                                <%# DataBinder.Eval(Container.DataItem, "IsCasemixApproved").Equals(true) || (DataBinder.Eval(Container.DataItem, "IsCasemixApproved").Equals(false) && !DataBinder.Eval(Container.DataItem, "CasemixApprovedByUserID").Equals(DBNull.Value)) ? string.Empty : 
                                    string.Format("<a href=\"#\" onclick=\"ApproveRejectDialog('1', '{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/Toolbar/row_delete16.png\" border=\"0\" title=\"Reject\" /></a>", 
                                    DataBinder.Eval(Container.DataItem, "TYPE"), DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="RegistrationNo" UniqueName="RegistrationNo" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TransactionNo" UniqueName="TransactionNo" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SequenceNo" UniqueName="SequenceNo" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IsApprove" UniqueName="IsApprove" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="ApproveTemplateColumn" HeaderText="Approval"
                            Visible="false">
                            <ItemTemplate>
                                <%# GetStatus(DataBinder.Eval(Container.DataItem, "IsOrder"), DataBinder.Eval(Container.DataItem, "IsOrderRealization"), DataBinder.Eval(Container.DataItem, "IsApprove")) %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="IsVoid" UniqueName="IsVoid" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IsBillProceed" UniqueName="IsBillProceed" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="BillingTemplateColumn" Visible="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkIsApproved" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsApprove") %>'
                                    Enabled='false' Text="Approve"></asp:CheckBox><br />
                                <asp:CheckBox ID="chkIsBillProceed" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsBillProceed") %>'
                                    Enabled='false' Text="Proceed"></asp:CheckBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="IsOrder" UniqueName="IsOrder" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IsOrderRealization" UniqueName="IsOrderRealization"
                            Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="OrderTemplateColumn" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:CheckBox ID="orderChkbox" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsOrder") %>'
                                    Enabled="false" Text="Order"></asp:CheckBox><br />
                                <asp:CheckBox ID="realizationChkbox" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsOrderRealization") %>'
                                    Enabled="false" Text="Realization"></asp:CheckBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="StatusTemplateColumn" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:CheckBox ID="paymentChkbox" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsPaymentProceed") %>'
                                    Enabled="false" Text="Paid"></asp:CheckBox><br />
                                <asp:CheckBox ID="intermbillChkbox" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsIntermBillProceed") %>'
                                    Enabled="false" Text="Interm Billed"></asp:CheckBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="DiscountTemplateColumn" HeaderText="Setting"
                            Visible="false">
                            <HeaderStyle Width="50px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "IsApprove").Equals(false) || DataBinder.Eval(Container.DataItem, "IsPaymentProceed").Equals(true) || DataBinder.Eval(Container.DataItem, "IsIntermBillProceed").Equals(true) || DataBinder.Eval(Container.DataItem, "IsCorrected").Equals(true) ? string.Empty :
                                        string.Format("<a href=\"#\" onclick=\"openWinProcess('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')\">Setting</a>", DataBinder.Eval(Container.DataItem, "TYPE"), DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "TransactionNo"),
                                        DataBinder.Eval(Container.DataItem, "SequenceNo"), DataBinder.Eval(Container.DataItem, "ItemID"), DataBinder.Eval(Container.DataItem, "IsHoldTransactionEntry")) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="ExecutionDate" HeaderText="Execution Date" UniqueName="ExecutionDate"
                            SortExpression="ExecutionDate" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="ItemNameNotesTemplateColumn" HeaderText="Item Name">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                                <br />
                                <i>Casemix Notes : <span style="color: blue"><%# DataBinder.Eval(Container.DataItem, "Notes")%></span></i>
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
                        <telerik:GridTemplateColumn UniqueName="UpdaterTemplateColumn" HeaderText="Transaction"
                            Visible="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                Update by :
                                <%# DataBinder.Eval(Container.DataItem, "LastUpdateByUserID")%><br />
                                <%# String.Format("{0:dd/MM/yyyy HH:mm}", DataBinder.Eval(Container.DataItem, "LastUpdateDateTime"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="TYPE" UniqueName="TYPE" Visible="false" />
                        <telerik:GridBoundColumn DataField="CreatedDateTime" HeaderText="Created Date/Time" UniqueName="CreatedDateTime"
                            SortExpression="CreatedDateTime" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="CasemixUpdaterTemplateColumn" HeaderText="Status">
                            <HeaderStyle Width="150px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <%# string.Format("<img src=\"../../../../Images/Toolbar/{0}.png\" border=\"0\" alt=\"\" title=\"\" />", DataBinder.Eval(Container.DataItem, "IsCasemixApproved").Equals(true) ? "post_green_16" :
                                   (DataBinder.Eval(Container.DataItem, "IsCasemixApproved").Equals(false) && !DataBinder.Eval(Container.DataItem, "CasemixApprovedByUserID").Equals(DBNull.Value)) ? "row_delete16" : "post16_d")%>
                                Update by :
                                <%# DataBinder.Eval(Container.DataItem, "CasemixApprovedByUserID")%>
                                <br />
                                <%# String.Format("{0:dd/MM/yyyy HH:mm}", DataBinder.Eval(Container.DataItem, "CasemixApprovedDateTime"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="True">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgBloodRequest" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="25%" valign="top">
                        <table>
                            <tr>
                                <td class="label">Filter By Approval Status
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboFilterApprovalBloodRequest" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboFilterApprovalBloodRequest_SelectedIndexChanged" Width="304px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="--All--" Value="" />
                                            <telerik:RadComboBoxItem Text="Outstanding" Value="1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px" />
                            </tr>
                        </table>
                    </td>
                    <td width="75%" valign="top">
                        <table width="100%" cellspacing="1">
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdBloodRequest" runat="server" OnNeedDataSource="grdBloodRequest_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" ShowFooter="true" OnItemCommand="grdBloodRequest_ItemCommand">
                <MasterTableView DataKeyNames="TransactionNo" CommandItemDisplay="None">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="CommandTemplateColumn" HeaderStyle-Width="40px"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsValidatedByCasemix").Equals(true) || (DataBinder.Eval(Container.DataItem, "IsValidatedByCasemix").Equals(false) && !DataBinder.Eval(Container.DataItem, "ValidatedByCasemixUserID").Equals(DBNull.Value))) ? string.Empty : 
                                    string.Format("<a href=\"#\" onclick=\"ApproveRejectDialog2('0', '{0}'); return false;\"><img src=\"../../../../Images/Toolbar/post_green_16.png\" border=\"0\" title=\"Approve\" /></a>", 
                                    DataBinder.Eval(Container.DataItem, "TransactionNo"))%>
                                <hr />
                                <%# (DataBinder.Eval(Container.DataItem, "IsValidatedByCasemix").Equals(true) || (DataBinder.Eval(Container.DataItem, "IsValidatedByCasemix").Equals(false) && !DataBinder.Eval(Container.DataItem, "ValidatedByCasemixUserID").Equals(DBNull.Value))) ? string.Empty : 
                                    string.Format("<a href=\"#\" onclick=\"ApproveRejectDialog2('1', '{0}'); return false;\"><img src=\"../../../../Images/Toolbar/row_delete16.png\" border=\"0\" title=\"Reject\" /></a>", 
                                    DataBinder.Eval(Container.DataItem, "TransactionNo"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="linkApprove" runat="server" OnClientClick="javascript:if(!confirm('Approve selected transaction?'))return false;"
                                    CommandName="approve" Visible='<%# DataBinder.Eval(Container.DataItem, "IsVisible") %>'><img src="../../../../Images/Toolbar/post_green_16.png" border="0" /></asp:LinkButton>
                                <hr />
                                <%# (DataBinder.Eval(Container.DataItem, "IsValidatedByCasemix").Equals(true) || (DataBinder.Eval(Container.DataItem, "IsValidatedByCasemix").Equals(false) && !DataBinder.Eval(Container.DataItem, "ValidatedByCasemixUserID").Equals(DBNull.Value))) ? string.Empty : 
                                    string.Format("<a href=\"#\" onclick=\"ApproveRejectDialog2('1', '{0}'); return false;\"><img src=\"../../../../Images/Toolbar/row_delete16.png\" border=\"0\" title=\"Reject\" /></a>", 
                                    DataBinder.Eval(Container.DataItem, "TransactionNo"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Qty Approve" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" UniqueName="txtQtyBagCasemixAppr">
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txtQtyBagCasemixAppr" runat="server" NumberFormat-DecimalDigits="0"
                                    Value='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "QtyBagCasemixAppr")) %>'
                                    MinValue="0"
                                    MaxValue='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "QtyBagRequest")) %>'
                                    Enabled='<%# System.Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsVisible")) %>'
                                    Width="50px">
                                </telerik:RadNumericTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridBoundColumn DataField="RegistrationNo" UniqueName="RegistrationNo" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Transaction No"
                            UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="RequestDate" HeaderText="Request Date" UniqueName="RequestDate"
                            SortExpression="RequestDate" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="RequestTime" HeaderText="Request Time"
                            UniqueName="RequestTime" SortExpression="RequestTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="BloodBankNo" HeaderText="Blood Bank No"
                            UniqueName="BloodBankNo" SortExpression="BloodBankNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PdutNo" HeaderText="PDUT No"
                            UniqueName="PdutNo" SortExpression="PdutNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="BloodType" HeaderText="Blood Type"
                            UniqueName="BloodType" SortExpression="BloodType" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="BloodRhesus" HeaderText="Rhesus"
                            UniqueName="BloodRhesus" SortExpression="BloodRhesus" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn UniqueName="ItemNameNotesTemplateColumn" HeaderText="Blood Group">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "BloodGroup")%>
                                <br />
                                <i>Casemix Notes : <span style="color: blue"><%# DataBinder.Eval(Container.DataItem, "CasemixNotes")%></span></i>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="QtyBagRequest" HeaderText="Qty Request"
                            UniqueName="QtyBagRequest" SortExpression="QtyBagRequest" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="QtyBagRequest" HeaderText="Volume (ML/CC)"
                            UniqueName="VolumeBag" SortExpression="VolumeBag" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="20px"></telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="CasemixUpdaterTemplateColumn" HeaderText="Status">
                            <HeaderStyle Width="150px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <%# string.Format("<img src=\"../../../../Images/Toolbar/{0}.png\" border=\"0\" alt=\"\" title=\"\" />", DataBinder.Eval(Container.DataItem, "IsValidatedByCasemix").Equals(true)  ? "post_green_16" :
                                   (DataBinder.Eval(Container.DataItem, "IsValidatedByCasemix").Equals(false) && !DataBinder.Eval(Container.DataItem, "ValidatedByCasemixUserID").Equals(DBNull.Value)) ? "row_delete16" : "post16_d")%>
                                Update by :
                                <%# DataBinder.Eval(Container.DataItem, "ValidatedByCasemixUserID")%>
                                <br />
                                <%# String.Format("{0:dd/MM/yyyy HH:mm}", DataBinder.Eval(Container.DataItem, "ValidatedByCasemixDateTime"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="True">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgOtherGuarantor" runat="server">
            <telerik:RadGrid ID="grdRegistrationGuarantor" runat="server" OnNeedDataSource="grdRegistrationGuarantor_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdRegistrationGuarantor_UpdateCommand"
                OnDeleteCommand="grdRegistrationGuarantor_DeleteCommand" OnInsertCommand="grdRegistrationGuarantor_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="RegistrationNo,GuarantorID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="GuarantorID" HeaderText="ID" UniqueName="GuarantorID"
                            SortExpression="GuarantorID">
                            <HeaderStyle HorizontalAlign="Left" Width="90px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                            SortExpression="GuarantorName">
                            <HeaderStyle HorizontalAlign="Left" Width="350px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PlafondAmount" HeaderText="Plafond Amount" UniqueName="PlafondAmount"
                            SortExpression="PlafondAmount" DataFormatString="{0:n2}">
                            <HeaderStyle HorizontalAlign="Center" Width="150px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="~/Module/RADT/Registration/RegistrationGuarantorDetail.ascx"
                        EditFormType="WebUserControl">
                        <EditColumn UniqueName="RegistrationGuarantorEditCommand">
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
        <telerik:RadPageView ID="pgDiagnosa" runat="server">
            <telerik:RadGrid ID="grdEpisodeDiagnose" runat="server" OnNeedDataSource="grdEpisodeDiagnose_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEpisodeDiagnose_UpdateCommand"
                OnDeleteCommand="grdEpisodeDiagnose_DeleteCommand" OnInsertCommand="grdEpisodeDiagnose_InsertCommand"
                OnItemCreated="grdEpisodeDiagnose_ItemCreated" AllowSorting="true">
                <MasterTableView DataKeyNames="RegistrationNo, SequenceNo" CommandItemDisplay="None">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton" Visible="false">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="DiagnoseID" HeaderText="Diagnosis ID"
                            UniqueName="DiagnoseID" SortExpression="DiagnoseID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="DiagnoseName" HeaderText="Diagnosis Name" UniqueName="DiagnoseName"
                            SortExpression="DiagnoseName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            AllowSorting="false" />
                        <telerik:GridBoundColumn DataField="DiagnoseType" HeaderText="Diagnosis Type" UniqueName="DiagnoseType"
                            SortExpression="DiagnoseType" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                            SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            AllowSorting="false" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsAcuteDisease" HeaderText="Acute"
                            UniqueName="IsAcuteDisease" SortExpression="IsAcuteDisease" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsChronicDisease"
                            HeaderText="Chronic" UniqueName="IsChronicDisease" SortExpression="IsChronicDisease"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsOldCase" HeaderText="Old Case"
                            UniqueName="IsOldCase" SortExpression="IsOldCase" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsConfirmed" HeaderText="Confirmed"
                            UniqueName="IsConfirmed" SortExpression="IsConfirmed" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="CreateDateTime" HeaderText="Create Date Time"
                            UniqueName="CreateDateTime" SortExpression="CreateDateTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="CreateByUserID" HeaderText="Create By"
                            UniqueName="CreateByUserID" SortExpression="CreateByUserID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update Date Time" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                            HeaderText="Last Update By User ID" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?" Visible="false">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="../../MedicalRecord/DiagnosisAndProcedure/EpisodeDiagDetail.ascx"
                        EditFormType="WebUserControl">
                        <EditColumn UniqueName="EpisodeDiagnoseEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true" />
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgProcedure" runat="server">
            <telerik:RadGrid ID="grdEpisodeProcedure" runat="server" OnNeedDataSource="grdEpisodeProcedure_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEpisodeProcedure_UpdateCommand"
                OnDeleteCommand="grdEpisodeProcedure_DeleteCommand" OnInsertCommand="grdEpisodeProcedure_InsertCommand"
                AllowSorting="true" OnItemCreated="grdEpisodeProcedure_ItemCreated">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="RegistrationNo, SequenceNo">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton" Visible="false">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ProcedureDate" HeaderText="Date"
                            UniqueName="ProcedureDate" SortExpression="ProcedureDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="ProcedureTime" HeaderText="Time"
                            UniqueName="ProcedureTime" SortExpression="ProcedureTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                            SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            AllowSorting="false" />
                        <telerik:GridBoundColumn DataField="ProcedureName" HeaderText="Procedure Name" UniqueName="ProcedureName"
                            SortExpression="ProcedureName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            AllowSorting="false" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="CreateDateTime" HeaderText="Create Date Time"
                            UniqueName="CreateDateTime" SortExpression="CreateDateTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="CreateByUserID" HeaderText="Create By"
                            UniqueName="CreateByUserID" SortExpression="CreateByUserID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update Date Time" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                            HeaderText="Last Update By User ID" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?" Visible="false">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="../../MedicalRecord/DiagnosisAndProcedure/EpisodeProcDetail.ascx"
                        EditFormType="WebUserControl">
                        <EditColumn UniqueName="EpisodeProcedureEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true" />
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgPathway" runat="server">
            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                AutoGenerateColumns="false" OnItemDataBound="grdList_ItemDataBound" OnItemCommand="grdList_ItemCommand">
                <MasterTableView DataKeyNames="PathwayID,PathwayItemSeqNo" CommandItemDisplay="None">
                    <CommandItemTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="50%">&nbsp;&nbsp;&nbsp; Pathway Name :&nbsp
                                    <telerik:RadComboBox ID="cboPathwayName" runat="server" Width="304px" MarkFirstMatch="true"
                                        HighlightTemplatedItems="true" EnableLoadOnDemand="true" AutoPostBack="true"
                                        OnItemDataBound="cboPathwayName_ItemDataBound" OnItemsRequested="cboPathwayName_ItemsRequested"
                                        OnSelectedIndexChanged="cboPathwayName_SelectedIndexChanged">
                                        <FooterTemplate>
                                            Note : Show max 20 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="50%" align="right">
                                    <a href='#' onclick='javascript:ReloadPathway(0);return false;'>
                                        <img src='../../../../Images/Toolbar/refresh16.png' border='0' title='Refresh' /></a>
                                    &nbsp;Refresh &nbsp;&nbsp;&nbsp; <a href='#' onclick='javascript:ReloadPathway(1);return false;'>
                                        <img src='../../../../Images/Toolbar/row_delete16.png' border='0' title='Clear' /></a>
                                    &nbsp;Clear &nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </CommandItemTemplate>
                    <CommandItemStyle Height="29px" />
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="PathwayName" HeaderText="Pathway Name "></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="PathwayName" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="AssesmentGroupName" HeaderText="Item Group Name "></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="AssesmentGroupName" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridBoundColumn DataField="AssesmentGroupName" HeaderText="Group Name" UniqueName="AssesmentGroupName"
                            SortExpression="AssesmentGroupName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            Visible="false" />
                        <telerik:GridBoundColumn DataField="AssesmentName" HeaderText="Activity Name" UniqueName="AssesmentName"
                            SortExpression="AssesmentName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="100px" UniqueName="col_1" HeaderText="Day 1"
                            DataField="col_1" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtn_1" CommandArgument="SetValue|1" CommandName='<%# DataBinder.Eval(Container.DataItem, "col_1")%>' Enabled="false">
                                    <%# string.Format("<img src=\"../../../../Images/Toolbar/{0}.png\" border=\"0\" alt=\"\" title=\"\" />", DataBinder.Eval(Container.DataItem, "chk_1").Equals(true) ? "post_green_16" : "post16_d")%>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="100px" UniqueName="col_2" HeaderText="Day 2"
                            DataField="col_2" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtn_2" CommandArgument="SetValue|2" CommandName='<%# DataBinder.Eval(Container.DataItem, "col_2")%>' Enabled="false">
                                    <%# string.Format("<img src=\"../../../../Images/Toolbar/{0}.png\" border=\"0\" alt=\"\" title=\"\" />", DataBinder.Eval(Container.DataItem, "chk_2").Equals(true) ? "post_green_16" : "post16_d")%>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="100px" UniqueName="col_3" HeaderText="Day 3"
                            DataField="col_3" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtn_3" CommandArgument="SetValue|3" CommandName='<%# DataBinder.Eval(Container.DataItem, "col_3")%>' Enabled="false">
                                    <%# string.Format("<img src=\"../../../../Images/Toolbar/{0}.png\" border=\"0\" alt=\"\" title=\"\" />", DataBinder.Eval(Container.DataItem, "chk_3").Equals(true) ? "post_green_16" : "post16_d")%>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="100px" UniqueName="col_4" HeaderText="Day 4"
                            DataField="col_4" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtn_4" CommandArgument="SetValue|4" CommandName='<%# DataBinder.Eval(Container.DataItem, "col_4")%>' Enabled="false">
                                    <%# string.Format("<img src=\"../../../../Images/Toolbar/{0}.png\" border=\"0\" alt=\"\" title=\"\" />", DataBinder.Eval(Container.DataItem, "chk_4").Equals(true) ? "post_green_16" : "post16_d")%>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="100px" UniqueName="col_5" HeaderText="Day 5"
                            DataField="col_5" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtn_5" CommandArgument="SetValue|5" CommandName='<%# DataBinder.Eval(Container.DataItem, "col_5")%>' Enabled="false">
                                    <%# string.Format("<img src=\"../../../../Images/Toolbar/{0}.png\" border=\"0\" alt=\"\" title=\"\" />", DataBinder.Eval(Container.DataItem, "chk_5").Equals(true) ? "post_green_16" : "post16_d")%>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="100px" UniqueName="col_6" HeaderText="Day 6"
                            DataField="col_6" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtn_6" CommandArgument="SetValue|6" CommandName='<%# DataBinder.Eval(Container.DataItem, "col_6")%>' Enabled="false">
                                    <%# string.Format("<img src=\"../../../../Images/Toolbar/{0}.png\" border=\"0\" alt=\"\" title=\"\" />", DataBinder.Eval(Container.DataItem, "chk_6").Equals(true) ? "post_green_16" : "post16_d")%>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="100px" UniqueName="col_7" HeaderText="Day 7"
                            DataField="col_7" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtn_7" CommandArgument="SetValue|7" CommandName='<%# DataBinder.Eval(Container.DataItem, "col_7")%>' Enabled="false">
                                    <%# string.Format("<img src=\"../../../../Images/Toolbar/{0}.png\" border=\"0\" alt=\"\" title=\"\" />", DataBinder.Eval(Container.DataItem, "chk_7").Equals(true) ? "post_green_16" : "post16_d")%>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="PathwayItemID" UniqueName="PathwayItemID" SortExpression="PathwayItemID"
                            Visible="false" />
                        <telerik:GridBoundColumn DataField="PathwayID" UniqueName="PathwayID" SortExpression="PathwayID"
                            Visible="false" />
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgDocument" runat="server">
            <telerik:RadGrid ID="grdDocument" runat="server" OnNeedDataSource="grdDocument_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdDocument_DeleteCommand">
                <MasterTableView DataKeyNames="Name" CommandItemDisplay="Top">
                    <CommandItemTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="50%">&nbsp;&nbsp;&nbsp;
                                    <a href='#' onclick='javascript:MergeDocument();return false;'>
                                        <img src='../../../../Images/Toolbar/save16.png' border='0' title='Merge' /></a>
                                    &nbsp;Merge &nbsp;&nbsp;&nbsp;
                                </td>
                                <td width="50%" align="right">
                                    <a href='#' onclick='javascript:ReloadDocument();return false;'>
                                        <img src='../../../../Images/Toolbar/refresh16.png' border='0' title='Refresh' /></a>
                                    &nbsp;Refresh &nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </CommandItemTemplate>
                    <CommandItemStyle Height="29px" />
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="30px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedStateDocuments" AutoPostBack="True"
                                    runat="server" Checked="false"></asp:CheckBox>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="detailChkbox" runat="server" Checked="False"></asp:CheckBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="Order" HeaderStyle-Width="50px" HeaderText="Order" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="orderTextBox" runat="server" Width="96%" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MaxLength="2"></telerik:RadNumericTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="CommandTemplateColumn" HeaderStyle-Width="40px"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"DocumentFileAction(1, '{0}'); return false;\"><img src=\"../../../../Images/Toolbar/print_preview16.png\" border=\"0\" title=\"Open\" /></a>", 
                                    DataBinder.Eval(Container.DataItem, "Url"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Name" UniqueName="Name" HeaderText="File Name"
                            SortExpression="Name">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Path" UniqueName="Path" HeaderText="File Path"
                            SortExpression="Path">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Size" UniqueName="Size" HeaderText="File Size"
                            SortExpression="Size">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Accessed" HeaderText="Accessed Date/Time" UniqueName="Accessed"
                            SortExpression="Accessed" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="150px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete" Visible="false"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="True">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgRegRule" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblFilterItemService" runat="server" Text="Item ID / Item Name"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtFilterItemService" runat="server" Width="300px" MaxLength="100" />
                                </td>
                                <td width="20">
                                    <asp:ImageButton ID="btnFilterItemService" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdRegistrationRule" runat="server" OnNeedDataSource="grdRegistrationRule_NeedDataSource"
                GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="25"
                AllowSorting="true" OnInsertCommand="grdRegistrationRule_InsertCommand" OnUpdateCommand="grdRegistrationRule_UpdateCommand"
                OnDeleteCommand="grdRegistrationRule_DeleteCommand">
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="ItemID" ClientDataKeyNames="ItemID"
                    GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID">
                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name"
                            UniqueName="ItemName" SortExpression="ItemName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Qty" HeaderText="Qty"
                            UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="CasemixExceptionRegistrationRuleItemDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="CasemixExceptionRegistrationRuleItemEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
