<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="FoodDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Master.FoodDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFoodID" runat="server" Text="Food ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtFoodID" runat="server" Width="100px" MaxLength="10">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20">
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFoodName" runat="server" Text="Food Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtFoodName" runat="server" Width="300px" MaxLength="200">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvFoodName" runat="server" ErrorMessage="Food Name required"
                                ValidationGroup="entry" ControlToValidate="txtFoodName" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblWeight" runat="server" Text="Weight"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtWeight" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                            <telerik:RadComboBox ID="cboSRItemUnit" runat="server" Width="100px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRItemUnit" runat="server" ErrorMessage="Item Unit required"
                                ValidationGroup="entry" ControlToValidate="cboSRItemUnit" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRFoodGroup2" runat="server" Text="Food Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRFoodGroup2" runat="server" Width="300px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRFoodGroup2" runat="server" ErrorMessage="Food Type required"
                                ValidationGroup="entry" ControlToValidate="cboSRFoodGroup2" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRFoodGroup1" runat="server" Text="Food Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRFoodGroup1" runat="server" Width="300px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRFoodGroup1" runat="server" ErrorMessage="Food Group required"
                                ValidationGroup="entry" ControlToValidate="cboSRFoodGroup1" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblQtyPortion" runat="server" Text="Qty Portion"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtQtyPortion" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="2" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trIsPackage">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsPackage" Text="Package" runat="server" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trIsForSpecialCondition">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsForSpecialCondition" Text="For Patient With Special Condition" runat="server" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trIsIsSalesAvailable">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsSalesAvailable" Text="Sales Available" runat="server" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trItemID">
                        <td class="label">
                            <asp:Label ID="lblItemID" runat="server" Text="Item Tariff" />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboItemID" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboItemID_ItemDataBound"
                                OnItemsRequested="cboItemID_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item Tariff required"
                                ValidationGroup="entry" ControlToValidate="cboItemID" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" Text="Active" runat="server" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Food Item" PageViewID="pvItem" Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Food Package" PageViewID="pvPackage">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pvItem" runat="server" Selected="true">
            <telerik:RadGrid ID="grdFoodItem" runat="server" OnNeedDataSource="grdFoodItem_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdFoodItem_UpdateCommand"
                OnDeleteCommand="grdFoodItem_DeleteCommand" OnInsertCommand="grdFoodItem_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="60" DataField="Qty" HeaderText="Qty"
                            UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n2}" />
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRItemUnit" HeaderText="Unit"
                            UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="FoodItemDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EditFoodItemDetail">
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
        </telerik:RadPageView>
        <telerik:RadPageView ID="pvPackage" runat="server">
            <telerik:RadGrid ID="grdFoodPackage" runat="server" OnNeedDataSource="grdFoodPackage_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdFoodPackage_DeleteCommand" OnInsertCommand="grdFoodPackage_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="FoodDetailID">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="FoodDetailID" HeaderText="ID"
                            UniqueName="FoodDetailID" SortExpression="FoodDetailID" />
                        <telerik:GridBoundColumn DataField="FoodDetailName" HeaderText="Food Detail Name" UniqueName="FoodDetailName"
                            SortExpression="FoodDetailName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="FoodPackageDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EditFoodPackageDetail">
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
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
