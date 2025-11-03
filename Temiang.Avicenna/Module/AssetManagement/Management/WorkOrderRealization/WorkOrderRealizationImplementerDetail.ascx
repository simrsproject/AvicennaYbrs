<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkOrderRealizationImplementerDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.WorkOrderRealizationImplementerDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumAssetWorkOrderImplementer" runat="server" ValidationGroup="AssetWorkOrderImplementer" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="AssetWorkOrderImplementer"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblUserID" runat="server" Text="Implemented By"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboUserID" Width="300px" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboUserID_ItemDataBound"
                OnItemsRequested="cboUserID_ItemsRequested" Enabled="False">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "UserName")%>
                    </b>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
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
            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="250" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="AssetWorkOrderImplementer"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="AssetWorkOrderImplementer" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                OnClick="btnCancel_ButtonClick" CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>