<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    Title="Prescription Return" CodeBehind="PrescriptionTransactionList.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Charges.Dispensary.PrescriptionTransactionList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboOrderNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtOrderNotes" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboDispensaryID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboLocationID" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdTransPrescriptionItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransPrescriptionItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="196px" ReadOnly="true" />
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
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="244px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="22px" ReadOnly="true" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>

                        </td>
                        <td width="20"></td>
                        <td></td>
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
            <td style="width: 50%; vertical-align: top">
                <table width="100%">

                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnit" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" TextMode="MultiLine" ReadOnly="True" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <asp:Panel runat="server" ID="pnlDispensary">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDispensaryID" runat="server" Text="Dispensary"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboDispensaryID" runat="server" Width="300px" AutoPostBack="true"
                                    OnSelectedIndexChanged="cboDispensaryID_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvDispensaryID" runat="server" ErrorMessage="Dispensary required."
                                    ValidationGroup="entry" ControlToValidate="cboDispensaryID" SetFocusOnError="True"
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
                    </asp:Panel>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdTransPrescriptionItem" runat="server" OnNeedDataSource="grdTransPrescriptionItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" AllowPaging="false" AllowMultiRowSelection="true"
        OnItemDataBound="grdTransPrescriptionItem_ItemDataBound">
        <MasterTableView DataKeyNames="PrescriptionNo, SequenceNo" GroupLoadMode="Client">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="PrescriptionNo" HeaderText="Prescription No " />
                        <telerik:GridGroupByField FieldName="ApprovalDateTime" HeaderText="Date " />
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="ApprovalDateTime" SortOrder="Descending" />
                        <telerik:GridGroupByField FieldName="PrescriptionNo" SortOrder="Descending" />
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <HeaderTemplate>
                        <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                            runat="server"></asp:CheckBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="detailChkbox" runat="server"></asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="RegistrationNo" UniqueName="RegistrationNo" SortExpression="RegistrationNo"
                    Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PrescriptionNo" UniqueName="PrescriptionNo" SortExpression="PrescriptionNo"
                    Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SequenceNo" UniqueName="SequenceNo" SortExpression="SequenceNo"
                    Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ItemID" UniqueName="ItemID" SortExpression="ItemID"
                    Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="TemplateItemName">
                    <ItemTemplate>
                        <asp:Label ID="lblItemName" runat="server" Text='<%# GetItemName(DataBinder.Eval(Container.DataItem, "IsRFlag"), DataBinder.Eval(Container.DataItem, "ItemName")) %>' />
                        <br />
                        <i>Batch No :</i><asp:Label ID="lblBacthNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BatchNo") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridNumericColumn DataField="ResultQty" HeaderText="Available <br />QTY" UniqueName="ResultQty"
                    SortExpression="ResultQty" DataFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridTemplateColumn UniqueName="TemplateColumn1" HeaderStyle-Width="80px"
                    ItemStyle-HorizontalAlign="Center" HeaderText="Qty" HeaderStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtQty" runat="server" NumberFormat-DecimalDigits="2"
                            Value='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "ResultQty")) %>'
                            Width="100%">
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="SRItemUnit" HeaderText="Unit" UniqueName="SRItemUnit"
                    SortExpression="SRItemUnit">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn DataField="Price" HeaderText="Price" UniqueName="Price"
                    SortExpression="Price" DataFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="RecipeAmount" HeaderText="Recipe" UniqueName="Recipe"
                    SortExpression="Recipe" DataFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="DiscountAmount" HeaderText="Discount" UniqueName="DiscountAmount"
                    SortExpression="DiscountAmount" DataFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="Total" HeaderText="Total" UniqueName="Total"
                    SortExpression="Total" DataFormatString="{0:n2}">
                    <FooterStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn2" HeaderStyle-Width="50px"
                    HeaderText="Admin" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIsUsingAdmin" runat="server"></asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="TemplateColumn2" HeaderStyle-Width="60px"
                    ItemStyle-HorizontalAlign="Center" HeaderText="Returned Qty" HeaderStyle-HorizontalAlign="center" Visible="false">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtReturnedQty" runat="server" NumberFormat-DecimalDigits="2"
                            Value='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "ReturnedQty")) %>'
                            Width="100%">
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
        </ClientSettings>
    </telerik:RadGrid>
    <br />
    <br />
</asp:Content>
