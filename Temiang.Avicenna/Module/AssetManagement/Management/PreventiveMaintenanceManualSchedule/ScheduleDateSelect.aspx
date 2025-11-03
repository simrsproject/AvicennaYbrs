<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="ScheduleDateSelect.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.ScheduleDateSelect"
    Title="Schedule Setting" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="850">
        <tr>
            <td class="header" style="width: 200px">
                <asp:Label ID="Label1" runat="server" Text="- Select Date -"></asp:Label>
            </td>
            <td class="header" style="width: 650px">
            </td>
        </tr>
        <tr>
            <td valign="top" style="width: 200px">
                <telerik:RadCalendar ID="cldSchedule" runat="server" ShowOtherMonthsDays="False">
                </telerik:RadCalendar>
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkIsVoid" Text="Void" />
            </td>
        </tr>
    </table>
</asp:Content>
