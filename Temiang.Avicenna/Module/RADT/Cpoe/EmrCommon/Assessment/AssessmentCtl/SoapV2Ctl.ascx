<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SoapV2Ctl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.SoapV2Ctl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/EpisodeDiagnoseCtl.ascx" TagPrefix="uc1" TagName="EpisodeDiagnoseCtl" %>


<fieldset style="width: 49%;" runat="server" ID="SubjectiveText">
    <legend>SUBJECTIVE</legend>
    <telerik:RadTextBox ID="txtSubjective" runat="server" Width="100%" Height="80px"
        TextMode="MultiLine" Resize="Vertical" />
</fieldset>
<fieldset style="width: 49%;">
    <legend>OBJECTIVE</legend>
    <telerik:RadTextBox ID="txtObjective" runat="server" Width="100%" Height="80px"
        TextMode="MultiLine" Resize="Vertical" />
</fieldset>
<fieldset style="width: 49%;" runat="server" ID="AssessmentText">
    <legend>ASSESSMENT</legend>
    <telerik:RadTextBox ID="txtAssessment" runat="server" Width="100%" Height="80px"
        TextMode="MultiLine" Resize="Vertical" />
</fieldset>
<fieldset style="width: 49%;">
    <legend>Diagnosis / Assessment </legend>
    <uc1:EpisodeDiagnoseCtl runat="server" id="epDiagCtl" />
    </fieldset>
<fieldset style="width: 49%;">
    <legend>PLANNING</legend>
    <telerik:RadTextBox ID="txtPlanning" runat="server" Width="100%" Height="80px"
        TextMode="MultiLine" Resize="Vertical" />
</fieldset>
