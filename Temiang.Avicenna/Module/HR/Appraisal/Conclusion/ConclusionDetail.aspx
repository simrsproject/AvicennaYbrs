<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="ConclusionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Appraisal.Conclusion.ConclusionDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblConclusionID" runat="server" Text="ConclusionID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtConclusionID" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblConclusionName" runat="server" Text="Conclusion Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtConclusionName" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvConclusionName" runat="server" ErrorMessage="Conclusion Name required."
                                ValidationGroup="entry" ControlToValidate="txtConclusionName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMinValue" runat="server" Text="Minimum Value"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtMinValue" runat="server" Width="100px" NumberFormat-DecimalDigits="2" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvMinValue" runat="server" ErrorMessage="Minimum Value required."
                                ValidationGroup="entry" ControlToValidate="txtMinValue" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMaxValue" runat="server" Text="Maximum Value"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtMaxValue" runat="server" Width="100px" NumberFormat-DecimalDigits="2"/>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvMaxValue" runat="server" ErrorMessage="Maximum Value required."
                                ValidationGroup="entry" ControlToValidate="txtMaxValue" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>

                </table>
            </td>
            <td style="width: 50%" valign="top">
                <table>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
