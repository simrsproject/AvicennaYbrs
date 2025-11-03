<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    EnableEventValidation="false" Title="Inacbg E-Klaim" CodeBehind="eKlaimDetail.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Integration.Eklaim.eKlaimDetail" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    </telerik:RadScriptBlock>
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script type="text/javascript">
            function KeyPressDiagnosa(sender, eventArgs) {
                if (eventArgs.get_domEvent().keyCode == 13) {
                    sender.raise_ItemsRequested();
                }
            }

            function KeyPressProsedur(sender, eventArgs) {
                if (eventArgs.get_domEvent().keyCode == 13) {
                    __doPostBack('btnFilterProsedur', '');
                }
            }

            function openWinPayment(guarIdBuff, seqNo) {
                if ('RSI' == '<%= AppSession.Parameter.HealthcareID %>') {
                    __doPostBack("<%= btnFinal.UniqueID %>", 'payment');
                }
                else {
                    var isValid = "<%= IsReceiptAvalilable %>";

                    if (isValid == '0') {
                        var oWnd = $find("<%= winPayment.ClientID %>");
                        var oguar = $find("<%= cboJaminan.ClientID %>");

                        oWnd.setUrl('../../../Charges/Billing/FinalizeBilling/GuarantorPaymentReceiveExcludeDiscount.aspx?regNo=' + '<%= Request.QueryString["regno"] %>' + "&guarId=" + oguar.get_value().split('|')[0] + "&guarIdBuff=" + guarIdBuff + "&seqNo=" + seqNo + "&src=eklaim");
                        oWnd.set_width(1000);
                        oWnd.show();
                        //oWnd.maximize();
                        //oWnd.add_pageLoad(onClientPageLoad);
                    }
                    else {
                        __doPostBack("<%= btnFinal.UniqueID %>", 'payment');
                    }
                }
            }

            function openPrint() {
                var pesertano = $find("<%= txtNoPeserta.ClientID %>");
                var sepno = $find("<%= txtNoSep.ClientID %>");
                if (pesertano.get_value() == '' || sepno.get_value() == '') {
                    alert('No. Peserta dan No. SEP tidak boleh kosong.');
                    return;
                }

                var final = true; <%--"<%= btnFinal.Enabled == true %>";--%>
                if (final == false) {
                    alert('Status klaim belum final.');
                    return;
                }

                var url = "<%= Helper.UrlRoot() %>/Module/Reports/PdfUrlViewer.aspx?mode=eklaim&trno=" + sepno.get_value();
                //openWinEntryMaximize(url);
                //window.open(url);
                var oWnd = $find("<%= winPayment.ClientID %>");
                oWnd.setUrl(url);
                oWnd.set_title('Print');
                oWnd.show();
                oWnd.maximize();
            }

            function validasiSitb() {
                var sitbno = $find("<%= txtSitbNo.ClientID %>");
                if (sitbno.get_value() == '') {
                    alert('No Sitb tidak boleh kosong.');
                    return;
                }

                var sepno = $find("<%= txtNoSep.ClientID %>");

                var url = "SitbDialog.aspx?nosep=" + sepno.get_value() + "&nositb=" + sitbno.get_value();
                //openWinEntryMaximize(url);
                //window.open(url);
                var oWnd = $find("<%= winPayment.ClientID %>");
                oWnd.setUrl(url);
                oWnd.set_title('Konfirmasi Validasi Register SITB');
                oWnd.set_width(500);
                oWnd.show();
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument) {
                    if (oWnd.argument == 'payment') {
                        __doPostBack("<%= btnFinal.UniqueID %>", 'payment');
                        oWnd.argument = 'undefined';
                    }
                }
            }

            function OnClientItemsRequestingHandlerDiagnosa(sender, eventArgs) {
                if (sender.get_text().length < 3) {
                    eventArgs.set_cancel(true);
                }
                else {
                    var context = eventArgs.get_context();
                    context["filter"] = eventArgs.get_text();
                }
            }

            function DiagnosaKeyPressing(sender, eventArgs) {
                if (sender.get_value() != '' && eventArgs.get_domEvent().keyCode == 13) {
                    $('#<%= btnInsertDiagnosa.ClientID %>').click();
                }
            }

            function ProsedurKeyPressing(sender, eventArgs) {
                if (sender.get_value() != '' && eventArgs.get_domEvent().keyCode == 13) {
                    $('#<%= btnInsertProsedur.ClientID %>').click();
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboNoPeserta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtNoPeserta" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboJaminan">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtNoSep" />
                    <telerik:AjaxUpdatedControl ControlID="cboCOB" />
                    <telerik:AjaxUpdatedControl ControlID="cboEpisode" />
                    <telerik:AjaxUpdatedControl ControlID="cboStatusCov" />
                    <telerik:AjaxUpdatedControl ControlID="rblKomorbid" />

                    <telerik:AjaxUpdatedControl ControlID="rblKriteriaAksesNaat" />
                    <telerik:AjaxUpdatedControl ControlID="rblRsDaruratLapangan" />
                    <telerik:AjaxUpdatedControl ControlID="rblIsolasiMandiri" />
                    <telerik:AjaxUpdatedControl ControlID="rblCoInsidens" />
                    <telerik:AjaxUpdatedControl ControlID="cboStatusKelainan" />
                    <telerik:AjaxUpdatedControl ControlID="txtTerapiKovalesen" />

                    <telerik:AjaxUpdatedControl ControlID="fuResumeMedis" />
                    <telerik:AjaxUpdatedControl ControlID="btnFuResumeMedis" />
                    <telerik:AjaxUpdatedControl ControlID="fuRuangPerawatan" />
                    <telerik:AjaxUpdatedControl ControlID="btnFuRuangPerawatan" />
                    <telerik:AjaxUpdatedControl ControlID="fuLaboratorium" />
                    <telerik:AjaxUpdatedControl ControlID="btnFuLaboratorium" />
                    <telerik:AjaxUpdatedControl ControlID="fuRadiologi" />
                    <telerik:AjaxUpdatedControl ControlID="btnFuRadiologi" />
                    <telerik:AjaxUpdatedControl ControlID="fuPenunjang" />
                    <telerik:AjaxUpdatedControl ControlID="btnFuPenunjang" />
                    <telerik:AjaxUpdatedControl ControlID="fuObat" />
                    <telerik:AjaxUpdatedControl ControlID="btnFuObat" />
                    <telerik:AjaxUpdatedControl ControlID="fuBilling" />
                    <telerik:AjaxUpdatedControl ControlID="btnFuBilling" />
                    <telerik:AjaxUpdatedControl ControlID="fuKartu" />
                    <telerik:AjaxUpdatedControl ControlID="btnFuKartu" />
                    <telerik:AjaxUpdatedControl ControlID="fuLainlain" />
                    <telerik:AjaxUpdatedControl ControlID="btnFuLainlain" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkNaikKelas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rblNaikKelasRawat" />
                    <telerik:AjaxUpdatedControl ControlID="trLamaNaikKelas" />
                    <telerik:AjaxUpdatedControl ControlID="txtLamaNaikKelas" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkRawatIntensif">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtRawatIntensif" />
                    <telerik:AjaxUpdatedControl ControlID="txtVentilator" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtTglPulang">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtLOS" />
                    <telerik:AjaxUpdatedControl ControlID="txtLamaNaikKelas" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboDiagnosa">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboDiagnosa" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterDiagnosa">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboDiagnosa" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnInsertDiagnosa">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboDiagnosa" />
                    <telerik:AjaxUpdatedControl ControlID="grdDiagnosa" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdDiagnosa">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboDiagnosa" />
                    <telerik:AjaxUpdatedControl ControlID="grdDiagnosa" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterProsedur">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboProsedur" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnInsertProsedur">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboProsedur" />
                    <telerik:AjaxUpdatedControl ControlID="grdProsedur" />
                    <telerik:AjaxUpdatedControl ControlID="cboDializer" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdProsedur">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboProsedur" />
                    <telerik:AjaxUpdatedControl ControlID="grdProsedur" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtProsedurNonBedah">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTarifRS" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtTenagaAhli">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTarifRS" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtRadiologi">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTarifRS" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtRehabilitasi">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTarifRS" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtObat">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTarifRS" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtSewaAlat">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTarifRS" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtProsedurBedah">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTarifRS" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtKeperawatan">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTarifRS" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtLaboratorium">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTarifRS" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtKamarAkomodasi">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTarifRS" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtObatKronis">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTarifRS" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtAlkes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTarifRS" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtKonsultasi">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTarifRS" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtPenunjang">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTarifRS" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtPelayananDarah">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTarifRS" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtRawatIntensifTarif">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTarifRS" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtObatKemoterapi">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTarifRS" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtBMHP">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTarifRS" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFuResumeMedis">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fuResumeMedis" />
                    <telerik:AjaxUpdatedControl ControlID="grdResumeMedis" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFuRuangPerawatan">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fuRuangPerawatan" />
                    <telerik:AjaxUpdatedControl ControlID="grdRuangPerawatan" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFuLaboratorium">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fuLaboratorium" />
                    <telerik:AjaxUpdatedControl ControlID="grdLaboratorium" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFuRadiologi">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fuRadiologi" />
                    <telerik:AjaxUpdatedControl ControlID="grdRadiologi" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFuPenunjang">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fuPenunjang" />
                    <telerik:AjaxUpdatedControl ControlID="grdPenunjang" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFuObat">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fuObat" />
                    <telerik:AjaxUpdatedControl ControlID="grdObat" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFuBilling">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fuBilling" />
                    <telerik:AjaxUpdatedControl ControlID="grdBilling" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFuKartu">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fuKartu" />
                    <telerik:AjaxUpdatedControl ControlID="grdKartu" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFuLainlain">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fuLainlain" />
                    <telerik:AjaxUpdatedControl ControlID="grdLainlain" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboEpisode">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTotalEpisode" />
                    <telerik:AjaxUpdatedControl ControlID="grdEpisodeRuangRawat" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdEpisodeRuangRawat">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEpisodeRuangRawat" />
                    <telerik:AjaxUpdatedControl ControlID="txtTotalEpisode" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rblJenisRawat">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTariffPoliEks" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdResumeMedis">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdResumeMedis" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdRuangPerawatan">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRuangPerawatan" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdLaboratorium">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdLaboratorium" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdRadiologi">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRadiologi" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPenunjang">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPenunjang" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdObat">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdObat" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdBilling">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdBilling" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdKartu">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdKartu" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdLainlain">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdLainlain" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboCaraPulang">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="chkPemulasaraanJenazah" />
                    <telerik:AjaxUpdatedControl ControlID="chkKantongJenazah" />
                    <telerik:AjaxUpdatedControl ControlID="chkPetiJenazah" />
                    <telerik:AjaxUpdatedControl ControlID="chkPlastikErat" />
                    <telerik:AjaxUpdatedControl ControlID="chkDesinfektanJenazah" />
                    <telerik:AjaxUpdatedControl ControlID="chkTransportMobil" />
                    <telerik:AjaxUpdatedControl ControlID="chkDesinfektanMobil" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnImportDiagnosa">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDiagnosa2" />
                    <telerik:AjaxUpdatedControl ControlID="grdProsedur2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnImportProcedure">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdProsedur2" />
                    <telerik:AjaxUpdatedControl ControlID="grdDiagnosa2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkPasienTb">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtSitbNo" />
                    <telerik:AjaxUpdatedControl ControlID="btnValidasiSitb" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkVentilator">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTglIntubasi" />
                    <telerik:AjaxUpdatedControl ControlID="txtJamIntubasi" />
                    <telerik:AjaxUpdatedControl ControlID="txtTglEkstubasi" />
                    <telerik:AjaxUpdatedControl ControlID="txtJamEkstubasi" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkCoinsidenseCovid">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtNomorKlaimCovid" />
                    <telerik:AjaxUpdatedControl ControlID="btnValidasiCoinsidenseCovid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdDelivery">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDelivery" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow ID="winPayment" Animation="None" Width="1000px" Height="500px"
        runat="server" Behavior="Close, Move, Maximize" ShowContentDuringLoad="False" VisibleStatusbar="False"
        Modal="true" OnClientClose="onClientClose" />
    <table width="100%">
        <tr>
            <td />
            <td class="label">No. MR
            </td>
            <td class="label">Nama Pasien
            </td>
            <td class="label">Jenis Kelamin
            </td>
            <td class="label">Tgl. Lahir
            </td>
            <td />
        </tr>
        <tr>
            <td />
            <td>
                <telerik:RadTextBox ID="txtNoMR" runat="server" Width="100px" ReadOnly="true" />
            </td>
            <td>
                <telerik:RadTextBox ID="txtNamaPasien" runat="server" Width="300px" ReadOnly="true" />
            </td>
            <td>
                <asp:RadioButtonList ID="rblJenisKelamin" runat="server" RepeatDirection="Horizontal"
                    Enabled="false" Width="200px">
                    <asp:ListItem Text="Laki-Laki" Value="M" />
                    <asp:ListItem Text="Perempuan" Value="F" />
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadDatePicker ID="txtTglLahir" runat="server" Width="100px" DateInput-ReadOnly="true"
                    DatePopupButton-Enabled="false" />
            </td>
            <td />
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td />
            <td class="label">Jaminan / Cara Bayar
            </td>
            <td class="label">No. Identitas Pasien
            </td>
            <td class="label">No. SEP / No. Pengajuan Klaim COVID-19
            </td>
            <td class="label">COB
            </td>
            <td />
        </tr>
        <tr>
            <td />
            <td>
                <telerik:RadComboBox ID="cboJaminan" runat="server" Width="304px" AutoPostBack="true" OnSelectedIndexChanged="cboJaminan_SelectedIndexChanged" />
            </td>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="cboNoPeserta" runat="server" Width="104px" AutoPostBack="true" OnSelectedIndexChanged="cboNoPeserta_SelectedIndexChanged">
                                <Items>
                                    <telerik:RadComboBoxItem Value="nik" Text="NIK" />
                                    <telerik:RadComboBoxItem Value="kitas" Text="KITAS / KITAP" />
                                    <telerik:RadComboBoxItem Value="paspor" Text="PASPORT" />
                                    <telerik:RadComboBoxItem Value="kartu_jkn" Text="KARTU JKN" Selected="true" />
                                    <telerik:RadComboBoxItem Value="kk" Text="Kartu Keluarga" />
                                    <telerik:RadComboBoxItem Value="unhcr" Text="UNHCR" />
                                    <telerik:RadComboBoxItem Value="kelurahan" Text="Kelurahan" />
                                    <telerik:RadComboBoxItem Value="dinsos" Text="Dinas Sosial" />
                                    <telerik:RadComboBoxItem Value="dinkes" Text="Dinas Kesehatan" />
                                    <telerik:RadComboBoxItem Value="sjp" Text=" Surat Jaminan Perawatan (SJP)" />
                                    <telerik:RadComboBoxItem Value="klaim_ibu" Text="Bayi Baru Lahir" />
                                    <telerik:RadComboBoxItem Value="lainnya" Text="LAINNYA" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <telerik:RadTextBox ID="txtNoPeserta" runat="server" Width="200px" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <telerik:RadTextBox ID="txtNoSep" runat="server" Width="300px" />
            </td>
            <td>
                <telerik:RadComboBox ID="cboCOB" runat="server" Width="304px" />
            </td>
            <td />
        </tr>
    </table>
    <hr />
    <table width="100%">
        <tr>
            <td />
            <td valign="top" align="center">
                <table>
                    <tr>
                        <td class="label">Jenis Rawat
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="rblJenisRawat" runat="server" RepeatDirection="Horizontal"
                                            Enabled="false">
                                            <asp:ListItem Text="Jalan" Value="OPR|2" />
                                            <asp:ListItem Text="Inap" Value="IPR|1" />
                                            <asp:ListItem Text="IGD" Value="EMR|3" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td id="tdNaikKelas" runat="server">
                                        <asp:CheckBox ID="chkNaikKelas" runat="server" Text="Naik Kelas"
                                            AutoPostBack="true" OnCheckedChanged="chkNaikKelas_CheckedChanged" />
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td id="tdRawatIntensif" runat="server">
                                        <asp:CheckBox ID="chkRawatIntensif" runat="server" Text="Ada Rawat Intensif" AutoPostBack="true"
                                            OnCheckedChanged="chkRawatIntensif_CheckedChanged" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Tanggal Rawat
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <u>Masuk</u>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtTglMasuk" runat="server" Width="100px" DateInput-ReadOnly="true"
                                            DatePopupButton-Enabled="false" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <u>Pulang</u>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtTglPulang" runat="server" Width="100px" AutoPostBack="true"
                                            OnSelectedDateChanged="txtTglPulang_SelectedDateChanged" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Cara Masuk
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="cboCaraMasuk" Width="304px">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" Selected="true" />
                                    <telerik:RadComboBoxItem Value="gp" Text="Rujukan FKTP" />
                                    <telerik:RadComboBoxItem Value="hosp-trans" Text="Rujukan FKRTL" />
                                    <telerik:RadComboBoxItem Value="mp" Text="Rujukan Spesialis" />
                                    <telerik:RadComboBoxItem Value="outp" Text="Dari Rawat Jalan" />
                                    <telerik:RadComboBoxItem Value="inp" Text="Dari Rawat Inap" />
                                    <telerik:RadComboBoxItem Value="emd" Text="Dari Rawat Darurat" />
                                    <telerik:RadComboBoxItem Value="born" Text="Lahir di RS" />
                                    <telerik:RadComboBoxItem Value="nursing" Text="Rujukan Panti Jompo" />
                                    <telerik:RadComboBoxItem Value="psych" Text="Rujukan dari RS Jiwa" />
                                    <telerik:RadComboBoxItem Value="rehab" Text="Rujukan Fasilitas Rehab" />
                                    <telerik:RadComboBoxItem Value="other" Text="Lain-lain" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr id="trVentilator2" runat="server">
                        <td class="label">
                            <asp:CheckBox runat="server" ID="chkVentilator" Text="Ventilator" AutoPostBack="true" OnCheckedChanged="chkVentilator_CheckedChanged" /></td>
                        <td>
                            <table>
                                <tr>
                                    <td class="label">Intubasi</td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtTglIntubasi" runat="server" Width="100px" Enabled="false" />
                                    </td>
                                    <td>
                                        <telerik:RadTimePicker ID="txtJamIntubasi" runat="server" Width="100px" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">Ekstubasi</td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtTglEkstubasi" runat="server" Width="100px" Enabled="false" />
                                    </td>
                                    <td>
                                        <telerik:RadTimePicker ID="txtJamEkstubasi" runat="server" Width="100px" Enabled="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr id="trEpisode" runat="server">
                        <td class="label">Episode Ruang Rawat
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="cboEpisode" Width="304px" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="cboEpisode_SelectedIndexChanged">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" Selected="true" />
                                    <telerik:RadComboBoxItem Value="1" Text="ICU dengan ventilator" />
                                    <telerik:RadComboBoxItem Value="2" Text="ICU tanpa ventilator" />
                                    <telerik:RadComboBoxItem Value="3" Text="Isolasi tekanan negatif dengan ventilator" />
                                    <telerik:RadComboBoxItem Value="4" Text="Isolasi tekanan negatif tanpa ventilator" />
                                    <telerik:RadComboBoxItem Value="5" Text="Isolasi non tekanan negatif dengan ventilator" />
                                    <telerik:RadComboBoxItem Value="6" Text="Isolasi non tekanan negatif tanpa ventilator" />
                                    <telerik:RadComboBoxItem Value="7" Text="ICU tekanan negatif dengan ventilator" />
                                    <telerik:RadComboBoxItem Value="8" Text="ICU tekanan negatif tanpa ventilator" />
                                    <telerik:RadComboBoxItem Value="9" Text="ICU tanpa tekanan negatif dengan ventilator" />
                                    <telerik:RadComboBoxItem Value="10" Text="ICU tanpa tekanan negatif tanpa ventilator" />
                                    <telerik:RadComboBoxItem Value="11" Text="Isolasi tekanan negatif" />
                                    <telerik:RadComboBoxItem Value="12" Text="Isolasi tanpa tekanan negatif" />
                                </Items>
                            </telerik:RadComboBox>
                            <hr />
                            <telerik:RadGrid ID="grdEpisodeRuangRawat" runat="server" Width="304px"
                                AutoGenerateColumns="False" GridLines="None" AllowSorting="false" ShowHeader="false"
                                OnDeleteCommand="grdEpisodeRuangRawat_DeleteCommand">
                                <MasterTableView CommandItemDisplay="None" DataKeyNames="Key">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ID" UniqueName="ID" Visible="false" />
                                        <telerik:GridBoundColumn DataField="Nama" UniqueName="Nama" />
                                        <telerik:GridTemplateColumn ItemStyle-Wrap="true" HeaderStyle-Width="50px">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox runat="server" ID="txtJumlah" MinValue="1" Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Jumlah"))%>'
                                                    Width="99%" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                            ButtonType="ImageButton">
                                            <HeaderStyle Width="35px" />
                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true">
                                    <Resizing AllowColumnResize="True" />
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Status COVID-19
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="cboStatusCov" Width="104px" Enabled="false">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" Selected="true" />
                                    <telerik:RadComboBoxItem Value="1" Text="ODP" />
                                    <telerik:RadComboBoxItem Value="2" Text="PDP" />
                                    <telerik:RadComboBoxItem Value="3" Text="Terkonfirmasi" />
                                    <telerik:RadComboBoxItem Value="4" Text="Suspek" />
                                    <telerik:RadComboBoxItem Value="5" Text="Probabel" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Kriteria Akses NAAT
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rblKriteriaAksesNaat" runat="server" RepeatDirection="Horizontal"
                                Enabled="false">
                                <asp:ListItem Text="Kriteria A" Value="A" Selected="True" />
                                <asp:ListItem Text="Kriteria B" Value="B" />
                                <asp:ListItem Text="Kriteria C" Value="C" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr id="trLapangan" runat="server">
                        <td class="label">RS Darurat / Lapangan
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rblRsDaruratLapangan" runat="server" RepeatDirection="Horizontal"
                                Enabled="false">
                                <asp:ListItem Text="Ya" Value="1" />
                                <asp:ListItem Text="Tidak" Value="0" Selected="True" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr id="trNaikKelasRawat" runat="server">
                        <td class="label">Naik Kelas Rawat
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rblNaikKelasRawat" runat="server" RepeatDirection="Horizontal"
                                Enabled="false">
                                <asp:ListItem Text="Kelas 2" Value="ClassRL-003|kelas_2" />
                                <asp:ListItem Text="Kelas 1" Value="ClassRL-002|kelas_1" />
                                <asp:ListItem Text="Kelas VIP" Value="ClassRL-001|vip" />
                                <asp:ListItem Text="Kelas VVIP" Value="ClassRL-000|vvip" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr id="trNaikKelasRawat2" runat="server">
                        <td class="label">Penjamin Naik Kelas
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="cboPenjaminNaikKelas" Width="104px">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" Selected="true" />
                                    <telerik:RadComboBoxItem Value="peserta" Text="Peserta" />
                                    <telerik:RadComboBoxItem Value="pemberi_kerja" Text="Pemberi Kerja" />
                                    <telerik:RadComboBoxItem Value="asuransi_tambahan" Text="Asuransi Tambahan" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr id="trRawatIntensif" runat="server">
                        <td class="label">Rawat Intensif (hari)
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtRawatIntensif" runat="server" NumberFormat-DecimalDigits="0"
                                ShowSpinButtons="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">LOS (hari)
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtLOS" runat="server" NumberFormat-DecimalDigits="0"
                                ReadOnly="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">ADL Score (12 s/d 60)
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <u>Sub Acute</u>
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtSubAcute" runat="server" Width="96px" />
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <u>Chronic</u>
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtChronic" runat="server" Width="96px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">DPJP
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboDPJP" runat="server" Width="304px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:CheckBox runat="server" ID="chkPasienTb" Text="Pasien TB" AutoPostBack="true" OnCheckedChanged="chkPasienTb_CheckedChanged" /></td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtSitbNo" runat="server" Width="300px" Enabled="false" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnValidasiSitb" runat="server" Enabled="false" Text="Validasi" OnClientClick="validasiSitb();return false;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr id="trCoInsidenseCovid" runat="server">
                        <td class="label">
                            <asp:CheckBox runat="server" ID="chkCoinsidenseCovid" Text="Co-Insidense COVID-19" AutoPostBack="true" OnCheckedChanged="chkCoinsidenseCovid_CheckedChanged" /></td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtNomorKlaimCovid" runat="server" Width="300px" Enabled="false" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnValidasiCoinsidenseCovid" runat="server" Enabled="false" Text="Validasi" OnClick="btnValidasiCoinsidenseCovid_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px" />
                    </tr>
                </table>
            </td>
            <td valign="top" align="center">
                <table>
                    <tr>
                        <td class="label">Kelas Rawat
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rblKelasRawat" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblKelasRawat_SelectedIndexChanged" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Umur
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtUmur" runat="server" NumberFormat-DecimalDigits="0" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr id="trStatusKelainan" runat="server">
                        <td class="label">Status Kelainan
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="cboStatusKelainan" Width="104px" Enabled="false">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" Selected="true" />
                                    <telerik:RadComboBoxItem Value="1" Text="Tanpa kelainan" />
                                    <telerik:RadComboBoxItem Value="2" Text="Dengan kelainan" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr style="display: none">
                        <td class="label">Total Episode Rawat (hari)
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtTotalEpisode" runat="server" NumberFormat-DecimalDigits="0" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr id="trKomorbid" runat="server">
                        <td class="label">Komorbid / Komplikasi
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rblKomorbid" runat="server" RepeatDirection="Horizontal"
                                Enabled="false">
                                <asp:ListItem Text="Tidak Ada" Value="1" Selected="True" />
                                <asp:ListItem Text="Ada" Value="0" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                    </tr>

                    <tr id="trIsolasiMandiri" runat="server">
                        <td class="label">Isolasi Mandiri
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rblIsolasiMandiri" runat="server" RepeatDirection="Horizontal"
                                Enabled="false">
                                <asp:ListItem Text="Tidak" Value="1" Selected="True" />
                                <asp:ListItem Text="Ya" Value="0" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr id="trCoInsidens" runat="server">
                        <td class="label">Co-Insidens
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rblCoInsidens" runat="server" RepeatDirection="Horizontal"
                                Enabled="false">
                                <asp:ListItem Text="Ya" Value="1" />
                                <asp:ListItem Text="Tidak" Value="0" Selected="True" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                    </tr>

                    <tr>
                        <td class="label">Lama Naik Kelas (hari)
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtLamaNaikKelas" runat="server" NumberFormat-DecimalDigits="0"
                                ShowSpinButtons="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr id="trVentilator" runat="server">
                        <td class="label">Ventilator (jam)
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtVentilator" runat="server" NumberFormat-DecimalDigits="0"
                                ShowSpinButtons="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Berat Lahir (gram)
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtBeratLahir" runat="server" NumberFormat-DecimalDigits="0"
                                ShowSpinButtons="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Cara Pulang
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboCaraPulang" runat="server" Width="304px" AutoPostBack="true" OnSelectedIndexChanged="cboCaraPulang_SelectedIndexChanged" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Jenis Tarif
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboJenisTariff" runat="server" Width="304px" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Tarif Poli Eks.
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtTariffPoliEks" runat="server" NumberFormat-DecimalDigits="0"
                                ShowSpinButtons="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Terapi Kovalesen
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtTerapiKovalesen" runat="server" NumberFormat-DecimalDigits="0"
                                ShowSpinButtons="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                </table>
            </td>
            <td />
        </tr>
    </table>
    <hr />
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="33%"></td>
            <td width="34%" align="center">
                <table>
                    <tr>
                        <td class="label"><b>Tarif Rumah Sakit</b>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtTarifRS" runat="server" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                </table>
            </td>
            <td width="33%"></td>
        </tr>
        <tr>
            <td width="33%"></td>
            <td width="34%" align="center">
                <hr />
            </td>
            <td width="33%"></td>
        </tr>
        <tr>
            <td width="33%" align="right" valign="top">
                <table>
                    <tr>
                        <td class="label">Prosedur Non Bedah
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtProsedurNonBedah" runat="server" ShowSpinButtons="true"
                                OnTextChanged="txtProsedurNonBedah_TextChanged" AutoPostBack="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Tenaga Ahli
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtTenagaAhli" runat="server" ShowSpinButtons="true"
                                OnTextChanged="txtProsedurNonBedah_TextChanged" AutoPostBack="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Radiologi
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtRadiologi" runat="server" ShowSpinButtons="true"
                                OnTextChanged="txtProsedurNonBedah_TextChanged" AutoPostBack="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Rehabilitasi
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtRehabilitasi" runat="server" ShowSpinButtons="true"
                                OnTextChanged="txtProsedurNonBedah_TextChanged" AutoPostBack="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Obat
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtObat" runat="server" ShowSpinButtons="true" OnTextChanged="txtProsedurNonBedah_TextChanged"
                                AutoPostBack="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Sewa Alat
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtSewaAlat" runat="server" ShowSpinButtons="true"
                                OnTextChanged="txtProsedurNonBedah_TextChanged" AutoPostBack="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                </table>
            </td>
            <td width="34%" align="center" valign="top">
                <table>
                    <tr>
                        <td class="label">Prosedur Bedah
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtProsedurBedah" runat="server" ShowSpinButtons="true"
                                OnTextChanged="txtProsedurNonBedah_TextChanged" AutoPostBack="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Keperawatan
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtKeperawatan" runat="server" ShowSpinButtons="true"
                                OnTextChanged="txtProsedurNonBedah_TextChanged" AutoPostBack="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Laboratorium
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtLaboratorium" runat="server" ShowSpinButtons="true"
                                OnTextChanged="txtProsedurNonBedah_TextChanged" AutoPostBack="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Kamar / Akomodasi
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtKamarAkomodasi" runat="server" ShowSpinButtons="true"
                                OnTextChanged="txtProsedurNonBedah_TextChanged" AutoPostBack="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Obat Kronis
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtObatKronis" runat="server" ShowSpinButtons="true"
                                OnTextChanged="txtProsedurNonBedah_TextChanged" AutoPostBack="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Alkes
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtAlkes" runat="server" ShowSpinButtons="true" OnTextChanged="txtProsedurNonBedah_TextChanged"
                                AutoPostBack="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                </table>
            </td>
            <td width="33%" align="left" valign="top">
                <table>
                    <tr>
                        <td class="label">Konsultasi
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtKonsultasi" runat="server" ShowSpinButtons="true"
                                OnTextChanged="txtProsedurNonBedah_TextChanged" AutoPostBack="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Penunjang
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtPenunjang" runat="server" ShowSpinButtons="true"
                                OnTextChanged="txtProsedurNonBedah_TextChanged" AutoPostBack="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Pelayanan Darah
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtPelayananDarah" runat="server" ShowSpinButtons="true"
                                OnTextChanged="txtProsedurNonBedah_TextChanged" AutoPostBack="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Rawat Intensif
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtRawatIntensifTarif" runat="server" ShowSpinButtons="true"
                                OnTextChanged="txtProsedurNonBedah_TextChanged" AutoPostBack="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Obat Kemoterapi
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtObatKemoterapi" runat="server" ShowSpinButtons="true"
                                OnTextChanged="txtProsedurNonBedah_TextChanged" AutoPostBack="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">BMHP
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtBMHP" runat="server" ShowSpinButtons="true" OnTextChanged="txtProsedurNonBedah_TextChanged"
                                AutoPostBack="true" />
                        </td>
                        <td width="20px" />
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3">
                <i>Menyatakan benar bahwa data tarif yang tersebut di atas adalah benar sesuai dengan kondisi yang sesungguhnya.</i>
            </td>
        </tr>
    </table>
    <br />
    <cc:CollapsePanel2 ID="CollapsePanel1" runat="server" Title="COVID-19" IsCollapsed="true">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" colspan="3">
                    <b>Faktor pengurang, pilih checkbox pemeriksaan penunjang berikut yang tidak dilakukan:</b>
                </td>
            </tr>
            <tr>
                <td width="33%" align="right" valign="top">
                    <table>
                        <tr>
                            <td>
                                <asp:CheckBox runat="server" ID="chkAsamLaktat" Text="Asam Laktat" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox runat="server" ID="chkKultur" Text="Kultur MO (aerob) dengan resistansi" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox runat="server" ID="chkAPTT" Text="APTT" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox runat="server" ID="chkAnalisaGas" Text="Analisa Gas" /></td>
                        </tr>
                    </table>
                </td>
                <td width="34%" align="center" valign="top">
                    <table>
                        <tr>
                            <td>
                                <asp:CheckBox runat="server" ID="chkProcalcitonin" Text="Procalcitonin" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox runat="server" ID="chkDDimer" Text="D Dimer" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox runat="server" ID="chkWaktuPendarahan" Text="Waktu Pendarahan" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox runat="server" ID="chkAlbumin" Text="Albumin" /></td>
                        </tr>
                    </table>
                </td>
                <td width="33%" align="left" valign="top">
                    <table>
                        <tr>
                            <td>
                                <asp:CheckBox runat="server" ID="chkCRP" Text="CRP" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox runat="server" ID="chkPT" Text="PT" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox runat="server" ID="chkAntiHIV" Text="Anti HIV" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox runat="server" ID="chkThoraxAPPA" Text="Thorax AP / PA" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="10%"></td>
                <td align="center">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <b>Khusus pasien COVID-19 yang meninggal dunia</b>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="10%"></td>
            </tr>
            <tr>
                <td width="10%"></td>
                <td align="center">
                    <table width="100%">
                        <tr>
                            <td width="15%" align="center">
                                <asp:CheckBox runat="server" ID="chkPemulasaraanJenazah" Text="Pemulasaraan Jenazah" /></td>
                            <td width="14%" align="center">
                                <asp:CheckBox runat="server" ID="chkKantongJenazah" Text="Kantong Jenazah" /></td>
                            <td width="14%" align="center">
                                <asp:CheckBox runat="server" ID="chkPetiJenazah" Text="Peti Jenazah" /></td>
                            <td width="14%" align="center">
                                <asp:CheckBox runat="server" ID="chkPlastikErat" Text="Plastik Erat" /></td>
                            <td width="14%" align="center">
                                <asp:CheckBox runat="server" ID="chkDesinfektanJenazah" Text="Desinfektan Jenazah" /></td>
                            <td width="14%" align="center">
                                <asp:CheckBox runat="server" ID="chkTransportMobil" Text="Transport Mobil" /></td>
                            <td width="15%" align="center">
                                <asp:CheckBox runat="server" ID="chkDesinfektanMobil" Text="Desinfektan Mobil" /></td>
                        </tr>
                    </table>
                </td>
                <td width="10%"></td>
            </tr>
        </table>
        <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="5%"></td>
                <td align="center">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td><b>Unggah Berkas Pendukung Klaim (untuk kasus COVID-19) - Dalam Format *.pdf</b>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="5%"></td>
            </tr>
            <tr>
                <td width="5%"></td>
                <td>
                    <table width="100%">
                        <tr>
                            <td class="label" width="20%">Resume Medis</td>
                            <td class="label" width="20%">Ruang Perawatan</td>
                            <td class="label" width="20%">Hasil Laboratorium</td>
                            <td class="label" width="20%">Hasil Radiologi</td>
                            <td class="label" width="20%">Hasil Penunjang Lainnya</td>
                        </tr>
                        <tr>
                            <td width="20%" valign="top">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:FileUpload ID="fuResumeMedis" runat="server" /></td>
                                        <td width="35px" align="center">
                                            <asp:ImageButton runat="server" ID="btnFuResumeMedis" ImageUrl="~/Images/Toolbar/refresh16.png" OnClick="btnFileUpload_Click" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <telerik:RadGrid ID="grdResumeMedis" runat="server" Width="304px"
                                                AutoGenerateColumns="False" GridLines="None" AllowSorting="false" ShowHeader="false"
                                                OnDeleteCommand="grdResumeMedis_DeleteCommand">
                                                <MasterTableView CommandItemDisplay="None" DataKeyNames="FileId">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="FileId" UniqueName="FileId" Visible="false" />
                                                        <telerik:GridBoundColumn DataField="FileName" UniqueName="FileName" />
                                                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                            ButtonType="ImageButton">
                                                            <HeaderStyle Width="35px" />
                                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="true">
                                                    <Resizing AllowColumnResize="True" />
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20%" valign="top">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:FileUpload ID="fuRuangPerawatan" runat="server" /></td>
                                        <td width="20px">
                                            <asp:ImageButton runat="server" ID="btnFuRuangPerawatan" ImageUrl="~/Images/Toolbar/refresh16.png" OnClick="btnFileUpload_Click" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <telerik:RadGrid ID="grdRuangPerawatan" runat="server" Width="304px"
                                                AutoGenerateColumns="False" GridLines="None" AllowSorting="false" ShowHeader="false"
                                                OnDeleteCommand="grdResumeMedis_DeleteCommand">
                                                <MasterTableView CommandItemDisplay="None" DataKeyNames="FileId">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="FileId" UniqueName="FileId" Visible="false" />
                                                        <telerik:GridBoundColumn DataField="FileName" UniqueName="FileName" />
                                                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                            ButtonType="ImageButton">
                                                            <HeaderStyle Width="35px" />
                                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="true">
                                                    <Resizing AllowColumnResize="True" />
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20%" valign="top">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:FileUpload ID="fuLaboratorium" runat="server" /></td>
                                        <td width="20px">
                                            <asp:ImageButton runat="server" ID="btnFuLaboratorium" ImageUrl="~/Images/Toolbar/refresh16.png" OnClick="btnFileUpload_Click" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <telerik:RadGrid ID="grdLaboratorium" runat="server" Width="304px"
                                                AutoGenerateColumns="False" GridLines="None" AllowSorting="false" ShowHeader="false"
                                                OnDeleteCommand="grdResumeMedis_DeleteCommand">
                                                <MasterTableView CommandItemDisplay="None" DataKeyNames="FileId">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="FileId" UniqueName="FileId" Visible="false" />
                                                        <telerik:GridBoundColumn DataField="FileName" UniqueName="FileName" />
                                                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                            ButtonType="ImageButton">
                                                            <HeaderStyle Width="35px" />
                                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="true">
                                                    <Resizing AllowColumnResize="True" />
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20%" valign="top">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:FileUpload ID="fuRadiologi" runat="server" /></td>
                                        <td width="20px">
                                            <asp:ImageButton runat="server" ID="btnFuRadiologi" ImageUrl="~/Images/Toolbar/refresh16.png" OnClick="btnFileUpload_Click" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <telerik:RadGrid ID="grdRadiologi" runat="server" Width="304px"
                                                AutoGenerateColumns="False" GridLines="None" AllowSorting="false" ShowHeader="false"
                                                OnDeleteCommand="grdResumeMedis_DeleteCommand">
                                                <MasterTableView CommandItemDisplay="None" DataKeyNames="FileId">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="FileId" UniqueName="FileId" Visible="false" />
                                                        <telerik:GridBoundColumn DataField="FileName" UniqueName="FileName" />
                                                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                            ButtonType="ImageButton">
                                                            <HeaderStyle Width="35px" />
                                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="true">
                                                    <Resizing AllowColumnResize="True" />
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20%" valign="top">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:FileUpload ID="fuPenunjang" runat="server" /></td>
                                        <td width="20px">
                                            <asp:ImageButton runat="server" ID="btnFuPenunjang" ImageUrl="~/Images/Toolbar/refresh16.png" OnClick="btnFileUpload_Click" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <telerik:RadGrid ID="grdPenunjang" runat="server" Width="304px"
                                                AutoGenerateColumns="False" GridLines="None" AllowSorting="false" ShowHeader="false"
                                                OnDeleteCommand="grdResumeMedis_DeleteCommand">
                                                <MasterTableView CommandItemDisplay="None" DataKeyNames="FileId">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="FileId" UniqueName="FileId" Visible="false" />
                                                        <telerik:GridBoundColumn DataField="FileName" UniqueName="FileName" />
                                                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                            ButtonType="ImageButton">
                                                            <HeaderStyle Width="35px" />
                                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="true">
                                                    <Resizing AllowColumnResize="True" />
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="5%"></td>
            </tr>
            <tr>
                <td width="5%"></td>
                <td>
                    <table width="100%">
                        <tr>
                            <td class="label" width="20%">Resep Obat / Alkes</td>
                            <td class="label" width="20%">Tagihan (Billing)</td>
                            <td class="label" width="20%">Kartu Identitas</td>
                            <td class="label" width="20%">Dokumen KIPI</td>
                            <td class="label" width="20%">Lain-lain</td>
                        </tr>
                        <tr>
                            <td width="20%" valign="top">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:FileUpload ID="fuObat" runat="server" /></td>
                                        <td width="20px">
                                            <asp:ImageButton runat="server" ID="btnFuObat" ImageUrl="~/Images/Toolbar/refresh16.png" OnClick="btnFileUpload_Click" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <telerik:RadGrid ID="grdObat" runat="server" Width="304px"
                                                AutoGenerateColumns="False" GridLines="None" AllowSorting="false" ShowHeader="false"
                                                OnDeleteCommand="grdResumeMedis_DeleteCommand">
                                                <MasterTableView CommandItemDisplay="None" DataKeyNames="FileId">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="FileId" UniqueName="FileId" Visible="false" />
                                                        <telerik:GridBoundColumn DataField="FileName" UniqueName="FileName" />
                                                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                            ButtonType="ImageButton">
                                                            <HeaderStyle Width="35px" />
                                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="true">
                                                    <Resizing AllowColumnResize="True" />
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20%" valign="top">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:FileUpload ID="fuBilling" runat="server" />
                                        </td>
                                        <td width="20px">
                                            <asp:ImageButton runat="server" ID="btnFuBilling" ImageUrl="~/Images/Toolbar/refresh16.png" OnClick="btnFileUpload_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <telerik:RadGrid ID="grdBilling" runat="server" Width="304px"
                                                AutoGenerateColumns="False" GridLines="None" AllowSorting="false" ShowHeader="false"
                                                OnDeleteCommand="grdResumeMedis_DeleteCommand">
                                                <MasterTableView CommandItemDisplay="None" DataKeyNames="FileId">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="FileId" UniqueName="FileId" Visible="false" />
                                                        <telerik:GridBoundColumn DataField="FileName" UniqueName="FileName" />
                                                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                            ButtonType="ImageButton">
                                                            <HeaderStyle Width="35px" />
                                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="true">
                                                    <Resizing AllowColumnResize="True" />
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20%" valign="top">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:FileUpload ID="fuKartu" runat="server" />
                                        </td>
                                        <td width="20px">
                                            <asp:ImageButton runat="server" ID="btnFuKartu" ImageUrl="~/Images/Toolbar/refresh16.png" OnClick="btnFileUpload_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <telerik:RadGrid ID="grdKartu" runat="server" Width="304px"
                                                AutoGenerateColumns="False" GridLines="None" AllowSorting="false" ShowHeader="false"
                                                OnDeleteCommand="grdResumeMedis_DeleteCommand">
                                                <MasterTableView CommandItemDisplay="None" DataKeyNames="FileId">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="FileId" UniqueName="FileId" Visible="false" />
                                                        <telerik:GridBoundColumn DataField="FileName" UniqueName="FileName" />
                                                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                            ButtonType="ImageButton">
                                                            <HeaderStyle Width="35px" />
                                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="true">
                                                    <Resizing AllowColumnResize="True" />
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20%" valign="top">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:FileUpload ID="fuKipi" runat="server" />
                                        </td>
                                        <td width="20px">
                                            <asp:ImageButton runat="server" ID="btnFuKipi" ImageUrl="~/Images/Toolbar/refresh16.png" OnClick="btnFileUpload_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <telerik:RadGrid ID="grdKipi" runat="server" Width="304px"
                                                AutoGenerateColumns="False" GridLines="None" AllowSorting="false" ShowHeader="false"
                                                OnDeleteCommand="grdResumeMedis_DeleteCommand">
                                                <MasterTableView CommandItemDisplay="None" DataKeyNames="FileId">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="FileId" UniqueName="FileId" Visible="false" />
                                                        <telerik:GridBoundColumn DataField="FileName" UniqueName="FileName" />
                                                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                            ButtonType="ImageButton">
                                                            <HeaderStyle Width="35px" />
                                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="true">
                                                    <Resizing AllowColumnResize="True" />
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20%" valign="top">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:FileUpload ID="fuLainlain" runat="server" />
                                        </td>
                                        <td width="20px">
                                            <asp:ImageButton runat="server" ID="btnFuLainlain" ImageUrl="~/Images/Toolbar/refresh16.png" OnClick="btnFileUpload_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <telerik:RadGrid ID="grdLainlain" runat="server" Width="304px"
                                                AutoGenerateColumns="False" GridLines="None" AllowSorting="false" ShowHeader="false"
                                                OnDeleteCommand="grdResumeMedis_DeleteCommand">
                                                <MasterTableView CommandItemDisplay="None" DataKeyNames="FileId">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="FileId" UniqueName="FileId" Visible="false" />
                                                        <telerik:GridBoundColumn DataField="FileName" UniqueName="FileName" />
                                                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                            ButtonType="ImageButton">
                                                            <HeaderStyle Width="35px" />
                                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="true">
                                                    <Resizing AllowColumnResize="True" />
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="5%"></td>
            </tr>
        </table>
    </cc:CollapsePanel2>
    <br />
    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpagDetail">
        <Tabs>
            <telerik:RadTab runat="server" Text="Coding E-Klaim v5" Selected="True" PageViewID="pgOtherGuarantor">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Coding E-Klaim v6" PageViewID="pgPatientInformation">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpagDetail" runat="server" SelectedIndex="0" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgOtherGuarantor" runat="server">
            <table width="100%">
                <tr>
                    <td />
                    <td width="50%" valign="top" align="center">
                        <table>
                            <tr>
                                <td class="label">Diagnosa (ICD-10)
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboDiagnosa" runat="server" Width="304px" AllowCustomText="true" EnableLoadOnDemand="true" OnClientItemsRequesting="OnClientItemsRequestingHandlerDiagnosa" OnClientKeyPressing="DiagnosaKeyPressing">
                                        <WebServiceSettings Path="../../../../WebService/Sep.asmx" Method="GetDiagnosaEklaim" />
                                    </telerik:RadComboBox>
                                </td>
                                <td width="25px">&nbsp;
                                    <asp:ImageButton ID="btnFilterDiagnosa" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilterDiagnosa_Click" ToolTip="Search" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label" />
                                <td colspan="2">
                                    <asp:Button ID="btnInsertDiagnosa" runat="server" Text="Insert" OnClick="btnInsertDiagnosa_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="50%" valign="top">
                        <telerik:RadGrid ID="grdDiagnosa" runat="server" OnNeedDataSource="grdDiagnosa_NeedDataSource"
                            AutoGenerateColumns="False" GridLines="None" AllowSorting="false" ShowHeader="false"
                            OnItemCommand="grdDiagnosa_ItemCommand" OnDeleteCommand="grdDiagnosa_DeleteCommand">
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="RegistrationNo, SequenceNo">
                                <Columns>
                                    <telerik:GridButtonColumn UniqueName="PrimerColumn" Text="Set Primer" CommandName="Primer"
                                        ButtonType="ImageButton" ConfirmText="Set Primer this row?" ImageUrl="../../../../Images/Toolbar/post16.png">
                                        <HeaderStyle Width="35px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridButtonColumn>
                                    <telerik:GridTemplateColumn ItemStyle-Wrap="true">
                                        <ItemTemplate>
                                            <%# string.IsNullOrWhiteSpace(DataBinder.Eval(Container.DataItem, "DiagnoseName").ToString()) ? DataBinder.Eval(Container.DataItem, "DiagnosisText") : DataBinder.Eval(Container.DataItem, "DiagnoseName")  %>&nbsp;<b>[<%# DataBinder.Eval(Container.DataItem, "DiagnoseID")%>]</b>&nbsp;<i><%# DataBinder.Eval(Container.DataItem, "DiagnoseType")%></i>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="DiagnoseType" UniqueName="DiagnoseType" Visible="false" />
                                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                        ButtonType="ImageButton">
                                        <HeaderStyle Width="35px" />
                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="true">
                                <Resizing AllowColumnResize="True" />
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                    <td />
                </tr>
            </table>
            <hr />
            <table width="100%">
                <tr>
                    <td />
                    <td width="50%" valign="top" align="center">
                        <table>
                            <tr>
                                <td class="label">Prosedur (ICD-9-CM)
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboProsedur" runat="server" Width="304px" AllowCustomText="true" EnableLoadOnDemand="true" OnClientItemsRequesting="OnClientItemsRequestingHandlerDiagnosa" OnClientKeyPressing="ProsedurKeyPressing">
                                        <WebServiceSettings Path="../../../../WebService/Sep.asmx" Method="GetProcedureEklaim" />
                                    </telerik:RadComboBox>
                                </td>
                                <td width="25px">&nbsp;
                                    <asp:ImageButton ID="btnFilterProsedur" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilterProsedur_Click" ToolTip="Search" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label" />
                                <td colspan="2">
                                    <asp:Button ID="btnInsertProsedur" runat="server" Text="Insert" OnClick="btnInsertProsedur_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="50%" valign="top">
                        <telerik:RadGrid ID="grdProsedur" runat="server" OnNeedDataSource="grdProsedur_NeedDataSource"
                            AutoGenerateColumns="False" GridLines="None" AllowSorting="false" ShowHeader="false"
                            OnItemCommand="grdProsedur_ItemCommand" OnDeleteCommand="grdProsedur_DeleteCommand">
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="RegistrationNo, SequenceNo">
                                <Columns>
                                    <telerik:GridTemplateColumn ItemStyle-Wrap="true">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "ProcedureName")%>&nbsp;<b>[<%# DataBinder.Eval(Container.DataItem, "ProcedureID")%>]</b>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                        ButtonType="ImageButton">
                                        <HeaderStyle Width="35px" />
                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="true">
                                <Resizing AllowColumnResize="True" />
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                    <td />
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgPatientInformation" runat="server">
            <table width="100%">
                <tr>
                    <td />
                    <td width="50%" valign="top" align="center">
                        <table>
                            <tr>
                                <td class="label">Diagnosa (ICD-10)
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboDiagnosa2" runat="server" Width="304px" AllowCustomText="true" EnableLoadOnDemand="true" OnClientItemsRequesting="OnClientItemsRequestingHandlerDiagnosa">
                                        <WebServiceSettings Path="../../../../WebService/Sep.asmx" Method="GetDiagnosaInaGroupperEklaim" />
                                    </telerik:RadComboBox>
                                </td>
                                <td width="25px">&nbsp;
                                    <asp:ImageButton ID="btnFilterDiagnosa2" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilterDiagnosa2_Click" ToolTip="Search" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label" />
                                <td colspan="2">
                                    <asp:Button ID="btnImportDiagnosa" runat="server" Text="Import" OnClick="btnImportDiagnosa_Click" />
                                    <asp:Button ID="btnInsertDiagnosa2" runat="server" Text="Insert" OnClick="btnInsertDiagnosa2_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="50%" valign="top">
                        <telerik:RadGrid ID="grdDiagnosa2" runat="server" OnNeedDataSource="grdDiagnosa2_NeedDataSource"
                            AutoGenerateColumns="False" GridLines="None" AllowSorting="false" ShowHeader="false"
                            OnItemCommand="grdDiagnosa2_ItemCommand" OnDeleteCommand="grdDiagnosa2_DeleteCommand">
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="RegistrationNo, SequenceNo">
                                <Columns>
                                    <telerik:GridButtonColumn UniqueName="PrimerColumn" Text="Set Primer" CommandName="Primer"
                                        ButtonType="ImageButton" ConfirmText="Set Primer this row?" ImageUrl="../../../../Images/Toolbar/post16.png">
                                        <HeaderStyle Width="35px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridButtonColumn>
                                    <telerik:GridTemplateColumn ItemStyle-Wrap="true">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "DiagnoseName")%>&nbsp;<b>[<%# DataBinder.Eval(Container.DataItem, "DiagnoseID")%>]</b>&nbsp;<i><%# DataBinder.Eval(Container.DataItem, "DiagnoseType")%></i>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="DiagnoseType" UniqueName="DiagnoseType" Visible="false" />
                                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                        ButtonType="ImageButton">
                                        <HeaderStyle Width="35px" />
                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="true">
                                <Resizing AllowColumnResize="True" />
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                    <td />
                </tr>
            </table>
            <hr />
            <table width="100%">
                <tr>
                    <td />
                    <td width="50%" valign="top" align="center">
                        <table>
                            <tr>
                                <td class="label">Prosedur (ICD-9-CM)
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboProsedur2" runat="server" Width="304px" AllowCustomText="true" EnableLoadOnDemand="true" OnClientItemsRequesting="OnClientItemsRequestingHandlerDiagnosa">
                                        <WebServiceSettings Path="../../../../WebService/Sep.asmx" Method="GetProcedureInaGroupperEklaim" />
                                    </telerik:RadComboBox>
                                </td>
                                <td width="25px">&nbsp;
                                    <asp:ImageButton ID="btnFilterProsedur2" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilterProsedur2_Click" ToolTip="Search" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label" />
                                <td colspan="2">
                                    <asp:Button ID="btnImportProcedure" runat="server" Text="Import" OnClick="btnImportProcedure_Click" />
                                    <asp:Button ID="btnInsertProsedur2" runat="server" Text="Insert" OnClick="btnInsertProsedur2_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="50%" valign="top">
                        <telerik:RadGrid ID="grdProsedur2" runat="server" OnNeedDataSource="grdProsedur2_NeedDataSource"
                            AutoGenerateColumns="False" GridLines="None" AllowSorting="false" ShowHeader="false"
                            OnItemCommand="grdProsedur2_ItemCommand" OnDeleteCommand="grdProsedur2_DeleteCommand">
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="RegistrationNo, SequenceNo">
                                <Columns>
                                    <telerik:GridTemplateColumn ItemStyle-Wrap="true">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "ProcedureName")%>&nbsp;<b>[<%# DataBinder.Eval(Container.DataItem, "ProcedureID")%>]</b>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                        ButtonType="ImageButton">
                                        <HeaderStyle Width="35px" />
                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="true">
                                <Resizing AllowColumnResize="True" />
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                    <td />
                </tr>
            </table>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <br />
    <table width="100%">
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td colspan="4" style="text-align: center;">Tekanan Darah (mmHg):</td>
                    </tr>
                    <tr>
                        <td class="label">Sistole</td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtSistole" runat="server" Width="100px" Value="0" />
                        </td>
                        <td class="label">Diastole</td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtDiastole" runat="server" Width="100px" Value="0" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <cc:CollapsePanel2 ID="CollapsePanel2" runat="server" Title="APGAR Score" IsCollapsed="true">
        <table width="100%">
            <tr>
                <td align="center">
                    <table>
                        <tr>
                            <td></td>
                            <td class="label">Appearance</td>
                            <td class="label">Pulse</td>
                            <td class="label">Grimace</td>
                            <td class="label">Activity</td>
                            <td class="label">Respiration</td>
                        </tr>
                        <tr>
                            <td class="label">1 Menit</td>
                            <td>
                                <telerik:RadNumericTextBox ID="txt1Appearance" runat="server" Width="100px" Value="0" NumberFormat-DecimalDigits="0" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txt1Pulse" runat="server" Width="100px" Value="0" NumberFormat-DecimalDigits="0" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txt1Grimace" runat="server" Width="100px" Value="0" NumberFormat-DecimalDigits="0" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txt1Activity" runat="server" Width="100px" Value="0" NumberFormat-DecimalDigits="0" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txt1Respiration" runat="server" Width="100px" Value="0" NumberFormat-DecimalDigits="0" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">5 Menit</td>
                            <td>
                                <telerik:RadNumericTextBox ID="txt5Appearance" runat="server" Width="100px" Value="0" NumberFormat-DecimalDigits="0" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txt5Pulse" runat="server" Width="100px" Value="0" NumberFormat-DecimalDigits="0" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txt5Grimace" runat="server" Width="100px" Value="0" NumberFormat-DecimalDigits="0" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txt5Activity" runat="server" Width="100px" Value="0" NumberFormat-DecimalDigits="0" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txt5Respiration" runat="server" Width="100px" Value="0" NumberFormat-DecimalDigits="0" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel2>
    <br />
    <cc:CollapsePanel2 ID="CollapsePanel3" runat="server" Title="Persalinan" IsCollapsed="true">
        <table width="100%">
            <tr>
                <td width="50%" align="center" valign="top">
                    <table>
                        <tr>
                            <td class="label">Usia Kehamilan</td>
                            <td class="label">Gravida</td>
                            <td class="label">Partus</td>
                            <td class="label">Abortus</td>
                            <td class="label">Onset Kontraksi</td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadNumericTextBox ID="txtUsiaKehamilan" runat="server" Width="100px" Value="0" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtGravida" runat="server" Width="100px" Value="0" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtPartus" runat="server" Width="100px" Value="0" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtAbortus" runat="server" Width="100px" Value="0" />
                            </td>
                            <td>
                                <telerik:RadComboBox runat="server" ID="cboOnsetKontraksi" Width="304px">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="" Text="" Selected="true" />
                                        <telerik:RadComboBoxItem Value="spontan" Text="Spontan" />
                                        <telerik:RadComboBoxItem Value="induksi" Text="Induksi" />
                                        <telerik:RadComboBoxItem Value="non_spontan_non_induksi" Text="Non Spontan Non Induksi" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" align="center">
                    <telerik:RadGrid ID="grdDelivery" runat="server" OnNeedDataSource="grdDelivery_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdDelivery_DeleteCommand" OnUpdateCommand="grdDelivery_UpdateCommand"
                        OnInsertCommand="grdDelivery_InsertCommand">
                        <MasterTableView DataKeyNames="DeliverySequence" CommandItemDisplay="Top">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="35px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn DataField="DeliverySequence" HeaderText="No" UniqueName="DeliverySequence"
                                    SortExpression="DeliverySequence">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DeliveryMethod" HeaderText="Method" UniqueName="DeliveryMethod"
                                    SortExpression="DeliveryMethod">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DeliveryDttm" HeaderText="Waktu" UniqueName="DeliveryDttm"
                                    SortExpression="DeliveryDttm">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="LetakJanin" HeaderText="Letak Janin" UniqueName="LetakJanin"
                                    SortExpression="LetakJanin">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Kondisi" HeaderText="Kondisi" UniqueName="Kondisi"
                                    SortExpression="Kondisi">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="UseManual" HeaderText="Manual" Groupable="false"
                                    SortExpression="UseManual">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "UseManual") == "1" ? "Ya" : "Tidak"%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="UseForcep" HeaderText="Forcep" Groupable="false"
                                    SortExpression="UseForcep">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "UseForcep") == "1" ? "Ya" : "Tidak"%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="UseVacuum" HeaderText="Vacuum" Groupable="false"
                                    SortExpression="UseVacuum">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "UseVacuum") == "1" ? "Ya" : "Tidak"%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="Persalinan.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="DeliveryEditCommand">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="True">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel2>
    <br />
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <hr />
            </td>
            <td width="225px" align="left">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"
                    Width="100px" />
                &nbsp;
                <asp:Button ID="btnGroupper" runat="server" Text="Grouper" OnClick="btnGroupper_Click"
                    Width="100px" />
            </td>
            <td width="100px" style="display: none">
                <asp:Button ID="btnPayment" runat="server" Text="Payment" OnClientClick="openWinPayment('', '');return false;"
                    Width="100px" />
            </td>
            <td width="100px">
                <asp:Button ID="btnFinal" runat="server" Text="Final" OnClientClick="openWinPayment('', '');return false;"
                    Width="100px" Visible="false" />
                <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="100px" Visible="false"
                    OnClick="btnEdit_Click" />
            </td>
            <td width="4px"></td>
            <td width="100px">
                <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="openPrint();return false;" Width="100px" />
            </td>
            <td width="4px"></td>
            <td width="100px">
                <asp:Button ID="btnKirim" runat="server" Text="Kirim Online" OnClick="btnKirim_Click"
                    Width="100px" Enabled="false" />
            </td>
            <td>
                <hr />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="10%"></td>
            <td width="80%" align="center">
                <table>
                    <tr style="display: none">
                        <td class="label">Info
                        </td>
                        <td colspan="3">
                            <telerik:RadTextBox ID="txtInfo" runat="server" Width="718px" ReadOnly="true" />
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">Jenis Rawat
                        </td>
                        <td colspan="3">
                            <telerik:RadTextBox ID="txtJenisRawat" runat="server" Width="718px" ReadOnly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Group
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtGroupName" runat="server" Width="500px" ReadOnly="true" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtGroupID" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtGroupPrice" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Sub Acute
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtSubAcuteName" runat="server" Width="500px" ReadOnly="true" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtSubAcuteID" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtSubAcutePrice" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Chronic
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtChronicName" runat="server" Width="500px" ReadOnly="true" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtChronicID" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtChronicPrice" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Special Procedure
                        </td>
                        <td colspan="2">
                            <telerik:RadComboBox ID="cboSpecialProcedure" runat="server" Width="504px" OnSelectedIndexChanged="cboSpecialDrug_SelectedIndexChanged" AutoPostBack="true" />
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtSpecialProcedurePrice" runat="server" Width="100px"
                                ReadOnly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Special Prosthesis
                        </td>
                        <td colspan="2">
                            <telerik:RadComboBox ID="cboSpecialProsthesis" runat="server" Width="504px" OnSelectedIndexChanged="cboSpecialDrug_SelectedIndexChanged" AutoPostBack="true" />
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtSpecialProsthesisPrice" runat="server" Width="100px"
                                ReadOnly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Special Investigation
                        </td>
                        <td colspan="2">
                            <telerik:RadComboBox ID="cboSpecialInvestigation" runat="server" Width="504px" OnSelectedIndexChanged="cboSpecialDrug_SelectedIndexChanged" AutoPostBack="true" />
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtSpecialInvestigationPrice" runat="server" Width="100px"
                                ReadOnly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Special Drug
                        </td>
                        <td colspan="2">
                            <telerik:RadComboBox ID="cboSpecialDrug" runat="server" Width="504px" OnSelectedIndexChanged="cboSpecialDrug_SelectedIndexChanged" AutoPostBack="true" />
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtSpecialDrugPrice" runat="server" Width="100px"
                                ReadOnly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Penggunaan Dializer
                        </td>
                        <td colspan="2">
                            <telerik:RadComboBox ID="cboDializer" runat="server" Width="504px" AutoPostBack="true" OnSelectedIndexChanged="cboDializer_SelectedIndexChanged">
                                <Items>
                                    <telerik:RadComboBoxItem Text="" Value="" />
                                    <telerik:RadComboBoxItem Text="Single Use" Value="1" />
                                    <telerik:RadComboBoxItem Text="Multiple Use (reuse)" Value="0" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtDializer" runat="server" Width="100px"
                                ReadOnly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Transfusi Darah
                        </td>
                        <td colspan="3">Jumlah kantong darah:&nbsp;<telerik:RadNumericTextBox ID="txtKantongDarah" runat="server" Width="100px" Value="0" />&nbsp;kantong
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="right">Total :
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtGrouperTotal" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Tambahan Biaya
                        </td>
                        <td />
                        <td>
                            <telerik:RadNumericTextBox ID="txtSelisihPersen" runat="server" Width="100px" MinValue="0"
                                MaxValue="100" Type="Percent" />
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtTambahanBiaya" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="10%"></td>
        </tr>
    </table>
</asp:Content>
