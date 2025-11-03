<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DermaMcuCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.DermaMcuCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/QuestionCtl.ascx" TagPrefix="uc1" TagName="QuestionCtl" %>

<fieldset style="width: 98%;">
    <legend>EXAMINATION</legend>
    <table style="width: 100%">
        <tr>
            <td style="width: 50%">
                <uc1:QuestionCtl runat="server" ID="questAnamnesa" IsUseQuestionGroupCaption="True" />
            </td>
            <td>
                <uc1:QuestionCtl runat="server" ID="questKawin" IsUseQuestionGroupCaption="True" />
                <uc1:QuestionCtl runat="server" ID="questAbortus" IsUseQuestionGroupCaption="True" />
                <uc1:QuestionCtl runat="server" ID="questOthers" IsUseQuestionGroupCaption="True" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <uc1:QuestionCtl runat="server" ID="questPemeriksaan" IsUseQuestionGroupCaption="True" />
            </td>
        </tr>
    </table>
</fieldset>