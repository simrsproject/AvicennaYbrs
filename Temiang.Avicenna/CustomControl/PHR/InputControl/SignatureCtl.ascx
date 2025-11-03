<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SignatureCtl.ascx.cs" Inherits="Temiang.Avicenna.CustomControl.Phr.InputControl.SignatureCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<table width="100%">
    <tr>
        <td style="width: 128px">
            <a style="cursor: pointer;" onclick="javascript:editSignature('edit','<%=rbImage.ClientID %>','<%=hdnImage.ClientID %>');return false;">
            <telerik:RadBinaryImage ID="rbImage" runat="server"
                Width="300px" Height="125px" ResizeMode="Fit"></telerik:RadBinaryImage>
        </a>
            <div>
                <asp:HiddenField runat="server" ID="hdnImage" />
            </div>
        </td>
    </tr>
</table>

