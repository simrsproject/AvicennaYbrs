<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ThtMcuCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.ThtMcuCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/QuestionCtl.ascx" TagPrefix="uc1" TagName="QuestionCtl" %>

<fieldset style="width: 98%;">
    <legend>EXAMINATION</legend>
    <table style="width: 100%">
        <tr>
            <td class="labelcaption" style="width: 20%;">TELINGA</td>
            <td class="labelcaption">KANAN</td>
            <td class="labelcaption">KIRI</td>
        </tr>
        <tr>
            <td class="labelcaption" >Daun Telinga</td>
            <td>
                <telerik:RadTextBox ID="txtDaunTelingaKanan" runat="server" Width="100%" />
            <td>
                <telerik:RadTextBox ID="txtDaunTelingaKiri" runat="server" Width="100%" /></td>
        </tr>
        <tr>
            <td class="labelcaption" >Liang Telinga</td>
            <td>
                <telerik:RadTextBox ID="txtLiangTelingaKanan" runat="server" Width="100%" />
            <td>
                <telerik:RadTextBox ID="txtLiangTelingaKiri" runat="server" Width="100%" /></td>
        </tr>
        <tr>
            <td class="labelcaption" >Membran Tympani</td>
            <td>
                <telerik:RadTextBox ID="txtMembranTympaniKanan" runat="server" Width="100%" />
            <td>
                <telerik:RadTextBox ID="txtMembranTympaniKiri" runat="server" Width="100%" /></td>
        </tr>
        <tr>
            <td class="labelcaption" >Audiogram</td>
            <td>
                <telerik:RadTextBox ID="txtAudiogramKanan" runat="server" Width="100%" />
            <td>
                <telerik:RadTextBox ID="txtAudiogramKiri" runat="server" Width="100%" /></td>
        </tr>
        <tr>
            <td colspan="3">
                <uc1:QuestionCtl runat="server" ID="questTht" IsUseQuestionGroupCaption="True" />
            </td>
        </tr>
    </table>
</fieldset>
