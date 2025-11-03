<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DiagnoseCtl2.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.DiagnoseCtl2" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/EpisodeDiagnoseCtl.ascx" TagPrefix="uc1" TagName="EpisodeDiagnoseCtl" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<fieldset style="width: 49%;">
    <legend><b>DIAGNOSE</b></legend>
    <telerik:RadTextBox ID="txtDiagnose" runat="server" Width="100%" Height="40px"
        TextMode="MultiLine" Resize="Vertical" />    
</fieldset>

