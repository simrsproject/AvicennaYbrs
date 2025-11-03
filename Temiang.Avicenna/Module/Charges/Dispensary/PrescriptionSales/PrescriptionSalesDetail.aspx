<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="PrescriptionSalesDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.PrescriptionSalesDetail" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/CustomControl/SoapInfoCtl.ascx" TagPrefix="uc1" TagName="SoapInfoCtl" %>
<%@ Register Src="~/CustomControl/VitalSignInfoCtl.ascx" TagPrefix="cc" TagName="VitalSignInfoCtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <script type="text/javascript" language="javascript">
            function openVitalSignChart(vitalSignID) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/Common/VitalSignChart.aspx?patid=<%= PatientID %>&regno=<%= RegistrationNo %>&fregno=&vid=' + vitalSignID;
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
                oWnd.setSize(1000, 600);
                oWnd.center();
            }
            function openPrevBuyDialog(itemid, itemiid) {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var prescNo = $find("<%= txtPrescriptionNo.ClientID %>");
                oWnd.setUrl("PrevBuyDialog.aspx?itemid=" + itemid + "&itemiid=" + itemiid + "&rno=" + regNo.get_value() + "&pno=" + prescNo.get_value());

                oWnd.set_title('Previous Buy Info');
                oWnd.set_width(900);
                oWnd.set_height(300);
                oWnd.show();
            }

            function openWinBalanceInfo(itemid) {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                oWnd.setUrl("ItemBalanceInfo.aspx?itemid=" + itemid);
                oWnd.set_title('Balance Info');
                oWnd.set_width(900);
                oWnd.set_height(300);
                oWnd.show();
            }

            function openWinPatient(mode, recID, patient) {
                var combo = $find("<%= cboServiceUnitID.ClientID %>");
                if (combo.get_value() != '') {
                    var oWnd = $find("<%= radWinPatient.ClientID %>");
                    oWnd.setUrl("../../../RADT/Registration/PatientDetail.aspx?md=" + mode + "&pid=" + recID + "&pt=" + patient + "&rt=&unit=" + combo.get_value(), "radWinPatient");
                    oWnd.show();
                }
            }

            function onClientCloseWinPatient(oWnd, args) {
                if (oWnd.argument && oWnd.argument.PatientID != null) {
                    __doPostBack('ShowNewPatient', oWnd.argument.PatientID);
                }
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

            function OpenEtiquette(pNo, sNo) {
                var olocId = $find("<%= cboLocationID.ClientID %>");
                window.location.href = 'PrescriptionSalesEtiquetteDetail.aspx?' +
                    'md=view' +
                    '&pno=' + pNo + '&sno=' + sNo +
                    '&regno=<%= Request.QueryString["regno"] %>' +
                    '&type=<%= Request.QueryString["type"] %>' +
                    '&rt=<%= Request.QueryString["rt"] %>' +
                    '&locid=' + olocId.get_value();
            }
            function openWinGuarantorInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var oguar = $find("<%= cboGuarantorID.ClientID %>");
                var lblToBeUpdate = "<%= lblGuarantorInfo.ClientID %>";
                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/Finance/Master/GuarantorInfo/GuarantorInfoDialog.aspx?id=' + oguar.get_value() + '&lblGuarantorInfo=' + lblToBeUpdate + '")%>');
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function openTextEditorDialog(transID, seqNo) {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                oWnd.setUrl('TextDialog.aspx?id=' + transID + '&seq=' + seqNo);
                oWnd.set_width(480);
                oWnd.set_height(150);
                oWnd.show();
            }

            function txtBarcodeEntryKeyPress(sender, eventArgs) {
                var code = eventArgs.get_keyCode();
                if (code == 13) {
                    eventArgs.set_cancel(true); // Supaya tidak membuka edit grid
                    __doPostBack(sender._clientID.replace(/_/g, "$"), "addwithbarcode|" + sender.get_value());
                }
            }

            function gotoAddPaymentReceivedUrl() {
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var prescNo = $find("<%= txtPrescriptionNo.ClientID %>");
                var url = '../../Cashier/PaymentReceiveDirect/PaymentReceiveDirectDetail.aspx?md=new&regno=' + regNo.get_value() + '&prescno=' + prescNo.get_value();
                window.location.href = url;
            }

            function openWinEducation() {
                var oWnd = $find("<%= winReview.ClientID %>");
<%--                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/Charges/Dispensary/PrescriptionSales/PatientEducationEntry.aspx?mod=edit&regno="+txtRegistrationNo.Text+"&prescno="+txtPrescriptionNo.Text+"&ccm=setvalue&cet="+lblEducationByUserName.ClientID)%>');--%>

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/EmrIp/EmrIpCommon/PatientEducation/PatientEducationDetail.aspx?mod=edit&regno="+txtRegistrationNo.Text+"&prescno="+txtPrescriptionNo.Text+"&ccm=setvalue&cet="+lblEducationByUserName.ClientID)%>');
                oWnd.show();
                oWnd.setSize(1100, 800);
                oWnd.center();
            }

            function openWinReview() {
                var oWnd = $find("<%= winReview.ClientID %>");
                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/Charges/Dispensary/PrescriptionSales/PrescriptionReview.aspx?mod=edit&regno="+txtRegistrationNo.Text+"&prescno="+txtPrescriptionNo.Text+"&prgid="+ProgramID+"&ptype=p&ccm=setvalue&cet="+lblReviewByUserName.ClientID)%>');
                oWnd.show();
                oWnd.setSize(800, 500);
                oWnd.center();
            }

            function openWinReview2() {
                var oWnd = $find("<%= winReview.ClientID %>");
                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/Charges/Dispensary/PrescriptionSales/PrescriptionReview.aspx?mod=edit&regno="+txtRegistrationNo.Text+"&prescno="+txtPrescriptionNo.Text+"&prgid="+ProgramID+"&ptype=d&ccm=setvalue&cet="+lblDrugReviewByUserName.ClientID)%>');
                oWnd.show();
                oWnd.setSize(800, 500);
                oWnd.center();
            }

            function openWinReview3() {
                var oWnd = $find("<%= winReview.ClientID %>");
                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/Charges/Dispensary/PrescriptionSales/PrescriptionReview.aspx?mod=edit&regno="+txtRegistrationNo.Text+"&prescno="+txtPrescriptionNo.Text+"&prgid="+ProgramID+"&ptype=n&ccm=setvalue&cet="+lblFollowUpByUserName.ClientID)%>');
                oWnd.show();
                oWnd.setSize(800, 500);
                oWnd.center();
            }

            function openWinProgress() {
                var oWnd = $find("<%= winReview.ClientID %>");
                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/Charges/Dispensary/PrescriptionSales/PrescriptionProgress.aspx?mod=edit&regno="+txtRegistrationNo.Text+"&prescno="+txtPrescriptionNo.Text+"&prgid="+ProgramID+"&ccm=setvalue&cet="+lblProgress.ClientID)%>');
                oWnd.show();
                oWnd.setSize(600, 300);
                oWnd.center();
            }
            function openWinHighAlert() {
                var oWnd = $find("<%= winReview.ClientID %>");
                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/Charges/Dispensary/PrescriptionSales/PrescriptionHighAlertCheck.aspx?mod=edit&regno="+txtRegistrationNo.Text+"&prescno="+txtPrescriptionNo.Text+"&prgid="+ProgramID+"&ccm=setvalue&cet="+lblHighAlert.ClientID)%>');
                oWnd.show();
                oWnd.setSize(600, 300);
                oWnd.center();
            }
            function winReview_ClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg != null) {
                    if (arg.callbackMethod === 'setvalue') {
                        document.getElementById(arg.eventTarget).innerHTML = arg.value;
                    }
                }
            }

            function gotoView23PrescUrl() {
                var regno = $find("<%= txtRegistrationNo.ClientID %>");
                var pno = $find("<%= txtPrescriptionNo.ClientID %>");
                var url = 'PrescriptionSalesDetail.aspx?md=view&pno=' + pno.get_value() + '&regno=' + regno.get_value() + "&type=sales&rt=" + '<%= Request.QueryString["rt"] %>' + "&ono=&mode=direct&refno=bpjs";
                window.location.href = url;
            }

            var _stockStyleDisplay = '<%= AppParameter.IsYes(AppParameter.ParameterItem.IsPrescriptionShowStock)?"block":"none" %>';
            function cboItemID_ClientItemsRequesting(sender, eventArgs) {
                var context = eventArgs.get_context();

<%--                if (true === <%=cboServiceUnitID.Enabled.ToString().ToLower()%>) {
                    var cboServ = $find("<%=cboServiceUnitID.ClientID %>");
                    context["ServiceUnitID"] = cboServ.get_selectedItem().get_value();

                    var cboLoc = $find("<%=cboLocationID.ClientID %>");
                    context["LocationID"] = cboLoc.get_selectedItem().get_value();
                }
                else {
                    context["ServiceUnitID"] = "<%=Page.Request.QueryString["unit"]%>";
                    context["LocationID"] = "<%=Page.Request.QueryString["loc"]%>";
                }--%>

                context["ServiceUnitID"] = document.getElementById("<%=hdnServiceUnitID.ClientID%>").value;
                context["LocationID"] = document.getElementById("<%=hdnLocationID.ClientID%>").value;

                context["IsOnlyInStock"] = <%= AppSession.Parameter.IsPrescriptionOnlyInStock.ToString().ToLower() %>;

                <%--Filter Item berdasarkan Guarantor tidak diterapkan pada sales ini krn pasien boleh ambil obat lain--%>
                context["GuarantorID"] = "";

                context["UserType"] = "<%=AppSession.UserLogin.SRUserType%>";

                <%--Raspro di prescription sales tidak diterapkan--%>
                context["IsRasproEnable"] = false;
                context["AbLevel"] = "";
                context["AbRestrictionID"] = "";
                context["RasproSeqNo"] = "0";
                context["RasproType"] = "";
                context["RegistrationNo"] = "<%=RegistrationNo%>";
                context["IsFornas"] = "NULL"; <%-- Ambil di webservicec method nya--%>
                context["IsBalTot"] = true;
                context["IsUdd"] = false;
                context["IsFormPrescOrderForm"] = false;
                context["ArLine"] = "";
            }

            function showDropDown(sender, eventArgs) {
                sender.showDropDown();
                sender.requestItems("[showall]", false);
            }

            function openWindow(url, title) {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl(url);
                oWnd.set_title(title);
                oWnd.show();
                oWnd.maximize();

                // Cek position
                var pos = oWnd.getWindowBounds();
                if (pos.y < 0)
                    oWnd.moveTo(pos.x, 0);
            }
            function showCopyPrescription(no, isFromTemplate) {
                var guarId = '';

                if ('<%= Request.QueryString["mode"] %>' == 'direct') {
                    var cboPat = $find("<%= cboPatientID.ClientID %>");
                    if (cboPat.get_value() == '') {
                        alert('Patient required.');
                        return;
                    }
                }

                var cboGuar = $find("<%= cboGuarantorID.ClientID %>");
                if (cboGuar.get_value() == '') {
                    alert('Guarantor required.');
                    return;
                }

                guarId = cboGuar.get_value();

                var cboLoc = $find("<%=cboLocationID.ClientID %>");
                var locID = cboLoc.get_selectedItem().get_value();

                if (isFromTemplate === true) {
                    openWindow("<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/Medication/Template/PrescriptionCopyDialog.aspx?tno=" + no + "&locID=" + locID + "&guarId=" + guarId + "&regno=<%= RegistrationNo %>&sn=<%="collTransPrescriptionItem" + UniqueID() + hdnPageId.Value%>&ccm=rebind&cet=<%=grdTransPrescriptionItem.ClientID %>", "Copy Prescription from Template");
                }
                else {
                    openWindow("<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/Medication/Template/PrescriptionCopyDialog.aspx?prescno=" + no + "&locID=" + locID + "&guarId=" + guarId + "&regno=<%= RegistrationNo %>&sn=<%="collTransPrescriptionItem" + UniqueID() + hdnPageId.Value%>&ccm=rebind&cet=<%=grdTransPrescriptionItem.ClientID %>", "Copy Prescription from Transaction");
                }
            }
            function showCopyPrescription2(no, isFromTemplate) {
                var guarId = '';

                if ('<%= Request.QueryString["mode"] %>' == 'direct') {
                    var cboPat = $find("<%= cboPatientID.ClientID %>");
                    if (cboPat.get_value() == '') {
                        alert('Patient required.');
                        return;
                    }
                }

                var cboGuar = $find("<%= cboGuarantorID.ClientID %>");
                if (cboGuar.get_value() == '') {
                    alert('Guarantor required.');
                    return;
                }

                guarId = cboGuar.get_value();

                var cboLoc = $find("<%=cboLocationID.ClientID %>");
                var locID = cboLoc.get_selectedItem().get_value();

                if (isFromTemplate === true) {
                    openWindow("<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/Medication/Template/PrescriptionCopyDialog.aspx?tno=" + no + "&locID=" + locID + "&guarId=" + guarId + "&regno=<%= RegistrationNo %>&sn=<%="collTransPrescriptionItem" + UniqueID() + hdnPageId.Value%>&ccm=rebind&cet=<%=grdTransPrescriptionItem2.ClientID %>", "Copy Prescription from Template");
                }
                else {
                    openWindow("<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/Medication/Template/PrescriptionCopyDialog.aspx?prescno=" + no + "&locID=" + locID + "&guarId=" + guarId + "&regno=<%= RegistrationNo %>&sn=<%="collTransPrescriptionItem" + UniqueID() + hdnPageId.Value%>&ccm=rebind&cet=<%=grdTransPrescriptionItem2.ClientID %>", "Copy Prescription from Transaction");
                }
            }
            function openSaveAsNewTemplate(prescno) {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl("<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/Medication/Template/PrescriptionTemplateNew.aspx?prescno=" + prescno + "&sn=<%="collTransPrescriptionItem" + UniqueID() + hdnPageId.Value%>");
                oWnd.set_title("Save As New Template");
                oWnd.show();
                oWnd.setSize(500, 150);
                oWnd.center();

                // Cek position
                var pos = oWnd.getWindowBounds();
                if (pos.y < 0)
                    oWnd.moveTo(pos.x, 0);
                return false;
            }

            function openPrescriptionTemplate(mode, rt) {
                var url =
        '<%=Page.ResolveUrl("~/Module/Charges/Dispensary/PrescriptionSales/PrescriptionSalesCommon/PrescriptionTemplate.aspx")%>' + '?mode=' + mode + '&rt=' + rt;

                var oWnd = $find("<%= winPrescTemplate.ClientID %>");
                oWnd.setUrl(url);
                oWnd.center();
                oWnd.show();

            }

            function winPrescTemplate_ClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg != null) {
                    showCopyPrescription(arg.tno, true)
                }
            }

            function radWindow_ClientClose(oWnd, args) {
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

           function openWinApolKlaimDash() {
                var oWnd = $find("<%= winDialog.ClientID %>");
               oWnd.setUrl("../../../RADT/Bpjs/ApotekOnline/ApotekOnlineDashboard.aspx");
               oWnd.show();
               oWnd.setSize($(window).width() * 0.95, $(window).height() * 0.95);
               oWnd.center();
           }

           function openWinRef() {
               var oWnd = $find("<%= winDialog.ClientID %>");
               oWnd.setUrl("../../../RADT/Bpjs/ApotekOnline/ApotekOnlineReference.aspx");
               oWnd.show();
               oWnd.setSize($(window).width() * 0.95, $(window).height() * 0.95);
               oWnd.center();
           }
        </script>

    </telerik:RadScriptBlock>
    <telerik:RadWindow ID="winPrescTemplate" Width="400px" Height="450px" runat="server" VisibleStatusbar="false"
        ShowContentDuringLoad="false" Behaviors="Close,Move" Modal="True" OnClientClose="winPrescTemplate_ClientClose">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winDialog" Width="1000px" Height="600px" runat="server" Modal="True" ShowContentDuringLoad="False"
        Behaviors="Close,Move" VisibleStatusbar="False" OnClientClose="radWindow_ClientClose">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winReview" OnClientClose="winReview_ClientClose" Animation="None"
        Width="900px" Height="500px" runat="server" ShowContentDuringLoad="false" Behavior="Close"
        VisibleStatusbar="false" Modal="true" />
    <telerik:RadWindow ID="radWinPatient" Width="1200px" Height="500px" runat="server"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
        ReloadOnShow="True" OnClientClose="onClientCloseWinPatient" ShowContentDuringLoad="false">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Move,Maximize,Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadWindow runat="server" Animation="None" Width="500px" Height="400px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" Title="Document Checklist"
        ID="winDocsOption">
    </telerik:RadWindow>
    <asp:HiddenField runat="server" ID="hdnPageId" />

    <telerik:RadToolTipManager RenderMode="Lightweight" ID="toolTipMgr" runat="server" Position="BottomRight"
        Animation="Fade" Width="500px" Height="120px" Style="font-size: 18px; text-align: center; font-family: Arial;"
        RelativeTo="Mouse" AutoCloseDelay="0">
        <WebServiceSettings Method="PrevBuyToolTipData" Path="~/Module/Charges/Dispensary/PrescriptionSales/PrescriptionSalesCommon/Prescription.asmx"></WebServiceSettings>
    </telerik:RadToolTipManager>

    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Patient Information">
        <asp:HiddenField ID="hdnIsApproved" runat="server" />
        <asp:HiddenField ID="hdnIsVoid" runat="server" />
        <asp:HiddenField ID="hdnIsFromSOAP" runat="server" />

        <asp:HiddenField runat="server" ID="hdnRasproSeqNo" />
        <asp:HiddenField runat="server" ID="hdnRasproRefNo" />
        <asp:HiddenField runat="server" ID="hdnRasproType" />
        <asp:HiddenField runat="server" ID="hdnAbLevel" />
        <asp:HiddenField runat="server" ID="hdnAbRestrictionID" />
        <asp:HiddenField runat="server" ID="hdnRegistrationType" />
        <asp:HiddenField runat="server" ID="hdnIsAntibioticRestrictionApplied" />

        <asp:HiddenField runat="server" ID="hdnServiceUnitID" />
        <asp:HiddenField runat="server" ID="hdnLocationID" />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr valign="top">
                <td width="500px">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPrescriptionNo" runat="server" Text="Prescription No / Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadTextBox ID="txtPrescriptionNo" runat="server" Width="150px" MaxLength="20"
                                                ReadOnly="true" />
                                        </td>
                                        <td style="width: 20px" align="center">/
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtPrescriptionDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                                DatePopupButton-Enabled="false">
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20px" colspan="2">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvPrescriptionNo" runat="server" ErrorMessage="Prescription No required."
                                                ValidationGroup="entry" ControlToValidate="txtPrescriptionNo" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvPrescriptionDate" runat="server" ErrorMessage="Prescription Date required."
                                                ValidationGroup="entry" ControlToValidate="txtPrescriptionDate" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server" id="trExecutionDate">
                            <td class="label">
                                <asp:Label ID="lblExecutionDate" runat="server" Text="Execution Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtExecutionDate" runat="server" Width="100px">
                                </telerik:RadDatePicker>
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvExecutionDate" runat="server" ErrorMessage="Execution Date required."
                                    ValidationGroup="entry" ControlToValidate="txtExecutionDate" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblServiceUnitID" runat="server" Text="Dispensary"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AutoPostBack="True"
                                    OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Dispensary required."
                                    ValidationGroup="entry" ControlToValidate="cboServiceUnitID" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblLocationID" runat="server" Text="Location"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboLocationID" runat="server" Width="300px" AllowCustomText="true"
                                    Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboLocationID_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:RequiredFieldValidator ID="rfvLocationID" runat="server" ErrorMessage="Location required."
                                    ValidationGroup="entry" ControlToValidate="cboLocationID" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
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
                            <td width="20px"></td>
                            <td></td>
                        </tr>

                        <tr>
                            <td class="label">
                                <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                            </td>
                            <td class="entry">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" MaxLength="15"
                                                ReadOnly="true" />
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnNewPatient" Text="New Patient" OnClientClick="javascript:openWinPatient('new','','directpatient',0);return false;" />
                                        </td>
                                        <td></td>
                                        <td></td>
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
                                    <tr runat="server" id="trPatientName">
                                        <td>
                                            <telerik:RadTextBox ID="txtSalutation" runat="server" Width="28px" ReadOnly="true" />
                                        </td>
                                        <td style="width: 3px"></td>
                                        <td>
                                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="242px" ReadOnly="true" />
                                        </td>
                                        <td style="width: 3px"></td>
                                        <td>
                                            <telerik:RadTextBox ID="txtGender" runat="server" Width="24px" ReadOnly="true" />
                                        </td>
                                        <td>&nbsp; <a href="javascript:void(0);" onclick="javascript:openWinQuestionFormCheckList();"
                                            class="noti2_Container">
                                            <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo2" AssociatedControlID="txtRegistrationNo"
                                                Text=""></asp:Label>&nbsp; </a>
                                        </td>
                                    </tr>
                                </table>
                                <telerik:RadComboBox ID="cboPatientID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboPatientID_ItemDataBound"
                                    OnItemsRequested="cboPatientID_ItemsRequested" OnSelectedIndexChanged="cboPatientID_SelectedIndexChanged">
                                    <ItemTemplate>
                                        <b>
                                            <%# DataBinder.Eval(Container.DataItem, "PatientName")%>
                                        </b>&nbsp;-&nbsp;
                                        <%# System.Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth")).ToString(Temiang.Avicenna.Common.AppConstant.DisplayFormat.Date)%>
                                        <br />
                                        <%# DataBinder.Eval(Container.DataItem, "MedicalNo") %>
                                        &nbsp;|&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "PatientID") %>
                                        <br />
                                        Address :
                                        <%# DataBinder.Eval(Container.DataItem, "Address")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 30 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
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
                        <tr style="height: 24px; display: none">
                            <td class="label">
                                <asp:Label ID="lblSex" runat="server" Text="Gender"></asp:Label>
                            </td>
                            <td class="entry">
                                <asp:RadioButton ID="optSexFemale" runat="server" Text="Female" GroupName="Sex" />
                                <asp:RadioButton ID="optSexMale" runat="server" Text="Male" GroupName="Sex" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtAgeYear" runat="server" ReadOnly="true" Width="40px">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                                &nbsp;Y&nbsp;
                                <telerik:RadNumericTextBox ID="txtAgeMonth" runat="server" ReadOnly="true" Width="40px">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                                &nbsp;M&nbsp;
                                <telerik:RadNumericTextBox ID="txtAgeDay" runat="server" ReadOnly="true" Width="40px">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                                &nbsp;D
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblHeightWeight" runat="server" Text="Height / Weight"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadTextBox ID="txtHeight" runat="server" Width="40px" ReadOnly="true" />
                                        </td>
                                        <td>&nbsp;
                                            <asp:Label ID="lblHeight" runat="server" Text="Cm"></asp:Label>
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtWeight" runat="server" Width="40px" ReadOnly="true" />
                                        </td>
                                        <td>&nbsp;
                                            <asp:Label ID="lblWeight" runat="server" Text="Kg"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblAllergies" runat="server" Text="Drug Allergies "></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtAllergies" runat="server" Width="300px" ReadOnly="true"
                                    ForeColor="Red" TextMode="MultiLine" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr height="24px" style="display: none">
                            <td class="label"></td>
                            <td class="entry">
                                <asp:CheckBox ID="chkIsDirect" runat="server" Text="Direct Prescription" Enabled="false" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr runat="server" id="trPayment">
                            <td class="label"></td>
                            <td class="entry">
                                <asp:Button runat="server" ID="btnPaymentProcess" Text="Payment Received" OnClientClick="gotoAddPaymentReceivedUrl();return false;" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td width="500px">
                    <table width="100%">
                        <tr id="Tr1" runat="server">
                            <td class="label">
                                <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <telerik:RadComboBox ID="cboGuarantorID" runat="server" Width="300px" AllowCustomText="true"
                                                Filter="Contains" OnSelectedIndexChanged="cboGuarantorID_SelectedIndexChanged"
                                                AutoPostBack="True">
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
                            <td width="20">
                                <asp:RequiredFieldValidator ID="rfvGuarantorID" runat="server" ControlToValidate="cboGuarantorID"
                                    ErrorMessage=" Guarantor Required." ValidationGroup="entry" SetFocusOnError="true"
                                    Width="100%">
                                    <asp:Image ID="image7" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr runat="server" id="trGuarantorCardNo">
                            <td class="label">
                                <asp:Label runat="server" ID="lblGuarantorCardNo" Text="Guarantor Card No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtGuarantorCardNo" runat="server" Width="300px" ReadOnly="true" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr runat="server" id="trBpjsSepNo">
                            <td class="label">
                                <asp:Label runat="server" ID="lblBpjsSepNo" Text="BPJS SEP No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtBpjsSepNo" runat="server" Width="300px" ReadOnly="true" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr runat="server" id="trBpjApol" visible="false">
                            <td class="label">Tgl. SEP
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtTglSep" runat="server" Width="100px" DateInput-DateFormat="yyyy-MM-dd" DateInput-DisplayDateFormat="yyyy-MM-dd" DatePopupButton-Visible="true" DateInput-ReadOnly="true" />
                                <telerik:RadCheckBox ID="chkPRB" runat="server" Text="PRB" OnClientLoad="function(sender){ sender.get_element().style.pointerEvents = 'none'; }" />
                                <asp:Button ID="btnCariPasienSep" runat="server" Text="🔍" OnClick="btnCariPasienSep_Click" />
                                <asp:Button ID="btnOpenDialog" runat="server" Text="📊" OnClientClick="openWinApolKlaimDash(); return false;" />
                                <asp:Button ID="btnOpenRef" runat="server" Text="🛠" OnClientClick="openWinRef(); return false;" />
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                        <tr runat="server" id="trPRBApol" visible="false">
                            <td class="label">
                                <asp:Label ID="lblPRB" runat="server" Text="PRB Name "></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtNmPRB" runat="server" Width="300px" ReadOnly="true" ForeColor="Red" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr runat="server" id="trInsuranceID">
                            <td class="label">
                                <asp:Label runat="server" ID="lblInsuranceID" Text="Insurance ID"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtInsuranceID" runat="server" Width="300px" MaxLength="30" ReadOnly="true" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvParamedicID" runat="server" ControlToValidate="cboParamedicID"
                                    ErrorMessage=" Physician Required." ValidationGroup="entry" SetFocusOnError="true"
                                    Width="100%">
                                    <asp:Image ID="image2" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblClusterID" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100px" MaxLength="20"
                                    ReadOnly="true" />
                                &nbsp;
                                <asp:Label ID="lblServiceUnitName" runat="server"></asp:Label>
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr id="pnlRoom" runat="server">
                            <td class="label">
                                <asp:Label ID="lblRoomID" runat="server" Text="Room"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtRoomID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                                &nbsp;
                                <asp:Label ID="lblRoomName" runat="server"></asp:Label>
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr id="pnlBedID" runat="server">
                            <td class="label">
                                <asp:Label ID="lblBedID" runat="server" Text="Bed No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtBedID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr id="pnlClassID" runat="server">
                            <td class="label">
                                <asp:Label ID="lblClassID" runat="server" Text="Charge Class"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtClassID" runat="server" Width="100px" ReadOnly="true" />
                                &nbsp;
                                <asp:Label ID="lblClassName" runat="server"></asp:Label>
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblAdditionalNote" runat="server" Text="Notes"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtAdditionalNote" runat="server" Width="300px" MaxLength="200"
                                    TextMode="MultiLine" ForeColor="Red" Font-Size="Large" Height="50px" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr height="24px" runat="server" id="trUnitDose" style="display: none">
                            <td class="label"></td>
                            <td class="entry">
                                <asp:CheckBox ID="chkUnitDose" runat="server" Text="Unit Dose" AutoPostBack="true"
                                    OnCheckedChanged="chkUnitDose_CheckedChanged" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <asp:Panel ID="pnlAdditionalOpr" runat="server">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label4" runat="server" Text="Address"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtFullAddress" runat="server" Width="300px" MaxLength="200" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label1" runat="server" Text="Phone No / Mobile Phone No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadTextBox ID="txtNoTelp" runat="server" Width="147px" MaxLength="50" />
                                            </td>
                                            <td style="width: 5px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtMobilePhoneNo" runat="server" Width="148px" MaxLength="50" ReadOnly="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr style="display: none">
                                <td class="label">
                                    <asp:Label ID="lblQtyR" runat="server" Text="Number Of R/"></asp:Label>
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtQtyR" runat="server" ReadOnly="true" Width="40px"
                                                    Font-Bold="True">
                                                    <NumberFormat DecimalDigits="0" />
                                                </telerik:RadNumericTextBox>

                                            </td>
                                            <td style="width: 50px"></td>
                                            <td>
                                                <asp:Label ID="lblFloor" runat="server" Text="Floor" Visible="false"></asp:Label>
                                            </td>
                                            <td style="width: 30px"></td>
                                            <td class="entry">
                                                <telerik:RadComboBox ID="cboSRFloor" runat="server" Width="150px" Visible="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </asp:Panel>
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
                        <asp:Panel ID="pnlQueType" runat="server">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label2" runat="server" Text="Queue Type"></asp:Label>
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadComboBox ID="cboQueueType" runat="server" Width="150px" />
                                            </td>
                                            <td style="width: 40px"></td>
                                            <td class="entry">
                                                <telerik:RadTextBox ID="txtQueueNo" runat="server" Width="138px" MaxLength="20"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </asp:Panel>
                        <tr id="trIs23Days" runat="server">
                            <td class="label"></td>
                            <td>
                                <asp:CheckBox ID="chkIs23Days" runat="server" Text="23 Days Prescription" AutoPostBack="true"
                                    OnCheckedChanged="chkIs23Days_CheckedChanged" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr id="trGoTo23DaysPrescription" runat="server">
                            <td class="label"></td>
                            <td>
                                <asp:Button runat="server" ID="btnGoTo23DaysPrescription" Text="Go to 23 Days Prescription" OnClientClick="gotoView23PrescUrl();return false;" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <asp:Panel ID="pnlSplitBill" runat="server">
                            <tr>
                                <td class="label"></td>
                                <td>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkIsSplitBill" runat="server" Text="Split Bill" AutoPostBack="true"
                                                    OnCheckedChanged="chkIsSplitBill_CheckedChanged" />
                                            </td>
                                            <td>&nbsp;&nbsp;</td>
                                            <td>
                                                <asp:CheckBox ID="chkIsCash" runat="server" Text="Cash" AutoPostBack="true"
                                                    OnCheckedChanged="chkIsCash_CheckedChanged" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblFromRegistrationNo" runat="server" Text="From Registration No"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboFromRegistrationNo" runat="server" Width="300px" EnableLoadOnDemand="true"
                                        MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboFromRegistrationNo_ItemsRequested"
                                        OnItemDataBound="cboFromRegistrationNo_ItemDataBound" OnSelectedIndexChanged="cboFromRegistrationNo_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <ItemTemplate>
                                            <b>
                                                <%# DataBinder.Eval(Container.DataItem, "RegistrationNo")%>
                                            </b>
                                            <br />
                                            <%# DataBinder.Eval(Container.DataItem, "PatientName")%>
                                            <br />
                                            <%# DataBinder.Eval(Container.DataItem, "MedicalNo") %>
                                                    &nbsp;-&nbsp;
                                                    <%# DataBinder.Eval(Container.DataItem, "PatientID") %>
                                            <br />
                                            <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                                            <br />
                                            <%# DataBinder.Eval(Container.DataItem, "RoomName")%>
                                                    &nbsp;-&nbsp;
                                                    <%# DataBinder.Eval(Container.DataItem, "BedID")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 5 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvFromRegistrationNo" runat="server" ErrorMessage="From Registration No required."
                                        ValidationGroup="entry" ControlToValidate="cboFromRegistrationNo" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                        </asp:Panel>

                    </table>

                </td>
                <td style="vertical-align: top">
                    <table>
                        <tr>
                            <td style="vertical-align: top;">
                                <fieldset id="FieldSet1" style="width: 150px; min-height: 150px;">
                                    <legend>Photo</legend>
                                    <asp:Image runat="server" ID="imgPatientPhoto" Width="150px" Height="150px" />
                                </fieldset>
                            </td>
                            <td style="vertical-align: top;">
                                <div runat="server" id="trPrescriptionReview">
                                    <table>
                                        <tr>
                                            <td style="vertical-align: top;">
                                                <fieldset>
                                                    <legend>
                                                        <asp:Button runat="server" ID="btnPrescReview" Text="Review" OnClientClick="openWinReview();return false;" /></legend>
                                                    <asp:Label runat="server" ID="lblReviewByUserName"></asp:Label>
                                                </fieldset>
                                                <fieldset>
                                                    <legend>
                                                        <asp:Button runat="server" ID="btnDrugReview" Text="Drug Review" OnClientClick="openWinReview2();return false;" /></legend>
                                                    <asp:Label runat="server" ID="lblDrugReviewByUserName"></asp:Label>
                                                </fieldset>
                                                <fieldset>
                                                    <legend>
                                                        <asp:Button runat="server" ID="btnFollowUp" Text="Follow-up" OnClientClick="openWinReview3();return false;" /></legend>
                                                    <asp:Label runat="server" ID="lblFollowUpByUserName"></asp:Label>
                                                </fieldset>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <fieldset>
                                                    <legend>
                                                        <asp:Button runat="server" ID="btnPrescProgress" Text="Progress" OnClientClick="openWinProgress();return false;" /></legend>
                                                    <asp:Label runat="server" ID="lblProgress"></asp:Label>
                                                </fieldset>
                                                <fieldset>
                                                    <legend>
                                                        <asp:Button runat="server" ID="btnPrescHighAlert" Text="High Alert" OnClientClick="openWinHighAlert();return false;" /></legend>
                                                    <asp:Label runat="server" ID="lblHighAlert"></asp:Label>
                                                </fieldset>
                                                <fieldset>
                                                    <legend>
                                                        <asp:Button runat="server" ID="btnPrescEducation" Text="Education" OnClientClick="openWinEducation();return false;" /></legend>
                                                    <asp:Label runat="server" ID="lblEducationByUserName"></asp:Label>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>


                                </div>
                            </td>
                        </tr>
                    </table>
                    <fieldset runat="server" id="pnlPregnantStat" visible="false">
                        <legend><b>Pregnant & Breastfeeding Status</b></legend>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td style="vertical-align: top;">
                                    <table width="100%">
                                        <tr>
                                            <td class="label">Pregnant</td>
                                            <td style="width: 120px">
                                                <asp:CheckBox runat="server" ID="chkIsPregnant" Text="Pregnant" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">Breastfeeding</td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chkIsBreastfeeding" Text="Breastfeeding" /><br />
                                                <asp:CheckBox runat="server" ID="chkIsBreastfeedingL6M" Text="Child <6 Month" /><br />
                                                <asp:CheckBox runat="server" ID="chkIsBreastfeedingM6M" Text="Child >6 Month" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 50%; vertical-align: top;">
                                    <table width="100%">
                                        <tr>
                                            <td class="label">FDOLM</td>
                                            <td>
                                                <telerik:RadDatePicker ID="txtFdolm" runat="server" Width="100px" DatePopupButton-Visible="false" DateInput-ReadOnly="true">
                                                </telerik:RadDatePicker>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">Est Birth</td>
                                            <td>
                                                <telerik:RadDatePicker ID="txtEstBirthDate" runat="server" Width="100px" DatePopupButton-Visible="false" DateInput-ReadOnly="true">
                                                </telerik:RadDatePicker>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">Pregnant</td>
                                            <td>
                                                <telerik:RadTextBox runat="server" ID="txtPregnantAge" Width="100px" Enabled="false"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <asp:Panel runat="server" ID="pnlApol" Visible="false">
                        <telerik:RadTabStrip ID="RadTabStripAPOL" runat="server" MultiPageID="RadMultiPageAPOL">
                            <Tabs>
                                <telerik:RadTab Text="Entry APOL" PageViewID="pgEntryApol" Selected="true" />
                                <telerik:RadTab Text="Riwayat" PageViewID="pgRiwayatApol" />
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="RadMultiPageAPOL" runat="server" SelectedIndex="0">
                            <telerik:RadPageView ID="pgEntryApol" runat="server">
                                        <table>
                                            <tr>
                                                <td class="label">SEP / From SJP</td>
                                                <td>
                                                    <asp:TextBox ID="txtRefAsalSJP" runat="server" Width="140" />
                                                    <telerik:RadDatePicker ID="txtTglRsp" runat="server" Width="100px" DateInput-DateFormat="yyyy-MM-dd" DateInput-DisplayDateFormat="yyyy-MM-dd" DatePopupButton-Visible="true" DateInput-ReadOnly="true" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">Service Unit Apol</td>
                                                <td><asp:TextBox ID="txtPoliRSP" runat="server" Width="220px" /></td>
                                            </tr>
<%--                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblpoliapol" runat="server" Text="Poli Apol"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadComboBox ID="cboPoliapol" runat="server" Width="220px" AllowCustomText="true"
                                                        Filter="Contains">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td class="label">Drug Type</td>
                                                <td>
                                                    <telerik:RadComboBox runat="server" ID="cboJnsRsp" Width="230px">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Value="1" Text="Obat PRB" />
                                                            <telerik:RadComboBoxItem Value="2" Text="Obat Kronis Blm Stabil" />
                                                            <telerik:RadComboBoxItem Value="3" Text="Obat Kemoterapi" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">Apol Presc. No</td>
                                                <td><asp:TextBox ID="txtNoResep" runat="server" Width="220px" MaxLength="5" /></td>
                                            </tr>
                                            <tr>
                                                <td class="label">Physician</td>
                                                <td><asp:TextBox ID="txtKdDokter" runat="server" Width="220px" /></td>
                                            </tr>
<%--                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblDktr" runat="server" Text="Dokter"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadComboBox ID="cboDktrApol" runat="server" Width="220px" AllowCustomText="true"
                                                        Filter="Contains">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td class="label">Iter</td>
                                                <td>
                                                    <telerik:RadComboBox runat="server" ID="cboIterasi" Width="230px">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Value="0" Text="Non Iterasi" Selected="true" />
                                                            <telerik:RadComboBoxItem Value="1" Text="Iterasi" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnCreateAPOL" runat="server" Text="CREATE APOL HOUSE PRESC." OnClick="btnCreateHouseApol_Click" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCreateApolResult" runat="server" ForeColor="Green" />
                                                </td>
                                            </tr>
                                            <tr>
<%--                                                <td>
                                                    <asp:Button ID="btnSaveApolDtl" runat="server" Text="SAVE APOL DETAIL PRESC." OnClick="btnSaveApolDtl_Click" />
                                                </td>--%>
                                                <td>
                                                    <asp:Label ID="lblApolDtl" runat="server" ForeColor="Green" />
                                                </td>
                                            </tr>
                                        </table>
<%--                                        <table>
                                            <tr>
                                                <td><b>Keterangan:</b></td>
                                            </tr>
                                            <tr>
                                                <td style="color: blue;">● Tgl Entry: Tanggal Resep dientri/direkam ke aplikasi <br />
                                                    ● Tgl Resep: Tanggal Tertera pada Lembar Resep <br />
                                                    ● Tgl Pelayanan: Tanggal saat resep dilayani/diterima Apotek/Instasi Farmasi <br />
                                                    ● Tgl SEP - 15 Hari ≤ Tgl SEP ≤ Tgl Resep ≤ Tgl Entry ≤ Tgl Sistem
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td style="color: red">
                                                    Pastikan Obat Dalam Sediaan Tabung, Ampul, Vial, Flexpen, Penfill Sudah DiEntrikan (Signa Dan Jumlah Obat) Dalam Bilangan Bulat (Bukan Desimal)
                                                </td>
                                            </tr>
                                        </table>--%>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="pgRiwayatApol" runat="server">
                                <table>
                                    <tr>
                                        <td class="label">Periode</td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadDatePicker ID="txtPeriode1" runat="server" Width="100px" DateInput-DateFormat="yyyy-MM-dd" DateInput-DisplayDateFormat="yyyy-MM-dd" />
                                                    </td>
                                                    <td>&nbsp;-&nbsp;</td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="txtPeriode2" runat="server" Width="100px" DateInput-DateFormat="yyyy-MM-dd" DateInput-DisplayDateFormat="yyyy-MM-dd" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">Guarantor Card No</td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtNoKaHist" runat="server" Width="200px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label"></td>
                                        <td class="entry">
                                            <asp:Button ID="btnCariHistory" runat="server" Text="Find" OnClick="btnCariHistory_Click" />
                                        </td>
                                        <td />
                                    </tr>
                                </table>
                                <asp:Label ID="lblInfo" runat="server" ForeColor="Red" Font-Bold="true" /><br />
                                <telerik:RadGrid ID="grdListHist" runat="server" AutoGenerateColumns="False" ShowGroupPanel="false"
                                    AllowPaging="True" PageSize="5" AllowSorting="False" GridLines="None">
                                    <MasterTableView AutoGenerateColumns="False">
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderText="No SJP" DataField="Nosjp" UniqueName="Nosjp"
                                                HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn HeaderText="Tgl Pelayanan" DataField="Tglpelayanan" UniqueName="Tglpelayanan"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn HeaderText="No Resep" DataField="Noresep" UniqueName="Noresep"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn HeaderText="Kode Obat" DataField="Kodeobat" UniqueName="Kodeobat"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn HeaderText="Nama Obat" DataField="Namaobat" UniqueName="Namaobat"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn HeaderText="Jumlah Obat" DataField="Jmlobat" UniqueName="Jmlobat"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <table width="100%" runat="server" id="pnlOrderNote">
        <tr>
            <td style="width: 300px">
                <fieldset>
                    <legend><b>Order Note</b></legend>
                    <telerik:RadTextBox runat="server" ID="txtPrescriptionText" Width="99.7%" Height="172px"
                        ReadOnly="true" TextMode="MultiLine" />
                </fieldset>
            </td>
            <td style="width: 400px">
                <fieldset>
                    <legend><b>Vital Sign</b></legend>
                    <div style="overflow: auto; width: 100%; height: 172px;">
                        <cc:VitalSignInfoCtl runat="server" ID="vitalSignInfoCtl" IsShowHeader="false" />
                    </div>
                </fieldset>
            </td>
            <td valign="top">
                <uc1:SoapInfoCtl runat="server" ID="soapInfoCtl" />
            </td>
        </tr>
    </table>


    <telerik:RadTabStrip ID="tabs" runat="server" MultiPageID="mpg1" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Detail" PageViewID="pgDetail" Selected="True" />
            <telerik:RadTab runat="server" Text="Laboratory Result" PageViewID="pgRiwayat" />
            <telerik:RadTab runat="server" Text="Detail" PageViewID="pgDetail2" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpg1" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgDetail" runat="server" Selected="true">
            <telerik:RadGrid ID="grdTransPrescriptionItem" runat="server" OnNeedDataSource="grdTransPrescriptionItem_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdTransPrescriptionItem_UpdateCommand"
                OnItemDataBound="grdTransPrescriptionItem_ItemDataBound" OnDeleteCommand="grdTransPrescriptionItem_DeleteCommand"
                OnInsertCommand="grdTransPrescriptionItem_InsertCommand" OnItemCreated="grdTransPrescriptionItem_ItemCreated" ShowFooter="true">
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="SequenceNo">
                    <CommandItemTemplate>
                        <table width="100%">
                            <tr>
                                <td style="width: 150px">
                                    <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# this.DataModeCurrent!=AppEnum.DataMode.Read && !grdTransPrescriptionItem.MasterTableView.IsItemInserted %>'>
                                        <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/insert16.png" />
                                        &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new record"></asp:Label>
                                    </asp:LinkButton></td>
                                <td style="width: 200px">
                                    <%# this.DataModeCurrent==AppEnum.DataMode.New && (string.IsNullOrEmpty(Request.QueryString["mode"]) || (Request.QueryString["mode"]=="direct" && AppSession.Parameter.IsVisibleTemplateForDirectPrescription)) ? string.Format("<a href='javascript:void(0);' onclick=\"javascript:openPrescriptionTemplate('copy','{0}')\"><img src=\"{1}/Images/Toolbar/copy16.png\" alt='Copy' />&nbsp;Add from Template</a>&nbsp;&nbsp;", Request.QueryString["rt"],Helper.UrlRoot()):string.Empty %>
                                </td>
                                <td align="right"><%#this.IsPowerUser? string.Format("<a href='javascript:void(0);' onclick=\"javascript:openSaveAsNewTemplate('',false)\"><img src=\"{0}/Images/Toolbar/insert16.png\" alt='Copy' />&nbsp;Save As New Template</a>&nbsp;&nbsp;",Helper.UrlRoot()):string.Empty %></td>
                            </tr>
                        </table>
                    </CommandItemTemplate>
                    <CommandItemStyle Height="29px" />
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="SequenceNo" HeaderText="No." UniqueName="SequenceNo"
                            HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsCompound" HeaderText="C"
                            UniqueName="IsCompound" SortExpression="IsCompound" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ParentNo" HeaderText="Hdr" UniqueName="ParentNo"
                            HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn UniqueName="TemplateItemName2" HeaderStyle-Width="35px"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblR" runat="server" Text='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsRFlag")) ? @"R/" : "" %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="TemplateItemName">
                            <ItemTemplate>
                                <asp:Label ID="lblItemName" runat="server" Text='<%# GetItemName(DataBinder.Eval(Container.DataItem, "IsRFlag"), DataBinder.Eval(Container.DataItem, "ItemName")) %>' />
                                <a href="javascript:void(0);" onclick="javascript:openWinBalanceInfo('<%# DataBinder.Eval(Container.DataItem, "ItemID")%>')">
                                    <img src="../../../../Images/infoblue16.png" border="0" alt="Show Location" title="Show Balance All Location" /></a>
                                <%--<a href="javascript:void(0);" onclick="javascript:openPrevBuyDialog('<%# DataBinder.Eval(Container.DataItem, "ItemID")%>')">
                                    <img src="../../../../Images/infoblue16.png" border="0" alt="Show Prev Buy" title="Show Prev Buy" /></a>--%>
                                <br />
                                <i>Intervention :</i><asp:Label ID="lblIntervention" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemInterventionName") %>' /><br />
                                <i>Notes :</i><asp:Label ID="lblNotes" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Notes") %>' />
                                &nbsp;<i><asp:Label ID="lblCasemixNotes" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CombinedNotes") %>' ForeColor="Blue" /></i>
                                <br />
                                <i>Order :</i><asp:Label ID="lblOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderText") %>' /><br />
                                <i>Iter :</i><asp:Label ID="lblIter" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IterText") %>' /><br />
                                <i><span style="color: orangered"><%# DataBinder.Eval(Container.DataItem, "FornasRestrictionNotes")%></span></i>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Qty R" UniqueName="QtyR" HeaderStyle-Width="60px"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ItemQtyInString") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Qty T" UniqueName="QtyTaken" HeaderStyle-Width="70px"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                (<%# String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ResultQty")) %>)
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Unit" UniqueName="TemplateItemName4" HeaderStyle-Width="100px">
                            <ItemTemplate>
                                <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCompound")) ? DataBinder.Eval(Container.DataItem, "EmbalaceLabel") : DataBinder.Eval(Container.DataItem, "SRItemUnit")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="DosageQty" HeaderText="Formula"
                            UniqueName="DosageQty" SortExpression="DosageQty" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRDosageUnit" HeaderText="Unit"
                            UniqueName="SRDosageUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="SRConsumeMethodName" HeaderText="Consume Method" HeaderStyle-Width="150px"
                            UniqueName="SRConsumeMethodName" SortExpression="SRConsumeMethodName" />
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="ConsumeQty" HeaderText="Dosing"
                            UniqueName="ConsumeQty" SortExpression="ConsumeQty" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRConsumeUnit" HeaderText="Unit"
                            UniqueName="SRConsumeUnit" SortExpression="SRConsumeUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <%--<telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRConsumeUnitName" HeaderText="Unit"
                            UniqueName="SRConsumeUnitName" SortExpression="SRConsumeUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />--%>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                            UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RecipeAmount" HeaderText="Recipe Amount"
                            UniqueName="RecipeAmount" SortExpression="RecipeAmount" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmbalaceAmount" HeaderText="Embalace"
                            UniqueName="EmbalaceAmount" SortExpression="EmbalaceAmount" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="DiscountAmount" HeaderText="Discount"
                            UniqueName="DiscountAmount" SortExpression="DiscountAmount" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />

                        <%-- <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Total" UniqueName="Total"
                    DataType="System.Double" DataFields="ResultQty,Price,DiscountAmount,RecipeAmount"
                    SortExpression="Total" Expression="(({0} * {1}) - {2}) + {3}" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />--%>

                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="LineAmount" HeaderText="Total"
                            UniqueName="LineAmount" SortExpression="LineAmount" HeaderStyle-HorizontalAlign="Right"
                            FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"
                            Aggregate="Sum" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsPendingDelivery"
                            HeaderText="Pending" UniqueName="IsPendingDelivery" SortExpression="IsPendingDelivery"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />

                        <telerik:GridTemplateColumn HeaderText="Etq" UniqueName="Etiquette" HeaderStyle-Width="35px"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsRFlag")) ? (string.Format("<a href=\"#\" onclick=\"OpenEtiquette('{0}','{1}')\"><img src=\"../../../../Images/label16.png\" border=\"0\" alt=\"Etiquette\" title=\"Custom Etiquette\" /></a>", DataBinder.Eval(Container.DataItem, "PrescriptionNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"))) : ""%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="Text" HeaderStyle-Width="35px" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openTextEditorDialog('{0}','{1}')\"><img src='../../../../Images/Toolbar/details16.png' border=0 title=\"Text Editor\"/></a>", DataBinder.Eval(Container.DataItem, "PrescriptionNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="PrescriptionSalesItemDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="TransPrescriptionItemEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50%"></td>
                    <td width="50%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPatientAmount" runat="server" Text="Patient Amount"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtPatientAmount" runat="server" Width="100px" MaxLength="16"
                                        ReadOnly="true" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblGuarantorAmount" runat="server" Text="Guarantor Amount"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtGuarantorAmount" runat="server" Width="100px" MaxLength="16"
                                        ReadOnly="true" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgRiwayat" runat="server">
            <telerik:RadGrid ID="grdLabHist" runat="server" AutoGenerateColumns="False" GridLines="None"
                Height="400px" OnItemCommand="grdLabHist_ItemCommand">
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="OrderLabNo" GroupLoadMode="Client">
                    <CommandItemTemplate>
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbInsert" runat="server" CommandName="Reload">
                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/refresh16.png" />
                            &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Reload"></asp:Label>
                        </asp:LinkButton>
                    </CommandItemTemplate>
                    <CommandItemStyle Height="35px" />
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="OrderLabTglOrder" HeaderText="Order Date" />
                                <telerik:GridGroupByField FieldName="OrderLabNo" HeaderText="Order No" />
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="OrderLabTglOrder" SortOrder="Descending" />
                                <telerik:GridGroupByField FieldName="OrderLabNo" SortOrder="Descending" />
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TemplateItemName2" HeaderText="Laboratory Exam Summary">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "LabOrderSummary")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="Result" HeaderStyle-Width="150px">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "Result")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="StandarValue" UniqueName="StandarValue" HeaderText="Standard Value"
                            HeaderStyle-Width="150px" />
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="True" />
                    <Scrolling AllowScroll="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgDetail2" runat="server">
            <telerik:RadGrid ID="grdTransPrescriptionItem2" runat="server" OnNeedDataSource="grdTransPrescriptionItem_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdTransPrescriptionItem_UpdateCommand"
                OnItemDataBound="grdTransPrescriptionItem_ItemDataBound" OnDeleteCommand="grdTransPrescriptionItem_DeleteCommand"
                OnInsertCommand="grdTransPrescriptionItem_InsertCommand" ShowFooter="true">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="SequenceNo">
                    <CommandItemTemplate>
                        <table width="100%">
                            <tr>
                                <td align="left">&nbsp;&nbsp;&nbsp;Barcode Scan&nbsp;&nbsp;
                        <telerik:RadTextBox ID="txtBarcodeEntry" runat="server" Width="300px" AutoPostBack="True"
                            OnTextChanged="txtBarcodeEntry_OnTextChanged">
                            <ClientEvents OnKeyPress="txtBarcodeEntryKeyPress"></ClientEvents>
                        </telerik:RadTextBox>
                                </td>
                                <td>&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# !grdTransPrescriptionItem2.MasterTableView.IsItemInserted %>'>
                                        <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/insert16.png" />
                                        &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new record"></asp:Label>
                                    </asp:LinkButton></td>

                                <td align="right"><%# string.Format("<a href='javascript:void(0);' onclick=\"javascript:openSaveAsNewTemplate('',false)\"><img src=\"{0}/Images/Toolbar/copy16.png\" alt='Copy' />&nbsp;Save As New Template</a>&nbsp;&nbsp;",Helper.UrlRoot()) %></td>
                            </tr>
                        </table>
                    </CommandItemTemplate>
                    <CommandItemStyle Height="29px" />
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="SequenceNo" HeaderText="No." UniqueName="SequenceNo"
                            HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsCompound" HeaderText="C"
                            UniqueName="IsCompound" SortExpression="IsCompound" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ParentNo" HeaderText="Hdr" UniqueName="ParentNo"
                            HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn UniqueName="TemplateItemName2" HeaderStyle-Width="35px"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblR" runat="server" Text='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsRFlag")) ? @"R/" : "" %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="TemplateItemName">
                            <ItemTemplate>
                                <asp:Label ID="lblItemName" runat="server" Text='<%# GetItemName(DataBinder.Eval(Container.DataItem, "IsRFlag"), DataBinder.Eval(Container.DataItem, "ItemName")) %>' /><br />
                                <i>Intervention :</i><asp:Label ID="lblIntervention" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemInterventionName") %>' /><br />
                                <i>Notes :</i><asp:Label ID="lblNotes" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Notes") %>' /><br />
                                <i>Order :</i><asp:Label ID="lblOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderText") %>' /><br />
                                <i>Iter :</i><asp:Label ID="lblIter" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IterText") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Numero" UniqueName="TemplateItemName3" HeaderStyle-Width="60px"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ItemQtyInString") %>
                                (<%# String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ResultQty")) %>)
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Unit" UniqueName="TemplateItemName4" HeaderStyle-Width="100px">
                            <ItemTemplate>
                                <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCompound")) ? DataBinder.Eval(Container.DataItem, "EmbalaceLabel") : DataBinder.Eval(Container.DataItem, "SRItemUnit")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="DosageQty" HeaderText="Formula"
                            UniqueName="DosageQty" SortExpression="DosageQty" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRDosageUnit" HeaderText="Unit"
                            UniqueName="SRDosageUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="SRConsumeMethodName" HeaderText="Consume Method"
                            UniqueName="SRConsumeMethodName" SortExpression="SRConsumeMethodName" />
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="ConsumeQty" HeaderText="Dosing"
                            UniqueName="ConsumeQty" SortExpression="ConsumeQty" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <%-- <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRConsumeUnit" HeaderText="Unit"
                            UniqueName="SRConsumeUnit" SortExpression="SRConsumeUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />--%>
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRConsumeUnitName" HeaderText="Unit"
                            UniqueName="SRConsumeUnitName" SortExpression="SRConsumeUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                            UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="DiscountAmount" HeaderText="Discount"
                            UniqueName="DiscountAmount" SortExpression="DiscountAmount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <%-- <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Total" UniqueName="Total"
                    DataType="System.Double" DataFields="ResultQty,Price,DiscountAmount,RecipeAmount"
                    SortExpression="Total" Expression="(({0} * {1}) - {2}) + {3}" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />--%>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="LineAmount" HeaderText="Total"
                            UniqueName="LineAmount" SortExpression="LineAmount" HeaderStyle-HorizontalAlign="Center"
                            FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"
                            Aggregate="Sum" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsPendingDelivery"
                            HeaderText="Pending" UniqueName="IsPendingDelivery" SortExpression="IsPendingDelivery"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn HeaderText="Etq" UniqueName="Etiquette" HeaderStyle-Width="35px"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsRFlag")) ? (string.Format("<a href=\"#\" onclick=\"OpenEtiquette('{0}','{1}')\"><img src=\"../../../../Images/label16.png\" border=\"0\" alt=\"Etiquette\" title=\"Custom Etiquette\" /></a>", DataBinder.Eval(Container.DataItem, "PrescriptionNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"))) : ""%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="Text" HeaderStyle-Width="35px" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openTextEditorDialog('{0}','{1}')\"><img src='../../../../Images/Toolbar/details16.png' border=0 /></a>", DataBinder.Eval(Container.DataItem, "PrescriptionNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="PrescriptionSalesItemDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="TransPrescriptionItemEditCommand">
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
