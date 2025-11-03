<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SurgeryFemaleMcuCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.SurgeryFemaleMcuCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/QuestionCtl.ascx" TagPrefix="uc1" TagName="QuestionCtl" %>

<fieldset style="width: 98%;">
    <legend>EXAMINATION</legend>
    <table style="width: 100%">
        <tr>
            <td>
                <uc1:QuestionCtl runat="server" ID="questExamination" IsUseQuestionGroupCaption="True" />
            </td>
        </tr>
        <tr>
            <td>
                <div class="divcaption">Status Lokalis</div>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:QuestionCtl runat="server" ID="questExamination2" IsUseQuestionGroupCaption="True" />
            </td>
        </tr>
    </table>
</fieldset>