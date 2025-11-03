<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DiagnoseTherapyCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.DiagnoseTherapyCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/EpisodeDiagnoseCtl.ascx" TagPrefix="uc1" TagName="EpisodeDiagnoseCtl" %>


<fieldset style="width: 49%;">
    <legend>DIAGNOSE</legend>
    <telerik:RadTextBox ID="txtDiagnose" runat="server" Width="100%" Height="40px"
        TextMode="MultiLine" Resize="Vertical" />
    <fieldset>
        <legend>ICD 10</legend>
        <uc1:EpisodeDiagnoseCtl runat="server" id="epDiagCtl" />
        </fieldset>
</fieldset>

<table width="49%">
    <tr>
        <td class="label">PLAN / ACTION / THERAPY</td>
        <td>
            <telerik:RadTextBox ID="txtTherapy" runat="server" Width="100%" Height="100px"
                TextMode="MultiLine" Resize="Vertical" />
        </td>
    </tr>
    <tr runat="server" id="rowDischargeMethod">
        <td class="label">Discharge Method</td>
        <td>
            <telerik:RadComboBox ID="cboSRDischargeMethod" runat="server" Width="100%">
            </telerik:RadComboBox>
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

