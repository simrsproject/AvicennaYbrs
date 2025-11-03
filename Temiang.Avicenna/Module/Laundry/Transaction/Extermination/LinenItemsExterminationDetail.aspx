<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="LinenItemsExterminationDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Laundry.Transaction.LinenItemsExterminationDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvTransactionDate" runat="server" ErrorMessage="Transaction Date required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboFromServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboFromServiceUnitID_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvFromServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                                ValidationGroup="entry" ControlToValidate="cboFromServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFromLocationID" runat="server" Text="Location"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboFromLocationID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvFromLocationID" runat="server" ErrorMessage="Location required."
                                ValidationGroup="entry" ControlToValidate="cboFromLocationID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRAdjustmentType" runat="server" Text="Reason"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRAdjustmentType" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains"/>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRAdjustmentType" runat="server" ErrorMessage="Adjustment Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRAdjustmentType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image19" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" TextMode="MultiLine" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsApproved" runat="server" Text="Approved" Enabled="false" />
                            <asp:CheckBox ID="chkIsVoid" runat="server" Text="Void" Enabled="false" />
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
    <telerik:RadGrid ID="grdItemTransactionItem" runat="server" OnNeedDataSource="grdItemTransactionItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItemTransactionItem_UpdateCommand"
        OnDeleteCommand="grdItemTransactionItem_DeleteCommand" OnInsertCommand="grdItemTransactionItem_InsertCommand"
        AllowPaging="true" PageSize="15">
        <PagerStyle Mode="NextPrevAndNumeric" />
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="SequenceNo" PageSize="15">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100" DataField="Quantity" HeaderText="Qty"
                    UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="SRItemUnit" HeaderText="Unit"
                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="LinenItemsExterminationDetailItem.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="LinenItemsExterminationDetailItemEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
