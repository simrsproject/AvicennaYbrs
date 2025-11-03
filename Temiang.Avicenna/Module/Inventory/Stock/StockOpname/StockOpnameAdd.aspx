<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="StockOpnameAdd.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.StockOpname.RSCH.StockOpnameAdd"
    Title="New Stock Opname" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboFromServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboFromLocationID" />
                    <telerik:AjaxUpdatedControl ControlID="cboItemBin" />
                    <telerik:AjaxUpdatedControl ControlID="cboSRItemType" />
                    <telerik:AjaxUpdatedControl ControlID="cboSRTherapyGroupID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboFromLocationID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboItemBin" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboSRItemType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboItemGroupID" />
                    <telerik:AjaxUpdatedControl ControlID="cboSRTherapyGroupID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image6" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                    DatePopupButton-Enabled="false" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvTransactionDate" runat="server" ErrorMessage="Transaction Date required."
                    ValidationGroup="entry" ControlToValidate="txtTransactionDate" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboFromServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboFromServiceUnitID_SelectedIndexChanged" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvFromServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                    ValidationGroup="entry" ControlToValidate="cboFromServiceUnitID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblFromLocationID" runat="server" Text="Location"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboFromLocationID" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboFromLocationID_SelectedIndexChanged">
                </telerik:RadComboBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvFromLocationID" runat="server" ErrorMessage="Location required."
                    ValidationGroup="entry" ControlToValidate="cboFromLocationID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRItemType" runat="server" Text="Item Type"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRItemType" runat="server" Width="300px" AutoPostBack="true"
                    OnSelectedIndexChanged="cboSRItemType_SelectedIndexChanged" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvSRItemType" runat="server" ErrorMessage="Item Type required."
                    ValidationGroup="entry" ControlToValidate="cboSRItemType" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Item Bin"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboItemBin" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains">
                </telerik:RadComboBox>
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblItemGroupID" runat="server" Text="Item Group"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboItemGroupID" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains" />
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblProductType" runat="server" Text="Product Type"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRProductType" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains" />
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRTherapyGroup" runat="server" Text="Therapy Group"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRTherapyGroupID" runat="server" Width="300px" EnableLoadOnDemand="true"
                    MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboSRTherapyGroupID_ItemDataBound"
                    OnItemsRequested="cboSRTherapyGroupID_ItemsRequested">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ItemID")%>
                        &nbsp;-&nbsp;
                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 50 items
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
                <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" />
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
            </td>
            <td class="entry">
                <asp:RadioButtonList ID="rbtStockStatus" runat="server" RepeatDirection="Vertical"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="0" Text="All Item" />
                    <asp:ListItem Value="1" Text="Only In Stock" />
                </asp:RadioButtonList>
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
        <tr runat="server" id="trUsingBarcode">
            <td class="label">
            </td>
            <td class="entry">
                <asp:CheckBox ID="chkIsUsingBarcode" runat="server" Text="Using BARCODE" />
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
