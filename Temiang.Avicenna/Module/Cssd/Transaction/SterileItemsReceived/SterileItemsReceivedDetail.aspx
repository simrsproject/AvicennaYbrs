<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="SterileItemsReceivedDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Cssd.Transaction.SterileItemsReceivedDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function viewItemDetail(seqNo, itemId, itemName, srItemUnit, qty, type) {
                var oWnd = $find("<%= winDetailItem.ClientID %>");
                var rno = $find("<%= txtReceivedNo.ClientID %>");

                oWnd.setUrl('SterileItemsReceivedDetailItemInfo.aspx?itemid=' + itemId + '&itemname=' + itemName + '&unit=' + srItemUnit + '&qty=' + qty + '&rno=' + rno.get_value() + '&seq=' + seqNo + '&type=' + type + '&from=rec');

                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
                oWnd.add_pageLoad(onClientPageLoad);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="600px" Height="500px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winDetailItem">
    </telerik:RadWindow>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReceivedNo" runat="server" Text="Received No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtReceivedNo" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvReceivedNo" runat="server" ErrorMessage="Received No required."
                                ValidationGroup="entry" ControlToValidate="txtReceivedNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReceivedDate" runat="server" Text="Received Date / Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtReceivedDate" runat="server" Width="100px">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadMaskedTextBox ID="txtReceivedTime" runat="server" Mask="<00..23>:<00..59>"
                                            PromptChar="_" RoundNumericRanges="false" Width="50px">
                                        </telerik:RadMaskedTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvReceivedDate" runat="server" ErrorMessage="Received Date required."
                                ValidationGroup="entry" ControlToValidate="txtReceivedDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvReceivedTime" runat="server" ErrorMessage="Received Time required."
                                ValidationGroup="entry" ControlToValidate="txtReceivedTime" SetFocusOnError="True"
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
                                OnItemsRequested="cboFromServiceUnitID_ItemsRequested" AutoPostBack="True" OnSelectedIndexChanged="cboFromServiceUnitID_SelectedIndexChanged">
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
                            <asp:Label ID="Label1" runat="server" Text="From Room"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboFromRoomID" Width="300px" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSenderByID" runat="server" Text="Sender By"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSenderByID" Width="300px" AllowCustomText="true"
                                Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboSenderByID_SelectedIndexChanged" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSenderBy" runat="server" Text="Sender By (Desc)"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSenderBy" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSenderBy" runat="server" ErrorMessage="Sender By (Desc) required."
                                ValidationGroup="entry" ControlToValidate="txtSenderBy" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReceivedByUserID" runat="server" Text="Received By"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboReceivedByUserID" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboReceivedByUserID_ItemDataBound"
                                OnItemsRequested="cboReceivedByUserID_ItemsRequested">
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
                            <asp:RequiredFieldValidator ID="rfvReceivedByUserID" runat="server" ErrorMessage="Received By required."
                                ValidationGroup="entry" ControlToValidate="cboReceivedByUserID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trSRInstrumentType">
                        <td class="label">
                            <asp:Label ID="lblSRInstrumentType" runat="server" Text="Instrument Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rblSRInstrumentType" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="D" Text="Dirty Instrument" />
                                <asp:ListItem Value="C" Text="Clean Instrument" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRInstrumentType" runat="server" ErrorMessage="Instrument Type required."
                                ValidationGroup="entry" ControlToValidate="rblSRInstrumentType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsFromProductionOfGoods" Text="From Production Of Goods" runat="server"
                                Enabled="False" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblProductionNo" runat="server" Text="Reference No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtProductionNo" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdItem_DeleteCommand"
        OnInsertCommand="grdItem_InsertCommand" OnUpdateCommand="grdItem_UpdateCommand">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="ReceivedSeqNo">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn UniqueName="listDetailEdit" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (string.Format("<a href=\"#\" onclick=\"viewItemDetail('{0}', '{1}', '{2}', '{3}', '{4}', 'edit'); return false;\">{5}</a>",
                           DataBinder.Eval(Container.DataItem, "ReceivedSeqNo"), DataBinder.Eval(Container.DataItem, "ItemID"), DataBinder.Eval(Container.DataItem, "ItemName"),
                           DataBinder.Eval(Container.DataItem, "CssdItemUnit"), DataBinder.Eval(Container.DataItem, "Qty"), "<img src=\"../../../../Images/Toolbar/details16.png\" border=\"0\" title=\"Edit Item Detail\" />"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="listDetailView" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (string.Format("<a href=\"#\" onclick=\"viewItemDetail('{0}', '{1}', '{2}', '{3}', '{4}', 'view'); return false;\">{5}</a>",
                           DataBinder.Eval(Container.DataItem, "ReceivedSeqNo"), DataBinder.Eval(Container.DataItem, "ItemID"), DataBinder.Eval(Container.DataItem, "ItemName"),
                           DataBinder.Eval(Container.DataItem, "CssdItemUnit"), DataBinder.Eval(Container.DataItem, "Qty"), "<img src=\"../../../../Images/Toolbar/details16.png\" border=\"0\" title=\"View Item Detail\" />"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="ReceivedSeqNo" HeaderText="Seq No"
                    UniqueName="ReceivedSeqNo" SortExpression="ReceivedSeqNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ItemNo" HeaderText="Item #"
                    UniqueName="ItemNo" SortExpression="ItemNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name"
                    UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Qty" HeaderText="Qty"
                    UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="CssdItemUnit" HeaderText="Unit"
                    UniqueName="CssdItemUnit" SortExpression="CssdItemUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ExpiredDate" HeaderText="Expire Date"
                    UniqueName="ExpiredDate" SortExpression="ExpiredDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="ReuseTo" HeaderText="Reuse To"
                    UniqueName="ReuseTo" SortExpression="ReuseTo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsNeedUltrasound"
                    HeaderText="Need Ultrasound" UniqueName="IsNeedUltrasound" SortExpression="IsNeedUltrasound"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsDtt"
                    HeaderText="DTT Process" UniqueName="IsDtt" SortExpression="IsDtt"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DttDescription" HeaderText="Temperature"
                    UniqueName="DttDescription" SortExpression="DttDescription" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="SterileItemsReceivedItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="grdSterileItemsReceivedEditCommand">
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
