<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="RencanaKontrolList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.RencanaKontrolList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:radgrid id="grdList" runat="server" onneeddatasource="grdList_NeedDataSource"
        autogeneratecolumns="False" showgrouppanel="false" allowpaging="True" pagesize="15"
        allowsorting="True" gridlines="None" ondeletecommand="grdList_DeleteCommand">
        <mastertableview datakeynames="NoSuratKontrol, NoKartu, Nama" clientdatakeynames="NoSuratKontrol, NoKartu, Nama">
            <columns>
                <telerik:griddatetimecolumn headerstyle-width="150px" datafield="NoSuratKontrol" headertext="No SRK"
                    headerstyle-horizontalalign="Center" uniquename="NoSuratKontrol" sortexpression="NoSuratKontrol"
                    itemstyle-horizontalalign="Center" />
                <telerik:griddatetimecolumn datafield="JenisSuratKontrol" headertext="Jenis SRK"
                    headerstyle-horizontalalign="Center" uniquename="JenisSuratKontrol" sortexpression="JenisSuratKontrol"
                    itemstyle-horizontalalign="Center" />
                <telerik:griddatetimecolumn headerstyle-width="80px" datafield="TglRencanaKontrol" headertext="Tgl Rencana"
                    headerstyle-horizontalalign="Center" uniquename="TglRencanaKontrol" sortexpression="TglRencanaKontrol"
                    itemstyle-horizontalalign="Center" />
                <telerik:griddatetimecolumn headerstyle-width="80px" datafield="TglTerbitKontrol" headertext="Tgl Terbit"
                    headerstyle-horizontalalign="Center" uniquename="TglTerbit" sortexpression="TglTerbit"
                    itemstyle-horizontalalign="Center" />
                <telerik:gridboundcolumn datafield="NamaPoliTujuan" headertext="Poli Tujuan" uniquename="NamaPoliTujuan"
                    sortexpression="NamaPoliTujuan" />
                <telerik:gridboundcolumn headerstyle-width="120px" datafield="NoSepAsalKontrol" headertext="No SEP"
                    headerstyle-horizontalalign="Center" uniquename="NoSep" sortexpression="NoSep"
                    itemstyle-horizontalalign="Center" />
                <telerik:griddatetimecolumn headerstyle-width="80px" datafield="TglSEP" headertext="Tgl SEP"
                    headerstyle-horizontalalign="Center" uniquename="TglSep" sortexpression="TglSep"
                    itemstyle-horizontalalign="Center" />
                <telerik:gridboundcolumn headerstyle-width="110px" datafield="NoKartu" headertext="No Kartu"
                    uniquename="NoKartu" sortexpression="NoKartu" headerstyle-horizontalalign="Center"
                    itemstyle-horizontalalign="Center" />
                <telerik:gridboundcolumn datafield="Nama" headertext="Nama Pasien" uniquename="Nama"
                    sortexpression="Nama" />
                <telerik:gridboundcolumn datafield="NamaDokter" headertext="Nama DPJP" uniquename="NamaDokter"
                    sortexpression="NamaDokter" />
                <telerik:gridbuttoncolumn uniquename="DeleteColumn" text="Delete" commandname="Delete"
                    buttontype="ImageButton" confirmtext="Delete this row?">
                    <headerstyle width="30px" />
                    <itemstyle horizontalalign="Center" cssclass="MyImageButton" />
                </telerik:gridbuttoncolumn>
            </columns>
        </mastertableview>
    </telerik:radgrid>
</asp:Content>
