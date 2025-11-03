<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="IntermBillReturnList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.IntermBillReturnList" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script language="javascript" type="text/javascript">
            function RowSelected(sender, args) {
                __doPostBack("<%=grdIntermBill.UniqueID%>", "rebind:" + args.getDataKeyValue("PaymentNo"));
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdIntermBill" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdIntermBill">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdIntermBill" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td align="center">
                <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Payment List">
                    <telerik:RadGrid ID="grdList" runat="server" ShowStatusBar="true" OnNeedDataSource="grdList_NeedDataSource"
                        AllowPaging="True" AutoGenerateColumns="False" AllowSorting="true">
                        <PagerStyle Mode="NextPrevAndNumeric" />
                        <MasterTableView DataKeyNames="PaymentNo" ClientDataKeyNames="PaymentNo" PageSize="10">
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PaymentNo" HeaderText="Payment No"
                                    UniqueName="PaymentNo" SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="PaymentDate" HeaderText="Date"
                                    UniqueName="PaymentDate" SortExpression="PaymentDate" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Amount" HeaderText="Amount"
                                    UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridBoundColumn DataField="PrintReceiptAsName" HeaderText="Print Receipt As Name"
                                    UniqueName="PrintReceiptAsName" SortExpression="PrintReceiptAsName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" />
                            <ClientEvents OnRowSelected="RowSelected" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </cc:CollapsePanel>
                <cc:CollapsePanel ID="CollapsePanel2" runat="server" Title="Transaction Item">
                    <telerik:RadGrid ID="grdIntermBill" runat="server" OnNeedDataSource="grdIntermBill_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" ShowFooter="true" AllowPaging="false">
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="IntermBillNo">
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="IntermBillNo" HeaderText="IntermBill No"
                                    UniqueName="IntermBillNo" SortExpression="IntermBillNo" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="IntermBillDate" HeaderText="Date"
                                    UniqueName="IntermBillDate" SortExpression="IntermBillDate" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="StartDate" HeaderText="Start Date"
                                    UniqueName="StartDate" SortExpression="StartDate" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="EndDate" HeaderText="End Date"
                                    UniqueName="EndDate" SortExpression="EndDate" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="PatientAmount" HeaderText="Patient Amount"
                                    UniqueName="PatientAmount" SortExpression="PatientAmount" HeaderStyle-HorizontalAlign="Center"
                                    FooterStyle-HorizontalAlign="Right" Aggregate="Sum" FooterAggregateFormatString="{0:n2}"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="GuarantorAmount"
                                    HeaderText="Guarantor Amount" UniqueName="GuarantorAmount" SortExpression="GuarantorAmount"
                                    HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right" Aggregate="Sum"
                                    FooterAggregateFormatString="{0:n2}" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </cc:CollapsePanel>
            </td>
        </tr>
    </table>
</asp:Content>
