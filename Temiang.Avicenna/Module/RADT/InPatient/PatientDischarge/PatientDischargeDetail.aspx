<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PatientDischargeDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.InPatient.PatientDischargeDetail"
    Title="Patient Discharge" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function openWinRegistrationInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationInfo.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboSRDischargeMethod">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboReferralIdTo" />
                    <telerik:AjaxUpdatedControl ControlID="txtReferralNameTo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboReferralIdTo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtReferralNameTo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboParamedicID" />
                    <telerik:AjaxUpdatedControl ControlID="cboRoomID" />
                    <telerik:AjaxUpdatedControl ControlID="cboQue" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboParamedicID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboRoomID" />
                    <telerik:AjaxUpdatedControl ControlID="cboQue" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry" />
    <asp:CustomValidator ID="customValidator" ValidationGroup="entry" runat="server"></asp:CustomValidator>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" ReadOnly="true" />
                            <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();" class="noti_Container">
                                <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo" />
                            </a>
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationDate" runat="server" Text="Registration Date / Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtRegistrationDate" runat="server" Width="100px" Enabled="false" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtRegistrationTime" runat="server" Width="50px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px"></td>
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
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="245px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="22px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px"></td>
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
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnit" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnit" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRoomBed" runat="server" Text="Room / Bed"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRoomBed" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblClass" runat="server" Text="Class"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtClass" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsRoomIn" runat="server" Text="Room In" Enabled="False" />
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="1" cellspacing="5">
        <tr>
            <td>
                <table>
                    <tr>
                        <td style="width: 50%; vertical-align: top">
                            <fieldset>
                                <legend>
                                    <asp:Label ID="Label5" runat="server" Text="DISCHARGE DETAIL" Font-Bold="True" Font-Size="9"></asp:Label>
                                </legend>
                                <table style="width: 100%">
                                    <tr>
                                        <td width="100%" style="vertical-align: top">
                                            <table width="100%">
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblDischargeDate" runat="server" Text="Discharge Date / Time"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <table cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td>
                                                                    <telerik:RadDatePicker ID="txtDischargeDate" runat="server" Width="100px" />
                                                                </td>
                                                                <td>&nbsp;
                                                                </td>
                                                                <td>
                                                                    <telerik:RadMaskedTextBox ID="txtDischargeTime" runat="server" Mask="<00..23>:<00..59>"
                                                                        PromptChar="_" RoundNumericRanges="false" Width="50px">
                                                                    </telerik:RadMaskedTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 20px">
                                                        <asp:RequiredFieldValidator ID="rfvDischargeDate" runat="server" ErrorMessage="Discharge Date required."
                                                            ValidationGroup="entry" ControlToValidate="txtDischargeDate" SetFocusOnError="True">
                                                            <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                                                        </asp:RequiredFieldValidator>&nbsp;
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblSRDischargeMethod" runat="server" Text="Discharge Method"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboSRDischargeMethod" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="cboSRDischargeMethod_SelectedIndexChanged" />
                                                    </td>
                                                    <td style="width: 20px">
                                                        <asp:RequiredFieldValidator ID="rfvSRDischargeMethod" runat="server" ErrorMessage="Discharge Method required."
                                                            ValidationGroup="entry" ControlToValidate="cboSRDischargeMethod" SetFocusOnError="True"
                                                            Width="100%">
                                                            <asp:Image ID="Image16" runat="server" SkinID="rfvImage" />
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblReferralIdTo" runat="server" Text="Refer To"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox runat="server" ID="cboReferralIdTo" Width="300px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="cboReferralIdTo_SelectedIndexChanged" HighlightTemplatedItems="True"
                                                            MarkFirstMatch="false" EnableLoadOnDemand="true" NoWrap="True" OnItemDataBound="cboReferralIdTo_ItemDataBound"
                                                            OnItemsRequested="cboReferralIdTo_ItemsRequested">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td style="width: 20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblReferralNameTo" runat="server" Text="Refer To Description"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadTextBox ID="txtReferralNameTo" runat="server" Width="300px" MaxLength="100">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td style="width: 20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblSRDischargeCondition" runat="server" Text="Discharge Condition"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboSRDischargeCondition" runat="server" Width="300px" />
                                                    </td>
                                                    <td style="width: 20px">
                                                        <asp:RequiredFieldValidator ID="rfvSRDischargeCondition" runat="server" ErrorMessage="Discharge Condition required."
                                                            ValidationGroup="entry" ControlToValidate="cboSRDischargeCondition" SetFocusOnError="True"
                                                            Width="100%">
                                                            <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblDeathCertificateNo" runat="server" Text="Death Certificate No"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadTextBox ID="txtDeathCertificateNo" runat="server" Width="300px" MaxLength="20" />
                                                    </td>
                                                    <td style="width: 20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblDischargeMedicalNotes" runat="server" Text="Discharge Medical Notes"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadTextBox ID="txtDischargeMedicalNotes" runat="server" Width="300px" MaxLength="4000"
                                                            TextMode="MultiLine" />
                                                    </td>
                                                    <td style="width: 20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblDischargeNotes" runat="server" Text="Discharge Notes"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadTextBox ID="txtDischargeNotes" runat="server" Width="300px" MaxLength="4000"
                                                            TextMode="MultiLine" />
                                                    </td>
                                                    <td style="width: 20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">COVID-19 Status
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboCovidStatus" runat="server" Width="304px">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td style="width: 60px"></td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%">
                                    <tr id="trLoka" runat="server">
                                        <td>
                                            <table width="100%">
                                                <tr>
                                                    <td class="labelcaption">
                                                        <asp:Label ID="Label1" runat="server" Text="Lokadok"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="50%" style="vertical-align: top">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td class="label">
                                                                            <asp:Label ID="Label3" runat="server" Text="Date Appointment"></asp:Label>
                                                                        </td>
                                                                        <td class="entry">
                                                                            <telerik:RadDatePicker ID="rdpApptLokaDate" runat="server" Width="100px" />
                                                                        </td>
                                                                        <td style="width: 20px"></td>
                                                                        <td>
                                                                            <asp:HiddenField ID="hf_appt_id" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="label">
                                                                            <asp:Label ID="Label4" runat="server" Text="Notes"></asp:Label>
                                                                        </td>
                                                                        <td class="entry">
                                                                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" TextMode="MultiLine" />
                                                                        </td>
                                                                        <td style="width: 20px"></td>
                                                                        <td></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td width="50%" style="vertical-align: top"></td>
                                                        </tr>
                                                    </table>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td style="width: 50%; vertical-align: top">
                            <fieldset>
                                <legend>
                                    <asp:Label ID="Label2" runat="server" Text="PATIENT CONTROL PLAN (APPOINTMENT)" Font-Bold="True" Font-Size="9"></asp:Label>
                                </legend>
                                <table style="width: 100%">
                                    <tr>
                                        <td width="100%" style="vertical-align: top">
                                            <table width="100%">
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblAppointmentDate" runat="server" Text="Appointment Date"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <table cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td>
                                                                    <telerik:RadDatePicker ID="txtAppointmentDate" runat="server" Width="105px">
                                                                    </telerik:RadDatePicker>
                                                                </td>
                                                                <td>&nbsp;
                                                                </td>
                                                                <td>
                                                                    <telerik:RadMaskedTextBox ID="txtAppointmentDateTime" runat="server" Mask="<00..23>:<00..59>"
                                                                        PromptChar="_" RoundNumericRanges="false" Width="50px" Visible="False">
                                                                    </telerik:RadMaskedTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                                                    </td>
                                                    <td class="entry2Column">
                                                        <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AutoPostBack="true"
                                                            OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged" AllowCustomText="true"
                                                            Filter="Contains">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                                                    </td>
                                                    <td class="entry2Column">
                                                        <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="300px" AllowCustomText="true"
                                                            Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboParamedicID_SelectedIndexChanged">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblRoomID" runat="server" Text="Room"></asp:Label>
                                                    </td>
                                                    <td class="entry2Column">
                                                        <telerik:RadComboBox ID="cboRoomID" runat="server" Width="300px" />
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblQueNo" runat="server" Text="Que No" />
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboQue" runat="server" Width="300px" />
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblAppNotes" runat="server" Text="Notes"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadTextBox ID="txtAppNotes" runat="server" Width="300px" MaxLength="4000" TextMode="MultiLine" />
                                                    </td>
                                                    <td width="20px"></td>
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
            </td>
        </tr>
    </table>
</asp:Content>
