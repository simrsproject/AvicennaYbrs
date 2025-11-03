<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ItemGroupDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ItemGroupDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblItemGroupID" runat="server" Text="Item Group ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtItemGroupID" runat="server" Width="300px" MaxLength="10" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvItemGroupID" runat="server" ErrorMessage="Item Group ID required."
                    ValidationGroup="entry" ControlToValidate="txtItemGroupID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblItemGroupName" runat="server" Text="Item Group Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtItemGroupName" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvItemGroupName" runat="server" ErrorMessage="Item Group Name required."
                    ValidationGroup="entry" ControlToValidate="txtItemGroupName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRItemType" runat="server" Text="Item Type"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRItemType" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="cboSRItemType_SelectedIndexChanged"/>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvSRItemType" runat="server" ErrorMessage="Item Type required."
                    ValidationGroup="entry" ControlToValidate="cboSRItemType" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr runat="server" id="trRestrictionUserType">
            <td class="label">
                <asp:Label ID="lblRestrictionUserType" runat="server" Text="Restriction User Type (e-Prescription)"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadAutoCompleteBox ID="acbRestrictionUserType" runat="server" Width="300px" DropDownHeight="150">
                </telerik:RadAutoCompleteBox>
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>

        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Initial"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtInitial" runat="server" Width="100px" MaxLength="10" />
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
        <asp:Panel runat="server" ID="pnlCito">
            <tr>
            <td class="label">
                <asp:Label ID="lblCitoValue" runat="server" Text="Cito Value"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtCitoValue" runat="server" Width="100px" />
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
            </td>
            <td class="entry">
                <asp:CheckBox ID="chkIsCitoInPercent" runat="server" Text="Cito In Percent" />
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
        </asp:Panel>
        
        <%--        <tr>
            <td class="label">
                <asp:Label ID="lblAccountID" runat="server" Text="Account ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtAccountID" runat="server" Width="100px" MaxLength="15"
                    AutoPostBack="true" OnTextChanged="txtAccountID_TextChanged" />&nbsp;
                <asp:Label ID="lblAccountName" runat="server" Text=""></asp:Label>
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>--%>
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="CssClass"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtCssClass" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td width="20px">
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
    <asp:Panel runat="server" ID="pnlItemSubGroup">
        <telerik:RadGrid ID="grdItemSubGroup" runat="server" OnNeedDataSource="grdItemSubGroup_NeedDataSource"
            AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItemSubGroup_UpdateCommand"
            OnDeleteCommand="grdItemSubGroup_DeleteCommand" OnInsertCommand="grdItemSubGroup_InsertCommand">
            <HeaderContextMenu>
            </HeaderContextMenu>
            <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID">
                <Columns>
                    <telerik:GridEditCommandColumn ButtonType="ImageButton">
                        <HeaderStyle Width="30px" />
                        <ItemStyle CssClass="MyImageButton" />
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="ID"
                        UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ItemName" HeaderText="Sub Group"
                        UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                        UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridTemplateColumn />
                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                        ButtonType="ImageButton" ConfirmText="Delete this row?">
                        <HeaderStyle Width="30px" />
                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                    </telerik:GridButtonColumn>
                </Columns>
                <EditFormSettings UserControlName="ItemSubGroupDetail.ascx" EditFormType="WebUserControl">
                    <EditColumn UniqueName="ItemSubGroupEditCommand">
                    </EditColumn>
                </EditFormSettings>
            </MasterTableView>
            <FilterMenu>
            </FilterMenu>
            <ClientSettings EnableRowHoverStyle="true">
                <Resizing AllowColumnResize="True" />
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </telerik:RadGrid>
    </asp:Panel>
</asp:Content>
