<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EDCCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.EDCCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="EDC Name" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboEDCMachineID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboEDCMachineID_ItemDataBound"
                OnItemsRequested="cboEDCMachineID_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "EDCMachineName")%>
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
