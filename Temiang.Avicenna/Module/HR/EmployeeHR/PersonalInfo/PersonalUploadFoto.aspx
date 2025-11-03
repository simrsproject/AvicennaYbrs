<%@ Page Title="Upload Photo" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="PersonalUploadFoto.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.PersonalUploadFoto" %>
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