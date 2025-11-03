<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DentisPeV2Ctl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.DentisPeV2Ctl" %>
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

