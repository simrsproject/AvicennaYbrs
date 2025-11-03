<%@ Page Title="Exam Order Template" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="TemplateNew.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.ServiceUnitTransaction.TemplateNew" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">Template Name
            </td>
            <td>
                <asp:TextBox ID="txtTemplateName" runat="server" Width="350px" MaxLength="100"></asp:TextBox>
            </td>
            <td></td>
        </tr>
    </table>

</asp:Content>
