<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="CoorporateGradeDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.CoorporateGradeDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblCoorporateGradeID" runat="server" Text="ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtCoorporateGradeID" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvCoorporateGradeID" runat="server" ErrorMessage="ID required."
                                ValidationGroup="entry" ControlToValidate="txtCoorporateGradeID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCoorporateGradeLevel" runat="server" Text="Level"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtCoorporateGradeLevel" runat="server" Width="100px" NumberFormat-DecimalDigits="0" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvCoorporateGradeLevel" runat="server" ErrorMessage="Level required."
                                ValidationGroup="entry" ControlToValidate="txtCoorporateGradeLevel" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCoorporateGradeMin" runat="server" Text="Minimum"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtCoorporateGradeMin" runat="server" Width="100px" NumberFormat-DecimalDigits="0"/>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvCoorporateGradeMin" runat="server" ErrorMessage="Minimum required."
                                ValidationGroup="entry" ControlToValidate="txtCoorporateGradeMin" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCoorporateGradeMax" runat="server" Text="Maximum"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtCoorporateGradeMax" runat="server" Width="100px" NumberFormat-DecimalDigits="0"/>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvCoorporateGradeMax" runat="server" ErrorMessage="Maximum required."
                                ValidationGroup="entry" ControlToValidate="txtCoorporateGradeMax" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCoorporateGradeInterval" runat="server" Text="Interval"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtCoorporateGradeInterval" runat="server" Width="100px" NumberFormat-DecimalDigits="0"/>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvCoorporateGradeInterval" runat="server" ErrorMessage="Interval required."
                                ValidationGroup="entry" ControlToValidate="txtCoorporateGradeInterval" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Coefficient"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtCoorporateGradeCoefficient" runat="server" Width="100px" NumberFormat-DecimalDigits="2"/>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Coefficient required."
                                ValidationGroup="entry" ControlToValidate="txtCoorporateGradeCoefficient" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
    </table>
</asp:Content>