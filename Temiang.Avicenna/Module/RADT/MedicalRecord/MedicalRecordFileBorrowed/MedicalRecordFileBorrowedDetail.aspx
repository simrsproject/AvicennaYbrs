<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="MedicalRecordFileBorrowedDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.MedicalRecordFileBorrowedDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPatientID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="False" HighlightTemplatedItems="true" OnItemsRequested="cboPatientID_ItemsRequested"
                                OnItemDataBound="cboPatientID_ItemDataBound" OnSelectedIndexChanged="cboPatientID_SelectedIndexChanged"
                                AutoPostBack="True">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "MedicalNo")%>
                                    </b>
                                    <br />
                                    <%# DataBinder.Eval(Container.DataItem, "PatientName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 5 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="300px" AutoPostBack="True"
                                MaxLength="11" OnTextChanged="txtMedicalNo_TextChanged" Visible="False" />
                            <telerik:RadTextBox ID="txtPatientID" runat="server" Visible="False" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvMedicalNo" runat="server" ErrorMessage="Medical No required."
                                ValidationGroup="entry" ControlToValidate="cboPatientID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvMedicalNo2" runat="server" ErrorMessage="Medical No required."
                                ValidationGroup="entry" ControlToValidate="txtMedicalNo" SetFocusOnError="True"
                                Width="100%" Visible="False">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboRegistrationNo" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="False" HighlightTemplatedItems="true" OnItemsRequested="cboRegistrationNo_ItemsRequested"
                                OnItemDataBound="cboRegistrationNo_ItemDataBound" OnSelectedIndexChanged="cboRegistrationNo_SelectedIndexChanged"
                                AutoPostBack="True">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "RegistrationNo")%>
                                    </b>
                                    <br />
                                    <%# DataBinder.Eval(Container.DataItem, "PatientName")%>
                                    <br />
                                    <%# DataBinder.Eval(Container.DataItem, "MedicalNo") %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 5 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20"
                                OnTextChanged="txtRegistrationNo_TextChanged" AutoPostBack="True" Visible="False" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPhysician" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPhysicianName" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGender" runat="server" Text="Gender"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtGender" runat="server" Width="80px" ReadOnly="True" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAge" runat="server" Width="80px" ReadOnly="True" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAddress" runat="server" Width="300px" ReadOnly="True"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2">
                <table width="100%">
                    <tr>
                        <td class="labelcaption">
                            <asp:Label ID="Label2" runat="server" Text="Borrowing Information"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td>
                <table width="100%">
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblDateOfBorrowing" runat="server" Text="Borrowed Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtDateOfBorrowing" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvDateOfBorrowing" runat="server" ErrorMessage="Date Of Borrowing required."
                                ValidationGroup="entry" ControlToValidate="txtDateOfBorrowing" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                                ValidationGroup="entry" ControlToValidate="cboServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNameOfTheBorrower" runat="server" Text="Borrower's Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNameOfTheBorrower" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvNameOfTheBorrower" runat="server" ErrorMessage="Name Of The Borrower required."
                                ValidationGroup="entry" ControlToValidate="txtNameOfTheBorrower" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRForThePurposesOf" runat="server" Text="For The Purposes Of"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRForThePurposesOf" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRForThePurposesOf" runat="server" ErrorMessage="For The Purposes Of required."
                                ValidationGroup="entry" ControlToValidate="cboSRForThePurposesOf" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblDuration" runat="server" Text="Duration"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtDuration" runat="server" Width="100px"
                                MaxValue="999" MinValue="0" NumberFormat-DecimalDigits="0" />
                            &nbsp;<asp:Label ID="lblDays" runat="server" Text="day(s)"></asp:Label>
                            <telerik:RadNumericTextBox ID="txtPrevDuration" runat="server" Width="100px" NumberFormat-DecimalDigits="0" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNameOfGivenID" runat="server" Text="Submitted By"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNameOfGiven" runat="server" Width="300px" ReadOnly="True" />
                            <telerik:RadTextBox ID="txtNameOfGivenID" runat="server" Visible="False" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblDateOfReturn" runat="server" Text="Returned Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtDateOfReturn" runat="server" Width="100px" Enabled="False" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReturnByName" runat="server" Text="Returned By"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtReturnByName" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNameOfRecipientID" runat="server" Text="Received By"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNameOfRecipient" runat="server" Width="300px" ReadOnly="True" />
                            <telerik:RadTextBox ID="txtNameOfRecipientID" runat="server" Visible="False" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdHistory" runat="server" OnNeedDataSource="grdHistory_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="none" DataKeyNames="LastUpdateDateTime">
            <Columns>
                <telerik:GridBoundColumn DataField="LastUpdateDateTime" HeaderText="Date/Time" UniqueName="LastUpdateDateTime"
                    SortExpression="LastUpdateDateTime" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy HH:mm}">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="UpdateBy"
                    HeaderText="Update By" UniqueName="UpdateBy" SortExpression="UpdateBy"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Duration" HeaderText="Duration Day(s)"
                    UniqueName="Duration" SortExpression="Duration" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridTemplateColumn/>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="True">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
