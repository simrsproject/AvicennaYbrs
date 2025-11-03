<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SpiroMcuCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.SpiroMcuCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/QuestionCtl.ascx" TagPrefix="uc1" TagName="QuestionCtl" %>

<fieldset style="width: 98%;">
    <legend>EXAMINATION</legend>
    <table style="width: 100%">
        <tr>
            <td class="labelcaption" >Keterangan Klinik</td>
            <td colspan="3">
                <telerik:RadTextBox ID="txtKeteranganKlinis" runat="server" Width="100%" TextMode="MultiLine" Height="50px" />
        </tr>
        <tr>
            <td class="labelcaption" style="width: 15%;">HASIL PEMERIKSAAN</td>
            <td class="labelcaption" style="width: 30%;">MEAS</td>
            <td class="labelcaption" style="width: 30%;">PR</td>
            <td class="labelcaption" style="width: 25%;">% PR</td>
        </tr>
        <tr>
            <td class="labelcaption" >FCV</td>
            <td>
                <telerik:RadTextBox ID="txtFcvMeas" runat="server" Width="100%" />
            <td>
                <telerik:RadTextBox ID="txtFcvPr" runat="server" Width="100%" /></td>
            <td>
                <telerik:RadNumericTextBox ID="txtFcvPrNum" runat="server" Width="100%" /></td>
        </tr>
        <tr>
            <td class="labelcaption" >FCV1</td>
            <td>
                <telerik:RadTextBox ID="txtFcv1Meas" runat="server" Width="100%" />
            <td>
                <telerik:RadTextBox ID="txtFcv1Pr" runat="server" Width="100%" /></td>
            <td>
                <telerik:RadNumericTextBox ID="txtFcv1PrNum" runat="server" Width="100%" /></td>
        </tr>
        <tr>
            <td class="labelcaption" >FCV1/FVC %</td>
            <td>
                <telerik:RadTextBox ID="txtFcv2Meas" runat="server" Width="100%" />
            <td>
                <telerik:RadTextBox ID="txtFcv2Pr" runat="server" Width="100%" /></td>
            <td>
                <telerik:RadNumericTextBox ID="txtFcv2PrNum" runat="server" Width="100%" /></td>
        </tr>
        <tr>
            <td class="labelcaption" >FEF 25-75</td>
            <td>
                <telerik:RadTextBox ID="txtFef2Meas" runat="server" Width="100%" />
            <td>
                <telerik:RadTextBox ID="txtFef2Pr" runat="server" Width="100%" /></td>
            <td>
                <telerik:RadNumericTextBox ID="txtFef2PrNum" runat="server" Width="100%" /></td>
        </tr>
        <tr>
            <td class="labelcaption" >FEF</td>
            <td>
                <telerik:RadTextBox ID="txtFefMeas" runat="server" Width="100%" />
            <td>
                <telerik:RadTextBox ID="txtFefPr" runat="server" Width="100%" /></td>
            <td>
                <telerik:RadNumericTextBox ID="txtFefPrNum" runat="server" Width="100%" /></td>
        </tr>
        <tr>
            <td class="labelcaption" >FEF 25 %</td>
            <td>
                <telerik:RadTextBox ID="txtFef25Meas" runat="server" Width="100%" />
            <td>
                <telerik:RadTextBox ID="txtFef25Pr" runat="server" Width="100%" /></td>
            <td>
                <telerik:RadNumericTextBox ID="txtFef25PrNum" runat="server" Width="100%" /></td>
        </tr>
        <tr>
            <td class="labelcaption" >FEF 50 %</td>
            <td>
                <telerik:RadTextBox ID="txtFef50Meas" runat="server" Width="100%" />
            <td>
                <telerik:RadTextBox ID="txtFef50Pr" runat="server" Width="100%" /></td>
            <td>
                <telerik:RadNumericTextBox ID="txtFef50PrNum" runat="server" Width="100%" /></td>
        </tr>
        <tr>
            <td class="labelcaption" >FEF 75 %</td>
            <td>
                <telerik:RadTextBox ID="txtFef75Meas" runat="server" Width="100%" />
            <td>
                <telerik:RadTextBox ID="txtFef75Pr" runat="server" Width="100%" /></td>
            <td>
                <telerik:RadNumericTextBox ID="txtFef75PrNum" runat="server" Width="100%" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <uc1:QuestionCtl runat="server" ID="questKesan" IsUseQuestionGroupCaption="True" />
            </td>
        </tr>
    </table>
</fieldset>