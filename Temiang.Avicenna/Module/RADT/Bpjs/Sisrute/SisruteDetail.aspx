<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="SisruteDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.SisruteDetail" %>

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
                        <td class="label">
                            No. Rujukan
                        </td>
                        <td class="entry" colspan="2">
                            <telerik:RadTextBox runat="server" ID="txtNoRujukan" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">
                            No. RM
                        </td>
                        <td class="entry" colspan="2">
                            <telerik:RadComboBox runat="server" ID="cboNoRM" Width="100%" AutoPostBack="true"
                                EnableLoadOnDemand="true" OnSelectedIndexChanged="cboNoRM_SelectedIndexChanged"
                                OnItemDataBound="cboNoRM_ItemDataBound" OnItemsRequested="cboNoRM_ItemsRequested" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">
                            NIK
                        </td>
                        <td class="entry">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr style="display: none">
                                    <td>
                                        <asp:RadioButtonList runat="server" ID="rblNIK" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Diketahui" Value="0" Selected="True" />
                                            <asp:ListItem Text="Tidak Diketahui" Value="1" />
                                            <asp:ListItem Text="Mr. X" Value="2" />
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadTextBox runat="server" ID="txtNIK" Width="300px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            No. JKN
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtJKN" Width="300px" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Nama
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtNamaPasien" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Jenis Kelamin
                        </td>
                        <td class="entry">
                            <asp:RadioButtonList runat="server" ID="rblJenisKelamin" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Laki-laki" Value="1" Selected="True" />
                                <asp:ListItem Text="Perempuan" Value="2" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Tempat & Tgl. Lahir
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox runat="server" ID="txtTempatLahir" Width="300px" />
                                    </td>
                                    <td>
                                        &nbsp
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker runat="server" ID="txtTglLahir" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Alamat
                        </td>
                        <td class="entry" colspan="2">
                            <telerik:RadTextBox runat="server" ID="txtAlamat" Width="100%" TextMode="MultiLine" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">
                            No. Kontak
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtNoKontak" Width="300px" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td colspan="4">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Tgl. Rujukan
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker runat="server" ID="txtTglRujukan" Width="100px" />
                                    </td>
                                    <td>
                                        &nbsp
                                    </td>
                                    <td>
                                        <telerik:RadTimePicker runat="server" ID="txtJamRujukan" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Dokter DPJP
                        </td>
                        <td class="entry" colspan="2">
                            <telerik:RadTextBox runat="server" ID="txtDpjp" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">
                            Jenis Rujukan
                        </td>
                        <td class="entry">
                            <asp:RadioButtonList runat="server" ID="rblJenisRujukan" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Rawat Darurat / Inap" Value="2" Selected="True" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            Transportasi
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtTransportasi" Width="300px" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            Pilih Ambulance
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtNoAmbulance" Width="300px" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Diagnosis
                        </td>
                        <td class="entry" colspan="2">
                            <telerik:RadTextBox runat="server" ID="txtDiagnosis" Width="100%" TextMode="MultiLine" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">
                            Kode ICD X
                        </td>
                        <td class="entry" colspan="2">
                            <telerik:RadComboBox runat="server" ID="cboKodeIcdX" Width="100%" EnableLoadOnDemand="true"
                                OnClientItemsRequesting="OnClientItemsRequestingHandler" OnItemDataBound="cboKodeIcdX_ItemDataBound">
                                <WebServiceSettings Path="../../../../WebService/Sisrute.asmx" Method="GetDiagnosaIcdX" />
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">
                            Alasan Rujukan
                        </td>
                        <td class="entry" colspan="2">
                            <telerik:RadComboBox runat="server" ID="cboAlasanRujukan" Width="100%" EnableLoadOnDemand="true"
                                OnItemsRequested="cboAlasanRujukan_ItemsRequested" OnItemDataBound="cboAlasanRujukan_ItemDataBound" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">
                            Permintaan Layanan
                        </td>
                        <td class="entry" colspan="2">
                            <telerik:RadTextBox runat="server" ID="txtPermintaanLayanan" Width="100%" TextMode="MultiLine" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            Kode ICD 9 CM
                        </td>
                        <td class="entry" colspan="2">
                            <telerik:RadComboBox runat="server" ID="cboKodeIcd9Cm" Width="100%" EnableLoadOnDemand="true"
                                OnClientItemsRequesting="OnClientItemsRequestingHandler" OnItemDataBound="cboKodeIcd9Cm_ItemDataBound">
                                <WebServiceSettings Path="../../../../WebService/Sisrute.asmx" Method="GetDiagnosaIcd9Cm" />
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">
                            Faskes Tujuan
                        </td>
                        <td class="entry" colspan="2">
                            <telerik:RadComboBox runat="server" ID="cboFaskesTujuan" Width="100%" EnableLoadOnDemand="true"
                                OnItemsRequested="cboFaskesTujuan_ItemsRequested" OnItemDataBound="cboAlasanRujukan_ItemDataBound" />
                        </td>
                        <td width="20px" />
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Anamnesis & Pemeriksaan Fisik
                        </td>
                        <td colspan="2">
                            <telerik:RadTextBox runat="server" ID="txtAnamnesis" Width="100%" TextMode="MultiLine" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">
                            Kesadaran
                        </td>
                        <td class="entry">
                            <asp:RadioButtonList runat="server" ID="rblKesadaran" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Sadar" Value="1" Selected="True" />
                                <asp:ListItem Text="Tidak Sadar" Value="2" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Tekanan Darah (mmHg)
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtTekananDarah" Width="100px" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Frek. Nadi (kali/menit)
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtFrekNadi" Width="100px" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Suhu (*C)
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtSuhu" Width="100px" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Frek. Nafas (kali/menit)
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtFrekNafas" Width="100px" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Nyeri
                        </td>
                        <td class="entry">
                            <asp:RadioButtonList runat="server" ID="rblNyeri" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Tidak Nyeri" Value="0" Selected="True" />
                                <asp:ListItem Text="Ringan" Value="1" />
                                <asp:ListItem Text="Sedang" Value="2" />
                                <asp:ListItem Text="Berat" Value="3" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Riwayat Alergi
                        </td>
                        <td colspan="2">
                            <telerik:RadTextBox runat="server" ID="txtRiwayatAlergi" Width="100%" TextMode="MultiLine" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">
                            Hasil Lab.
                        </td>
                        <td colspan="2">
                            <telerik:RadTextBox runat="server" ID="txtHasilLab" Width="100%" TextMode="MultiLine" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">
                            Hasil Radiologi
                        </td>
                        <td colspan="2">
                            <telerik:RadTextBox runat="server" ID="txtHasilRadiologi" Width="100%" TextMode="MultiLine" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">
                            Terapi/Tindakan yang Diberikan
                        </td>
                        <td colspan="2">
                            <telerik:RadTextBox runat="server" ID="txtTerapi" Width="100%" TextMode="MultiLine" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">
                            Keterangan Lain
                        </td>
                        <td colspan="2">
                            <telerik:RadTextBox runat="server" ID="txtKeteranganLain" Width="100%" TextMode="MultiLine" />
                        </td>
                        <td width="20px" />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
