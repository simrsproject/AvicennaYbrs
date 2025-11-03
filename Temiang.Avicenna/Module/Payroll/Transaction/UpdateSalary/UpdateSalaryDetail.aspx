<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="UpdateSalaryDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Transaction.UpdateSalaryDetail"
    Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblWageTransactionID" runat="server" Text="WageTransactionID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtWageTransactionID" runat="server" Width="100px" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblPersonID" runat="server" Text="PersonID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPersonID" runat="server" Width="100px" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEmployeeNumber" runat="server" Text="Employee No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEmployeeNumber" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEmployeeName" runat="server" Text="Employee Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEmployeeName" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPayrollPeriod" runat="server" Text="Payroll Period"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPayrollPeriodID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPayrollPeriodID_ItemDataBound"
                                OnItemsRequested="cboPayrollPeriodID_ItemsRequested" Enabled="False">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "PayrollPeriodCode")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "PayrollPeriodName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px" colspan="2">
                            <asp:ImageButton ID="btnRecalculate" runat="server" ImageUrl="../../../../Images/Toolbar/process16.png"
                                CausesValidation="False" OnClick="btnRecalculate_Click" ToolTip="Recalculate" />
                            
                        </td>
                        <td>
                            <asp:Label ID="lblRecalculateResult" runat="server" Text="" ForeColor="Red" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" ShowFooter="false" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItem_UpdateCommand"
        AllowPaging="False">
        <PagerStyle Mode="NextPrevAndNumeric" />
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="WageTransactionItemID" PageSize="10">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SalaryComponentCode"
                    HeaderText="Code" UniqueName="SalaryComponentCode" SortExpression="SalaryComponentID"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="SalaryComponentName"
                    HeaderText="Salary Component" UniqueName="SalaryComponentName" SortExpression="SalaryComponentName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="NominalAmount" HeaderText="Amount"
                    UniqueName="NominalAmount" SortExpression="NominalAmount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SRCurrencyCode" HeaderText="Currency Code"
                    UniqueName="SRCurrencyCode" SortExpression="SRCurrencyCode" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CurrencyRate" HeaderText="Currency Rate"
                    UniqueName="CurrencyRate" SortExpression="CurrencyRate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CurrencyAmount" HeaderText="Currency Amount"
                    UniqueName="CurrencyAmount" SortExpression="CurrencyAmount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridTemplateColumn />
            </Columns>
            <EditFormSettings UserControlName="UpdateSalaryItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="UpdateSalaryItemEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
