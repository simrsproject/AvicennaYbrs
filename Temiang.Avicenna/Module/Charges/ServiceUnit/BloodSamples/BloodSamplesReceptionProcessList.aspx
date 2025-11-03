<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="BloodSamplesReceptionProcessList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.BloodSamples.BloodSamplesReceptionProcessList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript" language="javascript">

            function rowProcess(transno, seqno, status, takenby) {
                var tr = '<%= Request.QueryString["tr"] %>';
                if (tr == 'nrs') {
                    if (takenby == 'nrs') {

                        if (status == '0') {
                            if (confirm('Are you sure want to set as taken?')) {
                                __doPostBack("<%= grdList.UniqueID %>", transno + '|' + seqno + '|' + status);
                            }
                        } else if (status == '1') {
                            if (confirm('Are you sure want to set as submitted?')) {
                                __doPostBack("<%= grdList.UniqueID %>", transno + '|' + seqno + '|' + status);
                            }
                        } else if (status == '2') {
                            alert("Blood samples has been submitted");
                        } else {
                            alert("Blood samples has been received");
                        }
                    }
                    else {
                        if (status == '0') {
                            alert("Blood samples have not been sent");
                        } else if (status == '1') {
                            alert("Blood samples have not been sent");
                        } else if (status == '2') {
                            if (confirm('Are you sure want to set as received?')) {
                                __doPostBack("<%= grdList.UniqueID %>", transno + '|' + seqno + '|' + status);
                            }
                        } else {
                            alert("Blood samples has been received");
                        }
                    }
                }
                else {
                    if (takenby == 'nrs') {
                        if (status == '0') {
                            alert("Blood samples have not been sent");
                        } else if (status == '1') {
                            alert("Blood samples have not been sent");
                        } else if (status == '2') {
                            if (confirm('Are you sure want to set as received?')) {
                                __doPostBack("<%= grdList.UniqueID %>", transno + '|' + seqno + '|' + status);
                            }
                        } else {
                            alert("Blood samples has been received");
                        }
                    }
                    else {
                        if (status == '0') {
                            if (confirm('Are you sure want to set as taken?')) {
                                __doPostBack("<%= grdList.UniqueID %>", transno + '|' + seqno + '|' + status);
                            }
                        } else if (status == '1') {
                            if (confirm('Are you sure want to set as submitted?')) {
                                __doPostBack("<%= grdList.UniqueID %>", transno + '|' + seqno + '|' + status);
                            }
                        } else if (status == '2') {
                            if (confirm('Are you sure want to set as received?')) {
                                __doPostBack("<%= grdList.UniqueID %>", transno + '|' + seqno + '|' + status);
                            }
                        } else {
                            alert("Blood samples has been received");
                        }
                    }
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterTransactionNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterTakenBy">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboSRRegistrationType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboServiceUnitID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="Job Order Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtOrderDate1" runat="server" Width="100px" />
                                        </td>
                                        <td>&nbsp;-&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtOrderDate2" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td style="text-align: right"></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblTransactionNo" runat="server" Text="Job Order No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterTransactionNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td style="text-align: right"></td>
                        </tr>

                        <tr runat="server" id="trTakenBy">
                            <td class="label">
                                <asp:Label ID="lblSRBloodSampleTakenBy" runat="server" Text="Taken By"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboTakenBy" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterTakenBy" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td style="text-align: right"></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboStatus" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterStatus" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td style="text-align: right"></td>
                        </tr>
                    </table>
                </td>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label2" runat="server" Text="Registration Type"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboSRRegistrationType" Width="300px" AllowCustomText="true"
                                    Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboSRRegistrationType_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterRegistrationType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td style="text-align: right"></td>
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
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td style="text-align: right"></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / Medical No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterRegistration" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td style="text-align: right"></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="150" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td style="text-align: right"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" OnDetailTableDataBind="grdList_DetailTableDataBind"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="25" MasterTableView-HierarchyDefaultExpanded="true">
        <MasterTableView Name="master" DataKeyNames="TransactionNo" ClientDataKeyNames="TransactionNo"
            GroupLoadMode="Server" HierarchyLoadMode="ServerOnDemand">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="BloodSampleTakenBy" HeaderText="Taken By "></telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="SRBloodSampleTakenBy" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Service Unit "></telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="FromServiceUnitID" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Job Order No"
                    UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="TransactionDate" HeaderText="Date" UniqueName="TransactionDate"
                    SortExpression="RequestDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="TransactionTime" HeaderText="Time"
                    UniqueName="TransactionTime" SortExpression="TransactionTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                    <HeaderStyle HorizontalAlign="Center" Width="140px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                    SortExpression="MedicalNo">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="250px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                    SortExpression="PatientName">
                    <ItemTemplate>
                        <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                    SortExpression="ServiceUnitName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                    SortExpression="RoomName">
                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="BedID" HeaderText="Bed No"
                    UniqueName="BedID" SortExpression="BedID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="TransactionNo,SequenceNo" Name="grdDetail" Width="100%"
                    AutoGenerateColumns="false" ShowFooter="true" AllowPaging="true" PageSize="10">
                    <Columns>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" onclick=\"rowProcess('{0}','{1}','{2}','{3}'); return false;\">{4}</a>",
                                                                    DataBinder.Eval(Container.DataItem, "TransactionNo"),
                                                                    DataBinder.Eval(Container.DataItem, "SequenceNo"),
                                                                    DataBinder.Eval(Container.DataItem, "Status"),
                                                                    DataBinder.Eval(Container.DataItem, "SRBloodSampleTakenBy"),
                            ((DataBinder.Eval(Container.DataItem, "Status").ToString() == "3" ? "<img src=\"../../../../Images/Toolbar/post16.png\" border=\"0\" title=\"Received (By Analyst)\" />" :
                            (DataBinder.Eval(Container.DataItem, "Status").ToString() == "2" ? "<img src=\"../../../../Images/Toolbar/post_green_16.png\" border=\"0\" title=\"Submitted\" />" :
                            (DataBinder.Eval(Container.DataItem, "Status").ToString() == "1" ? "<img src=\"../../../../Images/Toolbar/post_yellow_16.png\" border=\"0\" title=\"Taken\" />" :
                            "<img src=\"../../../../Images/Toolbar/post16_d.png\" border=\"0\" title=\"Outstanding\" />" )))) )) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ItemName" HeaderText="Item"
                            UniqueName="ItemName" SortExpression="ItemName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="ChargeQuantity" HeaderText="Qty"
                            UniqueName="ChargeQuantity" SortExpression="ChargeQuantity" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRItemUnit" HeaderText="Unit"
                            UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRBloodSampleTakenBy" HeaderText="Taken By"
                            UniqueName="SRBloodSampleTakenBy" SortExpression="SRBloodSampleTakenBy"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false"  />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="SpecimenTypeName" HeaderText="Specimen Type"
                            UniqueName="SpecimenTypeName" SortExpression="SpecimenTypeName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="SpecimenTakenDateTime" HeaderText="Taken Date" UniqueName="SpecimenTakenDateTime"
                            SortExpression="SpecimenTakenDateTime" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="130px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="SpecimenTakenByUserName" HeaderText="Taken By"
                            UniqueName="SpecimenTakenByUserName" SortExpression="SpecimenTakenByUserName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="SpecimenSubmittedDateTime" HeaderText="Submitted Date" UniqueName="SpecimenSubmittedDateTime"
                            SortExpression="SpecimenSubmittedDateTime" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="130px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="SpecimenSubmittedByUserName" HeaderText="Submitted By"
                            UniqueName="SpecimenSubmittedByUserName" SortExpression="SpecimenSubmittedByUserName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="SpecimenReceivedDateTime" HeaderText="Received Date" UniqueName="SpecimenReceivedDateTime"
                            SortExpression="SpecimenReceivedDateTime" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="130px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="SpecimenReceivedByUserName" HeaderText="Received By (Analyst)"
                            UniqueName="SpecimenReceivedByUserName" SortExpression="SpecimenReceivedByUserName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn/>
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
            <ExpandCollapseColumn Visible="True" />
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
