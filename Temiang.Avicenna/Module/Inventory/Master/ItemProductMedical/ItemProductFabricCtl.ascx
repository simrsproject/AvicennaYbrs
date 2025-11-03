<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemProductFabricCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Master.ItemProductFabricCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemBalance" runat="server" ValidationGroup="ItemProductFabric" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemProductFabric"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblFabricID" runat="server" Text="Factory" />
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboFabricID" Width="300px" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboFabricID_ItemDataBound"
                OnItemsRequested="cboFabricID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "FabricName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 10 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvFabricID" runat="server" ErrorMessage="Factory required."
                ValidationGroup="ItemProductFabric" ControlToValidate="cboFabricID" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ItemProductFabric"
                Visible='<%# !(DataItem is GridInsertionObject) %>' />
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ItemProductFabric" Visible='<%# DataItem is GridInsertionObject %>' />
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel" />
        </td>
    </tr>
</table>
