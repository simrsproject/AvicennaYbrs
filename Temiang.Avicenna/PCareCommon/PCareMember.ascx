<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PCareMember.ascx.cs" Inherits="Temiang.Avicenna.PCareCommon.PCareMember" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script type="text/javascript">

</script>

<fieldset style="width: 1000px">
    <legend><b>Patient :</b></legend>
    <table style="width: 100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label17" runat="server" Text="Medical No"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100%" ReadOnly="True" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label18" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="100%" ReadOnly="True" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</fieldset>

<br />
<fieldset style="width: 1000px">
    <legend><b>Validate :</b></legend>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">No Kartu BPJS
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtNoKartuSearch" runat="server" Width="130px" />
                            <asp:ImageButton ID="btnNoKartuSearch" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnNoKartuSearch_Click" ToolTip="Validate" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">No KTP
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtNikSearch" runat="server" Width="130px" />
                            <asp:ImageButton ID="btnNikSearch" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnNikSearch_Click" ToolTip="Validate" />
                        </td>
                    </tr>
                </table>
            </td>
    </table>
</fieldset>
<fieldset style="width: 1000px">
    <legend><b>BPJS Info :</b></legend>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNoKartu" runat="server" Text="No Kartu BPJS"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtNoKartu" runat="server" Width="130px" ReadOnly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Nama"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtNama" runat="server" Width="100%" ReadOnly="True" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Hubungan Keluarga"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtHubunganKeluarga" runat="server" Width="100%" ReadOnly="True" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="Sex"></asp:Label>
                        </td>
                        <td class="entry300">
                            <asp:RadioButton ID="optSexPerempuan" runat="server" Text="Perempuan" GroupName="Sex" />
                            <asp:RadioButton ID="optSexLakilaki" runat="server" Text="Laki-laki" GroupName="Sex" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label7" runat="server" Text="Gol Darah"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtGolDarah" runat="server" Width="100px" ReadOnly="True" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label4" runat="server" Text="Tgl Lahir"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtTglLahir" runat="server" Width="110px" ReadOnly="True" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label5" runat="server" Text="Tgl Berlaku"></asp:Label>
                        </td>
                        <td class="entry300">
                            <table>
                                <tr>
                                    <td style="width: 110px">
                                        <telerik:RadTextBox ID="txtTglMulaiAktif" runat="server" Width="100%" ReadOnly="True" /></td>
                                    <td style="width: 6px">&nbsp;-&nbsp;</td>
                                    <td style="width: 110px">
                                        <telerik:RadTextBox ID="txtTglAkhirBerlaku" runat="server" Width="100%" ReadOnly="True" /></td>
                                    <td></td>
                                </tr>
                            </table>

                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label14" runat="server" Text="Status Aktif"></asp:Label>
                        </td>
                        <td class="entry300">
                            <asp:CheckBox runat="server" ID="chkAktif" />
                            <telerik:RadTextBox ID="txtKetAktif" runat="server" Width="287px" ReadOnly="True" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">

                    <tr>
                        <td class="label">
                            <asp:Label ID="Label12" runat="server" Text="No HP"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtNoHP" runat="server" Width="120px" ReadOnly="True" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label13" runat="server" Text="No KTP"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtNoKTP" runat="server" Width="295px" ReadOnly="True" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label8" runat="server" Text="Member Klinik Pratama"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtKdProviderPst_kdProvider" runat="server" Width="70px" ReadOnly="True" />&nbsp;
                        <telerik:RadTextBox ID="txtKdProviderPst_nmProvider" runat="server" Width="218px" ReadOnly="True" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label9" runat="server" Text="Provider Gigi"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtKdProviderGigi_kdProvider" runat="server" Width="70px" ReadOnly="True" />&nbsp;
                        <telerik:RadTextBox ID="txtKdProviderGigi_nmProvider" runat="server" Width="218px" ReadOnly="True" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label10" runat="server" Text="Kelas"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtJnsKelas_kode" runat="server" Width="70px" ReadOnly="True" />&nbsp;
                        <telerik:RadTextBox ID="txtJnsKelas_nama" runat="server" Width="218px" ReadOnly="True" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label11" runat="server" Text="Jenis Peserta"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtJnsPeserta_kode" runat="server" Width="70px" ReadOnly="True" />&nbsp;
                        <telerik:RadTextBox ID="txtJnsPeserta_nama" runat="server" Width="218px" ReadOnly="True" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label15" runat="server" Text="Asuransi"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtAsuransi_kdAsuransi" runat="server" Width="70px" ReadOnly="True" />&nbsp;
                        <telerik:RadTextBox ID="txtAsuransi_nmAsuransi" runat="server" Width="218px" ReadOnly="True" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label16" runat="server" Text="No Asuransi"></asp:Label>

                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtAsuransi_noAsuransi" runat="server" Width="295px" ReadOnly="True" />

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</fieldset>
