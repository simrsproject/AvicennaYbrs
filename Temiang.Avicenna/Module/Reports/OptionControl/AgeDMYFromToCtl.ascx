<%@ Control Language="C#" AutoEventWireup="true" Codebehind="AgeDMYFromToCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.AgeDMYFromToCtl" %>
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
                   <telerik:RadNumericTextBox ID="txtYFromAge" runat="server" Width="30px" ></telerik:RadNumericTextBox>  Y
                   <telerik:RadNumericTextBox ID="txtMFromAge" runat="server" Width="30px" ></telerik:RadNumericTextBox>  M
                   <telerik:RadNumericTextBox ID="txtDFromAge" runat="server" Width="30px" ></telerik:RadNumericTextBox>  D
                    &nbsp;</td>
                    <td style="width: 20px">
                        <asp:Label ID="lblToAge" runat="server" Text="s/d" />
                    </td>
                    <td>
                     <telerik:RadNumericTextBox ID="txtYToAge" runat="server" Width="30px"></telerik:RadNumericTextBox>  Y
                     <telerik:RadNumericTextBox ID="txtMToAge" runat="server" Width="30px"></telerik:RadNumericTextBox>  M
                     <telerik:RadNumericTextBox ID="txtDToAge" runat="server" Width="30px"></telerik:RadNumericTextBox>  D
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
