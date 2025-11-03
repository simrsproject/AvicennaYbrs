<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SterilizationProcessItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Cssd.Transaction.SterilizationProcessItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumCssdSterilizationProcessItem" runat="server" ValidationGroup="CssdSterilizationProcessItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="CssdSterilizationProcessItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblProcessSeqNo" runat="server" Text="Seq No / Item #"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtProcessSeqNo" runat="server" Width="100px" Enabled="false" Text="" />
            <telerik:RadTextBox ID="txtItemNo" runat="server" Width="100px" Enabled="false" Text="" />    
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvProcessSeqNo" runat="server" ErrorMessage="Sequence No required."
                ControlToValidate="txtProcessSeqNo" SetFocusOnError="True" ValidationGroup="CssdSterilizationProcessItem"
                Width="100%">
                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr style="display: none">
        <td class="label">
            <asp:Label ID="lblReferenceNo" runat="server" Text="Reference No"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtReceivedNo" runat="server" Width="100px" Enabled="false" />
            <telerik:RadTextBox ID="txtReceivedSeqNo" runat="server" Width="100px" Enabled="false" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboItemID" Height="190px" Width="350px" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboItemID_ItemDataBound" OnItemsRequested="cboItemID_ItemsRequested"
                Enabled="False">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
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
    <tr height="30">
        <td class="label">
            <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label>
        </td>
        <td class="entry">
            <table cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <telerik:RadNumericTextBox ID="txtQty" runat="server" Width="100px" MaxLength="10"
                            MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="2" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <telerik:RadComboBox runat="server" ID="cboSRCssdItemUnit" Width="100px" AllowCustomText="true"
                            Filter="Contains" Enabled="False">
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ErrorMessage="Quantity required."
                ControlToValidate="txtQty" SetFocusOnError="True" ValidationGroup="CssdSterilizationProcessItem"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvSRCssdItemUnit" runat="server" ErrorMessage="Unit required."
                ControlToValidate="cboSRCssdItemUnit" SetFocusOnError="True" ValidationGroup="CssdSterilizationProcessItem"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr height="30">
        <td class="label">
            <asp:Label ID="Label1" runat="server" Text="Weight"></asp:Label>
        </td>
        <td class="entry">
            <table cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <telerik:RadNumericTextBox ID="txtWeight" runat="server" Width="100px" MaxLength="10"
                            MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="2" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Gr
                    </td>
                </tr>
            </table>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvtWeight" runat="server" ErrorMessage="Weight required."
                ControlToValidate="txtWeight" SetFocusOnError="True" ValidationGroup="CssdSterilizationProcessItem"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="CssdSterilizationProcessItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="CssdSterilizationProcessItem" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
