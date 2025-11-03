<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PersonalInfoGoogleFormImport.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.PersonalInfoGoogleFormImport" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td class="label">Date</td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtDate" runat="server" Width="100px">
                </telerik:RadDatePicker>
            </td>
            <td width="20px">
                <asp:ImageButton ID="btnSearchDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                    OnClick="btnSearch_Click" ToolTip="Search" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Applicant Name</td>
            <td class="entry">
                <telerik:RadTextBox ID="txtName" runat="server" Width="300px" MaxLength="60" />
            </td>
            <td width="20px">
                <asp:ImageButton ID="btnSearchName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                    OnClick="btnSearch_Click" ToolTip="Search" />
            </td>
            <td></td>
        </tr>
    </table>
    <asp:Literal runat="server" ID="litMsg"></asp:Literal>
    <asp:Button runat="server" ID="btnFixFieldMapping" Text="Fix Field Mapping" OnClick="btnFixFieldMapping_Click" />
    <telerik:RadGrid ID="grdGoogleForm" runat="server" OnNeedDataSource="grdGoogleForm_NeedDataSource"
        AutoGenerateColumns="False" ShowGroupPanel="false" AllowPaging="True" PageSize="15"
        AllowSorting="True" GridLines="None">
        <CommandItemStyle />
        <MasterTableView DataKeyNames="Timestamp">
            <Columns>
                <telerik:GridTemplateColumn HeaderText="" UniqueName="IsSelectedEdit" HeaderStyle-Width="50px">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="chkIsSelected" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Timestamp" UniqueName="Timestamp" HeaderText="DateTime Stamp">
                    <HeaderStyle HorizontalAlign="Left" Width="140px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>

            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="false" AllowGroupExpandCollapse="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="false"></Selecting>
        </ClientSettings>
    </telerik:RadGrid>

</asp:Content>
