<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="EditFromRegistrationNo.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.EditFromRegistrationNo"
    Title="Note" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">From Registration No
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboFromRegistrationNo" Width="100%">
                </telerik:RadComboBox>
            </td>
            <td width="20"></td>
            <td></td>
        </tr>
    </table>
</asp:Content>
