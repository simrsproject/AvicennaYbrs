<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Persalinan.ascx.cs" Inherits="Temiang.Avicenna.Module.Finance.Integration.Eklaim.Persalinan" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumDelivery" runat="server" ValidationGroup="Delivery" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="Delivery"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr style="display: none;">
        <td colspan="3">
            <asp:HiddenField runat="server" ID="hdnSequence" />
        </td>
    </tr>
    <tr>
        <td class="label">Delivery Method</td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboDeliveryMethod" Width="304px">
                <Items>
                    <telerik:RadComboBoxItem Value="" Text="" Selected="true" />
                    <telerik:RadComboBoxItem Value="vaginal" Text="Vaginal" />
                    <telerik:RadComboBoxItem Value="sc" Text="SC" />
                </Items>
            </telerik:RadComboBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="rfvDeliveryMethod" runat="server" ErrorMessage="Delivery Method required."
                ControlToValidate="cboDeliveryMethod" SetFocusOnError="True" ValidationGroup="Delivery" Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="label">Waktu</td>
        <td class="entry">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <telerik:RadDatePicker ID="txtTanggalKelahiran" runat="server" Width="100px" />
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <telerik:RadTimePicker ID="txtJamKelahiran" runat="server" Width="100px" />
                    </td>
                </tr>
            </table>
        </td>
        <td>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvTanggalKelahiran" runat="server" ErrorMessage="Tanggal Kelahiran required."
                            ControlToValidate="txtTanggalKelahiran" SetFocusOnError="True" ValidationGroup="Delivery" Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvJamKelahiran" runat="server" ErrorMessage="Jam Kelahiran required."
                            ControlToValidate="txtJamKelahiran" SetFocusOnError="True" ValidationGroup="Delivery" Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="label">Letak Janin</td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboLetakJanin" Width="304px">
                <Items>
                    <telerik:RadComboBoxItem Value="" Text="" Selected="true" />
                    <telerik:RadComboBoxItem Value="kepala" Text="Kepala" />
                    <telerik:RadComboBoxItem Value="sungsang" Text="Sungsang" />
                    <telerik:RadComboBoxItem Value="lintang" Text="Lintang" />
                </Items>
            </telerik:RadComboBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="rfvLetakJanin" runat="server" ErrorMessage="Letak Janin required."
                ControlToValidate="cboLetakJanin" SetFocusOnError="True" ValidationGroup="Delivery" Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="label">Kondisi</td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboKondisi" Width="304px">
                <Items>
                    <telerik:RadComboBoxItem Value="" Text="" Selected="true" />
                    <telerik:RadComboBoxItem Value="livebirth" Text="Livebirth" />
                    <telerik:RadComboBoxItem Value="stillbirth" Text="Stillbirth" />
                </Items>
            </telerik:RadComboBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="rfvKondisi" runat="server" ErrorMessage="Kondisi required."
                ControlToValidate="cboKondisi" SetFocusOnError="True" ValidationGroup="Delivery" Width="100%">
                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="label"></td>
        <td class="entry">
            <asp:CheckBox runat="server" ID="chkBantuanManual" Text="Bantuan Manual" />
        </td>
        <td />
    </tr>
    <tr>
        <td class="label"></td>
        <td class="entry">
            <asp:CheckBox runat="server" ID="chkForcep" Text="Penggunaan Forcep" />
        </td>
        <td />
    </tr>
    <tr>
        <td class="label"></td>
        <td class="entry">
            <asp:CheckBox runat="server" ID="chkVacuum" Text="Penggunaan Vacuum" />
        </td>
        <td />
    </tr>
    <tr>
        <td align="right" colspan="3" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="Delivery"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="Delivery"
                Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button></td>
    </tr>
</table>
