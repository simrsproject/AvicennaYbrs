<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="ServiceRoomUploadFoto.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ServiceRoomUploadFoto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <telerik:RadUpload ID="ruFoto" runat="server" ControlObjectsVisibility="None">
                </telerik:RadUpload>
            </td>
        </tr>
    </table>
</asp:Content>
