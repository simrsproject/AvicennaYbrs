<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrescriptionSalesItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Charges.PrescriptionSalesItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style type="text/css">
    .RightAligned
    {
        text-align: right;
    }
</style>
<asp:ValidationSummary ID="vsumTransPrescriptionItem" runat="server" ValidationGroup="TransPrescriptionItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="TransPrescriptionItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<asp:HiddenField runat="server" ID="hdnCasemixApprovedUserId"/>
<asp:HiddenField runat="server" ID="hdnCasemixNotes"/>
<table cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td>
            <table>
                <tr>
                    <td class="entrydescription">
                        <table style="border-color: Gray; border-style: solid; height: 1px">
                            <tr>
                                <td class="header" width="20px">
                                    C
                                </td>
                                <td class="header" runat="server" id="trHeader" visible="false">
                                    Header
                                </td>
                                <td colspan="2" class="header">
                                    Item
                                </td>
                                <td class="header" runat="server" id="trIntervention">
                                    Intervention
                                </td>
                                <td class="header" colspan="3">
                                    Numero
                                </td>
                                <td class="header" colspan="3" runat="server" id="tdFormula" visible="false">
                                    Formula
                                </td>
                                <td class="header" runat="server" id="tdInterventionReasonHd" visible="false">
                                    Intervention Reason
                                </td>
                            </tr>
                            <tr>
                                <td width="20px">
                                    <asp:CheckBox ID="chkIsCompound" runat="server" AutoPostBack="true" OnCheckedChanged="chkIsCompound_CheckedChanged" />
                                </td>
                                <td runat="server" id="trHeader2" visible="false">
                                    <telerik:RadComboBox ID="cboParentNo" runat="server" Width="229px" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboParentNo_SelectedIndexChanged" />
                                </td>
                                <td>
                                    <telerik:RadComboBox runat="server" ID="cboItemID" Width="229px" EnableLoadOnDemand="true"
                                        HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboItemID_ItemDataBound"
                                        OnItemsRequested="cboItemID_ItemsRequested" OnSelectedIndexChanged="cboItemID_SelectedIndexChanged">
                                        <ItemTemplate>
                                            <b><%# DataBinder.Eval(Container.DataItem, "ItemName") %></b>
                                            <br />
                                            (<%# DataBinder.Eval(Container.DataItem, "ItemID")%>)&nbsp;<%# DataBinder.Eval(Container.DataItem, "Fornas") %>&nbsp;
                                            <br />
                                            Stock : <b><%# DataBinder.Eval(Container.DataItem, "Balance")%>&nbsp;(<%# DataBinder.Eval(Container.DataItem, "BalanceAll")%>)</b>&nbsp;<%# DataBinder.Eval(Container.DataItem, "SRItemUnit") %>
                                            <a href="javascript:void(0);" onclick="javascript:openWinBalanceInfo('<%# DataBinder.Eval(Container.DataItem, "ItemID")%>')"><img src="../../../../Images/infoblue16.png" border="0" alt="Show Location" title="Show Balance All Location" /></a>
                                            <br />
                                            <i><span style="color: orangered"><%# DataBinder.Eval(Container.DataItem, "FornasRestrictionNotes")%></span></i>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 50 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>

                                                    <%--fungsi cboItemID_ClientItemsRequesting ada di parent page--%>
                <%--<telerik:RadComboBox ID="cboItemID" runat="server" Width="229px" EmptyMessage="Select a Item"
                    EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true" AutoPostBack="true"
                    OnClientItemsRequesting="cboItemID_ClientItemsRequesting" OnClientFocus="showDropDown"
                    OnSelectedIndexChanged="cboItemID_SelectedIndexChanged">
                    <WebServiceSettings Method="PrescriptionItemSelection" Path="~/WebService/ComboBoxDataService.asmx" />
                    <ClientItemTemplate>
                     <div>
                        <ul class="details" style="#= Attributes.Style #">
                            <li class="bold"><span>#= Text #</span></li>
                            <li class="small" style="display:#= _stockStyleDisplay #"><span> ST: #= Attributes.Balance # (#= Attributes.BalanceTotal #) #= Attributes.SRItemUnit # <a href="javascript:void(0);" onclick="javascript:openWinBalanceInfo('#= Value #')"><img src="../../../../Images/infoblue16.png" border="0" alt="Show Location" title="Show Balance All Location" /></a></span></li>
                            <li class="smaller"><span>ZA: #= Attributes.ZatActive #  </span></li>
                            <li class="smaller"><span>#= Attributes.GenericFlag #</span></li>
                        </ul>
                    </div>
                    </ClientItemTemplate>
                </telerik:RadComboBox>--%>
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item ID required."
                                        ValidationGroup="TransPrescriptionItem" ControlToValidate="cboItemID" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td runat="server" id="trIntervention2">
<%--                                    <telerik:RadComboBox runat="server" ID="cboItemInterventionID" Width="229px" EnableLoadOnDemand="true"
                                        HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboItemInterventionID_ItemDataBound"
                                        OnItemsRequested="cboItemInterventionID_ItemsRequested" OnSelectedIndexChanged="cboItemInterventionID_SelectedIndexChanged">
                                        <ItemTemplate>
                                            <b><%# DataBinder.Eval(Container.DataItem, "ItemName") %></b>
                                            <br />
                                            (<%# DataBinder.Eval(Container.DataItem, "ItemID")%>)&nbsp;<%# DataBinder.Eval(Container.DataItem, "Fornas") %>
                                            <br />
                                            Stock : <b><%# DataBinder.Eval(Container.DataItem, "Balance")%>&nbsp;(<%# DataBinder.Eval(Container.DataItem, "BalanceAll")%>)</b>&nbsp;<%# DataBinder.Eval(Container.DataItem, "SRItemUnit") %>
                                            <a href="javascript:void(0);" onclick="javascript:openWinBalanceInfo('<%# DataBinder.Eval(Container.DataItem, "ItemID")%>')"><img src="../../../../Images/infoblue16.png" border="0" alt="Show Location" title="Show Balance All Location" /></a>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 50 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>--%>
                                                                                        <%--fungsi cboItemID_ClientItemsRequesting ada di parent page--%>
                <telerik:RadComboBox ID="cboItemInterventionID" runat="server" Width="229px" EmptyMessage="Select a Item"
                    EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true" AutoPostBack="true"
                    OnClientItemsRequesting="cboItemID_ClientItemsRequesting" OnClientFocus="showDropDown"
                    OnSelectedIndexChanged="cboItemInterventionID_SelectedIndexChanged">
                    <WebServiceSettings Method="PrescriptionItemSelection" Path="~/WebService/ComboBoxDataService.asmx" />
                    <ClientItemTemplate>
                     <div>
                        <ul class="details" style="#= Attributes.Style #">
                            <li class="bold"><span>#= Text #</span></li>
                            <li class="small" style="display:#= _stockStyleDisplay #"><span> ST: #= Attributes.Balance # (#= Attributes.BalanceTotal #) #= Attributes.SRItemUnit # <a href="javascript:void(0);" onclick="javascript:openWinBalanceInfo('#= Value #')"><img src="../../../../Images/infoblue16.png" border="0" alt="Show Location" title="Show Balance All Location" /></a></span></li>
                            <li class="smaller"><span>ZA: #= Attributes.ZatActive #  </span></li>
                            <li class="smaller"><span>#= Attributes.GenericFlag #</span></li>
                            <li class="smaller"><i><span style="color: orangered">#= Attributes.FornasRestrictionNotes #</span></i></li>
                        </ul>
                    </div>
                    </ClientItemTemplate>
                </telerik:RadComboBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtPrescriptionQty" runat="server" Width="80px" AutoPostBack="true"
                                        CssClass="RightAligned" OnTextChanged="txtPrescriptionQty_TextChanged" />
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Numero required."
                                        ValidationGroup="TransPrescriptionItem" ControlToValidate="txtPrescriptionQty"
                                        SetFocusOnError="True" Width="100%">
                                        <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtItemUnit" runat="server" Width="80px" ReadOnly="true" />
                                    <telerik:RadComboBox ID="cboEmbalace" runat="server" Width="104px" AutoPostBack="True"
                                        AllowCustomText="true" Filter="Contains" OnSelectedIndexChanged="cboEmbalace_SelectedIndexChanged"
                                        Visible="false" />
                                </td>
                                <td runat="server" id="tdFormula2" visible="false">
                                    <telerik:RadTextBox ID="txtDosage" runat="server" Width="80px" AutoPostBack="true"
                                        CssClass="RightAligned" OnTextChanged="txtDosage_TextChanged" />
                                </td>
                                <td runat="server" id="tdFormula3" visible="false">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Dosing required."
                                        ValidationGroup="TransPrescriptionItem" ControlToValidate="txtDosage" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td runat="server" id="tdFormula4" visible="false">
                                    <telerik:RadComboBox ID="cboItemUnit" runat="server" Width="104px" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboItemUnit_SelectedIndexChanged" />
                                </td>
                                <td runat="server" id="tdInterventionReasonDt" visible="false">
                                    <telerik:RadComboBox ID="cboSRInterventionReason" runat="server" Width="157px" />
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
        <td>
            <asp:Panel ID="pnlInfoLastBuy" runat="server" Visible="false" Width="100%">
                <fieldset style="padding: 0">
                    <legend><b>Previous Transaction Info</b></legend>
                    <table>
                        <tr>
                            <td class="header" style="text-align: center;">
                                Item Name
                            </td>
                            <td class="header" style="text-align: center;">
                                Qty
                            </td>
                            <td class="header" style="text-align: center;">
                                Unit
                            </td>
                            <td class="header" style="text-align: center;">
                                Date(d/m/y)
                            </td>
                            <td class="header" style="text-align: center;">
                                Day(s)
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Label ID="lblPrevItemName" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblPrevItemQty" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblPrevItemSRUnit" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblPrevDate" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTotalDays" runat="server"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                             <td style="text-align: center;" colspan="5" class="header">
                                <asp:Label ID="lblPrevConsumMethod" runat="server"></asp:Label>
                            </td>
                         </tr>
                    </table>
                </fieldset>
            </asp:Panel>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td class="entrydescription">
            <table style="border-color: Gray; border-style: solid; height: 1px">
                <tr>
                    <td class="header" colspan="2">
                        Consume Method
                    </td>
                    <td class="header" colspan="3">
                        Dosing
                    </td>
                    <td class="header">
                        Consume
                    </td>
                    <td class="header">
                        Order
                    </td>
                    <td class="header">
                        Iter
                    </td>
                    <td class="header">
                        Days of usage
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--<telerik:RadComboBox runat="server" ID="cboConsumeMethod" Width="229px" AllowCustomText="true"
                            Filter="Contains" />--%>
                        <telerik:RadComboBox ID="cboConsumeMethod" runat="server" Width="229px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" OnItemDataBound="cboConsumeMethod_ItemDataBound"
                            OnItemsRequested="cboConsumeMethod_ItemsRequested">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvConsumeMethod" runat="server" ErrorMessage="Consume Method required."
                            ValidationGroup="TransPrescriptionItem" ControlToValidate="cboConsumeMethod"
                            SetFocusOnError="True" Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtQtyConsume" runat="server" Width="80px" CssClass="RightAligned" />
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cboConsumeUnit" runat="server" Width="104px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" OnItemDataBound="cboConsumeUnit_ItemDataBound"
                            OnItemsRequested="cboConsumeUnit_ItemsRequested">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Consume Unit required."
                            ValidationGroup="TransPrescriptionItem" ControlToValidate="cboConsumeUnit" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cboAcPcDc" runat="server" Width="150px" AllowCustomText="true"
                            Filter="Contains">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtOrder" runat="server" MaxLength="500" />
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtIter" runat="server" MaxLength="500" />
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtDaysOfUsage" runat="server" Width="80px" MinValue="0"
                            NumberFormat-DecimalDigits="0" />
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
<table>
    <tr>
        <td class="entrydescription">
            <table style="border-color: Gray; border-style: solid; height: 1px">
                <tr>
                    <td class="header" colspan="2">
                        <asp:Label ID="lblTakenQty" runat="server" Text="Taken Qty"></asp:Label>
                    </td>
                    <td class="header">
                        <asp:Label ID="lblCompoundResult" runat="server" Text="Result Qty"></asp:Label>
                    </td>
                    <td class="header">
                        <asp:Label ID="lblPrice" runat="server" Text="Price"></asp:Label>
                    </td>
                    <td class="header">
                        <asp:Label ID="lblRecipeAmount" runat="server" Text="Recipe Amount"></asp:Label>
                    </td>
                    <td class="header">
                        <asp:Label ID="lblEmbalaceAmount" runat="server" Text="Embalace"></asp:Label>
                    </td>
                    <td class="header">
                        <asp:Label ID="lblDiscountPercent" runat="server" Text="Discount (%)"></asp:Label>
                    </td>
                    <td class="header">
                        <asp:Label ID="lblDiscount" runat="server" Text="Discount (Rp)"></asp:Label>
                    </td>
                    <td class="header">
                        <asp:Label ID="lblDiscountReason" runat="server" Text="Discount Reason"></asp:Label>
                    </td>
                    <td class="header">
                        <asp:Label ID="lblLineAmount" runat="server" Text="Total"></asp:Label>
                    </td>
                    <td class="header" runat="server" id="tdlblQty23Days">
                        <asp:Label ID="lblQty23Days" runat="server" Text="23 Days Qty"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadNumericTextBox ID="txtTakenQty" runat="server" Width="80px" AutoPostBack="true"
                            OnTextChanged="txtTakenQty_TextChanged" MinValue="0" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Taken Qty required."
                            ValidationGroup="TransPrescriptionItem" ControlToValidate="txtTakenQty" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtResultQty" runat="server" Width="80px" ReadOnly="True" />
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtPrice" runat="server" Width="100px" ReadOnly="True" />
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtRecipeAmount" runat="server" Width="100px" ReadOnly="True"
                            AutoPostBack="True" OnTextChanged="txtRecipeAmount_TextChanged" />
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtEmbalaceAmount" runat="server" Width="100px" ReadOnly="True"/>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtDiscountPercent" runat="server" Width="100px" AutoPostBack="true"
                            OnTextChanged="txtDiscountPercent_TextChanged" />
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtDiscountAmount" runat="server" Width="100px" AutoPostBack="true"
                            OnTextChanged="txtDiscountAmount_TextChanged" />
                    </td>
                    <td>
                        <telerik:RadComboBox runat="server" ID="cboDiscountReason" Width="154px" />
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtLineAmount" runat="server" Width="100px" ReadOnly="true" />
                    </td>
                    <td runat="server" id="tdTxtQty23Days">
                        <telerik:RadNumericTextBox ID="txtQty23Days" runat="server" Width="80px" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="20px" />
        <td />
    </tr>
    <tr>
        <td class="entrydescription">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="45%">
                        <telerik:RadTextBox ID="txtNotes" runat="server" Width="100%" MaxLength="500" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td width="40%">
                        <telerik:RadTextBox ID="txtCasemixApprovedByUserID" runat="server" ReadOnly="true" Width="100%" />
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="chkIsPending" Text="Pending Delivery" />
                        <asp:CheckBox runat="server" ID="chkIsCasemixApproved" Text="Casemix Approved" />
                        <asp:CheckBox runat="server" ID="chkIsVoid" Text="Casemix Approved" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="20px" />
        <td />
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblFornasRestrictionNotes" runat="server" Text="" ForeColor="OrangeRed" Font-Italic="true"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="TransPrescriptionItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>' OnClientClick="if(IsShortClick('u')) return;" />
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="TransPrescriptionItem" Visible='<%# DataItem is GridInsertionObject %>'
                OnClientClick="if(IsShortClick('i')) return;" />
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel" OnClientClick="if(IsShortClick('c')) return;" />
        </td>
    </tr>
</table>
