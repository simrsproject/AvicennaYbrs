<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="RecruitmentPlanDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Recruitment.Master.RecruitmentPlanDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblRecruitmentPlanID" runat="server" Text="Recruitment Plan ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtRecruitmentPlanID" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRecruitmentPlanID" runat="server" ErrorMessage="Recruitment Plan ID required."
                                ValidationGroup="entry" ControlToValidate="txtRecruitmentPlanID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trRecruitmentPlanName">
                        <td class="label">
                            <asp:Label ID="lblRecruitmentPlanName" runat="server" Text="Recruitment Plan Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRecruitmentPlanName" runat="server" Width="300px" MaxLength="400" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRecruitmentPlanName" runat="server" ErrorMessage="Recruitment Plan Name required."
                                ValidationGroup="entry" ControlToValidate="txtRecruitmentPlanName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblOrganizationUnitID" runat="server" Text="Department"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboOrganizationUnitID" runat="server" Width="300px" AutoPostBack="true"
                                EnableLoadOnDemand="true" MarkFirstMatch="true" HighlightTemplatedItems="true"
                                OnItemDataBound="cboOrganizationUnitID_ItemDataBound" OnItemsRequested="cboOrganizationUnitID_ItemsRequested"
                                OnSelectedIndexChanged="cboOrganizationUnitID_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblDivision" runat="server" Text="Division"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboDivisionID" runat="server" Width="300px" AutoPostBack="true"
                                EnableLoadOnDemand="true" MarkFirstMatch="true" HighlightTemplatedItems="true"
                                OnItemDataBound="cboDivisionID_ItemDataBound" OnItemsRequested="cboDivisionID_ItemsRequested"
                                OnSelectedIndexChanged="cboDivisionID_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSubDivision" runat="server" Text="Sub Division"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboSubDivisonID" runat="server" Width="300px" AutoPostBack="true"
                                EnableLoadOnDemand="true" MarkFirstMatch="true" HighlightTemplatedItems="true"
                                OnItemDataBound="cboSubDivisonID_ItemDataBound" OnItemsRequested="cboSubDivisonID_ItemsRequested"
                                OnSelectedIndexChanged="cboSubDivisonID_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSection" runat="server" Text="Section / Service Unit"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboSectionID" runat="server" Width="300px" AutoPostBack="true"
                                EnableLoadOnDemand="true" MarkFirstMatch="true" HighlightTemplatedItems="true"
                                OnItemDataBound="cboSectionID_ItemDataBound" OnItemsRequested="cboSectionID_ItemsRequested">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPositionID" runat="server" Text="Position"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboPositionID" runat="server" Width="300px" AutoPostBack="true"
                                EnableLoadOnDemand="true" MarkFirstMatch="true" HighlightTemplatedItems="true"
                                OnItemDataBound="cboPositionID_ItemDataBound" OnItemsRequested="cboPositionID_ItemsRequested"
                                OnSelectedIndexChanged="cboPositionID_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPositionID" runat="server" ErrorMessage="Position required."
                                ValidationGroup="entry" ControlToValidate="cboPositionID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Notes
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtNotes" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
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
                            <telerik:RadDatePicker ID="txtValidFrom" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvValidFrom" runat="server" ErrorMessage="Valid From required."
                                ValidationGroup="entry" ControlToValidate="txtValidFrom" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblValidTo" runat="server" Text="Valid To"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtValidTo" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvValidTo" runat="server" ErrorMessage="Valid To required."
                                ValidationGroup="entry" ControlToValidate="txtValidTo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNumberOfRequestedEmployees" runat="server" Text="Requested Employees No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtNumberOfRequestedEmployees" runat="server" Width="100px"
                                NumberFormat-DecimalDigits="0" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvNumberOfRequestedEmployees" runat="server" ErrorMessage="Number Of Requested Employees required."
                                ValidationGroup="entry" ControlToValidate="txtNumberOfRequestedEmployees" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
