<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AlphabetCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.AlphabetCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Alphabet" />
        </td>
        <td>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <telerik:RadTextBox runat="server" ID="txtFromAlphabet" Width="50px" MaxLength="1">
                        </telerik:RadTextBox>
                    </td>
                    <td>
                        &nbsp;<asp:Label ID="lblTo" runat="server" Text="To" />&nbsp;
                    </td>
                    <td>
                        <telerik:RadTextBox runat="server" ID="txtToAlphabet" Width="50px" MaxLength="1">
                        </telerik:RadTextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
