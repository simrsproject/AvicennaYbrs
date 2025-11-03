<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="DocumentDefinitionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.DocumentDefinitionDetail" %>

<%@ Register Src="~/CustomControl/MatrixCtl.ascx" TagName="Matrix" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr style="display: none">
            <td class="label">
                <asp:Label ID="lblDocumentDefinitionID" runat="server" Text="Document Definition ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtDocumentDefinitionID" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvDocumentDefinitionID" runat="server" ErrorMessage="Document Definition ID required."
                    ValidationGroup="entry" ControlToValidate="txtDocumentDefinitionID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblDepartmentID" runat="server" Text="Department"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboDepartmentID" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvDepartmentID" runat="server" ErrorMessage="Department required."
                    ValidationGroup="entry" ControlToValidate="cboDepartmentID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRFilesAnalysis" runat="server" Text="Files Analysis Type"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRFilesAnalysis" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvSRFilesAnalysis" runat="server" ErrorMessage="Files Analysis Type required."
                    ValidationGroup="entry" ControlToValidate="cboSRFilesAnalysis" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        
        <tr>
            <td class="label">
            </td>
            <td class="entry">
                <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdDocumentFiles" runat="server" AutoGenerateColumns="false"
        AllowMultiRowSelection="true" OnNeedDataSource="grdDocumentFiles_NeedDataSource">
        <MasterTableView DataKeyNames="DocumentFilesID,IsSelect">
            <Columns>
                <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn2" HeaderStyle-Width="50px" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DocumentFilesID" HeaderText="ID"
                    UniqueName="DocumentFilesID" SortExpression="DocumentFilesID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="False" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DocumentNumber" HeaderText="Number"
                    UniqueName="DocumentNumber" SortExpression="DocumentNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="DocumentName" HeaderText="Document Name" UniqueName="DocumentName"
                    SortExpression="DocumentName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="True"></Selecting>
        </ClientSettings>
    </telerik:RadGrid>
    <uc1:Matrix ID="ctlMatrix" runat="server" EntityClassNameMatrix="DocumentDefinitionItem"
        EntityClassNameSelection="DocumentFiles" FieldNameLinkToHeaderTable="DocumentDefinitionID"
        FieldNameLinkToSelectionTable="DocumentFilesID" FieldNameDisplayInSelectionTable="DocumentNumber,DocumentName"
        FieldNameValueInSelectionTable="DocumentFilesID" LinkTextBoxToHeader="txtDocumentDefinitionID"
        MatrixContainFieldRowIndex="False" Visible="False"></uc1:Matrix>
</asp:Content>
