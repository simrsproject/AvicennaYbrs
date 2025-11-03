<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PhysicianTeamDetailItem.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.InPatient.PhysicianTeamDetailItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumPhysicianTeamDetailItem" runat="server" ValidationGroup="PhysicianTeamDetailItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PhysicianTeamDetailItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<asp:HiddenField runat="server" ID="hdnSourceType"/>    
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" valign="top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPhysicianID" runat="server" Text="Physician"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboPhysicianID" Width="300px" EnableLoadOnDemand="True"
                            HighlightTemplatedItems="True" MarkFirstMatch="True" OnItemDataBound="cboPhysicianID_ItemDataBound"
                            OnItemsRequested="cboPhysicianID_ItemsRequested">
                            <FooterTemplate>
                                Note : Show max 15 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="rfvPhysicianID" runat="server" ErrorMessage="Physician required."
                            ValidationGroup="PhysicianTeamDetailItem" ControlToValidate="cboPhysicianID"
                            SetFocusOnError="True" Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRParamedicTeamStatus" runat="server" Text="Team Status"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRParamedicTeamStatus" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRParamedicTeamStatus" runat="server" ErrorMessage="Team Status required."
                            ControlToValidate="cboSRParamedicTeamStatus" SetFocusOnError="True" ValidationGroup="PhysicianTeamDetailItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPeriod" runat="server" Text="Period"></asp:Label>
                    </td>
                    <td class="entry">
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadDateTimePicker ID="txtStartDateTime" runat="server" AutoPostBackControl="None">
                                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd-MMM-yyyy HH:mm" DateFormat="dd-MMM-yyyy HH:mm">
                                        </DateInput>
                                        <TimeView ID="TimeView1" runat="server" TimeFormat="HH:mm">
                                        </TimeView>
                                    </telerik:RadDateTimePicker>
                                </td>
                                <td>
                                    &nbsp;to&nbsp;
                                </td>
                                <td>
                                    <telerik:RadDateTimePicker ID="txtEndDateTime" runat="server" AutoPostBackControl="None">
                                        <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd-MMM-yyyy HH:mm" DateFormat="dd-MMM-yyyy HH:mm">
                                        </DateInput>
                                        <TimeView ID="TimeView2" runat="server" TimeFormat="HH:mm">
                                        </TimeView>
                                    </telerik:RadDateTimePicker>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvStartDateTime" runat="server" ErrorMessage="Start Period required."
                            ValidationGroup="PhysicianTeamDetailItem" ControlToValidate="txtStartDateTime" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine"
                            Height="50px" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PhysicianTeamDetailItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="PhysicianTeamDetailItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" valign="top">
            <table width="100%">
            </table>
        </td>
    </tr>
</table>
