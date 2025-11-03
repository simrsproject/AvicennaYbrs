<%@  Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="DirectPrescriptionReturnDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.DirectPrescriptionReturnDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">
            function OnButtonClick() {
                var oWnd = $find("<%= winCharges.ClientID %>");

                oWnd.setUrl('DirectPrescriptionReturnItemList.aspx');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd, args) {
                var txtRefNo = $find("<%= txtReferenceNo.ClientID %>");

                if (oWnd.argument)
                    txtRefNo.set_value(oWnd.argument.refno);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="850px" Height="600px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" Title="Prescription List"
        OnClientClose="onClientClose" ID="winCharges">
    </telerik:RadWindow>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPrescriptionNo" runat="server" Text="Prescription No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPrescriptionNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPrescriptionNo" runat="server" ErrorMessage="Prescription No required."
                                ValidationGroup="entry" ControlToValidate="txtPrescriptionNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPrescriptionDate" runat="server" Text="Prescription Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtPrescriptionDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                DatePopupButton-Enabled="false">
                            </telerik:RadDatePicker>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPrescriptionDate" runat="server" ErrorMessage="Prescription Date required."
                                ValidationGroup="entry" ControlToValidate="txtPrescriptionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Dispensary"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Dispensary ID required."
                                ValidationGroup="entry" ControlToValidate="cboServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
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
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReferenceNo" runat="server" Text="Reference No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtReferenceNo" runat="server" Width="300px" MaxLength="20"
                                ShowButton="true" AutoPostBack="true" ClientEvents-OnButtonClick="OnButtonClick"
                                OnTextChanged="txtReferenceNo_TextChanged" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMedicalNo" runat="server" Text="Patient ID / Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing=" 0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientID" runat="server" Width="145px" ReadOnly="true" />

                                    </td>
                                    <td width="5px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="145px" ReadOnly="true" />
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
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtSalutation" runat="server" Width="28px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px">
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="270px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
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
                    <tr style="height: 24px">
                        <td class="label">
                            <asp:Label ID="lblSex" runat="server" Text="Gender"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:RadioButton ID="optSexFemale" runat="server" Text="Female" GroupName="Sex" />
                            <asp:RadioButton ID="optSexMale" runat="server" Text="Male" GroupName="Sex" />
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
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtParamedicID" runat="server" Width="100px" MaxLength="20"
                                ReadOnly="true" />
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
                            <asp:Label ID="lblClusterID" runat="server" Text="Service Unit"></asp:Label>
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
                            <asp:Label ID="lblClassID" runat="server" Text="Class"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtClassID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblClassName" runat="server"></asp:Label>
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
    <telerik:RadGrid ID="grdTransPrescriptionItem" runat="server" OnNeedDataSource="grdTransPrescriptionItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdTransPrescriptionItem_UpdateCommand"
        OnDeleteCommand="grdTransPrescriptionItem_DeleteCommand" OnInsertCommand="grdTransPrescriptionItem_InsertCommand"
        ShowFooter="true">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="SequenceNo">
            <Columns>
                <telerik:GridBoundColumn DataField="SequenceNo" HeaderText="Seq. No" UniqueName="SequenceNo"
                    HeaderStyle-Width="75px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="ParentNo" HeaderText="Parent No" UniqueName="ParentNo"
                    HeaderStyle-Width="75px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="False" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsRFlag" HeaderText="R/"
                    UniqueName="IsRFlag" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="TemplateItemName">
                    <ItemTemplate>
                        <asp:Label ID="lblItemName" runat="server" Text='<%# GetItemName(DataBinder.Eval(Container.DataItem, "IsRFlag"), DataBinder.Eval(Container.DataItem, "ItemName")) %>' />
                        <br />
                        <i>Intervention :</i>
                        <asp:Label ID="lblInterventionItemName" runat="server" Font-Italic="true" Text='<%# GetItemName(DataBinder.Eval(Container.DataItem, "IsRFlag"), DataBinder.Eval(Container.DataItem, "ItemInterventionName")) %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="FrequencyOfDosing"
                    HeaderText="Frequency" UniqueName="FrequencyOfDosing" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="DosingPeriod" HeaderText="Times"
                    UniqueName="DosingPeriod" SortExpression="DosingPeriod" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="DurationOfDosing" HeaderText="Duration"
                    UniqueName="DurationOfDosing" SortExpression="DurationOfDosing" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="ResultQty" HeaderText="Qty"
                    UniqueName="ResultQty" SortExpression="ResultQty" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRItemUnit" HeaderText="Unit"
                    UniqueName="SRItemUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="110px" DataField="Price" HeaderText="Price"
                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="DiscountAmount" HeaderText="Discount"
                    UniqueName="DiscountAmount" SortExpression="DiscountAmount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RecipeAmount" HeaderText="Recipe"
                    UniqueName="RecipeAmount" SortExpression="RecipeAmount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridCalculatedColumn HeaderStyle-Width="120px" HeaderText="Total" UniqueName="Total"
                    DataType="System.Double" DataFields="LineAmount" SortExpression="Total" Expression="{0}"
                    FooterStyle-HorizontalAlign="Right" Aggregate="Sum" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="35px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="PrescriptionSalesItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="TransPrescriptionItemEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
