<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="EditGuarantorDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.EditGuarantorDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="winRegistration" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="700px" Height="300px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">

            function openWinGuarantorInfo() {
                var oWnd = $find("<%= winRegistration.ClientID %>");
                var oguar = $find("<%= cboGuarantorID.ClientID %>");
                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/Finance/Master/GuarantorInfo/GuarantorInfoDialog.aspx?id=' + oguar.get_value() + '")%>');
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }
            function openWinRegistrationInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationInfo.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }
        </script>

        <style type="text/css">
            .cssLeft
            {
                float: left;
            }
        </style>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboGuarantorID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboSRBusinessMethod" />
                    <telerik:AjaxUpdatedControl ControlID="txtPlafonValue" />
                    <telerik:AjaxUpdatedControl ControlID="chkIsPlavonTypeGlobal" />
                    <%--<telerik:AjaxUpdatedControl ControlID="cboEmployeeID" />
                    <telerik:AjaxUpdatedControl ControlID="cboGuarSRRelationship" />--%>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboGuarantorGroupID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboSRBusinessMethod" />
                    <telerik:AjaxUpdatedControl ControlID="txtPlafonValue" />
                    <telerik:AjaxUpdatedControl ControlID="chkIsPlavonTypeGlobal" />
                    <%--<telerik:AjaxUpdatedControl ControlID="cboEmployeeID" />
                    <telerik:AjaxUpdatedControl ControlID="cboGuarSRRelationship" />--%>
                    <telerik:AjaxUpdatedControl ControlID="cboGuarantorID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboSRBusinessMethod">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtPlafonValue" />
                    <telerik:AjaxUpdatedControl ControlID="chkIsPlavonTypeGlobal" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
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
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblMedicalNo" Text="Medical No" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblPatientName" Text="Patient Name" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSex" runat="server" Text="Gender"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <asp:RadioButton ID="optSexFemale" runat="server" Text="Female" GroupName="Sex" Enabled="false" />
                                            <asp:RadioButton ID="optSexMale" runat="server" Text="Male" GroupName="Sex" Enabled="false" />
                                        </td>
                                        <td style="width: 20px">
                                        </td>
                                        <td>
                                        </td>
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
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblRoomBed" Text="Room" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox runat="server" ID="txtRoom" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblBedNo" Text="Bed" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox runat="server" ID="txtBed" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblPhysicianName" Text="Physician" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox runat="server" ID="txtPhysicianName" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20px">
                                        </td>
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
                    <legend>GUARANTOR</legend>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr runat="server" id="trGuarantorHeader">
                                        <td class="label">
                                            <asp:Label ID="lblGuarantorGroup" runat="server" Text="Guarantor Group"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadComboBox ID="cboGuarantorGroupID" runat="server" Width="300px" HighlightTemplatedItems="True"
                                                            AutoPostBack="True" MarkFirstMatch="False" EnableLoadOnDemand="true" OnItemDataBound="cboGuarantorID_ItemDataBound"
                                                            OnItemsRequested="cboGuarantorGroupID_ItemsRequested" OnSelectedIndexChanged="cboGuarantorGroupID_SelectedIndexChanged">
                                                            <FooterTemplate>
                                                                Note : Show max 30 result
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadComboBox ID="cboGuarantorID" runat="server" Width="300px" AutoPostBack="True"
                                                            EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                                                            OnItemDataBound="cboGuarantorID_ItemDataBound" OnItemsRequested="cboGuarantorID_ItemsRequested"
                                                            OnSelectedIndexChanged="cboGuarantorID_SelectedIndexChanged">
                                                            <FooterTemplate>
                                                                Note : Show max 30 result
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnGuarantorContractSummary" runat="server" ImageUrl="../../../Images/stickynote16.png"
                                                            CausesValidation="False" OnClientClick="openWinGuarantorInfo();return false;"
                                                            ToolTip="Guarantor Info" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvGuarantorID" runat="server" ErrorMessage="Guarantor required."
                                                ValidationGroup="entry" ControlToValidate="cboGuarantorID" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblBusinessMethod" runat="server" Text="Guarantor Method"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboSRBusinessMethod" runat="server" Width="300px" AutoPostBack="true"
                                                OnSelectedIndexChanged="cboSRBusinessMethod_SelectedIndexChanged">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvSRBusinessMethod" runat="server" ErrorMessage="Guarantor Method required."
                                                ValidationGroup="entry" ControlToValidate="cboSRBusinessMethod" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblPlafond" runat="server" Text="Plafond Amount"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtPlafonValue" runat="server" Width="100px" Value="0" />
                                                    </td>
                                                    <td>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkIsPlavonTypeGlobal" runat="server" Text="Global Flafond" Enabled="False" />
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text="- GUARANTOR INFORMATION -"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <asp:Panel runat="server" ID="pnlEmployeeInfo">
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="lblEmployeeID" runat="server" Text="Employee ID"></asp:Label>
                                            </td>
                                            <td class="entry2Column">
                                                <telerik:RadComboBox ID="cboEmployeeID" runat="server" Width="300px" AutoPostBack="False"
                                                    EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="True"
                                                    OnItemDataBound="cboEmployeeID_ItemDataBound" OnItemsRequested="cboEmployeeID_ItemsRequested">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber") %>
                                                        &nbsp;-&nbsp;
                                                        <%# DataBinder.Eval(Container.DataItem, "FirstName")%>
                                                        &nbsp;
                                                        <%# DataBinder.Eval(Container.DataItem, "MiddleName")%>
                                                        &nbsp;
                                                        <%# DataBinder.Eval(Container.DataItem, "LastName")%>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Note : Show max 15 result
                                                    </FooterTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td width="20">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="lblGuarSRRelationship" runat="server" Text="Relation"></asp:Label>
                                            </td>
                                            <td class="entry2Column">
                                                <telerik:RadComboBox ID="cboGuarSRRelationship" runat="server" Width="300px" />
                                            </td>
                                            <td width="20">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblInsuranceID" runat="server" Text="Insurance ID"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadTextBox ID="txtInsuranceID" runat="server" Width="300px" MaxLength="255" />
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="Label3" runat="server" Text="BPJS SEP No"></asp:Label>
                                        </td>
                                        <td class="entry300">
                                            <telerik:RadTextBox ID="txtBpjsSepNo" runat="server" Width="300px" MaxLength="50" />
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblGuarIDCardNo" runat="server" Text="Guarantor Card No"></asp:Label>
                                        </td>
                                        <td class="entry300">
                                            <telerik:RadTextBox ID="txtGuarIDCardNo" runat="server" Width="300px" />
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="- GUARANTOR UPDATE HISTORY -"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <telerik:RadGrid ID="grdHistoryUpdateGuarantor" runat="server" OnNeedDataSource="grdHistoryUpdateGuarantor_NeedDataSource"
                                                AutoGenerateColumns="False" GridLines="None" AllowPaging="true" PageSize="5"
                                                AllowSorting="False">
                                                <HeaderContextMenu>
                                                </HeaderContextMenu>
                                                <MasterTableView CommandItemDisplay="None" DataKeyNames="FromGuarantorID, ToGuarantorID, LastUpdateDateTime"
                                                    GroupLoadMode="Client">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="LastUpdateDateTime" HeaderText="Date & Time"
                                                            UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime" DataType="System.DateTime"
                                                            DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                                            <HeaderStyle HorizontalAlign="Center" Width="105px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="FromGuarantorName" HeaderText="From" UniqueName="FromGuarantorName"
                                                            SortExpression="FromGuarantorName">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="ToGuarantorName" HeaderText="To" UniqueName="ToGuarantorName"
                                                            SortExpression="ToGuarantorName">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="LastUpdateByUserID" HeaderText="Update By" UniqueName="LastUpdateByUserID"
                                                            SortExpression="LastUpdateByUserID" HeaderStyle-Width="120px">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <FilterMenu>
                                                </FilterMenu>
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
                    <legend>C O B</legend>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="grdRegistrationGuarantor" runat="server" OnNeedDataSource="grdRegistrationGuarantor_NeedDataSource"
                                    AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdRegistrationGuarantor_UpdateCommand"
                                    OnDeleteCommand="grdRegistrationGuarantor_DeleteCommand" OnInsertCommand="grdRegistrationGuarantor_InsertCommand">
                                    <HeaderContextMenu>
                                    </HeaderContextMenu>
                                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="RegistrationNo,GuarantorID">
                                        <Columns>
                                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle CssClass="MyImageButton" />
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridBoundColumn DataField="GuarantorID" HeaderText="ID" UniqueName="GuarantorID"
                                                SortExpression="GuarantorID">
                                                <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                                                SortExpression="GuarantorName">
                                                <HeaderStyle HorizontalAlign="Left" Width="250px"/>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PlafondAmount" HeaderText="Plafond Amount" UniqueName="PlafondAmount"
                                                SortExpression="PlafondAmount" DataFormatString="{0:n2}">
                                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                                                SortExpression="Notes">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <EditFormSettings UserControlName="RegistrationGuarantorDetail.ascx" EditFormType="WebUserControl">
                                            <EditColumn UniqueName="RegistrationGuarantorEditCommand">
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
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
