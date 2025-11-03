<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="PaymentReceiptPickList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.Cashier.PaymentReceiptPickList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script language="javascript" type="text/javascript">
            function RowSelected(sender, args) {
                __doPostBack("<%= grdDetail.UniqueID %>", args.getDataKeyValue("PaymentNo"));
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%">
        <tr>
            <td>
                <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Payment Outstanding">
                    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" AllowPaging="True"
                        AllowMultiRowSelection="true">
                        <HeaderContextMenu>
                        </HeaderContextMenu>
                        <MasterTableView DataKeyNames="PaymentNo" ClientDataKeyNames="PaymentNo">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                            runat="server"></asp:CheckBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="detailChkbox" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="PaymentNo" HeaderText="Payment No" UniqueName="PaymentNo"
                                    SortExpression="PaymentNo" HeaderStyle-Width="200px">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="PaymentDate" HeaderText="Payment Date"
                                    UniqueName="PaymentDate" SortExpression="PaymentDate" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="PaymentTime" HeaderText="Time" UniqueName="PaymentTime"
                                    SortExpression="PaymentTime" HeaderStyle-Width="50px">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="PrintReceiptAsName"
                                    HeaderText="Print Receipt As Name" UniqueName="PrintReceiptAsName" SortExpression="PrintReceiptAsName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn DataField="Amount" HeaderText="Amount" UniqueName="Amount"
                                    SortExpression="Amount" DataFormatString="{0:n2}">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridNumericColumn>
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
                </cc:CollapsePanel>
                <cc:CollapsePanel runat="server" ID="CollapsePanel2" Title="Detail Item">
                    <telerik:RadGrid ID="grdDetail" runat="server" AutoGenerateColumns="False" GridLines="None"
                        OnNeedDataSource="grdDetail_NeedDataSource" AllowPaging="True">
                        <PagerStyle Mode="NextPrevAndNumeric" />
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="SequenceNo">
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="refToAppStandardReferenceItem_PaymentType"
                                    HeaderText="Payment Type" UniqueName="refToAppStandardReferenceItem_PaymentType"
                                    SortExpression="refToAppStandardReferenceItem_PaymentType" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="refToAppStandardReferenceItem_PaymentMethod"
                                    HeaderText="Payment Method" UniqueName="refToAppStandardReferenceItem_PaymentMethod"
                                    SortExpression="refToAppStandardReferenceItem_PaymentMethod" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Amount" HeaderText="Amount"
                                    UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </cc:CollapsePanel>
            </td>
        </tr>
    </table>
</asp:Content>
