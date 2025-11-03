<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="PrescriptionVerificationList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.PrescriptionVerificationList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function rowVerified(presNo) {
                if (confirm('Are you sure to verify this prescription?')) {
                    __doPostBack("<%= grdPrescription.UniqueID %>", 'verified|' + presNo);
                }
            }
            function gotoViewUrl(pno, regno, rt, ret) {
                if (ret == '0') {
                    var url = '../PrescriptionSales/PrescriptionSalesDetail.aspx?md=view&pno=' + pno + '&regno=' + regno + "&type=sales&rt=" + rt + "&ono=&ver=1";
                    window.location.href = url;
                }
                else {
                    var url = '../PrescriptionReturn/PrescriptionReturnDetail.aspx?md=view&pno=' + pno + '&regno=' + regno + "&ver=1";
                    window.location.href = url;
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false" />
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdPrescriptionVerif" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdPrescriptionVerif" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdPrescriptionVerif" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterParamedic">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdPrescriptionVerif" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdPrescriptionVerif" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterDispensary">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptioVerifDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionDate2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescriptionVerif" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionNo2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescriptionVerif" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterDispensary2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescriptionVerif" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionType2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescriptionVerif" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdPrescription">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdPrescriptionVerif" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPrescriptionVerif">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescriptionVerif" LoadingPanelID="fw_ajxLoadingPanel" />
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
                                <asp:Label ID="lblRegistrationType" runat="server" Text="Registration Type"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboRegistrationType" Width="300px" runat="server" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" OnItemDataBound="cboRegistrationType_ItemDataBound"
                                    OnItemsRequested="cboRegistrationType_ItemsRequested">
                                    <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "ItemName")%> </b>
                                    </ItemTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterRegistrationType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
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
                                <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / Medical No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterRegistration" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboParamedicID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterParamedic" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="150" />
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Prescription List" PageViewID="pgList" Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Presc. Verification List" PageViewID="pgVerifList">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray" SelectedIndex="0">
        <telerik:RadPageView ID="pgList" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPrescriptioDate" runat="server" Text="Prescription Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtPrescriptionDate" runat="server" Width="110px" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnFilterPrescriptionDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPrescriptionNo" runat="server" Text="Prescription No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtPrescriptionNo" runat="server" Width="300px" MaxLength="20" />
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterPrescriptionNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDispensary" runat="server" Text="Dispensary"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboDispensaryID" Width="300px" AllowCustomText="true"
                                        Filter="Contains">
                                    </telerik:RadComboBox>
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterDispensary" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPrescriptionType" runat="server" Text="Prescription Type"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboPrescriptionType" Width="300px">
                                    </telerik:RadComboBox>
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterPrescriptionType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdPrescription" runat="server" OnNeedDataSource="grdPrescription_NeedDataSource"
                OnDetailTableDataBind="grdPrescription_DetailTableDataBind" AllowSorting="true"
                ShowStatusBar="true" AllowPaging="true" PageSize="15">
                <MasterTableView DataKeyNames="PrescriptionNo" AutoGenerateColumns="false" GroupLoadMode="Client">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="DispensaryName" HeaderText="Dispensary "></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="DispensaryName" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center"
                            ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%# (this.IsUserApproveAble.Equals(true) ? string.Format("<a href=\"#\" onclick=\"rowVerified('{0}'); return false;\"><img src=\"../../../../Images/ok16.png\" border=\"0\" title=\"Verify\" /></a>",
                                                                                        DataBinder.Eval(Container.DataItem, "PrescriptionNo")) :
                                string.Empty) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="view" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# (this.IsUserUnApproveAble.Equals(true) ? string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}','{1}','{2}','{3}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View to void\" /></a>",
                                                                                    DataBinder.Eval(Container.DataItem, "PrescriptionNo"), DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "rt"), DataBinder.Eval(Container.DataItem, "ret")) : string.Empty) %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="PrescriptionDate"
                            HeaderText="Presc. Date" UniqueName="PrescriptionDate" SortExpression="PrescriptionDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PrescriptionNo" HeaderText="Prescription No"
                            UniqueName="PrescriptionNo" SortExpression="PrescriptionNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="RegistrationNo" HeaderText="Registration No"
                            UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="MedicalNo" HeaderText="Medical No"
                            UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="250px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <ItemTemplate>
                                <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" HeaderStyle-Width="150px"
                            UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                            SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                            SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Note" HeaderText="Notes" UniqueName="Note" SortExpression="Note"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridBoundColumn DataField="ApprovalDateTime" HeaderText="Approved Date"
                            UniqueName="ApprovalDateTime" SortExpression="ApprovalDateTime" DataType="System.DateTime"
                            DataFormatString="{0:MM/dd/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ApprovedByUserName" HeaderText="Approved By" UniqueName="ApprovedByUserName"
                            SortExpression="ApprovedByUserName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn UniqueName="fStatus" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%#string.Format("<img style=\"border: 0px; text-align:center; vertical-align: middle;\" src=\"../../../../Images/Toolbar/{0} \" title=\"Status: {1}\"/>",
                                        ((int)DataBinder.Eval(Container.DataItem, "Status") == 3 ? "post16.png" : ((int)DataBinder.Eval(Container.DataItem, "Status") == 2 ? "post_green_16.png" : ((int)DataBinder.Eval(Container.DataItem, "Status") == 1 ? "post_yellow_16.png" : "post16_d.png"))),
                                        ((int)DataBinder.Eval(Container.DataItem, "Status") == 3 ? "Delivered" : ((int)DataBinder.Eval(Container.DataItem, "Status") == 2 ? "Completed" : ((int)DataBinder.Eval(Container.DataItem, "Status") == 1 ? "In Progress" : "Outstanding")))
                                        )%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="Detail" DataKeyNames="SequenceNo" AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridBoundColumn DataField="SequenceNo" HeaderText="Seq. No" UniqueName="SequenceNo"
                                    HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsCompound" HeaderText="C"
                                    UniqueName="IsCompound" SortExpression="IsCompound" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="ParentNo" HeaderText="Header" UniqueName="ParentNo"
                                    HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridTemplateColumn UniqueName="TemplateItemName2" HeaderStyle-Width="35px"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblR" runat="server" Text='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsRFlag")) ? @"R/" : "" %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="TemplateItemName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemName" runat="server" Text='<%# GetItemName(DataBinder.Eval(Container.DataItem, "IsRFlag"), DataBinder.Eval(Container.DataItem, "ItemName")) %>' /><br />
                                        <i>Intervention :</i><asp:Label ID="lblIntervention" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemInterventionName") %>' /><br />
                                        <i>Notes :</i><asp:Label ID="lblNotes" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Notes") %>' /><br />
                                        <i>Order :</i><asp:Label ID="lblOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderText") %>' /><br />
                                        <i>Iter :</i><asp:Label ID="lblIter" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IterText") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Numero" UniqueName="TemplateItemName3" HeaderStyle-Width="60px"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "PrescriptionQty") %><br />
                                        (<%# DataBinder.Eval(Container.DataItem, "ResultQty") %>)
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Unit" UniqueName="TemplateItemName4" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCompound")) ? DataBinder.Eval(Container.DataItem, "EmbalaceLabel") : DataBinder.Eval(Container.DataItem, "SRItemUnit")%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="DosageQty" HeaderText="Dosing"
                                    UniqueName="DosageQty" SortExpression="DosageQty" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" />
                                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRDosageUnit" HeaderText="Unit"
                                    UniqueName="SRDosageUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="SRConsumeMethodName" HeaderText="Consume Method"
                                    UniqueName="SRConsumeMethodName" SortExpression="SRConsumeMethodName" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RecipeAmount" HeaderText="Recipe"
                                    UniqueName="RecipeAmount" SortExpression="RecipeAmount" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="false" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="DiscountAmount" HeaderText="Discount"
                                    UniqueName="DiscountAmount" SortExpression="DiscountAmount" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="LineAmount" HeaderText="Total"
                                    UniqueName="LineAmount" SortExpression="LineAmount" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgVerifList" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPrescriptionDate2" runat="server" Text="Prescription Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtPrescriptionDate2" runat="server" Width="110px" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnFilterPrescriptionDate2" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilterVerif_Click" ToolTip="Search" />
                                </td>
                            </tr>

                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPrescriptionNo2" runat="server" Text="Prescription No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtPrescriptionNo2" runat="server" Width="300px" MaxLength="20" />
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterPrescriptionNo2" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilterVerif_Click" ToolTip="Search" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblVerificationDate" runat="server" Text="Presc. Verification Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtVerificationDate" runat="server" Width="110px" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnFilterPrescriptioVerifDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilterVerif_Click" ToolTip="Search" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDispensary2" runat="server" Text="Dispensary"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboDispensaryID2" Width="300px" AllowCustomText="true"
                                        Filter="Contains">
                                    </telerik:RadComboBox>
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterDispensary2" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilterVerif_Click" ToolTip="Search" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPrescriptionType2" runat="server" Text="Prescription Type"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboPrescriptionType2" Width="300px">
                                    </telerik:RadComboBox>
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterPrescriptionType2" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilterVerif_Click" ToolTip="Search" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdPrescriptionVerif" runat="server" OnNeedDataSource="grdPrescriptionVerif_NeedDataSource"
                OnDetailTableDataBind="grdPrescriptionVerif_DetailTableDataBind" AllowSorting="true"
                ShowStatusBar="true" AllowPaging="true" PageSize="15">
                <MasterTableView DataKeyNames="PrescriptionNo" AutoGenerateColumns="false" GroupLoadMode="Client">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="DispensaryName" HeaderText="Dispensary "></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="DispensaryName" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="PrescriptionDate"
                            HeaderText="Presc. Date" UniqueName="PrescriptionDate" SortExpression="PrescriptionDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PrescriptionNo" HeaderText="Prescription No"
                            UniqueName="PrescriptionNo" SortExpression="PrescriptionNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="RegistrationNo" HeaderText="Registration No"
                            UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="MedicalNo" HeaderText="Medical No"
                            UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="250px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <ItemTemplate>
                                <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" HeaderStyle-Width="150px"
                            UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                            SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                            SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Note" HeaderText="Notes" UniqueName="Note" SortExpression="Note"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridBoundColumn DataField="ApprovalDateTime" HeaderText="Approved Date"
                            UniqueName="ApprovalDateTime" SortExpression="ApprovalDateTime" DataType="System.DateTime"
                            DataFormatString="{0:MM/dd/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ApprovedByUserName" HeaderText="Approved By" UniqueName="ApprovedByUserName"
                            SortExpression="ApprovedByUserName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="VerifiedDateTime" HeaderText="Verified Date"
                            UniqueName="VerifiedDateTime" SortExpression="VerifiedDateTime" DataType="System.DateTime"
                            DataFormatString="{0:MM/dd/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="VerifiedByUserName" HeaderText="Verified By" UniqueName="VerifiedByUserName"
                            SortExpression="VerifiedByUserName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn UniqueName="fStatus" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%#string.Format("<img style=\"border: 0px; text-align:center; vertical-align: middle;\" src=\"../../../../Images/Toolbar/{0} \" title=\"Status: {1}\"/>",
                                        ((int)DataBinder.Eval(Container.DataItem, "Status") == 3 ? "post16.png" : ((int)DataBinder.Eval(Container.DataItem, "Status") == 2 ? "post_green_16.png" : ((int)DataBinder.Eval(Container.DataItem, "Status") == 1 ? "post_yellow_16.png" : "post16_d.png"))),
                                        ((int)DataBinder.Eval(Container.DataItem, "Status") == 3 ? "Delivered" : ((int)DataBinder.Eval(Container.DataItem, "Status") == 2 ? "Completed" : ((int)DataBinder.Eval(Container.DataItem, "Status") == 1 ? "In Progress" : "Outstanding")))
                                        )%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="Detail" DataKeyNames="SequenceNo" AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridBoundColumn DataField="SequenceNo" HeaderText="Seq. No" UniqueName="SequenceNo"
                                    HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsCompound" HeaderText="C"
                                    UniqueName="IsCompound" SortExpression="IsCompound" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="ParentNo" HeaderText="Header" UniqueName="ParentNo"
                                    HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridTemplateColumn UniqueName="TemplateItemName2" HeaderStyle-Width="35px"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblR" runat="server" Text='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsRFlag")) ? @"R/" : "" %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="TemplateItemName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemName" runat="server" Text='<%# GetItemName(DataBinder.Eval(Container.DataItem, "IsRFlag"), DataBinder.Eval(Container.DataItem, "ItemName")) %>' /><br />
                                        <i>Intervention :</i><asp:Label ID="lblIntervention" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemInterventionName") %>' /><br />
                                        <i>Notes :</i><asp:Label ID="lblNotes" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Notes") %>' /><br />
                                        <i>Order :</i><asp:Label ID="lblOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderText") %>' /><br />
                                        <i>Iter :</i><asp:Label ID="lblIter" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IterText") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Numero" UniqueName="TemplateItemName3" HeaderStyle-Width="60px"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "PrescriptionQty") %><br />
                                        (<%# DataBinder.Eval(Container.DataItem, "ResultQty") %>)
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Unit" UniqueName="TemplateItemName4" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCompound")) ? DataBinder.Eval(Container.DataItem, "EmbalaceLabel") : DataBinder.Eval(Container.DataItem, "SRItemUnit")%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="DosageQty" HeaderText="Dosing"
                                    UniqueName="DosageQty" SortExpression="DosageQty" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" />
                                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRDosageUnit" HeaderText="Unit"
                                    UniqueName="SRDosageUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="SRConsumeMethodName" HeaderText="Consume Method"
                                    UniqueName="SRConsumeMethodName" SortExpression="SRConsumeMethodName" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RecipeAmount" HeaderText="Recipe"
                                    UniqueName="RecipeAmount" SortExpression="RecipeAmount" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="false" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="DiscountAmount" HeaderText="Discount"
                                    UniqueName="DiscountAmount" SortExpression="DiscountAmount" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="LineAmount" HeaderText="Total"
                                    UniqueName="LineAmount" SortExpression="LineAmount" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
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
