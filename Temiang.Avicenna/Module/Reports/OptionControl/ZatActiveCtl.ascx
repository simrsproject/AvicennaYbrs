<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ZatActiveCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.ZatActiveCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Zat Active" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboZatActiveID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboZatActiveID_ItemDataBound"
                OnItemsRequested="cboZatActiveID_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ZatActiveName")%>
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