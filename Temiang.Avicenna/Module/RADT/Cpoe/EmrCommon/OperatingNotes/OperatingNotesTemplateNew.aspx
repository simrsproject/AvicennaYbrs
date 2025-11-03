<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="OperatingNotesTemplateNew.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.EmrCommon.OperatingNotes.OperatingNotesTemplateNew"
    Title="Operating Notes Template New" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td width="25%" class="label">Template Name
            </td>
            <td width="75%">
                <asp:TextBox ID="txtTemplateName" runat="server" Width="400px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <fieldset style="width: 97%">
        <legend>Operating Note </legend>
        <telerik:RadTextBox ID="txtOperatingNotes" runat="server" Width="100%" Height="285px" ReadOnly="false"
                            TextMode="MultiLine" />
    </fieldset>

</asp:Content>
