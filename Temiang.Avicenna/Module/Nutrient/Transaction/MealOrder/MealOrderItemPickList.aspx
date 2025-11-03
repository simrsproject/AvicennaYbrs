<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="MealOrderItemPickList.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Transaction.MealOrderItemPickList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilter">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
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
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image6" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table width="100%">
        <tr>
            <td width="50%" class="labelcaption">
                <asp:Label ID="lblItemList" runat="server" Text="Food List" Font-Bold="true"></asp:Label>
            </td>
            <td width="50%" class="labelcaption">
                <asp:Label ID="lblSelected" runat="server" Text="Selected Food" Font-Bold="true"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFilter" runat="server" Text="Food Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtFilter" runat="server" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnFilter" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%"></td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td valign="top" width="50%">
                <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" AllowSorting="true"
                    AllowPaging="true" PageSize="30" OnItemCommand="grdList_ItemCommand">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="FoodID" ClientDataKeyNames="FoodID">
                        <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldName="FoodGroup2Name" HeaderText="Menu Item Group "></telerik:GridGroupByField>
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="SRFoodGroup2" SortOrder="Ascending"></telerik:GridGroupByField>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <telerik:GridBoundColumn DataField="FoodID" HeaderText="ID" UniqueName="FoodID"
                                SortExpression="FoodID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                AllowSorting="false" Visible="False" />
                            <telerik:GridBoundColumn DataField="FoodName" HeaderText="Food Name" UniqueName="FoodName"
                                SortExpression="FoodName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                AllowSorting="false" />
                            <telerik:GridBoundColumn DataField="SRFoodGroup1" HeaderText="SRFoodGroup1" UniqueName="SRFoodGroup1"
                                SortExpression="SRFoodGroup1" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                AllowSorting="false" Visible="False" />
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="FoodGroupName" HeaderText="Group"
                                UniqueName="FoodGroupName" SortExpression="FoodGroupName" HeaderStyle-HorizontalAlign="Left"
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
                    GridLines="None" AllowSorting="true" AllowPaging="true" PageSize="30" OnItemCommand="grdSelected_ItemCommand"
                    OnNeedDataSource="grdSelected_NeedDataSource">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="FoodID">
                        <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldName="FoodGroup2Name" HeaderText="Menu Item Group "></telerik:GridGroupByField>
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="SRFoodGroup2" SortOrder="Ascending"></telerik:GridGroupByField>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <telerik:GridBoundColumn DataField="FoodName" HeaderText="Food Name" UniqueName="FoodName"
                                SortExpression="FoodName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                AllowSorting="false" />
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="FoodGroupName" HeaderText="Group"
                                UniqueName="FoodGroupName" SortExpression="FoodGroupName" HeaderStyle-HorizontalAlign="Left"
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
