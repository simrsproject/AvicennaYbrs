<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FollowUpPlanCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.FollowUpPlanCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<fieldset style="width: 49%;">
    <legend>FOLLOW UP PLAN</legend>
    <table style="width: 100%">
        <tr>
            <td class="label">Follow Up Plan</td>
            <td>
                <asp:RadioButtonList ID="optFollowUpPlanType" runat="server" RepeatDirection="Vertical">
                    <asp:ListItem Text="Outpatient" Value="OP"></asp:ListItem>
                    <asp:ListItem Text="Inpatient" Value="IP"></asp:ListItem>
                    <asp:ListItem Text="Refer to Hospital" Value="RH"></asp:ListItem>
                    <asp:ListItem Text="Refer to Puskesmas" Value="RP"></asp:ListItem>
                    <asp:ListItem Text="Clinic" Value="CL"></asp:ListItem>
                    <asp:ListItem Text="Family Doctor" Value="FM"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Consul To</td>
            <td>
                <asp:RadioButtonList ID="optConsulToType" runat="server" RepeatDirection="Vertical">
                    <asp:ListItem Text="Medical Rehabilitation" Value="MDR"></asp:ListItem>
                    <asp:ListItem Text="Nutritionists" Value="NUT"></asp:ListItem>
                    <asp:ListItem Text="Other Specialist" Value="OTH"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td>
                <telerik:RadTextBox ID="txtConsulTo" runat="server" Width="100%" /></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Inpatient Indication</td>
            <td>
                <telerik:RadTextBox ID="txtInpatientIndication" runat="server" Width="100%" Height="100px"
                    TextMode="MultiLine" Resize="Vertical" />

            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Control Plan / Care Plan</td>
            <td>
                <telerik:RadTextBox ID="txtControlPlan" runat="server" Width="100%" />

            </td>
            <td></td>
        </tr>
    </table>
</fieldset>


