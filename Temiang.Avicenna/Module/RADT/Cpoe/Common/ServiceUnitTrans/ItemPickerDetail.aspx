<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="ItemPickerDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.ItemPickerDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboItemType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
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
    <table width="100%">
        <tr>
            <td width="60%" class="labelcaption">
                <asp:Label ID="lblItemList" runat="server" Text="Item List" Font-Bold="true"></asp:Label>
            </td>
            <td width="40%" class="labelcaption">
                <asp:Label ID="lblSelected" runat="server" Text="Selected Item" Font-Bold="true"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="60%">
                <table width="100%">
                    <tr runat="server" id="pnlItemType">
                        <td class="label">
                            <asp:Label ID="lblItemType" runat="server" Text="Item Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboItemType" Width="304px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboItemType_SelectedIndexChanged">
                                <Items>
                                    <telerik:RadComboBoxItem Selected="true" Value="01" Text="Service" />
                                    <telerik:RadComboBoxItem Value="00" Text="Product" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFilter" runat="server" Text="Item Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtFilter" runat="server" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnFilter" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="40%">
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td valign="top" width="60%">
                <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" AllowSorting="true"
                    AllowPaging="true" PageSize="15" OnItemCommand="grdList_ItemCommand" 
                    OnItemDataBound="grdList_ItemDataBound"
                    OnDetailTableDataBind="grdList_DetailTableDataBind">
                    <HeaderContextMenu>
                        
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID">
                        <Columns>
                            <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                AllowSorting="false" />
                            <telerik:GridBoundColumn DataField="Balance" HeaderText="Balance" UniqueName="Balance"
                                HeaderStyle-Width="60px" SortExpression="Balance" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" />
                            <telerik:GridBoundColumn DataField="SRItemUnit" HeaderText="Unit" UniqueName="SRItemUnit"
                                HeaderStyle-Width="50px" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Left" />
                                
                            <telerik:GridTemplateColumn UniqueName="TemplateColumn1" HeaderStyle-Width="50px"
                                ItemStyle-HorizontalAlign="Center" HeaderText="Cito" HeaderStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkCito" runat="server" Enabled='<%# DataBinder.Eval(Container.DataItem, "IsAllowCito") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="TemplateColumn1" HeaderStyle-Width="100px"
                                ItemStyle-HorizontalAlign="Center" HeaderText="Cito Options" HeaderStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <telerik:RadComboBox ID="cboSRCitoPercentage" runat="server" Width="90px"></telerik:RadComboBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            
                            <telerik:GridTemplateColumn UniqueName="TemplateColumn1" HeaderStyle-Width="50px"
                                ItemStyle-HorizontalAlign="Center" HeaderText="Qty" HeaderStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txtQty" runat="server" NumberFormat-DecimalDigits="0"
                                        Value="1" Width="90%" MinValue="0">
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn>
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:RequiredFieldValidator ID="rfvQty" runat="server" ErrorMessage="Quantity required."
                                        ControlToValidate="txtQty" SetFocusOnError="True" Width="100%">
                                        <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="70" DataField="Price" HeaderText="Price"
                                UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            <telerik:GridTemplateColumn UniqueName="TemplateColumn3" HeaderStyle-Width="30px"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkAdd" runat="server" CommandName="add"><img src="../../../../Images/Toolbar/insert16.png" border="0" /></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <DetailTables>
                            <telerik:GridTableView Name="detail" DataKeyNames="ItemID" AutoGenerateColumns="false">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                        SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="75px" DataField="Qty" HeaderText="Consume Qty"
                                        UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="Balance" HeaderText="Balance"
                                        UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="SRItemUnit" HeaderText="Unit"
                                        UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Left" />
                                </Columns>
                            </telerik:GridTableView>
                        </DetailTables>
                    </MasterTableView>
                    <FilterMenu>
                        
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
            <td valign="top" width="40%">
                <telerik:RadGrid ID="grdSelected" runat="server" AutoGenerateColumns="False"
                    GridLines="None" AllowSorting="true" AllowPaging="true" PageSize="12" OnItemCommand="grdSelected_ItemCommand"
                    OnNeedDataSource="grdSelected_NeedDataSource">
                    <HeaderContextMenu>
                        
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID">
                        <Columns>
                            <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                AllowSorting="false" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="Cito" HeaderText="Cito"
                                UniqueName="Cito" SortExpression="Cito" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SRCitoPercentageName" HeaderText="Cito Option"
                                UniqueName="SRCitoPercentageName" SortExpression="SRCitoPercentageName" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn DataField="Qty" HeaderText="Qty" UniqueName="Qty" HeaderStyle-Width="50px"
                                SortExpression="ItemName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                AllowSorting="false" />
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