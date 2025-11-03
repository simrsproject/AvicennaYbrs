<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PlanActionTherapyCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.PlanActionTherapyCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/QuestionCtl.ascx" TagPrefix="uc1" TagName="QuestionCtl" %>

<uc1:QuestionCtl runat="server" ID="questPlanning" IsUseQuestionGroupCaption="True" Visible="true"/>

<table runat="server" id="tblTherapyFreeText" width="49%">
    <tr>
        <td class="label">Plan / Action / Therapy</td>
        <td>
            <telerik:RadTextBox ID="txtTherapy" runat="server" Width="100%" Height="200px"
                TextMode="MultiLine" Resize="Vertical" />
        </td>
    </tr>
</table>

