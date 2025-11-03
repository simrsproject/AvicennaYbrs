<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PettyCashUnitCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.PettyCashUnitCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Unit" />
        </td>
        <td class="entry">
             <telerik:RadComboBox ID="cboEntry" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboEntry_ItemDataBound"
                OnItemsRequested="cboEntry_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%> </b>
                    <br />
                </ItemTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>