<%@ Page Title="Identitas Korban" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="DataQueryDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Integration.JasaRaharja.DataQueryDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" witdh="100%">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Nama Korban
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtNamaKorban" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            NIK
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtNIK" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Tipe ID
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtTipeID" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Alamat
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtAlamat" Width="300px" ReadOnly="true" TextMode="MultiLine" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            No Telp
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtNoTelp" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Jenis Kelamin
                        </td>
                        <td class="entry">
                            <asp:RadioButtonList runat="server" ID="rblJenisKelamin" RepeatDirection="Horizontal"
                                Enabled="false">
                                <asp:ListItem Value="L" Text="Laki-laki" />
                                <asp:ListItem Value="P" Text="Perempuan" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Umur (tahun)
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtUmur" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Tgl Kejadian
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker runat="server" ID="txtTglKejadian" Width="300px" DatePopupButton-Enabled="false"
                                DateInput-ReadOnly="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Tgl Masuk RS
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker runat="server" ID="txtTglMasukRS" Width="100px" DatePopupButton-Enabled="false"
                                DateInput-ReadOnly="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Nama Ruangan
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtNamaRuangan" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
