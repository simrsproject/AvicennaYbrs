<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="JournalGroupDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.JournalGroupDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                Group Module Name
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtGroupName" runat="server" Width="300px" />
            </td>
            <td width="20px">
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                Notes
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" TextMode="MultiLine" />
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td class="label" />
            <td class="entry">
                <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td colspan="2">
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Journal Code" Selected="True" PageViewID="pgJournalCode" />
                        <telerik:RadTab runat="server" Text="User Access" PageViewID="pgUserAccess" Visible="false" />
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderStyle="Solid" SelectedIndex="0"
                    BorderColor="Gray">
                    <telerik:RadPageView ID="pgJournalCode" runat="server">
                        <telerik:RadListBox runat="server" ID="listJournalCode" CheckBoxes="true" Height="300px"
                            Width="100%" />
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pgUserAccess" runat="server">
                        <telerik:RadGrid ID="grdUserAccess" runat="server" OnNeedDataSource="grdUserAccess_NeedDataSource"
                            AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdUserAccess_UpdateCommand"
                            OnDeleteCommand="grdUserAccess_DeleteCommand" OnInsertCommand="grdUserAccess_InsertCommand">
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="JournalUserID" PageSize="15">
                                <Columns>
                                    <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                        <HeaderStyle Width="35px" />
                                        <ItemStyle CssClass="MyImageButton" />
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridBoundColumn DataField="UserName" HeaderText="User Name" UniqueName="UserName"
                                        SortExpression="UserName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                        ButtonType="ImageButton" ConfirmText="Delete this row?">
                                        <HeaderStyle Width="35px" />
                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <EditFormSettings UserControlName="JournalGroupUserItem.ascx" EditFormType="WebUserControl">
                                    <EditColumn UniqueName="JournalGroupUserItemEditCommand">
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
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </td>
            <td width="20px" />
            <td />
        </tr>
    </table>
</asp:Content>
