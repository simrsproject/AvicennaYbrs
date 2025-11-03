<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="EmployeePermissionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Permission.EmployeePermissionDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td>
                            <telerik:RadTextBox ID="txtPermissionID" runat="server" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPermissionDate" runat="server" Text="Permission Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtPermissionDate" runat="server" Width="100px" Enabled="false" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvPermissionDate" runat="server" ErrorMessage="Permission Date required."
                                ValidationGroup="entry" ControlToValidate="txtPermissionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSupervisorID" runat="server" Text="Supervisor"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSupervisorID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboSupervisorID_ItemDataBound"
                                OnItemsRequested="cboSupervisorID_ItemsRequested">
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
                            <asp:RequiredFieldValidator ID="rfvSupervisorID" runat="server" ErrorMessage="Supervisor / Manager required."
                                ValidationGroup="entry" ControlToValidate="cboSupervisorID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
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
                                OnItemsRequested="cboPersonID_ItemsRequested">
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
                        <td class="label">
                            <asp:Label ID="lblSRPermissionType" runat="server" Text="Permission Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRPermissionType" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboSRPermissionType_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRPermissionType" runat="server" ErrorMessage="Permission Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRPermissionType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsPayCut" Text="Pay Cut" runat="server" />&nbsp;&nbsp;
                            <telerik:RadNumericTextBox ID="txtPayCutDays" runat="server" Width="70px" NumberFormat-DecimalDigits="0" /> Days
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPeriod" runat="server" Text="Period"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtPermissionDateFrom" runat="server" Width="100px" />
                                    </td>
                                    <td style="width: 15px">to
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtPermissionDateTo" runat="server" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPermissionDateFrom" runat="server" ErrorMessage="From Date required."
                                ControlToValidate="txtPermissionDateFrom" SetFocusOnError="True" ValidationGroup="entry"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvPermissionDateTo" runat="server" ErrorMessage="To Date required."
                                ControlToValidate="txtPermissionDateTo" SetFocusOnError="True" ValidationGroup="entry"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTime" runat="server" Text="Permission Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTimePicker ID="txtPermissionTimeFrom" runat="server" Width="100px" />
                                    </td>
                                    <td>&nbsp;-&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTimePicker ID="txtPermissionTimeTo" runat="server" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="labelcaption" colspan="4">
                            <asp:Label ID="Label5" runat="server" Text="Verification Information"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblVerifiedDate" runat="server" Text="Verified Date/Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDateTimePicker ID="txtVerifiedDateTime" runat="server" AutoPostBackControl="None"
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
                            <asp:Label ID="lblVerifiedBy" runat="server" Text="Verified By"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtVerifiedBy" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
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
</asp:Content>
