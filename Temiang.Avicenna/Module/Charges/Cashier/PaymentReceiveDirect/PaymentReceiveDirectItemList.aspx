<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="PaymentReceiveDirectItemList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.PaymentReceiveDirectItemList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script language="javascript" type="text/javascript">
            function RowSelected(sender, args) {
                __doPostBack("<%=grdDetail.UniqueID%>", "rebind:" + args.getDataKeyValue("TransactionNo"));
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdDetail">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Transaction List">
        <telerik:RadGrid ID="grdOrderList" runat="server" ShowStatusBar="true" OnNeedDataSource="grdOrderList_NeedDataSource"
            AllowPaging="True" AutoGenerateColumns="False" AllowSorting="true">
            <PagerStyle Mode="NextPrevAndNumeric" />
            <MasterTableView DataKeyNames="TransactionNo" ClientDataKeyNames="TransactionNo"
                PageSize="10">
                <Columns>
                    <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Transaction No"
                        UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                        HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                        SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="left" />
                </Columns>
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
                <ClientEvents OnRowSelected="RowSelected" />
            </ClientSettings>
        </telerik:RadGrid>
    </cc:CollapsePanel>
    <asp:Panel ID="Panel1" runat="server" Height="4px">
    </asp:Panel>
    <cc:CollapsePanel ID="CollapsePanel2" runat="server" Title="Transaction Item">
        <telerik:RadGrid ID="grdDetail" runat="server" AutoGenerateColumns="False" GridLines="None"
            OnNeedDataSource="grdDetail_NeedDataSource">
            <MasterTableView CommandItemDisplay="None" DataKeyNames="SequenceNo">
                <Columns>
                    <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Transaction No"
                        UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" Visible="false" />
                    <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                        HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false" />
                    <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                        SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="left"
                        Visible="false" />
                    <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="SequenceNo" HeaderText="SequenceNo"
                        UniqueName="SequenceNo" SortExpression="SequenceNo" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" Visible="false" />
                    <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                        SortExpression="ItemID" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                        Visible="true" HeaderStyle-Width="120px" />
                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                        SortExpression="ItemName" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                        Aggregate="Count" FooterAggregateFormatString="Total :" FooterStyle-HorizontalAlign="Right" />
                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Qty" HeaderText="Qty"
                        UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                        DataFormatString="{0:n2}" />
                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                        UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Total" UniqueName="Total"
                        DataType="System.Double" DataFields="Qty,Price" SortExpression="Total" Expression="{0} * {1}"
                        FooterStyle-HorizontalAlign="Right" Aggregate="Sum" FooterAggregateFormatString="{0:n2}"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsBillProceed" HeaderText="Proceed"
                        UniqueName="IsBillProceed" SortExpression="IsBillProceed" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" />
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </cc:CollapsePanel>
</asp:Content>
