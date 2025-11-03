<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PhysicalExamFreeTextCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.PhysicalExamFreeTextCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<fieldset style="width: 49%;">
    <legend>PHYSICAL EXAMINATION</legend>
    <telerik:RadTextBox ID="txtPhysicalExam" runat="server" Width="100%" Height="80px"
                        TextMode="MultiLine" Resize="Vertical" />
</fieldset>
