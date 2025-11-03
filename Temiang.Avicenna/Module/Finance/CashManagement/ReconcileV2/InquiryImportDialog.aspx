<%@ Page Title="Import" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="InquiryImportDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.CashManagement.ReconcileV2.InquiryImportDialog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">Excel Path File
            </td>
            <td style="width:150px;">
                <asp:FileUpload ID="fileuploadExcel" runat="server" />
            </td>
            <td style="width:30px;">
                <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label" colspan="4">*Excel file must contain columns: Tgl., Rincian Transaksi, No. Referensi, Debit, Kredit, Saldo</td>
        </tr>
    </table>

    <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="false" >
        <MasterTableView DataKeyNames="TransactionID">
            <Columns>
                                        <telerik:GridBoundColumn DataField="TransactionDateTime" HeaderText="Date"
                                            DataFormatString="{0:MM/dd/yyyy}" DataType="System.DateTime" 
                                            SortExpression="TransactionDateTime" UniqueName="TransactionDateTime" >
                                            <HeaderStyle HorizontalAlign="Left" Width="130" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Description" HeaderText="Description" SortExpression="Description"
                                            UniqueName="Description">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ReferenceNo" HeaderText="ReferenceNo" SortExpression="ReferenceNo"
                                            UniqueName="ReferenceNo">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridNumericColumn DataField="Debit" HeaderText="Debit" DataType="System.Decimal" DataFormatString="{0:N2}"
                                            UniqueName="Debit" SortExpression="Debit" >
                                            <HeaderStyle HorizontalAlign="Right" Width="150" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridNumericColumn DataField="Credit" HeaderText="Credit" DataType="System.Decimal" DataFormatString="{0:N2}"
                                            UniqueName="Credit" SortExpression="Credit" >
                                            <HeaderStyle HorizontalAlign="Right" Width="150" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridNumericColumn DataField="Balance" HeaderText="Credit" DataType="System.Decimal" DataFormatString="{0:N2}"
                                            UniqueName="Balance" SortExpression="Balance" >
                                            <HeaderStyle HorizontalAlign="Right" Width="150" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridBoundColumn DataField="CreatedDateTime" HeaderText="Date"
                                            DataFormatString="{0:MM/dd/yyyy}" DataType="System.DateTime" 
                                            SortExpression="CreatedDateTime" UniqueName="CreatedDateTime" >
                                            <HeaderStyle HorizontalAlign="Left" Width="130" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
        </MasterTableView>
    </telerik:RadGrid>

</asp:Content>
