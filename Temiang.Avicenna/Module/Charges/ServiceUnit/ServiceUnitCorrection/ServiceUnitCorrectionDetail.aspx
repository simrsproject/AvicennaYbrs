<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="ServiceUnitCorrectionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.ServiceUnitCorrectionDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">

            function openWinCharges() {
                var oWnd = $find("<%= winCharges.ClientID %>");
                oWnd.setUrl('ServiceUnitCorrectionItem.aspx?regno=' + '<%= Request.QueryString["regno"] %>' + '&cid=' + '<%= Request.QueryString["cid"] %>' + '&disch=' + '<%= Request.QueryString["disch"] %>' + '&verif=' + '<%= Request.QueryString["verif"] %>');
                oWnd.set_title('Transaction List');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd, args) {
                var txtRefNo = $find("<%= txtReferenceNo.ClientID %>");

                if (oWnd.argument && oWnd.argument.refno != null) {
                    if (txtRefNo.get_value() == '') {
                        txtRefNo.set_value(oWnd.argument.refno);
                    }
                    else
                        __doPostBack("<%= grdTransChargesItem.UniqueID %>", "rebind");
                }
            }

            function openWinRegistrationInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationInfo.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }

        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="820px" Height="600px" Behavior="Move"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="onClientClose"
        ID="winCharges">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top;">
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
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date"></asp:Label>
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
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRShift" runat="server" Text="Shift"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtShiftID" runat="server" Width="100px" ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblShiftName" runat="server" CssClass="labeldescription" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRShift" runat="server" ErrorMessage="Shift required."
                                ValidationGroup="entry" ControlToValidate="txtShiftID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtFromServiceUnitID" runat="server" Width="100px" MaxLength="10"
                                ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblFromServiceUnitName" runat="server" CssClass="labeldescription" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvFromServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                                ValidationGroup="entry" ControlToValidate="txtFromServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReferenceNo" runat="server" Text="Reference No"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtReferenceNo" runat="server" Width="200px" MaxLength="20"
                                            AutoPostBack="true" OnTextChanged="txtReferenceNo_TextChanged" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnGet" runat="server" Text="Request" OnClientClick="javascript:openWinCharges();return false;" />
                                        <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvReferenceNo" runat="server" ErrorMessage="Reference No required."
                                ValidationGroup="entry" ControlToValidate="txtReferenceNo" SetFocusOnError="True">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMedicalNo" runat="server" Text="RegistrationNo / Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="194px" MaxLength="20"
                                ReadOnly="true" />
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                            <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();" class="noti_Container">
                                <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo"
                                    Text=""></asp:Label>&nbsp; </a>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvRegistrationNo" runat="server" ErrorMessage="Registration No required."
                                ValidationGroup="entry" ControlToValidate="txtRegistrationNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
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
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="243px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="23px" ReadOnly="true" />
                                    </td>
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
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" Height="45px"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsProceed" runat="server" Text="Proceed" Enabled="false" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top;">
                <fieldset id="FieldSet1" style="min-height: 150px;">
                    <legend>Photo</legend>
                    <div>
                        <asp:Image runat="server" ID="imgPatientPhoto" Width="150px" Height="150px" />
                    </div>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <telerik:RadGrid ID="grdTransChargesItem" runat="server" OnNeedDataSource="grdTransChargesItem_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdTransChargesItem_UpdateCommand"
                    OnDeleteCommand="grdTransChargesItem_DeleteCommand" OnItemCreated="grdTransChargesItem_ItemCreated"
                    OnItemCommand="grdTransChargesItem_ItemCommand" ShowFooter="true">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, SequenceNo">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                <HeaderStyle Width="35px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
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
                                SortExpression="Total" Expression="0 - ((((0 - {0}) * {1}) - {2}) + {3})" FooterStyle-HorizontalAlign="Right"
                                Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:n2}" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                                UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" Visible="false" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsBillProceed" HeaderText="Locked"
                                UniqueName="IsBillProceed" SortExpression="IsBillProceed" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" Visible="False" />
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                <HeaderStyle Width="35px" />
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings UserControlName="ServiceUnitCorrectionEntry.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="ServiceUnitCorrectionEntryEditCommand">
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
            </td>
        </tr>
    </table>
</asp:Content>
