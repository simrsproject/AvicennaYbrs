<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="ImportFromExcel.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Budgeting.ImportFromExcel" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <asp:FileUpload ID="fUpload" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
