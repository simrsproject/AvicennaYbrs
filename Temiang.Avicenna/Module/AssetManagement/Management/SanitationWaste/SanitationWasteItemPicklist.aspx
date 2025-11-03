<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="SanitationWasteItemPicklist.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Management.SanitationWasteItemPicklist" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Received Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtFromTransactionDate" runat="server" Width="100px">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td></td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtToTransactionDate" runat="server" Width="100px">
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20" />
                        <asp:ImageButton ID="btnSearchDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                            OnClick="btnFilter_Click" />
                        <td />
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="False" GridLines="None"
                    OnNeedDataSource="grdList_NeedDataSource" OnPageIndexChanged="grdList_PageIndexChanged">
                    <MasterTableView DataKeyNames="TransactionNo,SRWasteType" ClientDataKeyNames="TransactionNo,SRWasteType">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="30px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                        runat="server" Checked="false"></asp:CheckBox>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="detailChkbox" runat="server" Checked="False"></asp:CheckBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate" HeaderText="Date"
                                UniqueName="TransactionDate" SortExpression="TransactionDate" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="TransactionNo" HeaderText="Received No"
                                UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRWasteType" HeaderText="SRWasteType"
                                UniqueName="SRWasteType" SortExpression="SRWasteType" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Left" Visible="false" />
                            <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="WasteTypeName" HeaderText="Waste Type"
                                UniqueName="WasteTypeName" SortExpression="WasteTypeName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridNumericColumn HeaderStyle-Width="110px" DataField="Qty" HeaderText="Qty Received (Kg)"
                                UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:n2}" />
                            <telerik:GridTemplateColumn HeaderStyle-Width="110px" HeaderText="Qty Disposal (Kg)"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txtQtyDisposal" runat="server" NumberFormat-DecimalDigits="2"
                                        Value='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "QtyDisposal")) %>'
                                        MinValue="0"
                                        MaxValue='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "QtyDisposal")) %>'
                                        Width="90px">
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn />
                        </Columns>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
