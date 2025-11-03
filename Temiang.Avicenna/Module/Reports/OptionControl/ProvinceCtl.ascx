<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProvinceCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.ProvinceCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Province" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboProvince" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboProvince_ItemDataBound"
                OnItemsRequested="cboProvince_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%> </b>
                    <br />
                </ItemTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>