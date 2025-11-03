<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="ItemExpiryDateList.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Warehouse.ItemExpiryDateList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function rowClosed(transNo, seqNo, ed, batchNo) {
                if (confirm('Are you sure to close this data?')) {
                    __doPostBack("<%= grdEd.UniqueID %>", 'close|' + transNo + '|' + seqNo + '|' + ed + '|' + batchNo);
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="chkIsClosed">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEd" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdEd">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEd" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%">
        <tr>
            <td style="width: 100%">
                <fieldset>
                    <legend>ITEM INFORMATION</legend>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblItemID" Text="Item ID" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtItemID" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblItemName" runat="server" Text="Item Name"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px" ReadOnly="true" TextMode="MultiLine" Height="35px" />
                                        </td>
                                        <td style="width: 20px"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSRItemType" runat="server" Text="Item Type"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadComboBox ID="cboSRItemType" runat="server" Width="300px" Enabled="False" />
                                        </td>
                                        <td style="width: 20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblQuantity" runat="server" Text="Balance"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadNumericTextBox ID="txtQuantity" runat="server" Width="100px" ReadOnly="True" />
                                            <telerik:RadComboBox ID="cboSRItemUnit" runat="server" Width="100px" Enabled="False" />
                                        </td>
                                        <td style="width: 20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label"></td>
                                        <td class="entry2Column">
                                            <asp:CheckBox runat="server" ID="chkIsClosed" AutoPostBack="true" Text="Already Closed"
                                                OnCheckedChanged="chkIsClosed_CheckedChanged" />
                                        </td>
                                        <td style="width: 20px"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <fieldset>
                    <legend>EXPIRY DATE & BATCH NO</legend>
                    <table width="100%">
                        <tr>
                            <td>
                                <telerik:RadGrid ID="grdEd" runat="server" OnNeedDataSource="grdEd_NeedDataSource"
                                    AutoGenerateColumns="False" GridLines="None" ShowFooter="True">
                                    <HeaderContextMenu>
                                    </HeaderContextMenu>
                                    <MasterTableView DataKeyNames="TransactionNo,SequenceNo,ExpiredDate,BatchNumber" GroupLoadMode="Client">
                                        <Columns>
                                            <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="ExpiredDate" HeaderText="Expiry Date"
                                                UniqueName="ExpiredDate" SortExpression="ExpiredDate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
                                            <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="BatchNumber" HeaderText="Batch No."
                                                UniqueName="BatchNumber" SortExpression="BatchNumber" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" FooterText="Total : " FooterStyle-HorizontalAlign="Right" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="QuantityInBaseUnit" HeaderText="Qty Received"
                                                UniqueName="QuantityInBaseUnit" SortExpression="QuantityInBaseUnit" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" FooterText=" " FooterStyle-HorizontalAlign="Right"
                                                Aggregate="Sum" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRItemUnit" HeaderText="Unit"
                                                UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="TransactionNo" HeaderText="Reference No"
                                                UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsClosed" HeaderText="Closed"
                                                UniqueName="IsClosed" SortExpression="IsClosed" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="25px" UniqueName="btnIsClosed">
                                                <ItemTemplate>
                                                    <%# (DataBinder.Eval(Container.DataItem, "IsClosed").Equals(false) ? 
                                                    (string.Format("<a href=\"#\" onclick=\"rowClosed('{0}', '{1}', '{2}', '{3}'); return false;\">{4}</a>", 
                                                        DataBinder.Eval(Container.DataItem, "TransactionNo"),
                                                        DataBinder.Eval(Container.DataItem, "SequenceNo"),
                                                        DataBinder.Eval(Container.DataItem, "ExpiredDate"),
                                                        DataBinder.Eval(Container.DataItem, "BatchNumber"),
                                                        "<img src=\"../../../../Images/Toolbar/delete16.png\" border=\"0\" alt=\"Close\" title=\"Close\" />")) : string.Empty)%>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn />
                                        </Columns>
                                    </MasterTableView>
                                    <FilterMenu>
                                    </FilterMenu>
                                    <ClientSettings EnableRowHoverStyle="True">
                                        <Resizing AllowColumnResize="True" />
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
