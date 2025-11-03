<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContinuedCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.ContinuedCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<fieldset style="width: 49%;">
    <legend>PHYSICAL EXAM</legend>
    <telerik:RadTextBox ID="txtPhysicalExam" runat="server" Width="100%" Height="80px"
        TextMode="MultiLine" Resize="Vertical" />
</fieldset>
<fieldset style="width: 49%;">
    <legend>ANCILLARY EXAMINATION</legend>
    <telerik:RadTextBox ID="txtOtherExam" runat="server" Width="100%" Height="80px"
        TextMode="MultiLine" Resize="Vertical" />
</fieldset>