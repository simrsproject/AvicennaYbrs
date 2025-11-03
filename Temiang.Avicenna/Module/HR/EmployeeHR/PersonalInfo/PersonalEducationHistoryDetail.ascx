<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PersonalEducationHistoryDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.PersonalEducationHistoryDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumPersonalEducationHistory" runat="server" ValidationGroup="PersonalEducationHistory" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PersonalEducationHistory"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblPersonalEducationHistoryID" runat="server" Text="Personal Education History ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtPersonalEducationHistoryID" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPersonalEducationHistoryID" runat="server" ErrorMessage="Personal Education History ID required."
                            ControlToValidate="txtPersonalEducationHistoryID" SetFocusOnError="True" ValidationGroup="PersonalEducationHistory" Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSREducationLevel" runat="server" Text="Education Level"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSREducationLevel" runat="server" Width="300px" AllowCustomText="true" Filter="Contains" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSREducationLevel" runat="server" ErrorMessage="Education Level required."
                            ControlToValidate="cboSREducationLevel" SetFocusOnError="True" ValidationGroup="PersonalEducationHistory" Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblInstitutionName" runat="server" Text="Institution Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtInstitutionName" runat="server" Width="300px" MaxLength="50" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvInstitutionName" runat="server" ErrorMessage="Institution Name required."
                            ControlToValidate="txtInstitutionName" SetFocusOnError="True" ValidationGroup="PersonalEducationHistory" Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblLocation" runat="server" Text="Location"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtLocation" runat="server" Width="300px" MaxLength="50" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblMajors" runat="server" Text="Majors"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtMajors" runat="server" Width="300px" MaxLength="100" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>

                <tr>
                    <td class="label">
                        <asp:Label ID="lblStartYear" runat="server" Text="Start Year"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtStartYear" runat="server" Width="300px" MaxLength="4" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblEndYear" runat="server" Text="End Year"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtEndYear" runat="server" Width="300px" MaxLength="4" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblGraduateDate" runat="server" Text="Graduate Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtGraduateDate" runat="server" Width="100px" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PersonalEducationHistory"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="PersonalEducationHistory"
                            Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button></td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDiplomaNo" runat="server" Text="Diploma No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtDiplomaNo" runat="server" Width="300px" MaxLength="100" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblGpa" runat="server" Text="Gpa"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtGpa" runat="server" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblAchievement" runat="server" Text="Achievement"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtAchievement" runat="server" Width="300px" MaxLength="20" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDiplomaVerificationNo" runat="server" Text="Diploma Verification No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtDiplomaVerificationNo" runat="server" Width="300px" MaxLength="100" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblEducationalAdjustmentDate" runat="server" Text="Educational Adjustment Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtEducationalAdjustmentDate" runat="server" Width="100px" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNote" runat="server" Text="Note"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNote" runat="server" Width="300px" Height="80px" MaxLength="500" TextMode="MultiLine" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>

</table>
