<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" 
	CodeBehind="CashEntryList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.CashManagement.CashEntryV2.CashEntryList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" AllowPaging="true" AllowCustomPaging="true" PageSize="50" >
        <MasterTableView DataKeyNames="TransactionId">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="JournalNumber" AllowSorting="false" HeaderText="Journal#" UniqueName="JournalNumber" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate" HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate" DataFormatString="dd/MM/yyyy" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionType" AllowSorting="false" HeaderText="Transaction Type" UniqueName="TransactionType" />                
                <telerik:GridBoundColumn DataField="AccountName" AllowSorting="false" HeaderText="Account" UniqueName="AccountName" />
                <telerik:GridBoundColumn DataField="Description" AllowSorting="false" HeaderText="Description" UniqueName="Description" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ChequeNumber" AllowSorting="false" HeaderText="Cheque/Giro Number" UniqueName="ChequeNumber" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="DueDate" HeaderText="Due Date" UniqueName="DueDate" AllowSorting="false" DataFormatString="dd/MM/yyyy" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridNumericColumn HeaderStyle-Width="150px" ItemStyle-Wrap="false" DataField="TotalAmount" AllowSorting="false" HeaderText="Amount" UniqueName="TotalAmount" DataFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"/>
                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="EditedBy" AllowSorting="false" HeaderText="Edited By" UniqueName="EditedBy" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="DateEdited" HeaderText="Edited Date" UniqueName="DateEdited" AllowSorting="false" DataFormatString="dd/MM/yyyy" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsPosted" HeaderText="Approved" UniqueName="IsPosted" AllowSorting="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void" UniqueName="IsVoid" AllowSorting="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>