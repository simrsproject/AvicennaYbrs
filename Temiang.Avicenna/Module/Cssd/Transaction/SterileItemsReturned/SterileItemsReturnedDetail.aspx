<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="SterileItemsReturnedDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Cssd.Transaction.SterileItemsReturnedDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function openWinPickList() {
                var oWnd = $find("<%= winPickList.ClientID %>");
                var otu = $find("<%= cboToServiceUnitID.ClientID %>");

                oWnd.setUrl("SterileItemsReturnedDetailItemPickList.aspx?tounit=" + otu.get_value());
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }
            
            function onClientClose(oWnd) {
                if (oWnd.argument && oWnd.argument.command == 'rebind')
                    __doPostBack("<%= grdItem.UniqueID %>", "rebind");
            }

            function viewItemDetail(seqNo, itemId, itemName, srItemUnit, qty, type) {
                var oWnd = $find("<%= winDetailItem.ClientID %>");
                var rno = $find("<%= txtReturnNo.ClientID %>");

                oWnd.setUrl('../SterileItemsReceived/SterileItemsReceivedDetailItemInfo.aspx?itemid=' + itemId + '&itemname=' + itemName + '&unit=' + srItemUnit + '&qty=' + qty + '&rno=' + rno.get_value() + '&seq=' + seqNo + '&type=' + type + '&from=ret');

                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
                oWnd.add_pageLoad(onClientPageLoad);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="820px" Height="600px" Behavior="Close, Move, Maximize"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" AutoSize="false"
        ReloadOnShow="true" OnClientClose="onClientClose" ID="winPickList">
    </telerik:RadWindow>
    <telerik:RadWindow runat="server" Animation="None" Width="600px" Height="500px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winDetailItem">
    </telerik:RadWindow>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReturnNo" runat="server" Text="Return No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtReturnNo" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblProcessDate" runat="server" Text="Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtReturnDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                DatePopupButton-Enabled="false" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvReturnDate" runat="server" ErrorMessage="Return Date required."
                                ValidationGroup="entry" ControlToValidate="txtReturnDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReturnTime" runat="server" Text="Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadMaskedTextBox ID="txtReturnTime" runat="server" Mask="<00..23>:<00..59>"
                                PromptChar="_" RoundNumericRanges="false" Width="50px">
                            </telerik:RadMaskedTextBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvReturnTime" runat="server" ErrorMessage="Time required."
                                ValidationGroup="entry" ControlToValidate="txtReturnTime" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblToServiceUnitID" runat="server" Text="To Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboToServiceUnitID" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboToServiceUnitID_ItemDataBound"
                                OnItemsRequested="cboToServiceUnitID_ItemsRequested">
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
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvToServiceUnitID" runat="server" ErrorMessage="To Service Unit required."
                                ValidationGroup="entry" ControlToValidate="cboToServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblHandedByUserID" runat="server" Text="Handed By"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboHandedByUserID" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboHandedByUserID_ItemDataBound"
                                OnItemsRequested="cboHandedByUserID_ItemsRequested">
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
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReceivedBy" runat="server" Text="Received By"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtReceivedBy" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvReceivedBy" runat="server" ErrorMessage="Received By required."
                                ValidationGroup="entry" ControlToValidate="txtReceivedBy" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:Button ID="btnGetPickList" runat="server" Text="Get Outstanding Items" Width="150px"
                                OnClientClick="javascript:openWinPickList();return false;" />
                            <asp:Button ID="btnResetItem" runat="server" Text="Reset" Width="50px" OnClick="btnResetItem_Click" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr style="display: none">
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsVoid" runat="server" Text="Void" Enabled="false" />
                            <asp:CheckBox ID="chkIsApproved" runat="server" Text="Approved" Enabled="false" />
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
    <telerik:RadGrid ID="grdItem" runat="server" ShowFooter="False" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdItem_DeleteCommand"
        AllowPaging="true" PageSize="10">
        <PagerStyle Mode="NextPrevAndNumeric" />
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="ReturnSeqNo">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="listDetailView" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (string.Format("<a href=\"#\" onclick=\"viewItemDetail('{0}', '{1}', '{2}', '{3}', '{4}', 'info'); return false;\">{5}</a>",
                           DataBinder.Eval(Container.DataItem, "ReturnSeqNo"), DataBinder.Eval(Container.DataItem, "ItemID"), DataBinder.Eval(Container.DataItem, "ItemName"),
                           DataBinder.Eval(Container.DataItem, "CssdItemUnit"), DataBinder.Eval(Container.DataItem, "Qty"), "<img src=\"../../../../Images/Toolbar/details16.png\" border=\"0\" title=\"View Item Detail\" />"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="ReturnSeqNo" HeaderText="Seq No"
                    UniqueName="ReturnSeqNo" SortExpression="ReturnSeqNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemNo" HeaderText="Item #"
                    UniqueName="ItemNo" SortExpression="ItemNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ItemName" HeaderText="Item Name"
                    UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Qty" HeaderText="Qty"
                    UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="CssdItemUnit" HeaderText="Unit"
                    UniqueName="CssdItemUnit" SortExpression="CssdItemUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Weight" HeaderText="Weight"
                    UniqueName="Weight" SortExpression="Weight" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
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
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
