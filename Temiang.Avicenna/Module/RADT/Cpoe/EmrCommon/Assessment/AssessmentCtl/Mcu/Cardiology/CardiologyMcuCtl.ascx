<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CardiologyMcuCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.CardiologyMcuCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/QuestionCtl.ascx" TagPrefix="uc1" TagName="QuestionCtl" %>

<fieldset style="width: 98%;">
    <legend>EXAMINATION</legend>
    <table style="width: 100%">
        <tr>
            <td>
                <uc1:QuestionCtl runat="server" ID="questLeher" IsUseQuestionGroupCaption="True" />
            </td>
        </tr>
        <tr>
            <td>
                <uc1:QuestionCtl runat="server" ID="questToraks" IsUseQuestionGroupCaption="True" />
            </td>
        </tr>
        <tr>
            <td>
                <uc1:QuestionCtl runat="server" ID="questPerut" IsUseQuestionGroupCaption="True" />
            </td>
        </tr>
        <tr>
            <td>
                <uc1:QuestionCtl runat="server" ID="questExtremitas" IsUseQuestionGroupCaption="True" />
            </td>
        </tr>
        <tr>
            <td>
                <uc1:QuestionCtl runat="server" ID="questOthers" IsUseQuestionGroupCaption="True" />
            </td>
        </tr>
    </table>
</fieldset>