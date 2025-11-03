<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="RiskManagementDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.v2.RiskManagementDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%" border="0">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRiskManagementNo" runat="server" Text="Risk Management No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRiskManagementNo" runat="server" Width="300px" MaxLength="20" ReadOnly="true" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvRiskManagementNo" runat="server" ErrorMessage="Risk Management No required."
                                ValidationGroup="entry" ControlToValidate="txtRiskManagementNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPeriodYear" runat="server" Text="Year Period"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPeriodYear" runat="server" Width="50px">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvPeriodYear" runat="server" ErrorMessage="Year Period required."
                                ValidationGroup="entry" ControlToValidate="txtPeriodYear" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboServiceUnitID_ItemDataBound"
                                OnItemsRequested="cboServiceUnitID_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%" border="0">
                    <tr style="display: none">
                        <td class="label"></td>
                        <td>
                            <asp:CheckBox ID="chkIsApproved" Text="Approved" runat="server" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdRiskManagementItem" runat="server" OnNeedDataSource="grdRiskManagementItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdRiskManagementItem_DeleteCommand"
        OnInsertCommand="grdRiskManagementItem_InsertCommand">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="SequenceNo">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="RiskManagementCategoryName" HeaderText="Category"
                    UniqueName="RiskManagementCategoryName" SortExpression="RiskManagementCategoryName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RiskManagementDescription" HeaderText="Description" UniqueName="RiskManagementDescription"
                    SortExpression="RiskManagementDescription">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="RiskManagementImpactName" HeaderText="Impact"
                    UniqueName="RiskManagementImpactName" SortExpression="RiskManagementImpactName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="ImpactScore" HeaderText="Impact Score"
                    UniqueName="ImpactScore" SortExpression="ImpactScore" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="RiskManagementProbabilityName" HeaderText="Probability"
                    UniqueName="RiskManagementProbabilityName" SortExpression="RiskManagementProbabilityName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="ProbabilityScore" HeaderText="Probability Score"
                    UniqueName="ProbabilityScore" SortExpression="ProbabilityScore" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" />

                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="RiskScore" HeaderText="Risk Score"
                    UniqueName="RiskScore" SortExpression="RiskScore" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" />

                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="RiskManagementBandsName" HeaderText="Bands"
                    UniqueName="RiskManagementBandsName" SortExpression="RiskManagementBandsName" Visible="false">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="80px" DataField="RiskManagementBandsName" HeaderText="Bands" UniqueName="BackColorTemplateColumn">
                    <ItemTemplate>
                        <div style="width: 100%; background-color: <%#DataBinder.Eval(Container.DataItem,"RiskManagementBandsColor")%>">
                            <%#DataBinder.Eval(Container.DataItem,"RiskManagementBandsName")%>
                        </div>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>

                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="RiskManagementControllingName" HeaderText="Controlling"
                    UniqueName="RiskManagementControllingName" SortExpression="RiskManagementControllingName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="ControllingScore" HeaderText="Controlling Score"
                    UniqueName="ControllingScore" SortExpression="ControllingScore" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" />

                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="TotalScore" HeaderText="Total Score"
                    UniqueName="TotalScore" SortExpression="TotalScore" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" />

                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="RiskRating" HeaderText="Risk Rating"
                    UniqueName="RiskRating" SortExpression="RiskRating" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" />

                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="Reason" HeaderText="Reason"
                    UniqueName="Reason" SortExpression="Reason">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="Action" HeaderText="Action"
                    UniqueName="Action" SortExpression="Action">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="Pic" HeaderText="PIC"
                    UniqueName="Pic" SortExpression="Pic">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="RiskManagementItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="grdRiskManagementItemEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="True">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
