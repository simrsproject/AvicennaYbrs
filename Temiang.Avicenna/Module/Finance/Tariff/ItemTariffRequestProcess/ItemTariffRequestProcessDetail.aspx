<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="ItemTariffRequestProcessDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Tariff.ItemTariffRequestProcessDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTariffRequestNo" runat="server" Text="Tariff Request No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTariffRequestNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTariffRequestNo" runat="server" ErrorMessage="Tariff Request No required."
                                ValidationGroup="entry" ControlToValidate="txtTariffRequestNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTariffRequestDate" runat="server" Text="Tariff Request Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTariffRequestDate" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTariffRequestDate" runat="server" ErrorMessage="Tariff Request Date required."
                                ValidationGroup="entry" ControlToValidate="txtTariffRequestDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRItemType" runat="server" Text="Item Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRItemType" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="cboSRItemType_SelectedIndexChanged" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRItemType" runat="server" ErrorMessage="Item Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRItemType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemGroup" runat="server" Text="Item Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboItemGroup" Width="300px" runat="server" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboItemGroup_ItemDataBound"
                                OnItemsRequested="cboItemGroup_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ItemGroupID")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ItemGroupName")%>
                                    </b>
                                    <br />
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFromSRTariffType" runat="server" Text="From Tariff Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboFromSRTariffType" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvFromSRTariffType" runat="server" ErrorMessage="From Tariff Type required."
                                ValidationGroup="entry" ControlToValidate="cboFromSRTariffType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblToSRTariffType" runat="server" Text="To Tariff Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboToSRTariffType" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvToSRTariffType" runat="server" ErrorMessage="To Tariff Type required."
                                ValidationGroup="entry" ControlToValidate="cboToSRTariffType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblStartingDate" runat="server" Text="Starting Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtStartingDate" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvStartingDate" runat="server" ErrorMessage="Starting Date required."
                                ValidationGroup="entry" ControlToValidate="txtStartingDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFromDate" runat="server" Text="From Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtFromDate" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ErrorMessage="From Date required."
                                ValidationGroup="entry" ControlToValidate="txtFromDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRoundingValue" runat="server" Text="Rounding"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtRoundingValue" runat="server" Width="100px" NumberFormat-DecimalDigits="0"
                                MaxLength="6" MinValue="0" /> 
                            <asp:Label ID="lblCaptionRounding" runat="server" Text="** Will apply to each Tariff Component" ForeColor="Red"></asp:Label>
                            
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRoundingValue" runat="server" ErrorMessage="Rounding (Per Tariff Component) required."
                                ValidationGroup="entry" ControlToValidate="txtRoundingValue" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                     <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsRoundingDown" runat="server" Text="Rounding Down" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsApproved" runat="server" Text="Approved" Enabled="false" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblApprovedDate" runat="server" Text="Approved Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtApprovedDate" runat="server" Width="100px" Enabled="false" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsVoid" runat="server" Text="Void" Enabled="false" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblVoidDate" runat="server" Text="Void Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtVoidDate" runat="server" Width="100px" Enabled="False" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <telerik:RadGrid ID="grdItemComp" runat="server" AutoGenerateColumns="False"
                    GridLines="None" OnNeedDataSource="grdItemComp_NeedDataSource">
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="TariffComponentID">
                        <Columns>
                            <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="TariffComponentID" HeaderText="ID"
                                UniqueName="TariffComponentID" SortExpression="TariffComponentID" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="TariffComponentName" HeaderText="Tariff Component"
                                UniqueName="TariffComponentName" SortExpression="TariffComponentName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridTemplateColumn HeaderText="Amount Value" UniqueName="AmountValue">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox runat="server" ReadOnly="<%#DataModeCurrent == Temiang.Avicenna.Common.AppEnum.DataMode.Read %>" ID="txtAmountValue" Width="100px" DbValue='<%#Eval("AmountValue")%>'>
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="150px" HeaderText="In Percent">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkIsValueInPercent" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                                        Checked='<%#DataBinder.Eval(Container.DataItem, "IsValueInPercent") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>

