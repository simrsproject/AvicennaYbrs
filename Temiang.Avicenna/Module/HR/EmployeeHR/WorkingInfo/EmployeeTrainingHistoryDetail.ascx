<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeTrainingHistoryDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.EmployeeTrainingHistoryDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEmployeeTrainingHistory" runat="server" ValidationGroup="EmployeeTrainingHistory" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EmployeeTrainingHistory"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="width: 33%; vertical-align: top">
            <table>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblEmployeeTrainingHistoryID" runat="server" Text="Employee Training History ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtEmployeeTrainingHistoryID" runat="server" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>

                <tr>
                    <td class="label">
                        <asp:Label ID="lblEventName" runat="server" Text="Training Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtEventName" runat="server" Width="300px" MaxLength="255" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvEventName" runat="server" ErrorMessage="Training Name required."
                            ControlToValidate="txtEventName" SetFocusOnError="True" ValidationGroup="EmployeeTrainingHistory"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRActivityType" runat="server" Text="Training Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRActivityType" runat="server" Width="300px" AllowCustomText="true"
                            Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboSRActivityType_SelectedIndexChanged" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRActivityType" runat="server" ErrorMessage="Training Type required."
                            ControlToValidate="cboSRActivityType" SetFocusOnError="True" ValidationGroup="EmployeeTrainingHistory"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRActivitySubType" runat="server" Text="Training Sub Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRActivitySubType" runat="server" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboSRActivitySubType_ItemDataBound"
                            OnItemsRequested="cboSRActivitySubType_ItemsRequested">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblTrainingLocation" runat="server" Text="Training Location"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtTrainingLocation" runat="server" Width="300px" MaxLength="255" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblTrainingInstitution" runat="server" Text="Training Organizer"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtTrainingInstitution" runat="server" Width="300px" MaxLength="255" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblStartDate" runat="server" Text="Training Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="txtStartDate" runat="server" Width="100px" MinDate="01/01/1900" MaxDate="12/31/2999" AutoPostBack="true" OnSelectedDateChanged="txtStartDate_SelectedDateChanged"/>
                                </td>
                                <td>&nbsp;-&nbsp;
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtEndDate" runat="server" Width="100px" MinDate="01/01/1900" MaxDate="12/31/2999" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ErrorMessage="Training Start Date required."
                            ControlToValidate="txtStartDate" SetFocusOnError="True" ValidationGroup="EmployeeTrainingHistory"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ErrorMessage="Training End Date required."
                            ControlToValidate="txtEndDate" SetFocusOnError="True" ValidationGroup="EmployeeTrainingHistory"
                            Width="100%">
                            <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSREmployeeTrainingDateSeparator" runat="server" Text="With Date Separator"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSREmployeeTrainingDateSeparator" runat="server" Width="75px" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">Total Hour
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtTotalHour" runat="server" Width="100px" Value="0" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDuration" runat="server" Text="Duration"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtDurationHour" runat="server" Width="100px" MinValue="0" NumberFormat-DecimalDigits="0" />
                                </td>
                                <td>&nbsp;Hour&nbsp;</td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtDurationMinutes" runat="server" Width="100px" MinValue="0" NumberFormat-DecimalDigits="0" />
                                </td>
                                <td>&nbsp;Minutes</td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">Credit Point
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtCreditPoint" runat="server" Width="100px" Value="0" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSREmployeeTrainingPointType" runat="server" Text="Training Point"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSREmployeeTrainingPointType" runat="server" Width="300px" AllowCustomText="true"
                            Filter="Contains" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EmployeeTrainingHistory"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="EmployeeTrainingHistory" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 33%; vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPlanningCosts" runat="server" Text="Planning Costs"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtPlanningCosts" runat="server" Width="100px" Value="0" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRTrainingFinancingSources" runat="server" Text="Financing Sources"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRTrainingFinancingSources" runat="server" Width="300px" AllowCustomText="true"
                            Filter="Contains" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblFee" runat="server" Text="Fee"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtFee" runat="server" Width="100px" Value="0" NumberFormat-DecimalDigits="2" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvFee" runat="server" ErrorMessage="Fee required."
                            ControlToValidate="txtFee" SetFocusOnError="True" ValidationGroup="EmployeeTrainingHistory"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSponsorFee" runat="server" Text="Sponsor Fee"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtSponsorFee" runat="server" Width="100px" Value="0" NumberFormat-DecimalDigits="2" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSponsorFee" runat="server" ErrorMessage="Sponsor Fee required."
                            ControlToValidate="txtFee" SetFocusOnError="True" ValidationGroup="EmployeeTrainingHistory"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSREmployeeTrainingRole" runat="server" Text="Role"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSREmployeeTrainingRole" runat="server" Width="300px" AllowCustomText="true"
                            Filter="Contains" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNote" runat="server" Text="Notes"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNote" runat="server" Width="300px" MaxLength="500"
                            TextMode="MultiLine" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label" />
                    <td class="entry">
                        <asp:CheckBox ID="chkIsInHouseTraining" runat="server" Text="In-House" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label" />
                    <td class="entry">
                        <asp:CheckBox ID="chkIsScheduledTraining" runat="server" Text="Scheduled" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label" />
                    <td class="entry">
                        <asp:CheckBox ID="chkIsAttending" runat="server" Text="Attend" />
                    </td>
                    <td width="20px" />
                    <td />
                    <td></td>
                </tr>
            </table>
        </td>
        <td style="width: 34%; vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblCertificateValidityPeriod" runat="server" Text="Certificate Validity Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtCertificateValidityPeriod" runat="server" Width="100px" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label" />
                    <td class="entry">
                        <asp:CheckBox ID="chkIsCommitmentToWork" runat="server" Text="Commitment To Work" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblLengthOfService" runat="server" Text="Service Year"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtLengthOfService" runat="server" Width="100px" Value="0" NumberFormat-DecimalDigits="0" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblServiceDate" runat="server" Text="Service Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="txtStartServiceDate" runat="server" Width="100px" />
                                </td>
                                <td>&nbsp;-&nbsp;
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtEndServiceDate" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td colspan="3">
                        <hr />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblEvaluationDate" runat="server" Text="Evaluation Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtEvaluationDate" runat="server" Width="100px" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
            </table>
        </td>
    </tr>
</table>
