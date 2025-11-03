<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="AppointmentSelectTime.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.OutPatient.AppointmentSelectTime"
    Title="Appointment Information" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function OnClientTimeSlotClick(sender, args) {
                var txtDate = $find("<%= txtAppointmentDate.ClientID %>");
                var txtTime = $find("<%= txtAppointmentTime.ClientID %>");

                txtDate.set_selectedDate(args.get_time());
                txtTime.set_value(args.get_time().format('HH:mm'));
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="schedule">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="schedule" />
                    <telerik:AjaxUpdatedControl ControlID="cboWorkTime" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboParamedicID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="schedule" />
                    <telerik:AjaxUpdatedControl ControlID="cboWorkTime" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboWorkTime">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="schedule" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboVisitTypeID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtVisitDuration" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <%--<telerik:AjaxSetting AjaxControlID="calSchedule">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="schedule" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image2" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ValidationSummary ID="vsumTransChargesItem" runat="server" ValidationGroup="Appointment"
        Width="100%" />
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="300px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboParamedicID_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label7" runat="server" Text="Work Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboWorkTime" runat="server" AutoPostBack="true" Width="300px"
                                OnSelectedIndexChanged="cboWorkTime_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label5" runat="server" Text="Date & Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtAppointmentDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                DatePopupButton-Enabled="false">
                            </telerik:RadDatePicker>
                            <telerik:RadMaskedTextBox ID="txtAppointmentTime" runat="server" Mask="<00..23>:<00..59>"
                                PromptChar="_" RoundNumericRanges="false" Width="50px">
                            </telerik:RadMaskedTextBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvAppointmentDate" runat="server" ErrorMessage="Appointment Date required."
                                ControlToValidate="txtAppointmentTime" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblVisitTypeID" runat="server" Text="Visit Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboVisitTypeID" runat="server" Width="300px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboVisitTypeID_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvVisitTypeID" runat="server" ErrorMessage="Visit Type ID required."
                                ControlToValidate="cboVisitTypeID" SetFocusOnError="True" Width="100%" ValidationGroup="Appointment">
                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblVisitDuration" runat="server" Text="Visit Duration (Minute)"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtVisitDuration" runat="server" Width="100px" MinValue="0"
                                NumberFormat-DecimalDigits="0">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td width="60px">
                            <asp:RequiredFieldValidator ID="rfvVisitDuration" runat="server" ErrorMessage="Visit Duration required."
                                ControlToValidate="txtVisitDuration" SetFocusOnError="True" Width="20px" ValidationGroup="Appointment">
                                <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="rvVisitDuration" ControlToValidate="txtVisitDuration" MinimumValue="1"
                                MaximumValue="1000" Type="Integer" SetFocusOnError="True" ValidationGroup="Appointment"
                                ErrorMessage="Visit Duration value invalid" runat="server" Width="20px">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RangeValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label6" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="2000" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table>
                    <tr>
                        <td valign="top">
                            <telerik:RadCalendar ID="calSchedule" runat="server" EnableMultiSelect="false" AutoPostBack="true"
                                OnSelectionChanged="calSchedule_SelectionChanged">
                            </telerik:RadCalendar>
                        </td>
                        <td>
                            <telerik:RadScheduler ID="schedule" runat="server" AllowDelete="False" AllowEdit="False"
                                AllowInsert="False" HoursPanelTimeFormat="HH:mm" LastDayOfWeek="Sunday"
                                MinutesPerRow="10" ShowAllDayRow="False" Width="100%" RowHeaderWidth="60px" Height="450px"
                                ShowDateHeaders="true" ShowFooter="False" ShowNavigationPane="true" ShowResourceHeaders="False"
                                ShowViewTabs="False" TimeLabelRowSpan="1" OnClientTimeSlotClick="OnClientTimeSlotClick"
                                OnNavigationComplete="schedule_NavigationComplete" RowHeight="20px">
                                <Localization AdvancedAllDayEvent="All day" />
                                <AdvancedForm DateFormat="M/d/yyyy" TimeFormat="h:mm tt" />
                            </telerik:RadScheduler>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
