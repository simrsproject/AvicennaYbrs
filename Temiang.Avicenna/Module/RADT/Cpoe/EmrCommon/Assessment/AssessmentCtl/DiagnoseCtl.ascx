<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DiagnoseCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.DiagnoseCtl" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/EpisodeDiagnoseCtl.ascx" TagPrefix="uc1" TagName="EpisodeDiagnoseCtl" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<fieldset style="width: 49%;" runat="server" id="diagnoseText">
    <legend><b>Diagnose</b></legend>
    <telerik:RadTextBox ID="txtDiagnose" runat="server" Width="100%" Height="40px"
        TextMode="MultiLine" Resize="Vertical" />
</fieldset>
<fieldset style="width: 49%;" runat="server" id="diagnoseDiffText">
    <legend><b>Differential Diagnose</b></legend>
    <telerik:RadTextBox ID="txtDiagnoseDiff" runat="server" Width="100%" Height="40px"
        TextMode="MultiLine" Resize="Vertical" />
</fieldset>
<fieldset>
    <legend><b>ICD 10</b></legend>
    <uc1:EpisodeDiagnoseCtl runat="server" ID="epDiagCtl" />
</fieldset>

