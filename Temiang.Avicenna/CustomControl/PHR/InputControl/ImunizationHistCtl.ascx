<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImunizationHistCtl.ascx.cs" Inherits="Temiang.Avicenna.CustomControl.Phr.InputControl.ImunizationHistCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<fieldset>
    <legend><b>IMMUNIZATION HISTORY</b></legend>

    <telerik:RadGrid ID="grdImunizationHist" runat="server" Width="100%" RenderMode="Lightweight" AutoGenerateColumns="False" EnableViewState="true"
        GridLines="None" OnNeedDataSource="grdImunizationHist_NeedDataSource" OnItemDataBound="grdImunizationHist_ItemDataBound">
        <MasterTableView DataKeyNames="ImmunizationID" ShowHeader="True" ShowHeadersWhenNoRecords="True">
            <Columns>
                <telerik:GridBoundColumn DataField="ImmunizationID" Display="False" UniqueName="ImmunizationID" />
                <telerik:GridBoundColumn DataField="MaxCount" Display="False" UniqueName="MaxCount" />
                <telerik:GridBoundColumn DataField="ReferenceNo" Display="False" UniqueName="ReferenceNo" />
                <telerik:GridBoundColumn DataField="Date_01" Display="False" UniqueName="Date_01" />
                <telerik:GridBoundColumn DataField="Date_02" Display="False" UniqueName="Date_02" />
                <telerik:GridBoundColumn DataField="Date_03" Display="False" UniqueName="Date_03" />
                <telerik:GridBoundColumn DataField="Date_04" Display="False" UniqueName="Date_04" />
                <telerik:GridBoundColumn DataField="Date_05" Display="False" UniqueName="Date_05" />
                <telerik:GridBoundColumn DataField="Date_06" Display="False" UniqueName="Date_06" />
                <telerik:GridBoundColumn DataField="Date_07" Display="False" UniqueName="Date_07" />
                <telerik:GridBoundColumn DataField="Date_08" Display="False" UniqueName="Date_08" />
                <telerik:GridBoundColumn DataField="Date_09" Display="False" UniqueName="Date_09" />
                <telerik:GridBoundColumn DataField="Date_10" Display="False" UniqueName="Date_10" />
                <telerik:GridBoundColumn DataField="IsChecked_01" Display="False" UniqueName="IsChecked_01" />
                <telerik:GridBoundColumn DataField="IsChecked_02" Display="False" UniqueName="IsChecked_02" />
                <telerik:GridBoundColumn DataField="IsChecked_03" Display="False" UniqueName="IsChecked_03" />
                <telerik:GridBoundColumn DataField="IsChecked_04" Display="False" UniqueName="IsChecked_04" />
                <telerik:GridBoundColumn DataField="IsChecked_05" Display="False" UniqueName="IsChecked_05" />
                <telerik:GridBoundColumn DataField="IsChecked_06" Display="False" UniqueName="IsChecked_06" />
                <telerik:GridBoundColumn DataField="IsChecked_07" Display="False" UniqueName="IsChecked_07" />
                <telerik:GridBoundColumn DataField="IsChecked_08" Display="False" UniqueName="IsChecked_08" />
                <telerik:GridBoundColumn DataField="IsChecked_09" Display="False" UniqueName="IsChecked_09" />
                <telerik:GridBoundColumn DataField="IsChecked_10" Display="False" UniqueName="IsChecked_10" />

                <telerik:GridBoundColumn DataField="ImmunizationName" UniqueName="ImmunizationName" HeaderText="Immunization" HeaderStyle-Width="200px" />
                <telerik:GridTemplateColumn UniqueName="InputDate_01" HeaderText="1">
                    <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td rowspan="2">
                                    <asp:CheckBox runat="server" ID="chkDate_01" /></td>
                                <td></td>
                                <td>
                                    <telerik:RadMonthYearPicker ID="txtMonthYear_01" runat="server" Width="120px" />
                                </td>
                            </tr>

                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="InputDate_02" HeaderText="2">
                    <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td rowspan="2">
                                    <asp:CheckBox runat="server" ID="chkDate_02" /></td>
                                <td>
                                    <telerik:RadMonthYearPicker ID="txtMonthYear_02" runat="server" Width="120px" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="InputDate_03" HeaderText="3">
                    <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td rowspan="2">
                                    <asp:CheckBox runat="server" ID="chkDate_03" /></td>
                                <td>
                                    <telerik:RadMonthYearPicker ID="txtMonthYear_03" runat="server" Width="120px" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="InputDate_04" HeaderText="4">
                    <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td rowspan="2">
                                    <asp:CheckBox runat="server" ID="chkDate_04" /></td>
                                <td>
                                    <telerik:RadMonthYearPicker ID="txtMonthYear_04" runat="server" Width="120px" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="InputDate_05" HeaderText="5">
                    <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td rowspan="2">
                                    <asp:CheckBox runat="server" ID="chkDate_05" /></td>
                                <td>
                                    <telerik:RadMonthYearPicker ID="txtMonthYear_05" runat="server" Width="120px" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="InputDate_06" HeaderText="6">
                    <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td rowspan="2">
                                    <asp:CheckBox runat="server" ID="chkDate_06" /></td>
                                <td>
                                    <telerik:RadMonthYearPicker ID="txtMonthYear_06" runat="server" Width="120px" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="InputDate_07" HeaderText="7">
                    <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td rowspan="2">
                                    <asp:CheckBox runat="server" ID="chkDate_07" /></td>
                                <td>
                                    <telerik:RadMonthYearPicker ID="txtMonthYear_07" runat="server" Width="120px" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="InputDate_08" HeaderText="8">
                    <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td rowspan="2">
                                    <asp:CheckBox runat="server" ID="chkDate_08" /></td>
                                <td>
                                    <telerik:RadMonthYearPicker ID="txtMonthYear_08" runat="server" Width="120px" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="InputDate_09" HeaderText="9">
                    <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td rowspan="2">
                                    <asp:CheckBox runat="server" ID="chkDate_09" /></td>
                                <td>
                                    <telerik:RadMonthYearPicker ID="txtMonthYear_09" runat="server" Width="120px" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="InputDate_10" HeaderText="10">
                    <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td rowspan="2">
                                    <asp:CheckBox runat="server" ID="chkDate_10" /></td>
                                <td>
                                    <telerik:RadMonthYearPicker ID="txtMonthYear_10" runat="server" Width="120px" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="BlankCol" />
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="False" AllowGroupExpandCollapse="False">
            <Resizing AllowColumnResize="False" />
        </ClientSettings>
    </telerik:RadGrid>
</fieldset>
