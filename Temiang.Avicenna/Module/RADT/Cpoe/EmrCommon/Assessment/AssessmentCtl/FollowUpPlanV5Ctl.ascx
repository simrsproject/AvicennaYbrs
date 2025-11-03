<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FollowUpPlanV5Ctl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.FollowUpPlanV5Ctl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<fieldset style="width: 49%;">
    <legend><b>FOLLOW UP PLAN</b></legend>

    <table style="width: 100%">
        <tr>
            <td class="label">Control Date</td>
            <td>
                <telerik:RadTextBox ID="txtInpatient" runat="server" Width="100%" /></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Consul To</td>
            <td>
                <telerik:RadTextBox ID="txtConsulTo" runat="server" Width="100%" /></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Refer To</td>
            <td>
                <telerik:RadTextBox ID="txtReferTo" runat="server" Width="100%" /></td>
            <td></td>
        </tr>
    </table>
</fieldset>


