<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SupplierCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.SupplierCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Supplier" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboSupplierID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboSupplierID_ItemDataBound"
                OnItemsRequested="cboSupplier_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "SupplierName")%>
                    </b>
                    <br />
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
