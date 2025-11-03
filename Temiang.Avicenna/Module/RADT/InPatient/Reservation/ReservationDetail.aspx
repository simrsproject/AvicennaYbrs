<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ReservationDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.InPatient.ReservationDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/CustomControl/AddressCtl.ascx" TagName="AddressCtl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReservationNo" runat="server" Text="Reservation No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtReservationNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvReservationNo" runat="server" ErrorMessage="Reservation No required."
                                ValidationGroup="entry" ControlToValidate="txtReservationNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
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
                                    <%# DataBinder.Eval(Container.DataItem, "Address")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" MaxLength="15"
                                ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFirstName" runat="server" Text="First Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtFirstName" runat="server" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="First Name required."
                                ValidationGroup="entry" ControlToValidate="txtFirstName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMiddleName" runat="server" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblLastName" runat="server" Text="Last Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtLastName" runat="server" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReservationDate" runat="server" Text="Reserved Until"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDateTimePicker ID="dtmReservationDateTime" runat="server" Width="170px">
                            </telerik:RadDateTimePicker>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvReservationDate" runat="server" ErrorMessage="Reservation Date required."
                                ValidationGroup="entry" ControlToValidate="dtmReservationDateTime" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr id="trRegistrationNo" runat="server">
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No" />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboRegistrationNo" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboRegistrationNo_ItemsRequested"
                                OnItemDataBound="cboRegistrationNo_ItemDataBound" AutoPostBack="true" OnSelectedIndexChanged="cboRegistrationNo_SelectedIndexChanged">
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
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td style="width: 100%" colspan="4">
                            <fieldset>
                                <legend>
                                    <asp:Label ID="lblPatientInfo" runat="server" Text="REGISTRATION INFORMATION" Font-Bold="True" Font-Size="9"></asp:Label>
                                </legend>
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 100%; vertical-align: top">
                                            <table width="100%">
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="Label1" runat="server" Text="Service Unit"></asp:Label>
                                                    </td>
                                                    <td class="entry2Column">
                                                        <telerik:RadTextBox ID="txtFromServiceUnit" runat="server" Width="300px" ReadOnly="true"
                                                            Enabled="false" />
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="Label2" runat="server" Text="Room"></asp:Label>
                                                    </td>
                                                    <td class="entry2Column">
                                                        <telerik:RadTextBox ID="txtFromRoom" runat="server" Width="300px" ReadOnly="true" Enabled="false" />
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="Label5" runat="server" Text="Bed No"></asp:Label>
                                                    </td>
                                                    <td class="entry2Column">
                                                        <telerik:RadTextBox ID="txtFromBedID" runat="server" Width="100px" MaxLength="10" ReadOnly="true"
                                                            Enabled="false" />
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="Label4" runat="server" Text="Class"></asp:Label>
                                                    </td>
                                                    <td class="entry2Column">
                                                        <telerik:RadTextBox ID="txtFromClass" runat="server" Width="300px" ReadOnly="true" Enabled="false" />
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
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Reservation Information" PageViewID="pgReservation" Selected="True" />
            <telerik:RadTab runat="server" Text="Address" PageViewID="pgAddress" />
            <telerik:RadTab runat="server" Text="Follow Up" PageViewID="pgFollowUp" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgReservation" runat="server" Selected="true">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50%" valign="top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged" HighlightTemplatedItems="True"
                                        MarkFirstMatch="true" OnItemDataBound="cboServiceUnitID_ItemDataBound" OnItemsRequested="cboServiceUnitID_ItemsRequested"
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
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblRoomID" runat="server" Text="Room"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadComboBox ID="cboRoomID" runat="server" Width="300px" AutoPostBack="true"
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
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblBedID" runat="server" Text="Bed No"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadComboBox ID="cboBedID" runat="server" Width="300px" AutoPostBack="true"
                                        HighlightTemplatedItems="True" OnItemDataBound="cboBedID_ItemDataBound" OnItemsRequested="cboBedID_ItemsRequested"
                                        OnSelectedIndexChanged="cboBedID_SelectedIndexChanged" MarkFirstMatch="true"
                                        EnableLoadOnDemand="true" NoWrap="True">
                                        <ItemTemplate>
                                            <b>
                                                <%# DataBinder.Eval(Container.DataItem, "BedID")%>
                                            </b>
                                            <%# string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "RegistrationNo").ToString()) ? "" : 
                                        string.Format("<br />Reg No: {0} <br />Patient Name: {1}", 
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                        DataBinder.Eval(Container.DataItem, "PatientName"))%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 10 result
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvBedID" runat="server" ErrorMessage="Bed No required."
                                        ValidationGroup="Registration" ControlToValidate="cboBedID" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image24" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblClassID" runat="server" Text="Class"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td style="width: 100px">
                                                <telerik:RadTextBox ID="txtClassID" runat="server" Width="100px" MaxLength="10" ReadOnly="true" />
                                            </td>
                                            <td style="width: 6px"></td>
                                            <td>
                                                <asp:Label ID="lblClassName_NT" runat="server" Text="" CssClass="labeldescription"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvClassID" runat="server" ErrorMessage="Class required."
                                        ValidationGroup="Registration" ControlToValidate="txtClassID" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="2000" TextMode="MultiLine" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td width="50%" valign="top"></td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgAddress" runat="server">
            <uc1:AddressCtl ID="ctlAddress" runat="server" />
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgFollowUp" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50%" valign="top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRAppointmentStatus" runat="server" Text="Reservation Status"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRReservationStatus" runat="server" Width="300px" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboSRReservationStatus_SelectedIndexChanged" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvSRAppointmentStatus" runat="server" ErrorMessage="Reservation Status required."
                                        ValidationGroup="entry" ControlToValidate="cboSRReservationStatus" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblFollowUpBy" runat="server" Text="Follow Up By"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtFollowUpBy" runat="server" Width="100px" ReadOnly="true" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblFollowUpDate" runat="server" Text="Follow Up Date Time"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtFollowUpDateTime" runat="server" Width="300px" ReadOnly="true" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td width="50%" valign="top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCreatedOfficer" runat="server" Text="Created Officer"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtCreatedOfficer" runat="server" Width="300px" ReadOnly="true" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCreatedDateTime" runat="server" Text="Created Date Time"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtCreatedDateTime" runat="server" Width="300px" ReadOnly="true" />
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
