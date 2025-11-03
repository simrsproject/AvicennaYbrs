<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="ProfessionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Master.HRBase.ProfessionDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblProfessionTypeCode" runat="server" Text="Profession Type Code"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtProfessionTypeCode" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvProfessionTypeCode" runat="server" ErrorMessage="Profession Type Code required."
                                ValidationGroup="entry" ControlToValidate="txtProfessionTypeCode" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblProfessionTypeName" runat="server" Text="Profession Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtProfessionTypeName" runat="server" Width="300px" MaxLength="200" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvProfessionTypeName" runat="server" ErrorMessage="Profession Type Name required."
                                ValidationGroup="entry" ControlToValidate="txtProfessionTypeName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblProfessionGroup" runat="server" Text="Profession Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboProfessionGroup" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvProfessionGroup" runat="server" ErrorMessage="Profession Group required."
                                ValidationGroup="entry" ControlToValidate="cboProfessionGroup" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                </table>
            </td>
        </tr>
    </table>
</asp:Content>