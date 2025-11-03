<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="PatientDocumentBatchUpload.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.PatientDocumentBatchUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" ID="pnlUpload">
        <table>
            <tr>
                <td class="label">
                    <asp:Label ID="Label4" runat="server" Text="Select File"></asp:Label>
                </td>
                <td class="entry">
                    <telerik:RadAsyncUpload ID="uplFile" runat="server" MultipleFileSelection="Automatic" AllowedFileExtensions=".jpeg,.jpg,.png,.pdf,.dcm" HideFileInput="true"
                        Width="100%">
                    </telerik:RadAsyncUpload>
                    <telerik:RadProgressArea runat="server" ID="pProgressArea" />
                    <span class="allowed-attachments">Select files to upload <span class="allowed-attachments-list">(<%= String.Join( ",", uplFile.AllowedFileExtensions ) %>)</span>
                    </span>
                </td>
                <td></td>
            </tr>
            <tr style="display:none;">
                <td class="label"></td>
                <td class="entry">
                    <asp:CheckBox runat="server" ID="chkIsUseOldMedicalNo" Text="File name using old Medical Record No" /></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="3"><br /><strong>* File Name Format:</strong> DocumentDate_MedicalRecordNo_SeqNo_DocumentName
                    <ul>
                        <li><strong>DocumentDate:</strong> use format ddmmyyyy or ddmmyy</li>
                        <li><strong>MedicalRecordNo:</strong> Medical Record No Patient</li>
                        <li><strong>SeqNo:</strong> Document Sequence No (optional) </li>
                        <li><strong>DocumentName</strong> (optional) </li>
                    </ul>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlResult" Visible="false">
        <telerik:RadGrid ID="grdResult" runat="server"
            GridLines="None" AutoGenerateColumns="false">
            <MasterTableView>
                <Columns>
                    <telerik:GridBoundColumn DataField="FileName" HeaderText="File" UniqueName="FileName"
                        SortExpression="FileName">
                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                        SortExpression="PatientName">
                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="MedicalNo" UniqueName="MedicalNo"
                        SortExpression="MedicalNo">
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No" UniqueName="RegistrationNo"
                        SortExpression="RegistrationNo">
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="RegistrationDate" HeaderText="Reg. Date" UniqueName="RegistrationDate"
                        SortExpression="RegistrationDate">
                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridDateTimeColumn>
                    <telerik:GridDateTimeColumn DataField="FileDate" HeaderText="File. Date" UniqueName="FileDate"
                        SortExpression="FileDate">
                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn DataField="ErrorMessage" HeaderText="ErrorMessage" UniqueName="ErrorMessage"
                        SortExpression="ErrorMessage">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </asp:Panel>

</asp:Content>
