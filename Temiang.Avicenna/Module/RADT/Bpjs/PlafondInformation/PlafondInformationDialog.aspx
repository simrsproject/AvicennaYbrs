<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="PlafondInformationDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.PlafondInformationDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboClassID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCoverageAmount" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAdd">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboClassID" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtCoverageAmount" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdPlafondHistory" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPlafondHistory">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPlafondHistory" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="700px" Height="300px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function openWinRegistrationInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationInfo.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }
        </script>

        <style type="text/css">
            .cssLeft {
                float: left;
            }
        </style>
    </telerik:RadCodeBlock>
    <table width="100%">
        <tr>
            <td>
                <fieldset>
                    <legend>PATIENT INFORMATION</legend>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblRegistrationNo" Text="Registration No" />
                                        </td>
                                        <td class="entry">
                                            <table cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20"
                                                            ReadOnly="true" />
                                                    </td>
                                                    <td>
                                                        <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();" class="noti_Container">
                                                            <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo"
                                                                Text=""></asp:Label>&nbsp; </a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20px"></td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblMedicalNo" Text="Medical No" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblPatientName" Text="Patient Name" />
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtSalutation" runat="server" Width="28px" ReadOnly="true" />
                                                    </td>
                                                    <td style="width: 3px"></td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="243px" ReadOnly="true" />
                                                    </td>
                                                    <td style="width: 3px"></td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="23px" ReadOnly="true" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20px"></td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblPlaceDOB" runat="server" Text="City / Date Of Birth"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="300px" MaxLength="20"
                                                ReadOnly="true" />
                                        </td>
                                        <td width="20"></td>
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
                                </table>
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblServiceUnit" Text="Service Unit" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox runat="server" ID="txtServiceUnitName" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20px"></td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblRoomBed" Text="Room / Bed" />
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadTextBox runat="server" ID="txtRoom" Width="200px" ReadOnly="True" />
                                                    </td>
                                                    <td style="width: 5px"></td>
                                                    <td>
                                                        <telerik:RadTextBox runat="server" ID="txtBed" Width="95px" ReadOnly="True" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20px"></td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblChargeClass" Text="Charge Class" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox runat="server" ID="txtChargeClass" Width="300px" ReadOnly="True" />
                                            <telerik:RadTextBox runat="server" ID="txtChargeClassID" Width="100px" Visible="False" />
                                        </td>
                                        <td width="20px"></td>
                                        <td />
                                    </tr>
<%--                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblCoverageClass" Text="Coverage Class" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox runat="server" ID="txtCoverageClass" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20px"></td>
                                        <td />
                                    </tr>--%>
                                    <tr>
                                        <td class="label">Coverage Class
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboCoverageClass" runat="server" Width="300px" AutoPostBack="False"
                                                EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="True"
                                                OnItemDataBound="cboCoverageClass_ItemDataBound" OnItemsRequested="cboCoverageClass_ItemsRequested">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "ClassName") %>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Note : Show max 15 result
                                                </FooterTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20px" />
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblPhysicianName" Text="Physician" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox runat="server" ID="txtPhysicianName" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20px"></td>
                                        <td />
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                <fieldset>
                    <legend>INITIAL DIAGNOSIS</legend>
                    <table width="100%">
                        <tr>
                            <td class="entry">
                                <table cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td>
                                            <telerik:RadTextBox ID="txtInitialDiagnosis" runat="server" Width="100%" TextMode="MultiLine" />
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                <fieldset>
                    <legend>APPROXIMATE PLAFOND COVERAGE</legend>
                    <table width="100%">
                        <tr>
                            <td width="50%" style="vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">Coverage Formula / Margin
                                        </td>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox runat="server" ID="txtBpjsCoverageFormula" Width="150px"
                                                Type="Percent" />
                                            <telerik:RadNumericTextBox runat="server" ID="txtMarginValue" Width="146px" Type="Percent"
                                                ReadOnly="True" />
                                        </td>
                                        <td width="20px" />
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">INA-CBG Grouper
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboBpjsPackageID" runat="server" Width="300px" AutoPostBack="False"
                                                EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="True"
                                                OnItemDataBound="cboBpjsPackageID_ItemDataBound" OnItemsRequested="cboBpjsPackageID_ItemsRequested">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "PackageName") %>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Note : Show max 15 result
                                                </FooterTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20px" />
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">Class Name
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox runat="server" ID="cboClassID" Width="300px" AutoPostBack="True"
                                                OnSelectedIndexChanged="cboClassID_SelectedIndexChanged" />
                                        </td>
                                        <td width="20px" />
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">Coverage Amount
                                        </td>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox runat="server" ID="txtCoverageAmount" Width="150px" />
                                        </td>
                                        <td width="20px" />
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label" />
                                        <td class="entry">
                                            <asp:Button runat="server" ID="btnAdd" Text="Insert" OnClick="btnAdd_Click" />
                                        </td>
                                        <td width="20px" />
                                        <td />
                                    </tr>
                                </table>
                            </td>
                            <td width="50%" style="vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <telerik:RadGrid ID="grdPlafondHistory" runat="server" OnNeedDataSource="grdPlafondHistory_NeedDataSource"
                                                AutoGenerateColumns="False" GridLines="None" AllowPaging="true" PageSize="15"
                                                OnItemCommand="grdPlafondHistory_ItemCommand" OnDeleteCommand="grdPlafondHistory_DeleteCommand"
                                                AllowSorting="False">
                                                <MasterTableView CommandItemDisplay="None" DataKeyNames="RegistrationNo, ClassID">
                                                    <Columns>
                                                        <telerik:GridButtonColumn UniqueName="EditColumn" Text="View" CommandName="View"
                                                            ButtonType="ImageButton" ImageUrl="~/Images/Toolbar/edit16.png">
                                                            <HeaderStyle Width="35px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridBoundColumn DataField="ClassName" HeaderText="Class Name" UniqueName="ClassName"
                                                            SortExpression="ClassName" />
                                                        <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="CoverageAmount" HeaderText="Coverage Amount"
                                                            UniqueName="CoverageAmount" SortExpression="CoverageAmount" HeaderStyle-HorizontalAlign="Right"
                                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                                        <telerik:GridTemplateColumn HeaderStyle-Width="10px"></telerik:GridTemplateColumn>
                                                        <telerik:GridDateTimeColumn DataField="LastUpdateDateTime" HeaderText="Last Update Date/Time" UniqueName="LastUpdateDateTime"
                                                            SortExpression="LastUpdateDateTime">
                                                            <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridDateTimeColumn>
                                                        <telerik:GridBoundColumn DataField="LastUpdateByUserID" HeaderText="Update By" UniqueName="LastUpdateByUserID"
                                                            SortExpression="LastUpdateByUserID" HeaderStyle-Width="100px" />
                                                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                                                            <HeaderStyle Width="35px" />
                                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                        </telerik:GridButtonColumn>
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
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                <fieldset>
                    <legend>BPJS S.E.P.</legend>
                    <table width="100%">
                        <tr>
                            <td width="50%" style="vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">BPJS SEP No
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtBpjsSepNo" runat="server" Width="300px" MaxLength="50" />
                                        </td>
                                        <td width="20px" />
                                        <td />
                                    </tr>
                                </table>
                            </td>
                            <td width="50%" style="vertical-align: top"></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
