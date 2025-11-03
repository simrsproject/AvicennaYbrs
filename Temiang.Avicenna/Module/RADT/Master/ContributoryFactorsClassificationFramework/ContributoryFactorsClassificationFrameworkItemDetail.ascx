<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContributoryFactorsClassificationFrameworkItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ContributoryFactorsClassificationFrameworkItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumFactorItem" runat="server" ValidationGroup="FactorItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="FactorItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblFactorItemID" runat="server" Text="Factor Item ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtFactorItemID" runat="server" Width="100px" MaxLength="10" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvFactorItemID" runat="server" ErrorMessage="FactorItem ID required."
                            ControlToValidate="txtFactorItemID" SetFocusOnError="True" ValidationGroup="FactorItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblFactorItemName" runat="server" Text="Factor Item Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtFactorItemName" runat="server" Width="300px" MaxLength="250" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvFactorItemName" runat="server" ErrorMessage="Factor Item Name required."
                            ControlToValidate="txtFactorItemName" SetFocusOnError="True" ValidationGroup="FactorItem"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="FactorItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="FactorItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
            </table>
        </td>
    </tr>
</table>