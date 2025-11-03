<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AgeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.AgeCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Age" />
        </td>
        <td>
            <table cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <telerik:RadNumericTextBox runat="server" ID="txtFromAge" Width="50px">
                        </telerik:RadNumericTextBox>
                    </td>
                    <td style="width: 20px">
                        <asp:Label ID="lblToAge" runat="server" Text="To" />
                    </td>
                    <td>
                        <telerik:RadNumericTextBox runat="server" ID="txtToAge" Width="50px">
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
