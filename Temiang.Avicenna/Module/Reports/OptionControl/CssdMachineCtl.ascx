<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CssdMachineCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.CssdMachineCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Machine" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboMachineID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboMachineID_ItemDataBound"
                OnItemsRequested="cboMachineID_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "MachineName")%>
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