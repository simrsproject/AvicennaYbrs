<%@ Page Title="Import" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="BpjsVerificationResultImport.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Receivable.BpjsVerificationResultImport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">Excel Path File
            </td>
            <td style="width:150px;">
                <asp:FileUpload ID="fileuploadExcel" runat="server" />
            </td>
            <td style="width:200px;">
                <asp:CheckBox ID="chkPrescTrans" runat="server" Text="Prescription Transaction, read from history SEP" Checked="false" /></td>
            <td style="width:30px;">
                <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label" colspan="5">*Excel file must contain columns: SepNo, RequestAmount, ApprovedAmount</td>
        </tr>
    </table>

    <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="false" OnItemDataBound="grdList_ItemDataBound" >
        <MasterTableView DataKeyNames="PaymentNo">
            <Columns>
                <telerik:GridBoundColumn DataField="PaymentNo" HeaderText="Payment No"
                    UniqueName="PaymentNo" SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor Name"
                    UniqueName="GuarantorName" SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="GuarantorCardNo" HeaderText="Guarantor Card No"
                    UniqueName="GuarantorCardNo" SortExpression="GuarantorCardNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="SepNo" HeaderText="Sep No"
                    UniqueName="SepNo" SortExpression="SepNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name"
                    UniqueName="PatientName" SortExpression="PatientName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />

                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RequestAmountSys" HeaderText="Request Amount (from System)"
                    UniqueName="RequestAmountSys" SortExpression="RequestAmountSys" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />

                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RequestAmount" HeaderText="Request Amount (from BPJS)"
                    UniqueName="RequestAmount" SortExpression="RequestAmount" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="ApprovedAmount" HeaderText="Approved Amount"
                    UniqueName="ApprovedAmount" SortExpression="ApprovedAmount" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>

</asp:Content>
