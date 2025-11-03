<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DentisPeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.DentisPeCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<table width="100%">
    <tr>
        <td class="label">Extra Oral</td>
        <td>
            <telerik:RadTextBox ID="txtExtraOral" runat="server" Width="100%" />
        </td>
    </tr>
</table>
<fieldset>
    <legend>Intra Oral</legend>
    <table style="width: 100%;">
        <tr>
            <td class="label">•	Lip</td>
            <td>
                <telerik:RadTextBox ID="txtBibir" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">•	Palatum</td>
            <td>
                <telerik:RadTextBox ID="txtPalatum" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">•	Tongue</td>
            <td>
                <telerik:RadTextBox ID="txtLidah" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">•	Floor of the mouth</td>
            <td>
                <telerik:RadTextBox ID="txtDasarMulut" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">•	Vestibulum</td>
            <td>
                <telerik:RadTextBox ID="txtVestibulum" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">•	Ginggiva</td>
            <td>
                <telerik:RadTextBox ID="txtGinggiva" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">•	MukosaBukal</td>
            <td>
                <telerik:RadTextBox ID="txtMukosaBukal" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">•	MukosaLingual</td>
            <td>
                <telerik:RadTextBox ID="txtMukosaLingual" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">•	Tonsil</td>
            <td>
                <telerik:RadTextBox ID="txtTonsil" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">•	Teeth</td>
            <td>
                <telerik:RadTextBox ID="txtTeeth" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">•	Other</td>
            <td>
                <telerik:RadTextBox ID="txtIntraOralOther" runat="server" Width="100%" />
            </td>
        </tr>
    </table>
</fieldset>
<table style="width: 100%;">
    <tr id="trNutriSkrinning" runat="server" visible="false">
        <td class="label">Nutrition Skrinning</td>
        <td colspan="2">
            <asp:RadioButtonList ID="optNutritionSkrinning" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Good" Value="Good"></asp:ListItem>
                <asp:ListItem Text="Medium" Value="Medium"></asp:ListItem>
                <asp:ListItem Text="Malnutrition" Value="Malnutrition"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td class="label">Action and Therapy</td>
        <td>
            <telerik:RadTextBox ID="txtActionAndTherapy" runat="server" Width="100%" Height="80px"
                TextMode="MultiLine" Resize="Vertical" />
        </td>
    </tr>
    <tr>
        <td class="label">Planning</td>
        <td>
            <telerik:RadTextBox ID="txtPlanning" runat="server" Width="100%" Height="80px"
                TextMode="MultiLine" Resize="Vertical" />
        </td>
    </tr>
    <tr>
        <td class="label">Notes</td>
        <td>
            <telerik:RadTextBox ID="txtPhysicalExamNotes" runat="server" Width="100%" Height="80px"
                TextMode="MultiLine" Resize="Vertical" />
        </td>
    </tr>
</table>
