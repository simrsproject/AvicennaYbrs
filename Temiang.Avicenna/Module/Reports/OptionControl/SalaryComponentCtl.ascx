<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalaryComponentCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.SalaryComponentCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Salary Component" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboSalaryComponetID" runat="server" Width="100%" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboSalaryComponetID_ItemDataBound"
                OnItemsRequested="cboSalaryComponetID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "SalaryComponentCode")%>
                    &nbsp;-&nbsp;
                    <%# DataBinder.Eval(Container.DataItem, "SalaryComponentName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 10 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
