<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="LaunderedProcessDetailItemPicklist.aspx.cs" Inherits="Temiang.Avicenna.Module.Laundry.Transaction.LaunderedProcessDetailItemPicklist" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel runat="server" ID="clpPanel1" Title="Outstanding Items">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 50%; vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblReceivedDate" runat="server" Text="Received Date" />
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtReceivedDateFrom" runat="server" Width="100px" />
                                        </td>
                                        <td>
                                            &nbsp;-&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtReceivedDateTo" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" />
                            </td>
                            <td />
                        </tr>
                    </table>
                </td>
                <td style="width: 50%; vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromServiceUnitID" runat="server" Text="From Service Unit" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboFromServiceUnitID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboFromServiceUnitID_ItemDataBound"
                                    OnItemsRequested="cboFromServiceUnitID_ItemsRequested">
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" />
                            </td>
                            <td />
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="False" GridLines="None"
                    OnNeedDataSource="grdList_NeedDataSource" OnPageIndexChanged="grdList_PageIndexChanged">
                    <MasterTableView DataKeyNames="ReceivedNo, ReceivedSeqNo" ClientDataKeyNames="ReceivedNo, ReceivedSeqNo">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="30px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                        runat="server" Checked="false"></asp:CheckBox>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="detailChkbox" runat="server" Checked="False"></asp:CheckBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridDateTimeColumn DataField="ReceivedDate" HeaderText="Date" UniqueName="ReceivedDate"
                                SortExpression="ReceivedDate">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="From Service Unit"
                                UniqueName="ServiceUnitName" SortExpression="ServiceUnitName">
                                <HeaderStyle HorizontalAlign="Left" Width="180px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ReceivedNo" HeaderText="Received No"
                                UniqueName="ReceivedNo" SortExpression="ReceivedNo" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="ReceivedSeqNo" HeaderText="Seq No"
                                UniqueName="ReceivedSeqNo" SortExpression="ReceivedSeqNo" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                                UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ItemName" HeaderText="Item Name"
                                UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Qty" HeaderText="Qty Received"
                                UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:n2}" />
                            <telerik:GridTemplateColumn HeaderStyle-Width="110px" HeaderText="Qty Outstanding"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="False">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txtQtyOutstanding" runat="server" Width="90px" DbValue='<%#Eval("QtyProcessed")%>'
                                        NumberFormat-DecimalDigits="2" Enabled="False" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="110px" HeaderText="Qty Processed"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txtQtyProcessed" runat="server" Width="90px" DbValue='<%#Eval("QtyProcessed")%>'
                                        NumberFormat-DecimalDigits="2" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemUnit" HeaderText="Unit"
                                UniqueName="ItemUnit" SortExpression="ItemUnit" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                                SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRItemUnit" HeaderText="Unit"
                                UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" Visible="False" />    
                        </Columns>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>