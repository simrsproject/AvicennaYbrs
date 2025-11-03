<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InternaMcuCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.InternaMcuCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/QuestionCtl.ascx" TagPrefix="uc1" TagName="QuestionCtl" %>

<fieldset style="width: 98%;">
    <legend>EXAMINATION</legend>
    <table style="width: 100%">
        <tr>
            <td>
                <uc1:QuestionCtl runat="server" ID="questInterna" IsUseQuestionGroupCaption="True" />
            </td>
            <tr>
            <td>
                <div class="divcaption">Status Lokalis</div>
            </td>
        </tr>
            <td>
                <uc1:QuestionCtl runat="server" ID="questInterna2" IsUseQuestionGroupCaption="True" />
            </td>
        </tr>
    </table>
</fieldset>
