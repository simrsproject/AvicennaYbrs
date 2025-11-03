<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConsultReferAnswerCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.ConsultReferAnswerCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<asp:HiddenField runat="server" ID="txtConsultReferNo" />
<fieldset style="width: <%#Width %>;">
    <legend>CONSULTATION AND CONSULTATION ANSWERS</legend>
    <table width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <fieldset>
                    <legend>CONSULTATION INFO</legend>
                    <table width="100%">
                        <tr>
                            <td class="label">Consult Date
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtReferDateTime" runat="server" Width="300px" DatePopupButton-Visible="False" ReadOnly="True">
                                </telerik:RadDatePicker>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Physician
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtFromParamedicName" runat="server" Width="300px" ReadOnly="True" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                    <fieldset>
                        <legend>CONSULTATION ANAMNESIS</legend>
                        <table width="100%">
                            <tr>
                                <td class="label">Chief Complaint
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtChiefComplaint" runat="server" Width="300px"
                                        TextMode="MultiLine" Resize="Vertical" ReadOnly="True" />
                                </td>

                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">Past Medical History
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtPastMedicalHistory" runat="server" Width="300px"
                                        TextMode="MultiLine" Resize="Vertical" ReadOnly="True" />
                                </td>

                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">History Present Illness
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtHpi" runat="server" Width="300px"
                                        TextMode="MultiLine" Resize="Vertical" ReadOnly="True" />
                                </td>

                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">Action / Examination / Treatment
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtActionExamTreatment" runat="server" Width="300px" Height="105px"
                                        TextMode="MultiLine" Resize="Vertical" ReadOnly="True" />
                                </td>

                            </tr>
                            <tr>
                                <td class="label">Consultation Notes
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" Height="105px"
                                        TextMode="MultiLine"  Resize="Vertical" ReadOnly="True" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset runat="server" id="pnlBasicFunction" visible="False">
                        <legend>BASIC FUNCTION EXAMINATION</legend>
                        <table width="100%">
                            <tr>
                                <td class="label">Active
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtActiveMotion" runat="server" Width="300px"
                                        TextMode="MultiLine" Resize="Vertical" ReadOnly="True" />
                                </td>

                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">Passive
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtPassiveMotion" runat="server" Width="300px"
                                        TextMode="MultiLine" Resize="Vertical" ReadOnly="True" />
                                </td>

                                <td></td>
                            </tr>
                        </table>
                    </fieldset>

                </fieldset>
            </td>            
            <td style="width: 50%; vertical-align: top;">
                <table runat="server" id="answerGen" width="100%">
                    <tr>
                        <td class="label">Answer Status
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRConsultAnswerType" runat="server" Width="300px" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Consultation Answer
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAnswer" runat="server" Width="300px" MaxLength="2000" Height="300px"
                                TextMode="MultiLine" Resize="Vertical" />
                        </td>
                        <td></td>
                    </tr>
                </table>
                <fieldset runat="server" id="answerPhy">
                    <legend>PHYSIOTHERAPY OPINION</legend>
                    <table width="100%">
                        <tr>
                            <td class="label">Answer Status
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRConsultAnswerTypePhysiotherapy" runat="server" Width="300px" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Physiotherapy Problems
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtAnswerPhysiotherapy" runat="server" Width="300px" MaxLength="2000"
                                    TextMode="MultiLine" Resize="Vertical" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Physiotherapy Diagnosis
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtAnswerDiagnose" runat="server" Width="300px" MaxLength="1000"
                                    TextMode="MultiLine" Resize="Vertical" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Physiotherapy Action Plan
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtAnswerPlan" runat="server" Width="300px" MaxLength="1000"
                                    TextMode="MultiLine" Resize="Vertical" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Actions and Modalities of FT
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtAnswerAction" runat="server" Width="300px" MaxLength="1000"
                                    TextMode="MultiLine" Resize="Vertical" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</fieldset>

