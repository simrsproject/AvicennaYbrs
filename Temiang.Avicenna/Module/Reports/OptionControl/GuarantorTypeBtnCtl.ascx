<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GuarantorTypeBtnCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.GuarantorTypeBtnCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Guarantor Type" />
        </td>
        <td>
            <asp:RadioButtonList ID="rbtGuarantorTypeBtn" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                <asp:ListItem Value="0" Text="All" />
                <asp:ListItem Value="1" Text="Personal" />
                <asp:ListItem Value="2" Text="Coorporate" />
            </asp:RadioButtonList>
        </td>
    </tr>
</table>
