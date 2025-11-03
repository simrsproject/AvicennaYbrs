<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="EmployeeLeaveRequestDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Leave.EmployeeLeaveRequestDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td>
                            <telerik:RadTextBox ID="txtLeaveRequestID" runat="server" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRequestDate" runat="server" Text="Request Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtRequestDate" runat="server" Width="100px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvRequestDate" runat="server" ErrorMessage="Request Date required."
                                ValidationGroup="entry" ControlToValidate="txtRequestDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPersonID" runat="server" Text="Employee Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPersonID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPersonID_ItemDataBound"
                                OnItemsRequested="cboPersonID_ItemsRequested" AutoPostBack="True" OnSelectedIndexChanged="cboPersonID_SelectedIndexChanged">
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
                            <asp:RequiredFieldValidator ID="rfvPersonID" runat="server" ErrorMessage="Employee Name required."
                                ValidationGroup="entry" ControlToValidate="cboPersonID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image25" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Employee Leave Type
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboEmployeeLeaveID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboEmployeeLeaveID_ItemDataBound"
                                OnItemsRequested="cboEmployeeLeaveID_ItemsRequested" AutoPostBack="True" OnSelectedIndexChanged="cboEmployeeLeaveID_SelectedIndexChanged">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeLeaveTypeName")%>
                                    <br>
                                    <i>Notes: <%# DataBinder.Eval(Container.DataItem, "Notes")%></i>
                                    <br>
                                    <i><%# DataBinder.Eval(Container.DataItem, "Period")%></i>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvEmployeeLeaveID" runat="server" ErrorMessage="Employee Leave Type required."
                                ValidationGroup="entry" ControlToValidate="cboEmployeeLeaveID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPeriod" runat="server" Text="Request Leave Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtRequestLeaveDateFrom" runat="server" Width="100px"
                                            MinDate="01/01/1900" MaxDate="12/31/2999" />
                                    </td>
                                    <td style="width: 15px">to
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtRequestLeaveDateTo" runat="server" Width="100px" MinDate="01/01/1900"
                                            MaxDate="12/31/2999" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRequestLeaveDateFrom" runat="server" ErrorMessage="From Date required."
                                ControlToValidate="txtRequestLeaveDateFrom" SetFocusOnError="True" ValidationGroup="entry"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvRequestLeaveDateTo" runat="server" ErrorMessage="To Date required."
                                ControlToValidate="txtRequestLeaveDateTo" SetFocusOnError="True" ValidationGroup="entry"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRequestDays" runat="server" Text="Request Days"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtRequestDays" runat="server" Width="100px" NumberFormat-DecimalDigits="0" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRequestDays" runat="server" ErrorMessage="Request Days required."
                                ValidationGroup="entry" ControlToValidate="txtRequestDays" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRequestWorkingDate" runat="server" Text="Request Working Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtRequestWorkingDate" runat="server" Width="100px" MinDate="01/01/1900"
                                MaxDate="12/31/2999" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRequestWorkingDate" runat="server" ErrorMessage="Request Working Date required."
                                ValidationGroup="entry" ControlToValidate="txtRequestWorkingDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" TextMode="MultiLine" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReplacementPersonID" runat="server" Text="Replacement Employee"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboReplacementPersonID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboReplacementPersonID_ItemDataBound"
                                OnItemsRequested="cboReplacementPersonID_ItemsRequested" AutoPostBack="False">
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
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="labelcaption" colspan="4">
                            <asp:Label ID="Label5" runat="server" Text="Employee Leave Information"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Period"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtStartDate" runat="server" Width="100px" Enabled="False" />
                                    </td>
                                    <td style="width: 15px">to
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtEndDate" runat="server" Width="100px" Enabled="False" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr height="30">
                        <td class="label">
                            <asp:Label ID="lblLeaveEntitlements" runat="server" Text="Leave Entitlements (Days)"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtLeaveEntitlementsQty" runat="server" Width="100px"
                                ReadOnly="true" NumberFormat-DecimalDigits="0" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr height="30">
                        <td class="label">
                            <asp:Label ID="lblAlreadyTaken" runat="server" Text="Already Taken"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtAlreadyTakenQty" runat="server" Width="100px" ReadOnly="true"
                                NumberFormat-DecimalDigits="0" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr height="30">
                        <td class="label">
                            <asp:Label ID="lblPending" runat="server" Text="Pending"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPendingQty" runat="server" Width="100px" ReadOnly="true"
                                NumberFormat-DecimalDigits="0" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr height="30">
                        <td class="label">
                            <asp:Label ID="lblBalance" runat="server" Text="Balance"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtBalace" runat="server" Width="100px" ReadOnly="true"
                                NumberFormat-DecimalDigits="0" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td>
                            <asp:CheckBox ID="chkIsApproved" Text="Approved" runat="server" />
                            <asp:CheckBox ID="chkIsVoid" Text="Void" runat="server" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Validation" PageViewID="pgvValidation" Selected="true" />
            <telerik:RadTab runat="server" Text="Verification" PageViewID="pgvVerification" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvValidation" runat="server">
            <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%" border="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblLeaveRequestStatus" runat="server" Text="Leave Request Status"></asp:Label>
                                </td>
                                <td class="entry">
                                    <asp:RadioButtonList ID="rblLeaveRequestStatus" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem>Approved</asp:ListItem>
                                        <asp:ListItem>Rejected</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvLeaveRequestStatus" runat="server" ErrorMessage="Leave Request Status required."
                                        ControlToValidate="rblLeaveRequestStatus" SetFocusOnError="True" ValidationGroup="entry"
                                        Width="100%">
                                        <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblValidatedDate" runat="server" Text="Validated Date/Time"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadDateTimePicker ID="txtValidatedDate" runat="server" AutoPostBackControl="None"
                                        Enabled="False">
                                        <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd/MM/yyyy HH:mm" DateFormat="dd/MM/yyyy HH:mm">
                                        </DateInput>
                                        <TimeView ID="TimeView2" runat="server" TimeFormat="HH:mm">
                                        </TimeView>
                                    </telerik:RadDateTimePicker>
                                </td>
                                <td width="20px"></td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblValidatedBy" runat="server" Text="Validated By"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtValidatedBy" runat="server" Width="300px" ReadOnly="true" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <asp:Panel runat="server" ID="pnlValidated1">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblValidated1DateTime" runat="server" Text="Validated #1 Date/Time"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadDateTimePicker ID="txtValidated1Date" runat="server" AutoPostBackControl="None"
                                            Enabled="False">
                                            <DateInput ID="DateInput4" runat="server" DisplayDateFormat="dd/MM/yyyy HH:mm" DateFormat="dd/MM/yyyy HH:mm">
                                            </DateInput>
                                            <TimeView ID="TimeView4" runat="server" TimeFormat="HH:mm">
                                            </TimeView>
                                        </telerik:RadDateTimePicker>
                                    </td>
                                    <td width="20px"></td>
                                    <td />
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblValidated1By" runat="server" Text="Validated #1 By"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtValidated1By" runat="server" Width="300px" ReadOnly="true" />
                                    </td>
                                    <td width="20"></td>
                                    <td></td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnlValidated2">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblValidated2DateTime" runat="server" Text="Validated #2 Date/Time"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadDateTimePicker ID="txtValidated2Date" runat="server" AutoPostBackControl="None"
                                            Enabled="False">
                                            <DateInput ID="DateInput3" runat="server" DisplayDateFormat="dd/MM/yyyy HH:mm" DateFormat="dd/MM/yyyy HH:mm">
                                            </DateInput>
                                            <TimeView ID="TimeView3" runat="server" TimeFormat="HH:mm">
                                            </TimeView>
                                        </telerik:RadDateTimePicker>
                                    </td>
                                    <td width="20px"></td>
                                    <td />
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblValidated2By" runat="server" Text="Validated #2 By"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtValidated2By" runat="server" Width="300px" ReadOnly="true" />
                                    </td>
                                    <td width="20"></td>
                                    <td></td>
                                </tr>
                            </asp:Panel>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblRejectedReason" runat="server" Text="Rejected Reason"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtRejectedReason" runat="server" Width="300px" TextMode="MultiLine" MaxLength="500" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvVerification" runat="server">
            <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%" border="0">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblVerifiedDateTime" runat="server" Text="Verified Date/Time"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadDateTimePicker ID="txtVerifiedDateTime" runat="server" AutoPostBackControl="None"
                                        Enabled="False">
                                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy HH:mm" DateFormat="dd/MM/yyyy HH:mm">
                                        </DateInput>
                                        <TimeView ID="TimeView1" runat="server" TimeFormat="HH:mm">
                                        </TimeView>
                                    </telerik:RadDateTimePicker>
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvVerifiedDateTime" runat="server" ErrorMessage="Verified Date required."
                                        ValidationGroup="entry" ControlToValidate="txtVerifiedDateTime" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblApprovedLeaveDate" runat="server" Text="Leave Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtApprovedLeaveDateFrom" runat="server" Width="100px"
                                                    MinDate="01/01/1900" MaxDate="12/31/2999" />
                                            </td>
                                            <td style="width: 15px">to
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="txtApprovedLeaveDateTo" runat="server" Width="100px" MinDate="01/01/1900"
                                                    MaxDate="12/31/2999" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvApprovedLeaveDateFrom" runat="server" ErrorMessage="Leave Date From required."
                                        ControlToValidate="txtApprovedLeaveDateFrom" SetFocusOnError="True" ValidationGroup="entry"
                                        Width="100%">
                                        <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="rfvApprovedLeaveDateTo" runat="server" ErrorMessage="Leave Date To required."
                                        ControlToValidate="txtApprovedLeaveDateTo" SetFocusOnError="True" ValidationGroup="entry"
                                        Width="100%">
                                        <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblApprovedDays" runat="server" Text="Leave Days"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtApprovedDays" runat="server" Width="100px" NumberFormat-DecimalDigits="0" />
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvApprovedDays" runat="server" ErrorMessage="Leave Days required."
                                        ValidationGroup="entry" ControlToValidate="txtApprovedDays" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblApprovedWorkingDate" runat="server" Text="Working Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtApprovedWorkingDate" runat="server" Width="100px" MinDate="01/01/1900"
                                        MaxDate="12/31/2999" />
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvApprovedWorkingDate" runat="server" ErrorMessage="Working Date required."
                                        ValidationGroup="entry" ControlToValidate="txtApprovedWorkingDate" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <asp:Panel runat="server" ID="pnlPayCut">
                                <tr>
                                    <td class="label"></td>
                                    <td class="entry">
                                        <asp:CheckBox ID="chkIsPayCut" Text="Pay Cut" runat="server" AutoPostBack="true" OnCheckedChanged="chkIsPayCut_CheckedChanged" />
                                    </td>
                                    <td width="20px"></td>
                                    <td />
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPayCutDays" runat="server" Text="Pay Cut Days"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadNumericTextBox ID="txtPayCutDays" runat="server" Width="100px" NumberFormat-DecimalDigits="0" />
                                    </td>
                                    <td width="20px"></td>
                                    <td />
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblSRWorkingDay" runat="server" Text="Working Day"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cboSRWorkingDay" runat="server" Width="300px" />
                                    </td>
                                    <td width="20px"></td>
                                    <td />
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label2" runat="server" Text="Payroll Period"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboPayrollPeriodID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPayrollPeriodID_ItemDataBound"
                                            OnItemsRequested="cboPayrollPeriodID_ItemsRequested">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "PayrollPeriodCode")%>
                                            &nbsp;-&nbsp;
                                            <%# DataBinder.Eval(Container.DataItem, "PayrollPeriodName")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                Note : Show max 12 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px"></td>
                                    <td></td>
                                </tr>
                            </asp:Panel>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
