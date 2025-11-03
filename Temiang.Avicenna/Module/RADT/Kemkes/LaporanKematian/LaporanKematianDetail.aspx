<%@  Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="LaporanKematianDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Kemkes.LaporanKematianDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">Tgl. Masuk</td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTglMasuk" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTglMasuk" runat="server" ErrorMessage="Tgl. masuk required."
                                ValidationGroup="entry" ControlToValidate="txtTglMasuk" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Nama</td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboNama" runat="server" Width="300px" AllowCustomText="true" EnableLoadOnDemand="true" AutoPostBack="true"
                                OnItemsRequested="cboNama_ItemsRequested" OnItemDataBound="cboNama_ItemDataBound" OnSelectedIndexChanged="cboNama_SelectedIndexChanged">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "MedicalNo") %><br />
                                    <%# DataBinder.Eval(Container.DataItem, "RegistrationNo") %><br />
                                    <%# DataBinder.Eval(Container.DataItem, "PatientName") %><br />
                                    <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName") %>
                                </ItemTemplate>
                            </telerik:RadComboBox>

                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvNama" runat="server" ErrorMessage="Nama required."
                                ValidationGroup="entry" ControlToValidate="cboNama" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
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
                            <asp:RadioButtonList ID="rbtJenisKelamin" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
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
                            <telerik:RadDatePicker ID="txtTglLahir" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTglLahir" runat="server" ErrorMessage="Tgl. lahir required."
                                ValidationGroup="entry" ControlToValidate="txtTglLahir" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Alamat KTP</td>
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
                        <td class="label">Kelurahan KTP</td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboKelurahanKtp" runat="server" Width="300px" AllowCustomText="true" EnableLoadOnDemand="true" AutoPostBack="true"
                                OnItemsRequested="cboKelurahanKtp_ItemsRequested" OnItemDataBound="cboKelurahanKtp_ItemDataBound" OnSelectedIndexChanged="cboKelurahanKtp_SelectedIndexChanged">
                                <ItemTemplate>
                                    Kelurahan : <%# DataBinder.Eval(Container.DataItem, "Kelurahan") %><br />
                                    Kecamatan : <%# DataBinder.Eval(Container.DataItem, "Kecamatan") %><br />
                                    Kab. Kota : <%# DataBinder.Eval(Container.DataItem, "KabKota") %><br />
                                    Provinsi : <%# DataBinder.Eval(Container.DataItem, "Provinsi") %>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvKelurahanKtp" runat="server" ErrorMessage="Kelurahan KTP required."
                                ValidationGroup="entry" ControlToValidate="cboKelurahanKtp" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Kecamatan KTP</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtKecamatanKtp" runat="server" Width="100px" ReadOnly="true" />
                            &nbsp;
                            <asp:Label runat="server" ID="lblKecamatanKtp" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvKecamatanKtp" runat="server" ErrorMessage="Kecamatan KTP required."
                                ValidationGroup="entry" ControlToValidate="txtKecamatanKtp" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Kab. Kota KTP</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtKabKotaKtp" runat="server" Width="100px" ReadOnly="true" />
                            &nbsp;
                            <asp:Label runat="server" ID="lblKabKotaKtp" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvKabKota" runat="server" ErrorMessage="Kab. Kota KTP required."
                                ValidationGroup="entry" ControlToValidate="txtKabKotaKtp" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Provinsi KTP</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtProvinsiKtp" runat="server" Width="100px" ReadOnly="true" />
                            &nbsp;
                            <asp:Label runat="server" ID="lblProvinsiKtp" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPropinsiKtp" runat="server" ErrorMessage="Propinsi KTP required."
                                ValidationGroup="entry" ControlToValidate="txtProvinsiKtp" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Alamat Domisili</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAlamatDomisili" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvAlamatDomisili" runat="server" ErrorMessage="Alamat domisili required."
                                ValidationGroup="entry" ControlToValidate="txtAlamatDomisili" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">Saturasi Oksigen</td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtSaturasiOksigen" runat="server" Width="100px" />&nbsp;mmHg
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSaturasiOksigen" runat="server" ErrorMessage="Saturasi oksigen required."
                                ValidationGroup="entry" ControlToValidate="txtSaturasiOksigen" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Tgl. Kematian</td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTglKematian" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTglKematian" runat="server" ErrorMessage="Tgl. kematian required."
                                ValidationGroup="entry" ControlToValidate="txtTglKematian" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Lokasi Kematian</td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboLokasiKematian" runat="server" Width="300px" AllowCustomText="true" Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvLokasiKematian" runat="server" ErrorMessage="Lokasi kematian required."
                                ValidationGroup="entry" ControlToValidate="cboLokasiKematian" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Penyebab Kematian</td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPenyebabKematian" runat="server" Width="300px" AllowCustomText="true" Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPenyebabKematian" runat="server" ErrorMessage="Penyebab kematian required."
                                ValidationGroup="entry" ControlToValidate="cboPenyebabKematian" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image14" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Kasus Kematian</td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboKasusKematian" runat="server" Width="300px" AllowCustomText="true" Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvKasusKematian" runat="server" ErrorMessage="Kasus kematian required."
                                ValidationGroup="entry" ControlToValidate="cboKasusKematian" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Status Komorbid</td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rbtStatusKomorbid" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="true"
                                OnSelectedIndexChanged="rbtStatusKomorbid_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="Tidak" />
                                <asp:ListItem Value="1" Text="Ya" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Komorbid 1</td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboKomorbid1" runat="server" Width="300px" AllowCustomText="true" Filter="Contains" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Komorbid 2</td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboKomorbid2" runat="server" Width="300px" AllowCustomText="true" Filter="Contains" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Komorbid 3</td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboKomorbid3" runat="server" Width="300px" AllowCustomText="true" Filter="Contains" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Komorbid 4</td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboKomorbid4" runat="server" Width="300px" AllowCustomText="true" Filter="Contains" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Status Kehamilan</td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rbtStatusKehamilan" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="0" Text="Tidak" />
                                <asp:ListItem Value="1" Text="Ya" />
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
