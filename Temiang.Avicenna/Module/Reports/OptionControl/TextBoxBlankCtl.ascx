<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TextBoxBlankCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.TextBoxBlankCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text=" " />
        </td>
        <td>
            <telerik:RadTextBox runat="server" ID="txtBoxBlank" Width="100px" ReadOnly="false">
            </telerik:RadTextBox>
        </td>
</table>
