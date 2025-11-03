<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PhysicalExamMethodCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.PhysicalExamMethodCtl" %>
<table width="100%">
    <tr>
        <td class="label">Inspection
        </td>
        <td>
            <telerik:RadTextBox ID="txtInspeksi" runat="server" Width="100%" Height="40px"
                TextMode="MultiLine" />
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">Palpation
        </td>
        <td>
            <telerik:RadTextBox ID="txtPalpasi" runat="server" Width="100%" Height="40px"
                TextMode="MultiLine" />
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">Percussion
        </td>
        <td>
            <telerik:RadTextBox ID="txtPerkusi" runat="server" Width="100%" Height="40px"
                TextMode="MultiLine" />
        </td>
    </tr>
    <tr>
        <td class="label">Auscultation
        </td>
        <td>
            <telerik:RadTextBox ID="txtAuskultasi" runat="server" Width="100%" Height="40px"
                TextMode="MultiLine" />
        </td>
    </tr>
</table>
