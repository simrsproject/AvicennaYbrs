<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="ReturnDistributionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Laundry.Transaction.ReturnDistributionDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReturnNo" runat="server" Text="Return No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtReturnNo" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvReturnNo" runat="server" ErrorMessage="Return No required."
                                ValidationGroup="entry" ControlToValidate="txtReturnNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReturnDate" runat="server" Text="Return Date / Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtReturnDate" runat="server" Width="100px">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadMaskedTextBox ID="txtReturnTime" runat="server" Mask="<00..23>:<00..59>"
                                            PromptChar="_" RoundNumericRanges="false" Width="50px">
                                        </telerik:RadMaskedTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvReturnDate" runat="server" ErrorMessage="Return Date required."
                                ValidationGroup="entry" ControlToValidate="txtReturnDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvReturnTime" runat="server" ErrorMessage="Return Time required."
                                ValidationGroup="entry" ControlToValidate="txtReturnTime" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFromServiceUnitID" runat="server" Text="From Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboFromServiceUnitID" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboFromServiceUnitID_ItemDataBound"
                                OnItemsRequested="cboFromServiceUnitID_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvFromServiceUnitID" runat="server" ErrorMessage="From Unit required."
                                ValidationGroup="entry" ControlToValidate="cboFromServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblHandedByUserID" runat="server" Text="Handed By"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboHandedByUserID" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="HandedByUserID_ItemDataBound"
                                OnItemsRequested="HandedByUserID_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "UserName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvHandedByUserID" runat="server" ErrorMessage="Handed By required."
                                ValidationGroup="entry" ControlToValidate="cboHandedByUserID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReceivedBy" runat="server" Text="Received By"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtReceivedBy" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvReceivedBy" runat="server" ErrorMessage="Received By (Desc) required."
                                ValidationGroup="entry" ControlToValidate="txtReceivedBy" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdItem_DeleteCommand"
        OnInsertCommand="grdItem_InsertCommand" OnUpdateCommand="grdItem_UpdateCommand">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="SeqNo">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="SeqNo" HeaderText="Seq No"
                    UniqueName="SeqNo" SortExpression="SeqNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ItemName" HeaderText="Item Name"
                    UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Qty" HeaderText="Qty"
                    UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ItemUnit" HeaderText="Unit"
                    UniqueName="ItemUnit" SortExpression="ItemUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="ReturnDistributionDetailItem.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="LaundryReturnDistributionItemEditCommand">
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
</asp:Content>