<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuestionPeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.QuestionPeCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/QuestionCtl.ascx" TagPrefix="uc1" TagName="QuestionCtl" %>

<fieldset style="width: 98%;">
    <legend>EXAMINATION</legend>
    <uc1:QuestionCtl runat="server" ID="peCtl" IsUseQuestionGroupCaption="False" />
</fieldset>
