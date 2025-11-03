<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="ContributoryFactorsClassificationFrameworkItemComponentList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ContributoryFactorsClassificationFrameworkItemComponentList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy runat="server" ID="RadAjaxManagerProxy1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdDetail">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFactorID" runat="server" Text="Factor"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtFactorID" runat="server" Width="100px" ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblFactorName" runat="server"></asp:Label>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFactorItemID" runat="server" Text="Factor Item"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtFactorItemID" runat="server" Width="100px" MaxLength="10"
                                ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblFactorItemName" runat="server"></asp:Label>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <telerik:RadGrid ID="grdDetail" runat="server" OnNeedDataSource="grdDetail_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdDetail_UpdateCommand"
                    OnDeleteCommand="grdDetail_DeleteCommand" OnInsertCommand="grdDetail_InsertCommand">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="FactorID,FactorItemID,ComponentID">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                <HeaderStyle Width="30px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn DataField="ComponentID" HeaderText="Component ID"
                                UniqueName="ComponentID" SortExpression="ComponentID">
                                <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ComponentName" HeaderText="Component Name"
                                UniqueName="ComponentID" SortExpression="ComponentID">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAllowEdit" HeaderText="Allow Edit"
                                UniqueName="IsAllowEdit" SortExpression="IsAllowEdit" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings UserControlName="ContributoryFactorsClassificationFrameworkItemComponentDetail.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="ContributoryFactorsClassificationFrameworkItemComponentEditCommand">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="True">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>