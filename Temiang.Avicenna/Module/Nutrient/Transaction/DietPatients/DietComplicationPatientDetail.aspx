<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="DietComplicationPatientDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Transaction.DietComplicationPatientDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdSelected" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdSelected">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdSelected" />
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
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblDietID" runat="server" Text="Diet"></asp:Label>
                        </td>
                        <td class="entry" colspan="3">
                            <telerik:RadTextBox ID="txtDietID" runat="server" Width="100px" ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblDietName" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                    
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td width="50%" class="labelcaption">
                <asp:Label ID="lblItemList" runat="server" Text="Diet Complication List" Font-Bold="true"></asp:Label>
            </td>
            <td width="50%" class="labelcaption">
                <asp:Label ID="lblSelected" runat="server" Text="Selected Diet Complication" Font-Bold="true"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td valign="top" width="50%">
                <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" AllowSorting="true"
                    AllowPaging="true" PageSize="12" OnItemCommand="grdList_ItemCommand">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="DietComplicationID">
                        <Columns>
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="DietComplicationID" HeaderText="Diet ID" UniqueName="DietComplicationID"
                                SortExpression="DietComplicationID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                AllowSorting="false" />
                            <telerik:GridBoundColumn DataField="DietComplicationName" HeaderText="Diet Name"
                                UniqueName="DietComplicationName" SortExpression="DietComplicationName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" AllowSorting="false" />
                            <telerik:GridTemplateColumn UniqueName="TemplateColumn3" HeaderStyle-Width="30px"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkAdd" runat="server" CommandName="add"><img src="../../../../Images/Toolbar/insert16.png" border="0" /></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
            <td valign="top" width="50%">
                <telerik:RadGrid ID="grdSelected" runat="server" AutoGenerateColumns="False"
                    GridLines="None" AllowSorting="true" AllowPaging="true" PageSize="12" OnItemCommand="grdSelected_ItemCommand"
                    OnNeedDataSource="grdSelected_NeedDataSource">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="DietComplicationID">
                        <Columns>
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="DietComplicationID" HeaderText="Diet ID" UniqueName="DietComplicationID"
                                SortExpression="DietComplicationID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                AllowSorting="false" />
                            <telerik:GridBoundColumn DataField="DietComplicationName" HeaderText="Diet Name"
                                UniqueName="DietComplicationName" SortExpression="DietComplicationName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" AllowSorting="false" />
                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="30px"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkRemove" runat="server" CommandName="remove"><img src="../../../../Images/Toolbar/row_delete16.png" border="0" /></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
