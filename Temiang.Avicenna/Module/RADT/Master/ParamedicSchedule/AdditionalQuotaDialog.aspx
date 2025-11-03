<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="AdditionalQuotaDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ParamedicScheduleAdditionalQuotaDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry" />
    <asp:CustomValidator ID="customValidator" ValidationGroup="entry" runat="server"></asp:CustomValidator>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicName" runat="server" Text="Phyisician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtParamedicName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitName" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnitName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblScheduleDate" runat="server" Text="Schedule Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtScheduleDate" runat="server" Width="100px" Enabled="false" />
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblOperationalTimeName" runat="server" Text="Operational Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtOperationalTimeID" runat="server" Width="30px" ReadOnly="true" />
                            <telerik:RadTextBox ID="txtOperationalTimeName" runat="server" Width="267px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                </table>
            </td>
        </tr>
    </table>
    <br />
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="50" ItemStyle-Height="20">
        <MasterTableView DataKeyNames="ServiceUnitID,ParamedicID,ScheduleDate" ClientDataKeyNames="ServiceUnitID,ParamedicID,ScheduleDate" GroupLoadMode="client" CommandItemDisplay="None">
            <ColumnGroups>
                <telerik:GridColumnGroup HeaderText="Quota Limit" Name="Limit" HeaderStyle-HorizontalAlign="Center">
                </telerik:GridColumnGroup>
                <telerik:GridColumnGroup HeaderText="Additional Quota" Name="Additional" HeaderStyle-HorizontalAlign="Center">
                </telerik:GridColumnGroup>
            </ColumnGroups>
            <Columns>
                <telerik:GridBoundColumn DataField="Quota" HeaderText="General - Direct"
                    UniqueName="Quota" SortExpression="Quota" DataFormatString="{0:n0}" ColumnGroupName="Limit">
                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="QuotaOnline" HeaderText="General - Online"
                    UniqueName="QuotaOnline" SortExpression="QuotaOnline" DataFormatString="{0:n0}" ColumnGroupName="Limit">
                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="QuotaBpjs" HeaderText="BPJS - Direct"
                    UniqueName="QuotaBpjs" SortExpression="QuotaBpjs" DataFormatString="{0:n0}" ColumnGroupName="Limit">
                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="QuotaBpjsOnline" HeaderText="BPJS - Online"
                    UniqueName="QuotaBpjsOnline" SortExpression="QuotaBpjsOnline" DataFormatString="{0:n0}" ColumnGroupName="Limit">
                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>

                <telerik:GridTemplateColumn HeaderStyle-Width="110px" HeaderText="General - Direct" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Additional">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtAddQuota" runat="server" NumberFormat-DecimalDigits="0"
                            Value='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "AddQuota")) %>'
                            Width="90px">
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="110px" HeaderText="General - Online" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Additional">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtAddQuotaOnline" runat="server" NumberFormat-DecimalDigits="0"
                            Value='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "AddQuotaOnline")) %>'
                            Width="90px">
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="110px" HeaderText="BPJS - Direct" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Additional">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtAddQuotaBpjs" runat="server" NumberFormat-DecimalDigits="0"
                            Value='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "AddQuotaBpjs")) %>'
                            Width="90px">
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="110px" HeaderText="BPJS - Online" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Additional">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtAddQuotaBpjsOnline" runat="server" NumberFormat-DecimalDigits="0"
                            Value='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "AddQuotaBpjsOnline")) %>'
                            Width="90px">
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
