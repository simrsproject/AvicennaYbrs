<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SMFCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.SMFCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="SMF" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboSMFID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboSMFID_ItemDataBound"
                OnItemsRequested="cboSMF_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "SMFName")%>
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
