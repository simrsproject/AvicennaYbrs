<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FdolmCtl.ascx.cs" Inherits="Temiang.Avicenna.CustomControl.Phr.InputControl.FdolmCtl" %>

<telerik:RadAjaxPanel runat="server" ID="ajxPnlFdolm">
    <table width="100%">
        <tr>
            <td class="label" style="width: 176px;">Hari Pertama Haid Terakhir</td>
            <td style="width: 130px">
                <telerik:RadDatePicker ID="txtFdolm" runat="server" Width="120px" AutoPostBack="true" OnSelectedDateChanged="txtFdolm_SelectedDateChanged">
                </telerik:RadDatePicker>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Perkiraan Lahir</td>
            <td style="width: 150px">
                <telerik:RadDatePicker ID="txtEstBirthDate" runat="server" Width="103px" DatePopupButton-Visible="false" DateInput-ReadOnly="true">
                </telerik:RadDatePicker>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Umur Kehamilan</td>
            <td>
                <asp:Label runat="server" ID="lblPregnantAge"></asp:Label></td>
            <td></td>
        </tr>
    </table>
</telerik:RadAjaxPanel>
