<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TriageEsiCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.TriageEsiCtl" %>
<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="ddlTriage">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtAirway" />
                <telerik:AjaxUpdatedControl ControlID="txtBreathing" />
                <telerik:AjaxUpdatedControl ControlID="txtCirculation" />
                <telerik:AjaxUpdatedControl ControlID="txtConscious" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<table width="100%">
    <tr>
        <td class="label">TRIAGE 5 LEVEL</td>
        <td colspan="3">
            <telerik:RadDropDownList ID="ddlTriage" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlTriage_SelectedIndexChanged" />
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">Airway</td>
        <td colspan="3">
            <telerik:RadTextBox ID="txtAirway" runat="server" Width="100%" ReadOnly="True" />
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">Breathing</td>
        <td colspan="3">
            <telerik:RadTextBox ID="txtBreathing" runat="server" Width="100%" ReadOnly="True" />
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">Circulation</td>
        <td colspan="3">
            <telerik:RadTextBox ID="txtCirculation" runat="server" Width="100%" ReadOnly="True" />
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">Conscious</td>
        <td colspan="3">
            <telerik:RadTextBox ID="txtConscious" runat="server" Width="100%" ReadOnly="True" />
        </td>
        <td></td>
    </tr>
</table>