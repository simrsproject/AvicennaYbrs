<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="DocumentChecklistDefinitionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.DocumentChecklistDefinitionDetail" %>

<%@ Register Src="~/CustomControl/MatrixCtl.ascx" TagName="Matrix" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblItemID" runat="server" Text="Code"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtItemID" runat="server" Width="300px" MaxLength="10" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Code required."
                    ValidationGroup="entry" ControlToValidate="txtItemID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblItemName" runat="server" Text="Document Checklist Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ErrorMessage="Document Checklist Name required."
                    ValidationGroup="entry" ControlToValidate="txtItemName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
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
    <uc1:Matrix ID="ctlMatrix" runat="server" EntityClassNameMatrix="DocumentChecklistDefinition"
        EntityClassNameSelection="DocumentFiles" FieldNameLinkToHeaderTable="SRDocumentChecklist"
        FieldNameLinkToSelectionTable="DocumentFilesID" FieldNameDisplayInSelectionTable="DocumentNumber,DocumentName"
        FieldNameValueInSelectionTable="DocumentFilesID" LinkTextBoxToHeader="txtItemID"
        MatrixContainFieldRowIndex="False" Visible="False"></uc1:Matrix>
</asp:Content>
