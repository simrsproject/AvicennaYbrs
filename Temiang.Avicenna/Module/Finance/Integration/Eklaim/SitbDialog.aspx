<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="SitbDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Integration.Eklaim.SitbDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td class="label">Status</td>
            <td class="entry">
                <telerik:RadTextBox runat="server" ID="txtStatus" Width="300px" ReadOnly="true" />
            </td>
        </tr>
        <tr>
            <td class="label">Keterangan</td>
            <td class="entry">
                <telerik:RadTextBox runat="server" ID="txtKeterangan" Width="300px" ReadOnly="true" />
            </td>
        </tr>
        <tr>
            <td class="label">Nama</td>
            <td class="entry">
                <telerik:RadTextBox runat="server" ID="txtNama" Width="300px" ReadOnly="true" />
            </td>
        </tr>
        <tr>
            <td class="label">NIK</td>
            <td class="entry">
                <telerik:RadTextBox runat="server" ID="txtNik" Width="300px" ReadOnly="true" />
            </td>
        </tr>
        <tr>
            <td class="label">Jenis Kelamin</td>
            <td class="entry">
                <telerik:RadTextBox runat="server" ID="txtJenisKelamin" Width="300px" ReadOnly="true" />
            </td>
        </tr>
    </table>
</asp:Content>
