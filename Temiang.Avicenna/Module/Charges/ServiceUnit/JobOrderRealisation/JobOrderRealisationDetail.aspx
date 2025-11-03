<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="JobOrderRealisationDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.JobOrderRealisationDetail"
    Title="Job Order Detail" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function openWinProcess(joNo, itemID, seqNo, unit, type) {
                var oWnd = $find("<%= winProcess.ClientID %>");
                oWnd.setUrl("JobOrderRealizationTariffComponent.aspx?joNo=" + joNo + "&itemID=" + itemID + "&seqNo=" + seqNo + "&unitID=" + unit + "&type=" + type + '&disch=<%= Page.Request.QueryString["disch"] %>');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinFilm(joNo, itemID, seqNo) {
                var oWnd = $find("<%= winProcess.ClientID %>");
                oWnd.setUrl("ExposureFactorDialog.aspx?joNo=" + joNo + "&itemID=" + itemID + "&seqNo=" + seqNo);
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.command == 'rebind') {
                    __doPostBack("<%= grdTransChargesItem.UniqueID %>", 'rebind');
                }
            }

            function rowDelete(joNo, seqNo) {
                var sReason = prompt("You are about to void this transaction, please fill the void reason");
                if (sReason == null || sReason == '') return;
                __doPostBack("<%= grdTransChargesItem.UniqueID %>", 'delete|' + joNo + '|' + seqNo + '|' + sReason);
            }

            function verifyOrder(joNo, seqNo) {
                __doPostBack("<%= grdTransChargesItem.UniqueID %>", 'verify|' + joNo + '|' + seqNo);
            }

            function verifyOrderAll() {
                __doPostBack("<%= grdTransChargesItem.UniqueID %>", 'verify_all');
            }

            function openWinRegistrationInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationInfo.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }

            function onClientClickUpdateRadNo() {
                var regno = $find("<%= txtRegistrationNo.ClientID %>");
                var diagno = prompt('Are you sure to change Radiology No for this patient?\nNew Radiology No :');
                if (diagno == null || diagno == '') {
                    args.set_cancel(true);
                }
                else {
                    __doPostBack("<%= grdTransChargesItem.UniqueID %>", 'updateRadNo!' + regno.get_value() + '!' + diagno);
                }
            }

            function openWinCons(transNo, seqNo, itemID, serviceUnitID) {
                var oWnd = $find("<%= winProcess.ClientID %>");

                var reg = $find("<%= txtRegistrationNo.ClientID %>");
                var pageId = document.getElementById('<%= hdnPageId.ClientID %>').value;
                //alert(serviceUnitID);
                oWnd.setUrl('../ServiceUnitTransaction/ItemConsumptionPackage.aspx?trans=' + transNo + '&seq=' + seqNo + '&item=' + itemID + '&unit=' + serviceUnitID + '&reg=' + reg.get_value() + '&pageId=' + pageId);
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdTransChargesItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="600px"
        Behavior="Close, Move" ShowContentDuringLoad="False" VisibleStatusbar="false"
        Modal="true" ID="winProcess" OnClientClose="onClientClose">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <asp:HiddenField runat="server" ID="hdnPageId" />
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image1" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Registration Detail">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" ReadOnly="true" />
                                            <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();" class="noti_Container">
                                                <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo"
                                                    Text=""></asp:Label>&nbsp; </a>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtSalutation" runat="server" Width="28px" ReadOnly="true" />
                                                    </td>
                                                    <td style="width: 3px"></td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="266px" ReadOnly="true" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <%--                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSex" runat="server" Text="Gender"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <asp:RadioButton ID="optSexFemale" runat="server" Text="Female" GroupName="Sex" Enabled="false" />
                                            <asp:RadioButton ID="optSexMale" runat="server" Text="Male" GroupName="Sex" Enabled="false" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>--%>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSex" runat="server" Text="Gender"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtSex" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
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
                                            <telerik:RadNumericTextBox ID="txtAgeInYear" runat="server" Width="30px" ReadOnly="true">
                                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                            </telerik:RadNumericTextBox>
                                            &nbsp;Y&nbsp;
                                            <telerik:RadNumericTextBox ID="txtAgeInMonth" runat="server" Width="30px" ReadOnly="true">
                                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                            </telerik:RadNumericTextBox>
                                            &nbsp;M&nbsp;
                                            <telerik:RadNumericTextBox ID="txtAgeInDay" runat="server" Width="30px" ReadOnly="true">
                                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                            </telerik:RadNumericTextBox>
                                            &nbsp;D&nbsp;
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr runat="server" id="trRadiologyNo" visible="False">
                                        <td class="label">
                                            <asp:Label ID="lblRadiologyNo" runat="server" Text="Radiology No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtRadiologyNo" runat="server" Width="300px" ReadOnly="true" />
                                                    </td>
                                                    <td style="width: 5px"></td>
                                                    <td>
                                                        <%--<a href="javascript:void(0);" onclick="javascript:onClientClickUpdateRadNo();" title="Edit Radiology No.">
                                                            <img src="../../../../Images/Toolbar/edit16.png" />
                                                        </a>--%>
                                                        <asp:ImageButton ID="btnUpdateRadNo" runat="server" ImageUrl="../../../../Images/Toolbar/edit16.png"
                                                            CausesValidation="False" OnClientClick="onClientClickUpdateRadNo();return false;"
                                                            ToolTip="Edit Radiology No." />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblJobOrderNo" runat="server" Text="Job Order No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtJobOrderNo" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblPhysicianSenders" runat="server" Text="Physician Senders"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtPhysicianSenders" runat="server" Width="300px" MaxLength="255" />
                                        </td>
                                        <td width="20px">
                                            <asp:RequiredFieldValidator ID="rfvPhysicianSenders" runat="server" ErrorMessage="Physician Senders required."
                                                ValidationGroup="entry" ControlToValidate="txtPhysicianSenders" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <asp:Panel runat="server" ID="pnlProdia">
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="lblSRProdiaContractID" runat="server" Text="Prodia Contract ID"></asp:Label>
                                            </td>
                                            <td class="entry2Column">
                                                <telerik:RadComboBox ID="cboSRProdiaContractID" runat="server" Width="304px" AllowCustomText="true"
                                                    Filter="Contains">
                                                </telerik:RadComboBox>
                                            </td>
                                            <td width="20"></td>
                                            <td></td>
                                        </tr>
                                    </asp:Panel>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100px" ReadOnly="true" />
                                            &nbsp;
                                            <asp:Label ID="lblServiceUnitName" runat="server"></asp:Label>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblRoomID" runat="server" Text="Room"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtRoomID" runat="server" Width="100px" ReadOnly="true" />
                                            &nbsp;
                                            <asp:Label ID="lblRoomName" runat="server"></asp:Label>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblClassID" runat="server" Text="Charge Class"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtClassID" runat="server" Width="100px" ReadOnly="true" />
                                            &nbsp;
                                            <asp:Label ID="lblClassName" runat="server"></asp:Label>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblBedID" runat="server" Text="Bed No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtBedID" runat="server" Width="100px" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtParamedicID" runat="server" Width="100px" ReadOnly="true" />
                                            &nbsp;
                                            <asp:Label ID="lblParamedicName" runat="server"></asp:Label>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtGuarantorID" runat="server" Width="100px" ReadOnly="true" />
                                            &nbsp;
                                            <asp:Label ID="lblGuarantorName" runat="server"></asp:Label>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr runat="server" id="trBpjsSepNo">
                                        <td class="label">
                                            <asp:Label ID="lblBpjsSepNo" runat="server" Text="BPJS SEP No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtBpjsSepNo" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" ReadOnly="True" TextMode="MultiLine"
                                                Height="40px" />
                                        </td>
                                        <td width="20px"></td>
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
                </td>
            </tr>
            <tr>
                <%--                <td>
                    <table width="100%">
                        <tr>
                            <td width="25%" class="labelcaption">
                                <asp:Label runat="server" ID="lblS" Text="(S) Subjective" />
                            </td>
                            <td width="25%" class="labelcaption">
                                <asp:Label runat="server" ID="lblO" Text="(O) Objective" />
                            </td>
                            <td width="25%" class="labelcaption">
                                <asp:Label runat="server" ID="lblA" Text="(A) Assessment" />
                            </td>
                            <td width="25%" class="labelcaption">
                                <asp:Label runat="server" ID="lblP" Text="(P) Planning" />
                            </td>
                        </tr>
                        <tr style="height: 100px;">
                            <td width="25%" valign="top">
                                <div style="overflow: auto;height:100px;">
                                    <asp:Literal runat="server" ID="litSoapS"></asp:Literal>
                                </div>
                            </td>
                            <td width="25%" valign="top">
                                <div style="overflow: auto;height:100px;">
                                    <asp:Literal runat="server" ID="litSoapO"></asp:Literal>
                                </div>
                            </td>
                            <td width="25%" valign="top">
                               <div style="overflow: auto;height:100px;">
                                    <asp:Literal runat="server" ID="litSoapA"></asp:Literal>
                                </div>
                            </td>
                            <td width="25%" valign="top">
                                <div style="overflow: auto;height:100px;">
                                    <asp:Literal runat="server" ID="litSoapP"></asp:Literal>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>--%>

                <td valign="top" class="RadGrid RadGrid_Default">
                    <div style="display: none">
                        <%--Dipakai untuk pengecekan "Diagnosis required"--%>
                        <asp:Literal runat="server" ID="litSoapA"></asp:Literal>
                    </div>
                    <asp:Literal runat="server" ID="litSoap"></asp:Literal>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboParamedicID" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr runat="server" id="trClinicalDiagnosis">
                        <td class="label">
                            <asp:Label ID="lblClinicalDiagnosis" runat="server" Text="Clinical Diagnosis"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtClinicalDiagnosis" runat="server" Width="300px" TextMode="MultiLine" Height="40px" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table>
                    <asp:Panel runat="server" ID="pnlLinkLis" Visible="False">
                        <tr>
                            <td class="label">Clinical Pathologist
                            </td>
                            <td class="entry2Column">
                                <telerik:RadComboBox ID="cboPhysicianIDPathology" runat="server" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">Laboratory Analyst
                            </td>
                            <td class="entry2Column">
                                <telerik:RadComboBox ID="cboAnalystID" runat="server" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20"></td>
                            <td />
                        </tr>
                    </asp:Panel>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <telerik:RadGrid ID="grdTransChargesItem" runat="server" OnNeedDataSource="grdTransChargesItem_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" ShowFooter="true" OnItemCreated="grdTransChargesItem_ItemCreated">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="SequenceNo">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="accept" HeaderText="" Groupable="false">
                                <HeaderTemplate>
                                    <%# string.Format("<a href=\"#\" onclick=\"verifyOrderAll(); return false;\">{0}</a>",
                                            "<img src=\"../../../../Images/Toolbar/post16.png\" border=\"0\" title=\"Verify All\" />")%>
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                <ItemTemplate>
                                    <%# GetStatus(DataBinder.Eval(Container.DataItem, "IsCasemixApproved"), DataBinder.Eval(Container.DataItem, "IsOrderRealization"), DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"), DataBinder.Eval(Container.DataItem, "ItemName"), DataBinder.Eval(Container.DataItem, "SRRegistrationType"), DataBinder.Eval(Container.DataItem, "SpecimenStatus"))%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="center" HeaderText="" Visible="false">
                                <ItemTemplate>
                                    <%# GetStatus(DataBinder.Eval(Container.DataItem, "IsCasemixApproved"), DataBinder.Eval(Container.DataItem, "IsOrderRealization"), DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"), DataBinder.Eval(Container.DataItem, "ItemName"), DataBinder.Eval(Container.DataItem, "SRRegistrationType"), DataBinder.Eval(Container.DataItem, "SpecimenStatus"))%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="center" HeaderText="">
                                <ItemTemplate>
                                    <%# GetStatusTariffComponent(DataBinder.Eval(Container.DataItem, "IsCasemixApproved"), DataBinder.Eval(Container.DataItem, "IsOrderRealization"), DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"), DataBinder.Eval(Container.DataItem, "ItemName"), DataBinder.Eval(Container.DataItem, "ItemID"), DataBinder.Eval(Container.DataItem, "ToServiceUnitID"), DataBinder.Eval(Container.DataItem, "SRItemType"), DataBinder.Eval(Container.DataItem, "SRRegistrationType"), DataBinder.Eval(Container.DataItem, "SpecimenStatus"))%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="cons" HeaderText="" Groupable="false">
                                <ItemTemplate>
                                    <%# (DataBinder.Eval(Container.DataItem, "IsItemTypeService").Equals(true) && DataBinder.Eval(Container.DataItem, "IsVoid").Equals(false) ? 
                            string.Format("<a href=\"#\" onclick=\"openWinCons('{0}','{1}','{2}','{3}'); return false;\"><img src=\"../../../../Images/Toolbar/consumption.png\" border=\"0\" title=\"Item Consumption\" /></a>", DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"), DataBinder.Eval(Container.DataItem, "ItemID"), DataBinder.Eval(Container.DataItem, "ToServiceUnitIDHeader")) : string.Empty)%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="" UniqueName="ExposureFactor" SortExpression="ExposureFactor"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="50px" />
                                <ItemTemplate>
                                    <%# string.Format("<a href=\"#\" onclick=\"openWinFilm('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/radfilm.png\" border=\"0\" title=\"Exposure Factor\" /></a>",
                                                                                        DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "ItemID"),
                                                                                                                                                                        DataBinder.Eval(Container.DataItem, "SequenceNo"))%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="SequenceNo" UniqueName="SequenceNo" SortExpression="SequenceNo"
                                Visible="false" />
                            <telerik:GridTemplateColumn UniqueName="ItemName" HeaderText="Item Name">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemName")%>&nbsp;<span style="color: red"><%# DataBinder.Eval(Container.DataItem, "PrevOrder")%></span>
                                    <br />
                                    <span style="color: blue"><%# DataBinder.Eval(Container.DataItem, "Notes")%></span>&nbsp
                                    <i><span style="color: blue"><%# DataBinder.Eval(Container.DataItem, "CombinedNotes")%></span></i>&nbsp
                                    <i><span style="color: green"><%# DataBinder.Eval(Container.DataItem, "SpecimenTypeName")%></span></i>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ParamedicCollectionName" HeaderText="Physician"
                                UniqueName="ParamedicCollectionName" SortExpression="ParamedicCollectionName"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="ChargeQuantity" HeaderText="Qty"
                                UniqueName="ChargeQuantity" SortExpression="ChargeQuantity" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" />
                            <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRItemUnit" HeaderText="Unit"
                                UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                                UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="DiscountAmount" HeaderText="Discount"
                                UniqueName="DiscountAmount" SortExpression="DiscountAmount" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CitoAmount" HeaderText="Cito"
                                UniqueName="CitoAmount" SortExpression="CitoAmount" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Total" UniqueName="Total"
                                DataType="System.Double" DataFields="ChargeQuantity,Price,DiscountAmount,CitoAmount"
                                SortExpression="Total" Expression="{0} * (({1} - {2}) + {3})" FooterStyle-HorizontalAlign="Right"
                                Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:n2}" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsCito" HeaderText="Cito"
                                UniqueName="IsCito" SortExpression="IsCito" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridTemplateColumn UniqueName="GridTemplateColumn2" HeaderStyle-Width="30px"
                                ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <a href="#" onclick="rowDelete('<%# DataBinder.Eval(Container.DataItem, "TransactionNo") %>', '<%# DataBinder.Eval(Container.DataItem, "SequenceNo") %>'); return false;">
                                        <img src="../../../../Images/Toolbar/row_delete16.png" border="0" />
                                    </a>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn DataField="ItemName" UniqueName="ItemName2" SortExpression="ItemName"
                                Visible="false" />
                            <telerik:GridBoundColumn DataField="SRRegistrationType" UniqueName="SRRegistrationType" SortExpression="SRRegistrationType"
                                Visible="false" />
                            <telerik:GridBoundColumn DataField="SpecimenStatus" UniqueName="SpecimenStatus" SortExpression="SpecimenStatus"
                                Visible="false" />
                            <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="CasemixApproved" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" Visible="False" UniqueName="chkIsCasemixApproved">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkIsCasemixApproved" runat="server" Width="50px" Checked='<%#Eval("IsCasemixApprovedFlag")%>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="OrderRealization" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" Visible="False" UniqueName="chkIsOrderRealization">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkIsOrderRealization" runat="server" Width="50px" Checked='<%#Eval("IsOrderRealization")%>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="false" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnLisSatusehat" runat="server" Text="Update LIS Table Satusehat" OnClick="btnLisSatusehat_Click" Visible="true" />
    <br />
    <br />
    <br />
</asp:Content>
