<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ServiceRoomDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ServiceRoomDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server">
        <script language="javascript" type="text/javascript">
            function openWinPhoto() {
                var oWnd = $find("<%= winUpload.ClientID %>");
                oWnd.SetUrl("ServiceRoomUploadFoto.aspx");
                oWnd.Show();
            }

            function win_ClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg != null) {
                    __doPostBack('<%= grdImages.UniqueID%>', "AfterUpload");
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winUpload" Animation="None" Width="800px" Height="500px"
        runat="server" Behavior="Maximize,Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true" OnClientClose="win_ClientClose">
    </telerik:RadWindow>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRoomID" runat="server" Text="Room ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRoomID" runat="server" Width="100px" MaxLength="10" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRoomID" runat="server" ErrorMessage="Room ID required."
                                ValidationGroup="entry" ControlToValidate="txtRoomID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRoomName" runat="server" Text="Room Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRoomName" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRoomName" runat="server" ErrorMessage="Room Name required."
                                ValidationGroup="entry" ControlToValidate="txtRoomName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100px" MaxLength="10"
                                            AutoPostBack="true" OnTextChanged="txtServiceUnitID_TextChanged" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblServiceUnitName" runat="server" Text="" CssClass="labeldescription"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Service Unit ID required."
                                ValidationGroup="entry" ControlToValidate="txtServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Floor"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboSRFloor" runat="server" Width="300px" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRGenderType" runat="server" Text="Specifically for Gender"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRGenderType" runat="server" Width="300px" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtItemID" runat="server" Width="100px" MaxLength="10" AutoPostBack="true"
                                            OnTextChanged="txtItemID_TextChanged" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblItemName" runat="server" Text="" CssClass="labeldescription"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr id="trItemBooked" runat="server">
                        <td class="label">
                            <asp:Label ID="lblItemBookedID" runat="server" Text="Item Booked"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtItemBookedID" runat="server" Width="100px" MaxLength="10"
                                            AutoPostBack="true" OnTextChanged="txtItemBookedID_TextChanged" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblItemBookedName" runat="server" Text="" CssClass="labeldescription"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTariffDiscountForRoomIn" runat="server" Text="Tariff Discount For Rooming In"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTariffDiscountForRoomIn" runat="server" Type="Percent"
                                Width="100px" MaxLength="5" MaxValue="999.99" MinValue="0" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblNumberOfBeds" runat="server" Text="Number Of Beds"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtNumberOfBeds" runat="server" Width="100px" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID1" runat="server" Text="Physician #1"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboParamedicID1" runat="server" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" MarkFirstMatch="false" OnItemDataBound="cboParamedicID_ItemDataBound"
                                OnItemsRequested="cboParamedicID_ItemsRequested">
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID2" runat="server" Text="Physician #2"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboParamedicID2" runat="server" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" MarkFirstMatch="false" OnItemDataBound="cboParamedicID_ItemDataBound"
                                OnItemsRequested="cboParamedicID_ItemsRequested">
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 130px">
                                        <asp:CheckBox ID="chkIsOperatingRoom" runat="server" Text="Operating Room" AutoPostBack="True"
                                            OnCheckedChanged="chkIsOperatingRoom_CheckedChanged" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsShowOnBookingOT" runat="server" Text="Show On Booking Operating Theater" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsBpjs" runat="server" Text="BPJS" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsResetPrice" runat="server" Text="Reset Price On Billing Calculation" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry" colspan="3">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 130px">
                                        <asp:CheckBox ID="chkIsIsolationRoom" runat="server" Text="Isolation Room" />
                                    </td>
                                    <td style="width: 175px">
                                        <asp:CheckBox ID="chkIsNegativePressureRoom" runat="server" Text="Negative Pressure Room" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsPandemicRoom" runat="server" Text="Pandemic Room" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 130px">
                                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                                    </td>
                                    <td style="width: 175px">
                                        <asp:CheckBox ID="chkIsVentilator" runat="server" Text="Ventilator" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" width="100%">
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Bed" Selected="True" PageViewID="pgBed">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Images" PageViewID="pgImages">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Auto Bill Item" PageViewID="pgAutoBillItem">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderStyle="Solid" SelectedIndex="0"
                    BorderColor="Gray">
                    <telerik:RadPageView ID="pgBed" runat="server">
                        <telerik:RadGrid ID="grdBed" runat="server" OnNeedDataSource="grdBed_NeedDataSource"
                            AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdBed_UpdateCommand"
                            OnDeleteCommand="grdBed_DeleteCommand" OnInsertCommand="grdBed_InsertCommand"
                            OnItemCreated="grdBed_ItemCreated">
                            <HeaderContextMenu>
                            </HeaderContextMenu>
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="BedID, ClassID">
                                <Columns>
                                    <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle CssClass="MyImageButton" />
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="BedID" HeaderText="Bed No"
                                        UniqueName="BedID" SortExpression="BedID" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ClassID" HeaderText="Class ID"
                                        UniqueName="ClassID" SortExpression="ClassID" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="ClassName" HeaderText="Class Name" UniqueName="ClassName"
                                        SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="DefaultChargeClassName" HeaderText="Tariff Class Default"
                                        UniqueName="DefaultChargeClassName" SortExpression="DefaultChargeClassName" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                                        SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsTemporary" HeaderText="Temporary"
                                        UniqueName="IsTemporary" SortExpression="IsTemporary" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="90px" DataField="IsNeedConfirmation"
                                        HeaderText="Need Confirmation" UniqueName="IsNeedConfirmation" SortExpression="IsNeedConfirmation"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                                        UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                        ButtonType="ImageButton" ConfirmText="Delete this row?">
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <EditFormSettings UserControlName="BedDetail.ascx" EditFormType="WebUserControl">
                                    <EditColumn UniqueName="BedEditCommand">
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
                    <telerik:RadPageView ID="pgImages" runat="server">
                        <telerik:RadGrid ID="grdImages" runat="server" OnNeedDataSource="grdImages_NeedDataSource"
                            OnItemDataBound="grdImages_ItemDataBound" OnDeleteCommand="grdImages_DeleteCommand"
                            AutoGenerateColumns="False" GridLines="None">
                            <HeaderContextMenu>
                            </HeaderContextMenu>
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="RoomID, SeqNo">
                                <CommandItemTemplate>
                                    <div style="padding: 4px;">
                                        <asp:LinkButton ID="lbAddPhoto" runat="server" OnClientClick="javascript:openWinPhoto();return false;">
                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/insert16.png" />
                                            &nbsp;<asp:Label runat="server" ID="Label1" Text="Add photo"></asp:Label>
                                        </asp:LinkButton>
                                    </div>
                                </CommandItemTemplate>
                                <Columns>
                                    <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle CssClass="MyImageButton" />
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SeqNo" HeaderText="No"
                                        UniqueName="SeqNo" SortExpression="SeqNo" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridTemplateColumn UniqueName="Photo" DataField="Photo" HeaderText="Photo">
                                        <ItemTemplate>
                                            <telerik:RadBinaryImage runat="server" ID="Photo" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="IndexNo" HeaderText="Index No"
                                        UniqueName="IndexNo" SortExpression="IndexNo" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" Visible="false" />
                                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                        ButtonType="ImageButton" ConfirmText="Delete this row?">
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                            </MasterTableView>
                            <FilterMenu>
                            </FilterMenu>
                            <ClientSettings EnableRowHoverStyle="true">
                                <Resizing AllowColumnResize="True" />
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pgAutoBillItem" runat="server">
                        <telerik:RadGrid ID="grdAutoBillItem" runat="server" OnNeedDataSource="grdAutoBillItem_NeedDataSource"
                            AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdAutoBillItem_UpdateCommand"
                            OnDeleteCommand="grdAutoBillItem_DeleteCommand" OnInsertCommand="grdAutoBillItem_InsertCommand">
                            <HeaderContextMenu>
                            </HeaderContextMenu>
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID">
                                <Columns>
                                    <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle CssClass="MyImageButton" />
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                                        UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="400px" DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                        SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="Quantity" HeaderText="Qty"
                                        UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemUnit" HeaderText="Unit"
                                        UniqueName="ItemUnit" SortExpression="ItemUnit" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridTemplateColumn />
                                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                        ButtonType="ImageButton" ConfirmText="Delete this row?">
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <EditFormSettings UserControlName="ServiceRoomAutoBillItemDetail.ascx" EditFormType="WebUserControl">
                                    <EditColumn UniqueName="ServiceRoomAutoBillItemEditCommand">
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
            </td>
        </tr>
    </table>
</asp:Content>
