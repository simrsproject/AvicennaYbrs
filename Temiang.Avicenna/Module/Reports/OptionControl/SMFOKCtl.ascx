<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SMFOKCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.SMFOKCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Speciality" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboSMFOK" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboSMFOK_ItemDataBound"
                OnItemsRequested="cboSMFOK_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                    </b>
                </ItemTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
