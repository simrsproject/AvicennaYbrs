<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ZatActiveInteractionDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Inventory.Master.ZatActiveInteractionDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsum" runat="server" ValidationGroup="InteractionZatActive" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="InteractionZatActive"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lbl1" runat="server" Text="Interaction with"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboInteractionZatActiveID" Height="190px" Width="300px"
                EnableLoadOnDemand="true" HighlightTemplatedItems="true" OnItemDataBound="cboInteractionZatActiveID_ItemDataBound"
                OnItemsRequested="cboInteractionZatActiveID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "ZatActiveName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 10 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvZatActiveID" runat="server" ErrorMessage="Interaction with required."
                ControlToValidate="cboInteractionZatActiveID" SetFocusOnError="True" ValidationGroup="InteractionZatActive"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lbl2" runat="server" Text="Interaction"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtInteraction" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine" Resize="Vertical" />
        </td>
        <td width="20">
            <asp:RequiredFieldValidator ID="rfvInteraction" runat="server" ErrorMessage="Interaction required."
                ValidationGroup="entry" ControlToValidate="txtInteraction" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>

    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="InteractionZatActive"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="InteractionZatActive" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
