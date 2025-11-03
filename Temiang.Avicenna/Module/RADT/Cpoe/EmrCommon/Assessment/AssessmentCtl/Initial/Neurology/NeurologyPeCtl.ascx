<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NeurologyPeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.NeurologyPeCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>


    <table style="width: 100%;">
        <tr>
            <td class="label">Neurologis
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNeurologis" runat="server" Width="100%" TextMode="MultiLine" Height="100px"  Resize="Vertical"/>
            </td>
        </tr>

        <tr>
            <td class="label">Notes
            </td>
            <td class="entry" >
                <telerik:RadTextBox ID="txtPhysicalExamNotes" runat="server" Width="100%" Height="80px"
                    TextMode="MultiLine" Resize="Vertical" />
            </td>
        </tr>
    </table>

