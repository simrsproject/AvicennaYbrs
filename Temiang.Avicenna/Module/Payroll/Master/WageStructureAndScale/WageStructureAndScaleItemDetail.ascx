<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WageStructureAndScaleItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.WageStructureAndScaleItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumWageStructureAndScale" runat="server" ValidationGroup="WageStructureAndScale" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="WageStructureAndScale"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemID" runat="server" Text="ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtItemID" runat="server" Width="300px" MaxLength="10" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="ID required."
                            ControlToValidate="txtItemID" SetFocusOnError="True" ValidationGroup="WageStructureAndScale"
                            Width="100%">
                            <asp:Image ID="Image14" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemName" runat="server" Text="Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px" MaxLength="200" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ErrorMessage="Name required."
                            ControlToValidate="txtItemName" SetFocusOnError="True" ValidationGroup="WageStructureAndScale"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="WageStructureAndScale"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="WageStructureAndScale" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
                
            </table>
        </td>
    </tr>
</table>