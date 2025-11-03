<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemNonMedicBalanceDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Inventory.Master.ItemNonMedicBalanceDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemBalance" runat="server" ValidationGroup="ItemNonMedic" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemNonMedic"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="cboItemID">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtSRItemUnitName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
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
                    </b>&nbsp; Stock :&nbsp;<%# DataBinder.Eval(Container.DataItem, "Balance","n0") %>
                    &nbsp;<%# DataBinder.Eval(Container.DataItem, "Unit") %>
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
        <td class="label">
            <asp:Label ID="lblMinimum" runat="server" Text="Minimum"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtMinimum" runat="server" Width="100px" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblMaximum" runat="server" Text="Maximum"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtMaximum" runat="server" Width="100px" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblBalance" runat="server" Text="Balance"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtBalance" runat="server" Width="100px" Enabled="false" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblSRItemBin" runat="server" Text="Item Bin"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboSRItemBin" Height="190px" Width="300px"
                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="False"
                OnItemDataBound="cboSRItemBin_ItemDataBound" OnItemsRequested="cboSRItemBin_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
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
    <tr runat="server" id="trItemSubBin">
        <td class="label">
            <asp:Label ID="lblItemSubBin" runat="server" Text="Item Sub Bin"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtItemSubBin" runat="server" Width="300px" MaxLength="150"/>
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
