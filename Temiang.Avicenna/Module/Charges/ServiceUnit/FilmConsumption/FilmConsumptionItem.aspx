<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="FilmConsumptionItem.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.FilmConsumptionItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy runat="server" ID="RadAjaxManagerProxy1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdDetail">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemID" runat="server" Width="100px" MaxLength="10"
                                ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblItemName" runat="server"></asp:Label>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <telerik:RadGrid ID="grdDetail" runat="server" OnNeedDataSource="grdDetail_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdDetail_UpdateCommand"
                    OnDeleteCommand="grdDetail_DeleteCommand" OnInsertCommand="grdDetail_InsertCommand">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="TransactionNo,SequenceNo,SRFilmID">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                <HeaderStyle Width="30px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn DataField="SRFilmID" HeaderText="Film ID" UniqueName="SRFilmID"
                                SortExpression="SRFilmID">
                                <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FilmName" HeaderText="Film Name" UniqueName="FilmName"
                                SortExpression="FilmName">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Qty" HeaderText="Qty" UniqueName="Qty"
                                SortExpression="Qty" DataFormatString="{0:n2}">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Kv" HeaderText="KV" UniqueName="Kv"
                                SortExpression="Kv" DataFormatString="{0:n2}">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Ma" HeaderText="MA" UniqueName="Ma"
                                SortExpression="Ma" DataFormatString="{0:n2}">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="S" HeaderText="S" UniqueName="S"
                                SortExpression="S" DataFormatString="{0:n2}">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Mas" HeaderText="MAS" UniqueName="Mas"
                                SortExpression="Mas" DataFormatString="{0:n2}">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings UserControlName="FilmConsumptionItemDetail.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="FilmConsumptionItemDetailEditCommand">
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
</asp:Content>