<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemIdiDetailItem.ascx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.ItemIdiDetailItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemIdiDetail" runat="server" ValidationGroup="ItemIdiDetail" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemIdiDetail"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblProcedureID" runat="server" Text="Procedure"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboProcedureID" runat="server" Width="300px" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboProcedureID_ItemDataBound"
                AutoPostBack="true" OnSelectedIndexChanged="cboProcedureID_SelectedIndexChanged"
                OnItemsRequested="cboProcedureID_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ProcedureName") %>
                    </b>
                    <br />
                    Procedure ID :
                                <%# DataBinder.Eval(Container.DataItem, "ProcedureID")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 50 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvProcedureID" runat="server" ErrorMessage="Procedure required."
                ControlToValidate="cboProcedureID" SetFocusOnError="True" ValidationGroup="ItemIdiDetail"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label"></td>
        <td class="entry">
            <telerik:RadTextBox ID="txtProcedureText" runat="server" Width="300px"
                ReadOnly="true" TextMode="MultiLine" />
        </td>
        <td width="20px" />
        <td />
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ItemIdiDetail"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ItemIdiDetail" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button></td>
    </tr>
</table>
