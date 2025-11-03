<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="Deceased.aspx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.EmrIp.EmrIpCommon.Deceased" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkDeceased" Text="Deceased" /></td>
            <td width="20px"></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Deceased Date</td>
            <td class="entry">
                <telerik:RadDatePicker runat="server" ID="txtDeceasedDate" Width="100px" /></td>
            <td width="20px"></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Deceased Time</td>
            <td class="entry">
                <telerik:RadTimePicker runat="server" ID="txtDeceasedTime" Width="100px" /></td>
            <td width="20px"></td>
            <td></td>
        </tr>
    </table>
</asp:Content>
