<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="InvoicingAdditionalDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Receivable.InvoicingAdditionalDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function onClientClose(oWnd) {
                //Jika apply di click
                var arg = oWnd.argument;
                if (arg != null) {
                    if (oWnd.argument.command == 'rebind') {
                        __doPostBack("<%= grdInvoicesItem.UniqueID %>", "rebind");
                    }
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="1000px" Height="600px"
        Behavior="Close, Move, Maximize" ShowContentDuringLoad="False" VisibleStatusbar="False"
        Modal="true" Title="Payment Pending" OnClientClose="onClientClose" ID="winPr">
    </telerik:RadWindow>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top; width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblInvoiceNo" runat="server" Text="Invoice No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInvoiceNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblInvoiceDate" runat="server" Text="Invoice Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtInvoiceDate" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvInvoiceDate" runat="server" ErrorMessage="Invoice Date required."
                                ValidationGroup="entry" ControlToValidate="txtInvoiceDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboGuarantorID" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" AutoPostBack="true" OnSelectedIndexChanged="cboGuarantorID_SelectedIndexChanged"
                                OnItemDataBound="cboGuarantorID_ItemDataBound" OnItemsRequested="cboGuarantorID_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "GuarantorName")%>
                                        &nbsp;(<%# DataBinder.Eval(Container.DataItem, "GuarantorID")%>) </b>
                                    <br />
                                    <%# DataBinder.Eval(Container.DataItem, "Address") %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvGuarantorID" runat="server" ErrorMessage="Guarantor required."
                                ValidationGroup="entry" ControlToValidate="cboGuarantorID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRReceivableType" runat="server" Text="Receivable Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRReceivableType" Width="300px" Enabled="false">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblInvoiceDueDate" runat="server" Text="Invoice Due Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtInvoiceDueDate" runat="server" Width="100px" DateInput-ReadOnly="True"
                                DatePopupButton-Enabled="False" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTermOfPayment" runat="server" Text="Term Of Payment"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTermOfPayment" runat="server" Width="100px" MaxLength="10"
                                MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="0" AutoPostBack="true"
                                OnTextChanged="txtTermOfPayment_TextChanged" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Note"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" TextMode="MultiLine" runat="server" Width="300px" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRReceivableStatus" runat="server" Text="Status"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRReceivableStatus" Width="300px" Enabled="false">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdInvoicesItem" runat="server" OnNeedDataSource="grdInvoicesItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdInvoicesItem_UpdateCommand"
        OnDeleteCommand="grdInvoicesItem_DeleteCommand" OnInsertCommand="grdInvoicesItem_InsertCommand"
        ShowFooter="False" AllowPaging="True" PageSize="50">
        <PagerStyle AlwaysVisible="True" Mode="NextPrevNumericAndAdvanced"></PagerStyle>
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="InvoiceNo, PaymentNo">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="PaymentNo" HeaderText="Seq No"
                    UniqueName="PaymentNo" SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="MedicalNo" HeaderStyle-Width="100px" HeaderText="Medical No" UniqueName="MedicalNo"
                    SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                    SortExpression="PatientName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="Amount" HeaderText="Amount"
                    UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                    FooterStyle-HorizontalAlign="Right" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="PpnAmount" HeaderText="PPn Amount"
                    UniqueName="PpnAmount" SortExpression="PpnAmount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                    FooterStyle-HorizontalAlign="Right" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="PphAmount" HeaderText="PPh Amount"
                    UniqueName="PphAmount" SortExpression="PphAmount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                    FooterStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ChartOfAccountName"
                    HeaderText="Chart Of Account" UniqueName="ChartOfAccountName" SortExpression="ChartOfAccountName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="InvoicingAdditionalItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="InvoicesItemEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="false" />
        </ClientSettings>
    </telerik:RadGrid>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                    <asp:Panel runat="server" ID="pnlDiscount">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblTransactionAmount" runat="server" Text="Transaction Amount" />
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtTransactionAmount" runat="server" Width="100px"
                                    MaxLength="16" ReadOnly="true" />
                            </td>
                            <td width="20"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label"></td>
                            <td class="entry">
                                <asp:CheckBox ID="chkIsDiscountInPercentage" runat="server" Text="Discount In Percentage"
                                    OnCheckedChanged="chkIsDiscountInPercentage_CheckedChanged" AutoPostBack="true" />
                            </td>
                            <td width="20"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDiscountPercentage" runat="server" Text="Discount (%)" />
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtDiscountPercentage" runat="server" Type="Percent"
                                    Width="100px" MaxLength="5" MaxValue="999.99" MinValue="0" AutoPostBack="true"
                                    OnTextChanged="txtDiscountPercentage_TextChanged" ReadOnly="true" />
                            </td>
                            <td width="20" />
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDiscountAmount" runat="server" Text="Discount Amount" />
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtDiscountAmount" runat="server" Width="100px" MaxLength="16"
                                    MinValue="0" AutoPostBack="true" OnTextChanged="txtDiscountAmount_TextChanged" />
                            </td>
                            <td width="20"></td>
                            <td />
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTotal" runat="server" Text="Total Amount" />
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTotal" runat="server" Width="100px" MaxLength="16"
                                ReadOnly="true" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
