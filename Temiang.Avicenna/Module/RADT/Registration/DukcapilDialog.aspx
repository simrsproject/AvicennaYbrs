<%@ Page Title="Data Dukcapil" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="DukcapilDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.DukcapilDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="../../../../../JavaScript/jquery.js"></script>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function OnClientItemsRequestingHandler(sender, eventArgs) {
                if (sender.get_text().length < 3) eventArgs.set_cancel(true);
                else {
                    var context = eventArgs.get_context();
                    context["filter"] = eventArgs.get_text();
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">Nama Lengkap
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="lblNamaLengkap" Width="300px" ReadOnly="true" />
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">NIK
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="lblNIK" Width="300px" ReadOnly="true" /></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">NO KK
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="lblNoKK" Width="300px" ReadOnly="true" /></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Jenis Kelamin
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="lblJenisKelamin" Width="300px" ReadOnly="true" /></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Tempat & Tanggal Lahir
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="lblTempatLahir" Width="300px" ReadOnly="true" /></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Alamat
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="lblAlamat" TextMode="MultiLine" Width="300px" ReadOnly="true" /></td>
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">Nama Ayah
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="lblNamaAyah" Width="300px" ReadOnly="true" /></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Nama Ibu
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="lblNamaIbu" Width="300px" ReadOnly="true" /></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Agama
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="lblAgama" Width="300px" ReadOnly="true" /></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Status Kawin
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="lblStatusKawin" Width="300px" ReadOnly="true" /></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Pendidikan Akhir
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="lblPendidikanAkhir" Width="300px" ReadOnly="true" /></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Pekerjaan
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="lblPekerjaan" Width="300px" ReadOnly="true" /></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">No. Rekam Medis
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboNoRekamMedis" Width="300px" AllowCustomText="true"
                                EnableLoadOnDemand="true" OnClientItemsRequesting="OnClientItemsRequestingHandler"
                                OnItemDataBound="cboNoMRSep_ItemDataBound">
                                <WebServiceSettings Path="../../../WebService/Sep.asmx" Method="GetPasien" />
                            </telerik:RadComboBox>
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
