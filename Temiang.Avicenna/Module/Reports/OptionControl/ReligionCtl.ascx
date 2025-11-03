<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReligionCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.ReligionCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Triage" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboReligion" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboReligion_ItemDataBound"
                OnItemsRequested="cboReligion_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                    </b>
                </ItemTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>