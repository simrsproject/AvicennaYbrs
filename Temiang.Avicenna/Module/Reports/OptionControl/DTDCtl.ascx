<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DTDCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.DTDCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="DTD" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboDtdNo" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboDtdNo_ItemDataBound"
                OnItemsRequested="cboDtdNo_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "DtdName")%>
                    <br />
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>