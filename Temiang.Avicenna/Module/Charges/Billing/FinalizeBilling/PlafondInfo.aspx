<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="PlafondInfo.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.Billing.FinalizeBilling.PlafondInfo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry" />
    <asp:CustomValidator ID="customValidator" ValidationGroup="entry" runat="server"></asp:CustomValidator>
    <table width="100%">
        <tr>
            <td>
                <fieldset>
                    <legend>PLAFOND UPDATE HISTORY</legend>
                    <table width="100%">
                        <tr>
                            <td>
                                <telerik:RadGrid ID="grdPlafondHistory" runat="server" OnNeedDataSource="grdPlafondHistory_NeedDataSource"
                                    AutoGenerateColumns="False" GridLines="None" AllowPaging="true" PageSize="15"
                                    AllowSorting="False">
                                    <MasterTableView CommandItemDisplay="None" DataKeyNames="HistoryID" GroupLoadMode="Client">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="LastUpdateDateTime" HeaderText="Date & Time"
                                                UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime" DataType="System.DateTime"
                                                DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                                                SortExpression="GuarantorName">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="PlafondAmount" HeaderText="Plafond Amount"
                                                UniqueName="PlafondAmount" SortExpression="PlafondAmount" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                            <telerik:GridBoundColumn DataField="LastUpdateByUserID" HeaderText="Update By" UniqueName="LastUpdateByUserID"
                                                SortExpression="LastUpdateByUserID" HeaderStyle-Width="120px">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <NestedViewTemplate>
                                            <asp:Label runat="server" ID="lblNoSEP" Text='<%# Eval("BpjsSepNo") %>' Visible="false" />
                                            <table runat="server" id="InnerContainer" width="100%">
                                                <tr>
                                                    <td width="33%" valign="top">
                                                        <telerik:RadGrid ID="grdGrouper" runat="server" AutoGenerateColumns="false" DataSourceID="odsGrouper">
                                                            <MasterTableView DataKeyNames="NoSEP">
                                                                <Columns>
                                                                    <telerik:GridTemplateColumn UniqueName="TemplateColumn1" HeaderText="Grouper">
                                                                        <ItemTemplate>
                                                                            <%# DataBinder.Eval(Container.DataItem, "KodeCBG")%><br />
                                                                            <%# DataBinder.Eval(Container.DataItem, "DeskripsiCBG")%><br />
                                                                            <%# DataBinder.Eval(Container.DataItem, "TariffCBG")%>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                        <asp:ObjectDataSource runat="server" ID="odsGrouper" SelectMethod="SelectGrouper"
                                                            TypeName="Temiang.Avicenna.Module.Charges.Billing.FinalizeBilling.PlafondInfo">
                                                            <SelectParameters>
                                                                <asp:ControlParameter Name="noSEP" ControlID="lblNoSEP" PropertyName="Text" Type="String" />
                                                            </SelectParameters>
                                                        </asp:ObjectDataSource>
                                                    </td>
                                                    <td width="34%" valign="top">
                                                        <telerik:RadGrid ID="grdCmg" runat="server" AutoGenerateColumns="false" DataSourceID="odsCmg">
                                                            <MasterTableView DataKeyNames="NoSEP">
                                                                <Columns>
                                                                    <telerik:GridTemplateColumn UniqueName="TemplateColumn1" HeaderText="Special CMG">
                                                                        <ItemTemplate>
                                                                            <%# DataBinder.Eval(Container.DataItem, "KodeCMG")%><br />
                                                                            <%# DataBinder.Eval(Container.DataItem, "DeskripsiCMG")%><br />
                                                                            <%# DataBinder.Eval(Container.DataItem, "TariffCMG")%><br />
                                                                            <%# DataBinder.Eval(Container.DataItem, "TipeCMG")%>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                        <asp:ObjectDataSource runat="server" ID="odsCmg" SelectMethod="SelectCmg" TypeName="Temiang.Avicenna.Module.Charges.Billing.FinalizeBilling.PlafondInfo">
                                                            <SelectParameters>
                                                                <asp:ControlParameter Name="noSEP" ControlID="lblNoSEP" PropertyName="Text" Type="String" />
                                                            </SelectParameters>
                                                        </asp:ObjectDataSource>
                                                    </td>
                                                    <td width="33%" valign="top">
                                                        <telerik:RadGrid ID="grdCmgOption" runat="server" AutoGenerateColumns="false" DataSourceID="odsCmgOption">
                                                            <MasterTableView DataKeyNames="NoSEP">
                                                                <Columns>
                                                                    <telerik:GridTemplateColumn UniqueName="TemplateColumn1" HeaderText="Special CMG Option">
                                                                        <ItemTemplate>
                                                                            <%# DataBinder.Eval(Container.DataItem, "KodeCMG")%><br />
                                                                            <%# DataBinder.Eval(Container.DataItem, "DeskripsiCMG")%><br />
                                                                            <%# DataBinder.Eval(Container.DataItem, "TipeCMG")%>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                        <asp:ObjectDataSource runat="server" ID="odsCmgOption" SelectMethod="SelectCmgOption"
                                                            TypeName="Temiang.Avicenna.Module.Charges.Billing.FinalizeBilling.PlafondInfo">
                                                            <SelectParameters>
                                                                <asp:ControlParameter Name="noSEP" ControlID="lblNoSEP" PropertyName="Text" Type="String" />
                                                            </SelectParameters>
                                                        </asp:ObjectDataSource>
                                                    </td>
                                                </tr>
                                            </table>
                                        </NestedViewTemplate>
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
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
