<%@ Control Language="C#" AutoEventWireup="true" Codebehind="AgeFromToCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.AgeFromToCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Age" />
        </td>
        <td>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                   <telerik:RadNumericTextBox ID="txtFromAge" runat="server" Width="30px" ></telerik:RadNumericTextBox>
                    &nbsp;</td>
                    <td style="width: 20px">
                        <asp:Label ID="lblToAge" runat="server" Text="s/d" />
                    </td>
                    <td>
                     <telerik:RadNumericTextBox ID="txtToAge" runat="server" Width="30px"></telerik:RadNumericTextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
