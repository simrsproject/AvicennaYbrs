<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceUnitLocationDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.ServiceUnitLocationDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="ServiceUnitLocation" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ServiceUnitLocation"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblLocationID" runat="server" Text="Location"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboLocationID" Width="300px" AutoPostBack="False"
                EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                OnItemDataBound="cboLocationID_ItemDataBound" OnItemsRequested="cboLocationID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "LocationName") %>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 30 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvLocationID" runat="server" ErrorMessage="Location required."
                ControlToValidate="cboLocationID" SetFocusOnError="True" ValidationGroup="ServiceUnitLocation"
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
            <asp:CheckBox ID="chkIsLocationMain" runat="server" Text="Main Location" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ServiceUnitLocation"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ServiceUnitLocation" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
