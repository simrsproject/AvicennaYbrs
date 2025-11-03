<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AnestesiTypeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.AnestesiTypeCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Anestesi Type" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboAnestesiType" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboAnestesiType_ItemDataBound"
                OnItemsRequested="cboAnestesiType_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%> </b>
                    <br />
                </ItemTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>