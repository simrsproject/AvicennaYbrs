<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PrmrjConfirmEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.PrmrjConfirmEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <legend>This patient has been diagnosed with Chronic Disease</legend>
        <asp:Label runat="server" ID="lblChronicDisease" BackColor="Black" ForeColor="Yellow" Font-Size="Large"  Width="100%"></asp:Label>
    </fieldset>
    <div style="height:4px;"></div>

    <fieldset>
        <legend>PRMRJ Follow Up</legend>
        <table width="100%">
            <tr>
                <td class="label">Important clinical notes
                </td>
                <td>
                    <telerik:RadTextBox ID="txtImportantclinicalNotes" runat="server" Width="99%" TextMode="MultiLine" Height="40px" Resize="Vertical" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Planning
                </td>
                <td>
                    <telerik:RadTextBox ID="txtPlanning" runat="server" Width="99%" TextMode="MultiLine" Height="40px" Resize="Vertical" />
                </td>
            </tr>
            <tr>
                <td class="label">Remark
                </td>
                <td>
                    <telerik:RadTextBox ID="txtRemark" runat="server" Width="99%" TextMode="MultiLine" Height="40px" Resize="Vertical" />

                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
