<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="IncentivePositionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.IncentivePositionDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblIncentivePositionCode" runat="server" Text="Code"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtIncentivePositionCode" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvIncentivePositionCode" runat="server" ErrorMessage="Incentive Position Code required."
                                ValidationGroup="entry" ControlToValidate="txtIncentivePositionCode" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblIncentivePositionName" runat="server" Text="Incentive Position"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtIncentivePositionName" runat="server" Width="300px" MaxLength="200" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvIncentivePositionName" runat="server" ErrorMessage="Incentive Position Name required."
                                ValidationGroup="entry" ControlToValidate="txtIncentivePositionName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblIncentivePositionGroup" runat="server" Text="Incentive Position Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboIncentivePositionGroup" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvIncentivePositionGroup" runat="server" ErrorMessage="Incentive Position Group required."
                                ValidationGroup="entry" ControlToValidate="cboIncentivePositionGroup" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"><asp:Label ID="lblPoint" runat="server" Text="Point"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPoint" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPoint" runat="server" ErrorMessage="Point required."
                                ValidationGroup="entry" ControlToValidate="txtPoint" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
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
