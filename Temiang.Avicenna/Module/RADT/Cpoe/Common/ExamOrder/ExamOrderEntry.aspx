<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="ExamOrderEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.ExamOrderEntry" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server">
        <script type="text/javascript">

            function winPicklist_ClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.rebind != null)
                    __doPostBack("<%= grdTransChargesItem.UniqueID %>", "rebind");
            }

            function openWinPickList() {
                var cboJO = $find("<%= cboServiceUnitIDJO.ClientID %>");
                if (cboJO != null) {
                    if (cboJO.get_visible()) {
                        if (cboJO.get_value() == '') {
                            alert('Service Unit Order is required.');
                            return;
                        }
                    }
                }

                if (cboJO.get_value() != '') {

                    var url = "";
                    if ((cboJO.get_value() == '<%=Temiang.Avicenna.Common.AppSession.Parameter.ServiceUnitLaboratoryID%>') ||
                    (cboJO.get_value() == '<%=Temiang.Avicenna.Common.AppSession.Parameter.ServiceUnitRadiologyID%>') ||
                    (cboJO.get_value() == '<%=Temiang.Avicenna.Common.AppSession.Parameter.ServiceUnitRadiologyID2%>')) {
                        url = '../ServiceUnitTrans/ItemPickerList.aspx?unit=' + cboJO.get_value() + '&reg=<%=Request.QueryString["regno"] %>&type=emr';
                    }
                    else {
                        url = '../ServiceUnitTrans/ItemPickerDetail.aspx?transno=<%=TransactionNo %>&unit=' +
                            cboJO.get_value() +
                            '&reg=' +
                            Request.QueryString["regno"] +
                            '&type=emr';
                    }

                    var oWnd = $find('<%=winPicklist.ClientID %>');
                    oWnd.setUrl(url);
                    oWnd.show();
                    oWnd.maximize();

                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" ID="winPicklist" Width="700px" Height="300px"
        Behaviors="Close,Move" Modal="True" VisibleStatusbar="False" VisibleTitlebar="False" OnClientClose="winPicklist_ClientClose">
    </telerik:RadWindow>
    <table width="100%">
        <tr>
            <td class="label">Service Unit Order
            </td>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="cboServiceUnitIDJO" runat="server" Width="304px" AutoPostBack="True" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtTransactionDateJO" runat="server" Width="100px" />
                        </td>
                    </tr>
                </table>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Notes
            </td>
            <td>
                <telerik:RadTextBox ID="txtNotesJO" runat="server" TextMode="MultiLine" Width="99%" />
            </td>
            <td width="20">
            </td>
            <td></td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdTransChargesItem" runat="server" OnNeedDataSource="grdTransChargesItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdTransChargesItem_DeleteCommand" OnItemCreated="grdTransChargesItem_ItemCreated"
        OnItemCommand="grdTransChargesItem_ItemCommand" ShowHeader="false">
        <MasterTableView DataKeyNames="TransactionNo,SequenceNo" CommandItemDisplay="Top" FilterExpression="ParentNo = ''">
            <CommandItemTemplate>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>&nbsp;&nbsp;&nbsp; <i>Item Name</i> &nbsp;
                                                                            <telerik:RadComboBox ID="cboItemIDJO" runat="server" Width="304px" EnableLoadOnDemand="true"
                                                                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboItemIDJO_ItemDataBound"
                                                                                OnItemsRequested="cboItemIDJO_ItemsRequested">
                                                                                <FooterTemplate>
                                                                                    Note : Show max 15 items
                                                                                </FooterTemplate>
                                                                            </telerik:RadComboBox>
                                    </td>
                                    <td>&nbsp;
                                                                            <asp:LinkButton ID="lbInsert" runat="server" CommandName="Insert">
                                                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../../Images/Toolbar/insert16.png" /> Add item
                                                                            </asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="lbPickList" runat="server" Visible='<%# !grdTransChargesItem.MasterTableView.IsItemInserted %>'
                                OnClientClick="javascript:openWinPickList();return false;">
                                                                <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../../Images/Toolbar/views16.png" /> Item picker
                            </asp:LinkButton>
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </CommandItemTemplate>
            <CommandItemStyle Height="29px" />
            <Columns>
                <telerik:GridBoundColumn DataField="ParentNo" UniqueName="ParentNo" SortExpression="ParentNo"
                    Visible="false" />
                <telerik:GridBoundColumn DataField="ItemName" UniqueName="ItemName" HeaderText="Item Name" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ChargeQuantity" HeaderText="Qty"
                    UniqueName="ChargeQuantity" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="SRItemUnit" HeaderText="Unit"
                    UniqueName="SRItemUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>


</asp:Content>
