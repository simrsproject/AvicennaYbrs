<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="WageStructureAndScaleDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.WageStructureAndScaleDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemID" runat="server" Text="ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemID" runat="server" Width="300px" MaxLength="10" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="ID required."
                                ValidationGroup="entry" ControlToValidate="txtItemID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemName" runat="server" Text="Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px" MaxLength="200" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ErrorMessage="Type required."
                                ValidationGroup="entry" ControlToValidate="txtItemName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNumericValue" runat="server" Text="Load (%)"></asp:Label></td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtNumericValue" Type="Percent" MinValue="0" MaxValue="100" runat="server" Width="100px" /></td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvNumericValue" runat="server" ErrorMessage="Load (%) required."
                                ValidationGroup="entry" ControlToValidate="txtNumericValue" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItem_UpdateCommand"
        OnDeleteCommand="grdItem_DeleteCommand" OnInsertCommand="grdItem_InsertCommand">
        <HeaderContextMenu>
            <CollapseAnimation Duration="200" Type="OutQuint" />
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="ItemID"
                    HeaderText="ID" UniqueName="ItemID" SortExpression="ItemID"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="ItemName"
                    HeaderText="Item Name" UniqueName="ItemName" SortExpression="ItemName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="WageStructureAndScaleItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="WageStructureAndScaleItemDetailEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
            <CollapseAnimation Duration="200" Type="OutQuint" />
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
