<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="DistributionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Warehouse.DistributionDetail" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            var i = 0;
            function openWinPickList() {
                i++;
                var oWnd = $find("<%= winPickList.ClientID %>");
                var oit = $find("<%= cboSRItemType.ClientID %>");
                var odrn = $find("<%= txtReferenceNo.ClientID %>");

                oWnd.setUrl("DistributionPickList.aspx?it=" + oit._value + "&wid=" + i + "&drn=" + odrn.get_value());
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }
           
            function openWinMinMaxPickList() {
                i++;
                var orefno = $find("<%= txtReferenceNo.ClientID %>");
                if (orefno != null) {
                    if (orefno.get_value() != '') {
                        alert('To use this function, the Distribution Request Number must be empty.');
                        return;
                    }
                }

                var cbofromLoc = $find("<%= cboFromLocationID.ClientID %>");
                if (cbofromLoc != null) {
                    if (cbofromLoc.get_value() == '') {
                        alert('From Location is required.');
                        return;
                    }
                }

                var cbotoLoc = $find("<%= cboToLocationID.ClientID %>");
                if (cbotoLoc != null) {
                    if (cbotoLoc.get_value() == '') {
                        alert('To Location is required.');
                        return;
                    }
                }

                var cboitype = $find("<%= cboSRItemType.ClientID %>");
                if (cboitype != null) {
                    if (cboitype.get_value() == '') {
                        alert('Item Type is required.');
                        return;
                    }
                }

                var oWnd = $find("<%= winPickList2.ClientID %>");
                var oit = $find("<%= cboSRItemType.ClientID %>");
                var ofloc = $find("<%= cboFromLocationID.ClientID %>");
                var otloc = $find("<%= cboToLocationID.ClientID %>");
                var ofsu = $find("<%= cboFromServiceUnitID.ClientID %>");
                var otsu = $find("<%= cboToServiceUnitID.ClientID %>");
                var oig = $find("<%= cboItemGroupID.ClientID %>");

                oWnd.setUrl("DistributionByMinMaxPickList.aspx?it=" + oit.get_selectedItem().get_value() + "&floc=" + ofloc.get_selectedItem().get_value() + "&tloc=" + otloc.get_selectedItem().get_value() + "&fsu=" + ofsu.get_selectedItem().get_value() + "&tsu=" + otsu.get_selectedItem().get_value() + "&ig=" + (oig.get_selectedItem() === null ? "" : oig.get_selectedItem().get_value()) + "&wid=" + i);
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinUsedPickList() {
                i++;

                var orefno = $find("<%= txtReferenceNo.ClientID %>");
                if (orefno != null) {
                    if (orefno.get_value() != '') {
                        alert('To use this function, the Distribution Request Number must be empty.');
                        return;
                    }
                }

                var cbofromLoc = $find("<%= cboFromLocationID.ClientID %>");
                if (cbofromLoc != null) {
                    if (cbofromLoc.get_value() == '') {
                        alert('From Location is required.');
                        return;
                    }
                }

                var cbotoLoc = $find("<%= cboToLocationID.ClientID %>");
                if (cbotoLoc != null) {
                    if (cbotoLoc.get_value() == '') {
                        alert('To Location is required.');
                        return;
                    }
                }

                var cboitype = $find("<%= cboSRItemType.ClientID %>");
                if (cboitype != null) {
                    if (cboitype.get_value() == '') {
                        alert('Item Type is required.');
                        return;
                    }
                }
                
                var oWnd = $find("<%= winPickList2.ClientID %>");
                var oit = $find("<%= cboSRItemType.ClientID %>");
                var ofloc = $find("<%= cboFromLocationID.ClientID %>");
                var otloc = $find("<%= cboToLocationID.ClientID %>");
                var ofsu = $find("<%= cboFromServiceUnitID.ClientID %>");
                var otsu = $find("<%= cboToServiceUnitID.ClientID %>");
                var oig = $find("<%= cboItemGroupID.ClientID %>");

                oWnd.setUrl("DistributionByUsedPickList.aspx?it=" + oit.get_selectedItem().get_value() + "&floc=" + ofloc.get_selectedItem().get_value() + "&tloc=" + otloc.get_selectedItem().get_value() + "&fsu=" + ofsu.get_selectedItem().get_value() + "&tsu=" + otsu.get_selectedItem().get_value() + "&ig=" + (oig.get_selectedItem() === null ? "" : oig.get_selectedItem().get_value()) + "&wid=" + i);
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinProcess() {
                var oWnd = $find("<%= winProcess.ClientID %>");
                var odrn = $find("<%= txtTransactionNo.ClientID %>");
                var ofloc = $find("<%= cboFromLocationID.ClientID %>");

                oWnd.setUrl("DistributionProcessToPr.aspx?drn=" + odrn.get_value() + "&floc=" + ofloc._value);
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd) {
                //Jika apply di click
                var arg = oWnd.argument;
                if (arg) {
                    $find("<%= RadAjaxManager.GetCurrent(Page).ClientID %>").ajaxRequest("");
                }
            }
            function viewEdDetail(seqNo) {
                var oWnd = $find("<%= winEdItem.ClientID %>");
                var otrn = $find("<%= txtTransactionNo.ClientID %>");
                var oit = $find("<%= cboSRItemType.ClientID %>");
                oWnd.setUrl('../ItemExpiryDate/ItemExpiryDateDetail.aspx?trn=' + otrn.get_value() + '&sqn=' + seqNo + '&itype=' + oit._value);

                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }
            function onClientCloseEd(oWnd, args) {
                if (oWnd.argument && oWnd.argument.command != null) {
                    __doPostBack("<%= grdItemTransactionItem.UniqueID %>", "rebind:");
                    oWnd.argument = null;
                }
            }
            function approvLevel(level) {
                if (!confirm('Approve this transaction, continue ?')) return false;

                __doPostBack("<%= grdApproval.UniqueID %>", "_approv|" + level);
                return false;
            }

            function unApprovLevel(level) {
                if (!confirm('UnApprove this transaction, continue ?')) return false;

                __doPostBack("<%= grdApproval.UniqueID %>", "_unapprov|" + level);
                return false;
            }
            function txtBarcodeEntryKeyPress(sender, eventArgs) {
                var code = eventArgs.get_keyCode();
                if (code == 13) {
                    eventArgs.set_cancel(true); // Supaya tidak membuka edit grid
                    __doPostBack(sender._clientID.replace(/_/g, "$"), "addwithbarcode|" + sender.get_value());
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="820px" Height="600px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" Title="Distribution Request Pending"
        OnClientClose="onClientClose" ID="winPickList">
    </telerik:RadWindow>
    <telerik:RadWindow runat="server" Animation="None" Width="820px" Height="600px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" Title="Stock Information"
        OnClientClose="onClientClose" ID="winPickList2">
    </telerik:RadWindow>
    <telerik:RadWindow runat="server" Animation="None" Width="500px" Height="300px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" Title="Process to Purchase Request"
        OnClientClose="onClientClose" ID="winProcess">
    </telerik:RadWindow>
    <telerik:RadWindow runat="server" Animation="None" Width="600px" Height="700px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winEdItem"
        OnClientClose="onClientCloseEd">
    </telerik:RadWindow>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                DatePopupButton-Enabled="false" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvTransactionDate" runat="server" ErrorMessage="Transaction Date required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboFromServiceUnitID" runat="server" Width="300px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboFromServiceUnitID_SelectedIndexChanged" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvFromServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                                ValidationGroup="entry" ControlToValidate="cboFromServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFromLocationID" runat="server" Text="Location"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboFromLocationID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvFromLocationID" runat="server" ErrorMessage="From Location required."
                                ValidationGroup="entry" ControlToValidate="cboFromLocationID" SetFocusOnError="True"
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
                            <telerik:RadComboBox ID="cboSRItemType" runat="server" Width="300px" AutoPostBack="True"
                                OnSelectedIndexChanged="cboSRItemType_SelectedIndexChanged" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemGroupID" runat="server" Text="Item Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboItemGroupID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboItemGroupID_ItemDataBound"
                                OnItemsRequested="cboItemGroupID_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemGroupID")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "ItemGroupName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReferenceNo" runat="server" Text="Dist. Request #"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtReferenceNo" runat="server" Width="130px" MaxLength="20"
                                            ReadOnly="True" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnGetPickList" runat="server" Text="Item Outstanding" Width="120px"
                                            OnClientClick="javascript:openWinPickList();return false;" />
                                        <asp:Button ID="btnResetItem" runat="server" Text="Reset" Width="50px" OnClick="btnResetItem_Click" />
                                    </td>
                                </tr>
                            </table>
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
                            <asp:Label ID="lblToServiceUnitID" runat="server" Text="Transfer To Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboToServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboToServiceUnitID_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvToServiceUnitID" runat="server" ErrorMessage="Transfer To Unit required."
                                ValidationGroup="entry" ControlToValidate="cboToServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblToLocationID" runat="server" Text="Location"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboToLocationID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvToLocationID" runat="server" ErrorMessage="To Location required."
                                ValidationGroup="entry" ControlToValidate="cboToLocationID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRDistributionType" runat="server" Text="Distribution Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRDistributionType" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains"/>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" TextMode="MultiLine" />
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
                    <tr runat="server" id="trProsesPr">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:Button ID="btnProcess" runat="server" Text="Process to Purchase Request" OnClientClick="javascript:openWinProcess();return false;" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 350px; vertical-align: top">
                <fieldset runat="server" id="boxApprovalProgress">
                    <legend>Approval Progress</legend>
                    <telerik:RadGrid ID="grdApproval" Width="100%" runat="server" ShowFooter="false"
                        OnNeedDataSource="grdApproval_NeedDataSource" AutoGenerateColumns="False" GridLines="None">
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="ApprovalLevel">
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="20px" DataField="ApprovalLevel" HeaderText=""
                                    UniqueName="ApprovalLevel" SortExpression="ApprovalLevel" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="true" />
                                <telerik:GridBoundColumn DataField="UserName" HeaderText="By" UniqueName="UserName"
                                    SortExpression="UserName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    Visible="true" />
                                <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText="Approve" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <table cellpadding="0" cellspacing="0" style="border-width: 0px">
                                            <tr>
                                                <td style="border-width: 0px">
                                                    <%#true.Equals(DataBinder.Eval(Container.DataItem, "IsApproved")) ? Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ApprovalDateTime")).ToString(AppConstant.DisplayFormat.DateTime) : string.Empty%>
                                                </td>
                                                <td style="border-width: 0px">
                                                    <asp:Panel runat="server" ID="pnlApprove" Visible='<%#true.Equals(DataBinder.Eval(Container.DataItem,"IsApproveAble")) %>'>
                                                        <a style="cursor: pointer;" href="#" onclick='approvLevel(<%#DataBinder.Eval(Container.DataItem,"ApprovalLevel")%>)'>
                                                            <img src="../../../../Images/Toolbar/post16.png" border="0" alt="" />
                                                        </a>
                                                    </asp:Panel>
                                                </td>
                                                <td style="border-width: 0px">
                                                    <asp:Panel runat="server" ID="pnlUnApprove" Visible='<%#true.Equals(DataBinder.Eval(Container.DataItem,"IsUnApproveAble")) %>'>
                                                        <a style="cursor: pointer;" href="#" onclick='unApprovLevel(<%#DataBinder.Eval(Container.DataItem,"ApprovalLevel")%>)'>
                                                            <img src="../../../../Images/Toolbar/delete16.png" border="0" alt="" />
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </fieldset>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItemTransactionItem" runat="server" ShowFooter="True" OnNeedDataSource="grdItemTransactionItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItemTransactionItem_UpdateCommand"
        OnDeleteCommand="grdItemTransactionItem_DeleteCommand" OnInsertCommand="grdItemTransactionItem_InsertCommand">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="SequenceNo">
            <CommandItemTemplate>
                &nbsp;&nbsp;&nbsp;Barcode Scan&nbsp;&nbsp;
                <telerik:RadTextBox ID="txtBarcodeEntry" runat="server" Width="300px" AutoCompleteType="Disabled"
                    SelectionOnFocus="SelectAll">
                    <ClientEvents OnKeyPress="txtBarcodeEntryKeyPress"></ClientEvents>
                </telerik:RadTextBox>
                &nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# !grdItemTransactionItem.MasterTableView.IsItemInserted %>'>
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/insert16.png" />
                    &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new record"></asp:Label>
                </asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="lbPickList" runat="server" Visible='<%# !grdItemTransactionItem.MasterTableView.IsItemInserted %>'
                    OnClientClick="javascript:openWinMinMaxPickList();return false;">
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/views16.png" />&nbsp;<asp:Label
                        runat="server" ID="lblPicList" Text="Pick from Stock Minimum"></asp:Label>
                </asp:LinkButton>
                <asp:LinkButton ID="lbUsedPickList" runat="server" Visible='<%# !grdItemTransactionItem.MasterTableView.IsItemInserted %>'
                    OnClientClick="javascript:openWinUsedPickList();return false;">
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/views16.png" />&nbsp;<asp:Label
                        runat="server" ID="Label1" Text="Pick from Usage History"></asp:Label>
                </asp:LinkButton>
            </CommandItemTemplate>
            <CommandItemStyle Height="29px" />
            <ColumnGroups>
                <telerik:GridColumnGroup HeaderText="To Location Info" Name="ToInfo" HeaderStyle-HorizontalAlign="Center">
                </telerik:GridColumnGroup>
            </ColumnGroups>
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="75" DataField="Quantity" HeaderText="Qty"
                    UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn HeaderStyle-Width="75px" DataField="SRItemUnit" HeaderText="Unit"
                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="75px" DataField="Balance" HeaderText="Balance"
                    UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" ColumnGroupName="ToInfo"/>
                <telerik:GridNumericColumn HeaderStyle-Width="75px" DataField="Booking" HeaderText="Pending"
                    UniqueName="Booking" SortExpression="Booking" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" ColumnGroupName="ToInfo"/>
                <telerik:GridNumericColumn HeaderStyle-Width="75px" DataField="Minimum" HeaderText="Min Qty"
                    UniqueName="Minimum" SortExpression="Minimum" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" ColumnGroupName="ToInfo"/>
                <telerik:GridNumericColumn HeaderStyle-Width="75px" DataField="Maximum" HeaderText="Max Qty"
                    UniqueName="Maximum" SortExpression="Maximum" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" ColumnGroupName="ToInfo"/>
                <telerik:GridNumericColumn HeaderStyle-Width="75px" DataField="Balance2" HeaderText="Balance (From)"
                    UniqueName="Balance2" SortExpression="Balance2" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridTemplateColumn UniqueName="editED" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsControlExpired").Equals(false) ? string.Empty :
                                            string.Format("<a href=\"#\" onclick=\"viewEdDetail('{0}'); return false;\">{1}</a>",
                                            DataBinder.Eval(Container.DataItem, "SequenceNo"), "<img src=\"../../../../Images/calendar16.png\" border=\"0\" title=\"Batch No. & ED\" />"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="DistributionItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="ItemTransactionItemEditCommand">
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
