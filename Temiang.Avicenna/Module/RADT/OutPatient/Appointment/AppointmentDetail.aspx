<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="AppointmentDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.OutPatient.AppointmentDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/CustomControl/AddressCtl.ascx" TagName="AddressCtl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">

        <script type="text/javascript">
        <!--
    function openWindowSelectTime() {
        var oWnd = $find("<%= winSelectTime.ClientID %>");
        var cboParamedicID = $find("<%= txtParamedicID.ClientID %>");
        var cboServiceUnitID = $find("<%= cboServiceUnitID.ClientID %>");
        var appDate = $find("<%= txtAppointmentDateTime.ClientID %>");
                var pmID = cboParamedicID.get_value();
                var suID = cboServiceUnitID._value;

                var date = appDate.get_selectedDate().format('DD/MM/YYYY');
                var url = "AppointmentSelectTime.aspx?pmID=" + pmID + "&suID=" + suID + "&pDate=" + date;
                oWnd.setUrl(url);
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onWindowSelectTimeClientClose(oWnd) {
                //Jika apply di click
                var arg = oWnd.argument;
                if (arg) {
                    var ajxPanel = $find("<%= AjaxPanel.ClientID %>");
                    ajxPanel.ajaxRequest('updateAppointment');
                }
                oWnd = null;
            }
            -->
        </script>

    </telerik:radcodeblock>
    <telerik:radwindow runat="server" animation="None" width="900px" height="600px" reloadonshow="true"
        showcontentduringload="False" visiblestatusbar="False" modal="true" behavior="Close"
        destroyonclose="false" onclientclose="onWindowSelectTimeClientClose" id="winSelectTime">
    </telerik:radwindow>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAppointmentNo" runat="server" Text="Appointment No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:radtextbox id="txtAppointmentNo" runat="server" width="300px" maxlength="20"
                                readonly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:radcombobox id="cboServiceUnitID" runat="server" width="300px" autopostback="true"
                                onselectedindexchanged="cboServiceUnitID_SelectedIndexChanged">
                            </telerik:radcombobox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Service Unit ID required."
                                ValidationGroup="entry" ControlToValidate="cboServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientID" runat="server" Text="Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:radcombobox id="cboPatientID" runat="server" width="300px" enableloadondemand="true"
                                            markfirstmatch="true" highlighttemplateditems="true" autopostback="true" onitemdatabound="cboPatientID_ItemDataBound"
                                            onitemsrequested="cboPatientID_ItemsRequested" onselectedindexchanged="cboPatientID_SelectedIndexChanged">
                                            <itemtemplate>
                                                <b>
                                                    <%# DataBinder.Eval(Container.DataItem, "PatientName")%>
                                                </b>&nbsp;-&nbsp;
                                                <%# System.Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth")).ToString(Temiang.Avicenna.Common.AppConstant.DisplayFormat.Date)%>
                                                <br />
                                                <%# DataBinder.Eval(Container.DataItem, "MedicalNo") %>
                                                &nbsp;|&nbsp;
                                                <%# DataBinder.Eval(Container.DataItem, "PatientID") %>
                                                <br />
                                                <%# DataBinder.Eval(Container.DataItem, "Address")%>
                                            </itemtemplate>
                                            <footertemplate>
                                                Note : Show max 10 items
                                            </footertemplate>
                                        </telerik:radcombobox>
                                    </td>
                                    <td style="width: 7px"></td>
                                    <td>
                                        <asp:LinkButton ID="lbClear" runat="server" OnClick="lbClear_Click" ToolTip="Clear Selected Patient">
                                            <img src="../../../../Images/cancel16.png" /></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:radtextbox id="txtMedicalNo" runat="server" width="100px" maxlength="15"
                                readonly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRSalutation" runat="server" Text="Salutation"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:radcombobox id="cboSRSalutation" runat="server" width="300px" autopostback="true"
                                onselectedindexchanged="cboSRSalutation_SelectedIndexChanged">
                            </telerik:radcombobox>
                        </td>
                        <td width="10px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFirstName" runat="server" Text="First Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:radtextbox id="txtFirstName" runat="server" width="300px">
                            </telerik:radtextbox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="First Name required."
                                ValidationGroup="entry" ControlToValidate="txtFirstName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:radtextbox id="txtMiddleName" runat="server" width="300px">
                            </telerik:radtextbox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblLastName" runat="server" Text="Last Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:radtextbox id="txtLastName" runat="server" width="300px">
                            </telerik:radtextbox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCityOfBirth" runat="server" Text="City / Date Of Birth"></asp:Label>
                        </td>
                        <td class="entry300" colspan="3">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 190px; vertical-align: top">
                                        <telerik:radtextbox id="txtCityOfBirth" runat="server" width="180px" maxlength="50">
                                        </telerik:radtextbox>
                                    </td>
                                    <td style="width: 10px; vertical-align: middle;">/
                                    </td>
                                    <td>
                                        <telerik:raddatepicker id="txtDateOfBirth" runat="server" width="100px" datepopupbutton-enabled="true">
                                        </telerik:raddatepicker>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="(Format: DD/MM/YYYY)" ForeColor="red" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%--<tr>
                        <td class="label">
                            <asp:Label ID="lblSex" runat="server" Text="Gender"></asp:Label>
                        </td>
                        <td class="entry300">
                            <asp:RadioButtonList ID="rbtSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="M" Text="Male" />
                                <asp:ListItem Value="F" Text="Female" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="10px">
                            <asp:RequiredFieldValidator ID="rfvSex" runat="server" ErrorMessage="Gender required."
                                ValidationGroup="entry" ControlToValidate="rbtSex" SetFocusOnError="True" Width="20px">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>--%>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSex" runat="server" Text="Gender"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboSRGenderType" runat="server" Width="300px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboSRGenderType_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="10px">
                            <asp:RequiredFieldValidator ID="rfvSRGenderType" runat="server" ErrorMessage="Gender required."
                                ValidationGroup="entry" ControlToValidate="cboSRGenderType" SetFocusOnError="True"
                                Width="100%" Visible="False">
                                <asp:Image ID="Image19" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSSN" runat="server" Text="SSN"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:radtextbox id="txtSSN" runat="server" width="300px" maxlength="50">
                            </telerik:radtextbox>
                        </td>
                        <td width="10px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                        </td>
                        <td class="entry2Column">
                            <telerik:radcombobox id="cboGuarantorID" runat="server" width="300px" highlighttemplateditems="True"
                                autopostback="True" markfirstmatch="false" enableloadondemand="true" nowrap="True"
                                onitemdatabound="cboGuarantorID_ItemDataBound" onitemsrequested="cboGuarantorID_ItemsRequested"
                                onselectedindexchanged="cboGuarantorID_SelectedIndexChanged">
                                <footertemplate>
                                    Note : Show max 30 result
                                </footertemplate>
                            </telerik:radcombobox>
                        </td>
                        <td style="width: 60px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:radtextbox id="txtParamedicID" runat="server" width="100px" readonly="true">
                            </telerik:radtextbox>
                            &nbsp;
                            <asp:Label ID="lblParamedicName" runat="server"></asp:Label>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvParamedicID" runat="server" ErrorMessage="Physician required."
                                ValidationGroup="entry" ControlToValidate="txtParamedicID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAppointmentDate" runat="server" Text="Appointment Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:raddatetimepicker id="txtAppointmentDateTime" runat="server" width="160px"
                                            dateinput-dateformat="dd/MM/yyyy HH:mm" dateinput-displaydateformat="dd/MM/yyyy HH:mm"
                                            dateinput-readonly="true" datepopupbutton-enabled="false" timepopupbutton-enabled="false">
                                        </telerik:raddatetimepicker>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSelectTime" runat="server" Text="Available Time" OnClientClick="javascript:openWindowSelectTime();return false;"
                                            Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvAppointmentDate" runat="server" ErrorMessage="Appointment Date required."
                                ValidationGroup="entry" ControlToValidate="txtAppointmentDateTime" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblVisitTypeID" runat="server" Text="Visit Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:radcombobox id="cboVisitTypeID" runat="server" width="300px">
                            </telerik:radcombobox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvVisitTypeID" runat="server" ErrorMessage="Visit Type ID required."
                                ValidationGroup="entry" ControlToValidate="cboVisitTypeID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblVisitDuration" runat="server" Text="Visit Duration"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:radnumerictextbox id="txtVisitDuration" runat="server" width="100px" minvalue="0"
                                numberformat-decimaldigits="0">
                            </telerik:radnumerictextbox>
                        </td>
                        <td width="40">
                            <asp:RequiredFieldValidator ID="rfvVisitDuration" runat="server" ErrorMessage="Visit Duration required."
                                ValidationGroup="entry" ControlToValidate="txtVisitDuration" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="rvVisitDuration" ValidationGroup="entry" ControlToValidate="txtVisitDuration"
                                MinimumValue="0" MaximumValue="1000" Type="Integer" SetFocusOnError="True" Display="Dynamic"
                                EnableClientScript="false" ErrorMessage="Visit Duration value invalid" runat="server"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RangeValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRAppointmentType" runat="server" Text="Appointment Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:radcombobox id="cboSRAppoinmentType" runat="server" width="300px" enableloadondemand="true"
                                markfirstmatch="true" highlighttemplateditems="true" autopostback="false" onitemdatabound="cboSRAppoinmentType_ItemDataBound"
                                onitemsrequested="cboSRAppoinmentType_ItemsRequested">
                                <itemtemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                                </itemtemplate>
                            </telerik:radcombobox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:radtextbox id="txtNotes" runat="server" width="300px" maxlength="2000" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblQueNo" runat="server" Text="Que No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:radtextbox id="txtQueNo" runat="server" width="50px" readonly="true" height="34px"
                                font-size="X-Large" font-bold="true" />
                            <telerik:radcombobox id="cboQueNo" runat="server" width="247px" enableloadondemand="true"
                                autopostback="true" markfirstmatch="true" highlighttemplateditems="true" onitemdatabound="cboQueNo_ItemDataBound"
                                onitemsrequested="cboQueNo_ItemsRequested" onselectedindexchanged="cboQueNo_SelectedIndexChanged"
                                font-bold="True" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Guarantor Card No
                        </td>
                        <td class="entry">
                            <telerik:radtextbox id="txtGuarantorCardNo" runat="server" width="300px" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Reference No
                        </td>
                        <td class="entry">
                            <telerik:radtextbox id="txtRefNo" runat="server" width="300px" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:radtabstrip id="tabStrip" runat="server" multipageid="multiPage" showbaseline="true"
        orientation="HorizontalTop">
        <tabs>
            <telerik:radtab runat="server" text="Address" pageviewid="pgAddress" selected="True" />
            <telerik:radtab runat="server" text="Follow Up" pageviewid="pgFollowUp" />
        </tabs>
    </telerik:radtabstrip>
    <telerik:radmultipage id="multiPage" runat="server" borderstyle="Solid" bordercolor="Gray">
        <telerik:radpageview id="pgAddress" runat="server" selected="true">
            <uc1:addressctl id="ctlAddress" runat="server" />
        </telerik:radpageview>
        <telerik:radpageview id="pgFollowUp" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50%" valign="top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRAppointmentStatus" runat="server" Text="Appointment Status"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:radcombobox id="cboSRAppointmentStatus" runat="server" width="300px" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPatientPIC" runat="server" Text="Patient PIC"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:radtextbox id="txtPatientPIC" runat="server" width="300px" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td width="50%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOfficerPIC" runat="server" Text="Follow Up Officer"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:radtextbox id="txtOfficerPIC" runat="server" width="300px" readonly="true" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblFollowUpDateTime" runat="server" Text="Follow Up Date Time"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:radtextbox id="txtFollowUpDateTime" runat="server" width="300px" readonly="true" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCreatedOfficer" runat="server" Text="Officer Created By"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:radtextbox id="txtCreatedOfficer" runat="server" width="300px" readonly="true" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCreatedDateTime" runat="server" Text="Created Date Time"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:radtextbox id="txtCreatedDateTime" runat="server" width="300px" readonly="true" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:radpageview>
    </telerik:radmultipage>
</asp:Content>
