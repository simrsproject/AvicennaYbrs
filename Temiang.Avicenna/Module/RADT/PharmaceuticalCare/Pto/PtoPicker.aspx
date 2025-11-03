<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PtoPicker.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PharmaceuticalCare.PtoPicker" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/CustomControl/RegistrationInfoCtl.ascx" TagPrefix="uc1" TagName="RegistrationInfoCtl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server" />
    <table width="100%" id="table1" runat="server" cellpadding="0" cellspacing="0">
        <tr>
            <td valign="top" width="20%">
            </td>
            <td valign="top" width="20%">
            </td>
            <td valign="top" width="20%">
            </td>
            <td valign="top" width="20%">
            </td>
            <td valign="top" width="20%">
            </td>
        </tr>
    </table>

</asp:Content>
