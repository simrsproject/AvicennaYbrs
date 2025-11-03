<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="MedicalRecordFileStatusList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.MedicalRecordFileStatusList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function rowConfirmed(regNo) {
                __doPostBack("<%= grdList.UniqueID %>", 'confirmed|' + regNo);
            }
            function gotoViewUrl(regNo) {
                location.replace('MedicalRecordFileStatusDetail.aspx?md=view&id=' + regNo);
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
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPhysician">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterInOutStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterFileStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterMedicalNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPatientName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtBarcodeEntry">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtBarcodeEntry" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
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
                <td style="vertical-align: top">
                    <table width="100%">
                        <tr id="pnlFilterDate" width="100%" runat="server">
                            <td class="label">
                                <asp:Label runat="server" ID="lblDate" Text="Registration Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtDate" runat="server" Width="100px">
                                </telerik:RadDatePicker>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPhysicianID" runat="server" Text="Physician"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboPhysicianID" Width="300px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" OnItemDataBound="cboPhysicianID_ItemDataBound"
                                    AutoPostBack="False" OnItemsRequested="cboPhysicianID_ItemsRequested">
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterPhysician" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblInOutStatus" runat="server" Text="In / Out Status"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboInOutStatus" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterInOutStatus" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="File Status"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboFileStatus" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterFileStatus" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label runat="server" ID="lblRegistrationNo" Text="Registration No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterRegistrationNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label runat="server" ID="lblMedicalNo" Text="Medical No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterMedicalNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label runat="server" ID="lblPatientName" Text="Patient Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterPatientName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegistrationType" runat="server" Text="Registration Type"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboRegistrationType" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterRegistrationType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBarcode" runat="server" Text="Barcode Scan Entry (MRN)"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtBarcodeEntry" runat="server" Width="300px" AutoPostBack="True"
                                    OnTextChanged="txtBarcodeEntry_OnTextChanged" />
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnDetailTableDataBind="grdList_DetailTableDataBind" OnItemDataBound="grdList_ItemDataBound"
        AutoGenerateColumns="False" AllowPaging="True" PageSize="15" AllowMultiRowSelection="true">
        <MasterTableView DataKeyNames="RegistrationNo" Name="MainTable">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="view">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" alt=\"View\" /></a>",
                            DataBinder.Eval(Container.DataItem, "RegistrationNo")) %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="IsNewPatient" HeaderText="New Patient" UniqueName="IsNewPatient" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="IsNewVisit" HeaderText="New Visit" UniqueName="IsNewVisit" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="RegistrationDate"
                    HeaderText="Reg. Date" UniqueName="RegistrationDate" SortExpression="RegistrationDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="RegistrationTime" HeaderText="Time"
                    UniqueName="RegistrationTime" SortExpression="RegistrationTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="MedicalNo" HeaderText="Medical No"
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
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="Sex" HeaderText="Gender"
                    UniqueName="Sex" SortExpression="Sex" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ServiceUnitName" HeaderText="Service Unit"
                    UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician"
                    UniqueName="ParamedicName" SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsTracer" HeaderText="Tracer"
                    UniqueName="IsTracer" SortExpression="IsTracer" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="MedicalFileCategory"
                    HeaderText="In / Out Status" UniqueName="MedicalFileCategory" SortExpression="MedicalFileCategory"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="MedicalFileStatus" HeaderText="File Status"
                    UniqueName="MedicalFileStatus" SortExpression="MedicalFileStatus" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />

                <telerik:GridTemplateColumn DataField="MedicalFileFolderColor" HeaderText="Folder Color (Last Visit)" UniqueName="TemplateColumn">
                    <ItemTemplate>
                        <div style="width: 100%; background-color: <%#DataBinder.Eval(Container.DataItem,"MedicalFileFolderColor")%>">
                            <%#DataBinder.Eval(Container.DataItem,"MedicalFileFolderText")%>
                        </div>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center"/>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsConfirmed").Equals(true) ? string.Empty : string.Format("<a href=\"#\" onclick=\"rowConfirmed('{0}'); return false;\">{1}</a>", 
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"), 
                                        "<img src=\"../../../../Images/Toolbar/post16.png\" border=\"0\" title=\"Confirmed\" />")) %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
            <DetailTables>
                <telerik:GridTableView Name="detail" DataKeyNames="RegistrationNo" AutoGenerateColumns="false"
                    GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                            SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="LastPositionDateTime"
                            HeaderText="Receive Date" UniqueName="LastPositionDateTime" SortExpression="LastPositionDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="UserName" HeaderText="User Name" UniqueName="UserName"
                            SortExpression="UserName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
