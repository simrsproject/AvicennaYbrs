<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AcctParameterDetail.aspx.cs" MasterPageFile="~/MasterPage/MasterDetail.Master"
Inherits="Temiang.Avicenna.Module.Finance.Master.AcctParameter.AcctParameterDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="ID"></asp:Label></td>
            <td class="entry">
                <telerik:RadTextBox ID="txtParameterID" runat="server" Width="300px">
                </telerik:RadTextBox>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvParameterID" runat="server" ErrorMessage="ID must be filled"
                    ControlToValidate="txtParameterID" ValidationGroup="entry" SetFocusOnError="True"
                    Width="100%">*</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Parameter Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtParameterName" runat="server" Width="300px" ReadOnly="True">
                </telerik:RadTextBox>
            </td>
            <td width="20px" align="left">
                <asp:RequiredFieldValidator ID="rfvParameterName" runat="server" ErrorMessage="Parameter Name must be filled"
                    ControlToValidate="txtParameterName" ValidationGroup="entry" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" /></asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label3" runat="server" Text="Parameter Value"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtParameterValue" runat="server" Width="300px">
                </telerik:RadTextBox>
            </td>
            <td width="20px" align="left">
                
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
