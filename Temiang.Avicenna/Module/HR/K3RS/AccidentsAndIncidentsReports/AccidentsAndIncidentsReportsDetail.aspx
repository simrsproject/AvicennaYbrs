<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="AccidentsAndIncidentsReportsDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.K3RS.AccidentsAndIncidentsReportsDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%" cellpadding="0" cellspacing="1">
        <tr>
            <td>
                <fieldset>
                    <legend><b>INCIDENT</b></legend>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20" ReadOnly="true" />
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvTransactionNo" runat="server" ErrorMessage="Transaction No required."
                                                ValidationGroup="entry" ControlToValidate="txtTransactionNo" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblIncidentDate" runat="server" Text="Incident Date / Time"></asp:Label>
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
                                            <asp:Label ID="Label8" runat="server" Text="Reporting Date / Time"></asp:Label>
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
                                    <tr style="display: none">
                                        <td class="label"></td>
                                        <td>
                                            <asp:CheckBox ID="chkIsApproved" Text="Approved" runat="server" />
                                            <asp:CheckBox ID="chkIsVoid" Text="Void" runat="server" />
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblChronology" runat="server" Text="Chronological Events"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtChronology" runat="server" Width="300px" TextMode="MultiLine" MaxLength="500" />
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
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblAspectsOfTheCause" runat="server" Text="Aspects Of The Cause"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtAspectsOfTheCause" runat="server" Width="300px" TextMode="MultiLine" MaxLength="500" />
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvAspectsOfTheCause" runat="server" ErrorMessage="Aspects Of The Cause required."
                                                ValidationGroup="entry" ControlToValidate="txtAspectsOfTheCause" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
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
    <table style="width: 100%" cellpadding="0" cellspacing="1">
        <tr>
            <td>
                <fieldset>
                    <legend><b>VICTIM</b></legend>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <fieldset>
                                                <legend><b>VICTIM IDENTITY</b></legend>
                                                <table width="100%">
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblPersonID" runat="server" Text="Employee Name"></asp:Label>
                                                        </td>
                                                        <td class="entry">
                                                            <telerik:RadComboBox ID="cboPersonID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="PersonID_ItemDataBound"
                                                                OnItemsRequested="PersonID_ItemsRequested" AutoPostBack="True" OnSelectedIndexChanged="cboPersonID_SelectedIndexChanged">
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber")%>
                                                                    &nbsp;-&nbsp;
                                                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    Note : Show max 20 items
                                                                </FooterTemplate>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td width="20px"></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblEmployeeNumber" runat="server" Text="Employee Number"></asp:Label>
                                                        </td>
                                                        <td class="entry">
                                                            <telerik:RadTextBox ID="txtEmployeeNumber" runat="server" Width="300px" ReadOnly="true" />
                                                        </td>
                                                        <td width="20"></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblDateOfBirth" runat="server" Text="Date Of Birth"></asp:Label>
                                                        </td>
                                                        <td class="entry">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <telerik:RadDatePicker ID="txtDateOfBirth" runat="server" Width="100px" DateInput-ReadOnly="true"
                                                                            DatePopupButton-Enabled="false">
                                                                        </telerik:RadDatePicker>
                                                                    </td>
                                                                    <td></td>
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
                                                        <td class="entry">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <telerik:RadNumericTextBox ID="txtAgeInYear" runat="server" Width="40px" ReadOnly="true">
                                                                            <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </td>
                                                                    <td>&nbsp;Y&nbsp;&nbsp;</td>
                                                                    <td>
                                                                        <telerik:RadNumericTextBox ID="txtAgeInMonth" runat="server" Width="40px" ReadOnly="true">
                                                                            <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </td>
                                                                    <td>&nbsp;M&nbsp;&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadNumericTextBox ID="txtAgeInDay" runat="server" Width="40px" ReadOnly="true">
                                                                            <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                                                        </telerik:RadNumericTextBox>
                                                                    </td>
                                                                    <td>&nbsp;D&nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td width="20"></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblSex" runat="server" Text="Sex"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadTextBox ID="txtSex" runat="server" Width="300px" ReadOnly="True" />
                                                        </td>
                                                        <td width="20"></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblPositionID" runat="server" Text="Position"></asp:Label>
                                                        </td>
                                                        <td class="entry">
                                                            <telerik:RadComboBox ID="cboPositionID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboPositionID_ItemDataBound"
                                                                OnItemsRequested="cboPositionID_ItemsRequested">
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "PositionCode")%>
                                                                &nbsp;-&nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "PositionName")%>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    Note : Show max 20 items
                                                                </FooterTemplate>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td width="20"></td>
                                                        <td></td>
                                                    </tr>

                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <fieldset>
                                                <legend><b>INCIDENT RECORDS</b></legend>
                                                <table width="100%">
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblSREmployeeIncidentType" runat="server" Text="Incident Type"></asp:Label>
                                                        </td>
                                                        <td class="entry">
                                                            <telerik:RadComboBox ID="cboSREmployeeIncidentType" runat="server" Width="300px" AllowCustomText="true"
                                                                Filter="Contains" />
                                                        </td>
                                                        <td width="20">
                                                            <asp:RequiredFieldValidator ID="rfvSREmployeeIncidentType" runat="server" ErrorMessage="Incident Type required."
                                                                ValidationGroup="entry" ControlToValidate="cboSREmployeeIncidentType" SetFocusOnError="True"
                                                                Width="100%">
                                                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblSRNeedleType" runat="server" Text="Needle Type"></asp:Label>
                                                        </td>
                                                        <td class="entry">
                                                            <telerik:RadComboBox ID="cboSRNeedleType" runat="server" Width="300px" AllowCustomText="true"
                                                                Filter="Contains" />
                                                        </td>
                                                        <td width="20"></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblSREmployeeIncidentStatus" runat="server" Text="Incident Status"></asp:Label>
                                                        </td>
                                                        <td class="entry">
                                                            <telerik:RadComboBox ID="cboSREmployeeIncidentStatus" runat="server" Width="300px" AllowCustomText="true"
                                                                Filter="Contains" />
                                                        </td>
                                                        <td width="20">
                                                            <asp:RequiredFieldValidator ID="rfvSREmployeeIncidentStatus" runat="server" ErrorMessage="Incident Status required."
                                                                ValidationGroup="entry" ControlToValidate="cboSREmployeeIncidentStatus" SetFocusOnError="True"
                                                                Width="100%">
                                                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblSREmployeeInjuryCategory" runat="server" Text="Injury Category"></asp:Label>
                                                        </td>
                                                        <td class="entry">
                                                            <telerik:RadComboBox ID="cboSREmployeeInjuryCategory" runat="server" Width="300px" AllowCustomText="true"
                                                                Filter="Contains" />
                                                        </td>
                                                        <td width="20">
                                                            <asp:RequiredFieldValidator ID="rfvSREmployeeInjuryCategory" runat="server" ErrorMessage="Injury Category required."
                                                                ValidationGroup="entry" ControlToValidate="cboSREmployeeInjuryCategory" SetFocusOnError="True"
                                                                Width="100%">
                                                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblLossTime" runat="server" Text="Loss Time"></asp:Label>
                                                        </td>
                                                        <td class="entry">
                                                            <telerik:RadNumericTextBox ID="txtLossTime" runat="server" Width="50px">
                                                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                                            </telerik:RadNumericTextBox>
                                                            Day(s)
                                                        </td>
                                                        <td width="20">
                                                            <asp:RequiredFieldValidator ID="rfvLossTime" runat="server" ErrorMessage="Loss Time required."
                                                                ValidationGroup="entry" ControlToValidate="txtLossTime" SetFocusOnError="True"
                                                                Width="100%">
                                                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblInjuredLocation" runat="server" Text="Injury Location"></asp:Label>
                                                        </td>
                                                        <td class="entry">
                                                            <telerik:RadTextBox ID="txtInjuredLocation" runat="server" Width="300px" MaxLength="100" />
                                                        </td>
                                                        <td width="20">
                                                            <asp:RequiredFieldValidator ID="rfvInjuredLocation" runat="server" ErrorMessage="Injury Location required."
                                                                ValidationGroup="entry" ControlToValidate="txtInjuredLocation" SetFocusOnError="True"
                                                                Width="100%">
                                                                <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblPlaceOfTreatment" runat="server" Text="Place Of Treatment"></asp:Label>
                                                        </td>
                                                        <td class="entry">
                                                            <telerik:RadTextBox ID="txtPlaceOfTreatment" runat="server" Width="300px" MaxLength="100" />
                                                        </td>
                                                        <td width="20">
                                                            <asp:RequiredFieldValidator ID="rfvPlaceOfTreatment" runat="server" ErrorMessage="Place Of Treatment required."
                                                                ValidationGroup="entry" ControlToValidate="txtPlaceOfTreatment" SetFocusOnError="True"
                                                                Width="100%">
                                                                <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <table style="width: 100%" cellpadding="0" cellspacing="1">
        <tr>
            <td>
                <fieldset>
                    <legend><b>INVESTIGATION</b></legend>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <fieldset>
                                                <legend><b>DIRECT CAUSE</b></legend>
                                                <table cellpadding="1" cellspacing="2" width="100%">
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblUnsafeCondition" runat="server" Text="Unsafe Condition"></asp:Label>
                                                        </td>
                                                        <td class="entry">
                                                            <telerik:RadTextBox ID="txtUnsafeCondition" runat="server" Width="300px" TextMode="MultiLine" MaxLength="500" />
                                                        </td>
                                                        <td width="20"></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblUnsafeAct" runat="server" Text="Unsafe Act"></asp:Label>
                                                        </td>
                                                        <td class="entry">
                                                            <telerik:RadTextBox ID="txtUnsafeAct" runat="server" Width="300px" TextMode="MultiLine" MaxLength="500" />
                                                        </td>
                                                        <td width="20"></td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <fieldset>
                                                <legend><b>INDIRECT CAUSE</b></legend>
                                                <table cellpadding="1" cellspacing="2" width="100%">
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblPersonalIndirectCause" runat="server" Text="Personal"></asp:Label>
                                                        </td>
                                                        <td class="entry">
                                                            <telerik:RadTextBox ID="txtPersonalIndirectCause" runat="server" Width="300px" TextMode="MultiLine" MaxLength="500" />
                                                        </td>
                                                        <td width="20"></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblWorkingIndirectCause" runat="server" Text="Working"></asp:Label>
                                                        </td>
                                                        <td class="entry">
                                                            <telerik:RadTextBox ID="txtWorkingIndirectCause" runat="server" Width="300px" TextMode="MultiLine" MaxLength="500" />
                                                        </td>
                                                        <td width="20"></td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table>
                    <tr>
                        <td style="width: 50%; vertical-align: top">
                            <table width="100%">
                                <tr runat="server" id="trEmployeeAccidentReportStatus">
                                    <td class="label">
                                        <asp:Label ID="lblSREmployeeAccidentReportStatus" runat="server" Text="Report Status"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboSREmployeeAccidentReportStatus" runat="server" Width="300px" AllowCustomText="true"
                                            Filter="Contains" />
                                    </td>
                                    <td width="20">
                                        <asp:RequiredFieldValidator ID="rfvSREmployeeAccidentReportStatus" runat="server" ErrorMessage="Report Status required."
                                            ValidationGroup="entry" ControlToValidate="cboSREmployeeAccidentReportStatus" SetFocusOnError="True"
                                            Width="100%">
                                            <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 50%; vertical-align: top"></td>
                    </tr>
                </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <table style="width: 100%" cellpadding="0" cellspacing="1">
        <tr>
            <td>
                <fieldset>
                    <legend><b>PREVENTION</b></legend>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 33%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblActionPlan" runat="server" Text="Action Plan"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtActionPlan" runat="server" Width="300px" TextMode="MultiLine" MaxLength="500" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 33%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblTarget" runat="server" Text="Target"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtTarget" runat="server" Width="300px" TextMode="MultiLine" MaxLength="500" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 34%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblAuthority" runat="server" Text="Authority"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtAuthority" runat="server" Width="300px" TextMode="MultiLine" MaxLength="500" />
                                        </td>
                                        <td width="20"></td>
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
</asp:Content>
