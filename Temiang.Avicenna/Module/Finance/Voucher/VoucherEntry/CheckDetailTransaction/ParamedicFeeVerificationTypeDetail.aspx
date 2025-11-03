<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParamedicFeeVerificationTypeDetail.aspx.cs" MasterPageFile="~/MasterPage/MasterDialog.Master"
Inherits="Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.CheckDetailTransaction.ParamedicFeeVerificationTypeDetail" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpagDetail">
        <Tabs>
            <telerik:RadTab runat="server" Text="Verification" Selected="True" PageViewID="pgLeft" />
            <telerik:RadTab runat="server" Text="Transaction" PageViewID="pgRight" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpagDetail" runat="server" BorderStyle="Solid" SelectedIndex="0"
        BorderColor="Gray">
        <telerik:RadPageView ID="pgLeft" runat="server">
            <telerik:RadGrid ID="grdDetail" runat="server" OnNeedDataSource="grdDetail_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" ShowFooter="true" AllowPaging="true"
                PageSize="15">
                <HeaderContextMenu>
                    
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo" GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Transaction No"
                            UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="VerificationDate" HeaderText="Verification Date"
                            UniqueName="VerificationDate" SortExpression="VerificationDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn  DataField="Paramedic" HeaderText="Phycisian"
                            UniqueName="Paramedic" SortExpression="Paramedic" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Left"/>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="StartDate" HeaderText="Verification Start Date"
                            UniqueName="StartDate" SortExpression="StartDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="EndDate" HeaderText="Verification End Date"
                            UniqueName="EndDate" SortExpression="EndDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center"  />
                        <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="VerificationAmount" HeaderText="Amount"
                            UniqueName="VerificationAmount" SortExpression="VerificationAmount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"  FooterStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="TaxAmount" HeaderText="Tax Amount"
                            UniqueName="TaxAmount" SortExpression="TaxAmount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"   />
                        
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                    
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgRight" runat="server">
            <telerik:RadTabStrip ID="tabMoreDetail" runat="server" MultiPageID="mpagMoreDetail">
                <Tabs>
                    <telerik:RadTab runat="server" Text="Transaction" Selected="True" PageViewID="pgMoreLeft" />
                    <telerik:RadTab runat="server" Text="Additional / Deduction" PageViewID="pgMoreRight" />
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="mpagMoreDetail" runat="server" BorderStyle="Solid" SelectedIndex="0"
                BorderColor="Gray">
                <telerik:RadPageView ID="pgMoreLeft" runat="server">
                    <telerik:RadGrid ID="grdFeeCalculation" runat="server" OnNeedDataSource="grdFeeCalculation_NeedDataSource"
                        AutoGenerateColumns="False" ShowFooter="true" GridLines="None">
                        <HeaderContextMenu>
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, SequenceNo, TariffComponentID"
                            FilterExpression="VerificationNo IS NOT NULL">
                            <Columns>
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
                                <telerik:GridBoundColumn DataField="PaymentMethod" HeaderText="Payment Method" UniqueName="PaymentMethod"
                                    SortExpression="PaymentMethod">
                                    <HeaderStyle HorizontalAlign="Left" Width="140px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TransactionNo" HeaderText="Transaction No" UniqueName="TransactionNo"
                                    SortExpression="TransactionNo" Visible="false">
                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                    SortExpression="ItemName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PriceItem" HeaderText="Price"
                                        UniqueName="PriceItem" SortExpression="PriceItem" DataFormatString="{0:n2}">
                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Price" HeaderText="Price"
                                        UniqueName="Price" SortExpression="Price" DataFormatString="{0:n2}" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DiscountItem" HeaderText="Discount"
                                        UniqueName="DiscountItem" SortExpression="DiscountItem" DataFormatString="{0:n2}">
                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Discount" HeaderText="Discount"
                                        UniqueName="Discount" SortExpression="Discount" DataFormatString="{0:n2}" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CalculatedAmount" HeaderText="Share"
                                        UniqueName="CalculatedAmount" SortExpression="CalculatedAmount" DataFormatString="{0:n0}%">
                                        <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FeeAmount" HeaderText="Gross" UniqueName="FeeAmount"
                                    SortExpression="FeeAmount" DataFormatString="{0:n2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                                    FooterAggregateFormatString="{0:n2}">
                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SumDeductionAmount" HeaderText="Deduction" UniqueName="SumDeductionAmount"
                                    SortExpression="SumDeductionAmount" DataFormatString="{0:n2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                                    FooterAggregateFormatString="{0:n2}">
                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Nett" HeaderText="Physician Fee" UniqueName="Nett"
                                    SortExpression="Nett" DataFormatString="{0:n2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                                    FooterAggregateFormatString="{0:n2}">
                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="VerificationNo" HeaderText="VerificationNo" UniqueName="VerificationNo"
                                    SortExpression="VerificationNo" Visible="false">
                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="false">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="false" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView ID="pgMoreRight" runat="server">
                    <telerik:RadGrid ID="grdAddDeduc" runat="server" OnNeedDataSource="grdAddDeduc_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None">
                        <HeaderContextMenu>
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo" FilterExpression="VerificationNo IS NOT NULL">
                            <Columns>
                                <telerik:GridBoundColumn DataField="TransactionNo" HeaderText="Transaction No" UniqueName="TransactionNo"
                                    SortExpression="TransactionNo">
                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                                    HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="ParamedicFeeAdjustType" HeaderText="Type" UniqueName="ParamedicFeeAdjustType"
                                    SortExpression="ParamedicFeeAdjustType">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Amount" HeaderText="Amount" UniqueName="Amount"
                                    SortExpression="Amount" DataFormatString="{0:n2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                                    FooterAggregateFormatString="{0:n2}">
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="VerificationNo" HeaderText="VerificationNo" UniqueName="VerificationNo"
                                    SortExpression="VerificationNo" Visible="false">
                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="false">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="false" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
            
            
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    
                
</asp:Content>
