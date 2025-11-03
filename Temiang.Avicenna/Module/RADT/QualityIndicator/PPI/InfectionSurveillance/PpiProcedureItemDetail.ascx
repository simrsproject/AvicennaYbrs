<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PpiProcedureItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.PpiProcedureItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumPpiProcedureSurveillance" runat="server" ValidationGroup="PpiProcedureSurveillance" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PpiProcedureSurveillance"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="Label3" runat="server" Text="Diagnose"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtDiagnose" runat="server" TextMode="MultiLine" Width="300px"
                            ReadOnly="true">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRealizationDateTimeFrom" runat="server" Text="Date & Time Started"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="100px">
                                    <telerik:RadDatePicker ID="txtRealizationDateFrom" runat="server" Width="100px" Enabled="False" />
                                </td>
                                <td width="50px">
                                    <telerik:RadTimePicker ID="txtRealizationTimeFrom" runat="server" TimeView-Interval="00:30"
                                        TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                        TimeView-Columns="4" TimeView-StartTime="00:00" TimeView-EndTime="23:59" Enabled="False" />
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
                        <asp:Label ID="lblRealizationDateTimeTo" runat="server" Text="Date & Time Finished"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="100px">
                                    <telerik:RadDatePicker ID="txtRealizationDateTo" runat="server" Width="100px" Enabled="False" />
                                </td>
                                <td width="50px">
                                    <telerik:RadTimePicker ID="txtRealizationTimeTo" runat="server" TimeView-Interval="00:30"
                                        TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                        TimeView-Columns="4" TimeView-StartTime="00:00" TimeView-EndTime="23:59" Enabled="False" />
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
                    </td>
                    <td class="entry">
                        <asp:RadioButtonList ID="rblIsCito" runat="server" RepeatDirection="Horizontal" Enabled="False">
                            <asp:ListItem Selected="true">Cito</asp:ListItem>
                            <asp:ListItem>Elective</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRWoundClassification" runat="server" Text="Wound Classification"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRWoundClassification" runat="server" Width="304px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRWoundClassification" runat="server" ErrorMessage="Wound Classification required."
                            ControlToValidate="cboSRWoundClassification" SetFocusOnError="True" ValidationGroup="PpiProcedureSurveillance"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRAsaScore" runat="server" Text="ASA Score"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRAsaScore" runat="server" Width="304px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRAsaScore" runat="server" ErrorMessage="ASA Score required."
                            ControlToValidate="cboSRAsaScore" SetFocusOnError="True" ValidationGroup="PpiProcedureSurveillance"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PpiProcedureSurveillance"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="PpiProcedureSurveillance" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
