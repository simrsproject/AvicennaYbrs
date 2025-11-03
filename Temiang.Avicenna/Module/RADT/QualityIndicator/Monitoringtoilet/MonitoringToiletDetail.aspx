<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="MonitoringToiletDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.MonitoringToiletDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTransactionNo" runat="server" ErrorMessage="Transaction No required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" OnSelectedDateChanged="txtTransactionDate_SelectedDateChanged"
                                AutoPostBack="True">
                            </telerik:RadDatePicker>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTransactionDate" runat="server" ErrorMessage="Transaction Date required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>                   
                    <tr>
                            <td class="label">
                                <asp:Label ID="lblUserName" runat="server" Text="User"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtUserName" runat="server" Width="300px" ReadOnly="true" />
                            </td>
                            <td width="20">
                            </td>
                            <td>
                            </td>
                        </tr>                        
                    <tr>
                            <td class="label">
                                <asp:Label ID="Label4" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboServiceUnitID_ItemsRequested"
                                    OnItemDataBound="cboServiceUnitID_ItemDataBound">
                                    <FooterTemplate>
                                        Note : Show max 15 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                            </td>
                            <td>
                            </td>
                        </tr>
                </table>
            </td>          
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" >       
        <MasterTableView CommandItemDisplay="None" DataKeyNames="SRMonitoringToilet">
            <Columns>               
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="SRMonitoringToilet" HeaderText="ID"
                    UniqueName="SRMonitoringToilet" SortExpression="SRMonitoringToilet" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="MonitoringToiletName" HeaderText="Monitoring Toilet"
                    UniqueName="MonitoringToiletName" SortExpression="MonitoringToiletName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />                                                         
                <telerik:GridTemplateColumn UniqueName="chkIsYes" HeaderText="Yes">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsYes") %>'
                                    ID="chkIsYes" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="chkIsNotApplicable" HeaderText="N/A" >
                            <HeaderStyle Width="100px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsNotApplicable") %>'
                                    ID="chkIsNotApplicable" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn />                
            </Columns>                     
        </MasterTableView>        
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="false" />
        </ClientSettings>
    </telerik:RadGrid>
     <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRecommendation" runat="server" Text="Recommendation"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRecommendation" runat="server" Width="300px" MaxLength="255" TextMode="MultiLine" />
                        </td>
                        <td width="20">
                        </td>
                    </tr>
                </table>
            </td>                    
        </tr>
    </table>
</asp:Content>
