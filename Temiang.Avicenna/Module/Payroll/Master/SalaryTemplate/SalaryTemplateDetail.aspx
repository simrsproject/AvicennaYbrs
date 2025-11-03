<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="SalaryTemplateDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.SalaryTemplateDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblSalaryTemplateID" runat="server" Text="Salary Template ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtSalaryTemplateID" runat="server" Width="300px" MaxLength="10" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvSalaryTemplateID" runat="server" ErrorMessage="Salary Template ID required."
                    ValidationGroup="entry" ControlToValidate="txtSalaryTemplateID"
                    SetFocusOnError="True" Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSalaryTemplateName" runat="server" Text="Salary Template Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtSalaryTemplateName" runat="server" Width="300px" MaxLength="50" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvSalaryTemplateName" runat="server" ErrorMessage="Salary Template Name required."
                    ValidationGroup="entry" ControlToValidate="txtSalaryTemplateName"
                    SetFocusOnError="True" Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
            </td>
            <td width="20px"></td>
            <td></td>
        </tr>
    </table>

    <telerik:RadGrid ID="grdSalaryTemplateItem" runat="server" AutoGenerateColumns="false"
        AllowMultiRowSelection="true" OnNeedDataSource="grdSalaryTemplateItem_NeedDataSource">
        <MasterTableView DataKeyNames="SalaryComponentID,IsSelect">
            <Columns>
                <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn2" HeaderStyle-Width="50px" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SalaryComponentID" HeaderText="ID"
                    UniqueName="SalaryComponentID" SortExpression="SalaryComponentID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn DataField="SalaryComponentCode" HeaderText="Code" UniqueName="SalaryComponentCode"
                    SortExpression="SalaryComponentCode" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />
                <telerik:GridBoundColumn DataField="SalaryComponentName" HeaderText="Salary Component" UniqueName="SalaryComponentName"
                    SortExpression="SalaryComponentName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="True"></Selecting>
        </ClientSettings>
    </telerik:RadGrid>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        //<![CDATA[
        serverID("ajaxManagerID", "<%= AjaxManager.ClientID %>");
        //]]>
    </script>

</telerik:RadCodeBlock>
</asp:Content>

