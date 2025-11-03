<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemProcedureDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ItemProcedureDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemServiceProcedure" runat="server" ValidationGroup="ItemServiceProcedure" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemServiceProcedure"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblSRProcedure" runat="server" Text="Procedure"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboSRProcedure" Height="190px" Width="300px"
                EnableLoadOnDemand="true" HighlightTemplatedItems="true" OnItemDataBound="cboSRProcedure_ItemDataBound"
                OnItemsRequested="cboSRProcedure_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 10 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvSRProcedure" runat="server" ErrorMessage="Procedure required."
                ControlToValidate="cboSRProcedure" SetFocusOnError="True" ValidationGroup="ItemServiceProcedure"
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