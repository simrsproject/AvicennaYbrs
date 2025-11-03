<%@ Page Title="Refence Apotek Online" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="ApotekOnlineReference.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.ApotekOnline.ApotekOnlineReference" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadTabStrip ID="tsReferensi" runat="server" MultiPageID="mpReferensi" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab Text="DPHO" PageViewID="pvDPHO" Selected="true" />
            <telerik:RadTab Text="Obat" PageViewID="pvObat" />
            <telerik:RadTab Text="Poli" PageViewID="pvPoli" />
            <telerik:RadTab Text="Fasilitas Kesehatan" PageViewID="pvFaskes" />
            <telerik:RadTab Text="Setting Apotek" PageViewID="pvSettingApotek" />
            <telerik:RadTab Text="Spesialistik" PageViewID="pvSpesialistik" />
        </Tabs>
    </telerik:RadTabStrip>

    <telerik:RadMultiPage ID="mpReferensi" runat="server">
        <telerik:RadPageView ID="pvDPHO" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="vertical-align: top; width: 40%">
                        <table width="100%">
                            <tr>
                                <td class="label">List DPHO BPJS</td>
                                <td colspan="2">
                                    <asp:Button runat="server" ID="btnDpho" Text="Get List DPHO" OnClick="btnDpho_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdDpho" runat="server" OnNeedDataSource="grdDpho_NeedDataSource" OnItemCommand="grdDpho_ItemCommand" AutoGenerateColumns="False" AllowPaging="True" PageSize="50" AllowSorting="true">
                <MasterTableView AutoGenerateColumns="False" DataKeyNames="Kodeobat, Namaobat">
                    <Columns>
                        <telerik:GridBoundColumn DataField="Kodeobat" HeaderText="Kode Obat" UniqueName="Kodeobat" SortExpression="Kodeobat" />
                        <telerik:GridBoundColumn DataField="Namaobat" HeaderText="Nama Obat" UniqueName="Namaobat" SortExpression="Namaobat" />
                        <telerik:GridBoundColumn DataField="Prb" HeaderText="Prb Obat" UniqueName="Prb" SortExpression="Prb" />
                        <telerik:GridBoundColumn DataField="Kronis" HeaderText="Kronis Obat" UniqueName="Kronis" SortExpression="Kronis" />
                        <telerik:GridBoundColumn DataField="Kemo" HeaderText="Kemo Obat" UniqueName="Kemo" SortExpression="Kemo" />
                        <telerik:GridBoundColumn DataField="Harga" HeaderText="Harga Obat" UniqueName="Harga" SortExpression="Harga" />
                        <telerik:GridBoundColumn DataField="Restriksi" HeaderText="Restriksi Obat" UniqueName="Restriksi" SortExpression="Restriksi" />
                        <telerik:GridBoundColumn DataField="Generik" HeaderText="Generik Obat" UniqueName="Generik" SortExpression="Generik" />
                        <telerik:GridBoundColumn DataField="Aktif" HeaderText="Aktif Obat" UniqueName="Aktif" SortExpression="Aktif" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>

        <telerik:RadPageView ID="pvPoli" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="vertical-align: top; width: 40%">
                        <table width="100%">
                            <tr>
                                <td class="label">Kode Atau Nama POLI BPJS</td>
                                <td class="entry">
                                    <telerik:RadTextBox runat="server" ID="txtKodePoli" Width="300px" />
                                </td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td class="label" />
                                <td colspan="2">
                                    <asp:Button runat="server" ID="btnPoli" Text="Get List POLI" OnClick="btnPoli_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdPoli" runat="server" AutoGenerateColumns="False" ShowGroupPanel="false" AllowPaging="True" AllowSorting="True" GridLines="None">
                <MasterTableView DataKeyNames="Kode, Nama">
                    <Columns>
                        <telerik:GridBoundColumn DataField="Kode" HeaderText="Kode Poli" UniqueName="Kode" SortExpression="Kode" />
                        <telerik:GridBoundColumn DataField="Nama" HeaderText="Nama Poli" UniqueName="Nama" SortExpression="Nama" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>

        <telerik:RadPageView ID="pvFaskes" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="vertical-align: top; width: 40%">
                        <table width="100%">
                            <tr>
                                <td class="label">Jenis Faskes
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboFaskes" runat="server" Width="304px">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="1" Text="fktp" />
                                            <telerik:RadComboBoxItem Value="2" Text="rumah sakit" />
                                            <telerik:RadComboBoxItem Value="3" Text="apotek" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">Nama FASKES</td>
                                <td class="entry">
                                    <telerik:RadTextBox runat="server" ID="txtNamaFaskes" Width="300px" />
                                </td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td class="label"></td>
                                <td colspan="2">
                                    <asp:Button runat="server" ID="btnFaskes" Text="Get List FASKES" OnClick="btnFaskes_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdFaskes" runat="server" AutoGenerateColumns="False" ShowGroupPanel="false" AllowPaging="True" AllowSorting="True" GridLines="None">
                <MasterTableView DataKeyNames="Kode, Nama">
                    <Columns>
                        <telerik:GridBoundColumn DataField="Kode" HeaderText="Kode Faskes" UniqueName="Kode" SortExpression="Kode" />
                        <telerik:GridBoundColumn DataField="Nama" HeaderText="Nama Faskes" UniqueName="Nama" SortExpression="Nama" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>

        <telerik:RadPageView ID="pvSettingApotek" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="label">Kode Apotek BPJS</td>
                    <td class="entry">
                        <asp:TextBox runat="server" ID="txtKodeApotek" Width="300px" />
                    </td>
                    <td style="text-align: left"></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td colspan="2">
                        <asp:Button runat="server" ID="btnSetting" Text="Get Setting Apotek" OnClick="btnSetting_Click" />
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdSetting" runat="server" AutoGenerateColumns="False" ShowGroupPanel="false" AllowPaging="True" AllowSorting="True" GridLines="None">
                <MasterTableView DataKeyNames="Kode">
                    <Columns>
                        <telerik:GridBoundColumn DataField="Kode" HeaderText="Kode" UniqueName="Kode" SortExpression="Kode" />
                        <telerik:GridBoundColumn DataField="Namaapoteker" HeaderText="Nama Apoteker" UniqueName="Namaapoteker" SortExpression="Namaapoteker" />
                        <telerik:GridBoundColumn DataField="Namakepala" HeaderText="Nama Kepala" UniqueName="Namakepala" SortExpression="Namakepala" />
                        <telerik:GridBoundColumn DataField="Jabatankepala" HeaderText="Jabatan Kepala" UniqueName="Jabatankepala" SortExpression="Jabatankepala" />
                        <telerik:GridBoundColumn DataField="Nipkepala" HeaderText="NIP Kepala" UniqueName="Nipkepala" SortExpression="Nipkepala" />
                        <telerik:GridBoundColumn DataField="Siup" HeaderText="SIUP" UniqueName="Siup" SortExpression="Siup" />
                        <telerik:GridBoundColumn DataField="Alamat" HeaderText="Alamat" UniqueName="Alamat" SortExpression="Alamat" />
                        <telerik:GridBoundColumn DataField="Kota" HeaderText="Kota" UniqueName="Kota" SortExpression="Kota" />
                        <telerik:GridBoundColumn DataField="Namaverifikator" HeaderText="Nama Verifikator" UniqueName="Namaverifikator" SortExpression="Namaverifikator" />
                        <telerik:GridBoundColumn DataField="Nppverifikator" HeaderText="NPP Verifikator" UniqueName="Nppverifikator" SortExpression="Nppverifikator" />
                        <telerik:GridBoundColumn DataField="Namapetugasapotek" HeaderText="Nama Petugas Apotek" UniqueName="Namapetugasapotek" SortExpression="Namapetugasapotek" />
                        <telerik:GridBoundColumn DataField="Nippetugasapotek" HeaderText="NIP Petugas Apotek" UniqueName="Nippetugasapotek" SortExpression="Nippetugasapotek" />
                        <telerik:GridBoundColumn DataField="Checkstock" HeaderText="Check Stock" UniqueName="Checkstock" SortExpression="Checkstock" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>

        <telerik:RadPageView ID="pvSpesialistik" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="vertical-align: top; width: 40%">
                        <table width="100%">
                            <tr>
                                <td class="label">Get List SPESIALISTIK BPJS</td>
                                <td colspan="10">
                                        <asp:Button runat="server" ID="btnSpesialistik" Text="Get List SPESIALISTIK" OnClick="btnSpesialistik_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdSpesialistik" runat="server" OnNeedDataSource="grdSpesialistik_NeedDataSource" OnItemCommand="grdSpesialistik_ItemCommand" AutoGenerateColumns="False" AllowPaging="True" PageSize="50" AllowSorting="true">
                <MasterTableView DataKeyNames="Kode">
                    <Columns>
                        <telerik:GridBoundColumn DataField="Kode" HeaderText="Kode Spesialistik" UniqueName="Kode" SortExpression="Kode" />
                        <telerik:GridBoundColumn DataField="Nama" HeaderText="Nama Spesialistik" UniqueName="Nama" SortExpression="Nama" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>

        <telerik:RadPageView ID="pvObat" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="vertical-align: top; width: 40%">
                        <table width="100%">
                            <tr>
                                <td class="label">Jenis Obat
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboJenisObat" runat="server" Width="304px">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="1" Text="Obat PRB" />
                                            <telerik:RadComboBoxItem Value="2" Text="Obat Kronis Belum Stabil" />
                                            <telerik:RadComboBoxItem Value="3" Text="Obat Kemoterapi" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">Tanggal Resep</td>
                                <td class="entry">
                                    <telerik:RadDatePicker runat="server" ID="txtTglResep" Width="100px" />
                                </td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td class="label">Filter Search</td>
                                <td class="entry">
                                    <telerik:RadTextBox runat="server" ID="txtFilterPencarian" Width="300px" />
                                </td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td class="label"></td>
                                <td colspan="2">
                                    <asp:Button runat="server" ID="btnObat" Text="Get List OBAT" OnClick="btnObat_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdObat" runat="server" AutoGenerateColumns="False" ShowGroupPanel="false" AllowPaging="True" AllowSorting="True" GridLines="None">
                <MasterTableView DataKeyNames="Kode">
                    <Columns>
                        <telerik:GridBoundColumn DataField="Kode" HeaderText="Kode Obat" UniqueName="Kode" SortExpression="Kode" />
                        <telerik:GridBoundColumn DataField="Nama" HeaderText="Nama Obat" UniqueName="Nama" SortExpression="Nama" />
                        <telerik:GridBoundColumn DataField="Harga" HeaderText="Harga" UniqueName="Harga" SortExpression="Harga" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
