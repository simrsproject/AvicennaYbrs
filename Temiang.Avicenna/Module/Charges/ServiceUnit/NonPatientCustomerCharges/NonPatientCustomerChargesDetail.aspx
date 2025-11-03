<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="NonPatientCustomerChargesDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.NonPatientCustomerChargesDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadScriptBlock1" runat="server">

        <script type="text/javascript" language="javascript">
            function openWinPatient(mode, recID, patient) {
                var combo = $find("<%= cboServiceUnitID.ClientID %>");
                if (combo.get_value() != '') {
                    var oWnd = $find("<%= radWinPatient.ClientID %>");
                    oWnd.setUrl("../../../RADT/Registration/PatientDetail.aspx?md=" + mode + "&pid=" + recID + "&pt=" + patient + "&rt=&unit=" + combo.get_value() + "&tp=inp", "radWinPatient");
                    oWnd.show();
                }
            }

            function onClientCloseWinPatient(oWnd, args) {
                if (oWnd.argument && oWnd.argument.PatientID != null) {
                    __doPostBack('ShowNewPatient', oWnd.argument.PatientID);
                }
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.rebind != null)
                    __doPostBack("<%= grdTransChargesItem.UniqueID %>", "rebind");
            }

            function openWinPickList() {
                var cboLoc = $find("<%= cboLocationID.ClientID %>");
                if (cboLoc != null) {
                    if (cboLoc.get_visible()) {
                        if (cboLoc.get_value() == '') {
                            alert('Location is required.');
                            return;
                        }
                    }
                }

                var cboGuar = $find("<%= cboGuarantorID.ClientID %>");
                if (cboGuar != null) {
                    if (cboGuar.get_visible()) {
                        if (cboGuar.get_value() == '') {
                            alert('Guarantor is required.');
                            return;
                        }
                    }
                }

                var trans = $find("<%= txtTransactionNo.ClientID %>");
                var reg = $find("<%= txtRegistrationNo.ClientID %>");
                var unit = $find("<%= cboServiceUnitID.ClientID %>");
                var loc = $find("<%= cboLocationID.ClientID %>");
                var guar = $find("<%= cboGuarantorID.ClientID %>");

                if (unit.get_value() != '') {
                    var oWnd = $find("<%= winCharges.ClientID %>");

                    if (loc.get_value() != '') {
                        oWnd.setUrl('../ServiceUnitTransaction/ItemPickerListItemProduct.aspx?transno=' + trans.get_value() + '&loc=' + loc.get_value() + '&reg=' + reg.get_value() + '&type=tr' + '&guar=' + guar.get_value());
                        oWnd.set_title('Item Product List');
                    }

                    oWnd.show();
                    //oWnd.maximize();
                    oWnd.add_pageLoad(onClientPageLoad);
                }
            }

            function ItemReturn(id) {
                if (!confirm("Are you sure want to process?")) return;
                __doPostBack("<%= grdExtramural.UniqueID %>", 'return|' + id);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="radWinPatient" Width="1200px" Height="500px" runat="server"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
        ReloadOnShow="True" OnClientClose="onClientCloseWinPatient" ShowContentDuringLoad="false">
    </telerik:RadWindow>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="600px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="onClientClose"
        ID="winCharges">
    </telerik:RadWindow>
    <telerik:RadWindow runat="server" Animation="None" Width="850px" Height="600px" ShowContentDuringLoad="False"
        VisibleStatusbar="False" Title="Allergy" ID="winAllergy" Behaviors="Close" Modal="true">
    </telerik:RadWindow>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTransactionNo" runat="server" ErrorMessage="Transaction No required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lbltxtTransactionDate" runat="server" Text="Transaction Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                DatePopupButton-Enabled="false">
                            </telerik:RadDatePicker>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTransactionDate" runat="server" ErrorMessage="Transaction Date required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Dispensary required."
                                ValidationGroup="entry" ControlToValidate="cboServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblLocationID" runat="server" Text="Location"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboLocationID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvLocationID" runat="server" ErrorMessage="Location required."
                                ValidationGroup="entry" ControlToValidate="cboLocationID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" MaxLength="15"
                                            ReadOnly="true" />
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="btnNewPatient" Text="New Data Customer" OnClientClick="javascript:openWinPatient('new','','directpatient',0);return false;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" ReadOnly="true" />
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
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvTxtPatientName" runat="server" ErrorMessage="Patient Name required."
                                ValidationGroup="entry" ControlToValidate="txtPatientName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvCboPatientID" runat="server" ErrorMessage="Patient Name required."
                                ValidationGroup="entry" ControlToValidate="cboPatientID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGender" runat="server" Text="Gender"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRGenderType" runat="server" Width="300px" Enabled="false">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
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
            <td width="50%">
                <table width="100%">
                    <tr runat="server">
                        <td class="label">
                            <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboGuarantorID" runat="server" Width="300px" HighlightTemplatedItems="True"
                                AutoPostBack="True" MarkFirstMatch="True" EnableLoadOnDemand="true" NoWrap="False"
                                OnItemDataBound="cboGuarantorID_ItemDataBound" OnItemsRequested="cboGuarantorID_ItemsRequested">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvGuarantorID" runat="server" ErrorMessage="Guarantor required."
                                ValidationGroup="entry" ControlToValidate="cboGuarantorID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trExtramural">
                        <td colspan="4">
                            <fieldset>
                                <legend>Extramural Items
                                </legend>
                                <telerik:RadGrid ID="grdExtramural" runat="server" OnNeedDataSource="grdExtramural_NeedDataSource"
                                    ShowFooter="true" AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdExtramural_UpdateCommand"
                                    OnDeleteCommand="grdExtramural_DeleteCommand" OnInsertCommand="grdExtramural_InsertCommand">
                                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ID">
                                        <Columns>
                                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle CssClass="MyImageButton" />
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridTemplateColumn HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <%# !IsReturn ? "" : (DataBinder.Eval(Container.DataItem, "IsReturned").Equals(true) ? 
                                                        "<img src=\"../../../../Images/Toolbar/post16.png\" border=\"0\" /></a>":
                                                        string.Format("<a href=\"#\" onclick=\"ItemReturn('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/post16_d.png\" border=\"0\" /></a>",
                                                                        DataBinder.Eval(Container.DataItem, "ID")))%>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="SRExtramuralItemName" HeaderText="Item Name" UniqueName="SRExtramuralItemName"
                                                SortExpression="SRExtramuralItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Qty" HeaderText="Qty"
                                                UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Right"
                                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="140px" DataField="LeasingPeriodInDays" HeaderText="Return Period (In Days)"
                                                UniqueName="LeasingPeriodInDays" SortExpression="LeasingPeriodInDays" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="GuarantyAmount" HeaderText="Guaranty"
                                                UniqueName="GuarantyAmount" SortExpression="GuarantyAmount" HeaderStyle-HorizontalAlign="Right"
                                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                            <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsReturned" HeaderText="Returned"
                                                UniqueName="IsReturned" SortExpression="IsReturned" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ReturnDate"
                                                HeaderText="Return Date" UniqueName="ReturnDate" SortExpression="ReturnDate"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <EditFormSettings UserControlName="NonPatientCustomerChargesExtramuralItemDetail.ascx" EditFormType="WebUserControl">
                                            <EditColumn UniqueName="NonPatientCustomerChargesExtramuralItemDetailEditCommand">
                                            </EditColumn>
                                        </EditFormSettings>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="false">
                                        <Resizing AllowColumnResize="True" />
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdTransChargesItem" runat="server" OnNeedDataSource="grdTransChargesItem_NeedDataSource"
        ShowFooter="true" AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdTransChargesItem_UpdateCommand"
        OnDeleteCommand="grdTransChargesItem_DeleteCommand" OnInsertCommand="grdTransChargesItem_InsertCommand"
        OnItemCreated="grdTransChargesItem_ItemCreated" OnItemCommand="grdTransChargesItem_ItemCommand">
        <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, SequenceNo"
            FilterExpression="ParentNo = ''">
            <CommandItemTemplate>
                &nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# !grdTransChargesItem.MasterTableView.IsItemInserted %>'>
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/insert16.png" />
                    &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new record"></asp:Label>
                </asp:LinkButton>
                &nbsp;&nbsp;
                <asp:LinkButton ID="lbPickList" runat="server" Visible='<%# !grdTransChargesItem.MasterTableView.IsItemInserted %>'
                    OnClientClick="javascript:openWinPickList();return false;">
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/views16.png" />
                    &nbsp;<asp:Label runat="server" ID="lblPicList" Text="Item picker"></asp:Label>
                </asp:LinkButton>
            </CommandItemTemplate>
            <CommandItemStyle Height="29px" />
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn UniqueName="extra" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsPackage").Equals(false) || DataBinder.Eval(Container.DataItem, "IsApprove").Equals(true)? string.Empty :
                                string.Format("<a href=\"#\" onclick=\"openWinExtra('{0}','{1}','{2}'); return false;\"><img src=\"../../../../Images/Toolbar/insert16.png\" border=\"0\" /></a>",
                                    DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"), 
                                        DataBinder.Eval(Container.DataItem, "ItemID")))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="comp" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsPackage").Equals(false) || DataBinder.Eval(Container.DataItem, "IsApprove").Equals(true)? string.Empty :
                                string.Format("<a href=\"#\" onclick=\"openWinComp('{0}','{1}','{2}'); return false;\"><img src=\"../../../../Images/Toolbar/dokter.png\" border=\"0\" /></a>",
                                    DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"), 
                                        DataBinder.Eval(Container.DataItem, "ItemID")))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="cons" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsPackage").Equals(false) || DataBinder.Eval(Container.DataItem, "IsApprove").Equals(true)? string.Empty :
                                string.Format("<a href=\"#\" onclick=\"openWinCons('{0}','{1}','{2}'); return false;\"><img src=\"../../../../Images/Toolbar/consumption.png\" border=\"0\" /></a>",
                                    DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"), 
                                        DataBinder.Eval(Container.DataItem, "ItemID")))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ParentNo" UniqueName="ParentNo" SortExpression="ParentNo"
                    Visible="false" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ParamedicCollectionName" HeaderText="Physician"
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
                    SortExpression="Total" Expression="(({0} * {1}) - {2}) + {3}" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsBillProceed" HeaderText="Locked"
                    UniqueName="IsBillProceed" SortExpression="IsBillProceed" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="False" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                    HeaderText="Updater" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="NonPatientCustomerChargesItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="NonPatientCustomerChargesItemEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
