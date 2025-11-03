<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailOpRegistrationAndDischargeApptItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.OutPatient.DetailOpRegistrationAndDischargeApptItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumDischargeAppt" runat="server" ValidationGroup="DischargeAppt" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="DischargeAppt"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
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
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                            ControlToValidate="cboServiceUnitID" SetFocusOnError="True" ValidationGroup="DischargeAppt"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
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
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvParamedicID" runat="server" ErrorMessage="Physician required."
                            ControlToValidate="cboParamedicID" SetFocusOnError="True" ValidationGroup="DischargeAppt"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRoomID" runat="server" Text="Room"></asp:Label>
                    </td>
                    <td class="entry2Column">
                        <telerik:RadComboBox ID="cboRoomID" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvRoomID" runat="server" ErrorMessage="Room required."
                            ControlToValidate="cboRoomID" SetFocusOnError="True" ValidationGroup="DischargeAppt"
                            Width="100%">
                            <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
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
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="DischargeAppt"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="DischargeAppt" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>