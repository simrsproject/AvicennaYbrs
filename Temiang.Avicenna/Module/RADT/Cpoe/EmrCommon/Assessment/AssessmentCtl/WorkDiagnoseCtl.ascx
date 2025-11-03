<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkDiagnoseCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.WorkDiagnoseCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/RegInMedDiagnoseCtl.ascx" TagPrefix="uc1" TagName="RegInMedDiagnoseCtl" %>

<telerik:RadAjaxManagerProxy ID="ajxProxy" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="grdDiagnose">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdDiagnose" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<fieldset style="width: 49%;">
    <legend><b>WORK DIAGNOSE</b></legend>
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
        <legend>ICD 10</legend>
        <uc1:RegInMedDiagnoseCtl runat="server" ID="regInMedDiagnoseCtl" />
    </fieldset>
</fieldset>

