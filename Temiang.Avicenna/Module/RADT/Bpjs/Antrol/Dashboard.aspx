<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.Antrol.Dashboard" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilter">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnUpdateTaskId">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboTaskId" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtTanggal" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtJam" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdList3" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilter2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilter3">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList3" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="cboTaskId" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtTanggal" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtJam" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList3">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList3" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterOutstanding">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboPoliOustanding" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="cboDokterOutstanding" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdOustanding" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdOustanding">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOustanding" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAntreanPerTanggal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAntreanPerTanggal" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdAntreanPerTanggal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAntreanPerTanggal" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="MultiPage1" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Per Nomor Booking" PageViewID="pgNomor" Selected="True" />
            <telerik:RadTab runat="server" Text="Per Tanggal" PageViewID="pgDetail" />
            <telerik:RadTab runat="server" Text="Per Bulan" PageViewID="pgRiwayat" />
            <telerik:RadTab runat="server" Text="Belum Dilayani" PageViewID="pgOustanding" />
            <telerik:RadTab runat="server" Text="Antrean Per Tanggal" PageViewID="pgAntreanPerTanggal" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="MultiPage1" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgNomor" runat="server" Selected="true">
            <cc:CollapsePanel ID="CollapsePanel3" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" style="vertical-align: top">
                            <table width="100%">
                                <tr>
                                    <td class="label">Nomor Booking
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox runat="server" ID="txtNomorBooking" Width="300px" />
                                    </td>
                                    <td style="text-align: left"></td>
                                </tr>
                                <tr>
                                    <td class="label" />
                                    <td colspan="2">
                                        <asp:Button runat="server" ID="btnFilter3" Text="Filter" OnClick="btnFilter3_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" style="vertical-align: top">
                            <table width="100%">
                                <tr>
                                    <td class="label">Task ID
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboTaskId" Width="304px">
                                            <Items>
                                                <telerik:RadComboBoxItem Value="" Text="" />
                                                <telerik:RadComboBoxItem Value="1" Text="1. mulai waktu tunggu admisi" />
                                                <telerik:RadComboBoxItem Value="2" Text="2. akhir waktu tunggu admisi/mulai waktu layan admisi" />
                                                <telerik:RadComboBoxItem Value="3" Text="3. akhir waktu layan admisi/mulai waktu tunggu poli" />
                                                <telerik:RadComboBoxItem Value="4" Text="4. akhir waktu tunggu poli/mulai waktu layan poli" />
                                                <telerik:RadComboBoxItem Value="5" Text="5. akhir waktu layan poli/mulai waktu tunggu farmasi" />
                                                <telerik:RadComboBoxItem Value="6" Text="6. akhir waktu tunggu farmasi/mulai waktu layan farmasi membuat obat" />
                                                <telerik:RadComboBoxItem Value="7" Text="7. akhir waktu obat selesai dibuat" />
                                                <telerik:RadComboBoxItem Value="99" Text="99. tidak hadir/batal" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td style="text-align: left"></td>
                                </tr>
                                <tr>
                                    <td class="label">Waktu
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadDatePicker runat="server" ID="txtTanggal" Width="100px" />
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <telerik:RadTimePicker runat="server" ID="txtJam" Width="100px">
                                                        <DateInput runat="server" DateFormat="HH:mm" DisplayDateFormat="HH:mm"></DateInput>
                                                        <TimeView runat="server" TimeFormat="HH:mm"></TimeView>
                                                    </telerik:RadTimePicker>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="text-align: left"></td>
                                </tr>
                                <tr>
                                    <td class="label" />
                                    <td colspan="2">
                                        <asp:Button runat="server" ID="btnUpdateTaskId" Text="Update" OnClick="btnUpdateTaskId_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </cc:CollapsePanel>
            <telerik:RadGrid ID="grdList3" runat="server" AutoGenerateColumns="False" ShowGroupPanel="false" AllowPaging="True" PageSize="15"
                AllowSorting="True" GridLines="None">
                <MasterTableView DataKeyNames="Kodebooking, Taskid">
                    <Columns>
                        <telerik:GridBoundColumn DataField="Kodebooking" HeaderText="Kodebooking"
                            UniqueName="Kodebooking" SortExpression="Kodebooking" />
                        <telerik:GridBoundColumn DataField="Taskid" HeaderText="Taskid"
                            UniqueName="Taskid" SortExpression="Taskid" />
                        <telerik:GridBoundColumn DataField="Taskname" HeaderText="Taskname"
                            UniqueName="Taskname" SortExpression="Taskname" />
                        <telerik:GridBoundColumn DataField="Waktu" HeaderText="Waktu"
                            UniqueName="Waktu" SortExpression="Waktu" />
                        <telerik:GridBoundColumn DataField="Wakturs" HeaderText="Wakturs"
                            UniqueName="Wakturs" SortExpression="Wakturs" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgDetail" runat="server">
            <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" style="vertical-align: top">
                            <table width="100%">
                                <tr>
                                    <td class="label">Tanggal
                                    </td>
                                    <td class="entry">
                                        <telerik:RadDatePicker runat="server" ID="txtStart" Width="100px" />
                                    </td>
                                    <td style="text-align: left"></td>
                                </tr>
                                <tr>
                                    <td class="label" />
                                    <td colspan="2">
                                        <asp:Button runat="server" ID="btnFilter" Text="Filter" OnClick="btnFilter_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" style="vertical-align: top"></td>
                    </tr>
                </table>
            </cc:CollapsePanel>
            <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="False" ShowGroupPanel="false"
                AllowSorting="True" GridLines="None" ShowFooter="true">
                <MasterTableView DataKeyNames="tanggal">
                    <Columns>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="Tanggal" HeaderText="Tanggal"
                            HeaderStyle-HorizontalAlign="Center" UniqueName="tanggal" SortExpression="tanggal"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn DataField="Namapoli" HeaderText="Nama Poli"
                            UniqueName="namapoli" SortExpression="namapoli" />

                        <telerik:GridBoundColumn DataField="JumlahAntrean" HeaderText="Jml. Antrean"
                            UniqueName="jumlah_antrean" SortExpression="jumlah_antrean" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center"
                            Aggregate="Sum" DataFormatString="{0:n2}" />

                        <telerik:GridBoundColumn DataField="JumlahPasien" HeaderText="Jml. Pasien"
                            UniqueName="JumlahPasien" SortExpression="JumlahPasien" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center"
                            Aggregate="Sum" DataFormatString="{0:n2}" />

                        <telerik:GridBoundColumn DataField="Persentase" HeaderText="Persentase"
                            UniqueName="Persentase" SortExpression="Persentase" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center"
                            Aggregate="Avg" DataFormatString="{0:n2}" />

                        <telerik:GridTemplateColumn HeaderText="Tunggu Admisi" UniqueName="waktu_task1"
                            SortExpression="waktu_task1">
                            <ItemTemplate>
                                Waktu : <%# DataBinder.Eval(Container.DataItem, "WaktuTask1")%>
                                <br />
                                Rata-rata : <%# DataBinder.Eval(Container.DataItem, "AvgWaktuTask1")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Layan Admisi" UniqueName="waktu_task2"
                            SortExpression="waktu_task2">
                            <ItemTemplate>
                                Waktu : <%# DataBinder.Eval(Container.DataItem, "WaktuTask2")%>
                                <br />
                                Rata-rata : <%# DataBinder.Eval(Container.DataItem, "AvgWaktuTask2")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Tunggu Poli" UniqueName="waktu_task3"
                            SortExpression="waktu_task3">
                            <ItemTemplate>
                                Waktu : <%# DataBinder.Eval(Container.DataItem, "WaktuTask3")%>
                                <br />
                                Rata-rata : <%# DataBinder.Eval(Container.DataItem, "AvgWaktuTask3")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Layan Poli" UniqueName="waktu_task4"
                            SortExpression="waktu_task4">
                            <ItemTemplate>
                                Waktu : <%# DataBinder.Eval(Container.DataItem, "WaktuTask4")%>
                                <br />
                                Rata-rata : <%# DataBinder.Eval(Container.DataItem, "AvgWaktuTask4")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Tunggu Farmasi" UniqueName="waktu_task5"
                            SortExpression="waktu_task5">
                            <ItemTemplate>
                                Waktu : <%# DataBinder.Eval(Container.DataItem, "WaktuTask5")%>
                                <br />
                                Rata-rata : <%# DataBinder.Eval(Container.DataItem, "AvgWaktuTask5")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Layan Farmasi" UniqueName="waktu_task6"
                            SortExpression="waktu_task6">
                            <ItemTemplate>
                                Waktu : <%# DataBinder.Eval(Container.DataItem, "WaktuTask6")%>
                                <br />
                                Rata-rata : <%# DataBinder.Eval(Container.DataItem, "AvgWaktuTask6")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Insertdate" HeaderText="Insert Date"
                            UniqueName="Insertdate" SortExpression="Insertdate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgRiwayat" runat="server">
            <cc:CollapsePanel ID="CollapsePanel2" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" style="vertical-align: top">
                            <table width="100%">
                                <tr>
                                    <td class="label">Bulan
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboMonthly" Width="300px" AllowCustomText="true"
                                            Filter="Contains">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td style="text-align: left"></td>
                                </tr>
                                <tr>
                                    <td class="label" />
                                    <td colspan="2">
                                        <asp:Button runat="server" ID="btnFilter2" Text="Filter" OnClick="btnFilter2_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" style="vertical-align: top"></td>
                    </tr>
                </table>
            </cc:CollapsePanel>
            <telerik:RadGrid ID="grdList2" runat="server" AutoGenerateColumns="False" ShowGroupPanel="false"
                AllowSorting="True" GridLines="None" ShowFooter="true">
                <MasterTableView DataKeyNames="tanggal">
                    <Columns>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="Tanggal" HeaderText="Tanggal"
                            HeaderStyle-HorizontalAlign="Center" UniqueName="tanggal" SortExpression="tanggal"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn DataField="Namapoli" HeaderText="Nama Poli"
                            UniqueName="namapoli" SortExpression="namapoli" />
                        <telerik:GridBoundColumn DataField="JumlahAntrean" HeaderText="Jml. Antrean"
                            UniqueName="jumlah_antrean" SortExpression="jumlah_antrean" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />

                        <telerik:GridBoundColumn DataField="JumlahPasien" HeaderText="Jml. Pasien"
                            UniqueName="JumlahPasien" SortExpression="JumlahPasien" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center"
                            Aggregate="Sum" DataFormatString="{0:n2}" />

                        <telerik:GridBoundColumn DataField="Persentase" HeaderText="Persentase"
                            UniqueName="Persentase" SortExpression="Persentase" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center"
                            Aggregate="Avg" DataFormatString="{0:n2}" />

                        <telerik:GridTemplateColumn HeaderText="Tunggu Admisi" UniqueName="waktu_task1"
                            SortExpression="waktu_task1">
                            <ItemTemplate>
                                Waktu : <%# DataBinder.Eval(Container.DataItem, "WaktuTask1")%>
                                <br />
                                Rata-rata : <%# DataBinder.Eval(Container.DataItem, "AvgWaktuTask1")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Layan Admisi" UniqueName="waktu_task2"
                            SortExpression="waktu_task2">
                            <ItemTemplate>
                                Waktu : <%# DataBinder.Eval(Container.DataItem, "WaktuTask2")%>
                                <br />
                                Rata-rata : <%# DataBinder.Eval(Container.DataItem, "AvgWaktuTask2")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Tunggu Poli" UniqueName="waktu_task3"
                            SortExpression="waktu_task3">
                            <ItemTemplate>
                                Waktu : <%# DataBinder.Eval(Container.DataItem, "WaktuTask3")%>
                                <br />
                                Rata-rata : <%# DataBinder.Eval(Container.DataItem, "AvgWaktuTask3")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Layan Poli" UniqueName="waktu_task4"
                            SortExpression="waktu_task4">
                            <ItemTemplate>
                                Waktu : <%# DataBinder.Eval(Container.DataItem, "WaktuTask4")%>
                                <br />
                                Rata-rata : <%# DataBinder.Eval(Container.DataItem, "AvgWaktuTask4")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Tunggu Farmasi" UniqueName="waktu_task5"
                            SortExpression="waktu_task5">
                            <ItemTemplate>
                                Waktu : <%# DataBinder.Eval(Container.DataItem, "WaktuTask5")%>
                                <br />
                                Rata-rata : <%# DataBinder.Eval(Container.DataItem, "AvgWaktuTask5")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Layan Farmasi" UniqueName="waktu_task6"
                            SortExpression="waktu_task6">
                            <ItemTemplate>
                                Waktu : <%# DataBinder.Eval(Container.DataItem, "WaktuTask6")%>
                                <br />
                                Rata-rata : <%# DataBinder.Eval(Container.DataItem, "AvgWaktuTask6")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Insertdate" HeaderText="Insert Date"
                            UniqueName="Insertdate" SortExpression="Insertdate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgOustanding" runat="server">
            <cc:CollapsePanel ID="CollapsePanel4" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" style="vertical-align: top">
                            <table width="100%">
                                <tr>
                                    <td class="label">Tanggal
                                    </td>
                                    <td class="entry">
                                        <telerik:RadDatePicker runat="server" ID="txtTglOutstanding" Width="100px" />
                                    </td>
                                    <td style="text-align: left"></td>
                                </tr>
                                <tr>
                                    <td class="label">Poli
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboPoliOustanding" AllowCustomText="true" Filter="Contains" Width="304px" />
                                    </td>
                                    <td style="text-align: left"></td>
                                </tr>
                                <tr>
                                    <td class="label">Dokter
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboDokterOutstanding" AllowCustomText="true" Filter="Contains" Width="304px" />
                                    </td>
                                    <td style="text-align: left"></td>
                                </tr>
                                <tr>
                                    <td class="label" />
                                    <td colspan="2">
                                        <asp:Button runat="server" ID="btnFilterOutstanding" Text="Filter" OnClick="btnFilterOutstanding_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" style="vertical-align: top"></td>
                    </tr>
                </table>
            </cc:CollapsePanel>
            <telerik:RadGrid ID="grdOustanding" runat="server" AutoGenerateColumns="False" ShowGroupPanel="false"
                AllowSorting="True" GridLines="None" OnDeleteCommand="grdOustanding_DeleteCommand" OnDetailTableDataBind="grdOustanding_DetailTableDataBind">
                <MasterTableView DataKeyNames="Kodebooking">
                    <Columns>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="Tanggal" HeaderText="Tanggal"
                            HeaderStyle-HorizontalAlign="Center" UniqueName="tanggal" SortExpression="tanggal"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="Jampraktek" HeaderText="Jam Praktek"
                            UniqueName="jampraktek" SortExpression="jampraktek" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="Namapoli" HeaderText="Nama Poli"
                            UniqueName="namapoli" SortExpression="namapoli" />
                        <telerik:GridBoundColumn DataField="Namadokter" HeaderText="Nama Dokter"
                            UniqueName="namadokter" SortExpression="namadokter" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="Kodebooking" HeaderText="Kode Booking"
                            UniqueName="kodebooking" SortExpression="kodebooking" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="Nokapst" HeaderText="No Peserta"
                            UniqueName="nokapst" SortExpression="nokapst" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="Nomorreferensi" HeaderText="Nomor Referensi"
                            UniqueName="nomorreferensi" SortExpression="nomorreferensi" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="Norekammedis" HeaderText="No RM"
                            HeaderStyle-HorizontalAlign="Center" UniqueName="norekammedis" SortExpression="norekammedis"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="Namapasien" HeaderText="Nama Pasien"
                            UniqueName="namapasien" SortExpression="namapasien" />
                        <telerik:GridBoundColumn DataField="Sumberdata" HeaderText="Sumber Data"
                            UniqueName="sumberdata" SortExpression="sumberdata" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Batal" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Batalkan antrian?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="detail" DataKeyNames="Taskid" AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridBoundColumn DataField="Kodebooking" HeaderText="Kodebooking"
                                    UniqueName="Kodebooking" SortExpression="Kodebooking" />
                                <telerik:GridBoundColumn DataField="Taskid" HeaderText="Taskid"
                                    UniqueName="Taskid" SortExpression="Taskid" />
                                <telerik:GridBoundColumn DataField="Taskname" HeaderText="Taskname"
                                    UniqueName="Taskname" SortExpression="Taskname" />
                                <telerik:GridBoundColumn DataField="Waktu" HeaderText="Waktu"
                                    UniqueName="Waktu" SortExpression="Waktu" />
                                <telerik:GridBoundColumn DataField="Wakturs" HeaderText="Wakturs"
                                    UniqueName="Wakturs" SortExpression="Wakturs" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgAntreanPerTanggal" runat="server">
            <cc:CollapsePanel ID="CollapsePanel5" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" style="vertical-align: top">
                            <table width="100%">
                                <tr>
                                    <td class="label">Tanggal
                                    </td>
                                    <td class="entry">
                                        <telerik:RadDatePicker runat="server" ID="txtAntreanPerTanggal" Width="100px" />
                                    </td>
                                    <td style="text-align: left"></td>
                                </tr>
                                <tr>
                                    <td class="label">Poli
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboPoliAntreanPerTanggal" AllowCustomText="true" Filter="Contains" Width="304px" />
                                    </td>
                                    <td style="text-align: left"></td>
                                </tr>
                                <tr>
                                    <td class="label">Dokter
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboDokterAntreanPerTanggal" AllowCustomText="true" Filter="Contains" Width="304px" />
                                    </td>
                                    <td style="text-align: left"></td>
                                </tr>
                                <tr>
                                    <td class="label" />
                                    <td colspan="2">
                                        <asp:Button runat="server" ID="btnAntreanPerTanggal" Text="Filter" OnClick="btnAntreanPerTanggal_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" style="vertical-align: top"></td>
                    </tr>
                </table>
            </cc:CollapsePanel>
            <telerik:RadGrid ID="grdAntreanPerTanggal" runat="server" AutoGenerateColumns="true" ShowGroupPanel="false"
                AllowSorting="True" GridLines="None" ShowFooter="false">
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
