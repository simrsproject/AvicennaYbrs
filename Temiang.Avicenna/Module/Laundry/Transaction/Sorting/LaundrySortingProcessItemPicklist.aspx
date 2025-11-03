<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="LaundrySortingProcessItemPicklist.aspx.cs" Inherits="Temiang.Avicenna.Module.Laundry.Transaction.LaundrySortingProcessItemPicklist" %>

<%@ Register Assembly="Temiang.Avicenna" Namespace="Temiang.Avicenna.CustomControl"
    TagPrefix="cc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterTypes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterMachineID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Washing Process">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblProcessDate" runat="server" Text="Process Date" />
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtDate" runat="server" Width="110px">
                                </telerik:RadDatePicker>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Filter By Process Date" />
                            </td>
                            <td></td>
                        </tr>
                        <tr runat="server" id="trTypes">
                            <td class="label"></td>
                            <td class="entry">
                                <asp:RadioButtonList ID="rblTypes" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="true">Non Infectious</asp:ListItem>
                                    <asp:ListItem>Infectious</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterTypes" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" />
                            </td>
                            <td />
                        </tr>
                    </table>
                </td>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblMachineID" runat="server" Text="Washing Machine" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboMachineID" runat="server" Width="300px" AllowCustomText="true"
                                    MarkFirstMatch="true">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterMachineID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Filter By Machine" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <telerik:RadGrid ID="grdList" runat="server" ShowStatusBar="true" OnNeedDataSource="grdList_NeedDataSource"
            AllowPaging="True" AutoGenerateColumns="False">
            <PagerStyle Mode="NextPrevAndNumeric" />
            <MasterTableView DataKeyNames="ListKey" ClientDataKeyNames="ListKey"
                PageSize="10">
                <GroupByExpressions>
                    <telerik:GridGroupByExpression>
                        <SelectFields>
                            <telerik:GridGroupByField FieldName="ListGroup" HeaderText="Process No "></telerik:GridGroupByField>
                        </SelectFields>
                        <GroupByFields>
                            <telerik:GridGroupByField FieldName="ListGroup" SortOrder="Ascending"></telerik:GridGroupByField>
                        </GroupByFields>
                    </telerik:GridGroupByExpression>
                </GroupByExpressions>
                <Columns>
                    <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="FromServiceUnitID" HeaderText="FromServiceUnitID"
                        UniqueName="FromServiceUnitID" SortExpression="FromServiceUnitID" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" Visible="false" />
                    <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="FromServiceUnitName" HeaderText="From Service Unit"
                        UniqueName="FromServiceUnitName" SortExpression="FromServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="ProcessSeqNo" HeaderText="Seq No"
                        UniqueName="ProcessSeqNo" SortExpression="ProcessSeqNo" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                        UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ItemName" HeaderText="Item Name"
                        UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Qty" HeaderText="Qty"
                        UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                        DataFormatString="{0:n2}" />
                    <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ItemUnit" HeaderText="Unit"
                        UniqueName="ItemUnit" SortExpression="ItemUnit" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
        </telerik:RadGrid>
    </cc:CollapsePanel>
</asp:Content>
