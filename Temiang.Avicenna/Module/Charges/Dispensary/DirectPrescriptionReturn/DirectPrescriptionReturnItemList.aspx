<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="DirectPrescriptionReturnItemList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.DirectPrescriptionReturnItemList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script language="javascript" type="text/javascript">
            function RowSelected(sender, args) {
                __doPostBack("<%= grdTransPrescriptionItem.UniqueID %>", args.getDataKeyValue("PrescriptionNo"));
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransPrescription" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransPrescriptionItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransPrescription" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransPrescriptionItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransPrescription" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransPrescriptionItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPatientName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransPrescription" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransPrescriptionItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdTransPrescription">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransPrescription" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransPrescriptionItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdTransPrescriptionItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransPrescriptionItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel3" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPrescriptionDate" runat="server" Text="Prescription Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtPrescriptionDate" runat="server" Width="100px">
                                </telerik:RadDatePicker>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterPrescriptionDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Filter By Prescription Date" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPrescriptionNo" runat="server" Text="Prescription No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPrescriptionNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterPrescriptionNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Filter By Prescription No" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%">
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
                                    OnClick="btnFilter_Click" ToolTip="Filter By Registration No" />
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
                                    OnClick="btnFilter_Click" ToolTip="Filter By Patient Name" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdTransPrescription" runat="server" 
        OnNeedDataSource="grdTransPrescription_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" AllowPaging="True" PageSize="7">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView DataKeyNames="PrescriptionNo" ClientDataKeyNames="PrescriptionNo">
            <Columns>
                <telerik:GridBoundColumn DataField="PrescriptionNo" HeaderText="Prescription No"
                    UniqueName="PrescriptionNo" SortExpression="PrescriptionNo">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn DataField="PrescriptionDate" HeaderText="Prescription Date"
                    UniqueName="PrescriptionDate" SortExpression="PrescriptionDate" DataFormatString="{0:dd/MM/yyyy}">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridNumericColumn>
                <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                    <HeaderStyle HorizontalAlign="Center" Width="140px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                    SortExpression="MedicalNo">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                    SortExpression="PatientName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridNumericColumn>
                <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                    SortExpression="GuarantorName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                    SortExpression="ServiceUnitName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsClosed" HeaderText="Closed"
                    UniqueName="IsClosed" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
            <ClientEvents OnRowSelected="RowSelected" />
        </ClientSettings>
    </telerik:RadGrid>
    <asp:Panel runat="server" Height="2px" />
    <telerik:RadGrid ID="grdTransPrescriptionItem" runat="server" 
        OnNeedDataSource="grdTransPrescriptionItem_NeedDataSource"
        OnItemDataBound="grdTransPrescriptionItem_ItemDataBound"
        AutoGenerateColumns="False" GridLines="None" AllowPaging="false" AllowMultiRowSelection="true">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView DataKeyNames="PrescriptionNo, SequenceNo">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="45px">
                    <HeaderTemplate>
                        <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                            runat="server"></asp:CheckBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="detailChkbox" runat="server"></asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="PrescriptionNo" UniqueName="PrescriptionNo" SortExpression="PrescriptionNo"
                    Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SequenceNo" UniqueName="SequenceNo" SortExpression="SequenceNo"
                    Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ItemID" UniqueName="ItemID" SortExpression="ItemID"
                    Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="TemplateItemName">
                    <ItemTemplate>
                        <asp:Label ID="lblItemName" runat="server" Text='<%# GetItemName(DataBinder.Eval(Container.DataItem, "IsRFlag"), DataBinder.Eval(Container.DataItem, "ItemName")) %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="TemplateColumn1" HeaderStyle-Width="75px"
                    ItemStyle-HorizontalAlign="Center" HeaderText="Qty" HeaderStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtQty" runat="server" NumberFormat-DecimalDigits="2"
                            Value='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "ResultQty")) %>'
                            MaxValue='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "ResultQty")) %>'
                            Width="60px">
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="SRItemUnit" HeaderText="Unit" UniqueName="SRItemUnit"
                    SortExpression="SRItemUnit">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn DataField="Price" HeaderText="Price" UniqueName="Price"
                    SortExpression="Price" DataFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="DiscountAmount" HeaderText="Discount" UniqueName="DiscountAmount"
                    SortExpression="DiscountAmount" DataFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="Total" HeaderText="Total" UniqueName="Total"
                    SortExpression="Total" DataFormatString="{0:n2}" Aggregate="Sum" FooterAggregateFormatString="{0:n2}"
                    Visible="false">
                    <FooterStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn2" HeaderStyle-Width="50px"
                    HeaderText="Admin" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIsUsingAdmin" runat="server" Checked="True"></asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
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
