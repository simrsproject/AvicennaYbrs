<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="SurgeryCostEstimationPreview.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.Billing.FinalizeBilling.SurgeryCostEstimationPreview" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboItemGroupID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboItemID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="lblClassID" Text="Charge Class" />
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboClassID" runat="server" Width="300px" />
            </td>
            <td width="20px"></td>
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblItemGroupID" runat="server" Text="Item Group" />
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboItemGroupID" Width="300px" AutoPostBack="true"
                    EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                    OnItemDataBound="cboItemGroupID_ItemDataBound" OnItemsRequested="cboItemGroupID_ItemsRequested"
                    OnSelectedIndexChanged="cboItemGroupID_SelectedIndexChanged">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ItemGroupName") %>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 10 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20px"></td>
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblItemID" runat="server" Text="Item Name" />
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboItemID" Width="300px" AutoPostBack="false"
                    EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                    OnItemDataBound="cboItemID_ItemDataBound" OnItemsRequested="cboItemID_ItemsRequested">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ItemName") %>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 30 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20px"></td>
            <td />
        </tr>
    </table>
</asp:Content>
