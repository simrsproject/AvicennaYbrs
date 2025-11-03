<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="BpjsSepDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.BpjsSepDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="../../../../JavaScript/jquery.js"></script>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function openWinCreatePasien() {
                var bpjsNo = $find("<%= txtNomorKartu.ClientID %>");
                if (bpjsNo.get_value() == '') return;

                var jp = $find("<%= cboJenisPelayanan.ClientID %>");
                if (jp.get_value() == '') return;

                var rt = '';
                if (jp.get_value() == '1') rt = 'IPR'
                else rt = 'OPR'

                var oWnd = $find("<%= winRujukan.ClientID %>");
                oWnd.setUrl('../../Registration/PatientDetail.aspx?md=new&pid=&pt=directpatient&rt=' + rt + '&bpjsNo=' + bpjsNo.get_value() + '&type=bpjs');
                oWnd.set_width($(window).width());
                oWnd.show();
            }

            function openWinRujukan() {
                var bpjsNo = $find("<%= txtNoRujukan.ClientID %>");
                if (bpjsNo.get_value() == '') return;
                var oWnd = $find("<%= winRujukan.ClientID %>");
                oWnd.setUrl('BpjsRujukanDialog.aspx?bpjsNo=' + bpjsNo.get_value());
                oWnd.show();
            }

            function openWinSearchPasien() {
                var oWnd = $find("<%= winRujukan.ClientID %>");
                oWnd.setUrl('BpjsSearchPatientDialog.aspx');
                oWnd.set_width($(window).width());
                oWnd.show();
            }

            function openWinCheckout() {
                var noKartu = $find("<%= txtNomorKartu.ClientID %>");
                if (noKartu.get_value() == '') {
                    alert('No kartu peserta tidak boleh kosong.');
                    return;
                }
                var firstDataItem = $find("<%=grdList.ClientID %>").get_masterTableView().get_dataItems()[0];
                if (firstDataItem == null) {
                    alert('Tidak ada riwayat kunjungan terakhir.');
                    return;
                }
                var noSep = firstDataItem.getDataKeyValue("noSEP");
                if (noSep == '' || noSep == null) {
                    alert('Tidak ada riwayat kunjungan terakhir.');
                    return;
                }

                var oWnd = $find("<%= winRujukan.ClientID %>");
                oWnd.setUrl('../BpjsCheckout/BpjsCheckoutDialog.aspx?src=sep&noSep=' + noSep);
                oWnd.set_width(800);
                oWnd.set_height(200);
                oWnd.show();
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.mode != null) __doPostBack("<%= grdList.UniqueID %>", oWnd.argument.mode);
            }

            function openWindowSepToReg() {
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                if (regNo.get_value() != '') {
                    alert('Pasien telah melakukan registrasi ke poli tujuan.');
                    return;
                }
                var bpjsNo = $find("<%= txtNoSEP.ClientID %>");
                if (bpjsNo.get_value() == '') return;

                var pid = $find("<%= txtNoPatientID.ClientID %>");
                if (pid.get_value() == '') return;

                var jp = $find("<%= cboJenisPelayanan.ClientID %>");
                if (jp.get_value() == '') return;

                var rt = '';
                if (jp.get_value() == '1') rt = 'IPR'
                else rt = 'OPR'

                var oWnd = window.$find("<%= winRegistrasi.ClientID %>");
                oWnd.setUrl("../../Registration/RegistrationDetail.aspx?md=new&id=" + pid.get_value() + "&sep=" + bpjsNo.get_value() + "&rt=" + rt + '&type=bpjs');
                oWnd.set_width($(window).width());
                oWnd.show();
            }

        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Behavior="Close, Move" ShowContentDuringLoad="False"
        Width="750px" Height="500px" VisibleStatusbar="False" Modal="true" ID="winRujukan"
        OnClientClose="onClientClose" />
    <telerik:RadWindow runat="server" Animation="None" Behavior="Close, Move" ShowContentDuringLoad="False"
        Width="750px" Height="500px" VisibleStatusbar="False" Modal="true" ID="winRegistrasi" />
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            No. Kartu / No KTP
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtNomorKartu" runat="server" Width="300px" />
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnFilter" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilter_Click" ToolTip="Cari Peserta" />
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnCheckOut" runat="server" ImageUrl="~/Images/Toolbar/views16.png"
                                            OnClientClick="openWinCheckout(); return false;" ToolTip="Checkout Manual" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td colspan="4">
                            <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="MultiPage1" ShowBaseLine="true"
                                Orientation="HorizontalTop">
                                <Tabs>
                                    <telerik:RadTab runat="server" Text="Detail" PageViewID="pgDetail" Selected="True" />
                                    <telerik:RadTab runat="server" Text="Riwayat" PageViewID="pgRiwayat" />
                                </Tabs>
                            </telerik:RadTabStrip>
                            <telerik:RadMultiPage ID="MultiPage1" runat="server" BorderStyle="Solid" BorderColor="Gray">
                                <telerik:RadPageView ID="pgDetail" runat="server" Selected="true">
                                    <telerik:RadTextBox runat="server" ID="txtPatientDetail" Width="99%" ReadOnly="true"
                                        Height="300px" TextMode="MultiLine" />
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="pgRiwayat" runat="server">
                                    <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="False" ShowGroupPanel="false"
                                        AllowPaging="True" PageSize="5" AllowSorting="False" GridLines="None" OnNeedDataSource="grdList_NeedDataSource">
                                        <MasterTableView DataKeyNames="noSEP" ClientDataKeyNames="noSEP">
                                            <Columns>
                                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="NoSEP" HeaderText="No SEP"
                                                    UniqueName="noSEP" SortExpression="noSEP" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <telerik:GridDateTimeColumn HeaderStyle-Width="70px" DataField="tglSEP" HeaderText="Tgl SEP"
                                                    HeaderStyle-HorizontalAlign="Center" UniqueName="tglSEP" SortExpression="tglSEP"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <telerik:GridDateTimeColumn HeaderStyle-Width="70px" DataField="tglPulang" HeaderText="Tgl Pulang"
                                                    HeaderStyle-HorizontalAlign="Center" UniqueName="tglPulang" SortExpression="TanggalLahir"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <telerik:GridBoundColumn DataField="jnsPelayanan" HeaderText="Jenis Pelayanan" UniqueName="jnsPelayanan"
                                                    SortExpression="jnsPelayanan" />
                                                <telerik:GridBoundColumn DataField="poliTujuan" HeaderText="Poli Tujuan" UniqueName="poliTujuan"
                                                    SortExpression="poliTujuan" />
                                                <telerik:GridBoundColumn DataField="diagnosa" HeaderText="Diagnosa" UniqueName="diagnosa"
                                                    SortExpression="diagnosa" />
                                                <telerik:GridNumericColumn DataField="biayaTagihan" HeaderText="Biaya Tagihan" HeaderStyle-HorizontalAlign="Center"
                                                    UniqueName="biayaTagihan" SortExpression="biayaTagihan" ItemStyle-HorizontalAlign="Right" />
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPageView>
                            </telerik:RadMultiPage>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            No SEP
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNoSEP" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Tanggal SEP
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTanggalSEP" runat="server" Width="100px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Tanggal SEP required."
                                ValidationGroup="entry" ControlToValidate="txtTanggalSEP" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            No Rujukan
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtNoRujukan" runat="server" Width="300px" />
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnFilterRujukan" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClientClick="openWinRujukan(); return false;" ToolTip="Cari Rujukan" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Tanggal Rujukan
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTanggalRujukan" runat="server" Width="100px" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Asal Rujukan
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtPPKRujukan" runat="server" Width="100px" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNamaPPKRujukan" runat="server" Text="" CssClass="labeldescription"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Jenis Pelayanan
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboJenisPelayanan" runat="server" Width="300px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboJenisPelayanan_SelectedIndexChanged" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Jenis Pelayanan required."
                                ValidationGroup="entry" ControlToValidate="cboJenisPelayanan" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Catatan
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtCatatan" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Diagnosa Awal
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboDiagnosaAwal" runat="server" Width="300px" MarkFirstMatch="false"
                                EnableLoadOnDemand="true" OnItemDataBound="cboDiagnosaAwal_ItemDataBound" OnItemsRequested="cboDiagnosaAwal_ItemsRequested" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Poli Tujuan
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPoliTujuan" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Kelas Rawat
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboKelasRawat" runat="server" Width="300px" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox runat="server" ID="chkIsLakaLantas" Text="Laka Lantas" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Lokasi Laka
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtLokasiLaka" Width="300px" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            No Medical Record
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboMR" runat="server" Width="300px" AllowCustomText="true"
                                            EnableLoadOnDemand="true" OnItemDataBound="cboMR_ItemDataBound" OnItemsRequested="cboMR_ItemsRequested"
                                            DataValueField="MedicalNo" DataTextField="PatientName" AutoPostBack="true" OnSelectedIndexChanged="cboMR_SelectedIndexChanged">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "MedicalNo")%>&nbsp;-&nbsp;<%# DataBinder.Eval(Container.DataItem, "PatientName")%>
                                            </ItemTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClientClick="openWinSearchPasien(); return false;" ToolTip="Cari Pasien" />
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Toolbar/new16.png"
                                            OnClientClick="openWinCreatePasien(); return false;" ToolTip="Create Pasien" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20" />
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label" />
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNoPatientID" runat="server" Width="100px" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr style="display: none">
                        <td class="label" />
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="100px" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
