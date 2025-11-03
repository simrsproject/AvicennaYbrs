<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PersonalInfoCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.PersonalInfoCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Employee" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboPersonID" runat="server" Width="100%" OnItemsRequested="cboPersonID_ItemsRequested"
                AllowCustomText="true" EnableLoadOnDemand="true" OnItemDataBound="cboPersonID_ItemDataBound" />
        </td>
    </tr>
</table>
