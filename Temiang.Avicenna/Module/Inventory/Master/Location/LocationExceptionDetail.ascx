<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocationExceptionDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Master.LocationExceptionDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumLocationExceptionSetting" runat="server" ValidationGroup="LocationExceptionSetting" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="LocationExceptionSetting"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblLocationExceptionID" runat="server" Text="Location"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboLocationExceptionID" Width="300px" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboLocationExceptionID_ItemDataBound" OnItemsRequested="cboLocationExceptionID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "LocationName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvLocationExceptionID" runat="server" ErrorMessage="Location required."
                ValidationGroup="LocationExceptionSetting" ControlToValidate="cboLocationExceptionID" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="LocationExceptionSetting"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="LocationExceptionSetting" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
