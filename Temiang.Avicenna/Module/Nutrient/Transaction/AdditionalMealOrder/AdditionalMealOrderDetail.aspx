<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="AdditionalMealOrderDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Transaction.AdditionalMealOrderDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblOrderNo" runat="server" Text="Order No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtOrderNo" runat="server" Width="300px" MaxLength="20" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvOrderNo" runat="server" ErrorMessage="Order No required."
                                ValidationGroup="entry" ControlToValidate="txtOrderNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblOrderDate" runat="server" Text="Order Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtOrderDate" runat="server" Width="100px">
                            </telerik:RadDatePicker>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvOrderDate" runat="server" ErrorMessage="Order Date required."
                                ValidationGroup="entry" ControlToValidate="txtOrderDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEffectiveDate" runat="server" Text="Effective Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtEffectiveDate" runat="server" Width="100px">
                            </telerik:RadDatePicker>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvEffectiveDate" runat="server" ErrorMessage="Effective Date required."
                                ValidationGroup="entry" ControlToValidate="txtEffectiveDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged"
                                AutoPostBack="True" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Service Unit required"
                                ValidationGroup="entry" ControlToValidate="cboServiceUnitID" SetFocusOnError="True"
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
                            <telerik:RadComboBox ID="cboSRMealSet" runat="server" Width="300px" />
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
                            <asp:Label ID="lblQty" runat="server" Text="Qty Order"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtQty" runat="server" Width="100px" MaxLength="10"
                                MinValue="0" NumberFormat-DecimalDigits="0" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblClassID" runat="server" Text="Class"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboClassID" runat="server" Width="300px" Enabled="False" />
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
                            <asp:Label ID="lblMenuID" runat="server" Text="Menu"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboMenuID" runat="server" Width="300px" Enabled="False" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" Height="43px" />
                        </td>
                        <td width="20px">
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
                <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItem_UpdateCommand"
                    OnInsertCommand="grdItem_InsertCommand" OnDeleteCommand="grdItem_DeleteCommand">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="FoodID">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                <HeaderStyle Width="30px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="FoodID" HeaderText="Food ID"
                                UniqueName="FoodID" SortExpression="FoodID" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="FoodName" HeaderText="Food Name"
                                UniqueName="FoodName" SortExpression="FoodName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Qty" HeaderText="Qty"
                                UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:n0}" />
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings UserControlName="AdditionalMealOrderItemDetail.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="AdditionalMealOrderItemDetailEditCommand">
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
