<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssetSubGroupDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Master.AssetSubGroupDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumAssetSubGroup" runat="server" ValidationGroup="AssetSubGroup" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="AssetSubGroup"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblAssetSubGroupId" runat="server" Text="Sub Group ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtAssetSubGroupId" runat="server" Width="300px" MaxLength="10" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvAssetSubGroupId" runat="server" ErrorMessage="Sub Group ID required."
                            ControlToValidate="txtAssetSubGroupId" SetFocusOnError="True" ValidationGroup="AssetSubGroup"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblAssetSubGroupName" runat="server" Text="Sub Group Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtAssetSubGroupName" runat="server" Width="300px" MaxLength="250" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvAssetSubGroupName" runat="server" ErrorMessage="Sub Group Name required."
                            ValidationGroup="entry" ControlToValidate="txtAssetSubGroupName" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblInitial" runat="server" Text="Initial"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtInitial" runat="server" Width="100px" MaxLength="3" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvInitial" runat="server" ErrorMessage="Initial required."
                            ValidationGroup="entry" ControlToValidate="txtInitial" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                    </td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="AssetSubGroup"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="AssetSubGroup" Visible='<%# DataItem is GridInsertionObject %>'>
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
