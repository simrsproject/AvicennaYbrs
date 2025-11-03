<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="BpjsReportList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.BpjsReportList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" valign="top">
                    <table>
                        <tr>
                            <td class="label">
                                Tanggal SEP
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker runat="server" ID="txtTglSEP" Width="100px" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterTanggalSEP" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                No Kartu
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox runat="server" ID="txtNoKartu" Width="300px" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                    </table>
                </td>
                <td width="50%" valign="top">
                    <table>
                        <tr>
                            <td class="label">
                                No Medical Record
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox runat="server" ID="txtNoMR" Width="300px" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnFilterNoMR" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                Poli Tujuan
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboPoliTujuan" Width="304px" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnFilterPoliTujuan" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AutoGenerateColumns="False" ShowGroupPanel="false" AllowPaging="True" PageSize="15"
        AllowSorting="True" GridLines="None">
        <MasterTableView DataKeyNames="NoSEP">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="NoSEP" HeaderText="No SEP"
                    UniqueName="NoSEP" SortExpression="NoSEP" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TanggalSEP" HeaderText="Tgl SEP"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="TanggalSEP" SortExpression="TanggalSEP"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="NoRujukan" HeaderText="No Rujukan"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="NoRujukan" SortExpression="NoRujukan"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TanggalRujukan" HeaderText="Tgl Rujukan"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="TanggalRujukan" SortExpression="TanggalRujukan"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="NomorKartu" HeaderText="No Kartu"
                    UniqueName="NomorKartu" SortExpression="NomorKartu" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn DataField="NamaPasienJK" HeaderText="Nama Pasien (JK)"
                    UniqueName="NamaPasienJK" SortExpression="NamaPasienJK" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TanggalLahir" HeaderText="Tgl Lahir"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="TanggalLahir" SortExpression="TanggalLahir"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="TypeOfService" HeaderText="Jenis Pelayanan" UniqueName="TypeOfService"
                    SortExpression="TypeOfService" HeaderStyle-Width="100px" />
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Poli Tujuan" UniqueName="ServiceUnitName"
                    SortExpression="ServiceUnitName" />
                <telerik:GridBoundColumn DataField="DiagnoseName" HeaderText="Diagnosa Awal" UniqueName="DiagnoseName"
                    SortExpression="DiagnoseName" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsLakaLantas" HeaderText="Laka Lantas"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="IsLakaLantas" SortExpression="IsLakaLantas"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsRegistration" HeaderText="Registrasi"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="IsRegistration" SortExpression="IsLakaLantas"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
