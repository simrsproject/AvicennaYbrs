<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageCtl.ascx.cs" Inherits="Temiang.Avicenna.CustomControl.Phr.InputControl.ImageCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<table width="100%">
    <tr>
        <td style="width: 128px">
            <a style="cursor: pointer;" onclick="javascript:editImage('edit','<%=rbImage.ClientID %>','<%=hdnImage.ClientID %>');return false;">
            <telerik:RadBinaryImage ID="rbImage" runat="server"
                Width="125px" Height="125px" ResizeMode="Fit"></telerik:RadBinaryImage>
        </a>
            <div>
                <asp:HiddenField runat="server" ID="hdnImage" />
            </div>
        </td>
        <td>
            <asp:TextBox runat="server" ID="txtNotes" Width="100%" Height="125px" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
</table>

