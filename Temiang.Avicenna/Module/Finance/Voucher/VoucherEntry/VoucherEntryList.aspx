<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="VoucherEntryList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.VoucherEntryList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function openWinImport(val) {
                var oWnd = $find("<%= winImport.ClientID %>");
                oWnd.setUrl("VoucherImport.aspx");
                oWnd.show();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="500px" Height="350px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" AutoSize="false"
        ReloadOnShow="true" ID="winImport">
    </telerik:RadWindow>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AllowPaging="true" AllowCustomPaging="true" PageSize="15">
        <MasterTableView DataKeyNames="JournalId">
            <Columns>
                <telerik:GridBoundColumn ItemStyle-Wrap="false" HeaderStyle-Width="120px" DataField="TransactionNumber"
                    HeaderText="No" UniqueName="TransactionNumber" SortExpression="TransactionNumber" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                    HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                    DataFormatString="dd/MM/yyyy" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn HeaderStyle-Width="50px" DataField="DateCreated" HeaderText="Time" UniqueName="DateCreated" SortExpression="DateCreated">
                    <ItemTemplate>
                        <%# Convert.ToDateTime(Eval("DateCreated").ToString()).ToString("HH:mm") %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="50px" DataField="RegistrationNo" HeaderText="Time" UniqueName="RegistrationNo" 
                    SortExpression="RegistrationNo" Visible="false">
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Description" AllowSorting="false" HeaderText="Description"
                    UniqueName="Description" />
                <telerik:GridBoundColumn DataField="ReferenceNo" HeaderStyle-Width="130px" AllowSorting="false"
                    HeaderText="Reference No" UniqueName="ReferenceNo" />
                <telerik:GridNumericColumn HeaderStyle-Width="150px" ItemStyle-Wrap="false" DataField="Debit"
                    AllowSorting="false" HeaderText="Debit" UniqueName="Debit" DataFormatString="{0:N2}"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                <telerik:GridNumericColumn HeaderStyle-Width="150px" ItemStyle-Wrap="false" DataField="Credit"
                    AllowSorting="false" HeaderText="Credit" UniqueName="Credit" DataFormatString="{0:N2}"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="EditedBy" AllowSorting="false"
                    HeaderText="Edited By" UniqueName="EditedBy" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="DateEdited" HeaderText="Edited Date"
                    UniqueName="DateEdited" SortExpression="DateEdited" DataFormatString="dd/MM/yyyy" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsPosted" HeaderText="Approved"
                    UniqueName="IsPosted" AllowSorting="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                    UniqueName="IsVoid" AllowSorting="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
