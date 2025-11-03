<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TextBoxBlank2Ctl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.TextBoxBlank2Ctl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text=" " />
        </td>
        <td>
            <telerik:RadTextBox runat="server" ID="txtBoxBlank" Width="100%" ReadOnly="false">
            </telerik:RadTextBox>
        </td>
</table>
