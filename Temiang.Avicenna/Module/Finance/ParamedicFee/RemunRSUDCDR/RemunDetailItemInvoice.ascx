<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RemunDetailItemInvoice.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.ParamedicFee.RemunDetailItemInvoice" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumMemorialItem" runat="server" ValidationGroup="MemorialItem" />
<asp:CustomValidator ErrorMessage="" ID="customValidator" OnServerValidate="customValidator_ServerValidate"
    runat="server" ValidationGroup="MemorialItem">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="width: 100%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        Guarantor
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboGuarantorID" TabIndex="0" runat="server" Height="300px"
                            Width="300px" AutoPostBack="true" HighlightTemplatedItems="true" DataTextField="GuarantorName"
                            DataValueField="GuarantorID" NoWrap="true" EnableLoadOnDemand="true">
                            <ItemTemplate>
                                <div>
                                    <b><%# Eval("GuarantorName") %></b>&nbsp;-&nbsp;<%# Eval("GuarantorID")%>
                                </div>
                            </ItemTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td style="text-align: left">
                        <asp:ImageButton ID="btnFilterGuarantor" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                            OnClick="btnFilter_Click" ToolTip="Search" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Invoice No
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtInvoiceNo" runat="server" Width="300px"></telerik:RadTextBox>
                    </td>
                    <td style="text-align: left">
                        <asp:ImageButton ID="btnFilterInvoiceNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                            OnClick="btnFilter_Click" ToolTip="Search" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <telerik:RadGrid ID="grdInvoices" runat="server" AutoGenerateColumns="False" GridLines="None" 
                            OnNeedDataSource="grdInvoices_NeedDataSource">
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="InvoiceNo" PageSize="10">
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderStyle-Width="40px">
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" id="chkDetail"/>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="InvoiceNo" HeaderText="No Invoice"
                                        UniqueName="InvoiceNo" SortExpression="InvoiceNo" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="Amount" HeaderText="Amount"
                                        UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                                    </telerik:GridNumericColumn >
                                </Columns>
                            </MasterTableView>
                            <FilterMenu>
                            </FilterMenu>
                            <ClientSettings EnableRowHoverStyle="true">
                                <Resizing AllowColumnResize="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table width="100%">
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="Button1" Text="Update" runat="server" CommandName="Update" ValidationGroup="MemorialItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="Button2" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="MemorialItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="Button3" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel">
                        </asp:Button>
                    </td>
                    <td style="width: 20px">
                    </td>
                    <td />
                </tr>
            </table>
        </td>
    </tr>
</table>
