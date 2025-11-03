<%@ Page Title="Item Information" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="BalanceDetailExpiredDateList.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Stock.BalanceDetailExpiredDateList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdEd">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEd" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%">
        <tr>
            <td style="width: 100%">
                <fieldset>
                    <legend>ITEM INFORMATION</legend>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblLocationID" Text="Location ID" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtLocationID" runat="server" Width="100px" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblLocationName" Text="Location Name" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtLocationName" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
                                        <td />
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblItemID" Text="Item ID" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtItemID" runat="server" Width="100px" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblItemName" runat="server" Text="Item Name"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px" ReadOnly="true" TextMode="MultiLine" Height="35px" />
                                        </td>
                                        <td style="width: 20px"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <fieldset>
                    <legend>BATCH NUMBER & EXPIRED DATE</legend>
                    <table width="100%">
                        <tr>
                            <td>
                                <telerik:RadGrid ID="grdEd" runat="server" OnNeedDataSource="grdEd_NeedDataSource"
                                    AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEd_UpdateCommand"
                                    OnDeleteCommand="grdEd_DeleteCommand" OnInsertCommand="grdEd_InsertCommand" ShowFooter="True">
                                    <HeaderContextMenu>
                                    </HeaderContextMenu>
                                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="BatchNumber,ExpiredDate">
                                        <Columns>
                                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle CssClass="MyImageButton" />
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="BatchNumber" HeaderText="Batch Number"
                                                UniqueName="BatchNumber" SortExpression="BatchNumber" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="ExpiredDate" HeaderText="Expired Date"
                                                UniqueName="ExpiredDate" SortExpression="ExpiredDate" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Balance" HeaderText="Balance"
                                                UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Right"
                                                ItemStyle-HorizontalAlign="Right" />
                                            <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsActive" HeaderText="Active"
                                                UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                            </telerik:GridButtonColumn>
                                            <telerik:GridTemplateColumn />
                                        </Columns>
                                        <EditFormSettings UserControlName="BalanceDetailExpiredDateItem.ascx" EditFormType="WebUserControl">
                                            <EditColumn UniqueName="BalanceDetailExpiredDateItemEditCommand">
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
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>