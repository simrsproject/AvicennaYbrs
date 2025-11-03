<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="EmployeeLeaveDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Leave.EmployeeLeaveDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td>
                            <telerik:RadTextBox ID="txtEmployeeLeaveID" runat="server" />
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
                        <td class="label">Employee Leave Type
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSREmployeeLeaveType" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSREmployeeLeaveType" runat="server" ErrorMessage="Employee Leave Type required."
                                ValidationGroup="entry" ControlToValidate="cboSREmployeeLeaveType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Notes
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="500" />
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr runat="server" id="trIsPayCut">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsPayCut" Text="Pay Cut" runat="server" />
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
                                        <telerik:RadDatePicker ID="txtStartDate" runat="server" Width="100px" MinDate="01/01/1900"
                                            MaxDate="12/31/2999" />
                                    </td>
                                    <td style="width: 15px">to
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtEnddate" runat="server" Width="100px" MinDate="01/01/1900"
                                            MaxDate="12/31/2999" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ErrorMessage="Start Date required."
                                ControlToValidate="txtStartDate" SetFocusOnError="True" ValidationGroup="entry"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ErrorMessage="End Date required."
                                ControlToValidate="txtStartDate" SetFocusOnError="True" ValidationGroup="entry"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Leave Entitlements (Days)
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtLeaveEntitlementsQty" runat="server" Width="100px" NumberFormat-DecimalDigits="0" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvLeaveEntitlementsQty" runat="server" ErrorMessage="Leave Entitlements (Days) required."
                                ValidationGroup="entry" ControlToValidate="txtLeaveEntitlementsQty" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr>
                        <td class="labelcaption" colspan="4">
                            <asp:Label ID="Label5" runat="server" Text="Employee Leave Request Information"></asp:Label>
                        </td>
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
                            <telerik:RadNumericTextBox ID="txtBalance" runat="server" Width="100px" ReadOnly="True" NumberFormat-DecimalDigits="0"/>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Employee Leave Request" PageViewID="pgvRequest"
                Selected="true">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvRequest" runat="server">
            <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="False" GridLines="None"
                OnNeedDataSource="grdList_NeedDataSource">
                <MasterTableView DataKeyNames="LeaveRequestID">
                    <ColumnGroups>
                        <telerik:GridColumnGroup HeaderText="Request" Name="Request" HeaderStyle-HorizontalAlign="Center">
                        </telerik:GridColumnGroup>
                    </ColumnGroups>
                    <ColumnGroups>
                        <telerik:GridColumnGroup HeaderText="Verified" Name="Verified" HeaderStyle-HorizontalAlign="Center">
                        </telerik:GridColumnGroup>
                    </ColumnGroups>
                    <Columns>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="LeaveRequestID" HeaderText="ID"
                            UniqueName="LeaveRequestID" SortExpression="LeaveRequestID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="False" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="RequestDate" HeaderText="Request Date"
                            UniqueName="RequestDate" SortExpression="RequestDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="RequestLeaveDateFrom"
                            HeaderText="Leave Date From" UniqueName="RequestLeaveDateFrom" SortExpression="RequestLeaveDateFrom"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ColumnGroupName="Request"/>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="RequestLeaveDateTo"
                            HeaderText="Leave Date To" UniqueName="RequestLeaveDateTo" SortExpression="RequestLeaveDateTo"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ColumnGroupName="Request"/>
                        <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="RequestDays" HeaderText="Leave Days"
                            UniqueName="RequestDays" SortExpression="Fee" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" ColumnGroupName="Request"/>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="RequestWorkingDate" HeaderText="Working Date"
                            UniqueName="RequestWorkingDate" SortExpression="RequestWorkingDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="Request"/>

                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="VerifiedDate" HeaderText="Verified Date"
                            UniqueName="VerifiedDate" SortExpression="VerifiedDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="ApprovedLeaveDateFrom"
                            HeaderText="Leave Date From" UniqueName="ApprovedLeaveDateFrom" SortExpression="ApprovedLeaveDateFrom"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ColumnGroupName="Verified"/>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="ApprovedLeaveDateTo"
                            HeaderText="Leave Date To" UniqueName="ApprovedLeaveDateTo" SortExpression="ApprovedLeaveDateTo"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ColumnGroupName="Verified"/>
                        <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="ApprovedDays" HeaderText="Leave Days"
                            UniqueName="ApprovedDays" SortExpression="Fee" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" ColumnGroupName="Verified"/>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="ApprovedWorkingDate" HeaderText="Working Date"
                            UniqueName="ApprovedWorkingDate" SortExpression="ApprovedWorkingDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="Verified"/>
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="VerifiedByUserID" HeaderText="Verified By"
                            UniqueName="VerifiedByUserID" SortExpression="VerifiedByUserID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" ColumnGroupName="Verified"/>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
