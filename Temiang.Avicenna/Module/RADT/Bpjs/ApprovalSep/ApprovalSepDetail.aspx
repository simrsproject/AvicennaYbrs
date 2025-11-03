<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="ApprovalSepDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.ApprovalSepDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td class="label">No Peserta</td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>

                            <telerik:RadTextBox ID="txtNoPeserta" runat="server" Width="300px" />
                        </td>
                        <td>&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:ImageButton ID="btnCariPasien" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnCariPasien_Click" ToolTip="Cari Peserta" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvNoSep" runat="server" ErrorMessage="No Peserta required."
                    ValidationGroup="entry" ControlToValidate="txtNoPeserta" SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">Tgl. SEP</td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtTglSep" runat="server" Width="100px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvTglSep" runat="server" ErrorMessage="Tgl. SEP required."
                    ValidationGroup="entry" ControlToValidate="txtTglSep" SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">Nama Peserta</td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNamaPeserta" runat="server" ReadOnly="true" Width="300px" />
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td class="label">Jenis Kelamin</td>
            <td class="entry">
                <asp:RadioButtonList ID="rbtSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Enabled="false">
                    <asp:ListItem Value="L" Text="Laki-laki" />
                    <asp:ListItem Value="P" Text="Perempuan" />
                </asp:RadioButtonList>
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td class="label">Jenis Pelayanan
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboPelayanan" runat="server" Width="304px">
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                        <telerik:RadComboBoxItem Value="1" Text="Rawat Inap" />
                        <telerik:RadComboBoxItem Value="2" Text="Rawat Jalan" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvPelayanan" runat="server" ErrorMessage="Jenis pelayanan required."
                    ValidationGroup="entry" ControlToValidate="cboPelayanan" SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td />
        </tr>
                <tr>
            <td class="label">Jenis Pengajuan
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboJenisPengajuan" runat="server" Width="304px">
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                        <telerik:RadComboBoxItem Value="1" Text="Pengajuan Backdate" />
                        <telerik:RadComboBoxItem Value="2" Text="Pengajuan Finger Print" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvPengajuan" runat="server" ErrorMessage="Jenis pengajuan required."
                    ValidationGroup="entry" ControlToValidate="cboJenisPengajuan" SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">Keterangan
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtKeterangan" runat="server" Width="300px" TextMode="MultiLine" />
            </td>
            <td width="20px" />
            <td />
        </tr>
    </table>
</asp:Content>
