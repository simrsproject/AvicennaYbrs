<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="FeasibilityTestDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Cssd.Transaction.FeasibilityTestDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function openWinPickList() {
                var oWnd = $find("<%= winPickList.ClientID %>");

                oWnd.setUrl('FeasibilityTestDetailItemPicklist.aspx');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd) {
                if (oWnd.argument && oWnd.argument.command == 'rebind')
                    __doPostBack("<%= grdItem.UniqueID %>", "rebind");
            }

            function viewItemDetail(rno, seqNo, itemId, itemName, srItemUnit, qty, type) {
                var oWnd = $find("<%= winDetailItem.ClientID %>");

                oWnd.setUrl('../SterileItemsReceived/SterileItemsReceivedDetailItemInfo.aspx?itemid=' + itemId + '&itemname=' + itemName + '&unit=' + srItemUnit + '&qty=' + qty + '&rno=' + rno + '&seq=' + seqNo + '&type=' + type + '&from=fts');

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
                            <asp:Label ID="lblFeasibilityTestNo" runat="server" Text="Transaction No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtFeasibilityTestNo" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFeasibilityTestDate" runat="server" Text="Date / Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtFeasibilityTestDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                            DatePopupButton-Enabled="false" />
                                    </td>
                                    <td>
                                        <telerik:RadMaskedTextBox ID="txtFeasibilityTestTime" runat="server" Mask="<00..23>:<00..59>"
                                            PromptChar="_" RoundNumericRanges="false" Width="50px">
                                        </telerik:RadMaskedTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvFeasibilityTestDate" runat="server" ErrorMessage="Transction Date required."
                                ValidationGroup="entry" ControlToValidate="txtFeasibilityTestDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvFeasibilityTestTime" runat="server" ErrorMessage="Transaction Time required."
                                ValidationGroup="entry" ControlToValidate="txtFeasibilityTestTime" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr id="trPickList" runat="server">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:Button ID="btnGetPickList" runat="server" Text="Get Outstanding Items" Width="150px"
                                OnClientClick="javascript:openWinPickList();return false;" />
                            <asp:Button ID="btnResetItem" runat="server" Text="Reset" Width="50px" OnClick="btnResetItem_Click" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="1000" TextMode="MultiLine" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" ShowFooter="False" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdItem_DeleteCommand" OnUpdateCommand="grdItem_UpdateCommand" AllowPaging="true" PageSize="10">
        <PagerStyle Mode="NextPrevAndNumeric" />
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="FeasibilityTestSeqNo">
            <ColumnGroups>
                <telerik:GridColumnGroup HeaderText="Package Item Info" Name="PackageItemInfo" HeaderStyle-HorizontalAlign="Center">
                </telerik:GridColumnGroup>
            </ColumnGroups>
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn UniqueName="listDetailEdit" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (string.Format("<a href=\"#\" onclick=\"viewItemDetail('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', 'edit'); return false;\">{6}</a>",
                           DataBinder.Eval(Container.DataItem, "ReceivedNo"), DataBinder.Eval(Container.DataItem, "ReceivedSeqNo"), DataBinder.Eval(Container.DataItem, "ItemID"), DataBinder.Eval(Container.DataItem, "ItemName"),
                           DataBinder.Eval(Container.DataItem, "CssdItemUnit"), DataBinder.Eval(Container.DataItem, "Qty"), "<img src=\"../../../../Images/Toolbar/details16.png\" border=\"0\" title=\"Edit Item Detail\" />"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="listDetailView" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (string.Format("<a href=\"#\" onclick=\"viewItemDetail('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', 'view'); return false;\">{6}</a>",
                           DataBinder.Eval(Container.DataItem, "ReceivedNo"), DataBinder.Eval(Container.DataItem, "ReceivedSeqNo"), DataBinder.Eval(Container.DataItem, "ItemID"), DataBinder.Eval(Container.DataItem, "ItemName"),
                           DataBinder.Eval(Container.DataItem, "CssdItemUnit"), DataBinder.Eval(Container.DataItem, "Qty"), "<img src=\"../../../../Images/Toolbar/details16.png\" border=\"0\" title=\"View Item Detail\" />"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="FeasibilityTestSeqNo" HeaderText="Seq No"
                    UniqueName="FeasibilityTestSeqNo" SortExpression="FeasibilityTestSeqNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ReceivedNo" HeaderText="Received No"
                    UniqueName="ReceivedNo" SortExpression="ReceivedNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemNo" HeaderText="Item #"
                    UniqueName="ItemNo" SortExpression="ItemNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ItemName" HeaderText="Item Name"
                    UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Qty" HeaderText="Qty"
                    UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="CssdItemUnit" HeaderText="Unit"
                    UniqueName="CssdItemUnit" SortExpression="CssdItemUnit" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsFeasibilityTestPassed" HeaderText="Passed"
                    UniqueName="IsFeasibilityTestPassed" SortExpression="IsFeasibilityTestPassed" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsBrokenInstrument" HeaderText="Broken Instrument"
                    UniqueName="IsBrokenInstrument" SortExpression="IsBrokenInstrument" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="QtyReplacements" HeaderText="Qty Replacements"
                    UniqueName="QtyReplacements" SortExpression="QtyReplacements" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsBrokenInstrumentDetail" HeaderText="Broken Instrument"
                    UniqueName="IsBrokenInstrumentDetail" SortExpression="IsBrokenInstrumentDetail" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="PackageItemInfo"/>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="QtyReplacementsDetail" HeaderText="Qty Replacements"
                    UniqueName="QtyReplacementsDetail" SortExpression="QtyReplacementsDetail" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" ColumnGroupName="PackageItemInfo"/>

                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="FeasibilityTestItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="grdFeasibilityTestItemEditCommand">
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
