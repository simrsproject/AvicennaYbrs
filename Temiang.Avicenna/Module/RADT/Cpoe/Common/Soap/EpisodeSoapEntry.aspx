<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="EpisodeSoapEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.EpisodeSoapEntry" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label" style="font-style: italic">Date
            </td>
            <td>
                <telerik:RadDatePicker ID="txtDateSOAP" runat="server" Enabled="False" Width="100px" />
                <telerik:RadTimePicker ID="txtTimeSOAP" runat="server" Width="93px" />
            </td>
        </tr>
        <tr>
            <td class="label" style="font-style: italic">Subjective
            </td>
            <td>
                <telerik:RadTextBox ID="txtSubjective" runat="server" Width="99%" TextMode="MultiLine" />
            </td>
        </tr>
        <tr>
            <td class="label" style="font-style: italic">Objective
            </td>
            <td>
                <telerik:RadTextBox ID="txtObjective" runat="server" Width="99%" TextMode="MultiLine" />
            </td>
        </tr>
        <tr>
            <td class="label" style="font-style: italic">Assessment / Diagnosis
            </td>
            <td>
                <telerik:RadTextBox ID="txtAssesment" runat="server" Width="99%" TextMode="MultiLine" />
            </td>
        </tr>
        <tr>
            <td class="label" style="font-style: italic">Planning
            </td>
            <td>
                <telerik:RadTextBox ID="txtPlanning" runat="server" Width="99%" TextMode="MultiLine" />
            </td>
        </tr>
        <tr>
            <td class="label" style="font-style: italic">Notes
            </td>
            <td>
                <telerik:RadTextBox ID="txtAttendingNotes" runat="server" Width="99%" />
            </td>
        </tr>
        <tr>
            <td class="label" style="font-style: italic"></td>
            <td>
                <asp:CheckBox ID="chkIsInformConcern" runat="server" Text="Inform Concern" />
            </td>
        </tr>
    </table>

</asp:Content>
