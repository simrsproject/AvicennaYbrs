<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ServiceUnitBookingDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.ServiceUnitBookingDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 100%">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label3" runat="server" Text="PATIENT DETAIL" Font-Bold="True" Font-Size="9"></asp:Label>
                    </legend>
                    <table style="width: 100%" cellpadding="1" cellspacing="1">
                        <tr>
                            <td style="width: 45%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblPatientID" runat="server" Text="Patient ID"></asp:Label>
                                        </td>
                                        <td class="entry">
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
                                                    Note : Show max 10 items
                                                </FooterTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20px">
                                            <asp:RequiredFieldValidator ID="rfvPatientID" runat="server" ErrorMessage="Patient ID required."
                                                ValidationGroup="entry" ControlToValidate="cboPatientID" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
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
                                    <tr>
                                        <td colspan="4">
                                            <asp:Label ID="lblInfoBookingOutstanding" runat="server" Text="test" Font-Size="11" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 45%; vertical-align: top;">
                                <table width="100%">
                                    <tr id="trRegistrationNo" runat="server">
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
                                        <td width="20px">
                                            <asp:RequiredFieldValidator ID="rfvRegistrationNo" runat="server" ErrorMessage="Registration No required."
                                                ValidationGroup="entry" ControlToValidate="cboRegistrationNo" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td />
                                    </tr>
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
                                                        <telerik:RadTextBox ID="txtPhoneNo" runat="server" Width="147px" MaxLength="50" />
                                                    </td>
                                                    <td style="width: 5px"></td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtMobilePhoneNo" runat="server" Width="148px" MaxLength="50" />
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
                                        <td class="label">
                                            <asp:Label ID="lblGuarantorCardNo" runat="server" Text="Guarantor Card No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtGuarantorCardNo" runat="server" Width="300px" ReadOnly="True" />
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
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 100%">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label1" runat="server" Text="BOOKING DETAIL" Font-Bold="True" Font-Size="9"></asp:Label>
                    </legend>
                    <table style="width: 100%" cellpadding="1" cellspacing="1">
                        <tr>
                            <td style="width: 45%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblBookingNo" runat="server" Text="Booking No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtBookingNo" runat="server" Width="300px" MaxLength="20"
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
                                                            TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm:ss"
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
                                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
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
                                                            TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm:ss"
                                                            TimeView-Columns="4" TimeView-StartTime="07:00" TimeView-EndTime="22:00" Width="90px" />
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
                                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblRoomBookingID" runat="server" Text="Room Booking"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboRoomBookingID" runat="server" Width="300px" AllowCustomText="true" Filter="Contains" AutoPostBack="true"
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
                                            <asp:Label ID="lblDiagnose" runat="server" Text="Diagnosis"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtDiagnose" runat="server" Width="300px" MaxLength="100" />
                                        </td>
                                        <td width="20px"></td>
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
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" Height="60px" MaxLength="4000" TextMode="MultiLine" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 45%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblParamedicID" runat="server" Text="Surgeon #1"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboPhysicianID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboPhysicianID_ItemsRequested"
                                                OnItemDataBound="cboPhysicianID_ItemDataBound">
                                                <FooterTemplate>
                                                    Note : Show max 15 items
                                                </FooterTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20px">
                                            <asp:RequiredFieldValidator ID="rfvPhysicianID" runat="server" ErrorMessage="Physician #1 required."
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
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboPhysicianID2" runat="server" Width="300px" EnableLoadOnDemand="true"
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
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblParamedicID3" runat="server" Text="Surgeon #3"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboPhysicianID3" runat="server" Width="300px" EnableLoadOnDemand="true"
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
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblParamedicID4" runat="server" Text="Surgeon #4"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboPhysicianID4" runat="server" Width="300px" EnableLoadOnDemand="true"
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
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblParamedicIDAnestesi" runat="server" Text="Anesthesiologist"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboPhysicianIDAnestesi" runat="server" Width="300px" EnableLoadOnDemand="true"
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
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSRAnestesi" runat="server" Text="Anesthetic Type"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboSRAnestesi" runat="server" Width="300px" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSRWoundClassification" runat="server" Text="Wound Classification"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboSRWoundClassification" runat="server" Width="300px" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSRAsaScore" runat="server" Text="ASA Score"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboSRAsaScore" runat="server" Width="300px" />
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
                                </table>
                            </td>
                            <td valign="top"></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
