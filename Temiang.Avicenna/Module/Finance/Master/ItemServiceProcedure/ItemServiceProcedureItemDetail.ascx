<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemServiceProcedureItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.ItemServiceProcedureItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemServiceProcedure" runat="server" ValidationGroup="ItemServiceProcedure" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemServiceProcedure"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboItemID" Height="190px" Width="300px"
                EnableLoadOnDemand="true" HighlightTemplatedItems="true" OnItemDataBound="cboItemID_ItemDataBound"
                OnItemsRequested="cboItemID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 30 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvcboItemID" runat="server" ErrorMessage="Item Service required."
                ControlToValidate="cboItemID" SetFocusOnError="True" ValidationGroup="ItemServiceProcedure"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ItemServiceProcedure"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ItemServiceProcedure" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>