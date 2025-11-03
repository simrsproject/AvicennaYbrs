<%@ Page Title="BPJS SEP" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="BpjsSepDataDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.BpjsSepDataDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilter">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtPatientDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy> 
    <table width="100%">
        <tr>
            <td class="label">
                Nomor Kartu / No KTP
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNomorKartu" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:ImageButton ID="btnFilter" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                    OnClick="btnFilter_Click" ToolTip="Cari" />
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                Detail Keanggotaan
            </td>
            <td class="entry">
                <telerik:RadTextBox runat="server" ID="txtPatientDetail" Width="100%" ReadOnly="true"
                    Height="250px" TextMode="MultiLine" />
            </td>
            <td width="20px" />
            <td />
        </tr>
    </table>
</asp:Content>
