<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PlanActionCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.PlanActionCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<table width="49%">
    <tr>
        <td class="label">Plan / Action / Therapy</td>
        <td>
            <telerik:RadTextBox ID="txtTherapy" runat="server" Width="100%" Height="200px"
                TextMode="MultiLine" Resize="Vertical" />
        </td>
    </tr>

    <tr runat="server" id="rowEstimatedDayInPatient">
        <td class="label">Estimated length of stay (day)</td>
        <td>
            <telerik:RadNumericTextBox ID="txtEstimatedDayInPatient" runat="server" Width="40px" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
        </td>
    </tr>
    <tr runat="server" id="rowPrognosis">
        <td class="label">Prognosis</td>
        <td>
            <telerik:RadTextBox ID="txtPrognosis" runat="server" Width="100%" Height="100px"
                TextMode="MultiLine" Resize="Vertical" />
        </td>
    </tr>
</table>

