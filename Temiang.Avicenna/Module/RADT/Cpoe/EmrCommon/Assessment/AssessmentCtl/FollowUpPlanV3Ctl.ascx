<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FollowUpPlanV3Ctl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.FollowUpPlanV3Ctl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<fieldset style="width: 49%;">
    <legend><b>FOLLOW UP PLAN</b></legend>

    <table style="width: 100%">
        <tr>
            <td class="label">
                <asp:RadioButton runat="server" ID="rbFupNON" Text="None" GroupName="FUP" /></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:RadioButton runat="server" ID="rbFupINP" Text="Inpatient" GroupName="FUP" />
            </td>
            <td style="width: 120px;">Room :</td>
            <td style="width: 320px;">
                <telerik:RadTextBox ID="txtRoom" runat="server" Width="250px" /></td>
            <td style="width: 80px;">&nbsp;&nbsp;Day Estimation</td>
            <td>&nbsp;:&nbsp;
                <telerik:RadTextBox ID="txtDayEst" runat="server" Width="50px" /></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td>DPJP Inpatient :</td>
            <td colspan="3">
                <telerik:RadComboBox runat="server" ID="cboDPjpInPatientID" Width="250px" EmptyMessage="Select a Paramedic"
                    EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true">
                    <WebServiceSettings Method="Paramedics" Path="~/WebService/ComboBoxDataService.asmx" />
                    <ClientItemTemplate>
                            <div>
                                <ul class="details">
                                    <li class="bold">
                                        <span>#= Text # </span>
                                    </li>
                                    <li class="smaller">
                                        <span>#= Attributes.SpecialtyName # </span>
                                    </li>
                                </ul>
                            </div>
                    </ClientItemTemplate>
                </telerik:RadComboBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:RadioButton runat="server" ID="rbFupSUR" Text="Surgical Room" GroupName="FUP" />
            </td>
            <%--            <td colspan="3">Date Time:&nbsp;<telerik:RadDateTimePicker ID="txtSurgicalDateTime" runat="server" Width="130px" />
            </td>--%>
        </tr>

        <tr>
            <td></td>
            <td>Patient Guide</td>
            <td colspan="3">
                <asp:RadioButtonList ID="optIsInPatientGuide" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
            <td></td>
        </tr>

        <tr>
            <td></td>
            <td colspan="4">
                <asp:RadioButton runat="server" ID="rbFupRJT" Text="Inpatient reject with reason" GroupName="FUP" />
                &nbsp;:&nbsp;<telerik:RadTextBox ID="txtInPatientRejectReason" runat="server" Width="250px" />
            </td>
        </tr>
        <tr>
            <td class="label">Refer To
            </td>
            <td>
                <asp:RadioButton runat="server" ID="rbFupRHS" Text="Hospital" GroupName="FUP" />
            </td>
            <td colspan="3">&nbsp;:&nbsp;<telerik:RadTextBox ID="txtReferToHospital" runat="server" Width="250px" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td colspan="4">
                <div style="display: none">
                    <asp:RadioButton runat="server" ID="rbFupRPK" Text="Refer to Puskesmas" GroupName="FUP" /></div>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:RadioButton runat="server" ID="rbFupRFD" Text="Family Doctor" GroupName="FUP" />
            </td>
            <td colspan="3">&nbsp;:&nbsp;<telerik:RadTextBox ID="txtReferToFamilyDoctor" runat="server" Width="250px" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td colspan="4">
                <asp:RadioButton runat="server" ID="rbFupRDT" Text="Doctor" GroupName="FUP" />
                <asp:RadioButton runat="server" ID="rbFupRHC" Text="Homecare" GroupName="FUP" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td>Refer Reason</td>
            <td colspan="3">&nbsp;:&nbsp;<telerik:RadTextBox ID="txtReferReason" runat="server" Width="250px" />
            </td>
        </tr>
        <tr>
            <td class="label">Control / Clinic Consult
            </td>
            <td colspan="4">

                <asp:RadioButton runat="server" ID="rbFupCMR" Text="Medical Rehabilitation" GroupName="FUP" />
                <asp:RadioButton runat="server" ID="rbFupCNT" Text="Nutritionists" GroupName="FUP" />
                <asp:RadioButton runat="server" ID="rbFupCIN" Text="Interna" GroupName="FUP" />
                <asp:RadioButton runat="server" ID="rbFupCPD" Text="Pediatry" GroupName="FUP" />
                <asp:RadioButton runat="server" ID="rbFupCNR" Text="Nursing" GroupName="FUP" />
                <asp:RadioButton runat="server" ID="rbFupCSG" Text="Surgical" GroupName="FUP" />
                <asp:RadioButton runat="server" ID="rbFupCTH" Text="THT" GroupName="FUP" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td colspan="4">
                <asp:RadioButton runat="server" ID="rbFupTET" Text="Teeth" GroupName="FUP" />
                <asp:RadioButton runat="server" ID="rbFupCEY" Text="Eye" GroupName="FUP" />
                <asp:RadioButton runat="server" ID="rbFupPDP" Text="PDP (Perawatan, Dukungan, dan Pengobatan)" GroupName="FUP" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:RadioButton runat="server" ID="rbFupCOT" Text="Other Specialist" GroupName="FUP" /></td>
            <td colspan="3">&nbsp;:&nbsp;<telerik:RadTextBox ID="txtConsulTo" runat="server" Width="250px" />
            </td>
        </tr>
        <tr>
            <td class="label">Date&nbsp;
            </td>
            <td colspan="4">
                <telerik:RadDatePicker ID="txtConsultDate" runat="server" Width="100px" />
            </td>
        </tr>
        <tr runat="server" id="trDOA">
            <td class="label">
                <asp:RadioButton runat="server" ID="rbFupDOA" Text="DOA" GroupName="FUP" />
            </td>
            <td colspan="4">Date Time:&nbsp;<telerik:RadDateTimePicker ID="txtDoaDateTime" runat="server" Width="130px" />
            </td>
        </tr>
    </table>
</fieldset>


