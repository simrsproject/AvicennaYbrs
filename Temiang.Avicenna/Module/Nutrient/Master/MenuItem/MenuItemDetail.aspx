<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="MenuItemDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Master.MenuItemDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function openWinCopyMenuItem() {
                var oWnd = $find("<%= winCopy.ClientID %>");
                var oid = $find("<%= txtMenuItemID.ClientID %>");
                oWnd.setUrl('CopyMenuItemDialog.aspx?id=' + oid.get_value());
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }
             
            function onClientClose(oWnd, args) {
                if (oWnd.argument) {
                    __doPostBack("<%= grdMenuItemFood.UniqueID %>", oWnd.argument);
                    oWnd.argument = 'undefined';
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winCopy" Animation="None" Width="800px" Height="400px" runat="server"
        Behavior="Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true" OnClientClose="onClientClose">
    </telerik:RadWindow>
    <table width="100%">
        <tr>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMenuItemID" runat="server" Text="Menu Item ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMenuItemID" runat="server" Width="300px" MaxLength="50">
                            </telerik:RadTextBox>&nbsp;&nbsp;
                            <asp:ImageButton ID="btnCopyMenuItem" runat="server" ImageUrl="../../../../Images/copy16.png"
                                CausesValidation="False" OnClientClick="openWinCopyMenuItem();return false;"
                                ToolTip="Copy Menu Item" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMenuItemName" runat="server" Text="Description"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMenuItemName" runat="server" Width="300px" MaxLength="200">
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
                                OnItemsRequested="cboMenuID_ItemsRequested" AutoPostBack="true" EmptyMessage="Select...">
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
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblVersionID" runat="server" Text="Version"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboVersionID" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="cboVersionID_SelectedIndexChanged" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvVersionID" runat="server" ErrorMessage="Version required"
                                ValidationGroup="entry" ControlToValidate="cboVersionID" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSeqNo" runat="server" Text="Seq No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSeqNo" runat="server" Width="100px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSeqNo" runat="server" ErrorMessage="Seq No required"
                                ValidationGroup="entry" ControlToValidate="cboSeqNo" SetFocusOnError="True" Width="100%">*</asp:RequiredFieldValidator>
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
                            <asp:Label ID="lblClassID" runat="server" Text="Class"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboClassID" runat="server" Width="300px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvClassID" runat="server" ErrorMessage="Class required"
                                ValidationGroup="entry" ControlToValidate="cboClassID" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRMealSet" runat="server" Text="Set"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRMealSet" runat="server" Width="300px" AutoPostBack="True"
                                OnSelectedIndexChanged="cboSRMealSet_SelectedIndexChanged" />
                            <telerik:RadTextBox ID="txtSRMealSet" runat="server" Visible="False" />
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
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" Text="Active" runat="server" />
                        </td>
                        <td width="20">
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
                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="FoodID" HeaderText="Food ID"
                                UniqueName="FoodID" SortExpression="FoodID" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="FoodName" HeaderText="Food Name" UniqueName="FoodName"
                                SortExpression="FoodName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="FoodGroup" HeaderText="Food Group"
                                UniqueName="FoodGroup" SortExpression="FoodGroup" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="FoodType" HeaderText="Food Type"
                                UniqueName="FoodType" SortExpression="FoodType" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="MenuItemFoodGroup" HeaderText="Menu Item Group"
                                UniqueName="MenuItemFoodGroup" SortExpression="MenuItemFoodGroup" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsOptional" HeaderText="Optional Menu"
                                UniqueName="IsOptional" SortExpression="IsOptional" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsStandard" HeaderText="Standard Menu"
                                UniqueName="IsStandard" SortExpression="IsStandard" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings UserControlName="MenuItemFoodDetail.ascx" EditFormType="WebUserControl">
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
