<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    Codebehind="EDCMachineDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.EDCMachineDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEDCMachineID" runat="server" Text="EDC Machine ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEDCMachineID" runat="server" Width="300px" MaxLength="10" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvEDCMachineID" runat="server" ErrorMessage="EDC Machine ID required."
                                ValidationGroup="entry" ControlToValidate="txtEDCMachineID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEDCMachineName" runat="server" Text="EDC Machine Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEDCMachineName" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvEDCMachineName" runat="server" ErrorMessage="E DC Machine Name required."
                                ValidationGroup="entry" ControlToValidate="txtEDCMachineName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRCardProvider" runat="server" Text="Card Provider"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRCardProvider" runat="server" Width="304px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRCardProvider" runat="server" ErrorMessage="Card Provider required."
                                ValidationGroup="entry" ControlToValidate="cboSRCardProvider" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountId" runat="server" Text="Chart Of Account"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountId" Height="190px" Width="300px"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountId_SelectedIndexChanged"
                                OnItemDataBound="cboChartOfAccountId_ItemDataBound" OnItemsRequested="cboChartOfAccountId_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSubledgerId" runat="server" Text="Subledger"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerId" Height="190px" Width="300px"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerId_ItemDataBound" OnItemsRequested="cboSubledgerId_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%> 
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%> 
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            
                        </td>
                        <td>
                        </td>
                    </tr> 
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <telerik:RadGrid ID="grdEDCMachineTariff" runat="server" OnNeedDataSource="grdEDCMachineTariff_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEDCMachineTariff_UpdateCommand"
                    OnDeleteCommand="grdEDCMachineTariff_DeleteCommand" OnInsertCommand="grdEDCMachineTariff_InsertCommand">
                    <HeaderContextMenu>
                        
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="EDCMachineID, SRCardType">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                <HeaderStyle Width="30px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn DataField="CardTypeName" HeaderText="Card Type"
                                UniqueName="CardTypeName" SortExpression="CardTypeName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EDCMachineTariff"
                                HeaderText="Card Fee (%)" UniqueName="EDCMachineTariff" SortExpression="EDCMachineTariff"
                                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="AddFeeAmount"
                                HeaderText="Additional Card Fee Amount" UniqueName="AddFeeAmount" SortExpression="AddFeeAmount"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            <telerik:GridTemplateColumn HeaderStyle-Width="10px"/>
                            <telerik:GridBoundColumn DataField="ChartOfAccountCode" HeaderText="Card Fee Chart Of Account Code"
                                UniqueName="ChartOfAccountCode" SortExpression="ChartOfAccountCode" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="ChartOfAccountName" HeaderText="Card Fee Chart Of Account Name"
                                UniqueName="ChartOfAccountName" SortExpression="ChartOfAccountName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsChargedToPatient" HeaderText="Charged To Patient"
                                UniqueName="IsChargedToPatient" SortExpression="IsChargedToPatient" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                                UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings UserControlName="EDCMachineTariffDetail.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="EDCMachineTariffEditCommand">
                            </EditColumn>
                        </EditFormSettings>
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
