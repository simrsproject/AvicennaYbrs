<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pDateBetween.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.pDateBetween" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblParameterCaption" runat="server" Text="From" />
        </td>
        <td>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <telerik:RadDatePicker runat="server" ID="txtFromDate" Width="100px">
                        </telerik:RadDatePicker>
                    </td>
                    <td style="width: 20px">
                        <asp:Label ID="lblToDate" runat="server" Text="To" />
                    </td>
                    <td>
                        <telerik:RadDatePicker runat="server" ID="txtToDate" Width="100px">
                        </telerik:RadDatePicker>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
