<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="ItemTariffRequest2Detail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Tariff.ItemTariffRequest2Detail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script language="javascript" type="text/javascript">
            function openTariffComp(itemId, classId) {
                var otrn = $find("<%= txtTariffRequestNo.ClientID %>");
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.SetUrl("ItemTariffRequest2CompList.aspx?trn=" + otrn.get_value() + "&itemId=" + itemId + "&classId=" + classId);
                oWnd.Show();
                /*oWnd.Maximize();*/
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winDialog" Animation="None" Width="600px" Height="400px"
        runat="server" Behavior="Maximize,Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTariffRequestNo" runat="server" Text="Tariff Request No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTariffRequestNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTariffRequestNo" runat="server" ErrorMessage="Tariff Request No required."
                                ValidationGroup="entry" ControlToValidate="txtTariffRequestNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTariffRequestDate" runat="server" Text="Tariff Request Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTariffRequestDate" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTariffRequestDate" runat="server" ErrorMessage="Tariff Request Date required."
                                ValidationGroup="entry" ControlToValidate="txtTariffRequestDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRTariffType" runat="server" Text="Tariff Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRTariffType" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRTariffType" runat="server" ErrorMessage="Tariff Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRTariffType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRItemType" runat="server" Text="Item Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRItemType" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="cboSRItemType_SelectedIndexChanged" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRItemType" runat="server" ErrorMessage="Item Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRItemType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemGroup" runat="server" Text="Item Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboItemGroup" Width="300px" runat="server" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboItemGroup_ItemDataBound"
                                OnItemsRequested="cboItemGroup_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ItemGroupID")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ItemGroupName")%>
                                    </b>
                                    <br />
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvItemGroup" runat="server" ErrorMessage="Item Group required."
                                ValidationGroup="entry" ControlToValidate="cboItemGroup" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblStartingDate" runat="server" Text="Starting Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtStartingDate" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvStartingDate" runat="server" ErrorMessage="Starting Date required."
                                ValidationGroup="entry" ControlToValidate="txtStartingDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr runat="server" id="trImportFromDate">
                        <td class="label">
                            <asp:Label ID="lblImportFromDate" runat="server" Text="Import From Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtImportFromDate" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvImportFromDate" runat="server" ErrorMessage="Import From Date required."
                                ValidationGroup="entry" ControlToValidate="txtImportFromDate" SetFocusOnError="True"
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
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsApproved" runat="server" Text="Approved" Enabled="false" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblApprovedDate" runat="server" Text="Approved Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtApprovedDate" runat="server" Width="100px" Enabled="false" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsVoid" runat="server" Text="Void" Enabled="false" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblVoidDate" runat="server" Text="Void Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtVoidDate" runat="server" Width="100px" Enabled="False" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsImport" runat="server" Text="Import from Excel" />
                            <asp:CheckBox ID="chkIsNew" runat="server" Text="New Item Tariff" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1">
        <Tabs>
            <telerik:RadTab runat="server" Text="List of Items" Selected="True" PageViewID="pgManual">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Import Results" PageViewID="pgImport">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderStyle="Solid" SelectedIndex="0"
        BorderColor="Gray">
        <telerik:RadPageView ID="pgManual" runat="server">
            <telerik:RadGrid ID="grdItemTariffRequestItem" runat="server" OnNeedDataSource="grdItemTariffRequestItem_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItemTariffRequestItem_UpdateCommand"
                OnDeleteCommand="grdItemTariffRequestItem_DeleteCommand" OnInsertCommand="grdItemTariffRequestItem_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="TariffRequestNo, ClassID, ItemID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ClassName" HeaderText="Class"
                            UniqueName="ClassName" SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                            UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridTemplateColumn />
                        <telerik:GridTemplateColumn UniqueName="tariffComp">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openTariffComp('{0}', '{1}'); return false;\"><img src=\"../../../../Images/Toolbar/details16.png\" border=\"0\" title=\"Tariff Component\" /></a>",
                                                                                                            DataBinder.Eval(Container.DataItem, "ItemID"), DataBinder.Eval(Container.DataItem, "ClassID"))%>
                            </ItemTemplate>
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ItemTariffRequest2ItemDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ItemTariffRequest2ItemEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="false">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgImport" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label10" runat="server" Text="Item ID / Item Name"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtFilterItemID" runat="server" Width="300px" MaxLength="100" />
                                </td>
                                <td width="20">
                                    <asp:ImageButton ID="btnFilterItemID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdImportResults" runat="server" OnNeedDataSource="grdImportResults_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" AllowPaging="true" PageSize="15">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ClassID, ItemID, TariffComponentID">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ClassName" HeaderText="Class"
                            UniqueName="ClassName" SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="TariffComponentName"
                            HeaderText="Tariff Component" UniqueName="TariffComponentName" SortExpression="TariffComponentName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="GeneralPrice" HeaderText="General Price"
                            UniqueName="GeneralPrice" SortExpression="GeneralPrice" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="OldPrice" HeaderText="Old Price"
                            UniqueName="OldPrice" SortExpression="OldPrice" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="NewPrice" HeaderText="New Price"
                            UniqueName="NewPrice" SortExpression="NewPrice" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridTemplateColumn />
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="false">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>


</asp:Content>
