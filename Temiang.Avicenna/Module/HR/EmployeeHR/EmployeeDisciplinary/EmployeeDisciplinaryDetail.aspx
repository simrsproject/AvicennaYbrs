<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="EmployeeDisciplinaryDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Employee.EmployeeDisciplinaryDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblEmployeeDisciplinaryID" runat="server" Text="Employee Disciplinary ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtEmployeeDisciplinaryID" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvEmployeeDisciplinaryID" runat="server" ErrorMessage="Employee Disciplinary ID required."
                                ValidationGroup="entry" ControlToValidate="txtEmployeeDisciplinaryID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPersonalID" runat="server" Text="Personal Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPersonalID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboPersonalID_ItemDataBound"
                                OnItemsRequested="cboPersonalID_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRWarningLevel" runat="server" Text="Warning Level"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRWarningLevel" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="cboSRWarningLevel_SelectedIndexChanged"/>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRWarningLevel" runat="server" ErrorMessage="Warning Level required."
                                ValidationGroup="entry" ControlToValidate="cboSRWarningLevel" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblIncidentDate" runat="server" Text="Incident Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtIncidentDate" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvIncidentDate" runat="server" ErrorMessage="Incident Date required."
                                ValidationGroup="entry" ControlToValidate="txtIncidentDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblDateIssue" runat="server" Text="Date Issue"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtDateIssue" runat="server" Width="100px"/>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvDateIssue" runat="server" ErrorMessage="Date Issue required."
                                ValidationGroup="entry" ControlToValidate="txtDateIssue" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblViolation" runat="server" Text="Violation"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtViolation" runat="server" Width="300px" Height="80px"
                                MaxLength="1000" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvViolation" runat="server" ErrorMessage="Violation required."
                                ValidationGroup="entry" ControlToValidate="txtViolation" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEffectViolation" runat="server" Text="Effect Violation"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEffectViolation" runat="server" Width="300px" Height="80px"
                                MaxLength="1000" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRViolationDegree" runat="server" Text="Violation Degree"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRViolationDegree" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRViolationType" runat="server" Text="Violation Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRViolationType" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAdviceGiven" runat="server" Text="Advice Given"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAdviceGiven" runat="server" Width="300px" Height="80px"
                                MaxLength="1000" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSanctionGiven" runat="server" Text="Sanction Given"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSanctionGiven" runat="server" Width="300px" Height="80px"
                                MaxLength="1000" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
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
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEffectiveDate" runat="server" Text="Effective Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtEffectiveDate" runat="server" Width="100px" AutoPostBack="true" OnSelectedDateChanged="txtEffectiveDate_SelectedDateChanged"/>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvEffectiveDate" runat="server" ErrorMessage="Effective Date required."
                                ValidationGroup="entry" ControlToValidate="txtEffectiveDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblValidUntil" runat="server" Text="Valid Until"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtValidUntil" runat="server" Width="100px"/>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvValidUntil" runat="server" ErrorMessage="Valid Until required."
                                ValidationGroup="entry" ControlToValidate="txtValidUntil" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
    </table>
</asp:Content>
