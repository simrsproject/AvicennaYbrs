<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DietMenuDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Nutrient.Master.DietMenuDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumDietMenu" runat="server" ValidationGroup="DietMenu" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="DietMenu"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblFormOfFood" runat="server" Text="Form Of Food"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboFormOfFood" runat="server" Width="300px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvFormOfFood" runat="server" ErrorMessage="FormOfFood required."
                ControlToValidate="cboFormOfFood" SetFocusOnError="True" ValidationGroup="DietMenu"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblMenuID" runat="server" Text="Menu"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboMenuID" Width="300px" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboMenuID_ItemDataBound" OnItemsRequested="cboMenuID_ItemsRequested"
                ValidationGroup="other">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "MenuName")%>
                        &nbsp;(<%# DataBinder.Eval(Container.DataItem, "MenuID")%>) </b>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvMenuID" runat="server" ErrorMessage="Menu required."
                ControlToValidate="cboMenuID" SetFocusOnError="True" ValidationGroup="DietMenu"
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
            <asp:CheckBox ID="chkIsActive" Text="Active" runat="server" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="DietMenu"
                CausesValidation="true" Visible='<%# !(DataItem is GridInsertionObject) %>'>
            </asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="DietMenu" CausesValidation="true" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
