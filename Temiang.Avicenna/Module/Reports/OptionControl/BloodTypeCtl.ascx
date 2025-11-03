<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BloodTypeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.BloodTypeCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Triage" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboBloodType" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboBloodType_ItemDataBound"
                OnItemsRequested="cboBloodType_ItemsRequested">
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