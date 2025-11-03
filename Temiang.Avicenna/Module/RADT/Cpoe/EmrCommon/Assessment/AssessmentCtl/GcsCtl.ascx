<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GcsCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.GcsCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%--<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
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
</telerik:RadAjaxManagerProxy>--%>



<telerik:RadAjaxPanel runat="server" ID="ajxPnl">
    <table width="100%">
        <tr>
            <td class="label">General Condition</td>
            <td colspan="3">
                <asp:RadioButtonList ID="optCondition" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="optCondition_OnSelectedIndexChanged">
                    <asp:ListItem Text="Mild" Value="Mild"></asp:ListItem>
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
        <tr>
            <td class="label"></td>
            <td colspan="3">
                <telerik:RadTextBox ID="txtConsciousnessNote" runat="server" Width="100%" />
            </td>
            <td></td>
        </tr>
        <tr id="trPainScale" runat="server">
            <td class="label">Pain Scale</td>
            <td colspan="3">
                <asp:RadioButtonList ID="optPainScale" runat="server" RepeatDirection="Horizontal" Width="400px">
                    <asp:ListItem Text="0" Value="0">0<img class="ps00"/></asp:ListItem>
                    <asp:ListItem Text="1" Value="1">1<img class="ps01"/></asp:ListItem>
                    <asp:ListItem Text="2" Value="2">2<img class="ps02"/></asp:ListItem>
                    <asp:ListItem Text="3" Value="3">3<img class="ps03"/></asp:ListItem>
                    <asp:ListItem Text="4" Value="4">4<img class="ps04"/></asp:ListItem>
                    <asp:ListItem Text="5" Value="5">5<img class="ps05"/></asp:ListItem>
                    <asp:ListItem Text="6" Value="6">6<img class="ps06"/></asp:ListItem>
                    <asp:ListItem Text="7" Value="7">7<img class="ps07"/></asp:ListItem>
                    <asp:ListItem Text="8" Value="8">8<img class="ps08"/></asp:ListItem>
                    <asp:ListItem Text="9" Value="9">9<img class="ps09"/></asp:ListItem>
                    <asp:ListItem Text="10" Value="10">10<img class="ps10"/></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td></td>
        </tr>
        <tr id="trFlacc" runat="server" visible="false">
            <td class="label">FLACC</td>
            <td colspan="3">
                <table width="100%">
                    <tr>
                        <td class="label">Face</td>
                        <td>
                            <telerik:RadDropDownList ID="ddlFlaccFace" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Legs</td>
                        <td>
                            <telerik:RadDropDownList ID="ddlFlaccLegs" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Activity</td>
                        <td>
                            <telerik:RadDropDownList ID="ddlFlaccActivity" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Cry</td>
                        <td>
                            <telerik:RadDropDownList ID="ddlFlaccCry" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Consolability</td>
                        <td>
                            <telerik:RadDropDownList ID="ddlFlaccConsolability" runat="server" Width="100%" />
                        </td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
        <tr style="display: none">
            <td class="label">Pain Scale</td>
            <td>
                <telerik:RadDropDownList ID="ddlSRPainScaleType" runat="server" Width="150px" />
            </td>
            <td>
                <telerik:RadNumericTextBox ID="txtPainScale" runat="server" Width="30px" NumberFormat-DecimalDigits="0" />
            </td>
            <td></td>
        </tr>
    </table>
</telerik:RadAjaxPanel>
