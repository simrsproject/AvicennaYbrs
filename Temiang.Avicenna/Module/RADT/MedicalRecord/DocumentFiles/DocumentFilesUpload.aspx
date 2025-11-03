<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="DocumentFilesUpload.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.DocumentFilesUpload" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="File Template"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadUpload ID="uplFileTemplate" runat="server" ControlObjectsVisibility="None"
                    Width="300px">
                </telerik:RadUpload>
            </td>
            <td width="20px">
                
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
