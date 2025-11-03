<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemIdiDetailItemSmf.ascx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.ItemIdiDetailItemSmf" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemIdiDetail" runat="server" ValidationGroup="ItemIdiDetail" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemIdiDetail"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblItemID" runat="server" Text="Item tariff"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboItemID" runat="server" Width="300px" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboItemID_ItemDataBound"
                OnItemsRequested="cboItemID_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ItemName") %>
                    </b>
                    <br />
                    Item ID : <%# DataBinder.Eval(Container.DataItem, "ItemID")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 50 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item tariff required."
                ControlToValidate="cboItemID" SetFocusOnError="True" ValidationGroup="ItemIdiDetail"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
        <tr>
        <td class="label">
            <asp:Label ID="Label1" runat="server" Text="SMF"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboSmfID" runat="server" Width="300px">
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Smf required."
                ControlToValidate="cboSmfID" SetFocusOnError="True" ValidationGroup="ItemIdiDetail"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ItemIdiDetail"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ItemIdiDetail" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button></td>
    </tr>
</table>
