<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ObgynMcuCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.ObgynMcuCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/QuestionCtl.ascx" TagPrefix="uc1" TagName="QuestionCtl" %>

<fieldset style="width: 98%;">
    <legend>EXAMINATION</legend>
    <table style="width: 100%">
        <tr>
            <td>
                <uc1:QuestionCtl runat="server" ID="questAnamnesa" IsUseQuestionGroupCaption="True" />
            </td>
        </tr>
        <tr>
            <td>
                <uc1:QuestionCtl runat="server" ID="questRiwayatObs" IsUseQuestionGroupCaption="True" />
            </td>
        </tr>
        <tr>
            <td>
                <uc1:QuestionCtl runat="server" ID="questPemeriksaan" IsUseQuestionGroupCaption="True" />
            </td>
        </tr>
    </table>
</fieldset>