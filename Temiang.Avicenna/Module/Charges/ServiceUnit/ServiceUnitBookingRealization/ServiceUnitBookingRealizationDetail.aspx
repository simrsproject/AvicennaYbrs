<%@ Page Title="Booking Schedule Detail" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="ServiceUnitBookingRealizationDetail.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Charges.ServiceUnitBookingRealizationDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboRoomBookingID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboPhysicianID" />
                    <telerik:AjaxUpdatedControl ControlID="cboPhysicianID2" />
                    <telerik:AjaxUpdatedControl ControlID="cboPhysicianID3" />
                    <telerik:AjaxUpdatedControl ControlID="cboPhysicianID4" />
                    <telerik:AjaxUpdatedControl ControlID="cboPhysicianIDAnestesi" />
                    <telerik:AjaxUpdatedControl ControlID="cboAssistantID1" />
                    <telerik:AjaxUpdatedControl ControlID="cboAssistantID2" />
                    <telerik:AjaxUpdatedControl ControlID="cboAssistantID3" />
                    <telerik:AjaxUpdatedControl ControlID="cboAssistantID4" />
                    <telerik:AjaxUpdatedControl ControlID="cboAssistantID5" />
                    <telerik:AjaxUpdatedControl ControlID="cboInstrumentatorID1" />
                    <telerik:AjaxUpdatedControl ControlID="cboInstrumentatorID2" />
                    <telerik:AjaxUpdatedControl ControlID="cboInstrumentatorID3" />
                    <telerik:AjaxUpdatedControl ControlID="cboInstrumentatorID4" />
                    <telerik:AjaxUpdatedControl ControlID="cboInstrumentatorID5" />
                    <telerik:AjaxUpdatedControl ControlID="cboAssistantIDAnestesi" />
                    <telerik:AjaxUpdatedControl ControlID="cboAssistantIDAnestesi2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtBookingDateFrom">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtBookingDateTo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtBookingTimeFrom">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtBookingTimeTo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboRegistrationNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboProcedureClassID" />
                    <telerik:AjaxUpdatedControl ControlID="txtGuarantorName" />
                    <telerik:AjaxUpdatedControl ControlID="txtGuarantorCardNo" />
                    <telerik:AjaxUpdatedControl ControlID="txtClass" />
                    <telerik:AjaxUpdatedControl ControlID="txtRoom" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtRealizationDateFrom">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtRealizationDateTo" />
                    <telerik:AjaxUpdatedControl ControlID="txtIncisionDate" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtRealizationTimeFrom">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtRealizationTimeTo" />
                    <telerik:AjaxUpdatedControl ControlID="txtIncisionTime" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rblNeedPa">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rblNeedPa" />
                    <telerik:AjaxUpdatedControl ControlID="txtPaDate" />
                    <telerik:AjaxUpdatedControl ControlID="txtSourceOfTissue" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdImplantInstallation">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdImplantInstallation" />
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 100%">
                <fieldset>
                    <legend>
                        <asp:Label ID="lblPatientDetail" runat="server" Text="PATIENT DETAIL" Font-Bold="True"
                            Font-Size="9"></asp:Label>
                    </legend>
                    <table style="width: 100%" cellpadding="1" cellspacing="1">
                        <tr>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboRegistrationNo" runat="server" Width="300px" EnableLoadOnDemand="true"
                                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboRegistrationNo_ItemsRequested"
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
                                                    <%# DataBinder.Eval(Container.DataItem, "RoomName")%>
                                                    &nbsp;-&nbsp;
                                                    <%# DataBinder.Eval(Container.DataItem, "BedID")%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Note : Show max 5 items
                                                </FooterTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td witdh="20px">
                                            <asp:RequiredFieldValidator ID="rfvRegistrationNo" runat="server" ErrorMessage="Registration No required."
                                                ValidationGroup="entry" ControlToValidate="cboRegistrationNo" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblPatientID" runat="server" Text="Patient ID"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtPatientID" runat="server" Width="100px" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="20"
                                                ReadOnly="true" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblGender" runat="server" Text="Gender"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboSRGenderType" runat="server" Width="300px" Enabled="false">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox runat="server" ID="txtYear" Width="50px" ReadOnly="true" />&nbsp;Y
                                            &nbsp;
                                            <telerik:RadTextBox runat="server" ID="txtMonth" Width="50px" ReadOnly="true" />&nbsp;M
                                            &nbsp;
                                            <telerik:RadTextBox runat="server" ID="txtDay" Width="50px" ReadOnly="true" />&nbsp;D
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtAddress" runat="server" Width="300px" ReadOnly="true"
                                                TextMode="MultiLine" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No / Mobile Phone No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtPhoneNo" runat="server" Width="147px" MaxLength="50" ReadOnly="true" />
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
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblGuarantor" runat="server" Text="Guarantor"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtGuarantorName" runat="server" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">Guarantor Card No
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtGuarantorCardNo" runat="server" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="Label18" runat="server" Text="Class / Room"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtClass" runat="server" Width="148px" ReadOnly="True" /></td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtRoom" runat="server" Width="150px" ReadOnly="True" /></td>
                                                </tr>
                                            </table>

                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align: top">
                                <fieldset id="FieldSet1" style="width: 150px; min-height: 150px;">
                                    <legend>Photo</legend>
                                    <asp:Image runat="server" ID="imgPatientPhoto" Width="150px" Height="150px" />
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="BOOKING DETAIL" Font-Bold="true" PageViewID="pgDetail" Selected="True" />
            <telerik:RadTab runat="server" Text="IMPLANT INSTALLATION" Font-Bold="true" PageViewID="pgImplant" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgDetail" runat="server" Selected="true">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 100%">
                        <fieldset>
                            <table style="width: 100%" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td style="width: 33%; vertical-align: top;">
                                        <table width="100%">
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblBookingNo" runat="server" Text="Booking No"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadTextBox ID="txtBookingNo" runat="server" Width="250px" MaxLength="20"
                                                        ReadOnly="true" />
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblBookingDateFrom" runat="server" Text="Booking From"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <telerik:RadDatePicker ID="txtBookingDateFrom" runat="server" Width="100px" AutoPostBack="true"
                                                                    OnSelectedDateChanged="txtBookingDateFrom_SelectedDateChanged">
                                                                </telerik:RadDatePicker>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="txtBookingTimeFrom" runat="server" TimeView-Interval="00:30"
                                                                    TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                                                    TimeView-Columns="4" TimeView-StartTime="00:00" TimeView-EndTime="23:59" AutoPostBack="true"
                                                                    OnSelectedDateChanged="txtBookingTimeFrom_SelectedDateChanged" Width="90px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="20px">
                                                    <asp:RequiredFieldValidator ID="rfvBookingDateFrom" runat="server" ErrorMessage="Booking Date From required."
                                                        ValidationGroup="entry" ControlToValidate="txtBookingDateFrom" SetFocusOnError="True"
                                                        Width="100%">
                                                        <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rfvBookingTimeFrom" runat="server" ErrorMessage="Booking Time From required."
                                                        ValidationGroup="entry" ControlToValidate="txtBookingTimeFrom" SetFocusOnError="True"
                                                        Width="100%">
                                                        <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblBookingDateTo" runat="server" Text="Booking To"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <telerik:RadDatePicker ID="txtBookingDateTo" runat="server" Width="100px">
                                                                </telerik:RadDatePicker>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="txtBookingTimeTo" runat="server" TimeView-Interval="00:30"
                                                                    TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                                                    TimeView-Columns="4" TimeView-StartTime="00:00" TimeView-EndTime="23:59" Width="90px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="20px">
                                                    <asp:RequiredFieldValidator ID="rfvBookingDateTo" runat="server" ErrorMessage="Booking Date To required."
                                                        ValidationGroup="entry" ControlToValidate="txtBookingDateTo" SetFocusOnError="True"
                                                        Width="100%">
                                                        <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rfvBookingTimeTo" runat="server" ErrorMessage="Booking Time To required."
                                                        ValidationGroup="entry" ControlToValidate="txtBookingTimeTo" SetFocusOnError="True"
                                                        Width="100%">
                                                        <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="Label1" runat="server" Text="Realization From"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <telerik:RadDatePicker ID="txtRealizationDateFrom" runat="server" Width="100px" AutoPostBack="true"
                                                                    OnSelectedDateChanged="txtRealizationDateFrom_SelectedDateChanged">
                                                                </telerik:RadDatePicker>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="txtRealizationTimeFrom" runat="server" TimeView-Interval="00:30"
                                                                    TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                                                    TimeView-Columns="4" TimeView-StartTime="00:00" TimeView-EndTime="23:59" AutoPostBack="true"
                                                                    OnSelectedDateChanged="txtRealizationTimeFrom_SelectedDateChanged" Width="90px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="20px">
                                                    <asp:RequiredFieldValidator ID="rfvRealizationDateFrom" runat="server" ErrorMessage="Realization Date From required."
                                                        ValidationGroup="entry" ControlToValidate="txtRealizationDateFrom" SetFocusOnError="True"
                                                        Width="100%">
                                                        <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rfvRealizationTimeFrom" runat="server" ErrorMessage="Realization Time From required."
                                                        ValidationGroup="entry" ControlToValidate="txtRealizationTimeFrom" SetFocusOnError="True"
                                                        Width="100%">
                                                        <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="Label2" runat="server" Text="Realization To"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <telerik:RadDatePicker ID="txtRealizationDateTo" runat="server" Width="100px">
                                                                </telerik:RadDatePicker>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="txtRealizationTimeTo" runat="server" TimeView-Interval="00:30"
                                                                    TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                                                    TimeView-Columns="4" TimeView-StartTime="00:00" TimeView-EndTime="23:59" Width="90px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="20px">
                                                    <asp:RequiredFieldValidator ID="rfvRealizationDateTo" runat="server" ErrorMessage="Realization Date To required."
                                                        ValidationGroup="entry" ControlToValidate="txtRealizationDateTo" SetFocusOnError="True"
                                                        Width="100%">
                                                        <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rfvRealizationTimeTo" runat="server" ErrorMessage="Realization Time To required."
                                                        ValidationGroup="entry" ControlToValidate="txtRealizationTimeTo" SetFocusOnError="True"
                                                        Width="100%">
                                                        <asp:Image ID="Image14" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr runat="server" id="trIncisionDateTime">
                                                <td class="label">
                                                    <asp:Label ID="lblIncisionDateTime" runat="server" Text="Incision Date/Time"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <telerik:RadDatePicker ID="txtIncisionDate" runat="server" Width="100px">
                                                                </telerik:RadDatePicker>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="txtIncisionTime" runat="server" TimeView-Interval="00:30"
                                                                    TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                                                    TimeView-Columns="4" TimeView-StartTime="00:00" TimeView-EndTime="23:59" Width="90px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblRoomBookingID" runat="server" Text="Room Booking"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadComboBox ID="cboRoomBookingID" runat="server" Width="250px" AllowCustomText="true" Filter="Contains" AutoPostBack="true"
                                                        OnSelectedIndexChanged="cboRoomBookingID_SelectedIndexChanged">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td width="20px">
                                                    <asp:RequiredFieldValidator ID="rfvRoomBookingID" runat="server" ErrorMessage="Room Booking required."
                                                        ValidationGroup="entry" ControlToValidate="cboRoomBookingID" SetFocusOnError="True"
                                                        Width="100%">
                                                        <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblDiagnose" runat="server" Text="Pre Diagnosis"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadTextBox ID="txtDiagnose" runat="server" Width="250px" MaxLength="100" Height="45px" TextMode="MultiLine" />
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblPostDiagnosis" runat="server" Text="Post Diagnosis"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadTextBox ID="txtPostDiagnosis" runat="server" Width="250px" MaxLength="100" Height="45px" TextMode="MultiLine" />
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                            <asp:Panel runat="server" ID="pnlDiagnoseType">
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="Label15" runat="server" Text="Diagnosis Type"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboSRProcedureDiagnoseType" runat="server" Width="250px" AllowCustomText="true"
                                                            Filter="Contains" />
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel runat="server" ID="pnlIndication">
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblSRIndication" runat="server" Text="Indication"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboSRIndication" runat="server" Width="250px" AllowCustomText="true"
                                                            Filter="Contains" />
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                            </asp:Panel>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblOperationCategory" runat="server" Text="Category"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadComboBox ID="cboOperationCategory" runat="server" Width="250px" AllowCustomText="true"
                                                        Filter="Contains" />
                                                </td>
                                                <td width="20px">
                                                    <asp:RequiredFieldValidator ID="rfvOperationCategory" runat="server" ErrorMessage="Category required."
                                                        ValidationGroup="entry" ControlToValidate="cboOperationCategory" SetFocusOnError="True"
                                                        Width="100%">
                                                        <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label"></td>
                                                <td class="entry">
                                                    <asp:RadioButtonList ID="rbtActionType" runat="server" RepeatDirection="Horizontal"
                                                        RepeatLayout="Flow">
                                                        <asp:ListItem Value="1" Text="Cito" />
                                                        <asp:ListItem Value="0" Text="Elective" />
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr id="pnlProcedureClass" runat="server">
                                                <td class="label">
                                                    <asp:Label ID="lblProcedureClassID" runat="server" Text="Charge Class"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadComboBox ID="cboProcedureClassID" runat="server" Width="250px" AllowCustomText="true"
                                                        Filter="Contains" />
                                                </td>
                                                <td width="20">
                                                    <asp:RequiredFieldValidator ID="rfvProcedureClassID" runat="server" ErrorMessage="Charge Class required."
                                                        ValidationGroup="entry" ControlToValidate="cboProcedureClassID" SetFocusOnError="True"
                                                        Width="100%">
                                                        <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr runat="server" id="trArrivedDate">
                                                <td class="label">
                                                    <asp:Label ID="Label16" runat="server" Text="Patient Arrived At"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <telerik:RadDatePicker ID="txtArrivedDate" runat="server" Width="100px">
                                                                </telerik:RadDatePicker>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTimePicker ID="txtArrivedTime" runat="server" TimeView-Interval="00:30"
                                                                    TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                                                    TimeView-Columns="4" TimeView-StartTime="00:00" TimeView-EndTime="23:59" Width="90px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 33%; vertical-align: top;">
                                        <table width="100%">
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblParamedicID" runat="server" Text="Surgeon #1"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadComboBox ID="cboPhysicianID" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                        MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboPhysicianID_ItemsRequested"
                                                        OnItemDataBound="cboPhysicianID_ItemDataBound">
                                                        <FooterTemplate>
                                                            Note : Show max 15 items
                                                        </FooterTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td width="20px">
                                                    <asp:RequiredFieldValidator ID="rfvPhysicianID" runat="server" ErrorMessage="Surgeon #1 required."
                                                        ValidationGroup="entry" ControlToValidate="cboPhysicianID" SetFocusOnError="True"
                                                        Width="100%">
                                                        <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblParamedicID2" runat="server" Text="Surgeon #2"></asp:Label>
                                                    <asp:LinkButton ID="btnSurgeonMore" runat="server" Text="more" OnClick="btnSurgeonMore_Clicked">
                                                    </asp:LinkButton>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadComboBox ID="cboPhysicianID2" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                        MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboPhysicianID2_ItemsRequested"
                                                        OnItemDataBound="cboPhysicianID_ItemDataBound">
                                                        <FooterTemplate>
                                                            Note : Show max 15 items
                                                        </FooterTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr id="trSurgeon3" runat="server" visible="false">
                                                <td class="label">
                                                    <asp:Label ID="lblParamedicID3" runat="server" Text="Surgeon #3"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadComboBox ID="cboPhysicianID3" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                        MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboPhysicianID3_ItemsRequested"
                                                        OnItemDataBound="cboPhysicianID_ItemDataBound">
                                                        <FooterTemplate>
                                                            Note : Show max 15 items
                                                        </FooterTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr id="trSurgeon4" runat="server" visible="false">
                                                <td class="label">
                                                    <asp:Label ID="lblParamedicID4" runat="server" Text="Surgeon #4"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadComboBox ID="cboPhysicianID4" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                        MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboPhysicianID4_ItemsRequested"
                                                        OnItemDataBound="cboPhysicianID_ItemDataBound">
                                                        <FooterTemplate>
                                                            Note : Show max 15 items
                                                        </FooterTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                            <asp:Panel runat="server" ID="pnlAssistantAndInstrumentator">
                                                <tr runat="server">
                                                    <td class="label">
                                                        <asp:Label ID="lblAssistantID1" runat="server" Text="Assistant #1"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboAssistantID1" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboAssistantID1_ItemsRequested"
                                                            OnItemDataBound="cboPhysicianID_ItemDataBound">
                                                            <FooterTemplate>
                                                                Note : Show max 15 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblAssistantID2" runat="server" Text="Assistant #2"></asp:Label>
                                                        <asp:LinkButton ID="btnAssistMore" runat="server" Text="more" OnClick="btnAssistMore_Clicked">
                                                        </asp:LinkButton>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboAssistantID2" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboAssistantID1_ItemsRequested"
                                                            OnItemDataBound="cboPhysicianID_ItemDataBound">
                                                            <FooterTemplate>
                                                                Note : Show max 15 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr id="trAssist3" runat="server" visible="false">
                                                    <td class="label">
                                                        <asp:Label ID="Label3" runat="server" Text="Assistant #3"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboAssistantID3" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboAssistantID1_ItemsRequested"
                                                            OnItemDataBound="cboPhysicianID_ItemDataBound">
                                                            <FooterTemplate>
                                                                Note : Show max 15 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr id="trAssist4" runat="server" visible="false">
                                                    <td class="label">
                                                        <asp:Label ID="Label4" runat="server" Text="Assistant #4"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboAssistantID4" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboAssistantID1_ItemsRequested"
                                                            OnItemDataBound="cboPhysicianID_ItemDataBound">
                                                            <FooterTemplate>
                                                                Note : Show max 15 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr id="trAssist5" runat="server" visible="false">
                                                    <td class="label">
                                                        <asp:Label ID="Label5" runat="server" Text="Assistant #5"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboAssistantID5" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboAssistantID1_ItemsRequested"
                                                            OnItemDataBound="cboPhysicianID_ItemDataBound">
                                                            <FooterTemplate>
                                                                Note : Show max 15 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblResident" runat="server" Text="Resident"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadTextBox ID="txtResident" runat="server" Width="250px" />
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblInstrumentatorID1" runat="server" Text="Instrumentator #1"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboInstrumentatorID1" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboInstrumentatorID1_ItemsRequested"
                                                            OnItemDataBound="cboPhysicianID_ItemDataBound">
                                                            <FooterTemplate>
                                                                Note : Show max 15 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblInstrumentatorID2" runat="server" Text="Instrumentator #2"></asp:Label>
                                                        <asp:LinkButton ID="btnInstruMore" runat="server" Text="more" OnClick="btnInstruMore_Clicked">
                                                        </asp:LinkButton>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboInstrumentatorID2" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboInstrumentatorID1_ItemsRequested"
                                                            OnItemDataBound="cboPhysicianID_ItemDataBound">
                                                            <FooterTemplate>
                                                                Note : Show max 15 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr id="trInstru3" runat="server" visible="false">
                                                    <td class="label">
                                                        <asp:Label ID="Label10" runat="server" Text="Instrumentator #3"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboInstrumentatorID3" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboInstrumentatorID1_ItemsRequested"
                                                            OnItemDataBound="cboPhysicianID_ItemDataBound">
                                                            <FooterTemplate>
                                                                Note : Show max 15 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr id="trInstru4" runat="server" visible="false">
                                                    <td class="label">
                                                        <asp:Label ID="Label11" runat="server" Text="Instrumentator #4"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboInstrumentatorID4" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboInstrumentatorID1_ItemsRequested"
                                                            OnItemDataBound="cboPhysicianID_ItemDataBound">
                                                            <FooterTemplate>
                                                                Note : Show max 15 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr id="trInstru5" runat="server" visible="false">
                                                    <td class="label">
                                                        <asp:Label ID="Label12" runat="server" Text="Instrumentator #5"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboInstrumentatorID5" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboInstrumentatorID1_ItemsRequested"
                                                            OnItemDataBound="cboPhysicianID_ItemDataBound">
                                                            <FooterTemplate>
                                                                Note : Show max 15 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblInstrumentatorAssistant" runat="server" Text="Assistant Instru / Circular #1"></asp:Label>
                                                        <asp:LinkButton ID="btnInstAssistMore" runat="server" Text="more" OnClick="btnInstAssistMore_Clicked">
                                                        </asp:LinkButton>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboInstrumentatorAssistant" runat="server" Width="250px"
                                                            EnableLoadOnDemand="true" MarkFirstMatch="true" HighlightTemplatedItems="true"
                                                            OnItemsRequested="cboInstrumentatorAssistant_ItemsRequested" OnItemDataBound="cboPhysicianID_ItemDataBound">
                                                            <FooterTemplate>
                                                                Note : Show max 15 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr id="trInstAssist2" runat="server" visible="false">
                                                    <td class="label">
                                                        <asp:Label ID="Label6" runat="server" Text="Assistant Instru / Circular #2"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboInstrumentatorAssistant2" runat="server" Width="250px"
                                                            EnableLoadOnDemand="true" MarkFirstMatch="true" HighlightTemplatedItems="true"
                                                            OnItemsRequested="cboInstrumentatorAssistant_ItemsRequested" OnItemDataBound="cboPhysicianID_ItemDataBound">
                                                            <FooterTemplate>
                                                                Note : Show max 15 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr id="trInstAssist3" runat="server" visible="false">
                                                    <td class="label">
                                                        <asp:Label ID="Label7" runat="server" Text="Assistant Instru / Circular #3"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboInstrumentatorAssistant3" runat="server" Width="250px"
                                                            EnableLoadOnDemand="true" MarkFirstMatch="true" HighlightTemplatedItems="true"
                                                            OnItemsRequested="cboInstrumentatorAssistant_ItemsRequested" OnItemDataBound="cboPhysicianID_ItemDataBound">
                                                            <FooterTemplate>
                                                                Note : Show max 15 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr id="trInstAssist4" runat="server" visible="false">
                                                    <td class="label">
                                                        <asp:Label ID="Label8" runat="server" Text="Assistant Instru / Circular #4"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboInstrumentatorAssistant4" runat="server" Width="250px"
                                                            EnableLoadOnDemand="true" MarkFirstMatch="true" HighlightTemplatedItems="true"
                                                            OnItemsRequested="cboInstrumentatorAssistant_ItemsRequested" OnItemDataBound="cboPhysicianID_ItemDataBound">
                                                            <FooterTemplate>
                                                                Note : Show max 15 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr id="trInstAssist5" runat="server" visible="false">
                                                    <td class="label">
                                                        <asp:Label ID="Label9" runat="server" Text="Assistant Instru / Circular #5"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboInstrumentatorAssistant5" runat="server" Width="250px"
                                                            EnableLoadOnDemand="true" MarkFirstMatch="true" HighlightTemplatedItems="true"
                                                            OnItemsRequested="cboInstrumentatorAssistant_ItemsRequested" OnItemDataBound="cboPhysicianID_ItemDataBound">
                                                            <FooterTemplate>
                                                                Note : Show max 15 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                            </asp:Panel>
                                        </table>
                                    </td>
                                    <td style="width: 34%; vertical-align: top;">
                                        <table width="100%">
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblParamedicIDAnestesi" runat="server" Text="Anesthesiologist"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadComboBox ID="cboPhysicianIDAnestesi" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                        MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboPhysicianIDAnestesi_ItemsRequested"
                                                        OnItemDataBound="cboPhysicianID_ItemDataBound">
                                                        <FooterTemplate>
                                                            Note : Show max 15 items
                                                        </FooterTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr runat="server" id="trAssistantIDAnestesi">
                                                <td class="label">
                                                    <asp:Label ID="lblAssistantIDAnestesi" runat="server" Text="Assistant #1"></asp:Label>
                                                    <asp:LinkButton ID="btnAsstAnestesiMore" runat="server" Text="more" OnClick="btnAsstAnestesiMore_Clicked">
                                                    </asp:LinkButton>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadComboBox ID="cboAssistantIDAnestesi" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                        MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboAssistantIDAnestesi_ItemsRequested"
                                                        OnItemDataBound="cboPhysicianID_ItemDataBound">
                                                        <FooterTemplate>
                                                            Note : Show max 15 items
                                                        </FooterTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr runat="server" id="trAssistantIDAnestesi2" visible="false">
                                                <td class="label">
                                                    <asp:Label ID="lblAssistantIDAnestesi2" runat="server" Text="Assistant #2"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadComboBox ID="cboAssistantIDAnestesi2" runat="server" Width="250px" EnableLoadOnDemand="true"
                                                        MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboAssistantIDAnestesi_ItemsRequested"
                                                        OnItemDataBound="cboPhysicianID_ItemDataBound">
                                                        <FooterTemplate>
                                                            Note : Show max 15 items
                                                        </FooterTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblSRAnestesi" runat="server" Text="Anesthetic Type"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadComboBox ID="cboSRAnestesi" runat="server" Width="250px" AllowCustomText="true"
                                                        Filter="Contains" />
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr runat="server" id="trIsAnestheticConversion">
                                                <td class="label">
                                                    <asp:Label ID="lblIsAnestheticConversion" runat="server" Text="Anesthetic Conversion" />
                                                </td>
                                                <td class="entry">
                                                    <asp:RadioButtonList ID="rblIsAnestheticConversion" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="true">No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr id="trProc1" runat="server" visible="false">
                                                <td class="label">
                                                    <asp:Label ID="Label13" runat="server" Text="Primary Procedure"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadComboBox ID="cboSRProcedure1" runat="server" Width="250px" AllowCustomText="true"
                                                        Filter="Contains">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td width="20px">
                                                    <asp:RequiredFieldValidator ID="rfvSRProcedure1" runat="server" ErrorMessage="Primary Procedure required."
                                                        ValidationGroup="entry" ControlToValidate="cboSRProcedure1" SetFocusOnError="True" Width="100%">
                                                        <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr id="trProc2" runat="server" visible="false">
                                                <td class="label">
                                                    <asp:Label ID="Label14" runat="server" Text="Secondary Procedure"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadComboBox ID="cboSRProcedure2" runat="server" Width="250px" AllowCustomText="true"
                                                        Filter="Contains">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadTextBox ID="txtNotes" runat="server" Width="250px" MaxLength="4000" TextMode="MultiLine" />
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblSMF" runat="server" Text="Specialty / SMF"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadComboBox ID="cboSMF" runat="server" Width="250px" AllowCustomText="true"
                                                        Filter="Contains">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td width="20px">
                                                    <asp:RequiredFieldValidator ID="rfvSMF" runat="server" ErrorMessage="Specialty / SMF required."
                                                        ValidationGroup="entry" ControlToValidate="cboSMF" SetFocusOnError="True" Width="100%">
                                                        <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblSRWoundClassification" runat="server" Text="Wound Classification"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadComboBox ID="cboSRWoundClassification" runat="server" Width="250px" />
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblSRAsaScore" runat="server" Text="ASA Score"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadComboBox ID="cboSRAsaScore" runat="server" Width="250px" />
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr runat="server" id="trIsExtendedSurgery">
                                                <td class="label"></td>
                                                <td class="entry">
                                                    <asp:CheckBox ID="chkIsExtendedSurgery" runat="server" Text="Extended Surgery" />
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblNeedPa" runat="server" Text="Requires Anatomic Pathology Examination" />
                                                </td>
                                                <td class="entry">
                                                    <table cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButtonList ID="rblNeedPa" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblNeedPa_OnSelectedIndexChanged"
                                                                    AutoPostBack="true">
                                                                    <asp:ListItem Selected="true">No</asp:ListItem>
                                                                    <asp:ListItem>Yes</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td style="width: 5px"></td>
                                                            <td>
                                                                <telerik:RadDatePicker ID="txtPaDate" runat="server" Width="100px">
                                                                </telerik:RadDatePicker>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblSourceOfTissue" runat="server" Text="Source Of Tissue" />
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadTextBox ID="txtSourceOfTissue" runat="server" Width="250px" MaxLength="100" />
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblAmountOfBleeding" runat="server" Text="Amount Of Bleeding" />
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
                                                    <asp:Label ID="lblAmountOfTransfusions" runat="server" Text="Amount Of Transfusions" />
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadNumericTextBox ID="txtAmountOfTransfusions" runat="server" Width="100px">
                                                        <NumberFormat DecimalDigits="2" />
                                                    </telerik:RadNumericTextBox>&nbsp;cc
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label"></td>
                                                <td class="entry">
                                                    <asp:CheckBox ID="chkIsValidate" runat="server" Text="Validate" Enabled="false" />
                                                    <asp:CheckBox ID="chkIsApprove" runat="server" Text="Approved" Enabled="false" />
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblCreateBy" runat="server" Text="Created By"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadTextBox ID="txtCreatedBy" runat="server" Width="250px" ReadOnly="true" />
                                                </td>
                                                <td width="20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="Label17" runat="server" Text="Last Update By"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadTextBox ID="txtLastUpdateBy" runat="server" Width="250px" ReadOnly="true" />
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
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgImplant" runat="server">
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
                            UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn DataField="PlacementSite" HeaderText="Placement Site" UniqueName="PlacementSite"
                            SortExpression="PlacementSite" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ImplantInstallationItem.ascx" EditFormType="WebUserControl">
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
    <br />
    <br />
    <br />
</asp:Content>
