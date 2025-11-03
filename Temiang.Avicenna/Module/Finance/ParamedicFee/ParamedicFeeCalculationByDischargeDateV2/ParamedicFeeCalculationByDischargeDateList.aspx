<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="ParamedicFeeCalculationByDischargeDateList.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.ParamedicFee.V2.ParamedicFeeCalculationByDischargeDateList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            //            function onClientClose(oWnd, args) {
            //                if (oWnd.argument) {
            //                    if (oWnd.argument == 'rebind') {
            //                        __doPostBack("<%= grdList.UniqueID %>", 'rebind');
            //                        oWnd.argument = 'undefined';
            //                    }
            //                }
            //            }

            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
                switch (val) {
                    case "process":
                        __doPostBack("<%= grdList.UniqueID %>", 'rebind');
                        break;
                    case "refresh":
                        __doPostBack("<%= grdList.UniqueID %>", 'refresh');
                        break;
                    case "print":
                        __doPostBack("<%= grdList.UniqueID %>", 'print');
                        break;
                    default :
                        __doPostBack("<%= grdList.UniqueID %>", val);
                        break;
                }
            }

            function OpenFeeInfo(TransactionNo, SequenceNo, TariffComponentID, ParamedicID, IsPhysicianMember) {
                var oWnd = $find("<%= winPaymentInfo.ClientID %>");
                oWnd.setUrl("../ParamedicFeeVerificationByDischargeDateV2/FeeInfoDialog.aspx?TransactionNo=" + TransactionNo + "&SequenceNo=" + SequenceNo
                    + "&TariffComponentID=" + TariffComponentID
                    + "&ParamedicID=" + ParamedicID + "&IsPhysicianMember=" + IsPhysicianMember
                );
                oWnd.set_title("Detail Fee Information");
                oWnd.show();
                //oWnd.add_pageLoad(onClientPageLoad);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                    <telerik:AjaxUpdatedControl ControlID="lblInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Calculate" Value="process" ImageUrl="~/Images/Toolbar/process16.png"
                HoveredImageUrl="~/Images/Toolbar/process16_h.png" DisabledImageUrl="~/Images/Toolbar/process16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Refresh" Value="refresh" ImageUrl="~/Images/Toolbar/refresh16.png"
                HoveredImageUrl="~/Images/Toolbar/refresh16_h.png" DisabledImageUrl="~/Images/Toolbar/refresh16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Print (Draft)" Value="print" ImageUrl="~/Images/Toolbar/print16.png"
                HoveredImageUrl="~/Images/Toolbar/print16_h.png" DisabledImageUrl="~/Images/Toolbar/print16_d.png" />
        </Items>
    </telerik:RadToolBar>
    <%--<telerik:RadWindow ID="winProcess" Animation="None" Width="600px" Height="300px"
        Title="Processing Periode" runat="server" Behavior="Close,Move" ShowContentDuringLoad="false"
        VisibleStatusbar="false" Modal="true" OnClientClose="onClientClose">
    </telerik:RadWindow>--%>
    <telerik:RadWindow runat="server" ID="winPaymentInfo" Animation="None" Behaviors="Move, Close, Resize"
        Width="1200px" Height="500px" VisibleStatusbar="false" ShowContentDuringLoad="False"
        Modal="true" />
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image6" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDatePeriode" runat="server" Text="Period"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtDatePeriode1" runat="server" Width="100px" />
                                        </td>
                                        <td>
                                            &nbsp;-&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtDatePeriode2" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20px">
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="300px" HighlightTemplatedItems="True"
                                    MarkFirstMatch="true" EnableLoadOnDemand="true" NoWrap="True" OnItemDataBound="cboParamedicID_ItemDataBound"
                                    OnItemsRequested="cboParamedicID_ItemsRequested">
                                    <FooterTemplate>
                                        Note : Show max 15 result
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnExport" runat="server" OnClick="btnExport_Click" 
                                ImageUrl="~/Images/Toolbar/imp_exp_excel16.png" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false" HorizontalAlign="NotSet">
        <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
            OnItemDataBound="grdList_ItemDataBound"
            GridLines="None" AutoGenerateColumns="false" AllowSorting="true">
            <MasterTableView DataKeyNames="TransactionNo,SequenceNo,TariffComponentID,ParamedicID" 
                ClientDataKeyNames="TransactionNo,SequenceNo,TariffComponentID,ParamedicID"
                GroupLoadMode="Client">
                <GroupByExpressions>
                    <telerik:GridGroupByExpression>
                        <SelectFields>
                            <telerik:GridGroupByField FieldName="ParamedicName" HeaderText="Physician "></telerik:GridGroupByField>
                        </SelectFields>
                        <GroupByFields>
                            <telerik:GridGroupByField FieldName="ParamedicName" SortOrder="Ascending"></telerik:GridGroupByField>
                        </GroupByFields>
                    </telerik:GridGroupByExpression>
                    <telerik:GridGroupByExpression>
                        <SelectFields>
                            <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Service Unit ">
                            </telerik:GridGroupByField>
                        </SelectFields>
                        <GroupByFields>
                            <telerik:GridGroupByField FieldName="ServiceUnitName" SortOrder="Ascending"></telerik:GridGroupByField>
                        </GroupByFields>
                    </telerik:GridGroupByExpression>
                </GroupByExpressions>
                <Columns>
                    <telerik:GridCheckBoxColumn DataField="IsModified" HeaderText="IsModified" UniqueName="IsModified"
                                SortExpression="IsModified" Visible="false">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridCheckBoxColumn>
                    <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                        UniqueName="RegistrationNo" SortExpression="RegistrationNo" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="DischargeDate" HeaderText="Discharge Date" UniqueName="DischargeDate"
                        SortExpression="DischargeDate">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn DataField="refToRegistration_LOS" HeaderText="LOS"
                        UniqueName="refToRegistration_LOS" SortExpression="refToRegistration_LOS">
                        <HeaderStyle HorizontalAlign="Center" Width="40px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                        SortExpression="PatientName" Visible="false">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                        SortExpression="MedicalNo" Visible="false">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn UniqueName="PatientNameMix" HeaderText="Reg No / Medical No / Patient Name"
                    HeaderStyle-Width="150px">
                        <ItemTemplate>
                            <%# string.Format("{0}<br />{1}&nbsp;{2}", 
                                DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                DataBinder.Eval(Container.DataItem, "MedicalNo"),
                                DataBinder.Eval(Container.DataItem, "PatientName"))%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="TransactionNo" HeaderText="Transaction No" UniqueName="TransactionNo"
                        SortExpression="RegistrationNo" Visible="false">
                        <HeaderStyle HorizontalAlign="Center" Width="110px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ToServiceUnitName" HeaderText="To Unit" UniqueName="ToServiceUnitName"
                        SortExpression="ToServiceUnitName" Visible="false">
                        <HeaderStyle HorizontalAlign="Center" Width="120px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn UniqueName="TransNoSUMix" HeaderText="Transaction No / To Unit"
                    HeaderStyle-Width="130px">
                        <ItemTemplate>
                            <%# string.Format("{0}<br />{1}",
                                DataBinder.Eval(Container.DataItem, "TransactionNo"),
                                DataBinder.Eval(Container.DataItem, "ToServiceUnitName"))%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridDateTimeColumn DataField="TransactionDate" HeaderText="Transaction Date"
                        UniqueName="TransactionDate" SortExpression="TransactionDate">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                        SortExpression="ItemName">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="refToGuarantor_GuarantorName" HeaderText="Guarantor Name" UniqueName="refToGuarantor_GuarantorName"
                        SortExpression="refToGuarantor_GuarantorName">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="PriceItem" HeaderText="Price" UniqueName="PriceItem"
                        SortExpression="PriceItem" DataFormatString="{0:n2}">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ParamedicFee" HeaderText="Initial Fee" UniqueName="ParamedicFee"
                        SortExpression="ParamedicFee" DataFormatString="{0:n2}">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DeductionAmount" HeaderText="Deduction" UniqueName="DeductionAmount"
                        SortExpression="DeductionAmount" DataFormatString="{0:n2}" Visible="false">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn UniqueName="FeeAmountTemplate" HeaderText="Calculated Fee"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SRPhysicianFeeCategory").ToString()) >= 6) ? 
                                            string.Format("<a href=\"javascript:void(0);\" onclick=\"javascript:OpenFeeInfo('{1}','{2}','{3}','{4}','{5}')\">{0}</a>", 
                                                ((DataBinder.Eval(Container.DataItem, "FeeAmount")) is DBNull ? "" : ((decimal)DataBinder.Eval(Container.DataItem, "FeeAmount")).ToString("n2")), 
                                                DataBinder.Eval(Container.DataItem, "TransactionNo"),
                                                DataBinder.Eval(Container.DataItem, "SequenceNo"),
                                                DataBinder.Eval(Container.DataItem, "TariffComponentID"),
                                                DataBinder.Eval(Container.DataItem, "ParamedicID"),
                                                DataBinder.Eval(Container.DataItem, "IsPhysicianMember")) :
                                                ((DataBinder.Eval(Container.DataItem, "FeeAmount")) is DBNull ? "" : ((decimal)DataBinder.Eval(Container.DataItem, "FeeAmount")).ToString("n2"))
                                    %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="SumDeductionAmount" HeaderText="Deduction Before Tax" UniqueName="SumDeductionAmount"
                        SortExpression="SumDeductionAmount" DataFormatString="{0:n2}">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="LastCalculatedDateTime" HeaderText="Calculated Date"
                        UniqueName="LastCalculatedDateTime" SortExpression="LastCalculatedDateTime">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridDateTimeColumn>
                </Columns>
            </MasterTableView>
            <FilterMenu>
            </FilterMenu>
            <ClientSettings EnableRowHoverStyle="true">
                <Resizing AllowColumnResize="True" />
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </telerik:RadGrid>
    </telerik:RadAjaxPanel>
</asp:Content>
