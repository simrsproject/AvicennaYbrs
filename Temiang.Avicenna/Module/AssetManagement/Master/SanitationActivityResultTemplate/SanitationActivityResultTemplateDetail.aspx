<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="SanitationActivityResultTemplateDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Master.SanitationActivityResultTemplateDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSanitationActivityResultID" runat="server" Text="ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSanitationActivityResultID" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRWorkTradeItem" runat="server" Text="Work Trade Item"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRWorkTradeItem" runat="server" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRWorkTradeItem" runat="server" ErrorMessage="Work Trade Item required."
                                ValidationGroup="entry" ControlToValidate="cboSRWorkTradeItem" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblResultTemplateName" runat="server" Text="Result Template Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtResultTemplateName" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvResultTemplateName" runat="server" ErrorMessage="Result Template Name required."
                                ValidationGroup="entry" ControlToValidate="txtResultTemplateName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table style="width: 100%" cellpadding="0" cellspacing="1">
        <tr>
            <td>
                <fieldset>
                    <legend>RESULT</legend>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <telerik:RadEditor ID="txtResult" runat="server" Width="100%" Height="650px" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
