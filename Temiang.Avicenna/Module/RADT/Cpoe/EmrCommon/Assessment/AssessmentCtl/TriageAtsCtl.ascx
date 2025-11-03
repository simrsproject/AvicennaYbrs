<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TriageAtsCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.TriageAtsCtl" %>

<table width="100%">
        <tr>
        <td class="label"></td>
        <td>Kategori 1</td>
        <td>Kategori 2</td>
        <td>Kategori 3</td>
        <td>Kategori 4</td>
        <td>Kategori 5</td>
    </tr>
    <tr>
        <td class="label">Waktu Respon</td>
        <td>Segera membutuhkan penilaian dan perawatan</td>
        <td>Penilaian dan perawatan dilakukan dalam 10 menit</td>
        <td>Penilaian dan perawatan dilakukan dalam 30 menit</td>
        <td>Penilaian dan perawatan dilakukan dalam 60 menit</td>
        <td>Penilaian dan perawatan dilakukan dalam 120 menit</td>
    </tr>
        <tr>
        <td class="label">Keterangan Klinis</td>
        <td> <telerik:RadCheckBoxList runat="server" ID="CheckBoxList1" AutoPostBack="false" >
            <Databindings DataTextField="ItemName" DataValueField="ItemID" />
        </telerik:RadCheckBoxList></td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
</table>