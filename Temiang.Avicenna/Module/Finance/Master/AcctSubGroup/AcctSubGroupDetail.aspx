<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="AcctSubGroupDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.AcctSubGroupDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblAcctSubGroupID" runat="server" Text="Group Code"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtAcctSubGroupID" runat="server" Width="300px" MaxLength="10" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvAcctSubGroupID" runat="server" ErrorMessage="Group Code required."
                    ValidationGroup="entry" ControlToValidate="txtAcctSubGroupID"
                    SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblAcctSubGroupName" runat="server" Text="Group Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtAcctSubGroupName" runat="server" Width="300px" MaxLength="50" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvAcctSubGroupName" runat="server" ErrorMessage="Group Name required."
                    ValidationGroup="entry" ControlToValidate="txtAcctSubGroupName"
                    SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Description
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDescription" runat="server" Width="300px" MaxLength="200" />
            </td>
            <td width="20px"></td>
            <td></td>
        </tr>

    </table>
    <telerik:RadGrid ID="grdSubLedgers" runat="server" OnNeedDataSource="grdSubLedgers_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdSubLedgers_UpdateCommand"
        OnDeleteCommand="grdSubLedgers_DeleteCommand" OnInsertCommand="grdSubLedgers_InsertCommand">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="SubLedgerName">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SubLedgerName" HeaderText="Subledger Code"
                    UniqueName="SubLedgerName" SortExpression="SubLedgerName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="Description" HeaderText="Description"
                    UniqueName="Description" SortExpression="Description" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="HoDescription" HeaderText="Head Office Sub Ledger"
                    UniqueName="HoDescription" SortExpression="HoDescription" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsDirectCost" HeaderText="Direct Cost"
                    UniqueName="IsDirectCost" SortExpression="IsDirectCost" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
            </Columns>
            <EditFormSettings UserControlName="SubLedgerDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="SubLedgerEditCommand">
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
</asp:Content>

