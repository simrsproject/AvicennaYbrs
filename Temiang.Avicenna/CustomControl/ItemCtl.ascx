<%@ Control Language="C#" AutoEventWireup="true" Codebehind="ItemCtl.ascx.cs" Inherits="Temiang.Avicenna.CustomControl.ItemCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblItemID" runat="server" Text="Item*"></asp:Label>
        </td>
        <td class="entry">
            <telerik:radtextbox id="txtItemID" runat="server" width="300px" maxlength="10" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item ID required."
                ControlToValidate="txtItemID" SetFocusOnError="True" ValidationGroup="Item" Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblItemSubGroupID" runat="server" Text="Sub Group*"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox id="cboItemSubGroupID" runat="server" width="300px"/>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvItemSubGroupID" runat="server" ErrorMessage="Item Sub Group ID required."
                ControlToValidate="cboItemSubGroupID" SetFocusOnError="True" ValidationGroup="Item"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblItemName1" runat="server" Text="Item Name 1*"></asp:Label>
        </td>
        <td class="entry">
            <telerik:radtextbox id="txtItemName1" runat="server" width="300px" maxlength="100" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvItemName1" runat="server" ErrorMessage="Item Name 1 required."
                ControlToValidate="txtItemName1" SetFocusOnError="True" ValidationGroup="Item"
                Width="100%">
                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblItemName2" runat="server" Text="Item Name 2*"></asp:Label>
        </td>
        <td class="entry">
            <telerik:radtextbox id="txtItemName2" runat="server" width="300px" maxlength="100" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvItemName2" runat="server" ErrorMessage="Item Name 2 required."
                ControlToValidate="txtItemName2" SetFocusOnError="True" ValidationGroup="Item"
                Width="100%">
                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblAssetBookID" runat="server" Text="Asset Book ID*"></asp:Label>
        </td>
        <td class="entry">
            <telerik:radtextbox id="txtAssetBookID" runat="server" width="300px" maxlength="10" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvAssetBookID" runat="server" ErrorMessage="Asset Book ID required."
                ControlToValidate="txtAssetBookID" SetFocusOnError="True" ValidationGroup="Item"
                Width="100%">
                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
        </td>
        <td class="entry">
            <asp:CheckBox ID="chkIsActive" runat="server" Text="Is Active" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblNotes" runat="server" Text="Notes*"></asp:Label>
        </td>
        <td class="entry">
            <telerik:radtextbox id="txtNotes" runat="server" width="300px" maxlength="4000" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvNotes" runat="server" ErrorMessage="Notes required."
                ControlToValidate="txtNotes" SetFocusOnError="True" ValidationGroup="Item" Width="100%">
                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
</table>
