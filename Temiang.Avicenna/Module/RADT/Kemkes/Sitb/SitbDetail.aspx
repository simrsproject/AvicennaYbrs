<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="SitbDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Kemkes.SitbDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="../../../../JavaScript/jquery.js"></script>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function OnClientItemsRequestingHandler(sender, eventArgs) {
                if (sender.get_text().length < 3) eventArgs.set_cancel(true);
                else {
                    var context = eventArgs.get_context();
                    context["filter"] = eventArgs.get_text();
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">No SITB</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNoSitb" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">No. Registrasi</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNoRegistrasi" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Nama</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNama" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">NIK</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNik" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvNik" runat="server" ErrorMessage="NIK required."
                                ValidationGroup="entry" ControlToValidate="txtNik" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Jenis Kelamin</td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rblJenisKelamin" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="L" Text="Laki-laki" />
                                <asp:ListItem Value="P" Text="Perempuan" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Tgl. Lahir</td>
                        <td class="entry">
                            <telerik:RadDatePicker runat="server" ID="txtTglLahir" Width="100px" />
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Alamat</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAlamatKtp" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvAlamatKtp" runat="server" ErrorMessage="Alamat KTP required."
                                ValidationGroup="entry" ControlToValidate="txtAlamatKtp" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Kab. Kota</td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboKabKota" runat="server" Width="300px" AllowCustomText="true" Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboKabKota_SelectedIndexChanged" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvKabKota" runat="server" ErrorMessage="Kab. Kota KTP required."
                                ValidationGroup="entry" ControlToValidate="cboKabKota" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Provinsi</td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboProvinsi" runat="server" Width="300px" AllowCustomText="true" Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPropinsiKtp" runat="server" ErrorMessage="Propinsi KTP required."
                                ValidationGroup="entry" ControlToValidate="cboProvinsi" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">Diagnosa</td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboDiagnosa" runat="server" Width="300px" AllowCustomText="true"
                                OnItemDataBound="cboDiagnosa_ItemDataBound" EnableLoadOnDemand="true" OnClientItemsRequesting="OnClientItemsRequestingHandler">
                                <WebServiceSettings Path="../../../../WebService/Sep.asmx" Method="GetDiagnosa" />
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvDiagnosa" runat="server" ErrorMessage="Diagnosa required."
                                ValidationGroup="entry" ControlToValidate="cboDiagnosa" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Tipe Diagnosis</td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rblTipeDiagnosis" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="1" Text="Terkonfirmasi bakteriologis" />
                                <asp:ListItem Value="2" Text="Terdiagnosis klinis" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Lokasi Anatomi</td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rblLokasiAnatomi" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="1" Text="Paru" />
                                <asp:ListItem Value="2" Text="Ekstraparu" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Riwayat Pengobatan</td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rblRiwayatPengobatan" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                <asp:ListItem Value="1" Text="Baru" />
                                <asp:ListItem Value="2" Text="Kambuh" />
                                <asp:ListItem Value="3" Text="Diobati setelah gagal kategori 1" />
                                <asp:ListItem Value="4" Text="Diobati setelah gagal kategori 2" />
                                <asp:ListItem Value="5" Text="Diobati setelah putus berobat" />
                                <asp:ListItem Value="6" Text="Diobati setelah gagal pengobatan lini 2" />
                                <asp:ListItem Value="7" Text="Pernah diobati tidak diketahui hasilnya" />
                                <asp:ListItem Value="8" Text="Tidak diketahui" />
                                <asp:ListItem Value="9" Text="Lain-lain" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Tgl. Mulai Pengobatan</td>
                        <td class="entry">
                            <telerik:RadDatePicker runat="server" ID="txtTglMulaiPengobatan" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTglMulaiPengobatan" runat="server" ErrorMessage="Tgl. Mulai Pengobatan required."
                                ValidationGroup="entry" ControlToValidate="txtTglMulaiPengobatan" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Panduan OAT</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPanduanOat" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPanduanOat" runat="server" ErrorMessage="Panduan OAT required."
                                ValidationGroup="entry" ControlToValidate="txtPanduanOat" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Sebelum Pengobatan Hasil Mikroskopis</td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rblSebelumPengobatanHasilMikroskopis" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                <asp:ListItem Value="Neg" Text="Negatif" />
                                <asp:ListItem Value="1-9" Text="1-9" />
                                <asp:ListItem Value="1+" Text="1+" />
                                <asp:ListItem Value="2+" Text="2+" />
                                <asp:ListItem Value="3+" Text="3+" />
                                <asp:ListItem Value="Tidak Dilakukan" Text="Tidak Dilakukan" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Sebelum Pengobatan Hasil Tes Cepat</td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rblSebelumPengobatanHasilTesCepat" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                <asp:ListItem Value="Neg" Text="Negatif" />
                                <asp:ListItem Value="Rif Sen" Text="Rif Sen" />
                                <asp:ListItem Value="Rif Res" Text="Rif Res" />
                                <asp:ListItem Value="Rif Indet" Text="Rif Indet" />
                                <asp:ListItem Value="INVALID" Text="INVALID" />
                                <asp:ListItem Value="ERROR" Text="ERROR" />
                                <asp:ListItem Value="NO RESULT" Text="NO RESULT" />
                                <asp:ListItem Value="Tidak Dilakukan" Text="Tidak Dilakukan" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Sebelum Pengobatan Hasil Biakan</td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rblSebelumPengobatanHasilBiakan" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                <asp:ListItem Value="Neg" Text="Negatif" />
                                <asp:ListItem Value="1-19 BTA" Text="1-19 BTA" />
                                <asp:ListItem Value="1+" Text="1+" />
                                <asp:ListItem Value="2+" Text="2+" />
                                <asp:ListItem Value="3+" Text="3+" />
                                <asp:ListItem Value="4+" Text="4+" />
                                <asp:ListItem Value="NTM" Text="NTM" />
                                <asp:ListItem Value="Kontaminasi" Text="Kontaminasi" />
                                <asp:ListItem Value="Tidak Dilakukan" Text="Tidak Dilakukan" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Hasil Mikroskopis Bulan 2</td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rblHasilMikroskopisBulan2" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                <asp:ListItem Value="Neg" Text="Negatif" />
                                <asp:ListItem Value="1-9" Text="1-9" />
                                <asp:ListItem Value="1+" Text="1+" />
                                <asp:ListItem Value="2+" Text="2+" />
                                <asp:ListItem Value="3+" Text="3+" />
                                <asp:ListItem Value="Tidak Dilakukan" Text="Tidak Dilakukan" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Hasil Mikroskopis Bulan 3</td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rblHasilMikroskopisBulan3" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                <asp:ListItem Value="Neg" Text="Negatif" />
                                <asp:ListItem Value="1-9" Text="1-9" />
                                <asp:ListItem Value="1+" Text="1+" />
                                <asp:ListItem Value="2+" Text="2+" />
                                <asp:ListItem Value="3+" Text="3+" />
                                <asp:ListItem Value="Tidak Dilakukan" Text="Tidak Dilakukan" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Hasil Mikroskopis Bulan 5</td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rblHasilMikroskopisBulan5" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                <asp:ListItem Value="Neg" Text="Negatif" />
                                <asp:ListItem Value="1-9" Text="1-9" />
                                <asp:ListItem Value="1+" Text="1+" />
                                <asp:ListItem Value="2+" Text="2+" />
                                <asp:ListItem Value="3+" Text="3+" />
                                <asp:ListItem Value="Tidak Dilakukan" Text="Tidak Dilakukan" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Akhir Pengobatan Hasil Mikroskopis</td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rblAkhirPengobatanHasilMikroskopis" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                <asp:ListItem Value="Neg" Text="Negatif" />
                                <asp:ListItem Value="1-9" Text="1-9" />
                                <asp:ListItem Value="1+" Text="1+" />
                                <asp:ListItem Value="2+" Text="2+" />
                                <asp:ListItem Value="3+" Text="3+" />
                                <asp:ListItem Value="Tidak Dilakukan" Text="Tidak Dilakukan" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Tgl. Akhir Pengobatan</td>
                        <td class="entry">
                            <telerik:RadDatePicker runat="server" ID="txtTglAkhirPengobatan" Width="100px" />
                        </td>
                        <td width="20px">
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Hasil Akhir Pengobatan</td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rblHasilAkhirPengobatan" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                <asp:ListItem Value="1" Text="Sembuh" />
                                <asp:ListItem Value="2 Lengkap" Text="Pengobatan Lengkap" />
                                <asp:ListItem Value="3" Text="Putus Berobat" />
                                <asp:ListItem Value="4" Text="Meninggal" />
                                <asp:ListItem Value="5" Text="Gagal" />
                                <asp:ListItem Value="6" Text="Tidak Dievaluasi/pindah" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Foto Toraks</td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rblFotoToraks" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                <asp:ListItem Value="Positif" Text="Positif" />
                                <asp:ListItem Value="Negatif" Text="Negatif" />
                                <asp:ListItem Value="Tidak Dilakukan" Text="Tidak Dilakukan" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
