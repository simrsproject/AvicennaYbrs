<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="VehicleScheduleDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Ambulance.Transaction.VehicleScheduleDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align:top;">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label1" runat="server" Text="BOOKING DETAIL" Font-Bold="True" Font-Size="9"></asp:Label>
                    </legend>
                    <table style="width: 100%" cellpadding="1" cellspacing="1">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBookingNo" runat="server" Text="Booking No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20"
                                    ReadOnly="true" />
                            </td>
                            <td width="20px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label4" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboServiceUnitID_ItemsRequested"
                                    OnItemDataBound="cboServiceUnitID_ItemDataBound">
                                    <FooterTemplate>
                                        Note : Show max 15 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label21" runat="server" Text="Patient Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboRegistrationNo" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboRegistrationNo_ItemsRequested"
                                    OnItemDataBound="cboRegistrationNo_ItemDataBound" OnSelectedIndexChanged="cboRegistrationNo_SelectedIndexChanged" AutoPostBack="true">
                                    <ItemTemplate>
                                        <b>
                                            <%# DataBinder.Eval(Container.DataItem, "RegistrationNo")%>
                                        </b>&nbsp;-&nbsp;
                                        <b>
                                            <%# DataBinder.Eval(Container.DataItem, "PatientName")%>
                                        </b>&nbsp;-&nbsp;
                                        <br />
                                        <%# DataBinder.Eval(Container.DataItem, "MedicalNo") %>
                                        &nbsp;|&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName") %>
                                        <br />
                                        Address :
                                        <%# DataBinder.Eval(Container.DataItem, "Address")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 30 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr runat="server" ID="trRegInfo">
                            <td class="label">
                            </td>
                            <td class="entry" colspan="3">
                                <fieldset>
                                    <legend></legend>
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="width:100px;">Registration No</td>
                                            <td style="width:5px;">:</td>
                                            <td><telerik:RadLabel runat="server" ID="lblRegistrationNo"></telerik:RadLabel> </td>
                                        </tr>
                                        <tr>
                                            <td>Medical No</td>
                                            <td>:</td>
                                            <td><telerik:RadLabel runat="server" ID="lblMedicalNo"></telerik:RadLabel> </td>
                                        </tr>
                                        <tr>
                                            <td>Patient Name</td>
                                            <td>:</td>
                                            <td><telerik:RadLabel runat="server" ID="lblPatientName"></telerik:RadLabel> </td>
                                        </tr>
                                         <tr>
                                            <td>Address</td>
                                            <td>:</td>
                                            <td><telerik:RadLabel runat="server" ID="lblAddress"></telerik:RadLabel> </td>
                                        </tr>
                                        <tr>
                                            <td>Service Unit</td>
                                            <td>:</td>
                                            <td><telerik:RadLabel runat="server" ID="lblServiceUnit"></telerik:RadLabel> </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label12" runat="server" Text="Vehicle Type"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboVehicleType" runat="server" Width="300px" AutoPostBack="true" 
                                    OnSelectedIndexChanged="cboVehicleType_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Vehicle type required."
                                    ValidationGroup="entry" ControlToValidate="cboVehicleType" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBookingDateFrom" runat="server" Text="Booking Start"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtBookingDateStart" runat="server" Width="100px" AutoPostBack="true"
                                                OnSelectedDateChanged="txtBookingDateStart_SelectedDateChanged">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td>
                                            <telerik:RadTimePicker ID="txtBookingTimeStart" runat="server" TimeView-Interval="00:30"
                                                TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm:ss"
                                                TimeView-Columns="4" TimeView-StartTime="00:00" TimeView-EndTime="23:59" AutoPostBack="true"
                                                OnSelectedDateChanged="txtBookingTimeStart_SelectedDateChanged" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvBookingDateFrom" runat="server" ErrorMessage="Booking Date Start required."
                                    ValidationGroup="entry" ControlToValidate="txtBookingDateStart" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfvBookingTimeFrom" runat="server" ErrorMessage="Booking Time Start required."
                                    ValidationGroup="entry" ControlToValidate="txtBookingTimeStart" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBookingDateTo" runat="server" Text="Booking End"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtBookingDateEnd" runat="server" Width="100px">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td>
                                            <telerik:RadTimePicker ID="txtBookingTimeEnd" runat="server" TimeView-Interval="00:30"
                                                TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm:ss"
                                                TimeView-Columns="4" TimeView-StartTime="00:00" TimeView-EndTime="23:59" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvBookingDateTo" runat="server" ErrorMessage="Booking Date End required."
                                    ValidationGroup="entry" ControlToValidate="txtBookingDateEnd" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfvBookingTimeTo" runat="server" ErrorMessage="Booking Time End required."
                                    ValidationGroup="entry" ControlToValidate="txtBookingTimeEnd" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDestination" runat="server" Text="Destination"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtDestination" runat="server" Width="300px" MaxLength="100" />
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Destination required."
                                    ValidationGroup="entry" ControlToValidate="txtDestination" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblNotes" runat="server" Text="Additional Notes"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" TextMode="MultiLine" />
                            </td>
                            <td width="20px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label15" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="entry" colspan="3">
                                <fieldset>
                                    <legend>
                                        <asp:Label ID="Label16" runat="server" Text="" Font-Bold="True" Font-Size="9"></asp:Label>
                                    </legend>
                                    <table style="width: 100%" cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td colspan="3"><telerik:RadCheckBox ID="chkApproved" runat="server" Enabled="false" BackColor="White" Text="Order Approved"></telerik:RadCheckBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width:100px;">Created By</td>
                                            <td style="width:5px;">:</td>
                                            <td><asp:Label ID="lblCreatedBy" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Created DateTime</td>
                                            <td>:</td>
                                            <td><asp:Label ID="lblCreatedDateTime" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Approved By</td>
                                            <td>:</td>
                                            <td><asp:Label ID="lblApprovedBy" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Approved DateTime</td>
                                            <td>:</td>
                                            <td><asp:Label ID="lblApprovedDateTime" runat="server"></asp:Label></td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label22" runat="server" Text="Driver Information" Font-Bold="True" Font-Size="9"></asp:Label>
                    </legend>
                    <telerik:RadGrid ID="grdDriver" runat="server" OnNeedDataSource="grdDriver_NeedDataSource" AutoGenerateColumns="false">
                        <MasterTableView DataKeyNames="DriverID">
                            <Columns>
                                <telerik:GridBoundColumn DataField="DriverName" HeaderText="Driver Name"
                                    UniqueName="DriverName" SortExpression="DriverName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="SRDriverStatusName" HeaderText="Status" UniqueName="SRDriverStatusName"
                                    SortExpression="SRDriverStatusName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </fieldset>
            </td>
            <td style="width: 50%; vertical-align:top;">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label2" runat="server" Text="CONFIRMATION" Font-Bold="True" Font-Size="9"></asp:Label>
                    </legend>
                    <table style="width: 100%" cellpadding="1" cellspacing="1">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label14" runat="server" Text="Order Type"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboVehicleOrderType" runat="server" Width="300px" >
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvVehicleOrderType" runat="server" ErrorMessage="Order type required."
                                    ValidationGroup="entry" ControlToValidate="cboVehicleOrderType" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label3" runat="server" Text="Distance (KM)"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtDistanceInKM" runat="server" Width="300px" NumberFormat-DecimalDigits="2">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td width="20px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label5" runat="server" Text="Vehicle"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboVehicle" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboVehicle_ItemsRequested"
                                    OnItemDataBound="cboVehicle_ItemDataBound">
                                    <FooterTemplate>
                                        Note : Show max 15 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvVehicle" runat="server" ErrorMessage="Vehicle required."
                                    ValidationGroup="entry" ControlToValidate="cboVehicle" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label10" runat="server" Text="Driver"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboDriver" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboDriver_ItemsRequested"
                                    OnItemDataBound="cboDriver_ItemDataBound">
                                    <FooterTemplate>
                                        Note : Show max 15 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvDriver" runat="server" ErrorMessage="Driver required."
                                    ValidationGroup="entry" ControlToValidate="cboDriver" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label13" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="entry">
                                
                            </td>
                            <td width="20px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label17" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="entry" colspan="3">
                                <fieldset>
                                    <legend>
                                        <asp:Label ID="Label18" runat="server" Text="" Font-Bold="True" Font-Size="9"></asp:Label>
                                    </legend>
                                    <table style="width: 100%" cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td colspan="3"><telerik:RadCheckBox ID="chkIsConfirmed" runat="server" Enabled="false" BackColor="White" Text="Confirmed"></telerik:RadCheckBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width:100px;">Confirmed By</td>
                                            <td style="width:5px;"> </td>
                                            <td><asp:Label ID="lblConfirmedBy" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Confirmed DateTime</td>
                                            <td>:</td>
                                            <td><asp:Label ID="lblConfirmedDateTime" runat="server"></asp:Label></td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label11" runat="server" Text="REALIZATION" Font-Bold="True" Font-Size="9"></asp:Label>
                    </legend>
                    <table style="width: 100%" cellpadding="1" cellspacing="1">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label6" runat="server" Text="Odometer Start"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtOdometerStart" runat="server" Width="300px" NumberFormat-DecimalDigits="2">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td width="20px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label7" runat="server" Text="Odometer End"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtOdometerEnd" runat="server" Width="300px" NumberFormat-DecimalDigits="2">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td width="20px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label8" runat="server" Text="Realization Start"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtRealizationDateStart" runat="server" Width="100px">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td>
                                            <telerik:RadTimePicker ID="txtRealizationTimeStart" runat="server" TimeView-Interval="00:30"
                                                TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm:ss"
                                                TimeView-Columns="4" TimeView-StartTime="00:00" TimeView-EndTime="23:59"/>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20px">

                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label9" runat="server" Text="Realization End"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtRealizationDateEnd" runat="server" Width="100px">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td>
                                            <telerik:RadTimePicker ID="txtRealizationTimeEnd" runat="server" TimeView-Interval="00:30"
                                                TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm:ss"
                                                TimeView-Columns="4" TimeView-StartTime="00:00" TimeView-EndTime="23:59" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20px">

                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRealizationNotes" runat="server" Text="Notes"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtRealizationNotes" runat="server" Width="300px" MaxLength="4000" TextMode="MultiLine" Height="80px" />
                            </td>
                            <td width="20px">

                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="entry" colspan="3">
                                <fieldset>
                                    <legend>
                                        <asp:Label ID="Label20" runat="server" Text="" Font-Bold="True" Font-Size="9"></asp:Label>
                                    </legend>
                                    <table style="width: 100%" cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td colspan="3"><telerik:RadCheckBox ID="chkIsRealized" runat="server" Enabled="false" BackColor="White" Text="Realized"></telerik:RadCheckBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width:100px;">Realized By</td>
                                            <td style="width:5px;"> </td>
                                            <td><asp:Label ID="lblRealizedBy" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Realized DateTime</td>
                                            <td>:</td>
                                            <td><asp:Label ID="lblRealizedDateTime" runat="server"></asp:Label></td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
