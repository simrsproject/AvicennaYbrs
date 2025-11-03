<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DietCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.DietCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Diet" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboDietID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboDietID_ItemDataBound"
                OnItemsRequested="cboDietID_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "DietName")%> </b>
                    <br />
                </ItemTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>