<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="FabricDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Master.FabricDetail" %>

<%@ Register Src="~/CustomControl/AddressCtl.ascx" TagName="AddressCtl" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr style="display: none">
            <td colspan="2">
                <table width="100%">
                    <tr>
                        <td class="labelcaption">
                            <asp:Label ID="Label1" runat="server" Text="Factory Detail"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFabricID" runat="server" Text="Factory ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtFabricID" runat="server" Width="100px" MaxLength="10" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvFabricID" runat="server" ErrorMessage="Factory ID required."
                                ValidationGroup="entry" ControlToValidate="txtFabricID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFabricName" runat="server" Text="Factory Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtFabricName" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvFabricName" runat="server" ErrorMessage="Factory Name required."
                                ValidationGroup="entry" ControlToValidate="txtFabricName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblShortName" runat="server" Text="Short Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtShortName" runat="server" Width="300px" MaxLength="35" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblContractNumber" runat="server" Text="Contract Number"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtContractNumber" runat="server" Width="300px" MaxLength="50" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblContractStart" runat="server" Text="Contract Start"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtContractStart" runat="server" Width="100px" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblContractEnd" runat="server" Text="Contract End"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtContractEnd" runat="server" Width="100px" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblContractSummary" runat="server" Text="Contract Summary"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtContractSummary" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtContactPerson" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTaxRegistrationNo" runat="server" Text="Tax Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTaxRegistrationNo" runat="server" Width="300px" MaxLength="20" />
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
                            <asp:CheckBox ID="chkIsPKP" runat="server" Text="PKP" />
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
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
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
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1">
        <Tabs>
            <telerik:RadTab runat="server" Text="Address" Selected="True" PageViewID="pgAddress">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Related Supplier" PageViewID="pgSupplier">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderStyle="Solid" SelectedIndex="0"
        BorderColor="Gray">
        <telerik:RadPageView ID="pgAddress" runat="server">
            <uc1:AddressCtl ID="AddressCtl1" runat="server" />
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgSupplier" runat="server">
            <telerik:RadGrid ID="grdSupplierFabric" runat="server" OnNeedDataSource="grdSupplierFabric_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdSupplierFabric_UpdateCommand"
                OnDeleteCommand="grdSupplierFabric_DeleteCommand" OnInsertCommand="grdSupplierFabric_InsertCommand">
                <HeaderContextMenu>
                    
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="FabricID, SupplierID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SupplierID" HeaderText="Supplier ID"
                            UniqueName="SupplierID" SortExpression="SupplierID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="SupplierName" HeaderText="Supplier Name" UniqueName="SupplierName"
                            SortExpression="SupplierName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                            UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="SupplierFabricDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="SupplierFabricEditCommand">
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
</asp:Content>
