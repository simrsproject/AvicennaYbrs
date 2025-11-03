<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="MedicationNotAppropriateConfirm.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.MedicationNotAppropriateConfirm" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">Reason</td>
            <td>
                <telerik:RadComboBox ID="cboSRMedicationNotAppropriate" runat="server" Width="300px"/>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Additional Reason</td>
            <td>
                <telerik:RadTextBox ID="txtMedicationNotAppropriateReason" runat="server" TextMode="MultiLine" Height="100px" Width="300px" />
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
