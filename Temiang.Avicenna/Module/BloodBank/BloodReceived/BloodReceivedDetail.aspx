<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="BloodReceivedDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.BloodBank.BloodReceivedDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No / Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="140px" ReadOnly="true" />
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtTransctionDate" runat="server" Width="100px" Enabled="False">
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTransactionNo" runat="server" ErrorMessage="Transaction No required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvTransactionDate" runat="server" ErrorMessage="Transaction Date required."
                                ValidationGroup="entry" ControlToValidate="txtTransctionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / MRN"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="190px" MaxLength="20"
                                            ReadOnly="true" />
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="103px" MaxLength="15"
                                            ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
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
                                    <td style="width: 3px">
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="243px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px">
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="23px" ReadOnly="true" />
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
                        <td class="label">
                            <asp:Label ID="lblPlaceDOB" runat="server" Text="City / Date Of Birth"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
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
                        <td width="20">
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
                            <telerik:RadTextBox ID="txtParamedicID" runat="server" Width="100px" ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblParamedicName" runat="server"></asp:Label>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100px" MaxLength="20"
                                ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblServiceUnitName" runat="server"></asp:Label>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRoomID" runat="server" Text="Room"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRoomID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblRoomName" runat="server"></asp:Label>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBedID" runat="server" Text="Bed No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBedID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
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
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table style="width: 100%" cellpadding="0" cellspacing="1">
        <tr>
            <td>
                <fieldset>
                    <legend>REQUEST INFORMATION</legend>
                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblRequestDateTime" runat="server" Text="Request Date / Time"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadDatePicker ID="txtRequestDate" runat="server" Width="100px" Enabled="False">
                                                        </telerik:RadDatePicker>
                                                    </td>
                                                    <td>
                                                        <telerik:RadMaskedTextBox ID="txtRequestTime" runat="server" Mask="<00..23>:<00..59>"
                                                            PromptChar="_" RoundNumericRanges="false" Width="50px" Enabled="False">
                                                        </telerik:RadMaskedTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20px">
                                            <asp:RequiredFieldValidator ID="rfvRequestDate" runat="server" ErrorMessage="Request Date required."
                                                ValidationGroup="entry" ControlToValidate="txtRequestDate" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="rfvRequestTime" runat="server" ErrorMessage="Request Time required."
                                                ValidationGroup="entry" ControlToValidate="txtRequestTime" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblBloodBankNo" runat="server" Text="Blood Bank No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtBloodBankNo" runat="server" Width="300px" MaxLength="50"
                                                ReadOnly="True" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblPdutNo" runat="server" Text="PDUT No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtPdutNo" runat="server" Width="300px" MaxLength="50" ReadOnly="True" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblBloodType" runat="server" Text="Blood Type / Rhesus"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadComboBox runat="server" ID="cboSRBloodType" Width="105px" AllowCustomText="true"
                                                            Filter="Contains" Enabled="False">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td style="width: 5px">
                                                    </td>
                                                    <td>
                                                        <asp:RadioButtonList ID="rblBloodRhesus" runat="server" RepeatDirection="Horizontal"
                                                            RepeatLayout="Flow" Enabled="False">
                                                            <asp:ListItem Value="0" Text="+" />
                                                            <asp:ListItem Value="1" Text="-" />
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20px">
                                            <asp:RequiredFieldValidator ID="rfvSRBloodType" runat="server" ErrorMessage="Blood Type required."
                                                ValidationGroup="entry" ControlToValidate="cboSRBloodType" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSRBloodGroupRequest" runat="server" Text="Blood Group"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox runat="server" ID="cboSRBloodGroupRequest" Width="300px" AllowCustomText="true"
                                                Filter="Contains" Enabled="False">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20px">
                                            <asp:RequiredFieldValidator ID="rfvSRBloodGroupRequest" runat="server" ErrorMessage="Blood Type required."
                                                ValidationGroup="entry" ControlToValidate="cboSRBloodGroupRequest" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblHbResultValue" runat="server" Text="Hemoglobin Result"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtHbResultValue" runat="server" Width="100px" NumberFormat-DecimalDigits="2"
                                                            ReadOnly="True" />
                                                    </td>
                                                    <td style="width: 5px">
                                                    </td>
                                                    <td>
                                                        gram/dL
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
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblQtyBagRequest" runat="server" Text="Qty Bag / Volume"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtQtyBagRequest" runat="server" Width="100px" ReadOnly="True" />
                                                    </td>
                                                    <td style="width: 5px">
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtVolumeBag" runat="server" Width="100px" ReadOnly="True" />
                                                        ML/CC
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20px">
                                            <asp:RequiredFieldValidator ID="rfvQtyBagRequest" runat="server" ErrorMessage="Qty Bag required."
                                                ControlToValidate="txtQtyBagRequest" SetFocusOnError="True" ValidationGroup="entry"
                                                Width="100%">
                                                <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="rfvVolumeBag" runat="server" ErrorMessage="Volume required."
                                                ControlToValidate="txtVolumeBag" SetFocusOnError="True" ValidationGroup="entry"
                                                Width="100%">
                                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblDiagnose" runat="server" Text="Diagnosis"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtDiagnose" runat="server" Width="300px" MaxLength="500"
                                                ReadOnly="True" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblReason" runat="server" Text="Reason"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtReason" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine"
                                                ReadOnly="True" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblOfficer" runat="server" Text="Request By"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtOfficerByUserName" runat="server" Width="300px" ReadOnly="True" />
                                            <telerik:RadTextBox ID="txtOfficerByUserID" runat="server" Visible="False" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <asp:Panel ID="pnlValidatedByCasemix" runat="server">
                                        <tr runat="server">
                                            <td class="label"></td>
                                            <td class="entry">
                                                <asp:CheckBox runat="server" ID="chkIsValidatedByCasemix" Text="Validated By Casemix" Enabled="false" />
                                            </td>
                                            <td width="20px"></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="lblQtyBagCasemixAppr" runat="server" Text="Qty Bag Approved"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadNumericTextBox ID="txtQtyBagCasemixAppr" runat="server" Width="100px" ReadOnly="True" />
                                            </td>
                                            <td width="20px"></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="lblCasemixNotes" runat="server" Text="Casemix Notes"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadTextBox ID="txtCasemixNotes" runat="server" Width="300px" ReadOnly="true" />
                                            </td>
                                            <td width="20px"></td>
                                            <td></td>
                                        </tr>
                                    </asp:Panel>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdItem_DeleteCommand"
        OnInsertCommand="grdItem_InsertCommand" OnUpdateCommand="grdItem_UpdateCommand"
        OnItemCreated="grdItem_ItemCreated">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="BagNo">
            <ColumnGroups>
                <telerik:GridColumnGroup HeaderText="Submitted" Name="Submitted" HeaderStyle-HorizontalAlign="Center">
                </telerik:GridColumnGroup>
            </ColumnGroups>
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="150px" DataField="BagNo" HeaderText="Bag No" UniqueName="BagNo"
                    SortExpression="BagNo">
                    <ItemTemplate>
                        <%# string.Format("{0}", DataBinder.Eval(Container.DataItem, "BagNo"))%><br />
                        <i>ED:&nbsp;<%# DataBinder.Eval(Container.DataItem, "ExpiredDateTime", "{0:dd-MMM-yyyy HH:mm}")%></i>]
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="VolumeBag"
                    HeaderText="Volume (ML/CC)" UniqueName="VolumeBag" SortExpression="VolumeBag"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="BloodBagTemperature"
                    HeaderText="Blood Bag Temperature (°C)" UniqueName="BloodBagTemperature" SortExpression="BloodBagTemperature"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn DataField="ReceivedDate" HeaderText="Date" UniqueName="ReceivedDate"
                    SortExpression="ReceivedDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}" ColumnGroupName="Submitted">
                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="55px" DataField="ReceivedTime" HeaderText="Time"
                    UniqueName="ReceivedTime" SortExpression="ReceivedTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Submitted"/>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ExaminerByUserName"
                    HeaderText="By" UniqueName="ExaminerByUserName" SortExpression="ExaminerByUserName"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ColumnGroupName="Submitted"/>

                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="UnitOfficer" HeaderText="Received By"
                    UniqueName="UnitOfficer" SortExpression="UnitOfficer" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Left" />

                <telerik:GridCheckBoxColumn HeaderStyle-Width="90px" DataField="IsProceedToTransfusion"
                    HeaderText="Proceed To Transfusion" UniqueName="IsProceedToTransfusion" SortExpression="IsProceedToTransfusion"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="BloodBagNotes" HeaderText="Notes" UniqueName="BloodBagNotes"
                    SortExpression="BloodBagNotes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="BloodBagStatusName"
                    HeaderText="Blood Status" UniqueName="BloodBagStatusName" SortExpression="BloodBagStatusName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="BloodReceivedItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="grdBloodReceivedEditCommand">
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
</asp:Content>
