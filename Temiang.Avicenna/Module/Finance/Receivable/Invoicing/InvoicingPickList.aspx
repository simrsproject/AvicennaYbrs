<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="InvoicingPickList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Receivable.InvoicingPickList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchDischargeDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchGuarantorID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPatientName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPaymentDate" runat="server" Text="Payment Date" />
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtPaymentFromDate" runat="server" Width="100px" />
                                    </td>
                                    <td>
                                        &nbsp;-&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtPaymentToDate" runat="server" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnSearchDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnSearch_Click" ToolTip="Filter By Payment Date" />
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Discharge Date" />
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtDischargeDateFrom" runat="server" Width="100px" />
                                    </td>
                                    <td>
                                        &nbsp;-&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtDischargeDateTo" runat="server" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnSearchDischargeDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnSearch_Click" ToolTip="Filter By Discharge Date" />
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor" />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboGuarantorID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboGuarantorID_ItemDataBound"
                                OnItemsRequested="cboGuarantorID_ItemsRequested">
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnSearchGuarantorID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnSearch_Click" ToolTip="Filter By Guarantor" />
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnFilterRegistrationNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnSearch_Click" ToolTip="Filter By Registration No" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Medical No / Patient Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="150" />
                        </td>
                        <td style="text-align: left;">
                            <asp:ImageButton ID="btnFilterPatientName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnSearch_Click" ToolTip="Filter By Patient Name" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnit" runat="server" Width="300px" ></telerik:RadComboBox>
                        </td>
                        <td style="text-align: left;">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnSearch_Click" ToolTip="Filter By Patient Name" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource" AllowSorting="true"
        AutoGenerateColumns="False" GridLines="None" AllowPaging="False" AllowMultiRowSelection="true">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView DataKeyNames="PaymentNo">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="35px">
                    <HeaderTemplate>
                        <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                            runat="server"></asp:CheckBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="detailChkbox" runat="server"></asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="PaymentNo" HeaderText="Payment No" UniqueName="PaymentNo"
                    SortExpression="PaymentNo" HeaderStyle-Width="120px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="PaymentDate" HeaderText="Date"
                    UniqueName="PaymentDate" SortExpression="PaymentDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                    <HeaderStyle HorizontalAlign="Center" Width="140px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="DischargeDate" HeaderText="Date"
                    UniqueName="DischargeDate" SortExpression="DischargeDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                    SortExpression="MedicalNo">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                    SortExpression="PatientName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                    SortExpression="GuarantorName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn DataField="Amount" HeaderText="Amount" UniqueName="Amount"
                    SortExpression="Amount" DataFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
