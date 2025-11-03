<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="ItemExpiryDateDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Warehouse.ItemExpiryDateDetail" %>

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
                                            <asp:Label runat="server" ID="lblTransactionNo" Text="Transaction No" />
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="200px" ReadOnly="true" />
                                                    </td>
                                                    <td style="width: 5px">
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtSequenceNo" runat="server" Width="90px" ReadOnly="true" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblItemID" Text="Item ID" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtItemID" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblItemName" runat="server" Text="Item Name"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px" ReadOnly="true" TextMode="MultiLine" Height="35px" />
                                        </td>
                                        <td style="width: 20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSRItemType" runat="server" Text="Item Type"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadComboBox ID="cboSRItemType" runat="server" Width="300px" Enabled="False" />
                                        </td>
                                        <td style="width: 20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadNumericTextBox ID="txtQuantity" runat="server" Width="100px" ReadOnly="True" />
                                            <telerik:RadComboBox ID="cboSRItemUnit" runat="server" Width="100px" Enabled="False" />
                                        </td>
                                        <td style="width: 20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblConversionFactor" runat="server" Text="Conversion Factor" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtConversionFactor" runat="server" Width="100px"
                                                MinValue="1" ReadOnly="True" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
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
                                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="TransactionNo,SequenceNo,ExpiredDate">
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
                                                ItemStyle-HorizontalAlign="Center" FooterText="Total : " FooterStyle-HorizontalAlign="Right"/>
                                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Quantity" HeaderText="Quantity"
                                                UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Right"
                                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" FooterText=" " FooterStyle-HorizontalAlign="Right"
                                                Aggregate="Sum" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRItemUnit" HeaderText="Unit"
                                                UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="75px" DataField="ConversionFactor"
                                                HeaderText="Conversion" UniqueName="ConversionFactor" SortExpression="ConversionFactor"
                                                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                            </telerik:GridButtonColumn>
                                            <telerik:GridTemplateColumn />
                                        </Columns>
                                        <EditFormSettings UserControlName="ItemExpiryDateDetailItem.ascx" EditFormType="WebUserControl">
                                            <EditColumn UniqueName="ItemExpiryDateDetailItemEditCommand">
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
