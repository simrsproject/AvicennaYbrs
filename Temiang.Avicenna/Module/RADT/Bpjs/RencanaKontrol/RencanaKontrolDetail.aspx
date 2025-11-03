<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="RencanaKontrolDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.RencanaKontrolDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td class="label">No Kontrol</td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNoRujukan" runat="server" ReadOnly="true" Width="300px" />
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td class="label">Jenis Kontrol</td>
            <td class="entry">
                <asp:RadioButtonList ID="rblJenisKontrol" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblJenisKontrol_SelectedIndexChanged">
                    <asp:ListItem Text="SPRI" Value="1" />
                    <asp:ListItem Text="Rencana Kontrol" Value="2" Selected="True" />
                </asp:RadioButtonList>
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td class="label">No SEP</td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtNoSep" runat="server" Width="300px" />
                        </td>
                        <td>&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:ImageButton ID="btnCariPasienSep" runat="server" ImageUrl="~/Images/Toolbar/search16.png" OnClick="btnCariPasienSep_Click" ToolTip="Cari Sep" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20px"></td>
            <td />
        </tr>
        <tr>
            <td class="label">Tgl. SEP</td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtTglSep" runat="server" Width="100px" DateInput-ReadOnly="true" DatePopupButton-Enabled="false" />
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td class="label">Jenis Pelayanan</td>
            <td class="entry">
                <telerik:RadTextBox ID="txtJenisPelayanan" runat="server" Width="300px" ReadOnly="true" />
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td class="label">Poli SEP</td>
            <td class="entry">
                <telerik:RadTextBox ID="txtPoliSep" runat="server" Width="300px" ReadOnly="true" />
            </td>
            <td width="20px" />
            <td />
        </tr>
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
                            <asp:ImageButton ID="btnCariPasienPeserta" runat="server" ImageUrl="~/Images/Toolbar/search16.png" OnClick="btnCariPasienPeserta_Click" ToolTip="Cari Peserta" />
                        </td>
                    </tr>
                </table>

            </td>
            <td width="20px" />
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
            <td class="label">Tgl. Lahir</td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtTglLahir" runat="server" Width="100px" DateInput-ReadOnly="true" DatePopupButton-Enabled="false" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvTglLahir" runat="server" ErrorMessage="Tgl lahir required."
                    ValidationGroup="entry" ControlToValidate="txtTglLahir" SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">Tgl. Kontrol</td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtTglRujukan" runat="server" Width="100px" AutoPostBack="true" OnSelectedDateChanged="txtTglRujukan_SelectedDateChanged" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvTglRujukan" runat="server" ErrorMessage="Tgl kontrol required."
                    ValidationGroup="entry" ControlToValidate="txtTglRujukan" SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">Poli Kontrol</td>
            <td class="entry">
                <telerik:RadComboBox ID="cboPoliDirujuk" runat="server" Width="304px" AllowCustomText="true" AutoPostBack="true" Filter="Contains"
                    OnSelectedIndexChanged="cboPoliDirujuk_SelectedIndexChanged" />
            </td>
            <td width="20px">
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">DPJP Kontrol</td>
            <td class="entry">
                <telerik:RadComboBox ID="cboDpjpKontrol" runat="server" Width="304px" AllowCustomText="true" Filter="Contains" />
            </td>
            <td width="20px">
            </td>
            <td />
        </tr>
    </table>
</asp:Content>
