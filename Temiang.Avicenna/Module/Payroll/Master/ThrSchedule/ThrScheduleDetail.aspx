<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ThrScheduleDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.ThrScheduleDetail"
    Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table width="100%">
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblCounterID" runat="server" Text="CounterID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtCounterID" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvCounterID" runat="server" ErrorMessage="Counter ID required."
                                ValidationGroup="entry" ControlToValidate="txtCounterID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPayrollPeriod" runat="server" Text="Payroll Period" />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPayrollPeriodID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="False" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboPayrollPeriodID_ItemDataBound"
                                OnItemsRequested="cboPayrollPeriodID_ItemsRequested" OnSelectedIndexChanged="cboPayrollPeriodID_SelectedIndexChanged">
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
                            <asp:RequiredFieldValidator ID="rfvPayrollPeriod" runat="server" ErrorMessage="Payroll Period required."
                                ValidationGroup="entry" ControlToValidate="cboPayrollPeriodID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPayrollPeriodName" runat="server" Text="Payroll Period Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPayrollPeriodName" runat="server" Width="300px" MaxLength="200" Enabled="false" />
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
                            <asp:Label ID="lblPayDate" runat="server" Text="Pay Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtPayDate" runat="server" Width="100px" MinDate="01/01/1900"
                                MaxDate="12/31/2999" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPayDate" runat="server" ErrorMessage="Pay Date required."
                                ValidationGroup="entry" ControlToValidate="txtPayDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSPTYear" runat="server" Text="SPT Year"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtSPTYear" runat="server" Width="100px" NumberFormat-DecimalDigits="0" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSPTYear" runat="server" ErrorMessage="SPT Year required."
                                ValidationGroup="entry" ControlToValidate="txtSPTYear" SetFocusOnError="True"
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
    <telerik:RadGrid ID="grdDetail" runat="server" OnNeedDataSource="grdDetail_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdDetail_DeleteCommand" OnInsertCommand="grdDetail_InsertCommand">
        <HeaderContextMenu>
            <CollapseAnimation Duration="200" Type="OutQuint" />
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="CounterItemID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="SRReligion"
                    HeaderText="SRReligion" UniqueName="SRReligion"
                    SortExpression="SRReligion" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridNumericColumn DataField="ReligionName" HeaderText="Religion"
                    UniqueName="ReligionName" SortExpression="ReligionName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="ThrScheduleItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="ThrScheduleItemEditCommand">
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
