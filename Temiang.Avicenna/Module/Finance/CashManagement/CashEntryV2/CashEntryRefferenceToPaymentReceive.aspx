<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="CashEntryRefferenceToPaymentReceive.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.CashManagement.CashEntryV2.CashEntryRefferenceToPaymentReceive" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" LoadingPanelID="ral1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterAmount">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" LoadingPanelID="ral1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterEDC">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" LoadingPanelID="ral1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPaymentNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" LoadingPanelID="ral1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" LoadingPanelID="ral1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPatientName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" LoadingPanelID="ral1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnPayMethod">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" LoadingPanelID="ral1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" LoadingPanelID="ral1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdListItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" LoadingPanelID="ral1" />
                    <telerik:AjaxUpdatedControl ControlID="lblSelectedCount" />
                    <telerik:AjaxUpdatedControl ControlID="lblSelectedAmount" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="ral1" runat="server"></telerik:RadAjaxLoadingPanel>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">Payment Date
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDateTimePicker runat="server" ID="txtPaymentDateTimeFrom" Width="160px">
                                            <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                                            </Calendar>
                                            <TimeView runat="server" CellSpacing="-1" Culture="Indonesian (Indonesia)">
                                            </TimeView>
                                            <TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>
                                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                            <DateInput runat="server" DisplayDateFormat="dd/MM/yyyy HH:mm" DateFormat="dd/MM/yyyy HH:mm">
                                            </DateInput>
                                        </telerik:RadDateTimePicker>
                                    </td>
                                    <td>
                                        <telerik:RadDateTimePicker runat="server" ID="txtPaymentDateTimeTo" Width="160px">
                                            <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                                            </Calendar>
                                            <TimeView runat="server" CellSpacing="-1" Culture="Indonesian (Indonesia)">
                                            </TimeView>
                                            <TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>
                                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                            <DateInput runat="server" DisplayDateFormat="dd/MM/yyyy HH:mm" DateFormat="dd/MM/yyyy HH:mm">
                                            </DateInput>
                                        </telerik:RadDateTimePicker>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px" />
                        <asp:ImageButton ID="btnFilterDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                            OnClick="btnFilter_Click" ToolTip="Search" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Amount
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtAmount" runat="server" Width="344px" MaxLength="150"></telerik:RadNumericTextBox>
                        </td>
                        <td style="width: 20px" />
                        <asp:ImageButton ID="btnFilterAmount" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                            OnClick="btnFilter_Click" ToolTip="Search" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">EDC Machine
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboEDCMachineID" runat="server" Width="344px"></telerik:RadComboBox>
                        </td>
                        <td style="width: 20px" />
                        <asp:ImageButton ID="btnFilterEDC" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                            OnClick="btnFilter_Click" ToolTip="Search" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">PaymentNo / Card No
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPaymentNo" runat="server" Width="344px" MaxLength="150"></telerik:RadTextBox>
                        </td>
                        <td style="width: 20px" />
                        <asp:ImageButton ID="btnFilterPaymentNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                            OnClick="btnFilter_Click" ToolTip="Search" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">RegistrationNo
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="344px" MaxLength="150"></telerik:RadTextBox>
                        </td>
                        <td style="width: 20px" />
                        <asp:ImageButton ID="btnFilterRegistrationNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                            OnClick="btnFilter_Click" ToolTip="Search" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Patient Name
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="344px" MaxLength="150"></telerik:RadTextBox>
                        </td>
                        <td style="width: 20px" />
                        <asp:ImageButton ID="btnFilterPatientName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                            OnClick="btnFilter_Click" ToolTip="Search" />
                        <td />
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Payment Method
                        </td>
                        <td class="entry">
                            <asp:ListBox runat="server" SelectionMode="Multiple" ID="lbxPaymentMethod" 
                                DataValueField="SRPaymentMethodID" DataTextField="PaymentMethodName" Width="344px">
                            </asp:ListBox>
                        </td>
                        <td style="width: 20px" />
                        <asp:ImageButton ID="btnPayMethod" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                            OnClick="btnFilter_Click" ToolTip="Search" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Registration Type
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboRegistrationType" runat="server" Width="344px">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" />
                                    <telerik:RadComboBoxItem Value="OPR" Text="Outpatient" />
                                    <telerik:RadComboBoxItem Value="IPR" Text="Inpatient" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td style="width: 20px" />
                        <asp:ImageButton ID="btnFilterRegType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                            OnClick="btnFilter_Click" ToolTip="Search" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Cashier Name
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboCashierName" runat="server" Width="344px"></telerik:RadComboBox>
                        </td>
                        <td style="width: 20px" />
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                            OnClick="btnFilter_Click" ToolTip="Search" />
                        <td />
                    </tr>

                    <tr>
                        <td colspan="3">
                            <table cellpadding="0" cellspacing="0" class="info success" style="font-size: x-large; width: 100%;">
                                <tr>
                                    <td style="width: 120px">Selected</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="lblSelectedCount" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Amount</td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="lblSelectedAmount" runat="server"></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdListItem" runat="server" AllowPaging="true" AllowCustomPaging="true" PagerStyle-Mode="NextPrevNumericAndAdvanced"
        PageSize="18" ShowFooter="True" OnNeedDataSource="grdListItem_NeedDataSource"
        OnItemDataBound="grdListItem_ItemDataBound"
        AutoGenerateColumns="False" GridLines="Horizontal">
        <MasterTableView DataKeyNames="dataKey" GroupLoadMode="Client" CommandItemDisplay="None">
            <Columns>
                <telerik:GridBoundColumn DataField="dataKey" UniqueName="dataKey" SortExpression="dataKey"
                    Visible="false" />
                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px">
                    <HeaderTemplate>
                        <asp:CheckBox ID="ChkHD" OnCheckedChanged="ChkHD_CheckedChanged" runat="server" AutoPostBack="true"></asp:CheckBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="Chk" OnCheckedChanged="Chk_CheckedChanged" runat="server" AutoPostBack="true"></asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="PaymentNo" UniqueName="PaymentNo"
                    SortExpression="PaymentNo" HeaderText="Payment No" HeaderStyle-Width="120px" />
                <telerik:GridBoundColumn DataField="RegistrationNo" UniqueName="RegistrationNo"
                    SortExpression="RegistrationNo" HeaderText="Registration No" HeaderStyle-Width="150px" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="PaymentDate"
                    HeaderText="Payment Date" UniqueName="PaymentDate" SortExpression="PaymentDate" />
                <telerik:GridBoundColumn DataField="PaymentTime" HeaderText="Time"
                    UniqueName="PaymentTime" SortExpression="PaymentTime" HeaderStyle-Width="50px" />
                <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name"
                    UniqueName="PatientName" SortExpression="PatientName" />
                <telerik:GridBoundColumn DataField="PaymentMethodName" UniqueName="PaymentMethodName" SortExpression="PaymentMethodName"
                    HeaderText="Payment Method" />
                <telerik:GridBoundColumn DataField="EDCMachineName" UniqueName="EDCMachineName" SortExpression="EDCMachineName"
                    HeaderText="EDC" />
                <telerik:GridBoundColumn DataField="CardNo" UniqueName="CardNo" SortExpression="CardNo"
                    HeaderText="Card No" />
                <telerik:GridNumericColumn DataField="Amount" HeaderText="Amount" DataType="System.Decimal" DataFormatString="{0:N2}"
                    UniqueName="Amount" SortExpression="Amount">
                    <HeaderStyle HorizontalAlign="Right" Width="100" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridBoundColumn DataField="ApproveByUserID" UniqueName="ApproveByUserID" SortExpression="ApproveByUserID"
                    HeaderText="Cashier" />
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="True">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
    <br /><br />
</asp:Content>
