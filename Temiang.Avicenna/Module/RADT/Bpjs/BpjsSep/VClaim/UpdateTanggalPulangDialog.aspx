<%@  Title="Update Tanggal Pulang" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="UpdateTanggalPulangDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.VClaim.UpdateTanggalPulangDialog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboStatusPulang">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTanggalMeninggal" />
                    <telerik:AjaxUpdatedControl ControlID="txtNoSuratMeninggal" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table>
        <tr>
            <td class="label">No SEP</td>
            <td class="entry">
                <telerik:RadTextBox runat="server" ID="txtNoSep" Width="300px" ReadOnly="true"></telerik:RadTextBox></td>
            <td width="20px"></td>
            <td />
        </tr>
        <tr>
            <td class="label">Tanggal Pulang</td>
            <td class="entry">
                <telerik:RadDatePicker runat="server" ID="txtTglPulang" Width="100px"></telerik:RadDatePicker>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvTglPulang" runat="server" ErrorMessage="Tanggal Pulang required."
                    ValidationGroup="entry" ControlToValidate="txtTglPulang" SetFocusOnError="True">
                    <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">Status Pulang</td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboStatusPulang" Width="304px" AutoPostBack="true" OnSelectedIndexChanged="cboStatusPulang_SelectedIndexChanged">
                    <Items>
                        <telerik:RadComboBoxItem Text="" Value="" />
                        <telerik:RadComboBoxItem Value="1" Text="Atas Persetujuan Dokter" />
                        <telerik:RadComboBoxItem Value="3" Text="Atas Permintaan Sendiri" />
                        <telerik:RadComboBoxItem Value="4" Text="Meninggal" />
                        <telerik:RadComboBoxItem Value="5" Text="Lain-lain" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvStatusPulang" runat="server" ErrorMessage="Status Pulang required."
                    ValidationGroup="entry" ControlToValidate="cboStatusPulang" SetFocusOnError="True">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">Tanggal Meninggal</td>
            <td class="entry">
                <telerik:RadDatePicker runat="server" ID="txtTanggalMeninggal" Width="100px" DateInput-ReadOnly="true" DatePopupButton-Enabled="false"></telerik:RadDatePicker>
            </td>
            <td width="20px"></td>
            <td />
        </tr>
        <tr>
            <td class="label">No Surat Meninggal</td>
            <td class="entry">
                <telerik:RadTextBox runat="server" ID="txtNoSuratMeninggal" Width="300px" ReadOnly="true"></telerik:RadTextBox></td>
            <td width="20px"></td>
            <td />
        </tr>
        <tr>
            <td class="label">No LP</td>
            <td class="entry">
                <telerik:RadTextBox runat="server" ID="txtNoLP" Width="300px"></telerik:RadTextBox></td>
            <td width="20px"></td>
            <td />
        </tr>
    </table>
</asp:Content>
