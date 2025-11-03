<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="QueueingSoundUpload.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.QueueingSoundUpload" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblFilePath" runat="server" Text="File Path"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadUpload ID="uplFilePath" runat="server" ControlObjectsVisibility="None" Width="300px" >
                </telerik:RadUpload>
            </td>
            <td width="20px">
                
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
