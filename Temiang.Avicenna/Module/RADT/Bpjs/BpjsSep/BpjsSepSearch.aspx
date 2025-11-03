<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="BpjsSepSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.BpjsSepSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblSEPNo" runat="server" Text="No SEP" Width="100px"></asp:Label>
            </td>
            <td class="filter" />
            <td class="entry">
                <telerik:RadTextBox ID="txtNoSep" runat="server" Width="300px" />
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td class="label">No Kartu
            </td>
            <td class="filter" />
            <td class="entry">
                <telerik:RadTextBox ID="txtNoKartu" runat="server" Width="300px" />
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td class="label">No NIK
            </td>
            <td class="filter" />
            <td class="entry">
                <telerik:RadTextBox ID="txtNoNik" runat="server" Width="300px" />
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td class="label">Tanggal SEP
            </td>
            <td class="filter" />
            <td class="entry">
                <telerik:RadDatePicker ID="txtTanggalSep" runat="server" Width="100px" />
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td class="label">Nama Pasien
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterNamaPasien" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" Selected="true" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNamaPasien" runat="server" Width="300px" />
            </td>
            <td width="20px" />
            <td />
        </tr>
    </table>
</asp:Content>
