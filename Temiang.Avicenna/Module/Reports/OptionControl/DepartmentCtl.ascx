<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DepartmentCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.DepartmentCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Departement" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboDepartmentID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboDepartmentID_ItemDataBound"
                OnItemsRequested="cboDepartmentID_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "DepartmentName")%> </b>
                    <br />
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        
    </tr>
</table>