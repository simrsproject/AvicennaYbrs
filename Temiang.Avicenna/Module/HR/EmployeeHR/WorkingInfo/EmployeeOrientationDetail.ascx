<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeOrientationDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.EmployeeOrientationDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEmployeeOrientation" runat="server" ValidationGroup="EmployeeOrientation" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EmployeeOrientation"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblEmployeeOrientationID" runat="server" Text="ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtEmployeeOrientationID" runat="server" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblOrientationType" runat="server" Text="Orientation Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <asp:RadioButtonList ID="rblOrientationType" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="true">General</asp:ListItem>
                            <asp:ListItem>Particular</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDate" runat="server" Text="Orientation Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="txtStartDate" runat="server" Width="100px" />
                                </td>
                                <td>&nbsp;-&nbsp;
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtEndDate" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>

                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ErrorMessage="Start Date required."
                            ControlToValidate="txtStartDate" SetFocusOnError="True" ValidationGroup="EmployeeOrientation"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ErrorMessage="End Date required."
                            ControlToValidate="txtEndDate" SetFocusOnError="True" ValidationGroup="EmployeeOrientation"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDuration" runat="server" Text="Duration"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtDurationHour" runat="server" Width="70px" MinValue="0" NumberFormat-DecimalDigits="0" />
                                </td>
                                <td>&nbsp;Hour&nbsp;</td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtDurationMinutes" runat="server" Width="70px" MinValue="0" NumberFormat-DecimalDigits="0" />
                                </td>
                                <td>&nbsp;Minutes</td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EmployeeOrientation"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="EmployeeOrientation" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
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
