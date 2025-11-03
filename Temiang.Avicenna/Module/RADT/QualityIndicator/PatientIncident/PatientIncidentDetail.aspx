<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="PatientIncidentDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.PatientIncidentDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">
            function showIncidentType() {
                var oWnd = $find("<%= winIncidentType.ClientID %>");
                var pi = $find("<%= txtPatientIncidentNo.ClientID %>");

                oWnd.setUrl('PatientIncidentComponentTypePickList.aspx?piNo=' + pi.get_value());
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function deleteAllRow() {

            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.id != null)
                    __doPostBack("<%= grdIncidentComponentType.UniqueID %>", oWnd.argument.id);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="600px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="onClientClose"
        ID="winIncidentType">
    </telerik:RadWindow>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%" border="0">
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkNonPatient" runat="server" Text="Non Patient" OnCheckedChanged="chkNonPatient_OnCheckedChanged"
                                AutoPostBack="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientIncident" runat="server" Text="Incident No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPatientIncidentNo" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvPatientIncident" runat="server" ErrorMessage="Incident No required."
                                ValidationGroup="entry" ControlToValidate="txtPatientIncidentNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
                <asp:Panel ID="pnlPatient" runat="server">
                    <table width="100%" border="0">
                        <tr>
                            <td colspan="4">&nbsp;<b>Patient Identity</b>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="Registration No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboRegistrationNo" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" OnItemsRequested="cboRegistrationNo_ItemsRequested"
                                    OnItemDataBound="cboRegistrationNo_ItemDataBound" OnSelectedIndexChanged="cboRegistrationNo_SelectedIndexChanged"
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
                                        <%# DataBinder.Eval(Container.DataItem, "BedID")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 5 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label4" runat="server" Text="Patient Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="100"
                                    ReadOnly="True" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label2" runat="server" Text="Medical Record No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboMedicalNo" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboMedicalNo_ItemsRequested"
                                    OnItemDataBound="cboMedicalNo_ItemDataBound" OnSelectedIndexChanged="cboMedicalNo_SelectedIndexChanged"
                                    AutoPostBack="True">
                                    <ItemTemplate>
                                        <b>
                                            <%# DataBinder.Eval(Container.DataItem, "MedicalNo")%>
                                        </b>
                                        <br />
                                        <%# DataBinder.Eval(Container.DataItem, "PatientName")%>
                                        <br />
                                        <%# DataBinder.Eval(Container.DataItem, "PatientID") %>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 5 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20"></td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvNORM" runat="server" ErrorMessage="Medical Record No required."
                                    ValidationGroup="entry" ControlToValidate="cboMedicalNo" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblInitialName" runat="server" Text="Initial Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtInitialName" runat="server" Width="300px" MaxLength="50" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label3" runat="server" Text="Age / Gender"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadTextBox ID="txtAge" runat="server" Width="120px" ReadOnly="True" />
                                        </td>
                                        <td style="width: 5px"></td>
                                        <td>
                                            <telerik:RadTextBox ID="txtSex" runat="server" Width="50px" ReadOnly="True" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label10" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                                    Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged" />
                            </td>
                            <td width="20">
                                <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                                    ValidationGroup="entry" ControlToValidate="cboServiceUnitID" SetFocusOnError="True"
                                    Width="100%" Visible="False">
                                    <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label11" runat="server" Text="Room / Bed"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadComboBox ID="cboRoomID" runat="server" Width="196px" AutoPostBack="True"
                                                OnSelectedIndexChanged="cboRoomID_SelectedIndexChanged" />
                                        </td>
                                        <td style="width: 5px"></td>
                                        <td>
                                            <telerik:RadComboBox ID="cboBedID" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" OnItemsRequested="cboParamedicID_ItemsRequested"
                                    OnItemDataBound="cboParamedicID_ItemDataBound">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "ParamedicName")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 10 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlNonPatient" runat="server">
                    <table width="100%" border="0">
                        <tr>
                            <td colspan="4">&nbsp;<b>Identity</b>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label13" runat="server" Text="First Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtFirstName" runat="server" Width="300px" MaxLength="100" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label5" runat="server" Text="Middle Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtMiddleName" runat="server" Width="300px" MaxLength="100" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label12" runat="server" Text="Last Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtLastName" runat="server" Width="300px" MaxLength="100" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label14" runat="server" Text="Date Of Birth"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtDateOfBirth" runat="server" Width="100px" OnSelectedDateChanged="txtDateOfBirth_SelectedDateChanged"
                                    AutoPostBack="True">
                                </telerik:RadDatePicker>
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label15" runat="server" Text="Age / Gender"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtAgeNonPat" runat="server" Width="100px" MaxLength="100"
                                    ReadOnly="True" />
                                <asp:RadioButtonList ID="rbtSexNonPat" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Value="M" Text="Male" />
                                    <asp:ListItem Value="F" Text="Female" />
                                </asp:RadioButtonList>
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%" border="0">
                    <tr>
                        <td>&nbsp;<b>Incident Information</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReportedBy" runat="server" Text="Reported By"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtReportedBy" runat="server" Width="300px" MaxLength="100"
                                ReadOnly="True" />
                        </td>
                        <td width="20"></td>
                        <asp:RequiredFieldValidator ID="rfvReportedBy" runat="server" ErrorMessage="Reported By required."
                            ValidationGroup="entry" ControlToValidate="cboServiceUnitID" SetFocusOnError="True"
                            Width="100%" Visible="False">
                            <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label6" runat="server" Text="Reported By Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnitIDInCharge" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvServiceUnitInCharge" runat="server" ErrorMessage="Reported by unit required."
                                ValidationGroup="entry" ControlToValidate="cboServiceUnitIDInCharge" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trServiceUnitIncidentLocationID" visible="False">
                        <td class="label">
                            <asp:Label ID="lblServiceUnitIncidentLocationID" runat="server" Text="Incident Location"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnitIncidentLocationID" runat="server" Width="300px"
                                AllowCustomText="true" Filter="Contains" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvServiceUnitIncidentLocationID" runat="server" ErrorMessage="Incident Location required."
                                ValidationGroup="entry" ControlToValidate="cboServiceUnitIDInCharge" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblIncidentLocation" runat="server" Text="Incident Location"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtIncidentLocation" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvIncidentLocation" runat="server" ErrorMessage="Incident location required."
                                ValidationGroup="entry" ControlToValidate="txtIncidentLocation" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label7" runat="server" Text="Incident Date Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtIncidentDate" runat="server" Width="100px" OnSelectedDateChanged="txtIncidentDate_SelectedDateChanged"
                                            AutoPostBack="True">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        <telerik:RadTimePicker ID="txtIncidentTime" runat="server" Width="80px" TimeView-Interval="00:30"
                                            TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                            TimeView-Columns="4" TimeView-StartTime="07:00" TimeView-EndTime="22:00" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvIncidentDate" runat="server" ErrorMessage="Incident date required."
                                ValidationGroup="entry" ControlToValidate="txtIncidentDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvIncidentTime" runat="server" ErrorMessage="Incident time required."
                                ValidationGroup="entry" ControlToValidate="txtIncidentTime" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label8" runat="server" Text="Reporting Date Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtReportingDate" runat="server" Width="100px">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        <telerik:RadTimePicker ID="txtReportingTime" runat="server" Width="80px" TimeView-Interval="00:30"
                                            TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                            TimeView-Columns="4" TimeView-StartTime="07:00" TimeView-EndTime="22:00" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvReportingDate" runat="server" ErrorMessage="Reporting date required."
                                ValidationGroup="entry" ControlToValidate="txtReportingDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvReportingTime" runat="server" ErrorMessage="Reporting time required."
                                ValidationGroup="entry" ControlToValidate="txtReportingTime" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image14" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRIncidentOccurredOn" runat="server" Text="Occurred On"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRIncidentOccurredOn" runat="server" Width="300px" OnSelectedIndexChanged="cboSRIncidentOccurredOn_SelectedIndexChanged"
                                AutoPostBack="True" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRIncidentOccurredOn" runat="server" ErrorMessage="Occurred on required."
                                ValidationGroup="entry" ControlToValidate="cboSRIncidentOccurredOn" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblIncidentOccurredOnName" runat="server" Text="Occurred On Description"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtIncidentOccurredOnName" runat="server" Width="300px" MaxLength="250"
                                Enabled="False" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRIncidentOccurredInPatientsWith" runat="server" Text="Occurred In Patients With"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRIncidentOccurredInPatientsWith" runat="server" Width="300px"
                                OnSelectedIndexChanged="cboSRIncidentOccurredInPatientsWith_SelectedIndexChanged"
                                AutoPostBack="True" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRIncidentOccurredInPatientsWith" runat="server" ErrorMessage="Occurred In Patients With required."
                                ValidationGroup="entry" ControlToValidate="cboSRIncidentOccurredInPatientsWith" SetFocusOnError="True"
                                Width="100%" Visible="False">
                                <asp:Image ID="Image16" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblIncidentOccurredInPatientsWithName" runat="server" Text="Occurred In Patients With Description"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtIncidentOccurredInPatientsWithName" runat="server" Width="300px"
                                MaxLength="250" Enabled="False" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Incident Detail Information" PageViewID="pgvDetail"
                Selected="true" />
            <telerik:RadTab runat="server" Text="Investigation Units" PageViewID="pgvRelatedUnit" />
            <telerik:RadTab runat="server" Text="Incident Verification" PageViewID="pgvVerif" />
            <telerik:RadTab runat="server" Text="Cause Analysis" PageViewID="pgvCause" />
            <telerik:RadTab runat="server" Text="Safety Goals" PageViewID="pgvSafetyGoals" Visible="False" />
            <telerik:RadTab runat="server" Text="KTD" PageViewID="pgvKtd" Visible="False" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvDetail" runat="server">
            <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%" border="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblIncidentName" runat="server" Text="Incident"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtIncidentName" runat="server" Width="100%" TextMode="MultiLine" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvIncidentName" runat="server" ErrorMessage="Incident required."
                                        ValidationGroup="entry" ControlToValidate="txtIncidentName" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblChronology" runat="server" Text="Chronology"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtChronology" runat="server" Width="100%" TextMode="MultiLine"
                                        Height="130px" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvChronology" runat="server" ErrorMessage="Chronology required."
                                        ValidationGroup="entry" ControlToValidate="txtChronology" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image17" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr style="display: none">
                                <td class="label"></td>
                                <td>
                                    <asp:CheckBox ID="chkIsApproved" Text="Approved" runat="server" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td colspan="4">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="width: 50%; vertical-align: top;">
                                                <table width="100%">
                                                </table>
                                            </td>
                                            <td style="width: 50%; vertical-align: top;"></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblIncidentGroup" runat="server" Text="Incident Group"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRIncidentGroup" runat="server" Width="300px" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvSrIncidentGroup" runat="server" ErrorMessage="Incident group required."
                                        ValidationGroup="entry" ControlToValidate="cboSRIncidentGroup" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblClinicalImpact" runat="server" Text="Clinical Impact"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRClinicalImpact" runat="server" Width="300px" OnSelectedIndexChanged="cboSRClinicalImpact_SelectedIndexChanged"
                                        AutoPostBack="True" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvSrClinicalImpact" runat="server" ErrorMessage="Clinical impact required."
                                        ValidationGroup="entry" ControlToValidate="cboSRClinicalImpact" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblProbablyFrequency" runat="server" Text="Probability Frequency"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRProbabilityFrequency" runat="server" Width="300px"
                                        OnSelectedIndexChanged="cboSRProbabilityFrequency_SelectedIndexChanged" AutoPostBack="True" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSrFollowUp" runat="server" Text="Follow Up"></asp:Label>
                                </td>
                                <td class="entry" colspan="3">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadComboBox ID="cboSRIncidentFollowUp" runat="server" Width="300px" />
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="lblRiskGradingName" runat="server" Font-Italic="True" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblHandling" runat="server" Text="Handling"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtHandling" runat="server" Width="100%" TextMode="MultiLine" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label9" runat="server" Text="Handled By"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRHandledBy" runat="server" Width="300px" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvRelatedUnit" runat="server">
            <telerik:RadGrid ID="grdRelatedUnit" runat="server" OnNeedDataSource="grdRelatedUnit_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdRelatedUnit_DeleteCommand"
                OnInsertCommand="grdRelatedUnit_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="ServiceUnitID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ServiceUnitID" HeaderText="ID"
                            UniqueName="ServiceUnitID" SortExpression="ServiceUnitID">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitID"
                            SortExpression="ServiceUnitID">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="PatientIncidentRelatedUnitItemDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="grdRelatedUnitEditCommand">
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
        <telerik:RadPageView ID="pgvVerif" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label"></td>
                                <td>
                                    <asp:CheckBox ID="chkIsOccurInOtherUnits" Text="Occur In Other Units" runat="server"
                                        OnCheckedChanged="chkIsOccurInOtherUnits_CheckedChanged" AutoPostBack="true" />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOccurInOtherUnitsNotes" runat="server" Text="Occur In Other Units Notes"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtOccurInOtherUnitsNotes" runat="server" Width="300px" MaxLength="250"
                                        Enabled="False" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdIncidentComponentType" runat="server" OnNeedDataSource="grdIncidentComponentType_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdIncidentComponentType_UpdateCommand"
                OnDeleteCommand="grdIncidentComponentType_DeleteCommand" OnInsertCommand="grdIncidentComponentType_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="SRIncidentType,ComponentID,SubComponentID">
                    <CommandItemTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 10px;">&nbsp;
                                </td>
                                <td style="width: 500px;">
                                    <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# !grdIncidentComponentType.MasterTableView.IsItemInserted %>'>
                                        <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/insert16.png" />
                                        &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new record"></asp:Label>
                                    </asp:LinkButton>&nbsp;&nbsp;
                                    <asp:LinkButton ID="lbPickList" runat="server" Visible='<%# !grdIncidentComponentType.MasterTableView.IsItemInserted %>'
                                        OnClientClick="javascript:showIncidentType();return false;">
                                        <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/views16.png" />&nbsp;<asp:Label
                                            runat="server" ID="lblPickList" Text="Picklist Incident Type"></asp:Label>
                                    </asp:LinkButton>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td style="width: 90px;">
                                    <asp:LinkButton ID="lbDeleteAll" runat="server" Visible='<%# !grdIncidentComponentType.MasterTableView.IsItemInserted %>'
                                        OnClientClick="javascript:return confirm('Are You Sure Want To Delete All Row?');" OnClick="lbDeleteAll_OnClick">
                                        <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/delete16.png" />&nbsp;<asp:Label
                                            runat="server" ID="Label1" Text="Delete All"></asp:Label>
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </CommandItemTemplate>
                    <CommandItemStyle Height="29px" />
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="IncidentType" HeaderText="Incident Type" UniqueName="IncidentType"
                            SortExpression="IncidentType">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ComponentName" HeaderText="Component" UniqueName="ComponentName"
                            SortExpression="ComponentName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SubComponent" HeaderText="Sub Component" UniqueName="SubComponent"
                            SortExpression="SubComponent">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SubComponentName" HeaderText="Description" UniqueName="SubComponentName"
                            SortExpression="SubComponentName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Modus" HeaderText="Modus" UniqueName="Modus"
                            SortExpression="Modus">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="PatientIncidentComponentTypeItemDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="grdIncidentComponentTypeEditCommand">
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
        <telerik:RadPageView runat="server" ID="pgvCause">
            <telerik:RadGrid ID="grdCauseAnalysis" runat="server" AutoGenerateColumns="False"
                GridLines="None" OnNeedDataSource="grdCauseAnalysis_NeedDataSource">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="SRIncidentCauseAnalysis">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkIsSelect" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                                    Checked='<%#DataBinder.Eval(Container.DataItem, "IsSelect") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SRIncidentCauseAnalysis" HeaderText="ID"
                            UniqueName="SRIncidentCauseAnalysis" SortExpression="SRIncidentCauseAnalysis" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="IncidentCauseAnalysis" HeaderText="Incident Cause Analysis"
                            UniqueName="IncidentCauseAnalysis" SortExpression="IncidentCauseAnalysis" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="500px" HeaderText="Notes" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" UniqueName="Notes">
                            <ItemTemplate>
                                <telerik:RadTextBox ID="txtNotes" runat="server" Width="450px" TextMode="MultiLine" Text='<%#Eval("Notes")%>' MaxLength="500"
                                    Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvSafetyGoals" runat="server">
            <telerik:RadGrid ID="grdSafetyGoals" runat="server" OnNeedDataSource="grdSafetyGoals_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdSafetyGoals_UpdateCommand"
                OnDeleteCommand="grdSafetyGoals_DeleteCommand" OnInsertCommand="grdSafetyGoals_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="SRSafetyGoals">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRSafetyGoals" HeaderText="ID"
                            UniqueName="SRSafetyGoals" SortExpression="SRSafetyGoals">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SafetyGoals" HeaderText="Safety Goal" UniqueName="SafetyGoals"
                            SortExpression="IncidentTypeName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Recommendation" HeaderText="Recommendation" UniqueName="Recommendation"
                            SortExpression="Recommendation">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="PatientIncidentSafetyGoalsItemDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="grdSafetyGoalsEditCommand">
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
        <telerik:RadPageView ID="pgvKtd" runat="server">
            <telerik:RadGrid ID="grdKtd" runat="server" OnNeedDataSource="grdKtd_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdKtd_UpdateCommand"
                OnDeleteCommand="grdKtd_DeleteCommand" OnInsertCommand="grdKtd_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="SRIncidentKTD">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRIncidentKTD" HeaderText="ID"
                            UniqueName="SRIncidentKTD" SortExpression="SRIncidentKTD">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IncidentKTD" HeaderText="KTD" UniqueName="SRIncidentKTD"
                            SortExpression="SRIncidentKTD">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="PatientIncidentKtdItemDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="grdKtdEditCommand">
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
    </telerik:RadMultiPage>
</asp:Content>
