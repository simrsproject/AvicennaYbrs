<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="MenuItemExtraDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Master.MenuItemExtraDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSeqNo" runat="server" Text="ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSeqNo" runat="server" Width="300px" MaxLength="5">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMenu" runat="server" Text="Menu"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboMenuID" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboMenuID_ItemDataBound" ValidationGroup="other"
                                OnItemsRequested="cboMenuID_ItemsRequested" EmptyMessage="Select...">
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvMenuID" runat="server" ErrorMessage="Menu required"
                                ValidationGroup="entry" ControlToValidate="cboMenuID" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRMealSet" runat="server" Text="Set"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRMealSet" runat="server" Width="300px" AutoPostBack="True"
                                OnSelectedIndexChanged="cboSRMealSet_SelectedIndexChanged" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRMealSet" runat="server" ErrorMessage="Set required"
                                ValidationGroup="entry" ControlToValidate="cboSRMealSet" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblStartingDate" runat="server" Text="Starting Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtStartingDate" runat="server" Width="100px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvStartingDate" runat="server" ErrorMessage="Starting Date required."
                                ValidationGroup="entry" ControlToValidate="txtStartingDate" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <telerik:RadGrid ID="grdMenuItemFood" runat="server" OnNeedDataSource="grdMenuItemFood_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdMenuItemFood_UpdateCommand"
                    OnInsertCommand="grdMenuItemFood_InsertCommand" OnDeleteCommand="grdMenuItemFood_DeleteCommand">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="FoodID">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                <HeaderStyle Width="30px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DayName" HeaderText="Day Name"
                                UniqueName="DayName" SortExpression="DayName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="FoodID" HeaderText="Food ID"
                                UniqueName="FoodID" SortExpression="FoodID" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="FoodName" HeaderText="Food Name" UniqueName="FoodName"
                                SortExpression="FoodName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="FoodGroup" HeaderText="Group"
                                UniqueName="FoodGroup" SortExpression="FoodGroup" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings UserControlName="MenuItemExtraFoodDetail.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="EditCommandColumn1">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings>
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
