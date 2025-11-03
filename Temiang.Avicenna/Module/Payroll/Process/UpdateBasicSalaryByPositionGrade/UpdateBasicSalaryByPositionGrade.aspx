<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="UpdateBasicSalaryByPositionGrade.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Process.UpdateBasicSalaryByPositionGrade" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblDate" runat="server" Text="Process Date" />
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtDate" runat="server" Width="100px" />
            </td>
            <td style="text-align: left">
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Payroll Period Name" />
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboPayrollPeriodID" runat="server" Width="304px" EnableLoadOnDemand="true"
                    MarkFirstMatch="False" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboPayrollPeriodID_ItemDataBound"
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
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Payroll Period Name required."
                    ValidationGroup="entry" ControlToValidate="cboPayrollPeriodID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td />
        </tr>
    </table>
    <telerik:RadGrid ID="grdEmployeeList" runat="server" OnNeedDataSource="grdEmployeeList_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdEmployeeList_DeleteCommand"
        OnItemCommand="grdEmployeeList_ItemCommand">
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="PersonID">
            <CommandItemTemplate>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <td align="left">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rbList" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                        OnSelectedIndexChanged="rbList_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Selected="True">
                                            <font color="white">Employee</font>
                                        </asp:ListItem>
                                        <asp:ListItem Value="1">
                                            <font color="white">Position Grade</font>
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
                                        <asp:Label runat="server" ID="lblAddRow" Text="Add new record" />
                                    </asp:LinkButton>
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:LinkButton ID="lbClear" runat="server" CommandName="Clear">
                                        <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/new16.png" />
                                        <asp:Label runat="server" ID="lbl" Text="Clear" />
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="right" style="display: none">
                        <asp:LinkButton ID="lbProcess" runat="server" CommandName="Process" OnClientClick="if (!confirm('Are you sure to process?')) return false;">
                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/refresh16.png" />
                            <asp:Label runat="server" ID="lblProcess" Text="Process" />
                        </asp:LinkButton>
                        &nbsp;&nbsp;
                    </td>
                </table>
            </CommandItemTemplate>
            <CommandItemStyle Height="29px" />
            <Columns>
                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                    SortExpression="EmployeeName" />
                <telerik:GridDateTimeColumn DataField="JoinDate" HeaderText="Join Date" UniqueName="JoinDate"
                    SortExpression="JoinDate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="75px" />
                <telerik:GridBoundColumn DataField="ServiceYear" HeaderText="Service Year" UniqueName="ServiceYear"
                    SortExpression="ServiceYear" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    HeaderStyle-Width="80px" />
                <telerik:GridBoundColumn DataField="PositionGradeID" UniqueName="PositionGradeID"
                    SortExpression="PositionGradeID" Visible="False" />
                <telerik:GridBoundColumn DataField="PositionGradeName" HeaderText="Current Position Grade"
                    UniqueName="PositionGradeName" SortExpression="PositionGradeName" HeaderStyle-Width="300px" />
                <telerik:GridBoundColumn DataField="GradeYear" HeaderText="Current Grade Year" UniqueName="GradeYear"
                    SortExpression="GradeYear" HeaderStyle-Width="100px" />
                <telerik:GridTemplateColumn UniqueName="TemplateColumn1" HeaderStyle-Width="320px"
                    HeaderText="Next Position Grade">
                    <ItemTemplate>
                        <telerik:RadComboBox ID="cboPositionGradeID" runat="server" Width="304px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPositionGradeID_ItemDataBound"
                            OnItemsRequested="cboPositionGradeID_ItemsRequested">
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="TemplateColumn2" HeaderStyle-Width="100px"
                    HeaderText="Next Grade Year">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox runat="server" ID="txtToGradeYear" Width="80px" NumberFormat-DecimalDigits="0" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
