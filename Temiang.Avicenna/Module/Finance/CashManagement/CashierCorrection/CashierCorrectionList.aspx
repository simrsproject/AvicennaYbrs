<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="CashierCorrectionList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.CashManagement.CashierCorrectionList"
    Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView DataKeyNames="PaymentCorrectionNo">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="PaymentCorrectionNo" HeaderText="Payment Correction No"
                    UniqueName="PaymentCorrectionNo" SortExpression="PaymentCorrectionNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="PaymentCorrectionDate" HeaderText="Correction Date"
                    UniqueName="PaymentCorrectionDate" SortExpression="PaymentCorrectionDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="PaymentCorrectionNo, PaymentNo, SequenceNo" Name="grdDetail" AutoGenerateColumns="False"
                    AllowPaging="true" PageSize="10">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PaymentNo" HeaderText="Payment No"
                            UniqueName="PaymentNo" SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="SequenceNo" HeaderText="Seq No"
                            UniqueName="SequenceNo" SortExpression="SequenceNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />    
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="RegistrationNo" HeaderText="Registration No"
                            UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />   
                        <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name"
                            UniqueName="PatientName" SortExpression="PatientName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />  
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="PaymentMethodOName" HeaderText="Payment Method"
                            UniqueName="PaymentMethodOName" SortExpression="PaymentMethodOName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" /> 
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="CardProviderOName" HeaderText="Card Provider"
                            UniqueName="CardProviderOName" SortExpression="CardProviderOName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="CardTypeOName" HeaderText="Card Type"
                            UniqueName="CardTypeOName" SortExpression="CardTypeOName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />   
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="EDCMachineOName" HeaderText="EDC"
                            UniqueName="EDCMachineOName" SortExpression="EDCMachineOName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />   
                            
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="CardProviderCName" HeaderText="Card Provider Correction"
                            UniqueName="CardProviderCName" SortExpression="CardProviderCName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="CardTypeCName" HeaderText="Card Type Correction"
                            UniqueName="CardTypeCName" SortExpression="CardTypeCName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />   
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="EDCMachineCName" HeaderText="EDC Correction"
                            UniqueName="EDCMachineCName" SortExpression="EDCMachineCName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Amount" HeaderText="Amount"
                            UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
