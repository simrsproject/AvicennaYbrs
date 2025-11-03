<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VisitReasonCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.VisitReasonCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Triage" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboVisitReason" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboVisitReason_ItemDataBound"
                OnItemsRequested="cboVisitReason_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                    </b>
                </ItemTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
