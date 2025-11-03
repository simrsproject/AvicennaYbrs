<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="BpjsVClaimDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.VClaim.BpjsVClaimDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="../../../../../JavaScript/jquery.js"></script>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function OnClientItemsRequestingHandler(sender, eventArgs) {

                var rujukan = $find("<%= cboNoRujukan.ClientID %>");
                var jenis = $find("<%= cboJenisRujukanSep.ClientID %>");

                if (sender.get_text().length < 3) eventArgs.set_cancel(true);
                else {
                    var context = eventArgs.get_context();
                    context["filter"] = eventArgs.get_text() + '|' + rujukan.get_text() + '|' + jenis.get_value();
                }
            }

            function OnClientItemsRequestingHandlerDpjp(sender, eventArgs) {
                var jp = $find("<%= cboPelayanan.ClientID %>");

                if (jp.get_value() == "2") {
                    var poli = $find("<%= cboPoliSep.ClientID %>");
                    if (poli.get_value() == '') {
                        alert('Poli tujuan belum dipilih');
                        eventArgs.set_cancel(true);
                    }
                }

                var tgl = $find("<%= txtTglSep.ClientID %>");

                if (sender.get_text().length < 3) eventArgs.set_cancel(true);
                else {
                    var context = eventArgs.get_context();
                    if (jp.get_value() == "2") context["filter"] = eventArgs.get_text() + "|" + poli.get_value().split('#')[0] + "|" + jp.get_value() + "|" + formatDate(tgl.get_selectedDate());
                    else context["filter"] = eventArgs.get_text() + "||" + jp.get_value() + "|" + formatDate(tgl.get_selectedDate());
                }
            }

            function formatDate(date) {
                var d = new Date(date),
                    month = '' + (d.getMonth() + 1),
                    day = '' + d.getDate(),
                    year = d.getFullYear();

                if (month.length < 2)
                    month = '0' + month;
                if (day.length < 2)
                    day = '0' + day;

                return [year, month, day].join('-');
            }

            function OnClientItemsRequestingHandlerRegistrasi(sender, eventArgs) {
                var jp = $find("<%= cboNoMRSep.ClientID %>");

                if (jp.get_value() == '') {
                    alert('No MR belum dipilih');
                    eventArgs.set_cancel(true)
                }

                if (sender.get_text().length < 3) eventArgs.set_cancel(true);
                else {
                    var context = eventArgs.get_context();
                    context["filter"] = eventArgs.get_text() + "|" + jp.get_value();
                }
            }

            function openWinCreatePasien() {
                var bpjsNo = $find("<%= txtNoPesertaPeserta.ClientID %>");
                if (bpjsNo.get_value() == '') {
                    alert('No Peserta belum ada');
                    return;
                }

                var jp = $find("<%= cboPelayanan.ClientID %>");
                if (jp.get_value() == '') return;

                var rt = '';
                if (jp.get_value() == '1') rt = 'IPR'
                else rt = 'OPR'

                var oWnd = $find("<%= winRujukan.ClientID %>");
                oWnd.setUrl('../../../Registration/PatientDetail.aspx?md=new&pid=&pt=directpatient&rt=' + rt + '&bpjsNo=' + bpjsNo.get_value() + '&type=bpjs');
                oWnd.set_width($(window).width());
                oWnd.show();
            }

            function openWinRujukan(type) {
                var perertaNo = $find("<%= txtNoPesertaPeserta.ClientID %>");
                var rujukanNo = $find("<%= cboNoRujukan.ClientID %>");

                var oWnd = $find("<%= winRujukan.ClientID %>");
                oWnd.setUrl('../BpjsRujukanDialog.aspx?type=' + type + '&bpjsNo=' + perertaNo.get_value() + '&rujukanNo=' + rujukanNo.get_value());
                oWnd.set_width($(window).width());
                oWnd.set_title('Cari Rujukan');
                oWnd.show();
            }

            function openWinSearchPasien() {
                var oWnd = $find("<%= winRujukan.ClientID %>");
                oWnd.setUrl('../BpjsSearchPatientDialog.aspx?type=1&poli=&mrn=');
                oWnd.set_width($(window).width());
                oWnd.set_title('Cari Pasien');
                oWnd.show();
            }

            function openWinSearchSuplesi() {
                var bpjsNo = $find("<%= txtNoPesertaPeserta.ClientID %>");
                if (bpjsNo.get_value() == '') {
                    alert('No Peserta belum ada');
                    return;
                }

                var oWnd = $find("<%= winRujukan.ClientID %>");
                oWnd.setUrl('BpjsVClaimSuplesiDialog.aspx?id=' + bpjsNo.get_value());
                oWnd.set_width($(window).width());
                oWnd.set_title('Cari Suplesi Jasa Raharja');
                oWnd.show();
            }

            function openWinSearchAppointment() {
                var klinik = '';
                var jp = $find("<%= cboPelayanan.ClientID %>");
                if (jp.get_value() == '1') {
                    alert('Jenis Pelayanan hanya untuk rawat jalan');
                    return;
                }
                var pid = $find("<%= txtNoMrPeserta.ClientID %>");
                var pasien = pid.get_value();

                var oWnd = $find("<%= winRujukan.ClientID %>");
                oWnd.setUrl('../BpjsSearchPatientDialog.aspx?type=2&poli=' + klinik + "&mrn=" + pasien);
                oWnd.set_width($(window).width());
                oWnd.set_title('Cari Appointment');
                oWnd.show();
            }

            function openWinSearchSkdp() {
                var bpjsNo = $find("<%= txtNoPesertaPeserta.ClientID %>");
                if (bpjsNo.get_value() == '') {
                    alert('No Peserta belum ada');
                    return;
                }

                var oWnd = $find("<%= winRujukan.ClientID %>");
                oWnd.setUrl('BpjsSkdpDialog.aspx?noka=' + bpjsNo.get_value());
                oWnd.set_width($(window).width());
                oWnd.set_title('Cari SKDP');
                oWnd.show();
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.mode != null) {
                    var str = oWnd.argument.mode.split('!');
                    if (str[1] == 'pasien') __doPostBack("<%= cboNoMRSep.UniqueID %>", oWnd.argument.mode);
                    else if (str[1] == 'appointment') __doPostBack("<%= txtAppointmentSep.UniqueID %>", oWnd.argument.mode);
                    else if (str[1] == 'suplesi') {
                        var suplesi = $find("<%= txtSuplesiSep.ClientID %>");
                        suplesi.set_value(str[2]);
                    }
                }
            }

            function openWindowSepToReg() {
                var bpjsNo = $find("<%= txtNoSep.ClientID %>");
                if (bpjsNo.get_value() == '') {
                    alert('No SEP belum ada');
                    return;
                }

                var pid = $find("<%= cboNoMRSep.ClientID %>");
                if (pid.get_value() == '') {
                    alert('No MR pasien belum diisi');
                    return;
                }

                //var isLinked = "<%= IsLinkedToRegistration %>";
                //if (isLinked != "") {
                //    alert('No SEP sudah di registrasi, ref : ' + isLinked);
                //    return;
                //}

                var jp = $find("<%= cboPelayanan.ClientID %>");
                if (jp.get_value() == '') return;

                var reg = $find("<%= cboRegistrasiIGD.ClientID %>");

                if (jp.get_value() == 'X') {
                    if (reg.get_value() == '') {
                        alert('Registrasi IGD atau Poliklinik belum diisi');
                        return;
                    }
                }

                var rt = '';
                if (jp.get_value() == '1') rt = 'IPR'
                else {
                    var poli = $find("<%= cboPoliSep.ClientID %>");
                    if (poli.get_value().split('#')[0] == 'IGD' || poli.get_value().split('#')[0] == 'UGD') rt = 'EMR';
                    else rt = 'OPR'
                }

                var apptNo = $find("<%= txtAppointmentSep.ClientID %>");

                var oWnd = window.$find("<%= winRegistrasi.ClientID %>");
                if (apptNo.get_value() == '') {
                    if (jp.get_value() == '1') {
                        if (reg.get_value() == '') {
                            oWnd.setUrl("../../../Registration/RegistrationDetail.aspx?md=new&id=" + pid.get_value().split('|')[1] + "&sep=" + bpjsNo.get_value() + "&rt=" + rt + '&type=bpjs&norm=');
                        }
                        else {
                            oWnd.setUrl("../../../Registration/RegistrationDetail.aspx?md=new&id=" + pid.get_value().split('|')[1] + "&sep=" + bpjsNo.get_value() + "&rt=" + rt + '&type=bpjs&norm=&trans=1&reg=' + reg.get_value());
                        }
                    }
                    else {
                        oWnd.setUrl("../../../Registration/RegistrationDetail.aspx?md=new&id=" + pid.get_value().split('|')[1] + "&sep=" + bpjsNo.get_value() + "&rt=" + rt + '&type=bpjs&norm=');
                    }
                }
                else oWnd.setUrl("../../../Registration/RegistrationDetail.aspx?md=new&id=" + pid.get_value().split('|')[1] + "&sep=" + bpjsNo.get_value() + "&rt=" + rt + '&type=bpjs&norm=&apptNo=' + apptNo.get_value());
                oWnd.set_width($(window).width());
                oWnd.show();
            }

            function openWinCheckService() {
                var oWnd = $find("<%= WinStatus.ClientID %>");
                oWnd.setUrl("BpjsVclaimCheckServiceDialog.aspx");
                oWnd.set_title('Avicenna - BPJS Service Status');
                oWnd.show();
            }

            function setNoSepAsRujukan(noSep) {
                var oWnd = $find("<%= cboNoRujukan.ClientID %>");
                oWnd.set_text(noSep);
            }

            function openWinEditPatient(sender, eventArgs) {
                //var bpjsNo = $find("<%= txtNoPesertaPeserta.ClientID %>");
                //if (sender.get_value() == '') {
                //    alert('No peserta pasien belum ada');
                //    return;
                //}

                var isnew = "<%= IsNewPatient %>";
                var response = isnew.split('|');
                if (response[0] != '0') {
                    alert(isnew);
                    return;
                }

                console.log(response);

                var jp = $find("<%= cboPelayanan.ClientID %>");
                if (jp.get_value() == '') return;

                var rt = '';
                if (jp.get_value() == '1') rt = 'IPR'
                else rt = 'OPR'

                var oWnd = $find("<%= winRujukan.ClientID %>");
                oWnd.setUrl('../../../Registration/PatientDetail.aspx?md=edit&pid=&pt=directpatient&rt=' + rt + '&noka=' + response[1]);
                oWnd.set_width($(window).width());
                oWnd.show();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Behavior="Close, Move" ShowContentDuringLoad="False"
        Width="750px" Height="720px" VisibleStatusbar="False" Modal="true" ID="winRujukan"
        OnClientClose="onClientClose" />
    <telerik:RadWindow runat="server" Animation="None" Behavior="Close, Move" ShowContentDuringLoad="False"
        Width="750px" Height="720px" VisibleStatusbar="False" Modal="true" ID="winRegistrasi" />
    <telerik:RadWindow runat="server" Animation="None" Behavior="Move" ShowContentDuringLoad="False"
        Width="500px" Height="10px" VisibleStatusbar="False" Modal="false" ID="WinStatus" />
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table>
                    <tr>
                        <td class="label">Jenis
                        </td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rblJenis" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                OnSelectedIndexChanged="rblJenis_SelectedIndexChanged">
                                <asp:ListItem Text="No Rujukan" Value="1" />
                                <asp:ListItem Text="No Kartu" Value="2" />
                                <asp:ListItem Text="No SEP" Value="3" Enabled="false" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <asp:Panel ID="pnlFilter" runat="server">
                        <tr>
                            <td class="label">Pelayanan
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboPelayanan" runat="server" Width="300px" AutoPostBack="true"
                                    OnSelectedIndexChanged="cboPelayanan_SelectedIndexChanged">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="1" Text="Rawat Inap" />
                                        <telerik:RadComboBoxItem Value="2" Text="Rawat Jalan" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                        <tr>
                            <td class="label">Tgl. SEP
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtTglSep" runat="server" Width="100px" DateInput-DateFormat="yyyy-MM-dd"
                                    DateInput-DisplayDateFormat="yyyy-MM-dd" AutoPostBack="true" OnSelectedDateChanged="txtTglSep_SelectedDateChanged" />
                                *yyyy-mm-dd
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlRujukan" runat="server">
                        <tr>
                            <td class="label">Asal Rujukan
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboAsalRujukan" runat="server" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="1" Text="Faskes 1" Selected="true" />
                                        <telerik:RadComboBoxItem Value="2" Text="Faskes 2 (RS)" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlKartu" runat="server">
                        <tr>
                            <td class="label">Jenis Kartu
                            </td>
                            <td class="entry">
                                <asp:RadioButtonList ID="rblJenisKartu" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="BPJS" Value="1" Selected="True" />
                                    <asp:ListItem Text="NIK (e-KTP)" Value="2" />
                                    <asp:ListItem Text="NIK Reader (e-KTP Reader)" Value="3" />
                                </asp:RadioButtonList>
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNomor" runat="server" Text="No" />
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtNomor" runat="server" Width="300px" />
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnCariRujukan" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClientClick="openWinRujukan('0'); return false;" ToolTip="Cari Rujukan" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="No kartu required."
                                ValidationGroup="entry" ControlToValidate="txtNomor" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label" />
                        <td class="entry">
                            <asp:Button ID="btnCariData" runat="server" Text="Cari Data" OnClick="btnCariData_Click" />
                            <asp:Button ID="btnClearData" runat="server" Text="Clear Data" OnClick="btnClearData_Click" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
                <hr />
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="MultiPage1" ShowBaseLine="true"
                    Orientation="HorizontalTop">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Detail" PageViewID="pgDetail" Selected="True" />
                        <telerik:RadTab runat="server" Text="Riwayat" PageViewID="pgRiwayat" />
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="MultiPage1" runat="server" BorderStyle="Solid" BorderColor="Gray">
                    <telerik:RadPageView ID="pgDetail" runat="server" Selected="true">
                        <table>
                            <tr>
                                <td class="label">Nama
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtNamaPeserta" runat="server" Width="300px" ReadOnly="true" />
                                </td>
                                <td width="20px" />
                                <td />
                            </tr>
                            <tr>
                                <td class="label">No Peserta
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtNoPesertaPeserta" runat="server" Width="300px" ReadOnly="true" ShowButton="false" ClientEvents-OnButtonClick="openWinEditPatient" />
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="No peserta required."
                                        ValidationGroup="entry" ControlToValidate="txtNoPesertaPeserta" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">NIK
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtNikPeserta" runat="server" Width="300px" ReadOnly="true" />
                                </td>
                                <td width="20px" />
                                <td />
                            </tr>
                            <tr>
                                <td class="label">Tgl. Lahir / Umur
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtTglLahirPeserta" runat="server" Width="100px" DateInput-ReadOnly="true"
                                                    DatePopupButton-Enabled="false" DateInput-DateFormat="yyyy-MM-dd" DateInput-DisplayDateFormat="yyyy-MM-dd" />
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtUmurPeserta" runat="server" Width="194px" ReadOnly="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px" />
                                <td />
                            </tr>
                            <tr>
                                <td class="label">Jenis Kelamin
                                </td>
                                <td class="entry">
                                    <asp:RadioButtonList ID="rblJenisKelamin" runat="server" RepeatDirection="Horizontal"
                                        Enabled="false">
                                        <asp:ListItem Text="Laki-laki" Value="L" />
                                        <asp:ListItem Text="Perempuan" Value="P" />
                                    </asp:RadioButtonList>
                                </td>
                                <td width="20px" />
                                <td />
                            </tr>
                            <tr>
                                <td class="label">No MR / No Telepon
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadTextBox ID="txtNoMrPeserta" runat="server" Width="143px" ReadOnly="true" />
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>/
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtNoTelpPeserta" runat="server" Width="144px" ReadOnly="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px" />
                                <td />
                            </tr>
                            <tr>
                                <td class="label">Hak Kelas
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtHakKelasPeserta" runat="server" Width="300px" ReadOnly="true" />
                                </td>
                                <td width="20px" />
                                <td />
                            </tr>
                            <tr>
                                <td class="label">Faskes Tingkat 1
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtFaskesTingkat1" runat="server" Width="300px" ReadOnly="true" />
                                </td>
                                <td width="20px" />
                                <td />
                            </tr>
                            <tr>
                                <td class="label">Jenis Peserta
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtJenisPeserta" runat="server" Width="300px" ReadOnly="true" />
                                </td>
                                <td width="20px" />
                                <td />
                            </tr>
                            <tr>
                                <td class="label">Status Peserta
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtStatusPeserta" runat="server" Width="300px" ReadOnly="true" />
                                </td>
                                <td width="20px" />
                                <td />
                            </tr>
                            <tr>
                                <td class="label">COB
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtCOBPeserta" runat="server" Width="300px" ReadOnly="true" />
                                </td>
                                <td width="20px" />
                                <td />
                            </tr>
                            <tr>
                                <td class="label">Informasi
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtInformasiPeserta" runat="server" Width="300px" ReadOnly="true" />
                                </td>
                                <td width="20px" />
                                <td />
                            </tr>
                            <tr>
                                <td class="label" />
                                <td class="entry">
                                    <asp:Button ID="btnCreatePatient" runat="server" Text="Pasien Baru" OnClientClick="javascript:openWinCreatePasien();return false;" />
                                </td>
                                <td width="20px" />
                                <td />
                            </tr>
                        </table>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pgRiwayat" runat="server">
                        <table>
                            <tr>
                                <td class="label">Periode</td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtPeriode1" runat="server" Width="100px" DateInput-DateFormat="yyyy-MM-dd" DateInput-DisplayDateFormat="yyyy-MM-dd" />
                                            </td>
                                            <td>&nbsp;-&nbsp;</td>
                                            <td>
                                                <telerik:RadDatePicker ID="txtPeriode2" runat="server" Width="100px" DateInput-DateFormat="yyyy-MM-dd" DateInput-DisplayDateFormat="yyyy-MM-dd" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label"></td>
                                <td class="entry">
                                    <asp:Button ID="btnCariHistory" runat="server" Text="Cari Data" OnClick="btnCariHistory_Click" />
                                </td>
                                <td />
                            </tr>
                        </table>
                        <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="False" ShowGroupPanel="false"
                            AllowPaging="True" PageSize="10" AllowSorting="False" GridLines="None">
                            <MasterTableView DataKeyNames="noSep" ClientDataKeyNames="noSep">
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderStyle-Width="150px" HeaderText="No SEP"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# string.Format("<a href=\"#\" onclick=\"setNoSepAsRujukan('{0}'); return false;\"><b>{0}</b></a>", DataBinder.Eval(Container.DataItem, "noSep"))%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="noRujukan" HeaderText="No Rujukan"
                                        UniqueName="noRujukan" SortExpression="noRujukan" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridDateTimeColumn HeaderStyle-Width="70px" DataField="tglSep" HeaderText="Tgl SEP"
                                        HeaderStyle-HorizontalAlign="Center" UniqueName="tglSep" SortExpression="tglSep"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridDateTimeColumn HeaderStyle-Width="70px" DataField="tglPlgSep" HeaderText="Tgl Pulang"
                                        HeaderStyle-HorizontalAlign="Center" UniqueName="tglPlgSep" SortExpression="tglPlgSep"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridBoundColumn DataField="ppkPelayanan" HeaderText="PPK Pelayanan" UniqueName="ppkPelayanan"
                                        SortExpression="ppkPelayanan" />
                                    <telerik:GridBoundColumn DataField="poli" HeaderText="Poli Tujuan" UniqueName="poli"
                                        SortExpression="poli" />
                                    <telerik:GridBoundColumn DataField="diagnosa" HeaderText="Diagnosa" UniqueName="diagnosa"
                                        SortExpression="diagnosa" />
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </td>
            <td width="50%" valign="top">
                <table>
                    <tr>
                        <td class="label">No SEP
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNoSep" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <asp:Panel ID="pnlRajal" runat="server">
                        <tr>
                            <td class="label">Poli
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkEksekutifSep" runat="server" Text="Eksekutif" />
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cboPoliSep" runat="server" Width="234px" AllowCustomText="true" AutoPostBack="true" OnSelectedIndexChanged="cboPoliSep_SelectedIndexChanged"
                                                OnItemDataBound="cboPoliSep_ItemDataBound" EnableLoadOnDemand="true" OnClientItemsRequesting="OnClientItemsRequestingHandler">
                                                <WebServiceSettings Path="../../../../../WebService/Sep.asmx" Method="GetPoli" />
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Poli required."
                                    ValidationGroup="entry" ControlToValidate="cboPoliSep" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td />
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td class="label">Asal Rujukan
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboJenisRujukanSep" runat="server" Width="300px">
                                <Items>
                                    <telerik:RadComboBoxItem Value="1" Text="Faskes 1" Selected="true" />
                                    <telerik:RadComboBoxItem Value="2" Text="Faskes 2 (RS)" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">PPK Asal Rujukan
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboAsalRujukanSep" runat="server" Width="300px" AllowCustomText="true"
                                OnItemDataBound="cboAsalRujukanSep_ItemDataBound" EnableLoadOnDemand="true" OnClientItemsRequesting="OnClientItemsRequestingHandler">
                                <WebServiceSettings Path="../../../../../WebService/Sep.asmx" Method="GetAsalRujukan" />
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="PPK asal rujukan required."
                                ValidationGroup="entry" ControlToValidate="cboAsalRujukanSep" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Tgl. Rujukan
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTglRujukanSep" runat="server" Width="100px" DateInput-DateFormat="yyyy-MM-dd"
                                DateInput-DisplayDateFormat="yyyy-MM-dd" />
                            *yyyy-mm-dd
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Tgl rujukan required."
                                ValidationGroup="entry" ControlToValidate="txtTglRujukanSep" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">No Rujukan
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboNoRujukan" runat="server" Width="300px" AllowCustomText="true"
                                AutoPostBack="true" OnItemDataBound="cboNoRujukan_ItemDataBound" OnSelectedIndexChanged="cboNoRujukan_SelectedIndexChanged">
                                <ItemTemplate>
                                    No. Rujukan :
                                    <%# DataBinder.Eval(Container.DataItem, "NoKunjungan")%><br />
                                    Tgl. Rujukan :
                                    <%# DataBinder.Eval(Container.DataItem, "TglKunjungan")%><br />
                                    PPK Rujukan :
                                    <%# DataBinder.Eval(Container.DataItem, "ProvPerujuk.Nama")%><br />
                                    Poli Rujukan :
                                    <%# DataBinder.Eval(Container.DataItem, "PoliRujukan.Nama")%>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Nama DPJP Perujuk
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboDpjpSep" runat="server" Width="300px" AllowCustomText="true"
                                OnItemDataBound="cboDpjpSep_ItemDataBound" EnableLoadOnDemand="true" OnClientItemsRequesting="OnClientItemsRequestingHandlerDpjp">
                                <WebServiceSettings Path="../../../../../WebService/Sep.asmx" Method="GetDPJP" />
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Nama DPJP Pelayanan
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboDpjpPelayanan" runat="server" Width="300px" AllowCustomText="true"
                                OnItemDataBound="cboDpjpSep_ItemDataBound" EnableLoadOnDemand="true" OnClientItemsRequesting="OnClientItemsRequestingHandlerDpjp">
                                <WebServiceSettings Path="../../../../../WebService/Sep.asmx" Method="GetDPJPByUnitMapping" />
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <%--                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Nama DPJP Pelayanan required."
                                ValidationGroup="entry" ControlToValidate="cboDpjpPelayanan" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>--%>
                        </td>
                        <td />
                    </tr>
                    <%--                    <tr>
                        <td class="label">Tgl. SEP
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTglSepSep" runat="server" Width="100px" DateInput-DateFormat="yyyy-MM-dd"
                                DateInput-DisplayDateFormat="yyyy-MM-dd" />
                            *yyyy-mm-dd
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Tgl. SEP required."
                                ValidationGroup="entry" ControlToValidate="txtTglSepSep" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>--%>
                    <tr>
                        <td class="label">No MR
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboNoMRSep" runat="server" Width="300px" AllowCustomText="true"
                                            EnableLoadOnDemand="true" OnClientItemsRequesting="OnClientItemsRequestingHandler"
                                            OnItemDataBound="cboNoMRSep_ItemDataBound">
                                            <WebServiceSettings Path="../../../../../WebService/Sep.asmx" Method="GetPasien" />
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnCariPasien" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClientClick="openWinSearchPasien(); return false;" ToolTip="Cari Pasien" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="No MR required."
                                ValidationGroup="entry" ControlToValidate="cboNoMRSep" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <asp:Panel ID="pnlRanap" runat="server">
                        <tr>
                            <td class="label">Kelas Rawat
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboKelasRawatSep" runat="server" Width="300px" DataTextField="Nama"
                                    DataValueField="Kode" />
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Kelas rawat required."
                                    ValidationGroup="entry" ControlToValidate="cboKelasRawatSep" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">Kelas Rawat Naik
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboKelasRawatNaik" runat="server" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="" Text="" />
                                        <telerik:RadComboBoxItem Value="1" Text="VVIP" />
                                        <telerik:RadComboBoxItem Value="2" Text="VIP" />
                                        <telerik:RadComboBoxItem Value="3" Text="Kelas 1" />
                                        <telerik:RadComboBoxItem Value="4" Text="Kelas 2" />
                                        <telerik:RadComboBoxItem Value="5" Text="Kelas 3" />
                                        <telerik:RadComboBoxItem Value="6" Text="ICCU" />
                                        <telerik:RadComboBoxItem Value="7" Text="ICU" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                        <tr>
                            <td class="label">Pembiayaan
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboPembiayaan" runat="server" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="" Text="" />
                                        <telerik:RadComboBoxItem Value="1" Text="Pribadi" />
                                        <telerik:RadComboBoxItem Value="2" Text="Pemberi Kerja" />
                                        <telerik:RadComboBoxItem Value="3" Text="Asuransi Kesehatan Tambahan" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td class="label">Diagnosa
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboDiagnosaSep" runat="server" Width="300px" AllowCustomText="true"
                                OnItemDataBound="cboDiagnosaSep_ItemDataBound" EnableLoadOnDemand="true" OnClientItemsRequesting="OnClientItemsRequestingHandler">
                                <WebServiceSettings Path="../../../../../WebService/Sep.asmx" Method="GetDiagnosa" />
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">No Telepon
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNoTeleponSep" runat="server" Width="300px" MaxLength="14" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="No telepon required."
                                ValidationGroup="entry" ControlToValidate="txtNoTeleponSep" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Catatan
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtCatatanSep" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label" />
                        <td class="entry">
                            <asp:CheckBox ID="chkCobSep" runat="server" Text="COB" AutoPostBack="true" OnCheckedChanged="chkCobSep_CheckedChanged" />
                            <asp:CheckBox ID="chkKatarakSep" runat="server" Text="Katarak" />
                            <asp:CheckBox ID="chkPenjaminKLLSep" runat="server" Text="Penjamin KLL" AutoPostBack="true"
                                OnCheckedChanged="chkPenjaminKLLSep_CheckedChanged" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <asp:Panel ID="pnlLakaLantas" runat="server">
                        <tr>
                            <td class="label">Jenis Kejadian
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboJenisKejadianLaka" runat="server" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="" Text="" />
                                        <telerik:RadComboBoxItem Value="0" Text="Bukan Kecelakaan lalu lintas [BKLL]" />
                                        <telerik:RadComboBoxItem Value="1" Text="KLL dan bukan kecelakaan Kerja [BKK]" />
                                        <telerik:RadComboBoxItem Value="2" Text="KLL dan KK" />
                                        <telerik:RadComboBoxItem Value="3" Text="Kecelakaan Kerja" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                        <tr>
                            <td class="label">No LP
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtNoLp" runat="server" Width="300px" />
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                        <tr>
                            <td class="label">Tgl. Kejadian
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtTglKejadianSep" runat="server" Width="100px" DateInput-DateFormat="yyyy-MM-dd"
                                    DateInput-DisplayDateFormat="yyyy-MM-dd" />
                                *yyyy-mm-dd
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                        <tr>
                            <td class="label">Keterangan
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtLokasiKLLSep" runat="server" Width="300px" TextMode="MultiLine" />
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                        <tr>
                            <td class="label">Lokasi KLL
                            </td>
                            <td class="entry">
                                <table cellpadding="1" cellspacing="1" width="100%">
                                    <tr>
                                        <td style="padding-left: 0">
                                            <telerik:RadComboBox ID="cboPropinsiSep" runat="server" Width="303px" AllowCustomText="true"
                                                EmptyMessage="Select Propinsi..." Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboPropinsiSep_SelectedIndexChanged" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 0">
                                            <telerik:RadComboBox ID="cboKabupatenSep" runat="server" Width="303px" AllowCustomText="true"
                                                EmptyMessage="Select Kabupaten..." Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboKabupatenSep_SelectedIndexChanged" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 0">
                                            <telerik:RadComboBox ID="cboKecamatanSep" runat="server" Width="303px" AllowCustomText="true"
                                                EmptyMessage="Select Kecamatan..." Filter="Contains" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                        <tr>
                            <td class="label" />
                            <td class="entry">
                                <asp:CheckBox ID="chkSuplesiSep" runat="server" Text="Suplesi" AutoPostBack="true"
                                    OnCheckedChanged="chkSuplesiSep_CheckedChanged" />
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                        <asp:Panel ID="pnlSuplesi" runat="server">
                            <tr>
                                <td class="label">No Sep Suplesi
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadTextBox ID="txtSuplesiSep" runat="server" Width="300px" />
                                            </td>
                                            <td>&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnCariSuplesi" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                                    OnClientClick="openWinSearchSuplesi(); return false;" ToolTip="Cari Suplesi Jasa Raharja" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px" />
                                <td />
                            </tr>
                        </asp:Panel>
                    </asp:Panel>
                    <tr>
                        <td class="label">Tujuan Kunjungan
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboTujuanKunjungan" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="cboTujuanKunjungan_SelectedIndexChanged">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" />
                                    <telerik:RadComboBoxItem Value="0" Text="Normal" />
                                    <telerik:RadComboBoxItem Value="1" Text="Prosedur" />
                                    <telerik:RadComboBoxItem Value="2" Text="Konsul Dokter" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Flag Procedure
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboFlagProcedure" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="cboFlagProcedure_SelectedIndexChanged">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" />
                                    <telerik:RadComboBoxItem Value="0" Text="Prosedur Tidak Berkelanjutan" />
                                    <telerik:RadComboBoxItem Value="1" Text="Prosedur dan Terapi Berkelanjutan" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Kode Penunjang
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboKodePenunjang" runat="server" Width="300px">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" />
                                    <telerik:RadComboBoxItem Value="1" Text="Radioterapi" />
                                    <telerik:RadComboBoxItem Value="2" Text="Kemoterapi" />
                                    <telerik:RadComboBoxItem Value="3" Text="Rehabilitasi Medik" />
                                    <telerik:RadComboBoxItem Value="4" Text="Rehabilitasi Psikososial" />
                                    <telerik:RadComboBoxItem Value="5" Text="Transfusi Darah" />
                                    <telerik:RadComboBoxItem Value="6" Text="Pelayanan Gigi" />
                                    <telerik:RadComboBoxItem Value="7" Text="Laboratorium" />
                                    <telerik:RadComboBoxItem Value="8" Text="USG" />
                                    <telerik:RadComboBoxItem Value="9" Text="Farmasi" />
                                    <telerik:RadComboBoxItem Value="10" Text="Lain-Lain" />
                                    <telerik:RadComboBoxItem Value="11" Text="MRI" />
                                    <telerik:RadComboBoxItem Value="12" Text="HEMODIALISA" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Assesment Pelayanan
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboAssesmentPelayanan" runat="server" Width="300px">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" />
                                    <telerik:RadComboBoxItem Value="1" Text="Poli spesialis tidak tersedia pada hari sebelumnya" />
                                    <telerik:RadComboBoxItem Value="2" Text="Jam Poli telah berakhir pada hari sebelumnya" />
                                    <telerik:RadComboBoxItem Value="3" Text="Dokter Spesialis yang dimaksud tidak praktek pada hari sebelumnya" />
                                    <telerik:RadComboBoxItem Value="4" Text="Atas Instruksi RS" />
                                    <telerik:RadComboBoxItem Value="5" Text="Tujuan Kontrol" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">No SKDP/SPRI
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboNoSkdp" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="cboNoSkdp_SelectedIndexChanged" OnItemDataBound="cboNoSkdp_ItemDataBound"
                                            AllowCustomText="true" Filter="Contains">
                                            <ItemTemplate>
                                                No. Surat Kontrol :
                                                <%# DataBinder.Eval(Container.DataItem, "NoSuratKontrol")%><br />
                                                Tipe Surat Kontrol :
                                                <%# DataBinder.Eval(Container.DataItem, "JenisSuratKontrol")%><br />
                                                Tgl. Kontrol :
                                                <%# DataBinder.Eval(Container.DataItem, "TglRencanaKontrol")%><br />
                                                Poli Kontrol :
                                                <%# DataBinder.Eval(Container.DataItem, "NamaPoliTujuan")%><br />
                                                Dpjp Kontrol :
                                                <%# DataBinder.Eval(Container.DataItem, "NamaDokter")%>
                                            </ItemTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td style="display: none">
                                        <asp:ImageButton ID="btnCariSKDP" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClientClick="openWinSearchSkdp(); return false;" ToolTip="Cari Surat Kontrol" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Nama DPJP SKDP/SPRI
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboDpjpKontrol" runat="server" Width="300px" AllowCustomText="true"
                                OnItemDataBound="cboDpjpSep_ItemDataBound" EnableLoadOnDemand="true" OnClientItemsRequesting="OnClientItemsRequestingHandlerDpjp">
                                <WebServiceSettings Path="../../../../../WebService/Sep.asmx" Method="GetDPJP" />
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">No Appointment
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtAppointmentSep" runat="server" Width="300px" />
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnCariAppointment" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClientClick="openWinSearchAppointment(); return false;" ToolTip="Cari Appointment" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <asp:Panel ID="pnlRegIGD" runat="server">
                        <tr>
                            <td class="label">Registrasi IGD / RAJAL (Transfer To RANAP)
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboRegistrasiIGD" runat="server" Width="300px" AllowCustomText="true"
                                    OnItemDataBound="cboRegistrasiIGD_ItemDataBound" EnableLoadOnDemand="true" OnClientItemsRequesting="OnClientItemsRequestingHandlerRegistrasi">
                                    <WebServiceSettings Path="../../../../../WebService/Sep.asmx" Method="GetRegistrasiIGD" />
                                    <FooterTemplate>
                                        10 data registrasi terakhir
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td class="label">Registrasi IGD / RAJAL / RANAP (Link To Registration)
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboRegistrasi" runat="server" Width="304px" AllowCustomText="true"
                                OnItemDataBound="cboRegistrasiIGD_ItemDataBound" EnableLoadOnDemand="true" OnClientItemsRequesting="OnClientItemsRequestingHandlerRegistrasi">
                                <WebServiceSettings Path="../../../../../WebService/Sep.asmx" Method="GetRegistrasiIGD" />
                                <FooterTemplate>
                                    10 data registrasi terakhir
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
