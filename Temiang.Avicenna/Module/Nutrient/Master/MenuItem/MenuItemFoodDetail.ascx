<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuItemFoodDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Nutrient.Master.MenuItemFoodDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumMenuItemFood" runat="server" ValidationGroup="MenuItemFood" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="MenuItemFood"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblFoodID" runat="server" Text="Food"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboFoodID" Width="300px" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboFoodID_ItemDataBound" OnItemsRequested="cboFoodID_ItemsRequested"
                ValidationGroup="other">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "FoodName")%>
                        &nbsp;(<%# DataBinder.Eval(Container.DataItem, "FoodID")%>) </b>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvFoodID" runat="server" ErrorMessage="Food required."
                ControlToValidate="cboFoodID" SetFocusOnError="True" ValidationGroup="MenuItemFood"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr runat="server" id="trSRMenuItemFoodGroup">
        <td class="label">
            <asp:Label ID="lblSRMenuItemFoodGroup" runat="server" Text="Menu Item Group"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboSRMenuItemFoodGroup" Width="300px">
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvSRMenuItemFoodGroup" runat="server" ErrorMessage="Menu Item Group required."
                ControlToValidate="cboFoodID" SetFocusOnError="True" ValidationGroup="MenuItemFood"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
        </td>
        <td class="entry">
            <asp:CheckBox ID="chkIsOptional" Text="Optional Menu" runat="server" OnCheckedChanged="chkIsOptional_CheckedChanged"
                AutoPostBack="True"/>
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
        </td>
        <td class="entry">
            <asp:CheckBox ID="chkIsStandard" Text="Standard Menu" runat="server" OnCheckedChanged="chkIsStandard_CheckedChanged"
                AutoPostBack="True"/>
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="MenuItemFood"
                CausesValidation="true" Visible='<%# !(DataItem is GridInsertionObject) %>'>
            </asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="MenuItemFood" CausesValidation="true" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
