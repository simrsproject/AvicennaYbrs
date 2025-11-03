<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="MembershipItemRedemptionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.CRM.MembershipItemRedemptionDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script language="javascript" type="text/javascript">
            function rowProcessBalanceAmount(id) {
                if (confirm('Are you sure to process the balance amount into points?')) {
                    __doPostBack("<%= grdListDetail.UniqueID %>", 'calculation|' + id);
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvTransactionNo" runat="server" ErrorMessage="Transaction No required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="120px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTransactionDate" runat="server" ErrorMessage="Transaction Date required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMembershipNo" runat="server" Text="Member No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboMembershipNo" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboMembershipNo_ItemDataBound"
                                OnItemsRequested="cboMembershipNo_ItemsRequested" OnSelectedIndexChanged="cboMembershipNo_SelectedIndexChanged">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "MembershipNo")%>
                                    </b>
                                    &nbsp;- Join Date: &nbsp;
                                    <%# System.Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "JoinDate")).ToString(Temiang.Avicenna.Common.AppConstant.DisplayFormat.Date)%>
                                    <br />
                                    <b><%# DataBinder.Eval(Container.DataItem, "PatientName") %></b>
                                    Address :
                                    <%# DataBinder.Eval(Container.DataItem, "Address")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvMemberNo" runat="server" ErrorMessage="Member No required."
                                ValidationGroup="entry" ControlToValidate="cboMembershipNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trSRMembershipType">
                        <td class="label">
                            <asp:Label ID="lblSRMembershipType" runat="server" Text="Membership Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRMembershipType" runat="server" Width="300px" Enabled="false" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRMembershipType" runat="server" ErrorMessage="Membership Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRMembershipType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblJoinDate" runat="server" Text="Join Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtJoinDate" runat="server" Width="120px" Enabled="false" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvJoinDate" runat="server" ErrorMessage="Join Date required."
                                ValidationGroup="entry" ControlToValidate="txtJoinDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Member Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtSalutation" runat="server" Width="30px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="242px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="22px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPlaceDOB" runat="server" Text="City / Date Of Birth"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtAgeYear" runat="server" ReadOnly="true" Width="40px">
                                            <NumberFormat DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td width="20px">&nbsp;Y</td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtAgeMonth" runat="server" ReadOnly="true" Width="40px">
                                            <NumberFormat DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td width="20px">&nbsp;M</td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtAgeDay" runat="server" ReadOnly="true" Width="40px">
                                            <NumberFormat DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>

                                    </td>
                                    <td width="20px">&nbsp;D</td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAddress" runat="server" Width="300px" ReadOnly="true"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No / Mobile Phone No"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtPhoneNo" runat="server" Width="148px" ReadOnly="true" />
                                    </td>
                                    <td width="5px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtMobilePhone" runat="server" Width="148px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEmail" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientID" runat="server" Text="Redeemed By"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPatientID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboPatientID_ItemDataBound"
                                OnItemsRequested="cboPatientID_ItemsRequested" OnSelectedIndexChanged="cboPatientID_SelectedIndexChanged">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "PatientName")%>
                                    </b>&nbsp;-&nbsp;
                                    <%# System.Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth")).ToString(Temiang.Avicenna.Common.AppConstant.DisplayFormat.Date)%>
                                    <br />
                                    Address :
                                    <%# DataBinder.Eval(Container.DataItem, "Address")%>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPatientID" runat="server" ErrorMessage="Redeemed By required."
                                ValidationGroup="entry" ControlToValidate="cboPatientID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRedeemedBy" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRedeemedBy" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRedeemedByAddress" runat="server" Text="Address"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRedeemedByAddress" runat="server" Width="300px" ReadOnly="true"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRedeemedByPhoneNo" runat="server" Text="Phone No / Mobile Phone No"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtRedeemedByPhoneNo" runat="server" Width="148px" ReadOnly="true" />
                                    </td>
                                    <td width="5px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtRedeemedByMobilePhoneNo" runat="server" Width="148px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBalance" runat="server" Text="Balance Points"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtBalance" runat="server" Width="100px" NumberFormat-DecimalDigits="0" ReadOnly="true" Font-Bold="true" Font-Size="Large" ForeColor="DarkBlue" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <telerik:RadGrid ID="grdListDetail" runat="server" OnNeedDataSource="grdListDetail_NeedDataSource"
                                AutoGenerateColumns="False" GridLines="None">
                                <HeaderContextMenu>
                                </HeaderContextMenu>
                                <MasterTableView CommandItemDisplay="None" DataKeyNames="MembershipDetailID">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="StartDate" HeaderText="Active Date" UniqueName="StartDate"
                                            SortExpression="StartDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EndDate" HeaderText="Valid Thru" UniqueName="EndDate"
                                            SortExpression="EndDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="BalanceAmount" HeaderText="Balance Amount"
                                            UniqueName="BalanceAmount" SortExpression="RewardPoint" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RewardPoint" HeaderText="Reward Points"
                                            UniqueName="RewardPoint" SortExpression="RewardPoint" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="ClaimedPoint" HeaderText="Redeemed Points"
                                            UniqueName="ClaimedPoint" SortExpression="ClaimedPoint" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Balance" HeaderText="Balance Points"
                                            UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                                        <telerik:GridTemplateColumn UniqueName="calculation">
                                            <ItemTemplate>
                                                <%# (string.Format("<a href=\"#\" onclick=\"rowProcessBalanceAmount('{0}'); return false;\"><img src=\"../../../Images/Toolbar/refresh16.png\" border=\"0\" title=\"Process the balance amount into points\" /></a>",
                                                                                                            DataBinder.Eval(Container.DataItem, "MembershipDetailID")))%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="35px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                        <td></td>
                    </tr>
                </table>

            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdListItem" runat="server" OnNeedDataSource="grdListItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdListItem_UpdateCommand"
        OnDeleteCommand="grdListItem_DeleteCommand" OnInsertCommand="grdListItem_InsertCommand">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemReedemID">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="35px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemReedemID" HeaderText="ID"
                    UniqueName="ItemReedemID" SortExpression="ItemReedemID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ItemReedemName" HeaderText="Item Reedem Name" UniqueName="ItemReedemName"
                    SortExpression="ItemReedemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ItemReedemGroup" HeaderText="Group"
                    UniqueName="ItemReedemGroup" SortExpression="ItemReedemGroup" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Qty" HeaderText="Qty"
                    UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PointsUsed" HeaderText="Points Used"
                    UniqueName="PointsUsed" SortExpression="PointsUsed" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="TotalPointsUsed" HeaderText="Total"
                    UniqueName="TotalPointsUsed" SortExpression="TotalPointsUsed" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridTemplateColumn />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="35px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="MembershipItemRedemptionDetailItem.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="MembershipItemRedemptionDetailItemCommand">
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
</asp:Content>
