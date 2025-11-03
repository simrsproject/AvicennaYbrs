<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="MembershipDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.CRM.MembershipDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script language="javascript" type="text/javascript">
            var fnNo = 0;
            function openPaymentList(id) {
                fnNo = 1;
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.SetUrl("MembershipRewardList.aspx?mid=" + id);
                oWnd.Show();
                oWnd.Maximize();
            }
            function openClaimedList(id) {
                fnNo = 2;
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.SetUrl("MembershipRedeemList.aspx?mid=" + id);
                oWnd.Show();
                oWnd.Maximize();
            }
            function rowProcessBalanceAmount(id) {
                if (confirm('Are you sure to process the balance amount into points?')) {
                    __doPostBack("<%= grdMembership.UniqueID %>", 'calculation|' + id);
                }
            }
            function rowClose(id) {
                if (confirm('Are you sure to close this data?')) {
                    __doPostBack("<%= grdMembership.UniqueID %>", 'close|' + id);
                }
            }
            function rowOpen(id) {
                if (confirm('Are you sure to open this data?')) {
                    __doPostBack("<%= grdMembership.UniqueID %>", 'open|' + id);
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winDialog" Animation="None" Width="800px" Height="500px"
        runat="server" Behavior="Maximize,Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMembershipNo" runat="server" Text="Member No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMembershipNo" runat="server" Width="300px" MaxLength="50" ReadOnly="True" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvMembershipNo" runat="server" ErrorMessage="Membership No required."
                                ValidationGroup="entry" ControlToValidate="txtMembershipNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trSRMembershipType">
                        <td class="label">
                            <asp:Label ID="lblSRMembershipType" runat="server" Text="Membership Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRMembershipType" runat="server" Width="300px" />
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
                            <telerik:RadDatePicker ID="txtJoinDate" runat="server" Width="120px" />
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
                    <tr runat="server" id="trPatientID">
                        <td class="label">
                            <asp:Label ID="lblPatientID" runat="server" Text="Patient ID"></asp:Label>
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
                                    <%# DataBinder.Eval(Container.DataItem, "MedicalNo") %>
                                    &nbsp;|&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "PatientID") %>
                                    <br />
                                    Address :
                                    <%# DataBinder.Eval(Container.DataItem, "Address")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPatientID" runat="server" ErrorMessage="Patient ID required."
                                ValidationGroup="entry" ControlToValidate="cboPatientID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trMedicalNo">
                        <td class="label">
                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trPersonID">
                        <td class="label">
                            <asp:Label ID="lblPersonID" runat="server" Text="Employee No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPersonID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboPersonID_ItemDataBound"
                                OnItemsRequested="cboPersonID_ItemsRequested" OnSelectedIndexChanged="cboPersonID_SelectedIndexChanged">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>
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
                            <asp:Label ID="lblPatientName" runat="server" Text="Member Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboSRSalutation" runat="server" Width="55px" AutoPostBack="true"
                                            OnSelectedIndexChanged="cboSRSalutation_SelectedIndexChanged" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="243px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSex" runat="server" Text="Gender"></asp:Label>
                        </td>
                        <td class="entry300">
                            <asp:RadioButtonList ID="rbtSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="M" Text="Male" />
                                <asp:ListItem Value="F" Text="Female" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="10px">
                            <asp:RequiredFieldValidator ID="rfvSex" runat="server" ErrorMessage="Gender required."
                                ValidationGroup="entry" ControlToValidate="rbtSex" SetFocusOnError="True" Width="20px">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCityOfBirth" runat="server" Text="City / Date Of Birth*"></asp:Label>
                        </td>
                        <td class="entry300">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 190px; vertical-align: top">
                                        <telerik:RadTextBox ID="txtCityOfBirth" runat="server" Width="180px" MaxLength="50">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td style="width: 10px; vertical-align: middle;">/
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtDateOfBirth" runat="server" Width="100px" AutoPostBack="true"
                                            OnSelectedDateChanged="txtDateOfBirth_SelectedDateChanged">
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="10px">
                            <asp:RequiredFieldValidator ID="rfvCityOfBirth" runat="server" ErrorMessage="City Of Birth required."
                                ValidationGroup="entry" ControlToValidate="txtCityOfBirth" SetFocusOnError="True"
                                Width="20px">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvDateOfBirth" runat="server" ErrorMessage="Date Of Birth required."
                                ValidationGroup="entry" ControlToValidate="txtDateOfBirth" SetFocusOnError="True"
                                Width="20px">
                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtAgeYear" runat="server" ReadOnly="true" Width="40px">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;Y&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeMonth" runat="server" ReadOnly="true" Width="40px">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;M&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeDay" runat="server" ReadOnly="true" Width="40px">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;D
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAddress" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No / Mobile Phone No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPhoneNo" runat="server" Width="148px" MaxLength="50" />
                            <telerik:RadTextBox ID="txtMobilePhone" runat="server" Width="150px" MaxLength="50" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEmail" runat="server" Width="300px" MaxLength="50" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <fieldset id="FieldSet1" style="width: 150px; min-height: 150px;">
                    <legend>Photo</legend>
                    <asp:Image runat="server" ID="imgPatientPhoto" Width="150px" Height="150px" />
                </fieldset>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Members" PageViewID="pgMembers" Selected="True" />
            <telerik:RadTab runat="server" Text="Reward Information" PageViewID="pgInfo" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgMembers" runat="server" Selected="true">
            <telerik:RadGrid ID="grdMembershipMember" runat="server" OnNeedDataSource="grdMembershipMember_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdMembershipMember_UpdateCommand"
                OnDeleteCommand="grdMembershipMember_DeleteCommand" OnInsertCommand="grdMembershipMember_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="PatientID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                            SortExpression="MedicalNo">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PatientName" HeaderText="Member Name" UniqueName="PatientName"
                            SortExpression="PatientName" HeaderStyle-Width="275px">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DateOfBirth" HeaderText="Date Of Birth" UniqueName="DateOfBirth"
                            SortExpression="DateOfBirth" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Address" HeaderText="Address" UniqueName="Address"
                            SortExpression="Address">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PhoneNo" HeaderText="Phone No" UniqueName="PhoneNo"
                            SortExpression="PhoneNo">
                            <HeaderStyle HorizontalAlign="Left" Width="120px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MobilePhoneNo" HeaderText="Mobile Phone No" UniqueName="MobilePhoneNo"
                            SortExpression="MobilePhoneNo">
                            <HeaderStyle HorizontalAlign="Left" Width="120px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Email" HeaderText="Email" UniqueName="Email"
                            SortExpression="Email">
                            <HeaderStyle HorizontalAlign="Left" Width="120px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsActive" HeaderText="Active" UniqueName="IsActive"
                            SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="MembershipMemberItem.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="MembershipMemberItemCommand">
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
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgInfo" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="tblDateRange">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDateRange" runat="server" Text="Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtStartDate" runat="server" Width="100px" Enabled="false">
                                    </telerik:RadDatePicker>
                                    to
                                    <telerik:RadDatePicker ID="txtEndDate" runat="server" Width="100px" Enabled="false">
                                    </telerik:RadDatePicker>
                                </td>
                                <td width="20">
                                    <asp:ImageButton ID="btnFilter" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdMembership" runat="server" OnNeedDataSource="grdMembership_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdMembership_UpdateCommand"
                OnDeleteCommand="grdMembership_DeleteCommand" OnInsertCommand="grdMembership_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="MembershipDetailID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
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
                        <telerik:GridBoundColumn DataField="StartDate" HeaderText="Date" UniqueName="StartDate"
                            SortExpression="StartDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No" UniqueName="RegistrationNo"
                            SortExpression="RegistrationNo">
                            <HeaderStyle HorizontalAlign="Center" Width="145px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                            SortExpression="MedicalNo">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName" HeaderStyle-Width="250px">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                            SortExpression="GuarantorName" HeaderStyle-Width="250px">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                            SortExpression="ServiceUnitName" HeaderStyle-Width="250px">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="TotalAmount" HeaderText="Total Amount"
                            UniqueName="TotalAmount" SortExpression="TotalAmount" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="ReedeemAmount" HeaderText="Reedeem Amount"
                            UniqueName="ReedeemAmount" SortExpression="ReedeemAmount" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="BalanceAmount" HeaderText="Balance Amount"
                            UniqueName="BalanceAmount" SortExpression="BalanceAmount" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RewardPoint" HeaderText="Reward Points"
                            UniqueName="RewardPoint" SortExpression="RewardPoint" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RewardPointRefferal" HeaderText="Reward Points Refferal"
                            UniqueName="RewardPointRefferal" SortExpression="RewardPointRefferal" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="ClaimedPoint" HeaderText="Redeemed Points"
                            UniqueName="ClaimedPoint" SortExpression="ClaimedPoint" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="Balance" HeaderText="Balance Points"
                            UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsClosed" HeaderText="Closed" UniqueName="IsClosed"
                            SortExpression="IsClosed" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn UniqueName="openclose">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsClosed").Equals(true) ? string.Format("<a href=\"#\" onclick=\"rowOpen('{0}'); return false;\"><img src=\"../../../Images/ok16.png\" border=\"0\" title=\"Open\" /></a>",
                                                                                                            DataBinder.Eval(Container.DataItem, "MembershipDetailID")) : string.Format("<a href=\"#\" onclick=\"rowClose('{0}'); return false;\"><img src=\"../../../Images/cancel16.png\" border=\"0\" title=\"Close\" /></a>",
                                                                                                            DataBinder.Eval(Container.DataItem, "MembershipDetailID")))%>
                            </ItemTemplate>
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="calculation">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsClosed").Equals(true) ? string.Empty : string.Format("<a href=\"#\" onclick=\"rowProcessBalanceAmount('{0}'); return false;\"><img src=\"../../../Images/Toolbar/refresh16.png\" border=\"0\" title=\"Process the balance amount into points\" /></a>",
                                                                                                            DataBinder.Eval(Container.DataItem, "MembershipDetailID")))%>
                            </ItemTemplate>
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="reward">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openPaymentList('{0}'); return false;\"><img src=\"../../../Images/rp16.png\" border=\"0\" title=\"Payment Transaction List\" /></a>",
                                                                                                            DataBinder.Eval(Container.DataItem, "MembershipDetailID"))%>
                            </ItemTemplate>
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="claimed">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openClaimedList('{0}'); return false;\"><img src=\"../../../Images/redeem16.png\" border=\"0\" title=\"Redeemed List\" /></a>",
                                                                                                            DataBinder.Eval(Container.DataItem, "MembershipDetailID"))%>
                            </ItemTemplate>
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="MembershipDetailItem.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="MembershipDetailItemCommand">
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
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
