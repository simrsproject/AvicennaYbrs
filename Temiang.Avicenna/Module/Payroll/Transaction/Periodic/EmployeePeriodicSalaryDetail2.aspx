<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="EmployeePeriodicSalaryDetail2.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Transaction.EmployeePeriodicSalaryDetail2" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top" width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSalaryComponetID" runat="server" Text="Salary Componet" />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSalaryComponetID" runat="server" Width="304px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboSalaryComponetID_ItemDataBound"
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
                            <asp:RequiredFieldValidator ID="rfvSalaryComponetID" runat="server" ErrorMessage="Salary Componet required."
                                ValidationGroup="entry" ControlToValidate="cboSalaryComponetID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Payroll Period" />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPayrollPeriodID" runat="server" Width="304px" EnableLoadOnDemand="true"
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
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Payroll Period required."
                                ValidationGroup="entry" ControlToValidate="cboPayrollPeriodID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top" width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Transaction Date" />
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRProcessType" runat="server" Text="Process Type" />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRProcessType" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRProcessType" runat="server" ErrorMessage="Process Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRProcessType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdEmployeeList" runat="server" OnNeedDataSource="grdEmployeeList_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdEmployeeList_DeleteCommand"
        OnItemCommand="grdEmployeeList_ItemCommand">
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="PersonID">
            <CommandItemTemplate>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="rbList" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                OnSelectedIndexChanged="rbList_SelectedIndexChanged" Font-Size="Small">
                                <asp:ListItem Value="0" Selected="True">
                                                <font color="blue">Employee</font>
                                </asp:ListItem>
                                <asp:ListItem Value="1">
                                                <font color="blue">Organization Unit</font>
                                </asp:ListItem>
                                <asp:ListItem Value="2">
                                                <font color="blue">Service Unit</font>
                                </asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            &nbsp;
                            <telerik:RadComboBox ID="cboPersonID" runat="server" Width="304px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPersonID_ItemDataBound"
                                OnItemsRequested="cboPersonID_ItemsRequested">
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            &nbsp;
                            <asp:LinkButton ID="lbInsert" runat="server" CommandName="Insert">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/insert16.png" />
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </CommandItemTemplate>
            <CommandItemStyle Height="29px" />
            <Columns>
                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                    SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                    HeaderStyle-Width="300px" />
                <telerik:GridTemplateColumn UniqueName="TemplateColumn1" HeaderStyle-Width="120px"
                    ItemStyle-HorizontalAlign="Center" HeaderText="Amount" HeaderStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtAmount" runat="server" NumberFormat-DecimalDigits="2" 
                            Width="100px" Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "AmountValue")) %>'/>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
                <telerik:GridButtonColumn />
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
