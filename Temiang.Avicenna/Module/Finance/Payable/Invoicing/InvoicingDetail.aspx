<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="InvoicingDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Payable.InvoicingDetail"
    Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function openWinInv() {
                var oWnd = $find("<%= winPr.ClientID %>");
                var supplier = $find("<%= cboSupplierID.ClientID %>");
                var invoice = $find("<%= txtInvoiceNo.ClientID %>");
                oWnd.setUrl('InvoicingPickList.aspx?sid=' + supplier.get_value() + '&inv=' + invoice.get_value() + '&trn=' + '&type=' + '<%= Request.QueryString["type"] %>');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function gotoEditUrl(id) {
                var oWnd = $find("<%= winPr.ClientID %>");
                var supplier = $find("<%= cboSupplierID.ClientID %>");
                var invoice = $find("<%= txtInvoiceNo.ClientID %>");
                oWnd.setUrl('InvoicingPickList.aspx?sid=' + supplier.get_value() + '&inv=' + invoice.get_value() + '&trn=' + id + '&type=' + '<%= Request.QueryString["type"] %>');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd) {
                //Jika apply di click
                if (oWnd.argument && oWnd.argument.command != null) {
                    __doPostBack("<%= grdInvoiceSupplierItem.UniqueID %>", "rebind");
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="1000px" Height="600px"
        Behavior="Close, Move, Maximize" ShowContentDuringLoad="False" VisibleStatusbar="False"
        Modal="true" Title="Purchase Order Receive Outstanding" OnClientClose="onClientClose"
        ID="winPr">
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
                            <asp:Label ID="lblInvoiceReceivedDate" runat="server" Text="Invoice Received Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtInvoiceReceivedDate" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvInvoiceReceivedDate" runat="server" ErrorMessage="Invoice Received Date required."
                                ValidationGroup="entry" ControlToValidate="txtInvoiceReceivedDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSupplierID" runat="server" Text="Supplier"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSupplierID" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" AutoPostBack="true" OnSelectedIndexChanged="cboSupplierID_SelectedIndexChanged"
                                OnItemDataBound="cboSupplierID_ItemDataBound" OnItemsRequested="cboSupplierID_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SupplierName")%>
                                        &nbsp;(<%# DataBinder.Eval(Container.DataItem, "SupplierID")%>) </b>
                                    <br />
                                    <%# DataBinder.Eval(Container.DataItem, "Address") %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSupplierID" runat="server" ErrorMessage="Supplier required."
                                ValidationGroup="entry" ControlToValidate="cboSupplierID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblInvoiceSuppNo" runat="server" Text="Invoice Supplier No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInvoiceSuppNo" runat="server" Width="300px" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:Button ID="btnGetItem" runat="server" Text="Outstanding" Width="110px" OnClientClick="javascript:openWinInv();return false;" />
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
                            <telerik:RadDatePicker ID="txtInvoiceDueDate" runat="server" Width="100px" Enabled="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rvfInvoiceDueDate" runat="server" ErrorMessage="Invoice Due Date required."
                                ValidationGroup="entry" ControlToValidate="txtInvoiceDueDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
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
                            <asp:Label ID="lblSRPayableStatus" runat="server" Text="Status"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRPayableStatus" Width="300px" Enabled="false">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
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
    <telerik:RadGrid ID="grdInvoiceSupplierItem" runat="server" OnNeedDataSource="grdInvoiceSupplierItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdInvoiceSupplierItem_UpdateCommand"
        OnDeleteCommand="grdInvoiceSupplierItem_DeleteCommand" OnInsertCommand="grdInvoiceSupplierItem_InsertCommand" OnDataBound="grdInvoiceSupplierItem_DataBound"
        ShowFooter="True">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="InvoiceNo, TransactionNo">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn UniqueName="edit" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsAllowEdit").Equals(false) ? string.Empty :
                             string.Format("<a href=\"#\" onclick=\"gotoEditUrl('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" title=\"Edit Detail\" /></a>",
                             DataBinder.Eval(Container.DataItem, "TransactionNo")))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="TransactionNo" HeaderText="Trans/Seq No"
                    UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                    HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="InvoiceSupplierNo"
                    HeaderText="Invoice Supp. No" UniqueName="InvoiceSupplierNo" SortExpression="InvoiceSupplierNo"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="InvoiceSupplierDate"
                    HeaderText="Invoice Supp. Date" UniqueName="InvoiceSupplierDate" SortExpression="InvoiceSupplierDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="Amount" HeaderText="Amount"
                    UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PPnAmount" HeaderText="PPn"
                    UniqueName="PPn" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="PphTypeName" HeaderText="Pph Type"
                    UniqueName="PphTypeName" SortExpression="PphTypeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <%--<telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PPh22Amount" HeaderText="PPh 22"
                    UniqueName="PPh22" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" Visible="False" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PPh23Amount" HeaderText="PPh 23"
                    UniqueName="PPh23" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" Visible="False" />--%>
                <telerik:GridCalculatedColumn HeaderStyle-Width="120px" HeaderText="PPh" UniqueName="PPh"
                    DataType="System.Double" DataFields="PPh22Amount,PPh23Amount,PphAmount"
                    SortExpression="PPh" Expression="{0} + {1} + {2}"
                    FooterStyle-HorizontalAlign="Right" Aggregate="Sum" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
<%--                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PPh22Amount" HeaderText="PPh 22"
                    UniqueName="PPh22" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}"  />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PPh23Amount" HeaderText="PPh 23"
                    UniqueName="PPh23" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridCalculatedColumn HeaderStyle-Width="120px" HeaderText="PPh" UniqueName="PPh"
                    DataType="System.Double" DataFields="PPh22Amount,PPh23Amount,PphAmount"
                    SortExpression="Total" Expression="{0} + {1} + {2}"
                    FooterStyle-HorizontalAlign="Right" Aggregate="Sum" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="False" />--%>
                <%--<telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PphAmount" HeaderText="PPh"
                    UniqueName="PphAmount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" Visible="False" />--%>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="StampAmount" HeaderText="Stamp / Shipping"
                    UniqueName="StampAmount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="DownPaymentAmount"
                    HeaderText="Down Payment" UniqueName="DownPaymentAmount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="OtherDeduction" HeaderText="Other Deduction"
                    UniqueName="OtherDeduction" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="Total" HeaderText="Total"
                    UniqueName="Total" SortExpression="Total" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="False" />
                <telerik:GridCalculatedColumn HeaderStyle-Width="120px" HeaderText="Total" UniqueName="Total"
                    DataType="System.Double" DataFields="Amount,PPnAmount,PPh22Amount,PPh23Amount,StampAmount,OtherDeduction,DownPaymentAmount,PphAmount"
                    SortExpression="Total" Expression="{0} + {1} - {2} - {3} + {4} - {5} - {6} - {7}"
                    FooterStyle-HorizontalAlign="Right" Aggregate="Sum" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="false" />
                <telerik:GridTemplateColumn HeaderStyle-Width="120px" HeaderText="Total" UniqueName="TotalTemplate">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox runat="server" ID="txtTotal" DbValue='<%#(
                            System.Convert.ToDecimal(Eval("Amount")) + 
                            System.Convert.ToDecimal(Eval("PpnBilled")) - 
                            System.Convert.ToDecimal(Eval("PPh22Amount")) - 
                            System.Convert.ToDecimal(Eval("PPh23Amount")) + 
                            System.Convert.ToDecimal(Eval("StampAmount")) - 
                            System.Convert.ToDecimal(Eval("OtherDeduction")) - 
                            System.Convert.ToDecimal(Eval("DownPaymentAmount")) - 
                            System.Convert.ToDecimal(Eval("PphAmount")))%>'
                            Width="110px" ReadOnly="true">
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                    <FooterTemplate>
                        <telerik:RadNumericTextBox ID="txtSumTotalTemplate" runat="server" Width="110px" ReadOnly="true">
                        </telerik:RadNumericTextBox>
                    </FooterTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="InvoiceSN" HeaderText="Tax Serial No"
                    UniqueName="InvoiceSN" SortExpression="InvoiceSN" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TaxInvoiceDate" HeaderText="Tax Invoice Date"
                    UniqueName="TaxInvoiceDate" SortExpression="TaxInvoiceDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="InvoicingItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="InvoiceSupplierItemEditCommand">
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
</asp:Content>
