
<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    Codebehind="IndicationDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Master.IndicationDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblIndicationID" runat="server" Text="Indication ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtIndicationID" runat="server" Width="100px" MaxLength="10" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvIndicationID" runat="server" ErrorMessage="Indication ID required."
                    ValidationGroup="entry" ControlToValidate="txtIndicationID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblIndicationName" runat="server" Text="Indication Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtIndicationName" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvIndicationName" runat="server" ErrorMessage="Indication Name required."
                    ValidationGroup="entry" ControlToValidate="txtIndicationName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
                        <td class="label">
                        <asp:Label ID="lblIsActive" runat="server" Text="Active"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
    </table>
    
</asp:Content>

