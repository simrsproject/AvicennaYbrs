<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" EnableEventValidation="true"
    CodeBehind="RegistrationDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.RegistrationDetail" %>

<%@ Register Src="~/CustomControl/AddressCtl.ascx" TagName="AddressCtl" TagPrefix="uc1" %>
<%@ Register TagPrefix="uc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>
<%@ Register Src="~/PCareCommon/PCareMemberInfoStatus.ascx" TagPrefix="uc1" TagName="PCareMemberInfoStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="Registration" />
    <asp:CustomValidator ID="customValidator" ValidationGroup="Registration" runat="server"></asp:CustomValidator>
    <telerik:RadWindow ID="winRegistration" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" OnClientClose="onClientClose" />
        <telerik:RadWindow ID="winPCare" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" OnClientClose="winPCare_OnClientClose" />
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function openWinBpjsInfo(sender, eventArgs) {
                var medNo = $find("<%= txtMedicalNo.ClientID %>");

                if (medNo.get_value() == '') return;

                var oWnd = $find("<%= winRegistration.ClientID %>");
                oWnd.setUrl('BPJSRegistrationInfo.aspx?type=<%= Request.QueryString["rt"] %>&medNo=' + medNo.get_value());
                oWnd.show();
            }

            function openWinRegistration() {
                var oWnd = $find("<%= winRegistration.ClientID %>");
                oWnd.setUrl('MotherRegistrationList.aspx');
                oWnd.show();
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument.regno) {
                    var txtRefNo = $find("<%= txtMotherRegistrationNo.ClientID %>");
                    txtRefNo.set_value(oWnd.argument.regno);
                }
                else if (oWnd.argument.bpjs) {
                    var txtBpjsNo = $find("<%= txtInsuranceID.ClientID %>");
                    txtBpjsNo.set_value(oWnd.argument.bpjs);
                }
                else if (oWnd.argument.sep) {
                    var txtBpjsSepNo = $find("<%= txtBpjsSepNo.ClientID %>");
                    txtBpjsSepNo.set_value(oWnd.argument.sep);
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
                var oWnd = $find("<%= winRegistration.ClientID %>");
                var oguar = $find("<%= cboGuarantorID.ClientID %>");
                var lblToBeUpdate = "<%= lblGuarantorInfo.ClientID %>";
                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/Finance/Master/GuarantorInfo/GuarantorInfoDialog.aspx?id=' + oguar.get_value() + '&lblGuarantorInfo=' + lblToBeUpdate + '")%>');
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function openWinBedBookingInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var obed = $find("<%= cboBedID.ClientID %>");
                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/Registration/BedBookingInfoDialog.aspx?id=' + obed.get_value() + '")%>');
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function openWinBedNotesInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var obed = $find("<%= cboBedID.ClientID %>");
                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/Registration/BedNotesInfoDialog.aspx?id=' + obed.get_value() + '")%>');
                //oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function openWinPatientNotes() {
                var oWnd = $find("<%= winRegistration.ClientID %>");
                var opatId = $find("<%= txtPatientID.ClientID %>");
                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/Registration/PatientNotesDialog.aspx?patId=' + opatId.get_value() + '")%>');
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function onWinCaptureImageClose(sender, eventArgs) {
                var arg = eventArgs.get_argument();
                if (arg) {
                    var img = document.getElementById("<%=imgPatientPhoto.ClientID%>");
                    img.setAttribute('src', arg);
                }
            }

            function openWinCaptureImage() {
                var oWnd;
                oWnd = radopen("PatientPhoto/CaptureImageForm.aspx", "winCaptureImage");
                oWnd.setSize(720, 380);
                oWnd.center();
            }

            function openRegPrint() {
                var oRegNo = $find("<%= txtRegistrationNo.ClientID %>");
                radopen("RegistrationPrint.aspx?regno=" + oRegNo.get_value() + "&rt=", "winRegPrint");
            }

            function onClientCloseRegPrint(oWnd, args) {
                if (oWnd.argument && oWnd.argument.print != null) {
                    radopen('<%=Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx")%>', "winPrintPreview");
                }
                oWnd = null;
            }

        </script>

        <script type="text/javascript" language="javascript">
            function onWinWebCamClose(sender, eventArgs) {
                // Get retval
                var arg = eventArgs.get_argument();
                if (arg) {
                    var img = document.getElementById("<%=imgPatientPhoto.ClientID%>");
                    img.setAttribute('src', arg);
                    var hdnImgData = document.getElementById("<%=hdnImgData.ClientID%>");
                    hdnImgData.value = arg;
                }
            }


            function openWinWebCam() {
                var oWnd = $find("<%= winWebCam.ClientID %>");
                oWnd.setUrl("PatientPhoto2/WebCam.aspx");
                oWnd.setSize(240 + 50, 320 + 50);
                oWnd.center();
                oWnd.show();
            }
            function CheckBpjsNo() {
                document.getElementById('<%= ButtonOk.ClientID %>').disabled = true;
                document.getElementById('<%= ButtonCancel.ClientID %>').disabled = true;

                var chkIsBpjsKapitasi = document.getElementById("<%= chkIsBpjsKapitasi.ClientID %>");

                if (chkIsBpjsKapitasi.checked === false) {
                    SubmitSave();
                    return;
                }

                var bpjsNo = ($find("<%= txtGuarIDCardNo.ClientID %>")).get_textBoxValue();
                var guarantorId = ($find("<%= cboGuarantorID.ClientID %>")).get_value();
                try {
                    $.ajax({
                        type: "POST",
                        url: "../../../WebService/PCareService.asmx/CheckBpjsNo",
                        data: "{'bpjsNo':'" + bpjsNo + "','guarantorId':'" + guarantorId + "'}", // if ur method take parameters
                        contentType: "application/json; charset=utf-8",
                        success: function (response) {
                            var result = response.d;

                            if (result === 'OK') {
                                SubmitSave();
                            } else {
                                var results = result.split('|');
                                if (results[0] === 'alert')
                                    alert(results[1]);
                                else if (confirm(results[1] + ' Continue Registration ?'))
                                    SubmitSave();
                            }
                        },
                        dataType: "json",
                        failure: function (response) {
                            var result = response.d;
                            var results = result.split('|');
                            if (results[0] === 'alert')
                                alert(results[1]);
                            else if (confirm(results[1] + ' Continue Registration ?'))
                                SubmitSave();
                        }
                    });
                }
                catch (e) {
                    if (confirm('Checking status BPJS error. ' + e + '. Continue Registration ?'))
                        SubmitSave();
                }

                document.getElementById('<%= ButtonOk.ClientID %>').disabled = false;
                document.getElementById('<%= ButtonCancel.ClientID %>').disabled = false;
            }
            function SubmitSave() {
                document.getElementById('<%= ButtonOk.ClientID %>').disabled = false;
                document.getElementById('<%= ButtonCancel.ClientID %>').disabled = false;
                document.getElementById('<%= btnSave.ClientID %>').click();
            }

            function openWinPCareMemberInfo() {
                var oWnd = $find("<%= winPCare.ClientID %>");
                var txtGuarantorCardNo = $find("<%= txtGuarIDCardNo.ClientID %>");
                var txtRegistrationNo = $find("<%= txtRegistrationNo.ClientID %>");
                var txtPatientID = $find("<%= txtPatientID.ClientID %>");
                var txtSsn = $find("<%= txtSsn.ClientID %>");
                oWnd.setUrl('<%=Page.ResolveUrl("~/PCareCommon/PCareMemberInfo.aspx")%>?bpjsno=' + txtGuarantorCardNo.get_textBoxValue() + '&regno=' + txtRegistrationNo.get_textBoxValue() + '&patientid=' + txtPatientID.get_textBoxValue() + '&nik=' + txtSsn.get_textBoxValue());
                oWnd.setSize(1040, 450);
                oWnd.center();
                oWnd.show();
            }


            function winPCare_OnClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg != null) {
                    if (arg.NoKartu) {
                        var txtBpjsNo = $find("<%= txtGuarIDCardNo.ClientID %>");
                        txtBpjsNo.set_value(arg.NoKartu);
                    }
                }
            }
        </script>



        <style type="text/css">
            .cssLeft {
                float: left;
            }
        </style>
    </telerik:RadCodeBlock>
    <div style="display: none">
        <asp:Button runat="server" ID="btnSave" ValidationGroup="Registration" OnClick="btnSave_Click" Text="Save" EnableTheming="False" />
        <asp:CheckBox runat="server" ID="chkIsBpjsKapitasi" Checked="False" />
    </div>
    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtRegistrationTime">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboSRShift" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboGuarantorID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboSRBusinessMethod" />
                    <telerik:AjaxUpdatedControl ControlID="txtPlafonValue" />
                    <telerik:AjaxUpdatedControl ControlID="chkIsPlavonTypeGlobal" />
                    <%--<telerik:AjaxUpdatedControl ControlID="cboEmployeeID" />
                    <telerik:AjaxUpdatedControl ControlID="cboGuarSRRelationship" />--%>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboGuarantorGroupID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboSRBusinessMethod" />
                    <telerik:AjaxUpdatedControl ControlID="txtPlafonValue" />
                    <telerik:AjaxUpdatedControl ControlID="chkIsPlavonTypeGlobal" />
                    <%--<telerik:AjaxUpdatedControl ControlID="cboEmployeeID" />
                    <telerik:AjaxUpdatedControl ControlID="cboGuarSRRelationship" />--%>
                    <telerik:AjaxUpdatedControl ControlID="cboGuarantorID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtCoverageClassID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblCoverageClassName" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboSRReferralGroup">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboReferralID" />
                    <telerik:AjaxUpdatedControl ControlID="txtReferralName" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboReferralID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtReferralName" />
                    <telerik:AjaxUpdatedControl ControlID="cboSRReferralGroup" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtMedicalNo" />
                    <telerik:AjaxUpdatedControl ControlID="chkIsPrintingPatientCard" />
                    <telerik:AjaxUpdatedControl ControlID="txtPlafonValue" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboSRBusinessMethod">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="mpagDetail" />
                    <telerik:AjaxUpdatedControl ControlID="txtPlafonValue" />
                    <telerik:AjaxUpdatedControl ControlID="chkIsPlavonTypeGlobal" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboSRBusinessMethod">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="mpagDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdRegistrationItemRule">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistrationItemRule" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtMotherRegistrationNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtMotherMedicalNo" />
                    <telerik:AjaxUpdatedControl ControlID="txtMotherName" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboClass">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboServiceUnitID" />
                    <telerik:AjaxUpdatedControl ControlID="cboChargeClassID" />
                    <telerik:AjaxUpdatedControl ControlID="cboRoomID" />
                    <telerik:AjaxUpdatedControl ControlID="cboBedID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboBedID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboServiceUnitID" />
                    <telerik:AjaxUpdatedControl ControlID="cboRoomID" />
                    <telerik:AjaxUpdatedControl ControlID="cboClass" />
                    <telerik:AjaxUpdatedControl ControlID="cboChargeClassID" />
                    <telerik:AjaxUpdatedControl ControlID="txtMedicalNo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboRoomID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboServiceUnitID" />
                    <telerik:AjaxUpdatedControl ControlID="txtMedicalNo" />
                    <telerik:AjaxUpdatedControl ControlID="cboRoomID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboSRVisitReason">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboReasonForTreatmentID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <%--<telerik:AjaxSetting AjaxControlID="cboReasonForTreatmentID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboReasonForTreatmentDescID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboReasonForTreatmentDescID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboReasonForTreatmentDescID" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <telerik:AjaxSetting AjaxControlID="grdVisite">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdVisite" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdRegistrationGuarantor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistrationGuarantor" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGetExtQueue">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtExtQueNo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Modal="true" VisibleStatusbar="false"
        DestroyOnClose="false" Behavior="Close, Move" ReloadOnShow="True" ShowContentDuringLoad="false">
        <Windows>
            <telerik:RadWindow ID="winCaptureImage" Width="720px" Height="380px" runat="server"
                OnClientClose="onWinCaptureImageClose">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winRegPrint" Width="400px" Height="300px" runat="server" Title="Select report then click Ok button"
                OnClientClose="onClientCloseRegPrint">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winPrintPreview" Behavior="Maximize,Move,Close" Width="1000px"
                Height="630px" runat="server" Title="Preview">
            </telerik:RadWindow>

            <telerik:RadWindow ID="winWebCam" Width="680px" Height="620px" runat="server" Modal="true"
                ShowContentDuringLoad="false" Behaviors="None" VisibleStatusbar="False"
                OnClientClose="onWinWebCamClose">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image6" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlInfo2" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image5" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo2" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField runat="server" ID="hdnSRPatientRiskStatus" />
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 43%; vertical-align: top;">
                <table width="100%">
                    <asp:Panel runat="server" ID="pnlAppointment">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblAppointmentNo" runat="server" Text="Appointment No"></asp:Label>
                            </td>
                            <td class="entry300">
                                <telerik:RadTextBox ID="txtAppointmentNo" runat="server" Width="300px" MaxLength="20"
                                    ReadOnly="true" />
                            </td>
                            <td style="width: 20px;">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblAppointmentDate" runat="server" Text="Appointment Date / Time"></asp:Label>
                            </td>
                            <td class="entry300">
                                <table cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtAppointmentDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                                DatePopupButton-Enabled="false">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtAppointmentTime" runat="server" Width="50px" ReadOnly="true" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 20px">&nbsp;
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                        </td>
                        <td class="entry300">
                            <asp:Panel ID="Panel2" runat="server" CssClass="cssLeft">
                                <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20"
                                    ReadOnly="true" />
                            </asp:Panel>
                            <asp:Panel ID="pnlInfoReg" runat="server" CssClass="cssLeft">
                                <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();" class="noti_Container">
                                    <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo"
                                        Text=""></asp:Label>&nbsp; </a>
                            </asp:Panel>
                        </td>
                        <td style="width: 20px;">
                            <asp:RequiredFieldValidator ID="rfvRegistrationNo" runat="server" ErrorMessage="Registration No. required."
                                ValidationGroup="Registration" ControlToValidate="txtRegistrationNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationDate" runat="server" Text="Registration Date / Time"></asp:Label>
                        </td>
                        <td class="entry300">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 103px">
                                        <telerik:RadDatePicker ID="txtRegistrationDate" runat="server" Width="100px" DateInput-ReadOnly="false"
                                            DatePopupButton-Enabled="true">
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
                        <td style="width: 20px">
                            <asp:RequiredFieldValidator ID="rfvRegistrationTime" runat="server" ErrorMessage="Registration Time required."
                                ValidationGroup="Registration" ControlToValidate="txtRegistrationTime" SetFocusOnError="True"
                                Width="20px">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRShift" runat="server" Text="Shift"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboSRShift" runat="server" Width="300px" />
                        </td>
                        <td style="width: 20px">
                            <asp:RequiredFieldValidator ID="rfvSRShift" runat="server" ErrorMessage="Shift required."
                                ValidationGroup="Registration" ControlToValidate="cboSRShift" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientID" runat="server" Text="Patient ID"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtPatientID" runat="server" Width="150px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px">
                            <asp:RequiredFieldValidator ID="rfvPatientID" runat="server" ErrorMessage="Patient ID required."
                                ValidationGroup="Registration" ControlToValidate="txtPatientID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                        </td>
                        <td class="entry300">
                            <table cellspacing="0" cellpadding="">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="150px" MaxLength="15"
                                            ReadOnly="true" ShowButton="false" />
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnPatientNotes" runat="server" ImageUrl="../../../Images/stickynote16.png"
                                            CausesValidation="False" OnClientClick="openWinPatientNotes();return false;"
                                            ToolTip="Patient Notes" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px"></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px">
                            <asp:RequiredFieldValidator ID="rfvPatientName" runat="server" ErrorMessage="Patient Name required."
                                ValidationGroup="Registration" ControlToValidate="txtPatientName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr runat="server" id="trGuarantorHeader">
                        <td class="label">
                            <asp:Label ID="lblGuarantorGroup" runat="server" Text="Guarantor Group"></asp:Label>
                        </td>
                        <td class="entry300">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <uc:RadComboBoxExt ID="cboGuarantorGroupID" runat="server" Width="300px" LookUpType="GuarantorGroups"
                                            AutoPostBack="true" OnSelectedIndexChanged="cboGuarantorGroupID_SelectedIndexChanged">
                                        </uc:RadComboBoxExt>
                                        <%--<telerik:RadComboBox ID="cboGuarantorGroupID" runat="server" Width="300px" HighlightTemplatedItems="True"
                                            AutoPostBack="True" MarkFirstMatch="false" EnableLoadOnDemand="true" OnItemDataBound="cboGuarantorID_ItemDataBound"
                                            OnItemsRequested="cboGuarantorGroupID_ItemsRequested" OnSelectedIndexChanged="cboGuarantorGroupID_SelectedIndexChanged">
                                            <FooterTemplate>
                                                Note : Show max 30 result
                                            </FooterTemplate>
                                        </telerik:RadComboBox>--%>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px"></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                        </td>
                        <td class="entry300">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboGuarantorID" runat="server" Width="300px" HighlightTemplatedItems="True"
                                            AutoPostBack="True" MarkFirstMatch="false" EnableLoadOnDemand="true" NoWrap="False"
                                            OnItemDataBound="cboGuarantorID_ItemDataBound" OnItemsRequested="cboGuarantorID_ItemsRequested"
                                            OnSelectedIndexChanged="cboGuarantorID_SelectedIndexChanged">
                                            <FooterTemplate>
                                                Note : Show max 30 result
                                            </FooterTemplate>
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
                        <td style="width: 20px">
                            <asp:RequiredFieldValidator ID="rfvGuarantorID" runat="server" ErrorMessage="Guarantor required."
                                ValidationGroup="Registration" ControlToValidate="cboGuarantorID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBusinessMethod" runat="server" Text="Guarantor Method"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboSRBusinessMethod" runat="server" Width="300px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboSRBusinessMethod_SelectedIndexChanged" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="60">
                            <asp:RequiredFieldValidator ID="rfvBusinessMethod" runat="server" ErrorMessage="Guarantor Method required."
                                ValidationGroup="Registration" ControlToValidate="cboSRBusinessMethod" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Plafond Amount"></asp:Label>
                        </td>
                        <td class="entry300">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtPlafonValue" runat="server" Width="150px" Value="0">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsPlavonTypeGlobal" runat="server" Text="Global Plafond" Enabled="False" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRReferralGroup" runat="server" Text="Referral Group"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboSRReferralGroup" runat="server" Width="300px" AutoPostBack="true"
                                EnableLoadOnDemand="true" MarkFirstMatch="False" HighlightTemplatedItems="true"
                                OnItemDataBound="cboSRReferralGroup_ItemDataBound" OnItemsRequested="cboSRReferralGroup_ItemsRequested"
                                OnSelectedIndexChanged="cboSRReferralGroup_SelectedIndexChanged">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td style="width: 20px">
                            <asp:RequiredFieldValidator ID="rfvSRReferralGroup" runat="server" ErrorMessage="Referral Group is required."
                                ValidationGroup="Registration" ControlToValidate="cboSRReferralGroup" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReferralID" runat="server" Text="Referral"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox runat="server" ID="cboReferralID" Width="300px" AutoPostBack="True"
                                OnSelectedIndexChanged="cboReferralID_SelectedIndexChanged" HighlightTemplatedItems="True"
                                MarkFirstMatch="false" EnableLoadOnDemand="true" NoWrap="True" OnItemDataBound="cboReferralID_ItemDataBound"
                                OnItemsRequested="cboReferralID_ItemsRequested">
                            </telerik:RadComboBox>
                        </td>
                        <td style="width: 20px"></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReferralName" runat="server" Text="Referral Description"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtReferralName" runat="server" Width="300px" MaxLength="100">
                            </telerik:RadTextBox>
                        </td>
                        <td style="width: 20px"></td>
                    </tr>
                    <tr runat="server" id="trPromoPackage">
                        <td class="label">
                            <asp:Label ID="lblItemConditionRuleID" runat="server" Text="Promo Package"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboItemConditionRuleID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboItemConditionRuleID_ItemDataBound"
                                OnItemsRequested="cboItemConditionRuleID_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ItemConditionRuleName")%>
                                    </b>
                                    &nbsp;- Valid From: &nbsp;
                                    <%# System.Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "StartingDate")).ToString(Temiang.Avicenna.Common.AppConstant.DisplayFormat.Date)%>
                                    &nbsp;to&nbsp;
                                    <%# System.Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "EndingDate")).ToString(Temiang.Avicenna.Common.AppConstant.DisplayFormat.Date)%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td style="width: 20px"></td>
                    </tr>
                    <tr runat="server" id="trMembershipNo">
                        <td class="label">
                            <asp:Label ID="lblMembershipNo" runat="server" Text="Employee Membership No"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboMembershipNo" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboMembershipNo_ItemDataBound"
                                OnItemsRequested="cboMembershipNo_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "MembershipNo")%>
                                    </b>
                                    &nbsp;- Join Date: &nbsp;
                                    <%# System.Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "JoinDate")).ToString(Temiang.Avicenna.Common.AppConstant.DisplayFormat.Date)%>
                                    <br />
                                    <b><%# DataBinder.Eval(Container.DataItem, "MemberName") %></b>
                                    Address :
                                    <%# DataBinder.Eval(Container.DataItem, "Address")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td style="width: 20px"></td>
                    </tr>
                </table>
            </td>
            <td style="width: 43%; vertical-align: top">
                <table width="100%" id="tblClass" runat="server">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblClass" runat="server" Text="Class"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboClass" runat="server" Width="284px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboClass_SelectedIndexChanged" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                    </tr>
                </table>
                <table width="100%">
                    <tr runat="server" id="tblServiceUnit">
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="284px" AutoPostBack="True"
                                OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged" HighlightTemplatedItems="True"
                                MarkFirstMatch="False" OnItemDataBound="cboServiceUnitID_ItemDataBound" OnItemsRequested="cboServiceUnitID_ItemsRequested"
                                EnableLoadOnDemand="true" NoWrap="True">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                                ValidationGroup="Registration" ControlToValidate="cboServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image21" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr runat="server" id="tblVisitType">
                        <td class="label">
                            <asp:Label ID="lblVisitTypeID" runat="server" Text="Visit Type"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboVisitTypeID" runat="server" Width="284px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr id="tblParamedic" runat="server">
                        <td class="label">
                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="284px" AllowCustomText="true"
                                Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboParamedicID_OnSelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvParamedicID" runat="server" ErrorMessage="Physician required."
                                ValidationGroup="Registration" ControlToValidate="cboParamedicID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <table width="100%" id="tblPhysicianSenders" runat="server" visible="False">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPhysicianSenders" runat="server" Text="Physician Senders"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtPhysicianSenders" runat="server" Width="280px" MaxLength="150" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                </table>
                <table id="tblRoom" runat="server" width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRoomID" runat="server" Text="Room"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboRoomID" runat="server" Width="284px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboRoomID_SelectedIndexChanged" HighlightTemplatedItems="True"
                                MarkFirstMatch="true" OnItemDataBound="cboRoomID_ItemDataBound" OnItemsRequested="cboRoomID_ItemsRequested"
                                EnableLoadOnDemand="true" NoWrap="True">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRoomID" runat="server" ErrorMessage="Room required."
                                ValidationGroup="Registration" ControlToValidate="cboRoomID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image22" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <table id="tblQue" runat="server" width="100%" visible="false">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblQue" runat="server" Text="Que No"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboQue" runat="server" Width="284px" AutoPostBack="True"
                                OnSelectedIndexChanged="cboQue_SelectedIndexChanged" OnItemDataBound="cboQue_ItemDataBound">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                    </tr>
                </table>
                <table id="tblInPatient" runat="server" width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBedID" runat="server" Text="Bed No"></asp:Label>
                        </td>
                        <td class="entry300">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboBedID" runat="server" Width="284px" AutoPostBack="true"
                                            OnSelectedIndexChanged="cboBedID_SelectedIndexChanged" HighlightTemplatedItems="True"
                                            MarkFirstMatch="true" OnItemDataBound="cboBedID_ItemDataBound" OnItemsRequested="cboBedID_ItemsRequested"
                                            EnableLoadOnDemand="true" NoWrap="True">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "ShortName")%>
                                                &nbsp;-&nbsp; <b>
                                                    <%# DataBinder.Eval(Container.DataItem, "BedID")%>
                                                </b>&nbsp;:&nbsp;
                                                <%# DataBinder.Eval(Container.DataItem, "RegistrationNo")%>
                                                &nbsp; (<%# DataBinder.Eval(Container.DataItem, "Sex")%>)
                                                <br />
                                                <%# DataBinder.Eval(Container.DataItem, "PatientName")%>
                                            </ItemTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:ImageButton ID="ibtnBedNotes" runat="server" ImageUrl="../../../Images/infoyellow16.png"
                                            CausesValidation="False" OnClientClick="openWinBedNotesInfo();return false;"
                                            ToolTip="Bed Notes" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:Panel ID="pnlBedBooking" runat="server" CssClass="cssLeft" Visible="False">
                                            <a href="javascript:void(0);" onclick="javascript:openWinBedBookingInfo();" class="noti_ContainerInfo"
                                                title="Bed Booking Info"></a>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvBedID" runat="server" ErrorMessage="Bed No required."
                                ValidationGroup="Registration" ControlToValidate="cboBedID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image24" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblChargeClassID" runat="server" Text="Charge Class"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboChargeClassID" runat="server" Width="284px" AutoPostBack="True"
                                OnSelectedIndexChanged="cboChargeClassID_SelectedIndexChanged" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvChargeClassID" runat="server" ErrorMessage="Charge Class required."
                                ValidationGroup="Registration" ControlToValidate="cboChargeClassID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image25" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCoverageClassID" runat="server" Text="Coverage Class"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboCoverageClassID" runat="server" Width="284px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td style="width: 20px">
                            <asp:RequiredFieldValidator ID="rfvCoverageClassID" runat="server" ErrorMessage="Coverage Class required."
                                ValidationGroup="Registration" ControlToValidate="cboCoverageClassID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image26" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr runat="server" id="trSMF">
                        <td class="label">
                            <asp:Label ID="lblSmfID" runat="server" Text="SMF"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboSmfID" runat="server" Width="284px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td style="width: 20px">
                            <asp:RequiredFieldValidator ID="rfvSmfID" runat="server" ErrorMessage="SMF required."
                                ValidationGroup="Registration" ControlToValidate="cboSmfID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientInType" runat="server" Text="Patient In Type"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboSRPatientInType" runat="server" Width="284px" Enabled="False" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPatientInType" runat="server" ErrorMessage="Patient In Type required."
                                ValidationGroup="Registration" ControlToValidate="cboSRPatientInType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr runat="server" id="trReferFromUnitName">
                        <td class="label">
                            <asp:Label ID="lblReferFromUnitName" runat="server" Text="Refer From"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtReferFromUnitName" runat="server" Width="280px" ReadOnly="true" />
                            <telerik:RadTextBox ID="txtReferFromUnitID" runat="server" Visible="False" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr runat="server" id="trReferByPhyisician">
                        <td class="label">
                            <asp:Label ID="lblReferByPhyisician" runat="server" Text="Refer By"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboReferByPhyisician" runat="server" Width="284px" HighlightTemplatedItems="True"
                                OnItemDataBound="cboReferByPhyisician_ItemDataBound" OnItemsRequested="cboReferByPhyisician_ItemsRequested"
                                EnableLoadOnDemand="true" NoWrap="True">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ParamedicName")%>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry300">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkIsParturition" runat="server" Text="Parturition" OnCheckedChanged="chkIsParturition_CheckedChanged"
                                            AutoPostBack="True" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsNewBornInfant" runat="server" Text="Newborn Infant" OnCheckedChanged="chkIsNewBornInfant_CheckedChanged"
                                            AutoPostBack="True" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                    </tr>
                </table>
                <table id="tblRoomIn" runat="server" width="100%">
                    <tr>
                        <td class="label"></td>
                        <td class="entry300">
                            <asp:CheckBox ID="chkIsRoomIn" runat="server" Text="Rooming In" Enabled="false" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td class="label">&nbsp;
                        </td>
                        <td class="entry300">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkIsPrintingPatientCard" runat="server" Text="Print Patient Card" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsSkipAutoBill" runat="server" Text="Skip Auto Bill" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr id="pnlBtnPrint" runat="server" visible="True">
                        <td>&nbsp;
                        </td>
                        <td class="entry300">
                            <asp:Button ID="btnPrint" runat="server" CssClass="minimal" Text="PRINT" Width="100px"
                                OnClientClick="javascript:openRegPrint();return false;" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr id="pnlQueNo" runat="server" visible="False">
                        <td></td>
                        <td class="entry300">
                            <asp:Label ID="lblQueNo" runat="server" Font-Bold="True" Font-Size="30"></asp:Label>
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr id="pnlInfoRegOpAlreadyExist" runat="server" visible="False">
                        <td colspan="3">
                            <asp:Label ID="lblInfoRegOpAlreadyExist" runat="server" ForeColor="Blue" Font-Size="11"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblPhysicianIsOnLeave" runat="server" Text="" ForeColor="Red" Font-Size="11"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table id="tblExtQueNo" runat="server" width="100%">
                    <tr>
                        <td class="label">External Queue No</td>
                        <td class="entry300">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox runat="server" ID="txtExtQueNo" MaxLength="20" Width="283px"></telerik:RadTextBox>
                                    </td>
                                    <td width="5px"></td>
                                    <td>
                                        <asp:ImageButton ID="btnGetExtQueue" runat="server" ImageUrl="~/Images/Toolbar/download16.png"
                                            OnClick="btnGetExtQueue_Click" ToolTip="Get External Queue No" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td>
                            <uc1:PCareMemberInfoStatus runat="server" ID="pcareMemberInfoStatus" />
                        </td>
                        <td width="40px"></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <fieldset id="FieldSet1" style="width: 180px; min-height: 180px;">
                                <legend>Photo</legend>
                                <asp:Image runat="server" ID="imgPatientPhoto" Width="180px" Height="180px" />
                                <%--                                <asp:Button runat="server" Text="Update Photo" ID="btnCaptureImage" Width="180px"
                                    OnClientClick="openWinCaptureImage();return false;" />--%>
                                <asp:Button runat="server" Text="Update Photo" ID="btnCaptureImage" Width="180px"
                                    OnClientClick="openWinWebCam();return false;" />
                                <asp:HiddenField runat="server" ID="hdnImgData" />
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset id="fsLastVisit" style="width: 184px;" runat="server">
                                <legend>Last Visit</legend>
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td style="width: 25%">Date
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:Label ID="lblLastVisitDate" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Service Unit
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:Label ID="lblLastVisitSvcUnit" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Guarantor
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:Label ID="lblLastGuar" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Referral Group
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:Label ID="lblLastReferralGroup" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Referral
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:Label ID="lblLastReferral" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Physician
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:Label ID="lblLastPhysician" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset id="fsOtherMRN" style="width: 184px;" runat="server">
                                <legend>Other MRN</legend>
                                <telerik:RadGrid ID="grdOtherMrn" runat="server" OnNeedDataSource="grdOtherMrn_NeedDataSource"
                                    AutoGenerateColumns="False" GridLines="None">
                                    <HeaderContextMenu>
                                    </HeaderContextMenu>
                                    <MasterTableView CommandItemDisplay="None" DataKeyNames="PatientID" ShowHeader="False">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="MRN" UniqueName="MedicalNo"
                                                SortExpression="MedicalNo">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <FilterMenu>
                                    </FilterMenu>
                                    <ClientSettings EnableRowHoverStyle="True">
                                        <Resizing AllowColumnResize="True" />
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpagDetail">
        <Tabs>
            <telerik:RadTab runat="server" Text="C O B" Selected="True" PageViewID="pgOtherGuarantor">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Patient Information" PageViewID="pgPatientInformation">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Birth Information" PageViewID="pgBirthInformation"
                Visible="false">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Anamnesis & Complaint" PageViewID="pgAnamnesis">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Responsible Person" PageViewID="pgResponsible">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Emergency Contact" PageViewID="pgEmergency">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Registration Item Rule" PageViewID="pgItemRule"
                Visible="False">
            </telerik:RadTab>
            <telerik:RadTab Text="Outstanding Visite" runat="server" PageViewID="pgVisite">
            </telerik:RadTab>
            <telerik:RadTab Text="ER Info Detail" runat="server" PageViewID="pgDetailER" Visible="False">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpagDetail" runat="server" BorderStyle="Solid" SelectedIndex="0"
        BorderColor="Gray">
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
                    <EditFormSettings UserControlName="RegistrationGuarantorDetail.ascx" EditFormType="WebUserControl">
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
        <telerik:RadPageView ID="pgPatientInformation" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%" valign="top">
                        <table width="100%">
<%--                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSex" runat="server" Text="Gender"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <asp:RadioButton ID="optSexFemale" runat="server" Text="Female" GroupName="Sex" Enabled="false" />
                                    <asp:RadioButton ID="optSexMale" runat="server" Text="Male" GroupName="Sex" Enabled="false" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>--%>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSex" runat="server" Text="Gender"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadComboBox ID="cboSRGenderType" runat="server" Width="300px" AllowCustomText="true" Enabled ="false"
                                        Filter="Contains">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDateOfBirth" runat="server" Text="Date Of Birth"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadDatePicker ID="txtDateOfBirth" runat="server" Width="100px" DateInput-ReadOnly="true"
                                        DatePopupButton-Enabled="false">
                                    </telerik:RadDatePicker>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadNumericTextBox ID="txtAgeInYear" runat="server" Width="30px" ReadOnly="true">
                                        <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                    </telerik:RadNumericTextBox>
                                    &nbsp;Y&nbsp;
                                    <telerik:RadNumericTextBox ID="txtAgeInMonth" runat="server" Width="30px" ReadOnly="true">
                                        <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                    </telerik:RadNumericTextBox>
                                    &nbsp;M&nbsp;
                                    <telerik:RadNumericTextBox ID="txtAgeInDay" runat="server" Width="30px" ReadOnly="true">
                                        <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                    </telerik:RadNumericTextBox>
                                    &nbsp;D&nbsp;
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblIsDisability" runat="server" Text="Disability"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <asp:RadioButtonList ID="rblIsDisability" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="0" Text="No" Selected="True" />
                                        <asp:ListItem Value="1" Text="Yes" />
                                    </asp:RadioButtonList>
                                </td>
                                <td style="width: 20px"></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSsn" runat="server" Text="SSN"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadTextBox ID="txtSsn" runat="server" Width="300px" ReadOnly="true" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPassportNo" runat="server" Text="Passport No"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadTextBox ID="txtPassportNo" runat="server" Width="300px" ReadOnly="true" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblGuarIDCardNo" runat="server" Text="BPJS No / Guarantor Card No"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadTextBox ID="txtGuarIDCardNo" runat="server" Width="300px" MaxLength="50" />
                                </td>
                                <td width="20">
                                    <asp:ImageButton ID="btnBpjsInfo" runat="server" ImageUrl="../../../Images/stickynote16.png"
                                        CausesValidation="False" OnClientClick="openWinPCareMemberInfo();return false;"
                                        ToolTip="BPJS Status" />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label" width="30%">
                                    <asp:Label ID="lblSRPatientCategory" runat="server" Text="Patient Category"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadComboBox ID="cboSRPatientCategory" runat="server" Width="300px" AllowCustomText="true"
                                        Filter="Contains" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr style="display: none">
                                <td class="label" width="30%">
                                    <asp:Label ID="lblMemberID" runat="server" Text="Member Package"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadTextBox ID="txtMemberID" runat="server" Width="100px" ReadOnly="true" />
                                    &nbsp;
                                    <asp:Label ID="lblMemberName" runat="server" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%" valign="top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSROccupation" runat="server" Text="Occupation"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadComboBox ID="cboSROccupation" runat="server" Width="300px" AllowCustomText="true" va
                                        Filter="Contains">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCompany" runat="server" Text="Company"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadTextBox ID="txtCompany" runat="server" Width="300px" MaxLength="100">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblInsuranceID" runat="server" Text="Insurance ID"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadTextBox ID="txtInsuranceID" runat="server" Width="300px" MaxLength="255" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label3" runat="server" Text="BPJS SEP No"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadTextBox ID="txtBpjsSepNo" runat="server" Width="300px" MaxLength="50"
                                        ShowButton="false" ClientEvents-OnButtonClick="openWinBpjsInfo" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <asp:Panel runat="server" ID="pnlEmployeeInfo">
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
                                                <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>
                                                <br />
                                                Class :
                                                <%# DataBinder.Eval(Container.DataItem, "ClassName")%>
                                                &nbsp;-&nbsp; BPJS Class :
                                                <%# DataBinder.Eval(Container.DataItem, "ClassNameBPJS")%>
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
                                        <telerik:RadComboBox ID="cboGuarSRRelationship" runat="server" Width="300px" AllowCustomText="true"
                                            Filter="Contains" />
                                    </td>
                                    <td width="20"></td>
                                    <td></td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" TextMode="MultiLine" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr runat="server" id="trOutstandingInfo" visible="false">
                                <td class="label" />
                                <td class="entry2Column">
                                    <asp:Label ID="lblOutstandingInfo" runat="server" Text="Patient have outstanding payment"
                                        Font-Bold="True" ForeColor="Red"></asp:Label>
                                </td>
                                <td width="20" />
                                <td />
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgBirthInformation" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblMotherRegistrationNo" runat="server" Text="Mother Registration No"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadTextBox ID="txtMotherRegistrationNo" runat="server" Width="300px" ShowButton="true"
                                        MaxLength="20" AutoPostBack="true" OnTextChanged="txtMotherRegistrationNo_TextChanged"
                                        ClientEvents-OnButtonClick="openWinRegistration" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblMotherMedicalNo" runat="server" Text="Mother Medical No"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadTextBox ID="txtMotherMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblMotherName" runat="server" Text="Mother Name"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadTextBox ID="txtMotherName" runat="server" Width="300px" ReadOnly="true" />
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
        <telerik:RadPageView ID="pgAnamnesis" runat="server">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblComplaint" runat="server" Text="Chief Complaint"></asp:Label>
                    </td>
                    <td class="entrydescription">
                        <telerik:RadTextBox ID="txtComplaint" runat="server" Width="800px" MaxLength="4000"
                            TextMode="MultiLine" Height="45px" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblAnamnesis" runat="server" Text="Anamnesis"></asp:Label>
                    </td>
                    <td class="entrydescription">
                        <telerik:RadTextBox ID="txtAnamnesis" runat="server" Width="800px" MaxLength="4000"
                            TextMode="MultiLine" Height="45px" />
                    </td>
                </tr>
            </table>
            <table width="100%" runat="server" id="pnlDiagnose" visible="false">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblInitialDiagnose" runat="server" Text="Initial Diagnose/INA-CBG Grouper"></asp:Label>
                    </td>
                    <td class="entrydescription">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txtInitialDiagnose" runat="server" Width="800px" MaxLength="4000"
                                        TextMode="MultiLine" Height="45px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadComboBox ID="cboBpjsPackageID" runat="server" Width="804px" AutoPostBack="False"
                                        EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="True"
                                        OnItemDataBound="cboBpjsPackageID_ItemDataBound" OnItemsRequested="cboBpjsPackageID_ItemsRequested">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblMedicationPlanning" runat="server" Text="Medication Planning"></asp:Label>
                    </td>
                    <td class="entrydescription">
                        <telerik:RadTextBox ID="txtMedicationPlanning" runat="server" Width="800px" MaxLength="4000"
                            TextMode="MultiLine" Height="45px" />
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgResponsible" runat="server">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 50%" valign="top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblNameOfTheResponsible" runat="server" Text="Responsible Name"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadTextBox ID="txtNameOfTheResponsible" runat="server" Width="300px" MaxLength="150" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvNameOfTheResponsible" runat="server" ErrorMessage="Responsible Name required."
                                        ValidationGroup="Registration" ControlToValidate="txtNameOfTheResponsible" SetFocusOnError="True"
                                        Width="100%" Visible="False">
                                        <asp:Image ID="Image16" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSsnOfTheResponsible" runat="server" Text="SSN"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadTextBox ID="txtSsnOfTheResponsible" runat="server" Width="300px" MaxLength="50" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvSsnOfTheResponsible" runat="server" ErrorMessage="Responsible SSN required."
                                        ValidationGroup="Registration" ControlToValidate="txtSsnOfTheResponsible" SetFocusOnError="True"
                                        Width="100%" Visible="False">
                                        <asp:Image ID="Image19" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblResponsiblePersonRelationShip" runat="server" Text="Relation"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadComboBox ID="cboResponsiblePersonRelationShip" runat="server" Width="300px"
                                        AllowCustomText="true" Filter="Contains" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvResponsiblePersonRelationShip" runat="server" ErrorMessage="Responsible Relation required."
                                        ValidationGroup="Registration" ControlToValidate="cboResponsiblePersonRelationShip" SetFocusOnError="True"
                                        Width="100%" Visible="False">
                                        <asp:Image ID="Image20" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblResponsiblePersonOccupation" runat="server" Text="Occupation"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadComboBox ID="cboResponsiblePersonOccupation" runat="server" Width="300px"
                                        AllowCustomText="true" Filter="Contains" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvResponsiblePersonOccupation" runat="server" ErrorMessage="Responsible Occupation required."
                                        ValidationGroup="Registration" ControlToValidate="cboResponsiblePersonOccupation" SetFocusOnError="True"
                                        Width="100%" Visible="False">
                                        <asp:Image ID="Image23" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label1" runat="server" Text="Job Description"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadTextBox ID="txtResponsiblePersonJobDescription" runat="server" Width="300px"
                                        MaxLength="150" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%" valign="top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblResponsiblePersonAddress" runat="server" Text="Address"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadTextBox ID="txtResponsiblePersonAddress" runat="server" Width="300px"
                                        MaxLength="250" TextMode="MultiLine" Height="60px" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvResponsiblePersonAddress" runat="server" ErrorMessage="Responsible Address required."
                                        ValidationGroup="Registration" ControlToValidate="txtResponsiblePersonAddress" SetFocusOnError="True"
                                        Width="100%" Visible="False">
                                        <asp:Image ID="Image27" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblResponsiblePhoneNo" runat="server" Text="Phone No"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadTextBox ID="txtResponsiblePhoneNo" runat="server" Width="300px" MaxLength="50" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvResponsiblePhoneNo" runat="server" ErrorMessage="Responsible Phone No required."
                                        ValidationGroup="Registration" ControlToValidate="txtResponsiblePhoneNo" SetFocusOnError="True"
                                        Width="100%" Visible="False">
                                        <asp:Image ID="Image28" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgEmergency" runat="server">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblContactName" runat="server" Text="Contact Name"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadTextBox ID="txtContactName" runat="server" Width="300px" MaxLength="100" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblContactSsn" runat="server" Text="SSN"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadTextBox ID="txtContactSsn" runat="server" Width="300px" MaxLength="50" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRRelation" runat="server" Text="Relation"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadComboBox ID="cboSRRelation" runat="server" Width="300px" AllowCustomText="true"
                                        Filter="Contains" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRContactOccupation" runat="server" Text="Occupation"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadComboBox ID="cboSRContactOccupation" runat="server" Width="300px" AllowCustomText="true"
                                        Filter="Contains" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <uc1:AddressCtl ID="AddressCtl" runat="server" />
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgItemRule" runat="server">
            <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <telerik:RadGrid ID="grdRegistrationItemRule" runat="server" OnNeedDataSource="grdRegistrationItemRule_NeedDataSource"
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
                                        <HeaderStyle HorizontalAlign="Left" Width="150px" />
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
                                        <HeaderStyle Width="30px" />
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
                                <Resizing AllowColumnResize="True" />
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgVisite" runat="server">
            <telerik:RadGrid ID="grdVisite" runat="server" AutoGenerateColumns="False" GridLines="None"
                OnNeedDataSource="grdVisite_NeedDataSource" OnDetailTableDataBind="grdVisite_DetailTableDataBind">
                <MasterTableView DataKeyNames="PaymentNo,ItemID" GroupLoadMode="Client" AllowPaging="true"
                    PageSize="8">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="PaymentNo" HeaderText="Payment No "></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="PaymentNo" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="30px"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                    runat="server"></asp:CheckBox>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="detailChkbox" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="PaymentNo" UniqueName="PaymentNo" HeaderText="Payment No"
                            SortExpression="PaymentNo" HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ItemID" HeaderText="ItemID" SortExpression="ItemID"
                            UniqueName="ItemID" Visible="false" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" SortExpression="ItemName"
                            UniqueName="ItemName" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="VisiteQty" HeaderText="Visite Qty"
                            UniqueName="VisiteQty" SortExpression="VisiteQty" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RealizationQty" HeaderText="Realization Qty"
                            UniqueName="RealizationQty" SortExpression="RealizationQty" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="detail" DataKeyNames="RegistrationNo, PaymentNo, PatientID, ItemID, PaymentReferenceNo" AutoGenerateColumns="false" AllowPaging="true">
                            <Columns>
                                <telerik:GridBoundColumn DataField="RegistrationNo" UniqueName="RegistrationNo" HeaderText="Registration No"
                                    SortExpression="RegistrationNo" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="RegistrationDate"
                                    HeaderText="Date" UniqueName="RegistrationDate" SortExpression="RegistrationDate"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="RegistrationTime" HeaderText="Time"
                                    UniqueName="RegistrationTime" SortExpression="RegistrationTime" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridTemplateColumn />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="PaymentReferenceNo"
                                    HeaderText="Reference No" UniqueName="PaymentReferenceNo" SortExpression="PaymentReferenceNo"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true" AllowGroupExpandCollapse="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgDetailER" runat="server">
            <table width="100%">
                <tr>
                    <td style="width: 50%" valign="top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRPatientInTypeEr" runat="server" Text="Patient In Type"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadComboBox ID="cboSRPatientInTypeEr" runat="server" Width="300px" AutoPostBack="True"
                                        OnSelectedIndexChanged="cboSRPatientInTypeEr_SelectedIndexChanged" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRPatientInCondition" runat="server" Text="Patient Initial Condition"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadComboBox ID="cboSRPatientInCondition" runat="server" Width="300px" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvSRPatientInCondition" runat="server" ErrorMessage="Patient Initial Condition required."
                                        ValidationGroup="Registration" ControlToValidate="cboSRPatientInCondition" SetFocusOnError="True">
                                        <asp:Image ID="Image14" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRERCaseType" runat="server" Text="Case Type"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadComboBox ID="cboSRERCaseType" runat="server" Width="300px" />
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvSRERCaseType" runat="server" ErrorMessage="Case Type required."
                                        ValidationGroup="Registration" ControlToValidate="cboSRERCaseType" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image18" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRTriage" runat="server" Text="Triage"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadComboBox ID="cboSRTriage" runat="server" Width="300px" />
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvSRTriage" runat="server" ErrorMessage="Triage required."
                                        ValidationGroup="Registration" ControlToValidate="cboSRTriage" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image32" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>

                        </table>
                    </td>
                    <td style="width: 50%" valign="top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRVisitReason" runat="server" Text="Visit Reason"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadComboBox ID="cboSRVisitReason" runat="server" Width="300px" AutoPostBack="True"
                                        OnSelectedIndexChanged="cboSRVisitReason_SelectedIndexChanged" />
                                </td>
                                <td style="width: 20px">
                                    <asp:RequiredFieldValidator ID="rfvSRVisitReason" runat="server" ErrorMessage="Visit Reason required."
                                        ValidationGroup="Registration" ControlToValidate="cboSRVisitReason" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblReasonForTreatment" runat="server" Text="Reason For Treatment"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadComboBox ID="cboReasonForTreatmentID" runat="server" Width="300px" HighlightTemplatedItems="True"
                                        AutoPostBack="True" MarkFirstMatch="false" EnableLoadOnDemand="true" NoWrap="True"
                                        OnItemDataBound="cboReasonForTreatmentID_ItemDataBound" OnItemsRequested="cboReasonForTreatmentID_ItemsRequested"
                                        OnSelectedIndexChanged="cboReasonForTreatmentID_SelectedIndexChanged">
                                        <FooterTemplate>
                                            Note : Show max 20 result
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvReasonForTreatmentID" runat="server" ErrorMessage="Reason For Treatment required."
                                        ValidationGroup="Registration" ControlToValidate="cboReasonForTreatmentID" SetFocusOnError="True">
                                        <asp:Image ID="Image17" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblReasonForTreatmentDesc" runat="server" Text="Reason For Treatment Desc"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadComboBox ID="cboReasonForTreatmentDescID" runat="server" Width="300px"
                                        HighlightTemplatedItems="True" AutoPostBack="True" MarkFirstMatch="false" EnableLoadOnDemand="true"
                                        NoWrap="True" OnItemDataBound="cboReasonForTreatmentDescID_ItemDataBound" OnItemsRequested="cboReasonForTreatmentDescID_ItemsRequested">
                                        <FooterTemplate>
                                            Note : Show max 20 result
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCauseOfAccident" runat="server" Text="Cause Of Accident / Type Or Location Of The Bite"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadTextBox ID="txtCauseOfAccident" runat="server" Width="300px" MaxLength="250"
                                        TextMode="MultiLine" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
