<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeEmploymentPeriodDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.EmployeeEmploymentPeriodDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEmployeeEmploymentPeriod" runat="server" ValidationGroup="EmployeeEmploymentPeriod" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EmployeeEmploymentPeriod"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblEmployeeEmploymentPeriodID" runat="server" Text="Employee Employment Period ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtEmployeeEmploymentPeriodID" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvEmployeeEmploymentPeriodID" runat="server" ErrorMessage="Employee Employment Period ID required."
                            ControlToValidate="txtEmployeeEmploymentPeriodID" SetFocusOnError="True" ValidationGroup="EmployeeEmploymentPeriod"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSREmploymentType" runat="server" Text="Employment Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSREmploymentType" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSREmploymentType" runat="server" ErrorMessage="Employment Type required."
                            ControlToValidate="cboSREmploymentType" SetFocusOnError="True" ValidationGroup="EmployeeEmploymentPeriod"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblEmployeeNo" runat="server" Text="Employee No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtEmployeeNo" runat="server" Width="300px" MaxLength="50"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvEmployeeNo" runat="server" ErrorMessage="Employee No required."
                            ControlToValidate="txtEmployeeNo" SetFocusOnError="True" ValidationGroup="EmployeeEmploymentPeriod"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSREmploymentCategory" runat="server" Text="Employment Category"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSREmploymentCategory" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label1" runat="server" Text="Recruitment Plan"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboRecruitmentPlanID" runat="server" Width="300px" AllowCustomText="true"
                            EnableLoadOnDemand="true" OnItemsRequested="cboRecruitmentPlanID_ItemsRequested"
                            OnItemDataBound="cboRecruitmentPlanID_ItemDataBound" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EmployeeEmploymentPeriod"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="EmployeeEmploymentPeriod" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblValidFrom" runat="server" Text="Valid From"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtValidFrom" runat="server" Width="100px" MinDate="01/01/1900"
                            MaxDate="12/31/2999" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvValidFrom" runat="server" ErrorMessage="Valid From required."
                            ControlToValidate="txtValidFrom" SetFocusOnError="True" ValidationGroup="EmployeeEmploymentPeriod"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblValidTo" runat="server" Text="Valid To"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtValidTo" runat="server" Width="100px" MinDate="01/01/1900"
                            MaxDate="12/31/2999" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvValidTo" runat="server" ErrorMessage="Valid To required."
                            ControlToValidate="txtValidTo" SetFocusOnError="True" ValidationGroup="EmployeeEmploymentPeriod"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNote" runat="server" Text="Notes"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNote" runat="server" Width="300px" Height="80px" MaxLength="500"
                            TextMode="MultiLine" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
