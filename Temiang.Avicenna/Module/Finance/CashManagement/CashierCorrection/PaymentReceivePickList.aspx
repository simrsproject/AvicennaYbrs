<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PaymentReceivePickList.aspx.cs" 
    Inherits="Temiang.Avicenna.Module.Finance.CashManagement.PaymentReceivePickList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPaymentNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPatientName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdListItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" />
                    <telerik:AjaxUpdatedControl ControlID="lblSelectedCount" />
                    <telerik:AjaxUpdatedControl ControlID="lblSelectedAmount" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width:50%; vertical-align:top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Payment Date
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtPaymentDateFrom" runat="server" Width="105px">
                                            <DateInput ID="DateInput1" runat="server" DateFormat="dd/MM/yyyy">
                                            </DateInput>
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtPaymentDateTo" runat="server" Width="105px">
                                            <DateInput ID="DateInput2" runat="server" DateFormat="dd/MM/yyyy">
                                            </DateInput>
                                        </telerik:RadDatePicker>
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
                        <td class="label">
                            Amount
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtAmount" runat="server" Width="340px" MaxLength="150"></telerik:RadNumericTextBox>
                        </td>
                        <td style="width: 20px" />
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            EDC Machine
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboEDCMachineID" runat="server" Width="344px" ></telerik:RadComboBox>
                        </td>
                        <td style="width: 20px" />
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            PaymentNo / Card No
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPaymentNo" runat="server" Width="340px" MaxLength="150"></telerik:RadTextBox>
                        </td>
                        <td style="width: 20px" />
                            <asp:ImageButton ID="btnFilterPaymentNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            RegistrationNo
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="340px" MaxLength="150"></telerik:RadTextBox>
                        </td>
                        <td style="width: 20px" />
                            <asp:ImageButton ID="btnFilterRegistrationNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Patient Name
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="340px" MaxLength="150"></telerik:RadTextBox>
                        </td>
                        <td style="width: 20px" />
                            <asp:ImageButton ID="btnFilterPatientName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        <td />
                    </tr>
                </table>
            </td>
            <td style="width:50%; vertical-align:top;">
                <table cellpadding="0" cellspacing="0" class="info success" style="font-size:x-large; width:100%;">
                    <tr>
                        <td style="width:120px">Selected</td>
                        <td>:</td>
                        <td><asp:Label ID="lblSelectedCount" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Amount</td>
                        <td>:</td>
                        <td><asp:Label ID="lblSelectedAmount" runat="server"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdListItem" runat="server" AllowPaging="true" AllowCustomPaging="true"
        PageSize="18" ShowFooter="True" OnNeedDataSource="grdListItem_NeedDataSource"
        OnItemDataBound="grdListItem_ItemDataBound"
        AutoGenerateColumns="False" GridLines="Horizontal">
        <MasterTableView DataKeyNames="dataKey" GroupLoadMode="Client" CommandItemDisplay="None">
            <Columns>
                <telerik:GridBoundColumn DataField="dataKey" UniqueName="dataKey" SortExpression="dataKey"
                    Visible="false" />
                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px">
                    <ItemTemplate>
                        <asp:CheckBox ID="Chk" OnCheckedChanged="Chk_CheckedChanged" runat="server" AutoPostBack="true">
                        </asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="PaymentNo" UniqueName="PaymentNo"
                    SortExpression="PaymentNo" HeaderText="Payment No" HeaderStyle-Width="120px" />
                <telerik:GridBoundColumn DataField="RegistrationNo" UniqueName="RegistrationNo"
                    SortExpression="RegistrationNo" HeaderText="Registration No" HeaderStyle-Width="150px" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="PaymentDate"
                    HeaderText="Payment Date" UniqueName="PaymentDate" SortExpression="PaymentDate" />
                <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name"
                    UniqueName="PatientName" SortExpression="PatientName" />
                <telerik:GridBoundColumn DataField="PaymentMethodName" UniqueName="PaymentMethodName" SortExpression="PaymentMethodName"
                    HeaderText="Payment Method" />
                <telerik:GridBoundColumn DataField="CardProviderName" UniqueName="CardProviderName" SortExpression="CardProviderName"
                    HeaderText="Card Provider" />
                <telerik:GridBoundColumn DataField="CardTypeName" UniqueName="CardTypeName" SortExpression="CardTypeName"
                    HeaderText="Card Type" />
                <telerik:GridBoundColumn DataField="EDCMachineName" UniqueName="EDCMachineName" SortExpression="EDCMachineName"
                    HeaderText="EDC" />
                <telerik:GridBoundColumn DataField="CardNo" UniqueName="CardNo" SortExpression="CardNo"
                    HeaderText="Card No" />
                <telerik:GridNumericColumn DataField="Amount" HeaderText="Amount" DataType="System.Decimal" DataFormatString="{0:N2}"
                    UniqueName="Amount" SortExpression="Amount" >
                    <HeaderStyle HorizontalAlign="Right" Width="100" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="True">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
