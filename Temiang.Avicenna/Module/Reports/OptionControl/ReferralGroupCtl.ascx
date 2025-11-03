<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReferralGroupCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.ReferralGroupCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Triage" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboReferralGroup" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboReferralGroup_ItemDataBound"
                OnItemsRequested="cboReferralGroup_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%> </b>
                    <br />
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>