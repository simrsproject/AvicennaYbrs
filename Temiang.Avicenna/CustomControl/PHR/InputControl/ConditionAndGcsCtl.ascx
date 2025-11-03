<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConditionAndGcsCtl.ascx.cs" Inherits="Temiang.Avicenna.CustomControl.Phr.InputControl.ConditionAndGcsCtl" %>
<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="optCondition">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtConsciousness" />
                <telerik:AjaxUpdatedControl ControlID="ddlGcsEye" />
                <telerik:AjaxUpdatedControl ControlID="ddlGcsMotor" />
                <telerik:AjaxUpdatedControl ControlID="ddlGcsVerbal" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="ddlGcsEye">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtConsciousness" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="ddlGcsMotor">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtConsciousness" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="ddlGcsVerbal">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtConsciousness" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<table width="100%">
    <tr>
        <td colspan="4">
            <asp:RadioButtonList ID="optCondition" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="optCondition_OnSelectedIndexChanged">
                <asp:ListItem Text="Mild" Value="Mild" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Moderate" Value="Moderate"></asp:ListItem>
                <asp:ListItem Text="Severe" Value="Severe"></asp:ListItem>
                <asp:ListItem Text="DOA" Value="DOA"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td class="labelcaption" style="width: 100px">Eye Opening</td>
        <td class="labelcaption" style="width: 150px">Best Motor</td>
        <td class="labelcaption" style="width: 150px">Best Verbal</td>
        <td></td>
    </tr>
    <tr>
        <td class="label">GCS</td>
        <td>
            <telerik:RadDropDownList ID="ddlGcsEye" runat="server" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlGcs_SelectedIndexChanged" />
        </td>
        <td>
            <telerik:RadDropDownList ID="ddlGcsMotor" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddlGcs_SelectedIndexChanged" />
        </td>
        <td>
            <telerik:RadDropDownList ID="ddlGcsVerbal" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddlGcs_SelectedIndexChanged" />
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">Conscious</td>
        <td colspan="3">
            <telerik:RadTextBox ID="txtConsciousness" runat="server" Width="100%" ReadOnly="True" />
        </td>
        <td></td>
    </tr>
</table>
