<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DischargePlanCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.DischargePlanCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<fieldset>
    <legend><b>DISCHARGE PLANNING</b></legend>
    <table width="49%">
        <tr >
            <td class="label">Estimated length of stay (day)</td>
            <td>
                <telerik:RadNumericTextBox ID="txtEstimatedDayInPatient" runat="server" Width="40px" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
            </td>
        </tr>        
        <tr >
            <td class="label">Discharge Date Plan</td>
            <td>
                <telerik:RadDatePicker ID="txtDischargeDatePlan" runat="server" Width="100px" />
            </td>
        </tr>
        <tr>
            <td class="label">Medical Action Plan</td>
            <td>
                <telerik:RadTextBox ID="txtDischargeMedicalPlan" runat="server" Width="100%" Height="200px"
                    TextMode="MultiLine" Resize="Vertical" />
            </td>
        </tr>
    </table>
</fieldset>
