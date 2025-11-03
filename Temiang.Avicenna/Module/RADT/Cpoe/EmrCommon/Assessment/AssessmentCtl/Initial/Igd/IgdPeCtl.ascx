<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IgdPeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.IgdPeCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/GcsCtl.ascx" TagPrefix="uc1" TagName="GcsCtl" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/QuestionCtl.ascx" TagPrefix="uc1" TagName="QuestionCtl" %>

<fieldset style="width: 98%;">
    <legend>PRIMARY SURVEY</legend>
    <table style="width: 100%">
        <tr>
            <td class="label">Triage Category</td>
            <td>
                <telerik:RadDropDownList ID="ddlTriage" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td valign="top" style="width: 50%">
                <uc1:QuestionCtl runat="server" ID="questJalanNapas" IsUseQuestionGroupCaption="True" />
                <uc1:QuestionCtl runat="server" ID="questPernapasan" IsUseQuestionGroupCaption="True" />
                <uc1:QuestionCtl runat="server" ID="questSirkulasi" IsUseQuestionGroupCaption="True" />
            </td>
            <td valign="top" style="width: 50%">
                <uc1:QuestionCtl runat="server" ID="questPenilaianBayi" IsUseQuestionGroupCaption="True" />
                <uc1:QuestionCtl runat="server" ID="questDisabilitas" IsUseQuestionGroupCaption="True" />
                <uc1:QuestionCtl runat="server" ID="questEksposur" IsUseQuestionGroupCaption="True" />
            </td>
        </tr>
    </table>
</fieldset>
<fieldset style="width: 98%;">
    <legend>SECONDARY SURVEY</legend>
    <table style="width: 100%">
        <tr>
            <td style="width: 50%" valign="top">
                <uc1:GcsCtl runat="server" ID="gcsCtl" />
            </td>
            <td valign="top">
                <uc1:QuestionCtl runat="server" ID="questKepalaLeher" IsUseQuestionGroupCaption="True" />
            </td>
        </tr>
        <tr>

            <td style="width: 50%" valign="top">
                <uc1:QuestionCtl runat="server" ID="questThorax" IsUseQuestionGroupCaption="True" />
            </td>
            <td valign="top">
                <uc1:QuestionCtl runat="server" ID="questAbdomenPelvis" IsUseQuestionGroupCaption="True" />
            </td>
        </tr>
        <tr>
            <td style="width: 50%" valign="top">
                <uc1:QuestionCtl runat="server" ID="questOth" IsUseQuestionGroupCaption="True" />
            </td>
            <td valign="top"></td>
        </tr>
    </table>

</fieldset>
<fieldset style="width: 48%;">
    <legend>ANCILLARY EXAM</legend>
    <uc1:QuestionCtl runat="server" ID="questAncillaryExam" IsUseQuestionGroupCaption="True" />
</fieldset>
