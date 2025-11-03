<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="PositionGradeDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.PositionInformation.PositionGradeDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblPositionGradeID" runat="server" Text="Position Grade ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPositionGradeID" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPositionGradeID" runat="server" ErrorMessage="Position Grade ID required."
                                ValidationGroup="entry" ControlToValidate="txtPositionGradeID"
                                SetFocusOnError="True" Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPositionGradeCode" runat="server" Text="Position Grade Code"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPositionGradeCode" runat="server" Width="300px" MaxLength="10" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPositionGradeCode" runat="server" ErrorMessage="Position Grade Code required."
                                ValidationGroup="entry" ControlToValidate="txtPositionGradeCode"
                                SetFocusOnError="True" Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPositionGradeName" runat="server" Text="Position Grade Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPositionGradeName" runat="server" Width="300px" MaxLength="200" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPositionGradeName" runat="server" ErrorMessage="Position Grade Name required."
                                ValidationGroup="entry" ControlToValidate="txtPositionGradeName"
                                SetFocusOnError="True" Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trInterval">
                        <td class="label">
                            <asp:Label ID="lblInterval" runat="server" Text="Interval"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInterval" runat="server" Width="300px" MaxLength="200" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRanking" runat="server" Text="Ranking"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtRanking" runat="server" Width="100px" NumberFormat-DecimalDigits="0" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRankName" runat="server" Text="Rank Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRankName" runat="server" Width="300px" MaxLength="200" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSREmploymentType" runat="server" Text="Employment Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSREmploymentType" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                     <tr runat="server" id="trLimit">
                        <td class="label">
                            <asp:Label ID="lblLimit" runat="server" Text="Limit"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtLowerLimit" runat="server" Width="100px" NumberFormat-DecimalDigits="0" />
                                    </td>
                                    <td>&nbsp;-&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtUpperLimit" runat="server" Width="100px" NumberFormat-DecimalDigits="0" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>

