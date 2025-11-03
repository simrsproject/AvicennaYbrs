<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="MedicationStatusConfirm.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.MedicationStatusConfirm" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="lblDate" Text="Stop Date"></asp:Label>
            </td>
            <td>
                <telerik:RadDateTimePicker ID="txtStatusDateTime" runat="server" Width="150px">
                    <DateInput ID="DateInput1" runat="server"
                        DisplayDateFormat="dd/MM/yyyy HH:mm"
                        DateFormat="dd/MM/yyyy HH:mm">
                    </DateInput>
                </telerik:RadDateTimePicker>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Reason</td>
            <td>
                <telerik:RadComboBox ID="cboSRMedicationStatusReason" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Additional Reason</td>
            <td>
                <telerik:RadTextBox ID="txtMedicationReason" runat="server" TextMode="MultiLine" Height="100px" Width="300px" />
            </td>
            <td></td>
        </tr>
                <tr>
            <td class="label">Reason</td>
            <td>
                <telerik:RadComboBox ID="RadComboBox1" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td>
                <asp:CheckBox ID="chkIsApplyInUddItem" runat="server" Text="Also apply in UDD Item Template"/>
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
