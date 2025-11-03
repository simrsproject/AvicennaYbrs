<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="LaunderedProcessDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Laundry.Transaction.LaunderedProcessDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function openWinPickList() {
                var oWnd = $find("<%= winPickList.ClientID %>");

                oWnd.setUrl('LaunderedProcessDetailItemPicklist.aspx');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinCentralizationPickList() {
                var oWnd = $find("<%= winPickList.ClientID %>");

                oWnd.setUrl('LaunderedProcessDetailItemCentralizationPicklist.aspx');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinInfectiousPickList() {
                var oWnd = $find("<%= winPickList.ClientID %>");

                oWnd.setUrl('LaunderedProcessDetailItemInfectiousPicklist.aspx');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinRewashingPickList() {
                var oWnd = $find("<%= winPickList.ClientID %>");

                oWnd.setUrl('LaunderedProcessDetailItemRewashingPicklist.aspx');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd) {
                if (oWnd.argument) {
                    if (oWnd.argument.command == 'rebind')
                        __doPostBack("<%= grdItem.UniqueID %>", "rebind");
                    else if (oWnd.argument.command == 'rebind1')
                        __doPostBack("<%= grdItem.UniqueID %>", "rebind1");
                    else if (oWnd.argument.command == 'rebind2')
                        __doPostBack("<%= grdItem.UniqueID %>", "rebind2");
                    else 
                        __doPostBack("<%= grdItem.UniqueID %>", "rebind3");
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="820px" Height="600px" Behavior="Close, Move, Maximize"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" AutoSize="false"
        ReloadOnShow="true" OnClientClose="onClientClose" ID="winPickList">
    </telerik:RadWindow>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblProcessNo" runat="server" Text="Process No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtProcessNo" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblProcessDate" runat="server" Text="Process Date / Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtProcessDate" runat="server" Width="100px" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadMaskedTextBox ID="txtProcessTime" runat="server" Mask="<00..23>:<00..59>"
                                            PromptChar="_" RoundNumericRanges="false" Width="50px">
                                        </telerik:RadMaskedTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvProcessDate" runat="server" ErrorMessage="Process Date required."
                                ValidationGroup="entry" ControlToValidate="txtProcessDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvProcessStartTime" runat="server" ErrorMessage="Process Time required."
                                ValidationGroup="entry" ControlToValidate="txtProcessTime" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMachineID" runat="server" Text="Washing Machine"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboMachineID" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboMachineID_ItemDataBound" OnItemsRequested="cboMachineID_ItemsRequested"
                                AutoPostBack="True" OnSelectedIndexChanged="cboMachineID_SelectedIndexChanged">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "MachineName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvMachineID" runat="server" ErrorMessage="Machine required."
                                ValidationGroup="entry" ControlToValidate="cboMachineID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRLaundryProgram" runat="server" Text="Program"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRLaundryProgram" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboSRLaundryProgram_ItemDataBound" OnItemsRequested="cboSRLaundryProgram_ItemsRequested"
                                AutoPostBack="True" OnSelectedIndexChanged="cboSRLaundryProgram_SelectedIndexChanged">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRLaundryProgram" runat="server" ErrorMessage="Program required."
                                ValidationGroup="entry" ControlToValidate="cboSRLaundryProgram" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRLaundryType" runat="server" Text="Laundry Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRLaundryType" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboSRLaundryType_ItemDataBound" OnItemsRequested="cboSRLaundryType_ItemsRequested"
                                AutoPostBack="True" OnSelectedIndexChanged="cboSRLaundryType_SelectedIndexChanged">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRLaundryType" runat="server" ErrorMessage="Laundry Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRLaundryType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:Button ID="btnGetPickList" runat="server" Text="Get Outstanding Items" Width="150px"
                                OnClientClick="javascript:openWinPickList();return false;" />
                            <asp:Button ID="btnGetPickListCentralization" runat="server" Text="Get Outstanding Items" Width="150px"
                                OnClientClick="javascript:openWinCentralizationPickList();return false;" />
                            <asp:Button ID="btnGetInfectiousPickList" runat="server" Text="Get Outstanding Items" Width="150px"
                                OnClientClick="javascript:openWinInfectiousPickList();return false;" />
                            <asp:Button ID="btnGetRewashingPickList" runat="server" Text="Get Outstanding Items" Width="150px"
                                OnClientClick="javascript:openWinRewashingPickList();return false;" />
                            <asp:Button ID="btnResetItem" runat="server" Text="Reset" Width="50px" OnClick="btnResetItem_Click" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMachineVolume" runat="server" Text="Machine Volume"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtMachineVolume" runat="server" Width="100px" MaxLength="10"
                                            MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="0" ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;Kg
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMachineNotes" runat="server" Text="Machine Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMachineNotes" runat="server" Width="300px" TextMode="MultiLine" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsVoid" runat="server" Text="Void" Enabled="false" />
                            <asp:CheckBox ID="chkIsApproved" runat="server" Text="Approved" Enabled="false" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Non Infectious" PageViewID="pgNon"
                Selected="True" />
            <telerik:RadTab runat="server" Text="Non Infectious" PageViewID="pgNonCtl"/>
            <telerik:RadTab runat="server" Text="Infectious" PageViewID="pgInf" />
             <telerik:RadTab runat="server" Text="Re-Washing" PageViewID="pgRew" />
            <telerik:RadTab runat="server" Text="Item Consumption" PageViewID="pgCons" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgNon" runat="server" Selected="true">
            <telerik:RadGrid ID="grdItem" runat="server" ShowFooter="False" OnNeedDataSource="grdItem_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItem_UpdateCommand"
                OnDeleteCommand="grdItem_DeleteCommand" AllowPaging="true" PageSize="10">
                <PagerStyle Mode="NextPrevAndNumeric" />
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ProcessSeqNo">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="ProcessSeqNo" HeaderText="Seq No"
                            UniqueName="ProcessSeqNo" SortExpression="ProcessSeqNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ItemName" HeaderText="Item Name"
                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Qty" HeaderText="Qty"
                            UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n2}" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ItemUnit" HeaderText="Unit"
                            UniqueName="ItemUnit" SortExpression="ItemUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="LaunderedProcessDetailItem.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="grdLaunderedProcessDetailItemEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgNonCtl" runat="server">
            <telerik:RadGrid ID="grdItemCentralized" runat="server" ShowFooter="False" OnNeedDataSource="grdItemCentralized_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnInsertCommand="grdItemCentralized_InsertCommand" OnUpdateCommand="grdItemCentralized_UpdateCommand"
                OnDeleteCommand="grdItemCentralized_DeleteCommand" AllowPaging="true" PageSize="10">
                <PagerStyle Mode="NextPrevAndNumeric" />
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ItemName" HeaderText="Item Name"
                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Qty" HeaderText="Qty"
                            UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n2}" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ItemUnit" HeaderText="Unit"
                            UniqueName="ItemUnit" SortExpression="ItemUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn/>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="LaunderedProcessDetailItemCentralized.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="grdLaunderedProcessDetailItemCentralizedEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgInf" runat="server">
            <telerik:RadGrid ID="grdItemInfectious" runat="server" ShowFooter="False" OnNeedDataSource="grdItemInfectious_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItemInfectious_UpdateCommand"
                OnDeleteCommand="grdItemInfectious_DeleteCommand" AllowPaging="true" PageSize="10">
                <PagerStyle Mode="NextPrevAndNumeric" />
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ProcessSeqNo">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="ProcessSeqNo" HeaderText="Seq No"
                            UniqueName="ProcessSeqNo" SortExpression="ProcessSeqNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ItemName" HeaderText="Item Name"
                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Qty" HeaderText="Qty"
                            UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n2}" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ItemUnit" HeaderText="Unit"
                            UniqueName="ItemUnit" SortExpression="ItemUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="LaunderedProcessDetailItemInfectious.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="grdLaunderedProcessDetailItemInfectiousEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgRew" runat="server">
            <telerik:RadGrid ID="grdItemRewashing" runat="server" ShowFooter="False" OnNeedDataSource="grdItemRewashing_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItemRewashing_UpdateCommand"
                OnDeleteCommand="grdItemRewashing_DeleteCommand" OnInsertCommand="grdItemRewashing_InsertCommand" AllowPaging="true" PageSize="10">
                <PagerStyle Mode="NextPrevAndNumeric" />
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ProcessSeqNo">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="ProcessSeqNo" HeaderText="Seq No"
                            UniqueName="ProcessSeqNo" SortExpression="ProcessSeqNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ItemName" HeaderText="Item Name"
                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Qty" HeaderText="Qty"
                            UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n2}" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ItemUnit" HeaderText="Unit"
                            UniqueName="ItemUnit" SortExpression="ItemUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="LaunderedProcessDetailItemRewashing.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="LaunderedProcessDetailItemRewashingEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgCons" runat="server">
            <telerik:RadGrid ID="grdItemConsumption" runat="server" ShowFooter="False" OnNeedDataSource="grdItemConsumption_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnInsertCommand="grdItemConsumption_InsertCommand" OnUpdateCommand="grdItemConsumption_UpdateCommand"
                OnDeleteCommand="grdItemConsumption_DeleteCommand" AllowPaging="true" PageSize="10">
                <PagerStyle Mode="NextPrevAndNumeric" />
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ItemName" HeaderText="Item Name"
                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Qty" HeaderText="Qty"
                            UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n2}" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ItemUnit" HeaderText="Unit"
                            UniqueName="ItemUnit" SortExpression="ItemUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn/>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="LaunderedProcessDetailItemConsumption.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="grdLaunderedProcessDetailItemConsumptionEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>

</asp:Content>
