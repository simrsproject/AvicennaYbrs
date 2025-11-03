<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    Codebehind="ParamedicFeeRemunDataCleansingList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.ParamedicFee.ParamedicFeeRemunDataCleansingList"
    Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
                switch (val) {
                    case "back": {
                        window.location.href = "ParamedicFeeRemunList.aspx";
                        break;
                    } case "delete":
                        __doPostBack("<%= grdList.UniqueID %>", "delete");
                        break;
                }
            }

            function NavigateToDetail(command, remunNo) {
                var url = "ParamedicFeeRemunDetail.aspx?md=" + command + "&RemunNo=" + remunNo + "";
                window.location.href = url;
            }
        </script>
    </telerik:RadScriptBlock>

    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterPeriod">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterParamedic">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterDup">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Back To Remuneration" Value="back" ImageUrl="~/Images/Toolbar/arrowleft_blue16.png"
                HoveredImageUrl="~/Images/Toolbar/arrowleft_blue16.png" DisabledImageUrl="~/Images/Toolbar/arrowleft_blue16.png" />
            <telerik:RadToolBarButton runat="server" Text="Mark As Deleted" Value="delete" ImageUrl="~/Images/Toolbar/cancel16.png"
                HoveredImageUrl="~/Images/Toolbar/cancel16_h.png" DisabledImageUrl="~/Images/Toolbar/cancel16_d.png" />
        </Items>
    </telerik:RadToolBar>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPeriode" runat="server" Text="Period"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtDateStart" runat="server" Width="100px" />
                            <telerik:RadDatePicker ID="txtDateEnd" runat="server" Width="100px" />
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnFilterPeriod" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                    </tr>

                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="300px" HighlightTemplatedItems="True"
                                MarkFirstMatch="true" EnableLoadOnDemand="true" NoWrap="True" OnItemDataBound="cboParamedicID_ItemDataBound"
                                OnItemsRequested="cboParamedicID_ItemsRequested">
                                <FooterTemplate>
                                    Note : Show max 25 result
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnFilterParamedic" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                        <td width="10px">
                        </td>
                        <td>
                            <asp:ImageButton ID="btnExport" runat="server" OnClick="btnExport_Click" 
                            ImageUrl="~/Images/Toolbar/imp_exp_excel16.png" />
                        </td>
                        <td class="entry">
                            <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal">
                                 <asp:ListItem Value="" Text="TEST" Selected="True"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Toolbar/search16.png" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
            </td>
        </tr>
       <%-- <tr>
           <td class="label">
               <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
           </td>
           <td class="entry">
              <asp:CheckBoxList ID="cblRegType" runat="server" RepeatDirection="Horizontal">
                  <asp:ListItem Value="" Text="xxx" Selected="True"></asp:ListItem>
              </asp:CheckBoxList>
           </td>
              <td style="text-align: left">
                <asp:ImageButton ID="btnFilterRegType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                 OnClick="btnFilter_Click" ToolTip="Search" />
           </td>
         </tr>--%>
    </table>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" PagerStyle-Mode="NextPrevNumericAndAdvanced" AllowSorting="true">
        <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, SequenceNo, TariffComponentID" >
            <Columns>

                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SmfName" HeaderText="SMF Name"
                    UniqueName="SmfName" SortExpression="SmfName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />

                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SmfID" HeaderText="SMF"
                    UniqueName="SmfID" SortExpression="SmfID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" 
                    Visible="false" />

                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ParamedicName" HeaderText="Paramedic Name"
                    UniqueName="ParamedicName" SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" 
                    ItemStyle-HorizontalAlign="Left" />

                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="MedicalNo" HeaderText="Medical No"
                    UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Left" 
                    ItemStyle-HorizontalAlign="Left" />

                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                    SortExpression="PatientName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor Name" UniqueName="GuarantorName"
                    SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridBoundColumn  DataField="ItemName" HeaderText="Service Name" UniqueName="ItemName" 
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName" 
                    SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridDateTimeColumn DataField="ExecutionDate" HeaderText="Transaction Date" UniqueName="ExecutionDate" 
                    SortExpression="ExecutionDate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                </telerik:GridDateTimeColumn>

                <telerik:GridBoundColumn DataField="IdiCode" HeaderText="Acuan IDI" UniqueName="IdiCode" 
                    SortExpression="IdiCode" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridBoundColumn DataField="ItemGroupID" HeaderText="Kode Layanan" UniqueName="ItemGroupID" 
                    SortExpression="ItemGroupID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridBoundColumn DataField="ItemGroupName" HeaderText="Jenis Layanan" UniqueName="ItemGroupName" 
                    SortExpression="ItemGroupName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridBoundColumn DataField="IdiName" HeaderText="Nama Tindakan IDI" UniqueName="IdiName" 
                    SortExpression="IdiName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridBoundColumn DataField="Score" HeaderText="Score" UniqueName="Score" 
                    SortExpression="Score" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridBoundColumn DataField="Rvu" HeaderText="Rvu" UniqueName="Rvu" 
                    SortExpression="Rvu" DataFormatString ="{0:N2}" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridBoundColumn DataField="Icd9Cm" HeaderText="ICD 9CM" UniqueName="Icd9Cm" 
                    SortExpression="Icd9Cm" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridBoundColumn DataField="ICD10" HeaderText="ICD 10" UniqueName="ICD10" 
                    SortExpression="ICD10" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridBoundColumn DataField="PriceItem" HeaderText="Tariff" UniqueName="PriceItem" 
                    SortExpression="PriceItem" DataFormatString ="{0:N2}" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridBoundColumn DataField="Qty" HeaderText="Qty" UniqueName="Qty" SortExpression="Qty" 
                    DataFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>

                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px">
                   <ItemTemplate>
                      <asp:CheckBox ID="chkbox" runat="server"></asp:CheckBox>
                   </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
