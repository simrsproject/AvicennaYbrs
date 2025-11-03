<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeNameCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.EmployeeNameCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Employee Name" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboPersonID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboPersonID_ItemDataBound"
                OnItemsRequested="cboPersonID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>