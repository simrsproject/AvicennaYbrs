<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssessmentQuestFieldCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.AssessmentQuestFieldCtl" %>
<%@ Register TagPrefix="uc1" TagName="QuestionCtl" Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/QuestionCtl.ascx" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<uc1:QuestionCtl runat="server" ID="quest" IsUseQuestionGroupCaption="False" />