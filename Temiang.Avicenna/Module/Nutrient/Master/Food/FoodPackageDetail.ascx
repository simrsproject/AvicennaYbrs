<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FoodPackageDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Master.FoodPackageDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumFoodPackage" runat="server" ValidationGroup="FoodPackage" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="FoodPackage"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblFoodID" runat="server" Text="Food"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboFoodID" Width="300px" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboFoodID_ItemDataBound"
                OnItemsRequested="cboFoodID_ItemsRequested" 
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
                ControlToValidate="cboFoodID" SetFocusOnError="True" ValidationGroup="FoodPackage"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="FoodPackage"
                CausesValidation="true" Visible='<%# !(DataItem is GridInsertionObject) %>'>
            </asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="FoodPackage" CausesValidation="true" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>