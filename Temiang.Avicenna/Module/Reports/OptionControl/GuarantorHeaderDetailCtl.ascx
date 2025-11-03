<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GuarantorHeaderDetailCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.GuarantorHeaderDetailCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaptionGroup" runat="server" Text="Guarantor Group" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboGuarantorGroupID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboGuarantorID_ItemDataBound"
                OnItemsRequested="cboGuarantorGroupID_ItemsRequested" AutoPostBack="True" OnSelectedIndexChanged="cboGuarantorGroupID_SelectedIndexChanged">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "GuarantorName")%>
                    </b>
                    <br />
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Guarantor" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboGuarantorID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboGuarantorID_ItemDataBound"
                OnItemsRequested="cboGuarantorID_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "GuarantorName")%>
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
