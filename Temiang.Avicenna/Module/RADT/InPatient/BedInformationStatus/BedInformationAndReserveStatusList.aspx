<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="BedInformationAndReserveStatusList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.InPatient.BedInformationAndReserveStatusList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function rowDelete(bedId) {
                if (confirm('Are you sure to clear selected bed?')) {
                    __doPostBack("<%= grdList.UniqueID %>", 'clear|' + bedId);
                }
            }

            function rowReleased(id) {
                if (confirm('Are you sure to released selected transaction?')) {
                    __doPostBack("<%= grdList.UniqueID %>", 'released|' + id);
                }
            }

            function rowVoid(id) {
                if (confirm('Are you sure to void selected transaction?')) {
                    __doPostBack("<%= grdList.UniqueID %>", 'void|' + id);
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdBedManagement" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboRoomID" />
                    <telerik:AjaxUpdatedControl ControlID="cboClassID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRoomID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdBedManagement" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboRoomID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboClassID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterClassID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdBedManagement" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchPatient">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterIsRoomIn">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterBedStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterGender">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterReligion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterAddress">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchClosed">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdBedManagement" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPatientSearch" runat="server" Text="Patient Name / Medical No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientSearch" runat="server" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnSearchPatient" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr runat="server" id="trReligion">
                            <td class="label">
                                <asp:Label ID="lblReligion" runat="server" Text="Religion"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboSRReligion" Width="300px" AllowCustomText="true"
                                    MarkFirstMatch="true">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterReligion" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr runat="server" id="trAddress">
                            <td class="label">
                                <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtAddress" runat="server" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterAddress" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label"></td>
                            <td class="entry">
                                <asp:CheckBox runat="server" ID="chkIsClosed" Text="Closed" />
                            </td>
                            <td width="20">
                                <asp:ImageButton runat="server" ID="btnSearchClosed" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr runat="server" id="trRoomIn">
                            <td class="label"></td>
                            <td class="entry">
                                <asp:CheckBox runat="server" ID="chkIsRoomIn" Text="Rooming In" />
                            </td>
                            <td width="20">
                                <asp:ImageButton runat="server" ID="btnFilterIsRoomIn" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblGender" runat="server" Text="Gender"></asp:Label>
                            </td>
                            <td class="entry">
                                <asp:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0" Selected="true">All</asp:ListItem>
                                    <asp:ListItem Value="1">Male</asp:ListItem>
                                    <asp:ListItem Value="2">Female</asp:ListItem>
                                    <asp:ListItem Value="3">All Male</asp:ListItem>
                                    <asp:ListItem Value="4">All Female</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td width="20">
                                <asp:ImageButton runat="server" ID="btnFilterGender" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBedStatus" runat="server" Text="Bed Status"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboBedStatus" Width="300px" AllowCustomText="true"
                                    MarkFirstMatch="true">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterBedStatus" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="300px" AllowCustomText="true"
                                    Filter="Contains" OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterServiceUnitID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRoomID" runat="server" Text="Room"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboRoomID" Width="300px" AutoPostBack="True"
                                    EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                                    OnItemDataBound="cboRoomID_ItemDataBound" OnItemsRequested="cboRoomID_ItemsRequested"
                                    OnSelectedIndexChanged="cboRoomID_SelectedIndexChanged">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "RoomName")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterRoomID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblClassID" runat="server" Text="Class"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboClassID" Width="300px" EnableLoadOnDemand="True"
                                    HighlightTemplatedItems="True" MarkFirstMatch="False" OnItemDataBound="cboClassID_ItemDataBound"
                                    OnItemsRequested="cboClassID_ItemsRequested">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "ClassName")%>
                                    </ItemTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterClassID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>

                        <tr>
                            <td class="label"></td>
                            <td class="entry" colspan="3">
                                <telerik:RadTextBox ID="txtReady" runat="server" Width="25px">
                                </telerik:RadTextBox>&nbsp;Ready&nbsp;
                                <telerik:RadTextBox ID="txtOccupied" runat="server" Width="25px">
                                </telerik:RadTextBox>&nbsp;Occupied&nbsp;
                                <telerik:RadTextBox ID="txtBooked" runat="server" Width="25px">
                                </telerik:RadTextBox>&nbsp;Booked&nbsp;
                                <telerik:RadTextBox ID="txtPending" runat="server" Width="25px">
                                </telerik:RadTextBox>&nbsp;Pending&nbsp;
                                <telerik:RadTextBox ID="txtCleaning" runat="server" Width="25px">
                                </telerik:RadTextBox>&nbsp;Cleaning&nbsp;
                                <telerik:RadTextBox ID="txtReserved" runat="server" Width="25px">
                                </telerik:RadTextBox>&nbsp;Reserved&nbsp;
                                <telerik:RadTextBox ID="txtRepaired" runat="server" Width="25px">
                                </telerik:RadTextBox>&nbsp;Repaired&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                                OnItemDataBound="grdList_ItemDataBound" AllowPaging="true" PageSize="15" AutoGenerateColumns="false">
                                <MasterTableView DataKeyNames="BedID" GroupLoadMode="client">
                                    <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Service Unit "></telerik:GridGroupByField>
                                            </SelectFields>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="ServiceUnitID" SortOrder="Ascending"></telerik:GridGroupByField>
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                        <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldName="RoomName" HeaderText="Room "></telerik:GridGroupByField>
                                            </SelectFields>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="RoomID" SortOrder="Ascending"></telerik:GridGroupByField>
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                    <Columns>
                                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px">
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "IsAttention").Equals(true)) ? string.Format("<img src=\"../../../../Images/Animated/warning16.gif\" border=\"0\" title=\"{0}\" />", DataBinder.Eval(Container.DataItem, "AttentionNotes"))  : string.Empty%>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="BedID" HeaderText="Bed No"
                                            UniqueName="BedID" SortExpression="BedID" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ClassName" HeaderText="Class"
                                            UniqueName="ClassName" SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridTemplateColumn UniqueName="BedStatusColor" HeaderStyle-Width="50px" HeaderText="Bed Status"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtBedStatusColor" runat="server" Width="20px" BackColor='<%# GetColor(DataBinder.Eval(Container.DataItem,"SRBedStatus")) %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemName" HeaderText="Bed Status"
                                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsRoomIn" HeaderText="Rooming In"
                                            UniqueName="IsRoomIn" SortExpression="IsRoomIn" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="RegistrationNo" HeaderText="Registration No"
                                            UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="MedicalNo" HeaderText="Medical No"
                                            UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SalutationName" HeaderText=""
                                            UniqueName="SalutationName" SortExpression="SalutationName" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                                        <telerik:GridTemplateColumn HeaderStyle-Width="250px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                                            SortExpression="PatientName">
                                            <ItemTemplate>
                                                <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" HeaderStyle-Width="60px"
                                            SortExpression="Sex" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ReligionName" HeaderText="Religion"
                                            UniqueName="ReligionName" SortExpression="ReligionName" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                                            SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                                        <telerik:GridBoundColumn DataField="DischargePlanDate" HeaderStyle-Width="90px" DataFormatString="{0:dd/MM/yyyy}"
                                            UniqueName="DischargePlanDate" HeaderText="Discharge Plan Date" SortExpression="DischargePlanDate"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsClosed" HeaderText="Closed"
                                            UniqueName="IsClosed" SortExpression="IsClosed" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="35px" Visible="false">
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "IsHoldTransactionEntry").Equals(true)) ? "<img src=\"../../../../Images/Toolbar/lock16.png\" border=\"0\" title=\"Lock\" />" : string.Empty%>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="ChargeClassID" UniqueName="ChargeClassID" Visible="False" />
                                        <telerik:GridBoundColumn DataField="CoverageClassID" UniqueName="CoverageClassID" Visible="False" />
                                        <telerik:GridBoundColumn DataField="ClassID" UniqueName="ClassID" Visible="False" />
                                        <telerik:GridBoundColumn DataField="DefaultClassID" UniqueName="DefaultClassID" Visible="False" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="RoomGender" HeaderText="RoomGender"
                                            UniqueName="RoomGender" SortExpression="RoomGender" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" Visible="False" />
                                        <telerik:GridTemplateColumn />
                                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px" Visible="false">
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "SRBedStatus").Equals("BedStatus-02") && DataBinder.Eval(Container.DataItem, "IsNeedToBeClear").Equals(true)) ?
                                                                                                        string.Format("<a href=\"#\" onclick=\"rowDelete('{0}'); return false;\">{1}</a>",
                                                                                                    DataBinder.Eval(Container.DataItem, "BedID"),
                                                                        "<img src=\"../../../../Images/Toolbar/refresh16.png\" border=\"0\" title=\"Clear Bed\" />") : string.Empty %>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true">
                                    <Resizing AllowColumnResize="True" />
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="1%">
                <table></table>
            </td>
            <td width="49%" valign="top">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadGrid ID="grdBedManagement" runat="server" OnNeedDataSource="grdBedManagement_NeedDataSource"
                                AllowPaging="true" PageSize="15" AutoGenerateColumns="false">
                                <MasterTableView DataKeyNames="BedID" GroupLoadMode="client">
                                    <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Service Unit "></telerik:GridGroupByField>
                                            </SelectFields>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="ServiceUnitName" SortOrder="Ascending"></telerik:GridGroupByField>
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                        <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldName="RoomName" HeaderText="Room "></telerik:GridGroupByField>
                                            </SelectFields>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="RoomName" SortOrder="Ascending"></telerik:GridGroupByField>
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="BedManagementID" HeaderText="ID" UniqueName="BedManagementID"
                                            SortExpression="BedManagementID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            Visible="False" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="BedID" HeaderText="Bed No"
                                            UniqueName="BedID" SortExpression="BedID" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ClassName" HeaderText="Class"
                                            UniqueName="ClassName" SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridTemplateColumn UniqueName="BedStatusColor" HeaderStyle-Width="50px" HeaderText="Bed Status"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtBedStatusColor" runat="server" Width="20px" BackColor='<%# GetColor(DataBinder.Eval(Container.DataItem,"SRBedStatus")) %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="BedStatusName" HeaderText="Bed Status"
                                            UniqueName="BedStatusName" SortExpression="BedStatusName" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                                        <telerik:GridDateTimeColumn HeaderStyle-Width="110px" DataField="TransactionDate"
                                            HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy HH.mm}" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="MedicalNo" HeaderText="Medical No"
                                            UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ReservationNo" HeaderText="Reservation No"
                                            UniqueName="ReservationNo" SortExpression="ReservationNo" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="RegistrationNo" HeaderText="Registration No"
                                            UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                                        <telerik:GridTemplateColumn HeaderStyle-Width="250px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                                            SortExpression="PatientName">
                                            <ItemTemplate>
                                                <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="Address" HeaderText="Address" UniqueName="Address"
                                            SortExpression="Address" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                                        <telerik:GridTemplateColumn />
                                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px">
                                            <ItemTemplate>
                                                <%# string.Format("<a href=\"#\" onclick=\"rowReleased('{0}'); return false;\">{1}</a>", DataBinder.Eval(Container.DataItem, "BedManagementID"),
                                                                                                            "<img src=\"../../../../Images/Toolbar/post16.png\" border=\"0\" title=\"Released\" />")%>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px">
                                            <ItemTemplate>
                                                <%# string.Format("<a href=\"#\" onclick=\"rowVoid('{0}'); return false;\">{1}</a>", DataBinder.Eval(Container.DataItem, "BedManagementID"),
                                                                                                            "<img src=\"../../../../Images/Toolbar/row_delete16.png\" border=\"0\" title=\"Void\" />")%>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true">
                                    <Resizing AllowColumnResize="True" />
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
