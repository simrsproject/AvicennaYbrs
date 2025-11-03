<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="EmployeeLoanDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Transaction.EmployeeLoanDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblEmployeeLoanID" runat="server" Text="Employee Loan ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtEmployeeLoanID" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvEmployeeLoanID" runat="server" ErrorMessage="Employee Loan ID required."
                                ValidationGroup="entry" ControlToValidate="txtEmployeeLoanID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPersonID" runat="server" Text="Employee Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPersonID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPersonID_ItemDataBound"
                                OnItemsRequested="cboPersonID_ItemsRequested">
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
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblLoanDate" runat="server" Text="Loan Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtLoanDate" runat="server" Width="100px" MinDate="01/01/1900"
                                MaxDate="12/31/2999" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvLoanDate" runat="server" ErrorMessage="Loan Date required."
                                ValidationGroup="entry" ControlToValidate="txtLoanDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="Loan No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtLoanNo" runat="server" Width="100px" NumberFormat-DecimalDigits="0" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Loan Number required."
                                ValidationGroup="entry" ControlToValidate="txtLoanNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRPurposeOfLoan" runat="server" Text="Purpose Of Loan"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRPurposeOfLoan" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRPurposeOfLoan" runat="server" ErrorMessage="Purpose Of Loan required."
                                ValidationGroup="entry" ControlToValidate="cboSRPurposeOfLoan" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Business Partner"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboBusinessPartnerID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAmount" runat="server" Text="Loan Amount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtAmount" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ErrorMessage="Loan Amount required."
                                ValidationGroup="entry" ControlToValidate="txtAmount" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Coverage Amount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtCoverageAmount" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Coverage Amount required."
                                ValidationGroup="entry" ControlToValidate="txtCoverageAmount" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblLoanAmountWithInterest" runat="server" Text="Loan Amount With Interest"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtLoanAmountWithInterest" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblPercentRateOfInterest" runat="server" Text="Percent Rate Of Interest"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPercentRateOfInterest" runat="server" Type="Percent"
                                Width="100px" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkFlatRate" runat="server" Text="Flat Rate" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsApproved" Text="Approved" runat="server" />
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
                            <asp:Label ID="Label5" runat="server" Text="Installment Method"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rbtInstallmentMethod" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" AutoPostBack="true" OnSelectedIndexChanged="rbtInstallmentMethod_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="By Qty" Selected="True" />
                                <asp:ListItem Value="1" Text="By Amount" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNumberOfInstallment" runat="server" Text="Installment Qty"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtNumberOfInstallment" runat="server" Width="100px"
                                ReadOnly="true" OnTextChanged="txtNumberOfInstallment_TextChanged" AutoPostBack="true" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAmountOfInstallment" runat="server" Text="Installment Amount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtAmountOfInstallment" runat="server" Width="100px"
                                ReadOnly="true" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSRHRPaymentMethod" runat="server" Text="Payment Method"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRHRPaymentMethod" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblStartPaymentPeriode" runat="server" Text="Start Payment Periode"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboStartPaymentID" runat="server" Width="300px" AllowCustomText="true"
                                EnableLoadOnDemand="true" DataTextField="PayrollPeriodName" DataValueField="PayrollPeriodID"
                                OnItemsRequested="cboStartPaymentID_ItemsRequested" OnItemDataBound="cboStartPaymentID_DataBound">
                                <FooterTemplate>
                                    Note : Show max 12 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvStartPaymentPeriode" runat="server" ErrorMessage="Start Payment Periode required."
                                ValidationGroup="entry" ControlToValidate="cboStartPaymentID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSalaryComponetID" runat="server" Text="Salary Componet Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSalaryComponetID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSalaryComponetID_ItemDataBound"
                                OnItemsRequested="cboSalaryComponetID_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "SalaryComponentCode")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "SalaryComponentName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSalaryComponetID" runat="server" ErrorMessage="Salary Componet Name required."
                                ValidationGroup="entry" ControlToValidate="cboSalaryComponetID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label4" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" TextMode="MultiLine" />
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
    <telerik:RadGrid ID="grdEmployeeLoanItem" runat="server" OnNeedDataSource="grdEmployeeLoanItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEmployeeLoanItem_UpdateCommand"
        OnDeleteCommand="grdEmployeeLoanItem_DeleteCommand" OnInsertCommand="grdEmployeeLoanItem_InsertCommand">
        <HeaderContextMenu>
            <CollapseAnimation Duration="200" Type="OutQuint" />
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeeLoanDetailID">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" Visible="false">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="EmployeeLoanDetailID"
                    HeaderText="Employee Loan Detail ID" UniqueName="EmployeeLoanDetailID" SortExpression="EmployeeLoanDetailID"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="EmployeeLoanID" HeaderText="Employee Loan ID"
                    UniqueName="EmployeeLoanID" SortExpression="EmployeeLoanID" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="InstallmentNumber"
                    HeaderText="Installment Number" UniqueName="InstallmentNumber" SortExpression="InstallmentNumber"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="PlanDate" HeaderText="Plan Date"
                    UniqueName="PlanDate" SortExpression="PlanDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PlanAmount" HeaderText="Plan Amount"
                    UniqueName="PlanAmount" SortExpression="PlanAmount" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="MainPayment" HeaderText="Main Payment"
                    UniqueName="MainPayment" SortExpression="MainPayment" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" Visible="false" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="InterestPayment"
                    HeaderText="Interest Payment" UniqueName="InterestPayment" SortExpression="InterestPayment"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}"
                    Visible="false" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ActualDate" HeaderText="Actual Date"
                    UniqueName="ActualDate" SortExpression="ActualDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="ActualAmount" HeaderText="Actual Amount"
                    UniqueName="ActualAmount" SortExpression="ActualAmount" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridBoundColumn DataField="PayrollPeriodName" HeaderText="Payroll Period Name"
                    UniqueName="PayrollPeriodName" SortExpression="PayrollPeriodName" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsPaid" HeaderText="Paid"
                    UniqueName="IsPaid" SortExpression="IsPaid" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="EmployeeLoanItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="EmployeeLoanItemDetailCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
            <CollapseAnimation Duration="200" Type="OutQuint" />
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
