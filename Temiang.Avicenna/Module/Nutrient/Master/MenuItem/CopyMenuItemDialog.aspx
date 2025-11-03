<%@ Page Title="Copy Menu Item" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="CopyMenuItemDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Master.CopyMenuItemDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMenuItemID" runat="server" Text="Menu Item ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMenuItemID" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMenuItemName" runat="server" Text="Description"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMenuItemName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMenu" runat="server" Text="Menu"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMenu" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px">
                            <telerik:RadTextBox ID="txtMenuID" runat="server" Visible="False" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblVersion" runat="server" Text="Version"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtVersion" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px">
                            <telerik:RadTextBox ID="txtVersionID" runat="server" Visible="False" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSeqNo" runat="server" Text="Seq No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSeqNo" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblClassID" runat="server" Text="Class"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtClass" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px">
                            <telerik:RadTextBox ID="txtClassID" runat="server" Visible="False" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <fieldset>
                    <legend>COPY FROM</legend>
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblFromSeqNo" runat="server" Text="Seq No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboFromSeqNo" runat="server" Width="300px" />
                                        </td>
                                        <td style="width: 20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblFromClassID" runat="server" Text="Class"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboClassID" runat="server" Width="300px" />
                                        </td>
                                        <td style="width: 20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
