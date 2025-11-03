<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InternalExternalPatientStatusCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.InternalExternalPatientStatusCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Status Patient" />
        </td>
        <td>
            <asp:RadioButtonList ID="rbtInternalExternalPatientStatus" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                <asp:ListItem Value="0" Text="All" />
                <asp:ListItem Value="1" Text="Inpatient" />
                <asp:ListItem Value="2" Text="Outpatient" />
            </asp:RadioButtonList>
        </td>
    </tr>
</table>