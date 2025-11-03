<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="PaymentReceiveHistory.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.PaymentReceiveHistory" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function rowReprocess(paymentNo, paymentRefNo) {
                __doPostBack("<%= grdList.UniqueID %>", 'reprocess|' + paymentNo + '|' + paymentRefNo);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                    AllowSorting="true" ShowStatusBar="true" AllowPaging="true" PageSize="10" OnDetailTableDataBind="grdList_DetailTableDataBind">
                    <MasterTableView DataKeyNames="PaymentNo, PaymentReferenceNo" AutoGenerateColumns="false" GroupLoadMode="Client"
                        ClientDataKeyNames="PaymentNo, PaymentReferenceNo">
                        <Columns>
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionCodeName" HeaderText="Payment Code"
                                UniqueName="TransactionCodeName" SortExpression="TransactionCodeName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PaymentType" HeaderText="Payment Type"
                                UniqueName="PaymentType" SortExpression="PaymentType" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PaymentNo" HeaderText="Payment No"
                                UniqueName="PaymentNo" SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PaymentReferenceNo" HeaderText="Payment Ref. No"
                                UniqueName="PaymentReferenceNo" SortExpression="PaymentReferenceNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridDateTimeColumn DataField="PaymentDate" HeaderText="Date" UniqueName="PaymentDate"
                                SortExpression="PaymentDate">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="PaymentTime" HeaderText="Time"
                                UniqueName="PaymentTime" SortExpression="PaymentTime" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="PrintReceiptAsName"
                                HeaderText="Print Receipt As Name" UniqueName="PrintReceiptAsName" SortExpression="PrintReceiptAsName"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                                SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridNumericColumn DataField="Amount" HeaderText="Total Payment" UniqueName="Amount"
                                SortExpression="Amount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:n2}" HeaderStyle-Width="150px" />
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="CreatedBy" HeaderText="Created By"
                                UniqueName="CreatedBy" SortExpression="CreatedBy" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                                HeaderText="Last Updated By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridTemplateColumn UniqueName="ClearTemplateColumn" HeaderText="">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "IsClearVisible").Equals(false) ? string.Empty : 
                                            string.Format("<a href=\"#\" onclick=\"rowReprocess('{0}','{1}'); return false;\">{2}</a>", DataBinder.Eval(Container.DataItem, "PaymentNo"), 
                                            DataBinder.Eval(Container.DataItem, "PaymentReferenceNo"), 
                                            "<img src=\"../../../../Images/reload_refresh_arrow.png\" border=\"0\" title=\"Reprocess\" />") %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <DetailTables>
                            <telerik:GridTableView Name="grdDetail" DataKeyNames="TransactionNo, SequenceNo"
                                AutoGenerateColumns="false">
                                <Columns>
                                    <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="ServiceUnitName" HeaderText="Service Unit"
                                        UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Transaction No"
                                        UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                                        HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
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
                                </Columns>
                            </telerik:GridTableView>
                            <telerik:GridTableView Name="grdDetailIbP" DataKeyNames="IntermBillNo" AutoGenerateColumns="false">
                                <Columns>
                                    <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="IntermBillNo" HeaderText="IntermBill No"
                                        UniqueName="IntermBillNo" SortExpression="IntermBillNo" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridBoundColumn DataField="IntermBillDate" HeaderText="Date" UniqueName="IntermBillDate"
                                        SortExpression="IntermBillDate" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}">
                                        <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridNumericColumn HeaderStyle-Width="130px" DataField="PatientAmount" HeaderText="Patient Amount"
                                        UniqueName="PatientAmount" SortExpression="PatientAmount" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="130px" DataField="GuarantorAmount"
                                        HeaderText="Guarantor Amount" UniqueName="GuarantorAmount" SortExpression="GuarantorAmount"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                    <telerik:GridCalculatedColumn HeaderStyle-Width="130px" HeaderText="Total" UniqueName="Total"
                                        DataType="System.Double" DataFields="PatientAmount,GuarantorAmount" SortExpression="Total"
                                        Expression="{0} + {1}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum" FooterAggregateFormatString="{0:n2}"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                    <telerik:GridTemplateColumn />
                                </Columns>
                            </telerik:GridTableView>
                            <telerik:GridTableView Name="grdDetailIbPG" DataKeyNames="IntermBillNo" AutoGenerateColumns="false">
                                <Columns>
                                    <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="IntermBillNo" HeaderText="IntermBill No"
                                        UniqueName="IntermBillNo" SortExpression="IntermBillNo" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridBoundColumn DataField="IntermBillDate" HeaderText="Date" UniqueName="IntermBillDate"
                                        SortExpression="IntermBillDate" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}">
                                        <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridNumericColumn HeaderStyle-Width="130px" DataField="PatientAmount" HeaderText="Patient Amount"
                                        UniqueName="PatientAmount" SortExpression="PatientAmount" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="130px" DataField="GuarantorAmount"
                                        HeaderText="Guarantor Amount" UniqueName="GuarantorAmount" SortExpression="GuarantorAmount"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                    <telerik:GridCalculatedColumn HeaderStyle-Width="130px" HeaderText="Total" UniqueName="Total"
                                        DataType="System.Double" DataFields="PatientAmount,GuarantorAmount" SortExpression="Total"
                                        Expression="{0} + {1}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum" FooterAggregateFormatString="{0:n2}"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                    <telerik:GridTemplateColumn />
                                </Columns>
                            </telerik:GridTableView>
                        </DetailTables>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
