<%@ Page Title="Add New Item" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="AddItem.aspx.cs" Inherits="Temiang.Avicenna.Module.Cssd.Transaction.AddItem" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboItemID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtQuantity" />
                    <telerik:AjaxUpdatedControl ControlID="txtSRItemUnit" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%">
        <tr>
            <td class="label">Item
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboItemID" Height="190px" Width="100%" EnableLoadOnDemand="true"
                    HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboItemID_ItemDataBound"
                    OnSelectedIndexChanged="cboItemID_SelectedIndexChanged" OnItemsRequested="cboItemID_ItemsRequested">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                        &nbsp;(<%# DataBinder.Eval(Container.DataItem, "ItemID")%>)
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 20 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item is required."
                    ValidationGroup="entry" ControlToValidate="cboItemID" SetFocusOnError="True"
                    Width="100%" Visible="False">
                    <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label">Current Qty
            </td>
            <td class="entry">
                <table cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <telerik:RadNumericTextBox ID="txtQuantity" runat="server" Width="100px" MaxLength="10"
                                MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="2" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtSRItemUnit" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvQty" runat="server" ErrorMessage="Current Qty is required."
                    ValidationGroup="entry" ControlToValidate="txtQuantity" SetFocusOnError="True"
                    Width="100%" Visible="False">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label">Note
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNote" runat="server" Width="100%" TextMode="MultiLine" MaxLength="500" />
            </td>
            <td width="20px"></td>
        </tr>
    </table>
</asp:Content>
