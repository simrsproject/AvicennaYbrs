<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="PaymentReceiptDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.Cashier.PaymentReceiptDetail"
    Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function openWinPayment() {
                var oWnd = $find("<%= winPr.ClientID %>");
                var regno = $find("<%= cboRegistrationNo.ClientID %>");
                var recno = $find("<%= txtPaymentReceiptNo.ClientID %>");
                oWnd.setUrl("PaymentReceiptPickList.aspx?regno=" + regno.get_value() + "&recno=" + recno.get_value());
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinPayment2() {
                var oWnd = $find("<%= winPr.ClientID %>");
                var regno = $find("<%= txtRegistrationNo.ClientID %>");
                var recno = $find("<%= txtPaymentReceiptNo.ClientID %>");
                oWnd.setUrl("PaymentReceiptPickList.aspx?regno=" + regno.get_value() + "&recno=" + recno.get_value());
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd) {
                //Jika apply di click
                var arg = oWnd.argument.command;
                if (arg == 'rebind') {
                    __doPostBack("<%= grdTransPaymentReceiptItem.UniqueID %>", "rebind");
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="1000px" Height="600px"
        Behavior="Close, Move, Maximize" ShowContentDuringLoad="False" VisibleStatusbar="False"
        Modal="true" Title="Payment Receive" OnClientClose="onClientClose" ID="winPr">
    </telerik:RadWindow>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top; width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPaymentReceiptNo" runat="server" Text="Receipt No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPaymentReceiptNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPaymentReceiptDate" runat="server" Text="Receipt Date / Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtPaymentReceiptDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                            DatePopupButton-Enabled="false">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPaymentReceiptTime" runat="server" Width="50px" MaxLength="5"
                                            ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr runat="server" id="trCboRegNo">
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboRegistrationNo" Height="190px" Width="300px"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboRegistrationNo_SelectedIndexChanged" OnItemDataBound="cboRegistrationNo_ItemDataBound"
                                OnItemsRequested="cboRegistrationNo_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "RegistrationNo")%>
                                        &nbsp;(<%# DataBinder.Eval(Container.DataItem, "PatientName")%>) </b>
                                    <br />
                                    <%# DataBinder.Eval(Container.DataItem, "MedicalNo")%>
                                    <br />
                                    <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName") %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvCboRegNo" runat="server" ErrorMessage="Registration No required."
                                ValidationGroup="entry" ControlToValidate="cboRegistrationNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr runat="server" id="trTxtRegNo">
                        <td class="label">
                            <asp:Label ID="lblRegNo" runat="server" Text="Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20"
                                AutoPostBack="True" OnTextChanged="txtRegistrationNo_TextChanged" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvTxtRegNo" runat="server" ErrorMessage="Registration No required."
                                ValidationGroup="entry" ControlToValidate="txtRegistrationNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 160px">
                                        <telerik:RadTextBox ID="txtMedicalNo" runat="server" MaxLength="20" ReadOnly="true"
                                            Width="150px" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnGetPayment" runat="server" Text="Get Payment List" Width="110px"
                                            OnClientClick="openWinPayment();return false;" />
                                        <asp:Button ID="btnGetPayment2" runat="server" Text="Get Payment List" Width="110px"
                                            OnClientClick="openWinPayment2();return false;" />    
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtSalutation" runat="server" Width="28px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px">
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="270px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPatientName" runat="server" ErrorMessage="Patient Name required."
                                ValidationGroup="entry" ControlToValidate="txtPatientName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPrintReceiptAsName" runat="server" Text="Print Receipt As Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPrintReceiptAsName" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPrintReceiptAsName" runat="server" ErrorMessage="Print Receipt As Name required."
                                ValidationGroup="entry" ControlToValidate="txtPrintReceiptAsName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnit" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnitName" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Note"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" TextMode="MultiLine" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsVoid" runat="server" Text="Void" Enabled="false" />
                            <asp:CheckBox ID="chkIsPrinted" runat="server" Text="Printed" Enabled="false" />
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
    <telerik:RadGrid ID="grdTransPaymentReceiptItem" runat="server" OnNeedDataSource="grdTransPaymentReceiptItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdTransPaymentReceiptItem_DeleteCommand">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="PaymentReceiptNo, PaymentNo">
            <Columns>
                <telerik:GridTemplateColumn HeaderStyle-Width="20px">
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="PaymentNo" HeaderText="Payment No"
                    UniqueName="PaymentNo" SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="PaymentDate" HeaderText="Date"
                    UniqueName="PaymentDate" SortExpression="PaymentDate" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="PaymentTime" HeaderText="Time"
                    UniqueName="PaymentTime" SortExpression="PaymentTime" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="PrintReceiptAsName"
                    HeaderText="Print Receipt As Name" UniqueName="PrintReceiptAsName" SortExpression="PrintReceiptAsName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <%--<telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="TotalPaymentAmount"
                    HeaderText="Amount" UniqueName="TotalPaymentAmount" SortExpression="TotalPaymentAmount"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"
                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right" FooterAggregateFormatString="{0:n2}" />--%>
                <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="Amount" HeaderText="Amount"
                    UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                    FooterAggregateFormatString="{0:n2}" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="false" />
        </ClientSettings>
    </telerik:RadGrid>
    <table width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
            </td>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTotalReceipt" runat="server" Text="Total Receipt" />
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTotalReceipt" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
