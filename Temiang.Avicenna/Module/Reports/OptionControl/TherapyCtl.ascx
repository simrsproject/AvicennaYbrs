<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TherapyCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.TherapyCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Therapy Group" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboSRTherapyGroupID" runat="server" Width="100%" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="True" OnItemDataBound="cboSRTherapyGroupID_ItemDataBound"
                OnItemsRequested="cboSRTherapyGroupID_ItemsRequested" OnSelectedIndexChanged="cboSRTherapyGroupID_SelectedIndexChanged">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "ItemID")%>
                    &nbsp;-&nbsp;
                    <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 50 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblTherapy" runat="server" Text="Therapy" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboTherapyID" runat="server" Width="100%" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="False" OnItemDataBound="cboTherapyID_ItemDataBound"
                OnItemsRequested="cboTherapyID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "TherapyID")%>
                    &nbsp;-&nbsp;
                    <%# DataBinder.Eval(Container.DataItem, "TherapyName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 10 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
