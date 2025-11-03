<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="RujukBalikSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.RujukBalikSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">No SRB
            </td>
            <td class="filter" />
            <td class="entry">
                <telerik:RadTextBox ID="txtNoSRB" runat="server" Width="300px" />
            </td>
            <td width="20px" />
            <td />
        </tr>
                <tr>
            <td class="label">No SEP
            </td>
            <td class="filter" />
            <td class="entry">
                <telerik:RadTextBox ID="txtNoSep" runat="server" Width="300px" />
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td class="label">Tanggal SRB
            </td>
            <td class="filter" />
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadDatePicker ID="txtTglAwal" runat="server" Width="100px" />
                        </td>
                        <td>&nbsp;-&nbsp;</td>
                        <td>
                            <telerik:RadDatePicker ID="txtTglAkhir" runat="server" Width="100px" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20px" />
            <td />
        </tr>
    </table>
</asp:Content>
