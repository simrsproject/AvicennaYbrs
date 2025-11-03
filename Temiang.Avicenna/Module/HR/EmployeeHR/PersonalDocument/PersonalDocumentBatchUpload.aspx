<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="PersonalDocumentBatchUpload.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.PersonalDocumentBatchUpload" %>
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
                <td colspan="3"><br /><strong>* File Name Format:</strong> EmployeeNo_SeqNo_DocumentName
                    <ul>
                        <li><strong>EmployeeNo:</strong> Employee No</li>
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
                        <HeaderStyle HorizontalAlign="Left" Width="200px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="PersonID" HeaderText="PersonID" UniqueName="PersonID"
                        SortExpression="PersonID" Visible="false">
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No" UniqueName="EmployeeNo"
                        SortExpression="EmployeeNo">
                        <HeaderStyle HorizontalAlign="Left" Width="130px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                        SortExpression="EmployeeName">
                        <HeaderStyle HorizontalAlign="Left" Width="250px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="FileDate" HeaderText="File Date" UniqueName="FileDate"
                        SortExpression="FileDate">
                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn DataField="ErrorMessage" HeaderText="Error Message" UniqueName="ErrorMessage"
                        SortExpression="ErrorMessage">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </asp:Panel>

</asp:Content>