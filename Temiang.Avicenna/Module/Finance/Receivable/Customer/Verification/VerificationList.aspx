<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="VerificationList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Receivable.Customer.VerificationList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function OnClientButtonClicking(sender, args) {
                __doPostBack("<%= grdList.UniqueID %>", 'process');
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchInvoice">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%">
                    <table width="100%" id="pnlFilterDate" runat="server">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblInvoiceDueDate" runat="server" Text="Invoice Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtFromDate" runat="server" Width="100px">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td>
                                            &nbsp;-&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtToDate" runat="server" Width="100px">
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchInvoice" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchInvoice_Click" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%">
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Process" Value="process" ImageUrl="~/Images/Toolbar/refresh16.png"
                HoveredImageUrl="~/Images/Toolbar/refresh16_h.png" DisabledImageUrl="~/Images/Toolbar/refresh16_d.png" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="false" OnNeedDataSource="grdList_NeedDataSource"
        AllowPaging="True" PageSize="15">
        <MasterTableView DataKeyNames="InvoiceNo" GroupLoadMode="client">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="CustomerName" HeaderText="Customer "></telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="CustomerName" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridTemplateColumn UniqueName="checkbox" HeaderText="Process">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="processChkBox" runat="server" Checked="false"></asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="datepicker" HeaderText="Aging Date">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <telerik:RadDatePicker ID="txtAgingDate" runat="server" Width="100px" SelectedDate='<%# DataBinder.Eval(Container.DataItem, "AgingDate") %>'>
                        </telerik:RadDatePicker>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="InvoiceNo" HeaderText="Invoice No"
                    UniqueName="InvoiceNo" SortExpression="InvoiceNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="InvoiceDate" HeaderText="Invoice Date"
                    UniqueName="InvoiceDate" SortExpression="InvoiceDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="InvoiceDueDate" HeaderText="Due Date"
                    UniqueName="InvoiceDueDate" SortExpression="InvoiceDueDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="CustomerName" HeaderText="Customer"
                    UniqueName="CustomerName" SortExpression="CustomerName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="TotalAmount" HeaderText="Total Amount"
                    UniqueName="TotalAmount" SortExpression="TotalAmount" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn DataField="InvoiceNotes" HeaderText="Notes" UniqueName="InvoiceNotes"
                    SortExpression="InvoiceNotes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="True">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
