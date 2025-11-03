<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="VoucherCodeList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.VoucherCodeList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="JournalCodeId, JournalCode">
            <Columns>
                <telerik:GridBoundColumn DataField="JournalCode" HeaderText="Journal Code" UniqueName="JournalCode"
                    SortExpression="JournalCode" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />
                <telerik:GridBoundColumn DataField="Description" HeaderText="Description" UniqueName="Description"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="NumberFormat" HeaderText="Number Format" UniqueName="NumberFormat"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="140px" />
                <telerik:GridBoundColumn DataField="CurrentNumber" HeaderText="Current Number" UniqueName="CurrentNumber"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="140px" />
                <telerik:GridBoundColumn DataField="NumberSeed" HeaderText="Number Seed" UniqueName="NumberSeed"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="140px" />
                <telerik:GridBoundColumn DataField="BankName" HeaderText="Bank" UniqueName="BankName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="240px" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Cash Type" UniqueName="ItemName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="140px" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsEnabled" HeaderText="Enabled"
                    UniqueName="IsEnabled" SortExpression="IsEnabled" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsAutoNumber" HeaderText="Auto Number"
                    UniqueName="IsAutoNumber" SortExpression="IsAutoNumber" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsBKU" HeaderText="BKU"
                    UniqueName="IsBKU" SortExpression="IsBKU" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
