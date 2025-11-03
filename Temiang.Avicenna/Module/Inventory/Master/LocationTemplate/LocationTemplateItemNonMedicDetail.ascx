<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocationTemplateItemNonMedicDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Master.LocationTemplateItemNonMedicDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemBalance" runat="server" ValidationGroup="ItemNonMedic" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemNonMedic"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboItemID" Height="190px" Width="300px" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboItemID_ItemDataBound"
                OnSelectedIndexChanged="cboItemID_SelectedIndexChanged" OnItemsRequested="cboItemID_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                    </b>
                    <br />
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ItemID")%>
                    </b>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 30 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="Label1" runat="server" Text="Unit"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtSRItemUnitName" runat="server" Width="100px" Enabled="false" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ItemNonMedic"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ItemNonMedic" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>